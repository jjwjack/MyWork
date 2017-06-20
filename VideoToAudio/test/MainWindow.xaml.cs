using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            myVideo.Play();
        }

        private void myVideo_MediaOpened(object sender, RoutedEventArgs e)
        {
            //E:\AppServ\www\github\MyWork\trunk\VideoToAudio\VideoToAudio\bin\Debug\LoveStory.mp4
            myVideo.Source = new Uri(@"E:\AppServ\www\github\MyWork\trunk\VideoToAudio\VideoToAudio\bin\Debug\LoveStory.mp4",UriKind.Absolute);
            myVideo.auto

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            myVideo.Play();
        }
    }
}
