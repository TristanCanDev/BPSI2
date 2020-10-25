
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


namespace BPSI2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public bool isRunning;
        public MainWindow()
        {
            InitializeComponent();
            StartUp();
        }

        private void MediaElement_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {

        }

        public void StartUp()
        {
            isRunning = true;
            while (isRunning == true)
            {
                CheckForInternetConnection();
            }
        }

        public bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    CoolKidGrid.Visibility = Visibility.Visible;
                    return true;
                
            }
            catch
            {
                CoolKidGrid.Visibility = Visibility.Hidden;
                return false;
            }
        }

        
    }
}
