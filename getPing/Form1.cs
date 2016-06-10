using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;


namespace getPing
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Threading.Timer timer = new System.Threading.Timer(ThreadFunc, null, 0, 10000*5);

        }

        private static void ThreadFunc(object state)
        {
            String[] ips = { "192.168.1.1", "192.168.1.61", "78.38.255.100 ", "10.10.53.209", "8.8.8.8", "4.2.2.4" };
            bool[] rtrn = new bool[6];
            //Do work in here.
            for (int i = 0; i < 6; i++)
            {
                rtrn[i] = PingHost(ips[i]);
                if (rtrn[i] == false)
                {
                    using (StreamWriter w = File.AppendText("getping.txt"))
                    {
                        w.WriteLine(DateTime.Now.ToString("MM/dd/yy H:mm:ss zzz") + "::" + ips[i] + "::" + rtrn[i]);
                    }
                }
                using (StreamWriter w = File.AppendText("getpingAll.txt"))
                {
                    w.WriteLine(DateTime.Now.ToString("MM/dd/yy H:mm:ss zzz") + "::" + ips[i] + "::" + rtrn[i]);
                }

            }
           


        }

        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            return pingable;
        }
    }
}
