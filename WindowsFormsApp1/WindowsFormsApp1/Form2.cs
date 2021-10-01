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

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "library";
            Form1 f = new Form1();//產生Form2的物件，才可以使用它所提供的Method

            this.Visible = true;//將Form1隱藏。由於在Form1的程式碼內使用this，所以this為Form1的物件本身
            f.Visible = true;//顯示第二個視窗
                             //f.ShowDialog();
            f.label11.Text = label1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "classroom";
            Form1 f = new Form1();//產生Form2的物件，才可以使用它所提供的Method

            this.Visible = false;//將Form1隱藏。由於在Form1的程式碼內使用this，所以this為Form1的物件本身
            f.Visible = true;//顯示第二個視窗
            //f.ShowDialog();
            f.label11.Text = label1.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Text = "demo";
            Form1 f = new Form1();//產生Form2的物件，才可以使用它所提供的Method

            this.Visible = false;//將Form1隱藏。由於在Form1的程式碼內使用this，所以this為Form1的物件本身
            f.Visible = true;//顯示第二個視窗
            //f.ShowDialog();
            f.label11.Text = label1.Text;
        }
    }
}
