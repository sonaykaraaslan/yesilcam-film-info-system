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

namespace SinemaBilgiSistemi
{
    public partial class yListeAraci : UserControl
    {
        public yListeAraci()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5433;  Database=Sinema; user ID=postgres; password=1234");
        private void label1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("insert into filmoyuncu(film_id) values (@p1) ",baglanti);
            komut1.Parameters.AddWithValue("@p1", label1.Text);
            komut1.ExecuteNonQuery();
            baglanti.Close();
        }
    }
}
