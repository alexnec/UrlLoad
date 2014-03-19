using System;
using System.Collections.Generic;
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

namespace UrlLoad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            lbURLs.Items.Add(@"http://www.cs.wustl.edu/~schmidt/patterns-ace.html");
            lbURLs.Items.Add(@"http://msdn.microsoft.com/en-us/library/dd537609%28v=vs.110%29.aspx");
            lbURLs.Items.Add(@"http://professorweb.ru/forum/21-async-await-c-5-0/p1#p47");
            lbURLs.Items.Add(@"http://www.yandex.ru/");
            lbURLs.Items.Add(@"http://www.nalog.ru/");
            lbURLs.Items.Add(@"http://mvccontrib.codeplex.com/");
        }

        private void btnGetRes_Click(object sender, RoutedEventArgs e)
        {
                Task.Factory.StartNew(() =>
                {
                    LoadResourses();
                });
        }

        void LoadResourses()
        {
            int i = 0;
            Parallel.ForEach(lbURLs.Items.OfType<string>(), urlResources =>
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(urlResources, AppDomain.CurrentDomain.BaseDirectory + i++ + ".html");
                    //this.lblCountFiles.Content = Thread.CurrentThread.ManagedThreadId.ToString();
                }
            });
        }
    }
}
