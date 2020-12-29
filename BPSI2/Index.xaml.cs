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
    /// Interaction logic for Index.xaml
    /// </summary>
    public partial class Index : Page
    {
        public Index()
        {
            InitializeComponent();
        }

        private void PavButton_Click(object sender, RoutedEventArgs e)
        {
            Uri page = new Uri("PavlovPage.xaml", UriKind.Relative);
            NavigationService.Navigate(page);
        }

        private void DevInterface_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GorButton_Click(object sender, RoutedEventArgs e)
        {
            Uri page = new Uri("GorillaPage.xaml", UriKind.Relative);
            NavigationService.Navigate(page);
        }
    }
}
