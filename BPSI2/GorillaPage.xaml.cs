using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BPSI2
{
    /// <summary>
    /// Interaction logic for GorillaPage.xaml
    /// </summary>
    public partial class GorillaPage : Page
    {
        string CurrentApp;
        string APK;
        string CurrentURL;
        public GorillaPage()
        {
            InitializeComponent();
        }

        public void GetBPSIopts(string appname)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu";
            if (File.Exists(path + "\\Resources\\bpsiopts.txt"))
            {
                File.Delete(path + "\\Resources\\bpsiopts.txt");
            }
            WebClient upsitxt = new WebClient();
            Uri txturl = new Uri("https://pastebin.com/raw/Ar2cKLHE");
            upsitxt.DownloadFile(txturl, path + "\\Resources\\bpsiopts.txt");

            string[] txtlines = File.ReadAllLines(path + "\\Resources\\bpsiopts.txt");

            foreach (string line in txtlines)
            {
                if (line.Contains(appname))
                {
                    if (line.Contains("NAME="))
                    {
                        CurrentApp = line.Substring(line.IndexOf("NAME=")).Replace("NAME=", "");
                    }
                }
                if (line.Contains(appname))
                {
                    if (line.Contains("DOWNLOADFROM="))
                    {
                        CurrentURL = line.Substring(line.IndexOf("DOWNLOADFROM=")).Replace("DOWNLOADFROM=", "");
                    }
                }
                if (line.Contains(appname))
                {
                    if (line.Contains("APK="))
                    {
                        APK = line.Substring(line.IndexOf("APK=")).Replace("APK=", "");
                    }
                }
            }
        }

        public void GrabAppFiles(string url, string appname)
        {
            WebClient p = new WebClient();

            //check to see if the app already has a directory (thank you ModernEra for a good bit of this code!!
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\GameFiles\\" + appname))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\GameFiles\\" + appname);
                Uri appdown = new Uri(url);
                String filename = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\GameFiles\\"+appname+"\\"+APK;

                statusblock.Text = "Downloading";
                mainbutton.IsEnabled = false;
                progressbaryes.Visibility = Visibility.Visible;
                p.DownloadFileCompleted += new AsyncCompletedEventHandler(good);
                p.DownloadProgressChanged += new DownloadProgressChangedEventHandler(progress);
                p.DownloadFileAsync(appdown, filename);


                void progress(object sender, DownloadProgressChangedEventArgs a)
                {
                    progressbaryes.Value = a.ProgressPercentage;
                }

                void good(object sender, AsyncCompletedEventArgs e)
                {
                    statusblock.Text = "Download Complete. Installing.";
                }
                
                

            }
            else
            {
                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\GameFiles\\" + appname, true);
                GrabAppFiles(CurrentURL, CurrentApp);
            }
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetBPSIopts("PreAlpha1dot6");
            GrabAppFiles(CurrentURL, CurrentApp);
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\GameFiles\\" + CurrentApp;
            ADBcommands adb = new ADBcommands();
            adb.pushAPK(statusblock, path, CurrentApp, APK);

        }
    }
}
