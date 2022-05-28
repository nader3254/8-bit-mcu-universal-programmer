using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace hex2array
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

     

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.BackColor = ColorTranslator.FromHtml("#0B0F18");
            panel2.BackColor = ColorTranslator.FromHtml("#00FFFF");
            timer1.Start();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (panel1.Width >= panel2.Width)
            {
                panel2.Width = panel2.Width + 15;

              

            }
            else
            {
                Form2 f = new Form2();
                f.Show();
                this.Hide();
                timer1.Stop();

            }
            

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}


/*
 * 
                Process proc = Process.Start(od.FileName);  //
                proc.WaitForInputIdle();
 *    while (proc.MainWindowHandle == IntPtr.Zero)
                {
                    Thread.Sleep(100);
                    proc.Refresh();
                }
                SetParent(proc.MainWindowHandle, this.Handle);
*/
