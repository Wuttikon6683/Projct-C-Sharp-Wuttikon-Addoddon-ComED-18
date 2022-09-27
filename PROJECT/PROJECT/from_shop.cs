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
    public partial class from_shop : Form
    {
        public String namez;
        String nn;
        String mv;
        String nametrue;
        String pricee;
        String menu;
        List<String> de; // product name
        List<String> pri; // product price
        MySqlConnection connn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=newnicotineshop;");
        DataTable dt;
        MySqlDataAdapter adpt;
        int na = 0;
        int ml = 0;
        int sumbt = 0;
        int total = 0;
        int sub = 0;

        private void showdataGridView_e_cigarette()
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT product_name,product_price,product_amount,product_details,product_link FROM stock";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }
        private void showdataGridView_saltnic()
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT product_name,product_price,product_amount,product_details,product_link FROM salt_nic";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }
        private void showdataGridView_coil()
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT product_name,product_price,product_amount,product_details,product_link FROM coil";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=newnicotineshop;";

            MySqlConnection conn = new MySqlConnection(connectionString);

            return conn;
        }
        public from_shop()
        {
            InitializeComponent();
            showdataGridView_e_cigarette();
            btn_saltnic_pickup.Hide();
            btn_coil_pickup.Hide();
            btn_search_saltnic.Hide();
            btn_search_coil.Hide();
            text_link.Hide();
            label7.Hide();
            arrow_saltnic.Hide();
            arrow_coil.Hide();
            arrow_shopping.Hide();
        }
        private void from_e_cigarette_Load(object sender, EventArgs e)
        {
            panel_shopping.Hide();
            label_date.Text = System.DateTime.Now.ToShortDateString();
            label_time.Text = System.DateTime.Now.ToShortTimeString();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            text_name.Text = dataGridView1.Rows[e.RowIndex].Cells["product_name"].FormattedValue.ToString();
            text_price.Text = dataGridView1.Rows[e.RowIndex].Cells["product_price"].FormattedValue.ToString();
            text_instock.Text = dataGridView1.Rows[e.RowIndex].Cells["product_amount"].FormattedValue.ToString();
            text_detail.Text = dataGridView1.Rows[e.RowIndex].Cells["product_details"].FormattedValue.ToString();
            text_link.Text = dataGridView1.Rows[e.RowIndex].Cells["product_link"].FormattedValue.ToString();
            pictureBoxzaza.ImageLocation = $@"{text_link.Text}";
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void btn_e_cigarette_pickup_Click(object sender, EventArgs e)
        {
            if (text_name.Text == "" || text_price.Text == "" || text_instock.Text == "" || text_link.Text == "" || text_detail.Text == "")
            {
                MessageBox.Show("กรุณาเลือกสินค้า");
            }
            else
            {
                try
                {
                    int stockk = int.Parse(text_instock.Text);
                    if (stockk > 0)
                    {
                        nn = ($"{text_price.Text}");
                        int mb = int.Parse(nn); // เปลี่ยน product_price จาก  string ไปเป็น int เพื่อใช้ในการคำนวณ
                        text_shop_total.Clear();
                        na = mb + na;
                        text_shop_total.SelectedText = ($"{na}"); // ราคารวมไปใส่ไว้ใน text_shop_total

                        total = na;
                        richTextBox_name.SelectedText = ($"{text_name.Text}\n");
                        richTextBox_price.SelectedText = ($"{text_price.Text}\n");
                        sumbt += 1; // เพิ่มในบิล
                        stockk -= 1; // ลบสต๊อก

                        MySqlConnection conn = databaseConnection();
                        MySqlCommand cmd = conn.CreateCommand();
                        conn.Open();
                        cmd.CommandText = $"UPDATE stock SET product_amount = \"{stockk}\" WHERE product_name = \"{text_name.Text}\""; // อัพเดตข้อมูลสต๊อก จาก stockk ที่เปลี่ยนค่ามาเป็น int แล้ว  
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show($"เลือกสินค้าเรียบร้อย");
                            showdataGridView_e_cigarette();
                            text_instock.Text = stockk.ToString(); // จะเป็นการอัพเดต stock หลังจากที่ได้เลือกสินค้าไป
                        }
                    }
                    else
                    {
                        MessageBox.Show("สินค้าหมด");
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void btn_saltnic_pickup_Click(object sender, EventArgs e)
        {
            if (text_name.Text == "" || text_price.Text == "" || text_instock.Text == "" || text_link.Text == "" || text_detail.Text == "")
            {
                MessageBox.Show("กรุณาเลือกสินค้า");
            }
            else
            {
                try
                {
                    int stockk = int.Parse(text_instock.Text);
                    if (stockk > 0)
                    {
                        nn = ($"{text_price.Text}");
                        int mb = int.Parse(nn);
                        text_shop_total.Clear();
                        na = mb + na;
                        text_shop_total.SelectedText = ($"{na}");
                        total = na;
                        richTextBox_name.SelectedText = ($"{text_name.Text}\n");
                        richTextBox_price.SelectedText = ($"{text_price.Text}\n");
                        sumbt += 1;
                        stockk -= 1;

                        MySqlConnection conn = databaseConnection();
                        MySqlCommand cmd = conn.CreateCommand();
                        conn.Open();
                        cmd.CommandText = $"UPDATE salt_nic SET product_amount = \"{stockk}\" WHERE product_name = \"{text_name.Text}\"";
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show($"เลือกสินค้าเรียบร้อย");
                            showdataGridView_saltnic();
                            text_instock.Text = stockk.ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("สินค้าหมด");
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void btn_coil_pickup_Click(object sender, EventArgs e)
        {
            if (text_name.Text == "" || text_price.Text == "" || text_instock.Text == "" || text_link.Text == "" || text_detail.Text == "")
            {
                MessageBox.Show("กรุณาเลือกสินค้า");
            }
            else
            {
                try
                {
                    int stockk = int.Parse(text_instock.Text);
                    if (stockk > 0)
                    {
                        nn = ($"{text_price.Text}");
                        int mb = int.Parse(nn);
                        text_shop_total.Clear();
                        na = mb + na;
                        text_shop_total.SelectedText = ($"{na}");
                        total = na;
                        richTextBox_name.SelectedText = ($"{text_name.Text}\n");
                        richTextBox_price.SelectedText = ($"{text_price.Text}\n");
                        sumbt += 1;
                        stockk -= 1;

                        MySqlConnection conn = databaseConnection();
                        MySqlCommand cmd = conn.CreateCommand();
                        conn.Open();
                        cmd.CommandText = $"UPDATE coil SET product_amount = \"{stockk}\" WHERE product_name = \"{text_name.Text}\"";
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show($"เลือกสินค้าเรียบร้อย");
                            showdataGridView_coil();
                            text_instock.Text = stockk.ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("สินค้าหมด");
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void num_load() // สร้างฟังชันก์แสดงลำดับสินค้าที่เราซื้อ
        {
            richTextBox_id.Clear(); // เป็นการ split new line ของลำดับสินค้า
            string[] productname = richTextBox_name.Text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            int loop = productname.Length;
            int n = 0;
            for (int i = 0; i < loop - 1; i++)
            {
                if (productname[i] != "")
                {
                    n += 1;
                    richTextBox_id.Text += $"{n}\n";
                }
            }
        }
        private void btn_buy_Click(object sender, EventArgs e)
        {
            mv = ($"{text_shop_getit.Text}");
            int mk = int.Parse(mv);
            sub = mk;
            if (mk >= na)
            {
                ml = mk - na;
                na = 0;
                MySqlConnection conn = databaseConnection();
                String sql = "INSERT INTO history (name_customer,product_customer,total,date,time) VALUES('" + namez + "','" + richTextBox_name.Text + "','" + text_shop_total.Text + "','" + label_date.Text + "','" + label_time.Text + "')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                conn.Close();
                pricee = richTextBox_name.Text; // ประกาศชื่อเพื่อให้ข้อมูลเข้าไปอยู่ใน Bill ด้วย
                menu = richTextBox_price.Text;

                richTextBox_name.Clear();
                richTextBox_price.Clear();

                mk = 0;
                text_shop_getit.Clear();
                text_shop_total.Clear();
                MessageBox.Show("Thank You");
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }
            if (mk < na)
            {
                MessageBox.Show("จำนวนเงินไม่เพียงพอ");
            }
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            MySqlConnection conn = databaseConnection();
            MySqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandText = $"SELECT firstname FROM account WHERE username = \"{namez}\"";
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                nametrue = Convert.ToString(dr.GetValue(0).ToString());

            }
            conn.Close();
            e.Graphics.DrawString("\t\t         Bill", new Font("Century Gothic", 24, FontStyle.Bold), Brushes.Black, new Point(50, 30));
            e.Graphics.DrawString("\t           NICOTINE SHOP", new Font("Century Gothic", 26, FontStyle.Bold), Brushes.Black, new Point(50, 90));
            e.Graphics.DrawString("Date " + System.DateTime.Now.ToString("dd/MM/yyyy HH : mm : ss น."), new Font("Century Gothic", 14, FontStyle.Regular), Brushes.Black, new PointF(500, 150));
            e.Graphics.DrawString("-----------------------------------------------------------------------------", new Font("Century Gothic", 16, FontStyle.Regular), Brushes.Black, new Point(80, 190));
            e.Graphics.DrawString(" ITEM                                                         PRICE(Bath)", new Font("Century Gothic", 20, FontStyle.Regular), Brushes.Black, new Point(130, 220));
            e.Graphics.DrawString("-----------------------------------------------------------------------------", new Font("Century Gothic", 16, FontStyle.Regular), Brushes.Black, new Point(80, 250));
            int y = 320;
            int number = 1;
            int i = 0;
            e.Graphics.DrawString("" + pricee, new Font("Century Gothic", 16, FontStyle.Regular), Brushes.Black, new PointF(80, 280));
            e.Graphics.DrawString("" + menu, new Font("Century Gothic", 16, FontStyle.Regular), Brushes.Black, new PointF(650, 280));

            for (i = 0; i < sumbt; i += 1)
            {
                y = y + 23;
            }

            number = number + 1;
            {
                e.Graphics.DrawString("-----------------------------------------------------------------------------", new Font("Century Gothic", 16, FontStyle.Regular), Brushes.Black, new Point(80, y + 20));
                e.Graphics.DrawString("SUBTOTAL        " + total.ToString() + " Bath", new Font("Century Gothic", 16, FontStyle.Regular), Brushes.Black, new Point(500, (y + 30) + 45));

                e.Graphics.DrawString("GET IT               " + sub + " Bath", new Font("Century Gothic", 16, FontStyle.Regular), Brushes.Black, new Point(500, ((y + 30) + 45) + 45));
                e.Graphics.DrawString("CHANCE           " + ml + " Bath", new Font("Century Gothic", 16, FontStyle.Regular), Brushes.Black, new Point(500, (((y + 30) + 45) + 45) + 45));


            }
            total = 0;
            sumbt = 0;
        }
        private void btn_delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("คุณแน่ใจหรือว่าลบรายการ", "ลบรายการ", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string[] productname = richTextBox_name.Text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                string[] productprice = richTextBox_price.Text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                richTextBox_name.Clear();
                richTextBox_price.Clear();
                na = 0;
                text_shop_getit.Clear();

                de = productname.ToList(); // list ของ productname
                pri = productprice.ToList(); // list ของ productprice
                int num_de = int.Parse(text_shop_deleteid.Text); // เอาไว้ใช้ลบข้อมูลใน text_shop_deleteid

                String nameproductstock = de[num_de - 1];

                de.Remove(de[num_de - 1]); // เอาข้อมูลออกจาก list productname จากข้อมูลที่คีย์ไปใน text_shop_deleteid
                pri.Remove(pri[num_de - 1]); // เอาข้อมูลออกจาก list productprice จากข้อมูลที่คีย์ไปใน text_shop_deleteid
                int loop = de.Count(); //  .Count นับว่าจำนวนใน de มีกี่ตัว
                for (int i = 0; i < loop; i++) // เป็นการ loop reset ลำดับของสินค้า เช่น ลำดับที่ 2 ออก จะเป็น 1 2 จะไม่เป็น  1 3
                {
                    richTextBox_name.Text += de[i] + "\n"; // เอา list de ตำแหน่งที่ i มาไว้อันดับแรก และ loop ไปเรื่อยๆ
                    richTextBox_price.Text += pri[i] + "\n"; // เอา list pri ตำแหน่งที่ i มาไว้อันดับแรก และ loop ไปเรื่อยๆ
                }

                int total = 0;
                for (int i = 0; i < (pri.Count() - 1); i++) // เอาราคาที่ลบสินค้าออกไปแล้ว มาบวกก็จะได้ total ตัวใหม่
                {
                    if (pri[i] != "")
                    {
                        int pric = int.Parse(pri[i]);
                        total += pric;
                    }

                }
                text_shop_total.Text = total.ToString();
                MessageBox.Show("ลบรายการ", "ลบ");
                num_load();

                for (int i = 0; i < loop - 1; i++)
                {
                    ////////// stock //////////
                    MySqlConnection conn = databaseConnection();
                    MySqlCommand cmd = conn.CreateCommand();
                    conn.Open();
                    cmd.CommandText = $"SELECT product_amount FROM stock WHERE product_name = \"{nameproductstock[i]}\"";
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        int stockk = Convert.ToInt32(dr.GetValue(0).ToString());
                        stockk += 1;
                        MySqlConnection conn2 = databaseConnection();
                        MySqlCommand cmd2 = conn2.CreateCommand();
                        conn2.Open();
                        cmd2.CommandText = $"UPDATE stock SET product_amount = \"{stockk}\" WHERE product_name = \"{nameproductstock[i]}\"";
                        int rows = cmd2.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            showdataGridView_e_cigarette();
                        }
                        conn2.Close();

                    }
                    conn.Close();
                    //////////////////////////
                    
                    ///////// saltnic ////////
                    MySqlConnection connn = databaseConnection();
                    MySqlCommand cmdd = connn.CreateCommand();
                    connn.Open();
                    cmdd.CommandText = $"SELECT product_amount FROM salt_nic WHERE product_name = \"{productname[i]}\"";
                    MySqlDataReader drr = cmdd.ExecuteReader();
                    if (drr.Read())
                    {
                        int stockk = Convert.ToInt32(drr.GetValue(0).ToString());
                        stockk += 1;
                        MySqlConnection conn3 = databaseConnection();
                        MySqlCommand cmd3 = conn3.CreateCommand();
                        conn3.Open();
                        cmd3.CommandText = $"UPDATE salt_nic SET product_amount = \"{stockk}\" WHERE product_name = \"{productname[i]}\"";
                        int rows = cmd3.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            showdataGridView_saltnic();
                        }
                        conn3.Close();

                    }
                    conn.Close();
                    //////////////////////////

                    ////////// coil //////////
                    MySqlConnection conn5 = databaseConnection();
                    MySqlCommand cmd5 = conn5.CreateCommand();
                    conn5.Open();
                    cmd5.CommandText = $"SELECT product_amount FROM coil WHERE product_name = \"{productname[i]}\"";
                    MySqlDataReader dr5 = cmd5.ExecuteReader();
                    if (dr5.Read())
                    {
                        int stockk = Convert.ToInt32(dr5.GetValue(0).ToString());
                        stockk += 1;
                        MySqlConnection conn2 = databaseConnection();
                        MySqlCommand cmd2 = conn2.CreateCommand();
                        conn2.Open();
                        cmd2.CommandText = $"UPDATE coil SET product_amount = \"{stockk}\" WHERE product_name = \"{productname[i]}\"";
                        int rows = cmd2.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            showdataGridView_coil();
                        }
                        conn2.Close();

                    }
                    conn.Close();
                    //////////////////////////
                    text_shop_deleteid.Clear();
                }
            }
        }
        private void btn_clear_e_cigarette_Click(object sender, EventArgs e)
        {
            {
                string[] productname = richTextBox_name.Text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                richTextBox_name.Clear();
                richTextBox_price.Clear();
                na = 0;
                text_shop_getit.Clear();

                int loop = productname.Length;
                for (int i = 0; i < loop - 1; i++)
                {
                    ////////// stock //////////
                    MySqlConnection conn = databaseConnection();
                    MySqlCommand cmd = conn.CreateCommand();
                    conn.Open();
                    cmd.CommandText = $"SELECT product_amount FROM stock WHERE product_name = \"{productname[i]}\"";
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        int stockk = Convert.ToInt32(dr.GetValue(0).ToString());
                        stockk += 1;
                        MySqlConnection conn2 = databaseConnection();
                        MySqlCommand cmd2 = conn2.CreateCommand();
                        conn2.Open();
                        cmd2.CommandText = $"UPDATE stock SET product_amount = \"{stockk}\" WHERE product_name = \"{productname[i]}\"";
                        int rows = cmd2.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            showdataGridView_e_cigarette();
                        }
                        conn2.Close();

                    }
                    conn.Close();
                    //////////////////////////

                    ///////// saltnic ////////
                    MySqlConnection connn = databaseConnection();
                    MySqlCommand cmdd = connn.CreateCommand();
                    connn.Open();
                    cmdd.CommandText = $"SELECT product_amount FROM salt_nic WHERE product_name = \"{productname[i]}\"";
                    MySqlDataReader drr = cmdd.ExecuteReader();
                    if (drr.Read())
                    {
                        int stockk = Convert.ToInt32(drr.GetValue(0).ToString());
                        stockk += 1;
                        MySqlConnection conn3 = databaseConnection();
                        MySqlCommand cmd3 = conn3.CreateCommand();
                        conn3.Open();
                        cmd3.CommandText = $"UPDATE salt_nic SET product_amount = \"{stockk}\" WHERE product_name = \"{productname[i]}\"";
                        int rows = cmd3.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            showdataGridView_saltnic();
                        }
                        conn3.Close();

                    }
                    conn.Close();
                    //////////////////////////

                    ////////// coil //////////
                    MySqlConnection conn5 = databaseConnection();
                    MySqlCommand cmd5 = conn5.CreateCommand();
                    conn5.Open();
                    cmd5.CommandText = $"SELECT product_amount FROM coil WHERE product_name = \"{productname[i]}\"";
                    MySqlDataReader dr5 = cmd5.ExecuteReader();
                    if (dr5.Read())
                    {
                        int stockk = Convert.ToInt32(dr5.GetValue(0).ToString());
                        stockk += 1;
                        MySqlConnection conn2 = databaseConnection();
                        MySqlCommand cmd2 = conn2.CreateCommand();
                        conn2.Open();
                        cmd2.CommandText = $"UPDATE coil SET product_amount = \"{stockk}\" WHERE product_name = \"{productname[i]}\"";
                        int rows = cmd2.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            showdataGridView_coil();
                        }
                        conn2.Close();

                    }
                    conn.Close();
                    //////////////////////////
                }

            }
            MessageBox.Show("เคลียร์สินค้าเรียบร้อย");
        }
        private void btn_search_stock_Click(object sender, EventArgs e)
        {
            if (textBox_search.Text != "")
            {
                MySqlConnection conn = databaseConnection();
                string query = "SELECT * from stock where product_name like '" + textBox_search.Text + "%'";
                conn.Open();
                MySqlCommand sqlcomm = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlcomm);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            else
            {
                showdataGridView_e_cigarette();
            }
        }

        private void btn_search_saltnic_Click(object sender, EventArgs e)
        {
            if (textBox_search.Text != "")
            {
                MySqlConnection conn = databaseConnection();
                string query = "SELECT * from salt_nic where product_name like '" + textBox_search.Text + "%'";
                conn.Open();
                MySqlCommand sqlcomm = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlcomm);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            else
            {
                showdataGridView_saltnic();
            }
        }

        private void btn_search_coil_Click(object sender, EventArgs e)
        {
            if (textBox_search.Text != "")
            {
                MySqlConnection conn = databaseConnection();
                string query = "SELECT * from coil where product_name like '" + textBox_search.Text + "%'";
                conn.Open();
                MySqlCommand sqlcomm = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlcomm);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            else
            {
                showdataGridView_coil();
            }
        }
        private void btn_e_cigarette_Click(object sender, EventArgs e)
        {
            showdataGridView_e_cigarette();
            btn_e_cigarette_pickup.Show();
            btn_saltnic_pickup.Hide();
            btn_coil_pickup.Hide();
            panel_shopping.Hide();
            text_link.Hide();
            label7.Hide();
            arrow_saltnic.Hide();
            arrow_coil.Hide();
            arrow_shopping.Hide();
            arrow_e_cigarette.Show();
            btn_search_stock.Show();
            btn_search_saltnic.Hide();
            btn_search_coil.Hide();

            text_name.Clear();
            text_price.Clear();
            text_instock.Clear();
            text_detail.Clear();
        }
 
        private void btn_saltnic_Click(object sender, EventArgs e)
        {
            showdataGridView_saltnic();
            btn_saltnic_pickup.Show();
            btn_e_cigarette_pickup.Hide();
            btn_coil_pickup.Hide();
            panel_shopping.Hide();
            text_link.Hide();
            label7.Hide();
            arrow_e_cigarette.Hide();
            arrow_coil.Hide();
            arrow_shopping.Hide();
            arrow_saltnic.Show();
            btn_search_saltnic.Show();
            btn_search_stock.Hide();
            btn_search_coil.Hide();

            text_name.Clear();
            text_price.Clear();
            text_instock.Clear();
            text_detail.Clear();
        }
        private void btn_coil_Click(object sender, EventArgs e)
        {
            showdataGridView_coil();
            btn_coil_pickup.Show();
            btn_e_cigarette_pickup.Hide();
            btn_saltnic_pickup.Hide();
            panel_shopping.Hide();
            text_link.Hide();
            label7.Hide();
            arrow_e_cigarette.Hide();
            arrow_saltnic.Hide();
            arrow_shopping.Hide();
            arrow_coil.Show();
            btn_search_coil.Show();
            btn_search_stock.Hide();
            btn_search_saltnic.Hide();


            text_name.Clear();
            text_price.Clear();
            text_instock.Clear();
            text_detail.Clear();
        }
        private void btn_shopping_Click(object sender, EventArgs e)
        {
            panel_shopping.Show();
            num_load();
            btn_e_cigarette_pickup.Hide();
            btn_saltnic_pickup.Hide();
            btn_coil_pickup.Hide();
            arrow_e_cigarette.Hide();
            arrow_saltnic.Hide();
            arrow_coil.Hide();
            arrow_shopping.Show();
        }
        private void btn_logout_Click(object sender, EventArgs e)
        {
            this.Close();
            from_login stck = new from_login();
            stck.Show();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            text_name.Text = dataGridView1.Rows[e.RowIndex].Cells["product_name"].FormattedValue.ToString();
            text_price.Text = dataGridView1.Rows[e.RowIndex].Cells["product_price"].FormattedValue.ToString();
            text_instock.Text = dataGridView1.Rows[e.RowIndex].Cells["product_amount"].FormattedValue.ToString();
            text_detail.Text = dataGridView1.Rows[e.RowIndex].Cells["product_details"].FormattedValue.ToString();
            text_link.Text = dataGridView1.Rows[e.RowIndex].Cells["product_link"].FormattedValue.ToString();
            pictureBoxzaza.ImageLocation = $@"{text_link.Text}";
        }
        private void text_shop_getit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
            }
        }
        private void text_link_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_search_TextChanged(object sender, EventArgs e)
        {
            
        }

    }
}
