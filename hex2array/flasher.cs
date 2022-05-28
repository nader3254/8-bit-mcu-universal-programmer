using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace hex2array
{
    /* 
     *        //             S U M M A R Y             //
     * i will design a finite state machine of 4 states
     * 1) request programming session 
     * 2) request download 
     * 3) request send data
     * 4) crc checking 
     *
     *  this fsm is designed accordin to UDS specifications ISO14229
     *  negative response =0x7f 
     *  positive response = codevalue + 0x40
     */






    public partial class flasher : Form
    {
        SerialPort ss;
        string[] code, code2, rowcode;
        byte[] codevalues; int[] checksum;
        bool StartOnce = true;
        bool colorChanger = true;
        enum state
        {
            donothing,ProgrammingSession,RequestDownloading,RequestSendingData,CRCProcess
        }
        enum responce
        {
            positive, negative,unevaluated
        }
        enum color
        {
           red, green, yellow , aqua
        }
        state     fsm =state.donothing;
        responce CurentResponse = responce.unevaluated;


        public flasher(string x)
        {
            InitializeComponent();
            try
            {
                 code = x.Split('}');
                 code2 = code[0].Split('{');
                 rowcode = code2[1].Split(',');
                for(int i=0;i<rowcode.Length;i++)
                {



                    rowcode[i] = rowcode[i].TrimStart('\r','\n',' ');
                    rowcode[i] = rowcode[i].TrimEnd('\r','\n', ' ');

                }
                codevalues=new byte[rowcode.Length];
                for (int i=0;i<rowcode.Length;i++)
                {
                    codevalues[i] = Convert.ToByte(Convert.ToInt32(rowcode[i], 16));

                }
                
              

            }
            catch
            {
                MessageBox.Show("invalid code ");
            }

          

        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void flasher_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            foreach(string port in ports)
            {
                comboBox1.Items.Add(port);
            }
            
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {

        }

        private void button2_MouseLeave(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            try
            {
                ss = new SerialPort(comboBox1.Text, Convert.ToInt32(comboBox2.Text));
                ss.Open();
                Thread masterthread;
                masterthread = new Thread(mainfunction);
                masterthread.Start();
                fsm = state.ProgrammingSession;
            }
            catch
            {
                MessageBox.Show("serial port is already oppened ...");
            }



        }
        void mainfunction()
        {
            while (true)
            {
                //DisplayStatus("hello this is a flash program .....",color.aqua);
                //Thread.Sleep(500);
                //DisplayStatus("hello this is a flash program .....", color.red);
                //Thread.Sleep(500);
                //DisplayStatus("hello this is a flash program .....", color.green);
                //Thread.Sleep(500);
                //DisplayStatus("hello this is a flash program .....", color.yellow);
                //Thread.Sleep(500);
                /*                                       F S M                                           */
                /* ProgrammingSession -> RequestDownloading -> RequestSendingData -> CRCProcess         */
                switch (fsm)
                {
                    case state.ProgrammingSession:
                        // uds programming session code 0x10 = 16
                        SendCodeFor(state.ProgrammingSession);
                        DisplayStatus("waiting for responce from MCU", color.aqua);
                        WaitForResponse(16);   //program will wait here until mcu response

                        switch(CurentResponse)
                        {
                            case responce.negative:

                                StartNegativeAction(fsm);

                            break;
                            case responce.positive:

                                StartPositiveAction(fsm);
                                panel7.Width = 70;

                            break;
                            default:
                               if(StartOnce)
                                {
                                   
                                    
                                }
                               else
                                {

                                }

                           
                            break;
                        }
                        

                        break;
/**********************************************************************************/
                    case state.RequestDownloading:
                        // uds download request code 0x34+address+datasize = 52
                        SendCodeFor(state.RequestDownloading);
                       // DisplayStatus("waiting for responce from MCU", color.aqua);
                        WaitForResponse(52);   //program will wait here until mcu response

                        switch (CurentResponse)
                        {
                            case responce.negative:

                                StartNegativeAction(fsm);

                                break;
                            case responce.positive:

                                StartPositiveAction(fsm);
                                panel7.Width += 120;

                                break;
                            default:
                                if (StartOnce)
                                {
                                    /* display waiting here */
                                    StartOnce = false;
                                }
                                else
                                {

                                }


                                break;
                        }
                        break;
/**********************************************************************************/
                    case state.RequestSendingData:
                        // uds request send code 0x36 = 54
                        SendCodeFor(state.RequestSendingData);
                       // DisplayStatus("waiting for responce from MCU", color.aqua);
                        WaitForResponse(54);   //program will wait here until mcu response

                        switch (CurentResponse)
                        {
                            case responce.negative:

                                StartNegativeAction(fsm);

                                break;
                            case responce.positive:

                                StartPositiveAction(fsm);
                                panel7.Width += 150;

                                break;
                            default:
                                if (StartOnce)
                                {
                                    /* display waiting here */
                                    StartOnce = false;
                                }
                                else
                                {

                                }


                                break;
                        }
                        break;
/**********************************************************************************/
                    case state.CRCProcess:
                        // uds programming session code 0x10 = 16
                         SendCodeFor(state.CRCProcess);
                        //WaitForResponse(16);   //program will wait here until mcu response
                        crcCheck();
                        switch (CurentResponse)
                        {
                            case responce.negative:

                                StartNegativeAction(fsm);

                                break;
                            case responce.positive:

                                StartPositiveAction(fsm);

                                break;
                            default:
                                if (StartOnce)
                                {
                                    /* display waiting here */
                                    StartOnce = false;
                                }
                                else
                                {

                                }


                                break;
                        }
                        break;
/**********************************************************************************/
                    default:
                        break;


                }


                

            }
        }
        void crcCheck()
        {
            //0x31
            int ctr = 0;
            checksum = new int[codevalues.Length / 16];
            for(int i=0;i<codevalues.Length;i++)
            {
                if(i%16==0&&i!=0)
                {
                    int tmp = 0;
                    for(int j=i-16;j<i;j++)
                    {
                        tmp += codevalues[j];
                    }
                    checksum[ctr] = tmp;
                    while(checksum[ctr]<255)
                    {
                        checksum[ctr] = checksum[ctr] / 8;
                    }

                    // MessageBox.Show(Convert.ToString(checksum[ctr]));
                    DisplayStatus("crc check : recieved byte ["+i+"] :"+Convert.ToString(checksum[ctr]), color.aqua);
                    Thread.Sleep(500);
                    ctr++;

                }
            }
            this.panel7.Width =460;
        }
        void DisplayStatus(string msg,color clr)
        {
            try
            {
                switch(clr)
                {
                    case color.red :

                        RichTextBox.CheckForIllegalCrossThreadCalls = false;
                        richTextBox1.ForeColor = ColorTranslator.FromHtml("#B30B00");
                        richTextBox1.Text += "\n";
                        richTextBox1.Text += msg;

                        break;
                    case color.yellow:

                        RichTextBox.CheckForIllegalCrossThreadCalls = false;
                        richTextBox1.ForeColor = ColorTranslator.FromHtml("#FFFF00");
                        richTextBox1.Text += "\n";
                        richTextBox1.Text += msg;

                        break;
                    case color.green:

                        RichTextBox.CheckForIllegalCrossThreadCalls = false;
                        richTextBox1.ForeColor = ColorTranslator.FromHtml("#04FF00");
                        richTextBox1.Text += "\n";
                        richTextBox1.Text += msg;

                        break;
                    default:

                        RichTextBox.CheckForIllegalCrossThreadCalls = false;
                        richTextBox1.ForeColor = ColorTranslator.FromHtml("#04FFFF");
                        richTextBox1.Text += "\n";
                        richTextBox1.Text += msg;

                        break;

                }
               
                    
              
            
            }
            catch
            {

            }
        }

        void StartNegativeAction(state currentstate)
        {
            switch(currentstate)
            {
                case state.ProgrammingSession:
                    DisplayStatus("error : programming session  -> mcu refsed to start flashing ..... ", color.red);
                    fsm = state.donothing;
                    break;
                case state.RequestDownloading:
                    DisplayStatus("error : Downloading request refused by mcu..... ", color.red);
                    fsm = state.donothing;
                    break;
                case state.RequestSendingData:
                    DisplayStatus("error :  send data request refused by mcu..... ", color.red);
                    fsm = state.donothing;
                    break;
                case state.CRCProcess:
                    DisplayStatus("error : crc check , an error occured when transfering data ", color.red);
                    fsm = state.donothing;
                    break;
                default:
                    break;

            }
           

        }
        void StartPositiveAction(state currentstate)
        {
            switch (currentstate)
            {
                case state.ProgrammingSession:
                    DisplayStatus(" programming session -> mcu positive responce..... ", color.yellow);
                    fsm = state.RequestDownloading;
                    break;
                case state.RequestDownloading:
                    DisplayStatus( "Downloading request -> mcu positive responce..... ", color.yellow);
                    fsm = state.RequestSendingData;
                    break;
                case state.RequestSendingData:
                    DisplayStatus(" send data request -> mcu positive responce..... ", color.yellow);
                    fsm = state.CRCProcess;
                    break;
                case state.CRCProcess:
                    DisplayStatus(" crc check : mcu recieved data successfully...... ", color.green);
                    timer1.Stop();
                    this.button1.BackColor = ColorTranslator.FromHtml("#0F6FAD");
                    fsm = state.donothing;
                    break;
                default:
                    break;

            }

        }
        void SendCodeFor(state currentstate)
        {
            switch(currentstate)
            {
                case state.ProgrammingSession:

                    byte[] a = { 16 };
                    send(a);
                    while (ss.BytesToWrite != 0) ;     //wait for sending all data

                    break;
                case state.RequestDownloading:
                    byte low, high;
                   Int64 datasize = codevalues.Length;
                    low  =(Byte)datasize ;
                   // int ds = 8>>datasize
                    high = (Byte)(datasize >> 8);
                    byte high2 = (Byte)(datasize >> 16);
                    byte high3 = (Byte)(datasize >> 24);
                    byte[] b = { 52, low, high ,high2,high3};
                    send(b);
                    while (ss.BytesToWrite != 0) ;     //wait for sending all data

                    break;

                case state.RequestSendingData:

                    byte[] tmp =new byte[codevalues.Length+1];
                    tmp[0] = 54;
                    for(int i=0;i<codevalues.Length;i++)
                    {
                        tmp[i + 1] = codevalues[i];
                    }
                    send(tmp);
                    while (ss.BytesToWrite != 0) ;     //wait for sending all data


                    break;
                case state.CRCProcess:

                    byte[] d = { 49 };
                    send(d);
                  //  while (ss.BytesToWrite != 0) ;     //wait for sending all data


                    break;
                default:
                    break;
            }
        }
        //x
        void WaitForResponse(int codev)
        {
            while(true)
            {       //y
                int tmp = recieve();

                if (tmp !=0)
                {
                    bool end = false;
                    EvaluateResponse(codev, tmp);
                    switch(CurentResponse)
                    {
                        case responce.negative:
                            end = true;
                        break;
                        case responce.positive:
                            end = true;
                            break;
                        default:

                        break;
                    }
                    if(end)
                    {
                        break;
                    }

                }
            }
        }

        void EvaluateResponse( int x, int y  )
        {
            int n = 127;

            int p = x + 64;

           if(y==p)
           {
                CurentResponse = responce.positive;

           }
            else
            {
                if(y==n)
                {
                    CurentResponse = responce.negative;
                }
                else
                {
                    CurentResponse = responce.unevaluated;

                }

            }

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            // scroll it automatically
            richTextBox1.ScrollToCaret();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string x = "#27C106";
            string y = "#EE3233";
            if(colorChanger)
            {
                this.button1.BackColor = ColorTranslator.FromHtml(x);
                colorChanger = false;
            }
            else
            {
                this.button1.BackColor = ColorTranslator.FromHtml(y);
                colorChanger = true;
            }
        }

        int recieve()
        {
            try
            {

                if (ss.IsOpen == true)
                {

                 return  ss.ReadByte();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to open");
                return 0;
            }
            return 0;
        }
        void send(byte[] x)
        {
            try
            {
                
                if (ss.IsOpen == true)
                {

                    ss.Write(x, 0, x.Length);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to open");
            }
        }
    }
}
