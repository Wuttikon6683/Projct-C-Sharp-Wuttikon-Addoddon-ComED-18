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
    public partial class from_admin : Form
    {
        int X = 0;
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=newnicotineshop;";

            MySqlConnection conn = new MySqlConnection(connectionString);

            return conn;
        }
        public from_admin()
        {
            InitializeComponent();
        }

        private void from_admin_Load(object sender, EventArgs e)
        {

        }

        private void btn_stock_clear_Click(object sender, EventArgs e)
        {
            text_stock_idproduct.Text = "";
            text_stock_nameproduct.Text = "";
            text_stock_priceproduct.Text = "";
            text_stock_amountproduct.Text = "";
            text_stock_detailproduct.Text = "";
            text_stock_link.Text = "";
        }

        private void show_Stock_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            show_Stock.CurrentRow.Selected = true;
            text_stock_idproduct.Text = show_Stock.Rows[e.RowIndex].Cells["product_id"].FormattedValue.ToString();
            text_stock_nameproduct.Text = show_Stock.Rows[e.RowIndex].Cells["product_name"].FormattedValue.ToString();
            text_stock_priceproduct.Text = show_Stock.Rows[e.RowIndex].Cells["product_price"].FormattedValue.ToString();
            text_stock_amountproduct.Text = show_Stock.Rows[e.RowIndex].Cells["product_amount"].FormattedValue.ToString();
            text_stock_detailproduct.Text = show_Stock.Rows[e.RowIndex].Cells["product_details"].FormattedValue.ToString();
            text_stock_link.Text = show_Stock.Rows[e.RowIndex].Cells["product_link"].FormattedValue.ToString();
        }
        private void showe_cigarette()
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();

            MySqlCommand cmd;

            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM stock";

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);

            conn.Close();
            show_Stock.DataSource = ds.Tables[0].DefaultView;
        }

        private void btn_stock_stock_Click(object sender, EventArgs e)
        {
            showe_cigarette();
        }

        private void btn_stock_savestock_Click(object sender, EventArgs e)
        {
            if (text_stock_idproduct.Text == "" || text_stock_nameproduct.Text == "" || text_stock_detailproduct.Text == "" || text_stock_priceproduct.Text == "" || text_stock_amountproduct.Text == "" || text_stock_link.Text == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วน");
            }
            else
            {
                MySqlConnection conn1 = databaseConnection();
                conn1.Open();
                MySqlCommand cmd1;
                cmd1 = conn1.CreateCommand();
                cmd1.CommandText = $"SELECT*FROM stock WHERE product_id =\"{text_stock_idproduct.Text}\"";
                MySqlDataReader row = cmd1.ExecuteReader();
                if (row.HasRows)
                {
                    MessageBox.Show("ข้อมูลอยู่ในระบบแล้ว");
                    X = 2;
                }
                if (X == 0)
                {
                    MySqlConnection conn = databaseConnection();
                    String sql = "INSERT INTO stock (product_id,product_name,product_price,product_amount,product_details,product_link) VALUES('" + text_stock_idproduct.Text + "' , '" + text_stock_nameproduct.Text + "','" + text_stock_priceproduct.Text + "','" + text_stock_amountproduct.Text + "','" + text_stock_detailproduct.Text + "' ,'" + text_stock_link.Text + "')";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        MessageBox.Show("เพิ่มเรียบร้อยแล้ว");
                        showe_cigarette();
                    }
                }
            }
            
                
        }

        private void btn_stock_edit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("คุณแน่ใจหรือไม่ว่าต้องการแก้ไขผลิตภัณฑ์", "แก้ไขสินค้า", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int selectedRow = show_Stock.CurrentCell.RowIndex;
                int editId = Convert.ToInt32(show_Stock.Rows[selectedRow].Cells["id"].Value);
                MySqlConnection conn = databaseConnection();
                String sql = "UPDATE stock SET product_name = '" + text_stock_nameproduct.Text + "',product_price = '" + text_stock_priceproduct.Text + "' , product_id = '" + text_stock_idproduct.Text + "', product_amount = '" + text_stock_amountproduct.Text + "', product_details = '" + text_stock_detailproduct.Text + "', product_link = '" + text_stock_link.Text + "' WHERE id = '" + editId + "'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();
                if (rows > 0)
                {
                    MessageBox.Show("แก้ไขเรียบร้อยแล้ว");
                    showe_cigarette();
                }
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void btn_stock_delete_Click(object sender, EventArgs e)
        {


            DialogResult dialogResult = MessageBox.Show("คุณแน่ใจหรือไม่ว่าต้องการลบผลิตภัณฑ์", "ลบสินค้า", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int selectedRow = show_Stock.CurrentCell.RowIndex;
                int deleteID = Convert.ToInt32(show_Stock.Rows[selectedRow].Cells["ID"].Value);

                MySqlConnection conn = databaseConnection();
                String sql = "DELETE FROM stock WHERE ID = '" + deleteID + "' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();

                if (rows > 0)
                {

                    MessageBox.Show("ลบเรียบร้อยแล้ว");
                    showe_cigarette();
                }

            }
            else if (dialogResult == DialogResult.No)
            {
                
            }
        }
        private void showsaltnic()
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();

            MySqlCommand cmd;

            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM salt_nic";

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);

            conn.Close();
            show_Stock.DataSource = ds.Tables[0].DefaultView;
        }
        private void btn_stock_saltnic_Click(object sender, EventArgs e)
        {
            showsaltnic();
        }

        private void btn_stock_savesaltnic_Click(object sender, EventArgs e)
        {
            if (text_stock_idproduct.Text == "" || text_stock_nameproduct.Text == "" || text_stock_detailproduct.Text == "" || text_stock_priceproduct.Text == "" || text_stock_amountproduct.Text == "" || text_stock_link.Text == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วน");
            }
            else
            {
                MySqlConnection conn1 = databaseConnection();
                conn1.Open();
                MySqlCommand cmd1;
                cmd1 = conn1.CreateCommand();
                cmd1.CommandText = $"SELECT*FROM salt_nic WHERE product_id =\"{text_stock_idproduct.Text}\"";
                MySqlDataReader row = cmd1.ExecuteReader();
                if (row.HasRows)
                {
                    MessageBox.Show("ข้อมูลอยู่ในระบบแล้ว");
                    X = 2;
                }
                if (X == 0)
                {
                    MySqlConnection conn = databaseConnection();
                    String sql = "INSERT INTO salt_nic (product_id,product_name,product_price,product_amount,product_details,product_link) VALUES('" + text_stock_idproduct.Text + "' , '" + text_stock_nameproduct.Text + "','" + text_stock_priceproduct.Text + "','" + text_stock_amountproduct.Text + "','" + text_stock_detailproduct.Text + "' ,'" + text_stock_link.Text + "')";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        MessageBox.Show("เพิ่มเรียบร้อยแล้ว");
                        showsaltnic();
                    }
                }
            }
                
                
        }

        private void btn_stock_editsaltnic_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("คุณแน่ใจหรือไม่ว่าต้องการแก้ไขผลิตภัณฑ์", "แก้ไขสินค้า", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int selectedRow = show_Stock.CurrentCell.RowIndex;
                int editId = Convert.ToInt32(show_Stock.Rows[selectedRow].Cells["id"].Value);
                MySqlConnection conn = databaseConnection();
                String sql = "UPDATE salt_nic SET product_name = '" + text_stock_nameproduct.Text + "' , product_price = '" + text_stock_priceproduct.Text + "' , product_id = '" + text_stock_idproduct.Text + "', product_amount = '" + text_stock_amountproduct.Text + "', product_details = '" + text_stock_detailproduct.Text + "', product_link = '" + text_stock_link.Text + "' WHERE id = '" + editId + "'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();
                if (rows > 0)
                {
                    MessageBox.Show("แก้ไขเรียบร้อยแล้ว");
                    showsaltnic();
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }
        }

        private void btn_stock_deletesaltnic_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("คุณแน่ใจหรือไม่ว่าต้องการลบผลิตภัณฑ์", "ลบสินค้า", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int selectedRow = show_Stock.CurrentCell.RowIndex;
                int deleteID = Convert.ToInt32(show_Stock.Rows[selectedRow].Cells["ID"].Value);

                MySqlConnection conn = databaseConnection();
                String sql = "DELETE FROM salt_nic WHERE ID = '" + deleteID + "' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();

                if (rows > 0)
                {
                    MessageBox.Show("ลบเรียบร้อยแล้ว");
                    showsaltnic();
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }

        }
        private void showcoil()
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();

            MySqlCommand cmd;

            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM coil";

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);

            conn.Close();
            show_Stock.DataSource = ds.Tables[0].DefaultView;
        }

        private void btn_stock_coil_Click(object sender, EventArgs e)
        {
            showcoil();
        }

        private void btn_stock_savecoil_Click(object sender, EventArgs e)
        {
            if (text_stock_idproduct.Text == "" || text_stock_nameproduct.Text == "" || text_stock_detailproduct.Text == "" || text_stock_priceproduct.Text == "" || text_stock_amountproduct.Text == "" || text_stock_link.Text == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วน");
            }
            else
            {
                MySqlConnection conn1 = databaseConnection();
                conn1.Open();
                MySqlCommand cmd1;
                cmd1 = conn1.CreateCommand();
                cmd1.CommandText = $"SELECT*FROM coil WHERE product_id =\"{text_stock_idproduct.Text}\"";
                MySqlDataReader row = cmd1.ExecuteReader();
                if (row.HasRows)
                {
                    MessageBox.Show("ข้อมูลอยู่ในระบบแล้ว");
                    X = 2;
                }
                if (X == 0)
                {
                    MySqlConnection conn = databaseConnection();
                    String sql = "INSERT INTO coil (product_id,product_name,product_price,product_amount,product_details,product_link) VALUES('" + text_stock_idproduct.Text + "' , '" + text_stock_nameproduct.Text + "','" + text_stock_priceproduct.Text + "','" + text_stock_amountproduct.Text + "','" + text_stock_detailproduct.Text + "' ,'" + text_stock_link.Text + "')";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        MessageBox.Show("เพิ่มเรียบร้อยแล้ว");
                        showcoil();
                    }
                }
            }
                
                
        }

        private void btn_stock_editcoil_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("คุณแน่ใจหรือไม่ว่าต้องการแก้ไขผลิตภัณฑ์", "แก้ไขสินค้า", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int selectedRow = show_Stock.CurrentCell.RowIndex;
                int editId = Convert.ToInt32(show_Stock.Rows[selectedRow].Cells["id"].Value);
                MySqlConnection conn = databaseConnection();
                String sql = "UPDATE coil SET product_name = '" + text_stock_nameproduct.Text + "' , product_price = '" + text_stock_priceproduct.Text + "' , product_id = '" + text_stock_idproduct.Text + "', product_amount = '" + text_stock_amountproduct.Text + "', product_details = '" + text_stock_detailproduct.Text + "', product_link = '" + text_stock_link.Text + "' WHERE id = '" + editId + "'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();
                if (rows > 0)
                {
                    MessageBox.Show("แก้ไขเรียบร้อยแล้ว");
                    showcoil();
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }
        }

        private void btn_stock_deletecoil_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("คุณแน่ใจหรือไม่ว่าต้องการลบผลิตภัณฑ์", "ลบสินค้า", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int selectedRow = show_Stock.CurrentCell.RowIndex;
                int deleteID = Convert.ToInt32(show_Stock.Rows[selectedRow].Cells["ID"].Value);

                MySqlConnection conn = databaseConnection();
                String sql = "DELETE FROM coil WHERE ID = '" + deleteID + "' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();

                if (rows > 0)
                {
                    MessageBox.Show("ลบเรียบร้อยแล้ว");
                    showcoil();
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            from_login stck = new from_login();
            stck.Show();
        }
        private void btn_admin_Users_Click(object sender, EventArgs e)
        {
            this.Hide();
            from_users stck = new from_users();
            stck.Show();
        }
        private void btn_history_Click(object sender, EventArgs e)
        {
            this.Hide();
            from_history stck = new from_history();
            stck.Show();
        }
        private void btn_admin_stock_Click(object sender, EventArgs e)
        {
            this.Hide();
            from_admin stck = new from_admin();
            stck.Show();
        }
        private void text_stock_idproduct_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.Encoding.UTF8.GetByteCount(new char[] { e.KeyChar }) > 1)
            {
                e.Handled = true;
            }
        }
        private void btn_e_cigarette_Click(object sender, EventArgs e)
        {

        }

        private void text_stock_nameproduct_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 65 || e.KeyChar > 90) && (e.KeyChar < 97 || e.KeyChar > 122) && (e.KeyChar != 8) && (e.KeyChar != 32))
            {
                e.Handled = true;
            }
        }

        private void text_stock_priceproduct_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
            }
        }

        private void text_stock_amountproduct_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
            }
        }

        
    }
}
