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
using System.Windows.Threading;

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



        /// <summary>
        /// 主窗体加载啦
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            image1.Source = new BitmapImage(new Uri(@"E:\AppServ\www\github\MyWork\trunk\VideoToAudio\VideoToAudio\bin\Debug\1.jpg", UriKind.RelativeOrAbsolute));
            GetImages();
            //大爷的！直接窗体加载就能自动播放！
            myMusic.Play();

        }
        /// <summary>
        /// 定义计时器，WPF居然没有timer这个控件！
        /// </summary>
        DispatcherTimer timer = new DispatcherTimer();
        /// <summary>
        /// 悲剧的手写timer事件！ 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_tick(object sender, EventArgs e)
        {
            double totalTime = myMusic.NaturalDuration.TimeSpan.TotalSeconds;
            if (myMusic.Position.TotalSeconds == totalTime)
            {
                myMusic.Stop();
                //播放完，游标还没跳到开始，按钮就变成“播放”了，阻断进程也不行。
                //System.Threading.Thread.Sleep(2000);
                btnPlayOrPause.Content = "播放";
            }
            else
            {
                //进度条位置等于音乐播放到的位置
                slider1.Value = myMusic.Position.TotalSeconds;
                //显示进度时间，后期要进行计算，符合时间格式
                lbTime.Content = "00:" + Math.Floor(myMusic.Position.TotalSeconds);
            }



        }



        /// <summary>
        /// 播放器加载啦
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myMusic_MediaOpened(object sender, RoutedEventArgs e)
        {
            //myMusic.Source = new Uri(@"E:\AppServ\www\github\MyWork\trunk\VideoToAudio\VideoToAudio\bin\Debug\LoveStory.mp3", UriKind.RelativeOrAbsolute);
            //获取歌曲总时长
            double totalTime = myMusic.NaturalDuration.TimeSpan.TotalSeconds;
            slider1.Maximum = totalTime;
            //下面3行是抄的，大意为：设置计时器时间间隔为1秒，把上面的timer_tick方法添加进timer.Tick事件，计时器启动
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_tick);
            timer.Start();
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
            //获取秒数，存入imageTimes集合
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
        /// <summary>
        /// 进度条改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            myMusic.Position = TimeSpan.FromSeconds(slider1.Value);
            //slider1.Value = myMusic.Position.TotalSeconds;
            //myMusic.Position = slider1.Value;
        }



        /// <summary>
        /// 播放和暂停按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlayOrPause_Click(object sender, RoutedEventArgs e)
        {
            //要将loadedBehavior和unloadedBehavior设为manual手动
            //如果设为开始就自动播放，播放和暂停按钮就不管用了。已解决！
            if (btnPlayOrPause.Content.ToString() == "播放")
            {
                myMusic.Play();
                btnPlayOrPause.Content = "暂停";
            }
            else
            {
                myMusic.Pause();
                btnPlayOrPause.Content = "播放";

            }

        }



        /// <summary>
        /// 音量条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //貌似音量最高也就10了
            myMusic.Volume = slider2.Value;
        }



    }
}
