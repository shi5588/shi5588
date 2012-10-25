using System;
using System.Windows.Forms;

namespace app3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //
            RMCrypt rm = new RMCrypt(this.textBox4.Text);
            this.textBox2.Text = rm.Encrypt(this.textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //
            RMCrypt rm = new RMCrypt(this.textBox4.Text);
            this.textBox3.Text = rm.Decrypt(this.textBox2.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RMCrypt rm = new RMCrypt();
            this.textBox2.Text = rm.Encrypt(this.textBox1.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RMCrypt rm = new RMCrypt();
            this.textBox3.Text = rm.Decrypt(this.textBox2.Text);
        }
    }
}
