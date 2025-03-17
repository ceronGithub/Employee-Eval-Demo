using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace emp_eval_full_version
{
    public partial class Form2 : Form
    {
        Form1 f1 = new Form1();
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f1.Reciever(textBox1.Text, textBox2.Text);
            MessageBox.Show("Grades has been updated!", "NOTE!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            f1.Show();
            this.Close();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            f1.Show();
        }
    }
}
