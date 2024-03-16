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
    public partial class FrmOyuncu : Form
    {
        int id;
        public FrmOyuncu()
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
            
            string getir = "select * from oyuncular";
            NpgsqlCommand komut=new NpgsqlCommand(getir,baglanti);
            NpgsqlDataAdapter da= new NpgsqlDataAdapter(komut);
            DataTable dt= new DataTable();  
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();

        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("insert into oyuncular (gercek_ad,gercek_soyad,sahne_ad,sahne_soyad,dogum_tarih,oyuncu_cinsiyet,oyuncu_burc) values (@p2,@p3,@p4,@p5,@p6,@p7,@p8)", baglanti);
            
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
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();   
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }
        
        private void button6_Click(object sender, EventArgs e)
        {
            kayitlarigetir();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
                NpgsqlCommand komut2 = new NpgsqlCommand("DELETE FROM oyuncular WHERE oyuncu_id=@p1", baglanti);
                komut2.Parameters.AddWithValue("@p1",id);
                                            
                   baglanti.Open();
                   komut2.ExecuteNonQuery();
                   baglanti.Close();
                   MessageBox.Show(" Silinmiştir");
                   textBox2.Clear();
                   textBox3.Clear();
                   textBox5.Clear();
                   textBox6.Clear();
                   textBox7.Clear();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            NpgsqlCommand komut3 = new NpgsqlCommand("update oyuncular set gercek_ad=@p2,gercek_soyad=@p3,sahne_ad=@p4,sahne_soyad=@p5,dogum_tarih=@p6,oyuncu_cinsiyet=@p7,oyuncu_burc=@p8 where oyuncu_id=@p1 ", baglanti);
           
            komut3.Parameters.AddWithValue("@p2", textBox2.Text);
            komut3.Parameters.AddWithValue("@p3", textBox3.Text);
            komut3.Parameters.AddWithValue("@p4", textBox4.Text);
            komut3.Parameters.AddWithValue("@p5", textBox5.Text);
            komut3.Parameters.AddWithValue("@p6", dateTimePicker1.Value.Date);
            komut3.Parameters.AddWithValue("@p7", textBox6.Text);
            komut3.Parameters.AddWithValue("@p8", textBox7.Text);
            komut3.Parameters.AddWithValue("@p1",id);

            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncellendi");
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();

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

        private void FrmOyuncu_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    }

