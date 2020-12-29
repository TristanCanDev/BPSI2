using System;
using System.Collections.Generic;
using System.Linq;
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
using System.IO;
using System.Net;
using System.IO.Compression;
using System.Diagnostics;
using System.IO.Packaging;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;


namespace BPSI2
{
    /// <summary>
    /// Interaction logic for PavlovPage.xaml
    /// </summary>
    public partial class PavlovPage : Page
    {
        public PavlovPage()
        {
            InitializeComponent();
        }

        public string CurrentApp;
        public string CurrentURL;
        public string APKname;
        public string comOBJ;
        public string OBBname;
        public void ReadUPSItxt(string appname)
        {
            

            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            WebClient upsitxt = new WebClient();
            Uri txturl = new Uri("https://thesideloader.co.uk/upsiopts.txt");
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\Resources\\upsiopts.txt"))
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\Resources\\upsiopts.txt");
            }
            upsitxt.DownloadFile(txturl, path + "\\Resources\\upsiopts.txt");

            string[] txtlines = File.ReadAllLines(path + "\\Resources\\upsiopts.txt");

            foreach (string line in txtlines)
            {
                if (line.Contains("DOWNLOADFROM="))
                {
                    if (line.Contains(appname))
                    {
                        CurrentURL = line.Substring(line.IndexOf("DOWNLOADFROM=")).Replace("DOWNLOADFROM=", "");
                    }
                }
                if (line.StartsWith("NAME="))
                {
                    if (line.Contains(appname))
                    {
                        CurrentApp = line.Substring(line.IndexOf("NAME=")).Replace("NAME=", "");
                    }
                }
                if (line.StartsWith("OBB="))
                {
                    if (line.Contains(appname))
                    {
                        OBBname = line.Substring(line.IndexOf("OBB=")).Replace("OBB=", "");
                    }
                }
                if (line.StartsWith("APK="))
                {
                    if (line.Contains(appname))
                    {
                        APKname = line.Substring(line.IndexOf("APK=")).Replace("APK=", "");
                    }
                }
                if (line.StartsWith("COMOBJECT="))
                {
                    if (line.Contains(appname))
                    {
                        comOBJ = line.Substring(line.IndexOf("COMOBJECT=")).Replace("COMOBJECT=", "");
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
                Uri appdown = new Uri(url);
                String filename = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\GameFiles\\" + appname + ".zip";

                statusblock.Text = "Downloading";
                mainbutton.IsEnabled = false;
                progressbaryes.Visibility = Visibility.Visible;
                p.DownloadFileCompleted += new AsyncCompletedEventHandler(extract);
                p.DownloadProgressChanged += new DownloadProgressChangedEventHandler(progress);
                p.DownloadFileAsync(appdown, filename);


                void progress(object sender, DownloadProgressChangedEventArgs a)
                {
                    progressbaryes.Value = a.ProgressPercentage;
                }

                

                async void extract(object sender, AsyncCompletedEventArgs f)
                {
                    statusblock.Text = "Download Complete! Unzipping the file!";
                    await Task.Run(() => ZipFile.ExtractToDirectory(filename, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\GameFiles\\" + appname));
                    statusblock.Text = "File Unzipped!";
                    mainbutton.IsEnabled = true;
                    progressbaryes.Visibility = Visibility.Hidden;
                    if (appname.Contains("Pavlov"))
                    {
                        
                        Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\GameFiles\\" +appname+"\\platform-tools", true);
                        File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\GameFiles\\"+appname+"\\ReadMe.txt");

                    }

                }

            }
            else
            {
                statusblock.Text = "GameFiles Already Exist. Pushing.";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string pavName = username.Text;
            if (pavName == "Username")
            {
                statusblock.Text = "Please set your username";
                return;
            }

            ReadUPSItxt("Pavlov");
            GrabAppFiles(CurrentURL, CurrentApp);
            PushGame(pavName);
        }

        private void pushmap_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();

            System.Windows.Forms.DialogResult result = openFileDlg.ShowDialog();

            if(result == System.Windows.Forms.DialogResult.OK)
            {
                string mapDir = openFileDlg.SelectedPath;
                string mapName = System.IO.Path.GetDirectoryName(mapDir);
                statusblock.Text = mapName;
                mapName = mapDir.Replace(mapName, "");
                statusblock.Text = mapName;
                mapName = mapName.Replace("\\", "");
                statusblock.Text = mapName;


                ADBcommands adb = new ADBcommands();
                adb.PushMap(mapDir, mapName, statusblock);
                adb.adbKill();
            }
        }
        void PushGame(string uName)
        {

            string fldrPATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"\\Blu\\GameFiles\\"+CurrentApp;
            ADBcommands adb = new ADBcommands();
            adb.uninstallGame(statusblock, comOBJ);
            adb.setperms(statusblock, OBBname);
            adb.pushOBB(statusblock, fldrPATH, CurrentApp, OBBname, comOBJ);
            adb.pushAPK(statusblock, fldrPATH, CurrentApp, APKname);
            adb.pavSetName(uName, statusblock);
            
        }

        void SetNameYes(string uName, string appname)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack == true)
            {
                NavigationService.GoBack();
            }
            else
            {
                return;
            }
        }
    }
}