using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace insoftpro_Projesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            
            Abone_Tanımlama abonetanımlama = new Abone_Tanımlama();
            abonetanımlama.StartPosition = FormStartPosition.CenterScreen;
            abonetanımlama.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            endeks_hesaplama endekshesaplama = new endeks_hesaplama();
            endekshesaplama.StartPosition = FormStartPosition.CenterScreen;
            endekshesaplama.Show();
            
        }
    }
}
