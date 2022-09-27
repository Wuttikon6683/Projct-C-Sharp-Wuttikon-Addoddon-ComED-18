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
    public partial class from_login : Form
    {
        int X = 0;
        MySqlConnection connectionString = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=newnicotineshop;");
        MySqlDataAdapter adapter;
        DataTable table;
        public from_login()
        {
            InitializeComponent();
        }
        private void openConnection()
        {
            if (connectionString.State == ConnectionState.Closed)
            {
                connectionString.Open();
            }
        }
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=newnicotineshop;";

            MySqlConnection conn = new MySqlConnection(connectionString);

            return conn;
        }
        public void dataAdapterLogin(String query)
        {
            openConnection();
            adapter = new MySqlDataAdapter(query, connectionString);
            table = new DataTable();
            adapter.Fill(table);

            if (text_Username.Text == "")
            {
                MessageBox.Show("กรุณากรอก Username");
                text_Username.Focus();
            }
            else if (text_Password.Text == "")
            {
                MessageBox.Show("กรุณากรอก Password");
                text_Password.Focus();
            }
            else if (table.Rows.Count > 0)
            {
                string status = table.Rows[0][8].ToString();
                if (status == "admin")
                {
                    MessageBox.Show("Admin");
                    this.Hide();
                    from_admin stck = new from_admin();
                    stck.Show();
                }
                else if (status == "")
                {
                    MessageBox.Show("User");
                    this.Hide();
                    from_shop stck = new from_shop();
                    stck.Show();

                    MySqlConnection conn = databaseConnection();
                    conn.Open();

                    MySqlCommand cmd;

                    cmd = conn.CreateCommand();
                    cmd.CommandText = $"SELECT*FROM account WHERE  username =\"{text_Username.Text}\" AND password=\"{text_Password.Text}\"";

                    MySqlDataReader Row = cmd.ExecuteReader();

                    if (Row.HasRows)
                    {
                        from_shop f3 = new from_shop();
                        MySqlConnection conn3 = databaseConnection();
                        conn3.Open(); // สร้างพารามิเตอร์ ID เก็บค่า usernameText 
                        MySqlCommand cmd2 = new MySqlCommand("SELECT firstname from account where  username = @ID", conn3);
                        cmd2.Parameters.AddWithValue("@ID", (text_Username.Text));
                        MySqlDataReader da = cmd2.ExecuteReader();
                        while (da.Read())

                        MessageBox.Show("เข้าสู่ระบบสำเร็จ");
                        from_shop f1 = new from_shop();
                        this.Hide();
                        f1.Show();
                        {
                            f1.namez = da.GetValue(0).ToString();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("กรุณากรอกข้อมูลให้ถูกต้อง");
                }
            }

        }
        private void btn_regis_Register_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = databaseConnection();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT*FROM account WHERE username =\"{text_regis_Username.Text}\"";
            MySqlDataReader row = cmd.ExecuteReader();
            if (row.HasRows)
            {
                MessageBox.Show("มีชื่อผู้ใช้นี้อยู่ในระบบอยู่แล้ว");
                X = 2;
            }
            if (X == 0)
            {
                if (text_regis_Username.Text == "" || text_regis_Password.Text == "" || text_regis_Firstname.Text == "" || text_regis_Lastname.Text == "" || text_regis_Address.Text == "" || text_regis_Phone.Text == "")
                {
                    MessageBox.Show("กรุณากรอกข้อมูลให้ครบ");
                }
                else
                {
                    try
                    {
                        MySqlConnection connn = databaseConnection();
                        String sql = "INSERT INTO newnicotineshop.account (username	,password,firstname,lastname,address,phone) VALUES('" + text_regis_Username.Text + "','" + text_regis_Password.Text + "','" + text_regis_Firstname.Text + "','" + text_regis_Lastname.Text + "','" + text_regis_Address.Text + "','" + text_regis_Phone.Text + "')";
                        MySqlCommand cmdd = new MySqlCommand(sql, connn);
                        connn.Open();

                        int rows = cmdd.ExecuteNonQuery();

                        conn.Close();
                        MessageBox.Show("เพิ่มข้อมูลเสร็จสิ้น");

                        text_regis_Username.Clear();
                        text_regis_Password.Clear();
                        text_regis_Firstname.Clear();
                        text_regis_Lastname.Clear();
                        text_regis_Address.Clear();
                        text_regis_Phone.Clear();
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
            }
            X = 0;
        }
 
        private void btn_Login_Click(object sender, EventArgs e)
        {
            string login = "SELECT * FROM newnicotineshop.account WHERE username = '" + text_Username.Text + "' AND password = '" + text_Password.Text + "'";
            dataAdapterLogin(login);
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Register_Click(object sender, EventArgs e)
        {
            panel_register.Show();
        }

        private void from_login_Load(object sender, EventArgs e)
        {
            panel_register.Hide();
        }

        private void btn_regis_Back_Click(object sender, EventArgs e)
        {
            panel_register.Hide();
        }

        private void btn_Exit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void panel_register_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
        private void text_regis_Firstname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 65 || e.KeyChar > 90) && (e.KeyChar < 97 || e.KeyChar > 122) && (e.KeyChar != 8) && (e.KeyChar != 32))
            {
                e.Handled = true;
            }
        }
        private void text_regis_Lastname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 65 || e.KeyChar > 90) && (e.KeyChar < 97 || e.KeyChar > 122) && (e.KeyChar != 8) && (e.KeyChar != 32))
            {
                e.Handled = true;
            }
        }
        private void text_regis_Phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
            }
        }
    }
}
