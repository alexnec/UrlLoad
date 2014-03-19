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
        delegate void FileLoadedType(string s);
        FileLoadedType FileLoaded;
        //Action FileLoaded;
        int numberLoadedFile = 0;
        int countFiles = 0;
        object lockOn = new Object();

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
            
            FileLoaded += (string s) =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    lock (lockOn)
                    {
                        countFiles++;
                        this.lblCountFiles.Content = countFiles.ToString();
                        lbOrder.Items.Add(countFiles.ToString() + " -- " + s);
                        if (lbURLs.Items.Count == countFiles)
                          this.lblStatusLoad.Content = "Завершено";
                    }
                }));
            };
        }

        private void btnGetRes_Click(object sender, RoutedEventArgs e)
        {
            numberLoadedFile = 0;
            countFiles = 0;
            this.lblCountFiles.Content = "0";
            this.lblStatusLoad.Content = "Загрузка...";
            LoadResourses();   
        }

        void LoadResourses()
        {
            foreach(string urlResources in lbURLs.Items)
            {
                using (WebClient webClient = new WebClient())
                {
                    ThreadPool.QueueUserWorkItem((o) => {
                        webClient.DownloadFile(urlResources, AppDomain.CurrentDomain.BaseDirectory 
                            + Interlocked.Increment(ref numberLoadedFile) + ".html");
                        
                        if (FileLoaded != null)
                            FileLoaded(urlResources);
                    }); 
                }
            }
        }
    }
}
