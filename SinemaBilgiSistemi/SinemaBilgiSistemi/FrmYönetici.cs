using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SinemaBilgiSistemi
{
    public partial class FrmYönetici : Form
    {
        int id;
        public FrmYönetici()
        {
            InitializeComponent();
        }
        

        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5433;  Database=Sinema; user ID=postgres; password=1234");
        

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void button3_Click(object sender, EventArgs e)//ekleme
        {
            baglanti.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("insert into yonetmenler (gercek_ad,gercek_soyad,sahne_ad,sahne_soyad,dogum_tarih,yonetmen_cinsiyet,yonetmen_burc) values (@p2,@p3,@p4,@p5,@p6,@p7,@p8)", baglanti);
            
            komut1.Parameters.AddWithValue("@p2", textBox2.Text);
            komut1.Parameters.AddWithValue("@p3", textBox3.Text);
            komut1.Parameters.AddWithValue("@p4", textBox4.Text);
            komut1.Parameters.AddWithValue("@p5", textBox5.Text);
            komut1.Parameters.AddWithValue("@p6", dateTimePicker1.Value.Date);
            komut1.Parameters.AddWithValue("@p7", textBox6.Text);
            komut1.Parameters.AddWithValue("@p8", textBox7.Text);
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("yeni kayıt tamamlandı");
            temizle();
        }

       
       
        private void button6_Click(object sender, EventArgs e)//listeleme
        {
            string sorgu = "select * from yonetmenler order by yonetmen_id";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds;
            dataGridView1.DataSource = ds.Tables[0];
        }

        
        private void button4_Click(object sender, EventArgs e)
        {
            

            NpgsqlCommand komut2 = new NpgsqlCommand("DELETE FROM yonetmenler WHERE yonetmen_id=@id", baglanti);
            komut2.Parameters.AddWithValue("@id", id);
            baglanti.Open();
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show(" Silinmiştir");



        }


        
        

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            NpgsqlCommand komut3 = new NpgsqlCommand("update yonetmenler set gercek_ad=@p2,gercek_soyad=@p3,sahne_ad=@p4,sahne_soyad=@p5,dogum_tarih=@p6,yonetmen_cinsiyet=@p7,yonetmen_burc=@p8 where yonetmen_id=@id ", baglanti);
            komut3.Parameters.AddWithValue("@p2", textBox2.Text);
            komut3.Parameters.AddWithValue("@p3", textBox3.Text);
            komut3.Parameters.AddWithValue("@p4", textBox4.Text);
            komut3.Parameters.AddWithValue("@p5", textBox5.Text);
            komut3.Parameters.AddWithValue("@p6", dateTimePicker1.Value.Date);
            komut3.Parameters.AddWithValue("@p7", textBox6.Text);
            komut3.Parameters.AddWithValue("@p8", textBox7.Text);
            komut3.Parameters.AddWithValue("@id", id);
           
            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncellendi");
            
        }

        


        

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }
        void temizle()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text= string.Empty;
            dateTimePicker1.Text= string.Empty; 
            textBox6.Text= string.Empty;
            textBox7.Text= string.Empty;


        }
        private void button2_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void FrmYönetici_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
        }
    }
    
}
