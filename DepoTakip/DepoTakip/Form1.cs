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


namespace DepoTakip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection bagla = new SqlConnection("Data Source=DESKTOP-49IJTPL;Initial Catalog=stoktakip;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }
        void temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }
        void listele()
        {
            bagla.Open();
            SqlDataAdapter cmd = new SqlDataAdapter("SELECT * FROM stok", bagla);
            DataTable dt = new DataTable();
            cmd.Fill(dt);
            dataGridView1.DataSource = dt;
            bagla.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Lütfen Boş Bırakmayınız");
            }
            else
            {
                bagla.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO stok(urunadi,urunkategori,stokadedi,stokno,barkodno,giristarih)values(@urunadi,@urunkategori,@stokadedi,@stokno,@barkodno,@giristarih)", bagla);
                cmd.Parameters.AddWithValue("@urunadi", textBox2.Text);
                cmd.Parameters.AddWithValue("@urunkategori", textBox3.Text);
                cmd.Parameters.AddWithValue("@stokadedi", textBox4.Text);
                cmd.Parameters.AddWithValue("@stokno", textBox5.Text);
                cmd.Parameters.AddWithValue("@barkodno", textBox6.Text);
                cmd.Parameters.AddWithValue("@giristarih", dateTimePicker1.Value);
                cmd.ExecuteNonQuery();
                bagla.Close();
                MessageBox.Show("Veri Kaydedildi");
                listele();
                temizle();
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            dateTimePicker1.Value = DateTime.Parse(dataGridView1.CurrentRow.Cells[6].Value.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Lütfen Boş Bırakmayınız");

            }
            else
            {
                bagla.Open();
                SqlCommand cmd = new SqlCommand("UPDATE stok SET urunadi=@urunadi, urunkategori=@urunkategori, stokadedi=@stokadedi, stokno=@stokno, barkodno=@barkodno, giristarih=@giristarihi WHERE id=@id", bagla);
                cmd.Parameters.AddWithValue("@urunadi", textBox2.Text);
                cmd.Parameters.AddWithValue("@urunkategori", textBox3.Text);
                cmd.Parameters.AddWithValue("@stokadedi", textBox4.Text);
                cmd.Parameters.AddWithValue("@stokno", textBox5.Text);
                cmd.Parameters.AddWithValue("@barkodno", textBox6.Text);
                cmd.Parameters.AddWithValue("@giristarihi", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@id", textBox1.Text);
                cmd.ExecuteNonQuery();
                bagla.Close();
                MessageBox.Show("Kayıt Güncellendi");
                listele();
                temizle();
            }



        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Lütfen Boş Bırakmayınız");

            }
            else
            {
                bagla.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM stok WHERE id=@id", bagla);
                cmd.Parameters.AddWithValue("@id", textBox1.Text);
                cmd.ExecuteNonQuery();
                bagla.Close();
                MessageBox.Show("Silme İşlemi Başarılı");
                listele();
                temizle();
            }

        }


        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            string filter = textBox7.Text.Trim();

            bagla.Open();
            string query = "SELECT * FROM stok WHERE urunadi LIKE @ad";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, bagla);
            dataAdapter.SelectCommand.Parameters.AddWithValue("@ad", "%" + filter + "%");
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            bagla.Close();

        }
    }
}
