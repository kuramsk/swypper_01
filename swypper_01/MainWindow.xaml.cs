using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using HtmlAgilityPack;



namespace swypper_01
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {        
        private BackgroundWorker bw = new BackgroundWorker();
        public MainWindow()
        {
            InitializeComponent();
            auth();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);

        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            for (int i = 1; (i <= i); i++)
            {
                if ((worker.CancellationPending == true))
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    Dispatcher.BeginInvoke(new Action(delegate()
                    {

                        listBox1.Items.Add((string)" "+i+"ololo");

                    }));
                    

                    System.Threading.Thread.Sleep(2500);
                    worker.ReportProgress((i));
                }
            }
        }

        public Thread newThread;
        public void auth()
        {
            //Form1.ActiveForm.Visible = false;
            string appId = "3908444";
            string scope = "4096";
            string redirUrl = "http://oauth.vk.com/blank.html";
            string displayType = "popup";
            string token = "token";

            string url = "https://oauth.vk.com/authorize?client_id=" + appId + "&scope=" + scope + "&redirect_uri=" + redirUrl + "&display=" + displayType + "&response_type=" + token;

            Form1 f2 = new Form1();

            f2.Show();
            WebBrowser browser = (WebBrowser)f2.Controls["webBrowser1"];
            browser.Navigate(url);
            newThread = new Thread(F2);
            newThread.Start();
            bw.RunWorkerAsync();
        }

        private void F2() {

            while (Settings1.Default.auth == false)
            {



                //https://api.vk.com/method/METHOD_NAME?PARAMETERS&access_token=ACCESS_TOKEN

                string method = "messages.getDialogs.xml";
                string paramss = "out";
                string gid = "0";
                string count = "count";
                string co = "5";
                string token = Settings1.Default.token;
                string resp = GET_http("https://api.vk.com/method/" + method + "?" + paramss + "=" + gid + "&" + count + "=" + co + "&access_token=" + token);

                Dispatcher.BeginInvoke(new Action(delegate()
                {

                    textBox1.Text = resp;

                }));
                //listBox1.Items.Add(resp)
                /*HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(resp);
                HtmlNodeCollection uid = doc.DocumentNode.SelectNodes("//message");
            
                try
                {
                    string[] uids = new string[uid.Count];
                     int i1 = 0;
                    foreach (HtmlNode Data_Node in uid)
                    {
                        uids[i1] = Data_Node.InnerHtml;
                        i1++;

                    } 
                   /* i1 = 0;
                    foreach (HtmlNode Data_Node in date)
                    {
                        dates[i1] = Data_Node.InnerHtml;
                        i1++;

                    }
                    i1 = 0;
                    foreach (HtmlNode Data_Node in body)
                    {
                        bodys[i1] = Data_Node.InnerHtml;
                        i1++;

                    }
                 
                        Dispatcher.BeginInvoke(new Action(delegate()
                        {
                        
                                   foreach (string bodyID in uids)
                                       listBox1.Items.Add(bodyID);
                           
                            

                        })); 

                }
                catch
                {

                    MessageBox.Show("Error");
                }*/

            }
        }

        public string GET_http(string url)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            System.Net.WebRequest reqGET = System.Net.WebRequest.Create(url);
            System.Net.WebResponse resp = reqGET.GetResponse();
            System.IO.Stream stream = resp.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            string html = sr.ReadToEnd();
            return html;
        }

        private void sendButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string method = "messages.send";
            string paramss = "uid";
            string gid = "6373305";
            string token = Settings1.Default.token;
            string resp = GET_http("https://api.vk.com/method/" + method + "?" + paramss + "=" + gid + "&message="+ textBox2.Text +"&access_token=" + token);

            textBox2.Text = "";
        }

        private void label2_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            textBox3.Text = (string)label2.Content;
        }

        private void label1_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            textBox3.Text = (string)label1.Content;
        }


    }
}
