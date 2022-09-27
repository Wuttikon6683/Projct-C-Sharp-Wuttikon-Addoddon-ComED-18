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
    public partial class from_users : Form
    {
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=newnicotineshop;";

            MySqlConnection conn = new MySqlConnection(connectionString);

            return conn;
        }
        public from_users()
        {
            InitializeComponent();
        }

        private void from_users_Load(object sender, EventArgs e)
        {
            showUsers();
        }
        private void showUsers()
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();

            MySqlCommand cmd;

            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM account";

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);

            conn.Close();
            dataGridViewshowuser.DataSource = ds.Tables[0].DefaultView;
        }
        private void btn_users_adduser_Click(object sender, EventArgs e)
        {
            if (text_user_Username.Text == "" || text_user_Password.Text == "" || text_user_Firstname.Text == "" || text_user_Lastname.Text == "" || text_user_Address.Text == "" || text_user_Phone.Text == "" || text_user_idcard.Text == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วน");
            }
            else
            {
                try
                {
                    MySqlConnection conn = databaseConnection();
                    String sql = "INSERT INTO newnicotineshop.account (username,password,firstname,lastname,address,phone,ID_card,status) VALUES('" + text_user_Username.Text + "','" + text_user_Password.Text + "','" + text_user_Firstname.Text + "','" + text_user_Lastname.Text + "','" + text_user_Address.Text + "','" + text_user_Phone.Text + "','" + text_user_idcard.Text + "','" + text_user_Status.Text + "')";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();

                    int rows = cmd.ExecuteNonQuery();

                    conn.Close();
                    MessageBox.Show("เพิ่มเรียบร้อยแล้ว");
                    showUsers();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void btn_users_delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("คุณแน่ใจหรือไม่ว่าต้องการลบบัญชี", "ลบบัญชี", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int selectedRow = dataGridViewshowuser.CurrentCell.RowIndex;
                int deleteID = Convert.ToInt32(dataGridViewshowuser.Rows[selectedRow].Cells["ID"].Value);

                MySqlConnection conn = databaseConnection();
                String sql = "DELETE FROM account WHERE ID = '" + deleteID + "' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();

                if (rows > 0)
                {
                    MessageBox.Show("ลบเรียบร้อยแล้ว");
                    showUsers();
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }
        }

        private void btn_users_edit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("คุณแน่ใจหรือไม่ว่าต้องการแก้ไขข้อมูล", "แก้ไขข้อมูล", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int selectedRow = dataGridViewshowuser.CurrentCell.RowIndex;
                int editId = Convert.ToInt32(dataGridViewshowuser.Rows[selectedRow].Cells["id"].Value);
                MySqlConnection conn = databaseConnection();
                String sql = "UPDATE account SET username = '" + text_user_Username.Text + "',password = '" + text_user_Password.Text + "',firstname = '" + text_user_Firstname.Text + "',lastname = '" + text_user_Lastname.Text + "',address = '" + text_user_Address.Text + "',phone = '" + text_user_Phone.Text + "',ID_card = '" + text_user_idcard.Text + "',status = '" + text_user_Status.Text + "' WHERE id = '" + editId + "'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();
                if (rows > 0)
                {
                    MessageBox.Show("แก้ไขเรียบร้อยแล้ว");
                    showUsers();
                }
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void btn_users_clear_Click(object sender, EventArgs e)
        {
            text_user_Username.Text = "";
            text_user_Password.Text = "";
            text_user_Firstname.Text = "";
            text_user_Lastname.Text = "";
            text_user_Phone.Text = "";
            text_user_Address.Text = "";
            text_user_idcard.Text = "";
            text_user_Status.Text = "";
        }
        private void dataGridViewshowuser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewshowuser.CurrentRow.Selected = true;
            text_user_Username.Text = dataGridViewshowuser.Rows[e.RowIndex].Cells["username"].FormattedValue.ToString();
            text_user_Password.Text = dataGridViewshowuser.Rows[e.RowIndex].Cells["password"].FormattedValue.ToString();
            text_user_Firstname.Text = dataGridViewshowuser.Rows[e.RowIndex].Cells["firstname"].FormattedValue.ToString();
            text_user_Lastname.Text = dataGridViewshowuser.Rows[e.RowIndex].Cells["lastname"].FormattedValue.ToString();
            text_user_Phone.Text = dataGridViewshowuser.Rows[e.RowIndex].Cells["phone"].FormattedValue.ToString();
            text_user_Address.Text = dataGridViewshowuser.Rows[e.RowIndex].Cells["address"].FormattedValue.ToString();
            text_user_idcard.Text = dataGridViewshowuser.Rows[e.RowIndex].Cells["ID_card"].FormattedValue.ToString();
            text_user_Status.Text = dataGridViewshowuser.Rows[e.RowIndex].Cells["status"].FormattedValue.ToString();
        }
        private void dataGridViewshowuser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewshowuser.CurrentRow.Selected = true;
            text_user_Username.Text = dataGridViewshowuser.Rows[e.RowIndex].Cells["username"].FormattedValue.ToString();
            text_user_Password.Text = dataGridViewshowuser.Rows[e.RowIndex].Cells["password"].FormattedValue.ToString();
            text_user_Firstname.Text = dataGridViewshowuser.Rows[e.RowIndex].Cells["firstname"].FormattedValue.ToString();
            text_user_Lastname.Text = dataGridViewshowuser.Rows[e.RowIndex].Cells["lastname"].FormattedValue.ToString();
            text_user_Phone.Text = dataGridViewshowuser.Rows[e.RowIndex].Cells["phone"].FormattedValue.ToString();
            text_user_Address.Text = dataGridViewshowuser.Rows[e.RowIndex].Cells["address"].FormattedValue.ToString();
            text_user_idcard.Text = dataGridViewshowuser.Rows[e.RowIndex].Cells["ID_card"].FormattedValue.ToString();
            text_user_Status.Text = dataGridViewshowuser.Rows[e.RowIndex].Cells["status"].FormattedValue.ToString();
        }
        private void btn_logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            from_login stck = new from_login();
            stck.Show();
        }

        private void btn_admin_stock_Click(object sender, EventArgs e)
        {
            this.Hide();
            from_admin stck = new from_admin();
            stck.Show();
        }

        private void btn_salehistory_Click(object sender, EventArgs e)
        {
            this.Hide();
            from_history stck = new from_history();
            stck.Show();
        }
        private void text_user_Firstname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 65 || e.KeyChar > 90) && (e.KeyChar < 97 || e.KeyChar > 122) && (e.KeyChar != 8) && (e.KeyChar != 32))
            {
                e.Handled = true;
            }
        }

        private void text_user_Lastname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 65 || e.KeyChar > 90) && (e.KeyChar < 97 || e.KeyChar > 122) && (e.KeyChar != 8) && (e.KeyChar != 32))
            {
                e.Handled = true;
            }
        }

        private void text_user_Phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
            }
        }

        private void text_user_idcard_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
            }
        }

        private void text_user_Status_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 65 || e.KeyChar > 90) && (e.KeyChar < 97 || e.KeyChar > 122) && (e.KeyChar != 8) && (e.KeyChar != 32))
            {
                e.Handled = true;
            }
        }

    }
}
