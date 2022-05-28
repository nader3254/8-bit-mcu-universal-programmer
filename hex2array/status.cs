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
    public partial class status : Form
    {
        string codee;
        public status(string x,string code)
        {
            InitializeComponent();
            label1.Text = x;
            timer1.Start();
            codee = code;
        }

        private void status_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Hide();
            outputdisplayer s = new outputdisplayer(codee);
            //this.Hide();
            s.Show();
            timer1.Stop();

        }
    }
}
