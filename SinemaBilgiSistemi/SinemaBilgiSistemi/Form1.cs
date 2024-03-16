using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SinemaBilgiSistemi
{
    public partial class form1 : Form
    {
        bool sidebarExpand;
        public form1()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5433;  Database=Sinema; user ID=postgres; password=1234");
        
        

        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
            if(sidebarExpand)
            {
                sidebar.Width -= 10;
                if(sidebar.Width==sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sidebarTimer.Stop();
                }
            }
            else
                sidebar.Width +=10;
                if (sidebar.Width == sidebar.MaximumSize.Width)
            {
                sidebarExpand = true;
                sidebarTimer.Stop();
            }
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            sidebarTimer.Start();
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            FrmYönetici frm=new FrmYönetici();
            frm.ShowDialog();
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmOyuncu frm=new FrmOyuncu();
            frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Filmkayit frm=new Filmkayit();
            frm.ShowDialog();   
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Filmtur frm=new Filmtur();  
            frm.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Filmoyuncucs frm=new Filmoyuncucs();
            frm.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Filmyönetmen frm= new Filmyönetmen();
            frm.ShowDialog();
        }
    }
}
