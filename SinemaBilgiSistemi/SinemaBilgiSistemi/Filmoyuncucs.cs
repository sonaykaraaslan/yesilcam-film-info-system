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
    public partial class Filmoyuncucs : Form
    {
        int selectedOyuncuID=0;
        int selectedFilmID=0;
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5433;  Database=Sinema; user ID=postgres; password=1234");

        public Filmoyuncucs()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        public void kayitlarigetir()
        {

            string getirOyuncular = "select oyuncu_id,gercek_ad,gercek_soyad from oyuncular";
            string getirFilmler = "select film_id,film_ad from filmler";
            
            baglanti.Open();
            using(NpgsqlCommand komut = new NpgsqlCommand(getirOyuncular, baglanti))
            {
                using(NpgsqlDataReader reader = komut.ExecuteReader())
                {
                    comboBox1.Items.Clear();

                    while (reader.Read())
                    {
                        int oyuncuID = Convert.ToInt32(reader["oyuncu_id"]);
                        string oyuncuAdi = reader["gercek_ad"].ToString()+" " + reader["gercek_soyad"].ToString();

                        comboBox1.Items.Add(new OyuncuItem { ID=oyuncuID, Adi=oyuncuAdi });
                    }
                }
            }

            using (NpgsqlCommand komut = new NpgsqlCommand(getirFilmler, baglanti))
            {
                using (NpgsqlDataReader reader = komut.ExecuteReader())
                {
                    comboBox2.Items.Clear();

                    while (reader.Read())
                    {
                        int filmID = Convert.ToInt32(reader["film_id"]);
                        string filmAdi = reader["film_ad"].ToString();

                        comboBox2.Items.Add(new FilmItem { ID = filmID, Adi = filmAdi });
                    }
                }
            }


           
            baglanti.Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender,EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                selectedOyuncuID=((OyuncuItem)comboBox1.SelectedItem).ID;
                MessageBox.Show(selectedOyuncuID.ToString());
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex != -1)
            {
                selectedFilmID = ((FilmItem)comboBox2.SelectedItem).ID;
                MessageBox.Show(selectedFilmID.ToString());
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            kayitlarigetir();
        }

       
        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            if (selectedOyuncuID != 0 && selectedFilmID != 0)
            {
                NpgsqlCommand kontrolOyuncuKomut = new NpgsqlCommand("SELECT COUNT(*) FROM public.filmoyuncu WHERE oyuncu_id = @p1 and film_id=@p2", baglanti);
                kontrolOyuncuKomut.Parameters.AddWithValue("@p1", selectedOyuncuID);
                kontrolOyuncuKomut.Parameters.AddWithValue("@p2", selectedFilmID);

                if (Convert.ToInt32(kontrolOyuncuKomut.ExecuteScalar())==0)
                {
                    if (textBox3.Text != "") 
                    {
                        NpgsqlCommand komut1 = new NpgsqlCommand("insert into filmoyuncu (oyuncu_id,film_id,odul_sayisi) values (@p2,@p3,@p4)", baglanti);
                        komut1.Parameters.AddWithValue("@p2", selectedOyuncuID);
                        komut1.Parameters.AddWithValue("@p3", selectedFilmID);
                        komut1.Parameters.AddWithValue("@p4", Convert.ToInt32(textBox3.Text));
                        komut1.ExecuteNonQuery();
                        MessageBox.Show("kayıt başarılı");
                    }
                    else
                    {
                        NpgsqlCommand komut1 = new NpgsqlCommand("insert into filmoyuncu (oyuncu_id,film_id,odul_sayisi) values (@p2,@p3,@p4)", baglanti);
                        komut1.Parameters.AddWithValue("@p2", selectedOyuncuID);
                        komut1.Parameters.AddWithValue("@p3", selectedFilmID);
                        komut1.Parameters.AddWithValue("@p4", 0);
                        komut1.ExecuteNonQuery();
                        MessageBox.Show("ödül sayısı boş bırakıldığı için 0 olarak atandı");
                    }

                }
                else
                {
                    if (textBox3.Text != "")
                    {
                        NpgsqlCommand komut1 = new NpgsqlCommand("update filmoyuncu set odul_sayisi=@p4 where oyuncu_id=@p2 and film_id=@p3", baglanti);
                        komut1.Parameters.AddWithValue("@p2", selectedOyuncuID);
                        komut1.Parameters.AddWithValue("@p3", selectedFilmID);
                        komut1.Parameters.AddWithValue("@p4", Convert.ToInt32(textBox3.Text));
                        komut1.ExecuteNonQuery();
                        MessageBox.Show("Oyuncu bu film için mevcut, ödül sayısı güncellendi");
                    }
                    else
                    {
                        NpgsqlCommand komut1 = new NpgsqlCommand("update filmoyuncu set odul_sayisi=@p4 where oyuncu_id=@p2 and film_id=@p3", baglanti);
                        komut1.Parameters.AddWithValue("@p2", selectedOyuncuID);
                        komut1.Parameters.AddWithValue("@p3", selectedFilmID);
                        komut1.Parameters.AddWithValue("@p4", 0);
                        komut1.ExecuteNonQuery();
                        MessageBox.Show("Oyuncu bu film için mevcut, ödül sayısı boş bırakıldığı için 0 olarak güncellendi");
                    }

                }
            }
            else
            {
                MessageBox.Show("film veya oyuncu seçimi boş");
            }
            baglanti.Close();

        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Filmoyuncucs_Load(object sender, EventArgs e)
        {
            kayitlarigetir();
        }
    }
}

public class OyuncuItem
{
    public int ID { get; set; }
    public string Adi { get; set; }

    public override string ToString()
    {
        return Adi;
    }
}

public class FilmItem
{
    public int ID { get; set; }
    public string Adi { get; set; }

    public override string ToString()
    {
        return Adi;
    }
}
