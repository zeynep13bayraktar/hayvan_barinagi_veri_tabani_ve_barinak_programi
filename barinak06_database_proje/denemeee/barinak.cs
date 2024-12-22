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
using Npgsql;

namespace denemeee
{
    public partial class barinak : Form
    {
        public barinak()
        {
            InitializeComponent();
        }

        private void barinak_Load(object sender, EventArgs e)
        {

        }

        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=barinak06; user ID=postgres; password=Zb96104019");

        private void btn_list_Click(object sender, EventArgs e)
        {
            string sorgu = "Select * from kisi";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("insert into kisi (kisiID, ad, soyad, telefon, email, adres) values (@p1, @p2, @p3, @p4, @p5, @p6)", baglanti);
            komut1.Parameters.AddWithValue("@p1", int.Parse(txt_ID.Text));
            komut1.Parameters.AddWithValue("@p2", txt_ad.Text);
            komut1.Parameters.AddWithValue("@p3", txt_soyad.Text);
            komut1.Parameters.AddWithValue("@p4", txt_tel.Text);
            komut1.Parameters.AddWithValue("@p5", txt_mail.Text);
            komut1.Parameters.AddWithValue("@p6", txt_adres.Text);
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("kişi ekleme işlemi başarılı bir şekilde gerçekleşti...");
        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("Delete From kisi where kisiID=@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", int.Parse(txt_ID.Text));
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi başarılı bir şekilde silindi...");

        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("update kisi set ad=@p1, soyad=@p2, telefon=@p3, email=@p4, adres=@p5 where kisiID=@p6",baglanti);
            komut3.Parameters.AddWithValue("@p1", txt_ad.Text);
            komut3.Parameters.AddWithValue("@p2", txt_soyad.Text);
            komut3.Parameters.AddWithValue("@p3", txt_tel.Text);
            komut3.Parameters.AddWithValue("@p4", txt_mail.Text);
            komut3.Parameters.AddWithValue("@p5", txt_adres.Text);
            komut3.Parameters.AddWithValue("@p6", int.Parse(txt_ID.Text));
            komut3.ExecuteNonQuery();
            MessageBox.Show("kişi bilgileri başarıyla güncellendi...");
            baglanti.Close();
        }

        private void txt_ara_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "Select * from kisi where (kisiID=@p1 or @p1 IS NULL) and (ad=@p2 or @p2 IS NULL) and (soyad=@p3 or @p3 IS NULL) and (telefon=@p4 or @p4 IS NULL) and (email=@p5 or @p5 IS NULL) and (adres=@p6 or @p6 IS NULL)";
            NpgsqlCommand komut4 = new NpgsqlCommand(sorgu, baglanti);
            komut4.Parameters.AddWithValue("@p1", string.IsNullOrWhiteSpace(txt_ID.Text) ? (object)DBNull.Value : int.Parse(txt_ID.Text));
            komut4.Parameters.AddWithValue("@p2", string.IsNullOrWhiteSpace(txt_ad.Text) ? (object)DBNull.Value : txt_ad.Text);
            komut4.Parameters.AddWithValue("@p3", string.IsNullOrWhiteSpace(txt_soyad.Text) ? (object)DBNull.Value : txt_soyad.Text);
            komut4.Parameters.AddWithValue("@p4", string.IsNullOrWhiteSpace(txt_tel.Text) ? (object)DBNull.Value : txt_tel.Text);
            komut4.Parameters.AddWithValue("@p5", string.IsNullOrWhiteSpace(txt_mail.Text) ? (object)DBNull.Value : txt_mail.Text);
            komut4.Parameters.AddWithValue("@p6", string.IsNullOrWhiteSpace(txt_adres.Text) ? (object)DBNull.Value : txt_adres.Text);

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut4);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();

        }
    }
}
