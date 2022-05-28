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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_MouseEnter(object sender, EventArgs e)
        {
         

        }

        private void Form2_MouseLeave(object sender, EventArgs e)
        {
           
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }
         string log;
         bool first=true;
        string code;
        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog od = new OpenFileDialog();      //
            if (od.ShowDialog() == DialogResult.OK)        //
            {                                              //   
                string stemp = od.FileName; // 
                char x = '\\';

                string[] ss = stemp.Split(x);
                stemp = "";
                for (int i = 0; i < ss.Length; i++)
                {
                    if (i != ss.Length - 1)
                    {
                        stemp += ss[i];
                        stemp += "\\\\";
                    }
                    else
                    {
                        stemp += ss[i];


                    }
                }


                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter("path.conf");
                //Write of text
                // if we used writeline() there will be some errors
                sw.Write(stemp);
                //Close the file
                sw.Close();
                Process proc = Process.Start("bin.exe");


                StreamReader sw2 = new StreamReader("history.log");
                //Write of text
                // if we used writeline() there will be some errors
                log = sw2.ReadToEnd();
                //Close the file
                sw2.Close();


                StreamReader sw3 = new StreamReader("outputfile.text");
                //Write of text
                // if we used writeline() there will be some errors
                code = sw3.ReadToEnd();
                //Close the file
                sw3.Close();

               
                status st = new status(log,code);
                st.Show();



            }
        }

        private void timer1_Tick(object sender, EventArgs e)
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
    }
}
