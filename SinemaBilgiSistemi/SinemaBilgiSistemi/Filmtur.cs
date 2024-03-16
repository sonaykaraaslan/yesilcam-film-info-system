using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SinemaBilgiSistemi
{
    public partial class Filmtur : Form
    {
      
        public Filmtur()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5433;  Database=Sinema; user ID=postgres; password=1234");
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            NpgsqlCommand komut1 = new NpgsqlCommand("insert into filmtur (tur_ad) values (@p2)", baglanti);
            
            komut1.Parameters.AddWithValue("@p2", textBox2.Text);
           
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("yeni kayıt tamamlandı");

        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
