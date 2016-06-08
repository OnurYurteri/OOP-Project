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
using System.Collections;

namespace _15253070P // Para çekme, günlük para çekme limiti, ve işlemler tablosuna yazımı tamam. Sırada para yatırma ve havale var.
{
    public partial class Form2 : Form
    {
        
        public Form2()
        {
            
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.Show();
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked== true)
            {
                textBox5.Enabled = true;
            }
            else if(checkBox1.Checked == false)
            {
                textBox5.Enabled = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                textBox6.Enabled = true;
                textBox6.PasswordChar = '\0';
            }
            else if (checkBox2.Checked == false)
            {
                textBox6.Enabled = false;
                textBox6.PasswordChar = '•';
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(!(textBox2.Text==textBox3.Text)||textBox1.Text==""||textBox2.Text=="")
            {
                MessageBox.Show("Bilgileri Doğru Bir Şekilde Giriniz");
            }
            else
            {
                SqlConnection conn = new SqlConnection("Data Source =.; Initial Catalog = BankaOtomasyon; Integrated Security = True");
                conn.Open();
                SqlCommand uKontrol = new SqlCommand();
                uKontrol.Connection = conn;
                uKontrol.CommandText = "select username from KullaniciTBL where username='"+textBox1.Text+"'";
                SqlDataReader rd = uKontrol.ExecuteReader();
                if(rd.Read()!=false)
                {
                    MessageBox.Show("Böyle bir Kullanıcı Zaten Mevcut");
                }
                else
                {
                    rd.Close();
                    SqlCommand uYaz = new SqlCommand();
                    uYaz.Connection = conn;
                    uYaz.CommandText = "insert into KullaniciTBL (username,password) values ('"+textBox1.Text+"','"+textBox2.Text+"')";
                    uYaz.ExecuteNonQuery();
                    MessageBox.Show(textBox1.Text + " İsimli Kullanıcı Başarıyla Eklendi");
                    kullaniciYenile();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                }
            }


        }

        

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {


                SqlConnection conn = new SqlConnection("Data Source =.; Initial Catalog = BankaOtomasyon; Integrated Security = True");
                conn.Open();
                SqlCommand uGetirDegistir = new SqlCommand();
                uGetirDegistir.Connection = conn;
                uGetirDegistir.CommandText = "Select * from KullaniciTBL where id=" + ((Kullanicilar)comboBox1.SelectedItem).id;
                SqlDataReader uGetirDegistirRd = uGetirDegistir.ExecuteReader();
                while (uGetirDegistirRd.Read())
                {
                    textBox5.Text = uGetirDegistirRd["username"].ToString();
                    textBox6.Text = uGetirDegistirRd["password"].ToString();

                }
                textBox6.PasswordChar = '•';
                uGetirDegistirRd.Close();
            }
            catch(Exception)
            {
                MessageBox.Show("Kullanıcı Seçmediniz");
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if(textBox5.Text=="" || textBox6.Text=="")
                {
                    MessageBox.Show("Kullanıcı Adı veya Şifre Boş Olamaz");
                }
                else
                {
                    SqlConnection conn = new SqlConnection("Data Source =.; Initial Catalog = BankaOtomasyon; Integrated Security = True");
                    conn.Open();
                    SqlCommand uDegistir = new SqlCommand();
                    uDegistir.Connection = conn;
                    uDegistir.CommandText = "update KullaniciTBL Set username='" + textBox5.Text + "', password='" + textBox6.Text + "' where id=" + ((Kullanicilar)comboBox1.SelectedItem).id;
                    uDegistir.ExecuteNonQuery();
                    MessageBox.Show(textBox5.Text + " Adlı Kullanıcının Düzenlemesi Başarılı");
                    kullaniciYenile();
                }
                
            } 
                catch(Exception)
            {
                MessageBox.Show("Kullanıcı Seçmediniz");
            }
            
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source =.; Initial Catalog = BankaOtomasyon; Integrated Security = True");
                conn.Open();
                SqlCommand uKaldir = new SqlCommand();
                uKaldir.Connection = conn;
                uKaldir.CommandText = "delete from KullaniciTBL where id=" + ((Kullanicilar)comboBox2.SelectedItem).id;
                uKaldir.ExecuteNonQuery();
                MessageBox.Show(" Kullanıcı Başarıyla Kaldırıldı");
                kullaniciYenile();
            }
            catch(Exception)
            {
                MessageBox.Show("Kullanıcı Seçmediniz");
            }
            
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(textBox4.Text=="" || textBox7.Text==""|| richTextBox1.Text=="" || textBox8.Text=="" || textBox9.Text=="")
            {
                MessageBox.Show("Eksik Bilgi Bırakmayınız");
            }
            else
            {
                SqlConnection conn = new SqlConnection("Data Source =.; Initial Catalog = BankaOtomasyon; Integrated Security = True");
                conn.Open();
                SqlCommand mEkle = new SqlCommand();
                mEkle.Connection = conn;
                mEkle.CommandText = "insert into MusterilerTBL (ad,soyad,adres,telefon,email) values ('" + textBox4.Text + "','" + textBox7.Text + "','" + richTextBox1.Text + "','" + textBox8.Text + "','" + textBox9.Text + "')";
                mEkle.ExecuteNonQuery();
                MessageBox.Show("Müşteri Başarıyla Eklendi.");
                textBox4.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                richTextBox1.Text = "";
            }
            
        }
        

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            comboBox6.Items.Clear();
            comboBox7.Items.Clear();
            comboBox8.Items.Clear();
            ArrayList musteriList = new ArrayList();
            ArrayList hesaplarList = new ArrayList();
            SqlConnection conn = new SqlConnection("Data Source =.; Initial Catalog = BankaOtomasyon; Integrated Security = True");
            conn.Open();
            SqlCommand mGetirPre = new SqlCommand();
            mGetirPre.Connection = conn;
            mGetirPre.CommandText = "Select musteriNo,ad,soyad from MusterilerTBL";
            SqlDataReader mGetirPreRd = mGetirPre.ExecuteReader();
            while (mGetirPreRd.Read())
            {
                musteriList.Add(new Musteriler((int)mGetirPreRd["musteriNo"], (string)mGetirPreRd["ad"], (string)mGetirPreRd["soyad"]));
            }
            for (int i = 0; i < musteriList.Count; i++)
            {
                comboBox3.Items.Add(musteriList[i]);
                comboBox6.Items.Add(musteriList[i]);
            }
            mGetirPreRd.Close();
            SqlCommand hGetir = new SqlCommand();
            hGetir.Connection = conn;
            hGetir.CommandText = "select hesapNo,musteriNo,ekHesap,hesap from HesaplarTBL";
            SqlDataReader hGetirRd = hGetir.ExecuteReader();
            while (hGetirRd.Read())
            {
                hesaplarList.Add(new Hesaplar((int)hGetirRd["hesapNo"],(int)hGetirRd["musteriNo"],(decimal)hGetirRd["ekHesap"],(decimal)hGetirRd["hesap"]));
            }
            for (int j = 0; j < hesaplarList.Count; j++)
            {
                if (((Hesaplar)hesaplarList[j]).bakiye==0)
                {
                    comboBox7.Items.Add(hesaplarList[j]);
                } 
            }
            for (int k = 0; k < hesaplarList.Count; k++)
            {
                comboBox4.Items.Add(hesaplarList[k]);
                comboBox5.Items.Add(hesaplarList[k]);
                comboBox8.Items.Add(hesaplarList[k]);
            }
            hGetirRd.Close();
            
            int netPara = 0;
            SqlCommand bankadakiPara = new SqlCommand();
            bankadakiPara.Connection = conn;
            bankadakiPara.CommandText = "select SUM(bakiye) from HesaplarTBL";
            SqlDataReader bankadakiParaRd = bankadakiPara.ExecuteReader();
            while (bankadakiParaRd.Read())
            {
                netPara = Convert.ToInt32(bankadakiParaRd[""]);
            }
            label35.Text = netPara.ToString() + " TL";

        }
        public void Yenile()
        {
            
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            comboBox6.Items.Clear();
            comboBox7.Items.Clear();
            comboBox8.Items.Clear();
            ArrayList musteriList = new ArrayList();
            ArrayList hesaplarList = new ArrayList();
            SqlConnection conn = new SqlConnection("Data Source =.; Initial Catalog = BankaOtomasyon; Integrated Security = True");
            conn.Open();
            SqlCommand mGetirPre = new SqlCommand();
            mGetirPre.Connection = conn;
            mGetirPre.CommandText = "Select musteriNo,ad,soyad from MusterilerTBL";
            SqlDataReader mGetirPreRd = mGetirPre.ExecuteReader();
            while (mGetirPreRd.Read())
            {
                musteriList.Add(new Musteriler((int)mGetirPreRd["musteriNo"], (string)mGetirPreRd["ad"], (string)mGetirPreRd["soyad"]));
            }
            for (int i = 0; i < musteriList.Count; i++)
            {
                comboBox3.Items.Add(musteriList[i]);
                comboBox6.Items.Add(musteriList[i]);
            }
            mGetirPreRd.Close();
            SqlCommand hGetir = new SqlCommand();
            hGetir.Connection = conn;
            hGetir.CommandText = "select hesapNo,musteriNo,ekHesap,hesap from HesaplarTBL";
            SqlDataReader hGetirRd = hGetir.ExecuteReader();
            while (hGetirRd.Read())
            {
                hesaplarList.Add(new Hesaplar((int)hGetirRd["hesapNo"], (int)hGetirRd["musteriNo"], (decimal)hGetirRd["ekHesap"], (decimal)hGetirRd["hesap"]));
            }
            for (int j = 0; j < hesaplarList.Count; j++)
            {
                if (((Hesaplar)hesaplarList[j]).bakiye == 0)
                {
                    comboBox7.Items.Add(hesaplarList[j]);
                }
            }
            for (int k = 0; k < hesaplarList.Count; k++)
            {
                comboBox4.Items.Add(hesaplarList[k]);
                comboBox5.Items.Add(hesaplarList[k]);
                comboBox8.Items.Add(hesaplarList[k]);
            }
            hGetirRd.Close();
            
            int netPara = 0;
            SqlCommand bankadakiPara = new SqlCommand();
            bankadakiPara.Connection = conn;
            bankadakiPara.CommandText = "select SUM(bakiye) from HesaplarTBL";
            SqlDataReader bankadakiParaRd = bankadakiPara.ExecuteReader();
            while (bankadakiParaRd.Read())
            {
                netPara = Convert.ToInt32(bankadakiParaRd[""]);
            }
            label35.Text = netPara.ToString() + " TL";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source =.; Initial Catalog = BankaOtomasyon; Integrated Security = True");
                conn.Open();
                SqlCommand mGetir = new SqlCommand();
                mGetir.Connection = conn;
                mGetir.CommandText = "select ad,soyad,adres,telefon,email from MusterilerTBL where musteriNo=" + ((Musteriler)comboBox3.SelectedItem).id;
                SqlDataReader mGetirRd = mGetir.ExecuteReader();
                while (mGetirRd.Read())
                {
                    textBox13.Text = mGetirRd.GetString(0);
                    textBox12.Text = mGetirRd.GetString(1);
                    richTextBox2.Text = mGetirRd.GetString(2);
                    textBox11.Text = mGetirRd.GetString(3);
                    textBox10.Text = mGetirRd.GetString(4);
                }
                mGetirRd.Close();
            }
            catch(Exception)
            {
                MessageBox.Show("Müşteri Seçmediniz");
            }
            

        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source =.; Initial Catalog = BankaOtomasyon; Integrated Security = True");
                conn.Open();
                SqlCommand mDegistir = new SqlCommand();
                mDegistir.Connection = conn;
                mDegistir.CommandText = "update MusterilerTBL set ad='" + textBox13.Text + "', soyad='" + textBox12.Text + "', adres='" + richTextBox2.Text + "',telefon='" + textBox11.Text + "',email='" + textBox10.Text + "' where musteriNo=" + ((Musteriler)comboBox3.SelectedItem).id;
                mDegistir.ExecuteNonQuery();
                MessageBox.Show(textBox13.Text + " " + textBox12.Text + " Adlı Müşteri Düzenlendi");
                comboBox3.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";
                textBox13.Text = "";
                richTextBox2.Text = "";
            }
            catch(Exception)
            {
                MessageBox.Show("Müşteri Seçmediniz");
            }
            
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox3.Checked==true)
            {
                groupBox16.Enabled = true;
            }
            else if(checkBox3.Checked==false)
            {
                groupBox16.Enabled = false;
                numericUpDown1.Value = 0;
                numericUpDown2.Value = 0;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source =.; Initial Catalog = BankaOtomasyon; Integrated Security = True");
                conn.Open();
                SqlCommand hesapAc = new SqlCommand();
                hesapAc.Connection = conn;
                int balance = (int)numericUpDown1.Value + (int)numericUpDown2.Value;
                hesapAc.CommandText = "insert into HesaplarTBL (musteriNo, ekHesap, hesap, bakiye) values (" + ((Musteriler)comboBox6.SelectedItem).id + "," + numericUpDown2.Value + "," + numericUpDown1.Value + "," + balance + ")";
                hesapAc.ExecuteNonQuery();
                MessageBox.Show("Hesap Açma İşlemi Gerçekleştirildi");
            }
            catch(Exception)
            {
                MessageBox.Show("Hesabın Ait Olduğu Müşteriyi Seçiniz");
            }
            
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source =.; Initial Catalog = BankaOtomasyon; Integrated Security = True");
                conn.Open();
                SqlCommand hesapKapa = new SqlCommand();
                hesapKapa.Connection = conn;
                hesapKapa.CommandText = "delete from HesaplarTBL where hesapNo=" + ((Hesaplar)comboBox7.SelectedItem).id;
                hesapKapa.ExecuteNonQuery();
                MessageBox.Show("Hesap Başarıyla Kapatıldı");
            }
            catch(Exception)
            {
                MessageBox.Show("Kapatılacak Hesabı Seçmediniz");
            }
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source =.; Initial Catalog = BankaOtomasyon; Integrated Security = True");
                conn.Open();
                int miktar = 0;
                SqlCommand paraSinir = new SqlCommand();
                paraSinir.Connection = conn;
                paraSinir.CommandText = "select miktar from ParaSinirTBL where musteriNo=" + ((Hesaplar)comboBox4.SelectedItem).musteriNo + " and datediff(Day, tarih, getdate()) = 0";
                SqlDataReader paraSinirRd = paraSinir.ExecuteReader();
                while (paraSinirRd.Read())
                {
                    miktar = Convert.ToInt32(paraSinirRd["miktar"]);
                }
                paraSinirRd.Close();
                if (miktar == 0 && (int)numericUpDown3.Value <= 750)
                {
                    if (((Hesaplar)comboBox4.SelectedItem).hesap >= (decimal)numericUpDown3.Value) //anahesaptan çek
                    {
                        SqlCommand paraCek = new SqlCommand();
                        paraCek.Connection = conn;
                        Hesaplar secili = (Hesaplar)comboBox4.SelectedItem;
                        int seciliSon = (int)secili.bakiye - (int)numericUpDown3.Value;
                        int _miktar = (int)((Hesaplar)comboBox4.SelectedItem).hesap - (int)numericUpDown3.Value;
                        paraCek.CommandText = "UPDATE HesaplarTBL SET hesap=" + _miktar + " WHERE hesapNo=" + ((Hesaplar)comboBox4.SelectedItem).id;
                        paraCek.ExecuteNonQuery();
                        SqlCommand paraSinirUpdate = new SqlCommand();
                        paraSinirUpdate.Connection = conn;
                        paraSinirUpdate.CommandText = "INSERT INTO ParaSinirTBL (musteriNo, miktar, tarih) values (" + ((Hesaplar)comboBox4.SelectedItem).musteriNo + "," + (int)numericUpDown3.Value + ",GETDATE())";
                        paraSinirUpdate.ExecuteNonQuery();
                        MessageBox.Show("Hesap Update Edildi, ParaSinir'a Satır Eklendi");
                        Yenile();
                        SqlCommand bakiyeUpdate = new SqlCommand();
                        bakiyeUpdate.Connection = conn;
                        bakiyeUpdate.CommandText = "UPDATE HesaplarTBL SET bakiye=" + seciliSon + " where hesapNo=" + secili.id;
                        bakiyeUpdate.ExecuteNonQuery();
                        SqlCommand islemKaydet = new SqlCommand();
                        islemKaydet.Connection = conn;
                        islemKaydet.CommandText = "INSERT INTO IslemlerTBL (hesapNo, islemTur, miktar, tarih) values (" + secili.id + ",2," + (int)numericUpDown3.Value + ",GETDATE())";
                        islemKaydet.ExecuteNonQuery();
                        comboBox4.Text = "";
                    }
                    else //ekHesaptan çek
                    {

                        SqlCommand paraCek = new SqlCommand();
                        paraCek.Connection = conn;
                        Hesaplar secili = (Hesaplar)comboBox4.SelectedItem;
                        int seciliSon = (int)secili.bakiye - (int)numericUpDown3.Value;
                        int _miktar = (int)((Hesaplar)comboBox4.SelectedItem).ekHesap - (int)numericUpDown3.Value;
                        paraCek.CommandText = "UPDATE HesaplarTBL SET ekHesap=" + _miktar + " WHERE hesapNo=" + ((Hesaplar)comboBox4.SelectedItem).id;
                        paraCek.ExecuteNonQuery();
                        SqlCommand paraSinirUpdate = new SqlCommand();
                        paraSinirUpdate.Connection = conn;
                        paraSinirUpdate.CommandText = "INSERT INTO ParaSinirTBL (musteriNo, miktar, tarih) values (" + ((Hesaplar)comboBox4.SelectedItem).musteriNo + "," + (int)numericUpDown3.Value + ",GETDATE())";
                        paraSinirUpdate.ExecuteNonQuery();
                        MessageBox.Show("ekHesap Update Edildi, ParaSinir'a Satır Eklendi");
                        Yenile();
                        SqlCommand bakiyeUpdate = new SqlCommand();
                        bakiyeUpdate.Connection = conn;
                        bakiyeUpdate.CommandText = "UPDATE HesaplarTBL SET bakiye=" + seciliSon + " where hesapNo=" + secili.id;
                        bakiyeUpdate.ExecuteNonQuery();
                        SqlCommand islemKaydet = new SqlCommand();
                        islemKaydet.Connection = conn;
                        islemKaydet.CommandText = "INSERT INTO IslemlerTBL (hesapNo, islemTur, miktar, tarih) values (" + secili.id + ",2," + (int)numericUpDown3.Value + ",GETDATE())";
                        islemKaydet.ExecuteNonQuery();
                        comboBox4.Text = "";
                    }
                }
                else if (miktar != 0 && miktar + (int)numericUpDown3.Value <= 750)
                {
                    if (((Hesaplar)comboBox4.SelectedItem).hesap >= (decimal)numericUpDown3.Value) //anahesaptan çek
                    {
                        SqlCommand paraCek = new SqlCommand();
                        paraCek.Connection = conn;
                        Hesaplar secili = (Hesaplar)comboBox4.SelectedItem;
                        int seciliSon = (int)secili.bakiye - (int)numericUpDown3.Value;
                        int _miktar = (int)((Hesaplar)comboBox4.SelectedItem).hesap - (int)numericUpDown3.Value;
                        paraCek.CommandText = "UPDATE HesaplarTBL SET hesap=" + _miktar + " WHERE hesapNo=" + ((Hesaplar)comboBox4.SelectedItem).id;
                        paraCek.ExecuteNonQuery();
                        SqlCommand paraSinirUpdate = new SqlCommand();
                        paraSinirUpdate.Connection = conn;
                        int _sinirMiktar = miktar + (int)numericUpDown3.Value;
                        paraSinirUpdate.CommandText = "UPDATE ParaSinirTBL SET miktar=" + _sinirMiktar + " WHERE musteriNo=" + ((Hesaplar)comboBox4.SelectedItem).musteriNo + " AND datediff(Day, tarih, getdate()) = 0";
                        paraSinirUpdate.ExecuteNonQuery();
                        MessageBox.Show("Hesap ve ParaSinir Update Edildi");
                        Yenile();
                        SqlCommand bakiyeUpdate = new SqlCommand();
                        bakiyeUpdate.Connection = conn;
                        bakiyeUpdate.CommandText = "UPDATE HesaplarTBL SET bakiye=" + seciliSon + " where hesapNo=" + secili.id;
                        bakiyeUpdate.ExecuteNonQuery();
                        SqlCommand islemKaydet = new SqlCommand();
                        islemKaydet.Connection = conn;
                        islemKaydet.CommandText = "INSERT INTO IslemlerTBL (hesapNo, islemTur, miktar, tarih) values (" + secili.id + ",2," + (int)numericUpDown3.Value + ",GETDATE())";
                        islemKaydet.ExecuteNonQuery();
                        comboBox4.Text = "";
                    }
                    else //ekHesaptan çek
                    {
                        SqlCommand paraCek = new SqlCommand();
                        paraCek.Connection = conn;
                        Hesaplar secili = (Hesaplar)comboBox4.SelectedItem;
                        int seciliSon = (int)secili.bakiye - (int)numericUpDown3.Value;
                        int _miktar = (int)((Hesaplar)comboBox4.SelectedItem).ekHesap - (int)numericUpDown3.Value;
                        paraCek.CommandText = "UPDATE HesaplarTBL SET ekHesap=" + _miktar + " WHERE hesapNo=" + ((Hesaplar)comboBox4.SelectedItem).id;
                        paraCek.ExecuteNonQuery();
                        SqlCommand paraSinirUpdate = new SqlCommand();
                        paraSinirUpdate.Connection = conn;
                        int _sinirMiktar = miktar + (int)numericUpDown3.Value;
                        paraSinirUpdate.CommandText = "UPDATE ParaSinirTBL SET miktar=" + _sinirMiktar + " WHERE musteriNo=" + ((Hesaplar)comboBox4.SelectedItem).musteriNo + " AND datediff(Day, tarih, getdate()) = 0";
                        paraSinirUpdate.ExecuteNonQuery();
                        MessageBox.Show("ekHesap ve ParaSinir Update Edildi");
                        Yenile();
                        SqlCommand bakiyeUpdate = new SqlCommand();
                        bakiyeUpdate.Connection = conn;
                        bakiyeUpdate.CommandText = "UPDATE HesaplarTBL SET bakiye=" + seciliSon + " where hesapNo=" + secili.id;
                        bakiyeUpdate.ExecuteNonQuery();
                        SqlCommand islemKaydet = new SqlCommand();
                        islemKaydet.Connection = conn;
                        islemKaydet.CommandText = "INSERT INTO IslemlerTBL (hesapNo, islemTur, miktar, tarih) values (" + secili.id + ",2," + (int)numericUpDown3.Value + ",GETDATE())";
                        islemKaydet.ExecuteNonQuery();
                        comboBox4.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Günlük para çekim sınırınız 750TL'dir");
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Hesap Seçiminiz Geçersiz");
            }
            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source =.; Initial Catalog = BankaOtomasyon; Integrated Security = True");
                conn.Open();
                Hesaplar secili = (Hesaplar)comboBox4.SelectedItem;
                SqlCommand paraYatir = new SqlCommand();
                paraYatir.Connection = conn;
                if (checkBox4.Checked == false)
                {
                    int hesapSon = (int)((Hesaplar)comboBox4.SelectedItem).hesap + (int)numericUpDown4.Value;
                    paraYatir.CommandText = "UPDATE HesaplarTBL SET hesap=" + hesapSon + " WHERE hesapNo=" + ((Hesaplar)comboBox4.SelectedItem).id;
                    paraYatir.ExecuteNonQuery();
                    Yenile();
                }
                else if (checkBox4.Checked == true)
                {
                    int ekHesapSon = (int)((Hesaplar)comboBox4.SelectedItem).ekHesap + (int)numericUpDown4.Value;
                    paraYatir.CommandText = "UPDATE HesaplarTBL SET ekHesap=" + ekHesapSon + " WHERE hesapNo=" + ((Hesaplar)comboBox4.SelectedItem).id;
                    paraYatir.ExecuteNonQuery();
                    Yenile();
                }


                SqlCommand bakiyeUpdate = new SqlCommand();
                bakiyeUpdate.Connection = conn;
                int bakiyeSon = (int)secili.bakiye + (int)numericUpDown4.Value;
                bakiyeUpdate.CommandText = "UPDATE HesaplarTBL SET bakiye=" + bakiyeSon + " where hesapNo=" + secili.id;
                bakiyeUpdate.ExecuteNonQuery();
                SqlCommand islemKaydet = new SqlCommand();
                islemKaydet.Connection = conn;
                islemKaydet.CommandText = "INSERT INTO IslemlerTBL (hesapNo, islemTur, miktar, tarih) values (" + secili.id + ",1," + (int)numericUpDown4.Value + ",GETDATE())";
                islemKaydet.ExecuteNonQuery();
                comboBox4.Text = "";
                MessageBox.Show("Para Yatırma İşlemi Tamamlandı.");
            }
            catch(Exception)
            {
                MessageBox.Show("Hesap Seçiminiz Geçersiz");
            }
            

        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source =.; Initial Catalog = BankaOtomasyon; Integrated Security = True");
                conn.Open();
                Hesaplar gonderen = (Hesaplar)comboBox4.SelectedItem;
                Hesaplar alici = (Hesaplar)comboBox5.SelectedItem;
                int gonderenHesap = (int)gonderen.hesap - (int)numericUpDown5.Value;
                int gonderenBakiye = (int)gonderen.bakiye - (int)numericUpDown5.Value;
                int alanHesap = (int)alici.hesap + (int)numericUpDown5.Value;
                int alanBakiye = (int)alici.bakiye + (int)numericUpDown5.Value;
                SqlCommand havaleGonderen = new SqlCommand();
                havaleGonderen.Connection = conn;
                havaleGonderen.CommandText = "UPDATE HesaplarTBL SET hesap=" + gonderenHesap + ", bakiye=" + gonderenBakiye + " WHERE hesapNo=" + gonderen.id;
                havaleGonderen.ExecuteNonQuery();
                SqlCommand havaleAlici = new SqlCommand();
                havaleAlici.Connection = conn;
                havaleAlici.CommandText = "UPDATE HesaplarTBL SET hesap=" + alanHesap + ", bakiye=" + alanBakiye + " WHERE hesapNo=" + alici.id;
                havaleAlici.ExecuteNonQuery();
                SqlCommand islemKaydet = new SqlCommand();
                islemKaydet.Connection = conn;
                islemKaydet.CommandText = "INSERT INTO IslemlerTBL (hesapNo,islemTur,miktar,tarih) values (" + gonderen.id + ",3," + (int)numericUpDown5.Value + ",GETDATE())";
                islemKaydet.ExecuteNonQuery();
                islemKaydet.CommandText = "INSERT INTO IslemlerTBL (hesapNo,islemTur,miktar,tarih) values (" + alici.id + ",4," + (int)numericUpDown5.Value + ",GETDATE())";
                islemKaydet.ExecuteNonQuery();
                SqlCommand islemNoAl = new SqlCommand();
                islemNoAl.Connection = conn;
                islemNoAl.CommandText = "SELECT IDENT_CURRENT('IslemlerTBL')";
                SqlDataReader islemNoAlRd = islemNoAl.ExecuteReader();
                int islemNo = 0;
                while (islemNoAlRd.Read())
                {
                    islemNo = Convert.ToInt32(islemNoAlRd[""]);
                }
                islemNoAlRd.Close();
                if (islemNo != 0)
                {
                    SqlCommand havaleKaydet = new SqlCommand();
                    havaleKaydet.Connection = conn;
                    int _islemNo = islemNo - 1;
                    havaleKaydet.CommandText = "INSERT INTO HavaleTBL (islemNo, gonderenHesap, alanHesap, tarih) values (" + _islemNo + "," + gonderen.id + "," + alici.id + ",GETDATE())";
                    havaleKaydet.ExecuteNonQuery();
                    havaleKaydet.CommandText = "INSERT INTO HavaleTBL (islemNo, gonderenHesap, alanHesap, tarih) values (" + islemNo + "," + gonderen.id + "," + alici.id + ",GETDATE())";
                    havaleKaydet.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Bağlantı Hatası");
                }
                Yenile();
                comboBox5.Text = "";
                comboBox4.Text = "";
                MessageBox.Show("Havele İşlemi Gerçekleştirildi.");

            }
            catch (Exception)
            {
                MessageBox.Show("Hesap Seçiminiz/Seçimleriniz Geçersiz");
            }
            
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source =.; Initial Catalog = BankaOtomasyon; Integrated Security = True");
                conn.Open();
                SqlCommand getir = new SqlCommand();
                getir.Connection = conn;
                getir.CommandText = "select IslemTurTBL.aciklama, IslemlerTBL.miktar, IslemlerTBL.tarih, CASE WHEN IslemlerTBL.islemTur=3 THEN HavaleTBL.alanHesap WHEN IslemlerTBL.islemTur=4 THEN HavaleTBL.gonderenHesap END from IslemlerTBL FULL OUTER JOIN IslemTurTBL ON IslemlerTBL.islemTur=IslemTurTBL.id FULL OUTER JOIN HavaleTBL ON IslemlerTBL.islemNo=HavaleTBL.islemNo where hesapNo=" + ((Hesaplar)comboBox8.SelectedItem).id + " AND IslemlerTBL.tarih BETWEEN '" + dateTimePicker1.Text + "' AND '" + dateTimePicker2.Text + "'";
                SqlDataAdapter da = new SqlDataAdapter(getir);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch(Exception)
            {
                MessageBox.Show("İşlem Yapılacak Hesabı Seçmediniz");
            }
            
        }

        private void button16_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source =.; Initial Catalog = BankaOtomasyon; Integrated Security = True");
            conn.Open();
            int cikanPara=0;
            int girenPara=0;
            int netPara=0;
            SqlCommand bankadanCikan = new SqlCommand();
            bankadanCikan.Connection = conn;
            bankadanCikan.CommandText = "select SUM(miktar) from IslemlerTBL where islemTur=2 and tarih='"+dateTimePicker4.Text+"'";
            SqlDataReader bankadanCikanRd = bankadanCikan.ExecuteReader();
            while (bankadanCikanRd.Read())
            {
                
                try
                {
                    cikanPara = Convert.ToInt32(bankadanCikanRd[""]);
                }
                catch(Exception)
                {
                    cikanPara = 0;
                }
                
            }
            bankadanCikanRd.Close();
            SqlCommand bankayaGiren = new SqlCommand();
            bankayaGiren.Connection = conn;
            bankayaGiren.CommandText = "select SUM(miktar) from IslemlerTBL where islemTur=1 and tarih='" + dateTimePicker4.Text + "'";
            SqlDataReader bankayaGirenRd = bankayaGiren.ExecuteReader();
            while (bankayaGirenRd.Read())
            {
                try
                {
                    girenPara = Convert.ToInt32(bankayaGirenRd[""]);
                }
                catch(Exception)
                {
                    girenPara = 0;
                }
                
            }
            bankayaGirenRd.Close();
            SqlCommand bankadakiPara = new SqlCommand();
            bankadakiPara.Connection = conn;
            bankadakiPara.CommandText = "select SUM(bakiye) from HesaplarTBL";
            SqlDataReader bankadakiParaRd = bankadakiPara.ExecuteReader();
            while (bankadakiParaRd.Read())
            {
                netPara = Convert.ToInt32(bankadakiParaRd[""]);
            }
            label33.Text = cikanPara.ToString()+" TL";
            label34.Text = girenPara.ToString() + " TL";
            label35.Text = netPara.ToString() + " TL";
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source =.; Initial Catalog = BankaOtomasyon; Integrated Security = True");
            conn.Open();
            ArrayList kullaniciList = new ArrayList();
            SqlCommand kGetir = new SqlCommand();
            kGetir.Connection = conn;
            kGetir.CommandText = "select id,username from KullaniciTBL";
            SqlDataReader kGetirRd = kGetir.ExecuteReader();
            while (kGetirRd.Read())
            {
                kullaniciList.Add(new Kullanicilar((int)kGetirRd["id"], (string)kGetirRd["username"]));
            }
            for (int m = 0; m < kullaniciList.Count; m++)
            {
                comboBox1.Items.Add(kullaniciList[m]);
                comboBox2.Items.Add(kullaniciList[m]);
            }
            kGetirRd.Close();
        }
        public void kullaniciYenile()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            SqlConnection conn = new SqlConnection("Data Source =.; Initial Catalog = BankaOtomasyon; Integrated Security = True");
            conn.Open();
            ArrayList kullaniciList = new ArrayList();
            SqlCommand kGetir = new SqlCommand();
            kGetir.Connection = conn;
            kGetir.CommandText = "select id,username from KullaniciTBL";
            SqlDataReader kGetirRd = kGetir.ExecuteReader();
            while (kGetirRd.Read())
            {
                kullaniciList.Add(new Kullanicilar((int)kGetirRd["id"], (string)kGetirRd["username"]));
            }
            for (int m = 0; m < kullaniciList.Count; m++)
            {
                comboBox1.Items.Add(kullaniciList[m]);
                comboBox2.Items.Add(kullaniciList[m]);
            }
            comboBox1.Text = "";
            comboBox2.Text = "";
            kGetirRd.Close();
        }

        
    }
}
