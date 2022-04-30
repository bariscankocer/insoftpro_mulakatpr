using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb; // Access bağlantısı kurabilmek için.



namespace insoftpro_Projesi
{
    public partial class Abone_Tanımlama : Form
    {
        public Abone_Tanımlama()
        {
            InitializeComponent();
        }
        OleDbConnection con;

        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
        private void Abone_Tanımlama_Load(object sender, EventArgs e)
        {
            griddoldur();

        }
        public string numara;
        public string tarih;
        public string adres;
        public string ad;
        public string soyad;
        public string para_birimi;
        public string KDV_oranı;

     public void veriekle()
        {
            numara = textBox1.Text;
            adres = textBox3.Text;
            ad = textBox4.Text;
            soyad = textBox5.Text;
            tarih = monthCalendar1.SelectionStart.ToString("d");
            para_birimi = comboBox1.SelectedItem.ToString();
            KDV_oranı = comboBox2.SelectedItem.ToString();

            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.16.0;Data Source=aboneler.accdb");
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "insert into Tanımlanmış_aboneler (Numara,Açılış_tarih,Adres,Ad,Soyad,Para_birimi,KDV_oranı) values ('" + numara + "','" + monthCalendar1.SelectionStart.ToString("d") + "','" + adres + "','" + ad + "','" + soyad + "','" + comboBox1.SelectedItem.ToString() + "','" + comboBox2.SelectedItem.ToString() + "')";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("kaydınız başarıyla yapılmıştır");
            textBox1.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

        }
        public void verigüncelle() 
        {
            numara = textBox1.Text;
            adres = textBox3.Text;
            ad = textBox4.Text;
            soyad = textBox5.Text;
            tarih = monthCalendar1.SelectionStart.ToString("d");
            para_birimi = comboBox1.SelectedItem.ToString();
            KDV_oranı = comboBox2.SelectedItem.ToString();
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "update Tanımlanmış_aboneler set Açılış_tarih='" + monthCalendar1.SelectionStart.ToString("d") + "',Adres='" + adres + "',Ad='" + ad + "',Soyad='" + soyad + "',Para_birimi='" + comboBox1.SelectedItem.ToString() + "',KDV_oranı='" + comboBox2.SelectedItem.ToString() + "'where Numara=" + textBox1.Text + "";
            cmd.ExecuteNonQuery();
            con.Close();
            griddoldur();

        }
        public  void griddoldur()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.16.0;Data Source=aboneler.accdb");

            da = new OleDbDataAdapter("Select * from Tanımlanmış_aboneler", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "Tanımlanmış_aboneler");
            dataGridView1.DataSource = ds.Tables["Tanımlanmış_aboneler"];
            con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            veriekle();
            griddoldur();
        }
      
        private void oleDbConnection1_InfoMessage(object sender, OleDbInfoMessageEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            verigüncelle();
           
        }
    }
}
