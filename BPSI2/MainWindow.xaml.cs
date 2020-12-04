using System.Runtime;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using System.Net;
using System.Timers;
using System.Windows.Navigation;
using System.IO;
using System.IO.Packaging;
using System.IO.Compression;

namespace BPSI2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
            if(CoolKidClass.checkforinternetconnection()== false)
            {
                //Offlinething.Visibility = Visibility.Visible;
            }
            else
            {
                //Offlinething.Visibility = Visibility.Hidden;
            }

            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"\\Blu\\");
            }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\GameFiles\\"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\GameFiles\\");
            }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\initialized.blu"))
            {
                adbInstall();
            }
            ADBcommands.adblocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\adb\\latestadb\\platform-tools\\adb.exe";
            foreach(var yes in System.Diagnostics.Process.GetProcessesByName("adb"))
            {
                yes.Kill();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            coolFrame.Navigate(new PavlovPage());

        }

        
        void adbInstall()
        {
            File.Create(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"\\Blu\\initialized.blu");
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\adb\\"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\adb\\");
            }
            
            WebClient adbdown = new WebClient();
            Uri adburl = new Uri("https://dl.google.com/android/repository/platform-tools-latest-windows.zip");

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\adb\\latestadb.zip"))
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\adb\\latestadb.zip");
            }
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\adb\\latestadb"))
            {
                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\adb\\latestadb", true);
            }
            adbdown.DownloadFile(adburl, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\adb\\latestadb.zip");
            ZipFile.ExtractToDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\adb\\latestadb.zip", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\adb\\latestadb");

            ADBcommands.adblocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blu\\adb\\latestadb\\platform-tools\\adb.exe";
        }

        
    }
}
