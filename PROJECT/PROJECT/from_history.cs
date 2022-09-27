using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PROJECT
{
    public partial class from_history : Form
    {
        public from_history()
        {
            InitializeComponent();
        }
        private void from_history_Load(object sender, EventArgs e)
        {
            showdataGridView2();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=newnicotineshop;";

            MySqlConnection conn = new MySqlConnection(connectionString);

            return conn;
        }
        private void showdataGridView2()
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM history";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataGridView_history.DataSource = ds.Tables[0].DefaultView;
        }
        private void btn_history_search_Click(object sender, EventArgs e)
        {
            if (text_search.Text != "")
            {
                MySqlConnection conn = databaseConnection();
                DataSet ds = new DataSet();
                conn.Open();
                MySqlCommand cmd;
                cmd = conn.CreateCommand();
                cmd.CommandText = ($"SELECT*FROM history WHERE name_customer like\"%{text_search.Text}\"");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds);
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    MySqlConnection conn2 = databaseConnection();
                    conn2.Open();
                    MySqlCommand cmd2;
                    cmd2 = conn2.CreateCommand(); // เอาราคาจาก total ใน From history มาบวกกัน ให้เป็นราคาทั้งหมดของผุ้คนนั้นๆ
                    cmd2.CommandText = ($"SELECT SUM(total) FROM history WHERE name_customer like\"%{text_search.Text}\"");
                    MySqlDataReader dr2 = cmd2.ExecuteReader();
                    while (dr2.Read())
                    {
                        text_calculate.Text = Convert.ToString(dr2[0]); // จะขึ้นโชว์ ราคารวมทั้งหมดที่ text_calculate
                    }
                    conn2.Close();
                }
                conn.Close();
                dataGridView_history.DataSource = ds.Tables[0].DefaultView; // โชว์ข้อมูลลูกค้าใน dataGridView2 ด้วย
            }
            else
            {
                showdataGridView2(); //หากไม่เสิร์ชชื่อ ก็จะโชว์ข้อมูลลูกค้าทุกคนทั้งหมด
            }
        }
        private void btn_history_calculate_Click(object sender, EventArgs e)
        {
            text_search.Clear();

            text_calculate.Text = "0";
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = ($"SELECT*FROM history WHERE date BETWEEN \"{dateTimePicker1.Text}\" AND \"{dateTimePicker2.Text}\"");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                MySqlConnection conn2 = databaseConnection();
                conn2.Open();
                MySqlCommand cmd2;
                cmd2 = conn2.CreateCommand(); // เอาราคาจาก total ใน From history มาบวกกัน ในระหว่างวันนั้นๆที่เราเลือกในปฏิทิน
                cmd2.CommandText = ($"SELECT SUM(total) FROM history WHERE date BETWEEN \"{dateTimePicker1.Text}\" AND \"{dateTimePicker2.Text}\"");
                MySqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    text_calculate.Text = Convert.ToString(dr2[0]); // จะขึ้นโชว์ ราคารวมยอดขายทั้งหมดที่ textBox6
                }
                conn2.Close();
            }
            conn.Close();
            dataGridView_history.DataSource = ds.Tables[0].DefaultView;
        }
        private void btn_admin_stock_Click(object sender, EventArgs e)
        {
            this.Hide();
            from_admin stck = new from_admin();
            stck.Show();
        }
        private void btn_admin_Users_Click(object sender, EventArgs e)
        {
            this.Hide();
            from_users stck = new from_users();
            stck.Show();
        }
        private void btn_logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            from_login stck = new from_login();
            stck.Show();
        }
    }
}
