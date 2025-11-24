using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source = AngadLegion\\MSSQLSERVER01; DataBase = CSDB; Integrated Security=SSPI; TrustServerCertificate = True");
            cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            LoadData();
        }

        private void LoadData()
        {
            cmd.CommandText = "SELECT * FROM Emp";
            dr = cmd.ExecuteReader();
            ShowData();
        }
        private void ShowData()
        {
            if (dr.Read())
            {
                textBox1.Text = dr[0].ToString();
                textBox2.Text = dr[1].ToString();
                textBox3.Text = dr[2].ToString();
                textBox4.Text = dr[5].ToString();
            }
            else
            {
                MessageBox.Show("End of Records");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowData();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox tb = ctrl as TextBox;
                    tb.Clear();
                }
            }
            dr.Close();
            cmd.CommandText = "Select IsNull(Max(Empno), 1000) + 1 From Emp";
            textBox1.Text = cmd.ExecuteScalar().ToString();
            textBox2.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmd.CommandText = "Insert Into Emp (Empno,Ename,Job, Salary) Values(" + textBox1.Text + ", '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "')";
            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Insert operations is successful.", "Success", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                LoadData();
            }
            else
            {
                MessageBox.Show("Failed inserting record into the table.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Update operation is successful.", "Success", MessageBoxButtons.OK);
            cmd.CommandText = "Delete From Emp Where Empno = " + textBox1.Text;
        }
    }
}
