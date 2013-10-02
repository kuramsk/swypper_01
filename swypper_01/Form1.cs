using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Threading;

namespace swypper_01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //http://REDIRECT_URI#access_token=533bacf01e11f55b536a565b57531ad114461ae8736d6506a3&expires_in=86400&user_id=8492 
            //swypper_01.Form1.ActiveForm.Opacity = 1;
            try
            {
                string url = webBrowser1.Url.ToString();
                string l = url.Split('#')[1];
                if (l[0] == 'a')
                {

                    //program
                    string token = l.Split('&')[0].Split('=')[1];
                    string id = l.Split('=')[3];
                    //MessageBox.Show(token + "  " + id);
                    Settings1.Default.token = token;
                    Settings1.Default.id = id;
                    Settings1.Default.auth = true;
                    //this.Close();
                    
                }
                Close();
                webBrowser1.Dispose();
            }
            catch {

                
                //Form1.ActiveForm.
            //Form1.ActiveForm.Enabled = true;
                //Form1.ActiveForm.Enabled = true;
            }
            
        }

        

    }
}
