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
        //先定义一个字典，存储图片名字和时间
        private Dictionary<string, int> imageAndTime = new Dictionary<string, int>();
        //定义一个泛型存放时间，用于拖动进度条时前后比较
        List<int> imageTimes = new List<int>();
        private string imagePath = @"E:\AppServ\www\github\MyWork\trunk\VideoToAudio\VideoToAudio\bin\Debug\";

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
            //获取目录下所有图片名字列表并计算时间，存入字典
            GetImages();
            //显示图片
            //存储所有时间
            foreach (var item in imageAndTime)
            {
                imageTimes.Add(item.Value);
            }
            //测试时间存进来没有
            //foreach (var item in imageTimes)
            //{
            //    Console.WriteLine(item);
            //}
            //string imageName = null;
            //string imagePath = @"E:\AppServ\www\github\MyWork\trunk\VideoToAudio\VideoToAudio\bin\Debug\" + imageName;
            image1.Source = new BitmapImage(new Uri(@"E:\AppServ\www\github\MyWork\trunk\VideoToAudio\VideoToAudio\bin\Debug\1.jpg", UriKind.RelativeOrAbsolute));

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
            //游标跟随播放时间移动
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

            //图片跟随播放时间变换
            foreach (var item in imageAndTime)
            {
                if (item.Value == Math.Floor(slider1.Value))
                {
                    image1.Source = new BitmapImage(new Uri(imagePath + item.Key, UriKind.RelativeOrAbsolute));
                }
                

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
        /// 获取文件夹里所有图片名，暂时使用字符串截取，后期加上正则识别。
        /// 换了一个方法，直接获得文件名。
        /// 使用字典存储图片名和时间，返回这个字典。
        /// </summary>
        private void GetImages()
        {

            //string imagePath = @"E:\MyWork\My\10视频转换\bin\Debug";
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
            //获取秒数，存入imageTimes集合。后来发现不好，采用了字典。
            //List<int> imageTimes = new List<int>();
            //foreach (FileInfo file in imageNames)
            //{
            //    if (file.Name.Length == 10)
            //    {
            //        int min = Convert.ToInt32(file.Name.Substring(0, 2));
            //        int sec = Convert.ToInt32(file.Name.Substring(2, 2));
            //        imageTimes.Add(min * 60 + sec);
            //    }
            //}

            //测试，输出每个图片文件名的秒数
            //foreach (var imageTime in imageTimes)
            //{
            //    Console.WriteLine(imageTime);
            //}
            //需要把图片名和对应时间作为键值对存入json，例如{"001580.jpg":15, "010425":64}
            //算了用字典吧！
            //Dictionary<string, int> imageAndTime = new Dictionary<string, int>();
            foreach (var imageName in imageNames)
            {
                //筛选一下，10个字符的才是图片名字的正确格式
                if (imageName.Name.Length == 10)
                {
                    int min = Convert.ToInt32(imageName.Name.Substring(0, 2));
                    int sec = Convert.ToInt32(imageName.Name.Substring(2, 2));
                    int imageTime = min * 60 + sec;
                    imageAndTime.Add(imageName.Name, imageTime);
                }
            }
            //return imageAndTime;
            //输出字典测试一下
            //foreach (var item in imageAndTime)
            //{
            //    Console.WriteLine(item.Key);
            //    Console.WriteLine(item.Value);
            //}

        }



        /// <summary>
        /// 拖动进度条，改变播放时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            myMusic.Position = TimeSpan.FromSeconds(slider1.Value);
            //拖动进度条的时候，判断该显示哪张图片，两个时间之间{"001580.jpg":15,"001974.jpg":19}
            
            
            foreach (var item in imageAndTime)
            {
                if (item.Value == Math.Floor(slider1.Value))
                {
                    image1.Source = new BitmapImage(new Uri(imagePath + item.Key, UriKind.RelativeOrAbsolute));
                }
                //此处有问题，提示索引不能为负或超出索引。slider_valuechange事件，是不是自己动也算？不仅仅是拖动？
                else
                {
                    for (int i = 0; i < imageTimes.Count; i++)
                    {
                        if (Math.Floor(slider1.Value) < imageTimes[i])
                        {
                            MessageBox.Show(imageTimes[i - 1].ToString());
                        }
                    }
                }
                
            }
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
