using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hex2array
{
    public partial class outputdisplayer : Form
    {
        public outputdisplayer(string code )
        {
            InitializeComponent();
            this.richTextBox1.Text = code;

          

      

         

               
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            this.button1.BackColor = ColorTranslator.FromHtml("#00FFFF");
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            this.button1.BackColor = ColorTranslator.FromHtml("#0F6FAD");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flasher f = new flasher(richTextBox1.Text);
            f.Show();
            this.Hide();
        }
    }
}
