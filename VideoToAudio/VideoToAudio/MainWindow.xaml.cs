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
using System.IO;

namespace _10视频转换
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 主窗体加载啦
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            image1.Source = new BitmapImage(new Uri(@"E:\MyWork\My\10视频转换\bin\Debug\1.jpg", UriKind.RelativeOrAbsolute));
            GetImages();

        }
        /// <summary>
        /// 播放器加载啦
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myMusic_MediaOpened(object sender, RoutedEventArgs e)
        {
            double totalTime = myMusic.NaturalDuration.TimeSpan.TotalSeconds;
            Console.WriteLine(totalTime);
        }
        /// <summary>
        /// 获取文件夹里所有图片名，暂时使用字符串截取，后期加上正则识别；换了一个方法，直接获得文件名。
        /// </summary>
        private void GetImages()
        {

            string imagePath = @"E:\MyWork\My\10视频转换\bin\Debug";
            //string[] files = Directory.GetFiles(imagePath, "*.jpg");
            //List<string> imageNames = new List<string>();
            //foreach (var file in files)
            //{
            //    imageNames.Add(file.Substring(file.Length-10,10));

            //}
            //foreach (var imageName in imageNames)
            //{
            //    Console.WriteLine(imageName);
            //}
            //下面的方法可以直接获取文件名，不用正则或字符串处理。
            DirectoryInfo folder = new DirectoryInfo(imagePath);
            FileInfo[] imageNames = folder.GetFiles("*.jpg");
            List<int> imageTimes = new List<int>();
            //获取秒数
            foreach (FileInfo file in imageNames)
            {
                if (file.Name.Length == 10)
                {
                    int min = Convert.ToInt32(file.Name.Substring(0, 2));
                    int sec = Convert.ToInt32(file.Name.Substring(2, 2));
                    imageTimes.Add(min * 60 + sec);
                }
            }
            //测试，输出每个图片文件名的秒数
            foreach (var imageTime in imageTimes)
            {
                Console.WriteLine(imageTime);
            }

        }



    }
}
