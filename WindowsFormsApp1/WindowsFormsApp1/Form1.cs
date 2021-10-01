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
using System.Timers;
using System.Runtime.InteropServices;
namespace WindowsFormsApp1



{
    public partial class Form1 : Form
    {
        private const string Path = "C:/Users/SAM/Desktop/456/";
        

        public Form1()
        {
            InitializeComponent();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string pic = comboBox1.Text;
                pictureBox1.Image = Image.FromFile("C:/Users/sam/Desktop/saved_images/" + pic + ".jpg");
                label7.Text = comboBox1.Text;
                MySqlConnection connection = new MySqlConnection("Datasource=140.116.39.239;port=3306;username=root;password=luke1luke1;Database =aiot");
                string selectQuery = "SELECT * FROM " + label11.Text + " where time = " + "'" + comboBox1.Text + "'";
                connection.Open();
                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string people = reader.GetString("person");
                    string obj = reader.GetString("obj");
                    string[] p = people.Split(',', '_');//切資料庫抓到people資料，做字串處理
                    string[] o = obj.Split(',', '_');//切資料庫抓到Obj資料，做字串處理
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();//設定一個 list


                    if (o.Length == 3)
                    {
                        int o_i_1 = 2;
                        int p_j_1 = 1;
                        for (int n = 0; n <= p.Length; n++)
                        {
                            if (p_j_1 > p.Length)//如果p_j跟o_i大於原本長度就break
                            {
                                break;
                            }
                            if (o[o_i_1] == p[p_j_1])//如果person的號碼跟ob一樣
                            {
                                sb.Append(p[p_j_1 - 1] + p[p_j_1]);
                                sb.Append("  have: ");
                                sb.Append("\n                 ");
                                sb.Append(o[0] + o[1]);//append 物品跟序號
                            }
                            if (p_j_1 < p.Length)//如果p_j跟o_i大於原本長度就break
                            {
                                p_j_1 += 2;
                            }
                        }
                    }

                    if (o.Length > 3)
                    {
                        int o_i = 2;
                        int p_j = 1;
                        int check = 0;

                        for (int n = 0; n <= o.Length * p.Length; n++)
                        {
                            if (p_j > p.Length || o_i > o.Length)//如果p_j跟o_i大於原本長度就break
                            {
                                break;
                            }
                            if (check != p_j)
                            {
                                sb.Append(p[p_j - 1] + p[p_j]);
                                sb.Append("  have: ");
                                sb.Append("\n");

                                check = p_j;
                            }
                            if (o[o_i] == p[p_j])//如果person的號碼跟ob一樣
                            {
                                sb.Append("                 ");
                                sb.Append(o[o_i - 2] + o[o_i - 1]);//append 物品跟序號
                                sb.Append(" .");
                                sb.Append("\n");
                                o_i += 3;
                            }
                            else if (o_i < o.Length + 2)
                            {//如果o_i小於原本長度就繼續加o_i
                                o_i += 3;
                            }
                            if (o_i > o.Length)//如果obj count大於整體長度就歸零
                            {
                                sb.Append("\n");
                                p_j += 2;//p_j加2讓person的序號能夠繼續做處理
                                o_i = 2;//o_i需要規回到2
                            }
                        }
                    }

                    label3.Text = sb.ToString();//最後把sb印到label3
                }
            }
            catch {

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

 
        private void button3_Click(object sender, EventArgs e)//連結資料庫
        {
            comboBox1.Items.Clear();
            Form2 f = new Form2();
            MySqlConnection connection = new MySqlConnection("Datasource=140.116.39.239;port=3306;username=root;password=luke1luke1;Database =aiot");
            string selectQuery = "SELECT * FROM " + label11.Text;
            connection.Open();
            MySqlCommand command = new MySqlCommand(selectQuery, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetString("time"));
            }
   
        }

        private void button6_Click(object sender, EventArgs e)//利用物品來選擇的按鈕，並且會把資料庫結果放到listbox中
        {
            try
            {
                listBox1.Items.Clear();
                string thestartDate = dateTimePicker1.Value.ToString("yyyy-MM-dd_HHmmss");
                string theendDate = dateTimePicker2.Value.ToString("yyyy-MM-dd_HHmmss");

                MySqlConnection connection = new MySqlConnection("Datasource=140.116.39.239;port=3306;username=root;password=luke1luke1;Database =aiot");
                string selectQuery = "SELECT * FROM " + label11.Text + " where time > '" + thestartDate
                    + "' and time < '" + theendDate + "' and obj LIKE " + "'%" + textBox1.Text + "%'";

                connection.Open();
                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listBox1.Items.Add(reader.GetString("time"));
                }
            }
            catch {

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)//選擇Listbox鐘的時間會顯示圖片以及尋物結果
        {
            try
            {
                string pic = listBox1.Text;
                pictureBox1.Image = Image.FromFile("C:/Users/sam/Desktop/saved_images/" + pic + ".jpg");
                label7.Text = listBox1.Text;
                MySqlConnection connection = new MySqlConnection("Datasource=140.116.39.239;port=3306;username=root;password=luke1luke1;Database =aiot");
                string selectQuery = "SELECT * FROM " + label11.Text + " where time = " + "'" + listBox1.Text + "'";

                connection.Open();
                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string people = reader.GetString("person");
                    string obj = reader.GetString("obj");
                    string[] p = people.Split(',', '_');//切資料庫抓到people資料，做字串處理
                    string[] o = obj.Split(',', '_');//切資料庫抓到Obj資料，做字串處理
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();//設定一個 list


                    if (o.Length == 3)
                    {
                        int o_i_1 = 2;
                        int p_j_1 = 1;
                        for (int n = 0; n <= p.Length; n++)
                        {
                            if (p_j_1 > p.Length)//如果p_j跟o_i大於原本長度就break
                            {
                                break;
                            }
                            if (o[o_i_1] == p[p_j_1])//如果person的號碼跟ob一樣
                            {
                                sb.Append(p[p_j_1 - 1] + p[p_j_1]);
                                sb.Append("  have: ");
                                sb.Append("\n                 ");
                                sb.Append(o[0] + o[1]);//append 物品跟序號
                            }
                            if (p_j_1 < p.Length)//如果p_j跟o_i大於原本長度就break
                            {
                                p_j_1 += 2;
                            }
                        }
                    }

                    if (o.Length > 3)
                    {
                        int o_i = 2;
                        int p_j = 1;
                        int check = 0;

                        for (int n = 0; n <= o.Length * p.Length; n++)
                        {
                            if (p_j > p.Length || o_i > o.Length)//如果p_j跟o_i大於原本長度就break
                            {
                                break;
                            }
                            if (check != p_j)
                            {
                                sb.Append(p[p_j - 1] + p[p_j]);
                                sb.Append("  have: ");
                                sb.Append("\n");

                                check = p_j;
                            }
                            if (o[o_i] == p[p_j])//如果person的號碼跟ob一樣
                            {
                                sb.Append("                 ");
                                sb.Append(o[o_i - 2] + o[o_i - 1]);//append 物品跟序號
                                sb.Append(" .");
                                sb.Append("\n");
                                o_i += 3;
                            }
                            else if (o_i < o.Length + 2)
                            {//如果o_i小於原本長度就繼續加o_i
                                o_i += 3;
                            }
                            if (o_i > o.Length)//如果obj count大於整體長度就歸零
                            {
                                sb.Append("\n");
                                p_j += 2;//p_j加2讓person的序號能夠繼續做處理
                                o_i = 2;//o_i需要規回到2
                            }
                        }
                    }

                    label3.Text = sb.ToString();//最後把sb印到label3
                }
            }
            catch
            {

            }
        }

        private void textBox1_Click(object sender, EventArgs e)//點選Textbox預設字會消失
        {
            textBox1.Text = "";
            textBox1.ForeColor = Color.Black;
        }
    }
}