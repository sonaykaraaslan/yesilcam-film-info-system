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
    public partial class Filmkayit : Form
    {
        int id;
        public Filmkayit()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5433;  Database=Sinema; user ID=postgres; password=1234");
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void kayitlarigetir()
        {

            string getir = "select * from filmler";
            NpgsqlCommand komut = new NpgsqlCommand(getir, baglanti);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();

        }
        void temizle()
        {
            textBox7.Text =string.Empty;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            dateTimePicker1.Text = string.Empty;
            textBox6.Text = string.Empty;
            richTextBox1.Text = string.Empty;


        }
        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            int turid;
            if (!int.TryParse(textBox6.Text, out turid))
            {
                MessageBox.Show("Geçerli bir tur ID giriniz.");
                baglanti.Close();
                return;
            }
            NpgsqlCommand kontrolKomut = new NpgsqlCommand("SELECT COUNT(*) FROM public.filmtur WHERE tur_id = @p8", baglanti);
            kontrolKomut.Parameters.AddWithValue("@p8", turid);

            int turIdSayisi = Convert.ToInt32(kontrolKomut.ExecuteScalar());

            if (turIdSayisi == 0)
            {
                MessageBox.Show("Belirtilen tur ID referans tablosunda mevcut değil.");
                baglanti.Close();
                return;
            }
            NpgsqlCommand komut1 = new NpgsqlCommand("insert into filmler (film_ad,film_rating,film_saat,film_butce,film_gise,film_vizyon,tur_id,film_detay,film_afis) values (@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", baglanti);
            
            komut1.Parameters.AddWithValue("@p2", textBox1.Text);
            komut1.Parameters.AddWithValue("@p3", int.Parse(textBox2.Text));//rating
            komut1.Parameters.AddWithValue("@p4", textBox3.Text);//saat
            komut1.Parameters.AddWithValue("@p5", double.Parse(textBox4.Text));//butce
            komut1.Parameters.AddWithValue("@p6", double.Parse(textBox5.Text));
            komut1.Parameters.AddWithValue("@p7", dateTimePicker1.Value.Date);
            komut1.Parameters.AddWithValue("@p8", turid);
            komut1.Parameters.AddWithValue("@p9", richTextBox1.Text);
            komut1.Parameters.AddWithValue("@p10",resimyolu );
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("yeni kayıt tamamlandı");
            temizle();
        }






        private void button5_Click(object sender, EventArgs e)
        {
            kayitlarigetir();
            temizle();
        }
        public string resimyolu = "";
        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog openPicture = new OpenFileDialog();
            openPicture.Title = "RESİM SEÇ";
            openPicture.Filter = "PNG|*.png|JPG|*.jpg;*.jpeg |All files (*.*)|*.*";
            openPicture.FilterIndex = 3;
            if (openPicture.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openPicture.FileName);
                resimyolu = openPicture.FileName.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NpgsqlCommand komut2 = new NpgsqlCommand("DELETE FROM filmler  WHERE film_id=@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", id);

            baglanti.Open();
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show(" Silinmiştir");
            temizle();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            richTextBox1.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
        }

        private void Filmkayit_Load(object sender, EventArgs e)
        {
            textBox7.ReadOnly = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            int turid;
            if (!int.TryParse(textBox6.Text, out turid))
            {
                MessageBox.Show("Geçerli bir tur ID giriniz.");
                baglanti.Close();
                return;
            }
            NpgsqlCommand kontrolKomut = new NpgsqlCommand("SELECT COUNT(*) FROM public.filmtur WHERE tur_id = @p8", baglanti);
            kontrolKomut.Parameters.AddWithValue("@p8", turid);

            int turIdSayisi = Convert.ToInt32(kontrolKomut.ExecuteScalar());

            if (turIdSayisi == 0)
            {
                MessageBox.Show("Belirtilen tur ID referans tablosunda mevcut değil.");
                baglanti.Close();
                return;
            }

            NpgsqlCommand komut3 = new NpgsqlCommand("update filmler set film_ad=@p2,film_rating=@p3,film_saat=@p4,film_butce=@p5,film_gise=@p6,film_vizyon=@p7,tur_id=@p8,film_detay=@p9,film_afis=@p10 where film_id=@p1 ", baglanti);

            komut3.Parameters.AddWithValue("@p2", textBox1.Text);
            komut3.Parameters.AddWithValue("@p3", int.Parse(textBox2.Text));//rating
            komut3.Parameters.AddWithValue("@p4", textBox3.Text);//saat
            komut3.Parameters.AddWithValue("@p5", int.Parse(textBox4.Text));//butce
            komut3.Parameters.AddWithValue("@p6", int.Parse(textBox5.Text));
            komut3.Parameters.AddWithValue("@p7", dateTimePicker1.Value.Date);
            komut3.Parameters.AddWithValue("@p8", turid);
            komut3.Parameters.AddWithValue("@p9", richTextBox1.Text);
            komut3.Parameters.AddWithValue("@p10", resimyolu);
            komut3.Parameters.AddWithValue("@p1", id);

            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncellendi");
        }
    }
}
