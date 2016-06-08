using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace _15253070P
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source =.; Initial Catalog = BankaOtomasyon; Integrated Security = True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT password FROM KullaniciTBL where username='"+textBox1.Text+"'", conn);
            SqlDataReader rd = cmd.ExecuteReader();
            int girdiMi = 0;
            while (rd.Read())
            {
                if (rd["password"].ToString() == textBox2.Text.ToString())
                {
                    Form2 form = new Form2();
                    form.Show();
                    this.Hide();
                    girdiMi = 1;
                }
                else
                {
                    MessageBox.Show("Hatali Kullanıcı Adı/Parola");
                }

            }
            if (girdiMi!=1)
            {
                MessageBox.Show("Hatali Kullanıcı Adı/Parola");
            }
            rd.Close();

        }

        
    }
}
