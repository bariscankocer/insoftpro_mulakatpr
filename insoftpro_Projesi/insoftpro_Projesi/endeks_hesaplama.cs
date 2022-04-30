using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Xml;


namespace insoftpro_Projesi
{
    public partial class endeks_hesaplama : Form
    {
        public endeks_hesaplama()
        {
            InitializeComponent();
        }
        OleDbConnection con;

        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
        public void griddolddur()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.16.0;Data Source=aboneler.accdb");

            da = new OleDbDataAdapter("Select * from Tanımlanmış_aboneler", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "Tanımlanmış_aboneler");
            dataGridView1.DataSource = ds.Tables["Tanımlanmış_aboneler"];
            con.Close();
        }
        public void gecmisdoviz()
        {
            string[] parçala;
            parçala = label7.Text.Split('.');
            string yıldeğeri = parçala[2];
            string aydeğeri = parçala[1];
            string gundeğeri = parçala[0];
            string tarihhepsi = label7.Text.ToString();
            try
            {
                XmlDocument xmlVerisi = new XmlDocument();
                string tarih = label7.Text.ToString();
                string sondeger = tarih.Split('.').Last();

                string xmlismi = "http://www.tcmb.gov.tr/kurlar/" + yıldeğeri + aydeğeri + "/" + gundeğeri + aydeğeri + yıldeğeri + ".xml";
                xmlVerisi.Load(xmlismi);

                decimal dolar = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "USD")).InnerText.Replace('.', ','));
                decimal euro = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "EUR")).InnerText.Replace('.', ','));
                decimal sterlin = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "GBP")).InnerText.Replace('.', ','));

                lblDolar.Text = dolar.ToString();
                lblEuro.Text = euro.ToString();
                lblSterlin.Text = sterlin.ToString();
            }
            catch (XmlException xml)
            {
                timer1.Stop();
                MessageBox.Show(xml.ToString());
            }

        }
        public void gunlukdoviz()
        {
            string[] parçala;
            parçala = label7.Text.Split('.');
            string yıldeğeri = parçala[2];
            string aydeğeri = parçala[1];
            string gundeğeri = parçala[0];
            string tarihhepsi = label7.Text.ToString();
            try
            {
                XmlDocument xmlVerisi = new XmlDocument();
                string tarih = label7.Text.ToString();
                string sondeger = tarih.Split('.').Last();

                string xmlismi = "http://www.tcmb.gov.tr/kurlar/today.xml";
                xmlVerisi.Load(xmlismi);

                decimal gunlukdolar = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "USD")).InnerText.Replace('.', ','));
                decimal gunlukeuro = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "EUR")).InnerText.Replace('.', ','));
                decimal gunluksterlin = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "GBP")).InnerText.Replace('.', ','));

                label18.Text = gunluksterlin.ToString();
                label17.Text = gunlukeuro.ToString();
                label16.Text = gunlukdolar.ToString();
            }
            catch (XmlException xml)
            {
                timer1.Stop();
                MessageBox.Show(xml.ToString());
            }

        }
        public int son_endeks;
        int değişken;
        // string ilkendeks;
        public void ilkindeksgetir()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.16.0;Data Source=endekshesaplama.accdb");

            da = new OleDbDataAdapter("Select ilk_endeks from endeks_hesaplama where numara ='" + label6.Text + "'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "endeks_hesaplama");
            dataGridView2.DataSource = ds.Tables["endeks_hesaplama"];
            //değişken = Convert.ToInt32(dataGridView2.Rows.ToString());
            // değişken = dataGridView2.Rows[0].Cells[1].Value.ToString();
            string colnum;
            colnum = dataGridView2.Columns[0].Name;
            label20.Text = dataGridView2.Rows[0].Cells[colnum].Value.ToString();

            con.Close();
        }

        public void ilkendekskontrol()
        {
            string tarih = monthCalendar1.SelectionStart.ToString("d");

            // string sonindeks;
            string numara = label6.Text;



            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.16.0;Data Source=endekshesaplama.accdb");

            da = new OleDbDataAdapter("select ilk_endeks  from endeks_hesaplama where numara='" + numara + "'", con);

            ds = new DataSet();
            con.Open();
            da.Fill(ds, "endeks_hesaplama");

            dataGridView2.DataSource = ds.Tables["endeks_hesaplama"];


            // int no = convert.toint32(datatableAdi.rows[0]["ogrNo"])
            try
            {
                değişken = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value.ToString());

            }
            catch (Exception)
            {
                MessageBox.Show("numara endeks verisi bulunamamıştır");

            }

            //  MessageBox.Show(ilk_endeks.ToString());
            con.Close();

        }
        public void sonendeks()
        {
            string tarih = monthCalendar1.SelectionStart.ToString("d");

            // string sonindeks;
            string numara = label6.Text;



            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.16.0;Data Source=endekshesaplama.accdb");

            da = new OleDbDataAdapter("select son_endeks  from endeks_hesaplama where numara='" + numara + "'", con);

            ds = new DataSet();
            con.Open();
            da.Fill(ds, "endeks_hesaplama");

            dataGridView2.DataSource = ds.Tables["endeks_hesaplama"];


            // int no = convert.toint32(datatableAdi.rows[0]["ogrNo"])
            try
            {
                son_endeks = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value.ToString());

            }
            catch (Exception)
            {
                MessageBox.Show("numara endeks verisi bulunamamıştır");

            }

            //  MessageBox.Show(ilk_endeks.ToString());
            con.Close();

        }


        public void ilkendeksguncelle()
        {



            string numara = label6.Text;

            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.16.0;Data Source=endekshesaplama.accdb");
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "update endeks_hesaplama set ilk_endeks = '" + textBox1.Text + "' where numara = '" + numara + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("kaydınız başarıyla yapılmıştır" + textBox1.Text);


        }
        public void sonindeksgüncelle()
        {



            string numara = label6.Text;

            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.16.0;Data Source=endekshesaplama.accdb");
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "update endeks_hesaplama set son_endeks = '" + textBox1.Text + "' where numara = '" + numara + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("son indeks kaydınız "+ textBox1.Text + "m3 olarak  başarıyla yapılmıştır" );


        }


        private void endeks_hesaplama_Load(object sender, EventArgs e)
        {
            griddolddur();
            timer1.Start();
            label6.Hide();
            label7.Hide();
            label8.Hide();
            label9.Hide();
            label10.Hide();
            label13.Hide();
            label14.Hide();
        }
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            label6.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString(); //[0] sütun numarası
            label6.Show();
            label7.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            label7.Show();
            label8.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString(); //[0] sütun numarası
            label8.Show();
            label9.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            label9.Show();
            label10.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString(); //[0] sütun numarası
            label10.Show();
            label13.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            label13.Show();
            label14.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString(); //[0] sütun numarası
            label14.Show();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            gunlukdoviz();
            gecmisdoviz();
            ilkendekskontrol();

            if (label13.Text == "USD")
            {

                decimal geçmişdolar = Convert.ToDecimal(lblDolar.Text);
                decimal anlıkdolar = Convert.ToDecimal(label16.Text);
                decimal sonuç = anlıkdolar - geçmişdolar;
                sonuç = sonuç / geçmişdolar;
                decimal kursonuç = sonuç * 100;
                decimal subedeli = Convert.ToDecimal(textBox1.Text);
                subedeli = subedeli * 2;                           /////////////////////////////// 1 m3 suyun tl cinsinden değeri 2 birim olarak hesaplandı
                subedeli = subedeli / 100;
                decimal kurlusubedeli = subedeli * kursonuç;
                if (label14.Text == "%1")
                {
                    decimal vergisi = kurlusubedeli / 100;
                    decimal birvergilisubedeli = kurlusubedeli + vergisi;
                    MessageBox.Show("%1 vergi ve geçen zamanın kur oranına göre hesaplanmış borcunuz:   " + birvergilisubedeli.ToString());
                }
                if (label14.Text == "%8")
                {
                    decimal vergisi = kurlusubedeli / 100; ;
                    vergisi = vergisi * 8;
                    decimal sekizvergilisubedeli = kurlusubedeli + vergisi;
                    MessageBox.Show("%8 vergi ve geçen zamanın kur oranına göre hesaplanmış borcunuz:   " + sekizvergilisubedeli.ToString());

                }
                if (label14.Text == "%18")
                {
                    decimal vergisi = kurlusubedeli / 100; ;
                    vergisi = vergisi * 18;
                    decimal onsekizvergilisubedeli = kurlusubedeli + vergisi;
                    MessageBox.Show("%18 vergi ve geçen zamanın kur oranına göre hesaplanmış borcunuz:   " + onsekizvergilisubedeli.ToString());

                }
            }
            if (label13.Text == "EURO")
            {
                decimal geçmişeuro = Convert.ToDecimal(lblEuro.Text);
                decimal anlıkeuro = Convert.ToDecimal(label17.Text);
                decimal sonuç = anlıkeuro - geçmişeuro;
                sonuç = sonuç / geçmişeuro;
                decimal kursonuç = sonuç * 100;
                decimal subedeli = Convert.ToDecimal(textBox1.Text);
                subedeli = subedeli * 2;                           /////////////////////////////// 1 m3 suyun tl cinsinden değeri 2 birim olarak hesaplandı
                subedeli = subedeli / 100;
                decimal kurlusubedeli = subedeli * kursonuç;
                if (label14.Text == "%1")
                {
                    decimal vergisi = kurlusubedeli / 100;
                    decimal birvergilisubedeli = kurlusubedeli + vergisi;
                    MessageBox.Show("%1 vergi ve geçen zamanın kur oranına göre hesaplanmış borcunuz:   " + birvergilisubedeli.ToString());
                }
                if (label14.Text == "%8")
                {
                    decimal vergisi = kurlusubedeli / 100; ;
                    vergisi = vergisi * 8;
                    decimal sekizvergilisubedeli = kurlusubedeli + vergisi;
                    MessageBox.Show("%8 vergi ve geçen zamanın kur oranına göre hesaplanmış borcunuz:   " + sekizvergilisubedeli.ToString());

                }
                if (label14.Text == "%18")
                {
                    decimal vergisi = kurlusubedeli / 100; ;
                    vergisi = vergisi * 18;
                    decimal onsekizvergilisubedeli = kurlusubedeli + vergisi;
                    MessageBox.Show("%18 vergi ve geçen zamanın kur oranına göre hesaplanmış borcunuz:   " + onsekizvergilisubedeli.ToString());

                }




            }
            if (label13.Text == "İNGİLİZ STERLİNİ")
            {
                decimal geçmişsterlin = Convert.ToDecimal(lblSterlin.Text);
                decimal anlıksterlin = Convert.ToDecimal(label18.Text);
                decimal sonuç = anlıksterlin - geçmişsterlin;
                sonuç = sonuç / geçmişsterlin;
                decimal kursonuç = sonuç * 100;
                decimal subedeli = Convert.ToDecimal(textBox1.Text);
                subedeli = subedeli * 2;                           /////////////////////////////// 1 m3 suyun tl cinsinden değeri 2 birim olarak hesaplandı
                subedeli = subedeli / 100;
                decimal kurlusubedeli = subedeli * kursonuç;
                if (label14.Text == "%1")
                {
                    decimal vergisi = kurlusubedeli / 100;
                    decimal birvergilisubedeli = kurlusubedeli + vergisi;
                    MessageBox.Show("%1 vergi ve geçen zamanın kur oranına göre hesaplanmış borcunuz:   " + birvergilisubedeli.ToString());
                }
                else if (label14.Text == "%8")
                {
                    decimal vergisi = kurlusubedeli / 100; ;
                    vergisi = vergisi * 8;
                    decimal sekizvergilisubedeli = kurlusubedeli + vergisi;
                    MessageBox.Show("%8 vergi ve geçen zamanın kur oranına göre hesaplanmış borcunuz:   " + sekizvergilisubedeli.ToString());

                }
                else if (label14.Text == "%18")
                {
                    decimal vergisi = kurlusubedeli / 100; ;
                    vergisi = vergisi * 18;
                    decimal onsekizvergilisubedeli = kurlusubedeli + vergisi;
                    MessageBox.Show("%18 vergi ve geçen zamanın kur oranına göre hesaplanmış borcunuz:   " + onsekizvergilisubedeli.ToString());

                }

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            ilkindeksgetir();
            gunlukdoviz();
            gecmisdoviz();

            int label = Convert.ToInt32(label20.Text);


            if (label == Convert.ToInt32("0"))
            {
                ilkendeksguncelle();
                MessageBox.Show("ilk endeks kaydınız güncellenmiştir");

            }

            else
            {
                sonindeksgüncelle();
               

            }
            int sonuç;
            sonuç = son_endeks - label;


            if (label13.Text == "USD")
            {

                decimal geçmişdolar = Convert.ToDecimal(lblDolar.Text);
                decimal anlıkdolar = Convert.ToDecimal(label16.Text);
                decimal sonuçkur = anlıkdolar - geçmişdolar;
                sonuçkur = sonuçkur / geçmişdolar;
                decimal kursonuç = sonuçkur * 100;
                decimal subedeli = Convert.ToDecimal(sonuç);
                subedeli = subedeli * 2;                           /////////////////////////////// 1 m3 suyun tl cinsinden değeri 2 birim olarak hesaplandı
                subedeli = subedeli / 100;
                decimal kurlusubedeli = subedeli * kursonuç;
                if (label14.Text == "%1")
                {
                    decimal vergisi = kurlusubedeli / 100;
                    decimal birvergilisubedeli = kurlusubedeli + vergisi;
                    MessageBox.Show("%1 vergi ve geçen zamanın kur oranına göre hesaplanmış borcunuz:   " + birvergilisubedeli.ToString());
                }
                if (label14.Text == "%8")
                {
                    decimal vergisi = kurlusubedeli / 100; ;
                    vergisi = vergisi * 8;
                    decimal sekizvergilisubedeli = kurlusubedeli + vergisi;
                    MessageBox.Show("%8 vergi ve geçen zamanın kur oranına göre hesaplanmış borcunuz:   " + sekizvergilisubedeli.ToString());

                }
                if (label14.Text == "%18")
                {
                    decimal vergisi = kurlusubedeli / 100; ;
                    vergisi = vergisi * 18;
                    decimal onsekizvergilisubedeli = kurlusubedeli + vergisi;
                    MessageBox.Show("%18 vergi ve geçen zamanın kur oranına göre hesaplanmış borcunuz:   " + onsekizvergilisubedeli.ToString());

                }
            }
            if (label13.Text == "EURO")
            {
                decimal geçmişeuro = Convert.ToDecimal(lblEuro.Text);
                decimal anlıkeuro = Convert.ToDecimal(label17.Text);
                decimal sonuçkur = anlıkeuro - geçmişeuro;
                sonuçkur = sonuçkur / geçmişeuro;
                decimal kursonuç = sonuçkur * 100;
                decimal subedeli = Convert.ToDecimal(sonuç);
                subedeli = subedeli * 2;                           /////////////////////////////// 1 m3 suyun tl cinsinden değeri 2 birim olarak hesaplandı
                subedeli = subedeli / 100;
                decimal kurlusubedeli = subedeli * kursonuç;
                if (label14.Text == "%1")
                {
                    decimal vergisi = kurlusubedeli / 100;
                    decimal birvergilisubedeli = kurlusubedeli + vergisi;
                    MessageBox.Show("%1 vergi ve geçen zamanın kur oranına göre hesaplanmış borcunuz:   " + birvergilisubedeli.ToString());
                }
                if (label14.Text == "%8")
                {
                    decimal vergisi = kurlusubedeli / 100; ;
                    vergisi = vergisi * 8;
                    decimal sekizvergilisubedeli = kurlusubedeli + vergisi;
                    MessageBox.Show("%8 vergi ve geçen zamanın kur oranına göre hesaplanmış borcunuz:   " + sekizvergilisubedeli.ToString());

                }
                if (label14.Text == "%18")
                {
                    decimal vergisi = kurlusubedeli / 100; ;
                    vergisi = vergisi * 18;
                    decimal onsekizvergilisubedeli = kurlusubedeli + vergisi;
                    MessageBox.Show("%18 vergi ve geçen zamanın kur oranına göre hesaplanmış borcunuz:   " + onsekizvergilisubedeli.ToString());

                }



            }
            if (label13.Text == "İNGİLİZ STERLİNİ")
            {
                decimal geçmişsterlin = Convert.ToDecimal(lblSterlin.Text);
                decimal anlıksterlin = Convert.ToDecimal(label18.Text);
                decimal sonuçkur = anlıksterlin - geçmişsterlin;
                sonuçkur = sonuçkur / geçmişsterlin;
                decimal kursonuç = sonuçkur * 100;
                decimal subedeli = Convert.ToDecimal(sonuç);
                subedeli = subedeli * 2;                           /////////////////////////////// 1 m3 suyun tl cinsinden değeri 2 birim olarak hesaplandı
                subedeli = subedeli / 100;
                decimal kurlusubedeli = subedeli * kursonuç;
                if (label14.Text == "%1")
                {
                    decimal vergisi = kurlusubedeli / 100;
                    decimal birvergilisubedeli = kurlusubedeli + vergisi;
                    MessageBox.Show("%1 vergi ve geçen zamanın kur oranına göre hesaplanmış borcunuz:   " + birvergilisubedeli.ToString());
                }
                else if (label14.Text == "%8")
                {
                    decimal vergisi = kurlusubedeli / 100; ;
                    vergisi = vergisi * 8;
                    decimal sekizvergilisubedeli = kurlusubedeli + vergisi;
                    MessageBox.Show("%8 vergi ve geçen zamanın kur oranına göre hesaplanmış borcunuz:   " + sekizvergilisubedeli.ToString());

                }
                else if (label14.Text == "%18")
                {
                    decimal vergisi = kurlusubedeli / 100; ;
                    vergisi = vergisi * 18;
                    decimal onsekizvergilisubedeli = kurlusubedeli + vergisi;
                    MessageBox.Show("%18 vergi ve geçen zamanın kur oranına göre hesaplanmış borcunuz:   " + onsekizvergilisubedeli.ToString());

                }

            }
        }
    }
}
