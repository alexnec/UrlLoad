using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace UrlLoad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        event EventHandler FileLoaded;
        int i = 0;

        public MainWindow()
        {
            InitializeComponent();
            lbURLs.Items.Add(@"http://www.cs.wustl.edu/~schmidt/patterns-ace.html");
            lbURLs.Items.Add(@"http://msdn.microsoft.com/en-us/library/dd537609%28v=vs.110%29.aspx");
            lbURLs.Items.Add(@"http://professorweb.ru/forum/21-async-await-c-5-0/p1#p47");
            lbURLs.Items.Add(@"http://www.yandex.ru/");
            lbURLs.Items.Add(@"http://www.nalog.ru/");
            lbURLs.Items.Add(@"http://mvccontrib.codeplex.com/");
            lbURLs.Items.Add(@"http://inosmi.ru/");
            lbURLs.Items.Add(@"http://altapress.ru/");
            lbURLs.Items.Add(@"http://news.mail.ru/");
            lbURLs.Items.Add(@"http://mail.ru/");

            FileLoaded += (s, e) =>
            {
                Thread.Sleep(1000);
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.lblCountFiles.Content = i.ToString();
                    //Interlocked.Exchange(ref lblCountFiles.Content, 83); 
                }));
            };
        }

        private void btnGetRes_Click(object sender, RoutedEventArgs e)
        {
            this.lblCountFiles.Content = "Загружаем...";
                Task.Factory.StartNew(() =>
                {
                    LoadResourses();
                });
        }

        void LoadResourses()
        {
            /*
            Parallel.ForEach(lbURLs.Items.OfType<string>(), urlResources =>
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(urlResources, AppDomain.CurrentDomain.BaseDirectory + Interlocked.Increment(ref i)  + ".html");
                    if (FileLoaded != null)
                        FileLoaded(null, null);
                }
            });*/

            foreach(string urlResources in lbURLs.Items)
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(urlResources, AppDomain.CurrentDomain.BaseDirectory + Interlocked.Increment(ref i) + ".html");
                    Thread.Sleep(1000);
                    if (FileLoaded != null)
                        FileLoaded(null, null);
                }
            }
        }
    }
}
