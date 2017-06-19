//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using System.IO;
//using System.Windows.Threading;

//namespace _10视频转换
//{
//    /// <summary>
//    /// MainWindow.xaml 的交互逻辑
//    /// </summary>
//    public partial class MainWindow : Window
//    {
//        //先定义一个字典，存储图片名字和时间
//        private Dictionary<string, int> imageAndTime = new Dictionary<string, int>();
//        //定义一个泛型存放时间，用于拖动进度条时前后比较
//        List<int> imageTimes = new List<int>();
//        private string imagePath = @"E:\AppServ\www\github\MyWork\trunk\VideoToAudio\VideoToAudio\bin\Debug\";
//        //开头视频时长
//        private int videoTotalTime = 0;


//        public MainWindow()
//        {
//            InitializeComponent();
//        }



//        /// <summary>
//        /// 主窗体加载啦
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        private void Window_Loaded(object sender, RoutedEventArgs e)
//        {
//            //获取目录下所有图片名字列表并计算时间，存入字典
//            GetImages();
//            //显示图片
//            //存储所有时间
//            foreach (var item in imageAndTime)
//            {
//                imageTimes.Add(item.Value);
//            }
//            //测试时间存进来没有
//            //foreach (var item in imageTimes)
//            //{
//            //    Console.WriteLine(item);
//            //}
//            //string imageName = null;
//            //string imagePath = @"E:\AppServ\www\github\MyWork\trunk\VideoToAudio\VideoToAudio\bin\Debug\" + imageName;
//            image1.Source = new BitmapImage(new Uri(@"E:\AppServ\www\github\MyWork\trunk\VideoToAudio\VideoToAudio\bin\Debug\1.jpg", UriKind.RelativeOrAbsolute));

//            //大爷的！直接窗体加载就能自动播放！
//            myMusic.Play();
//            //myVideo.Play();
//        }



//        /// <summary>
//        /// 定义计时器，WPF居然没有timer这个控件！
//        /// </summary>
//        DispatcherTimer timer = new DispatcherTimer();
//        /// <summary>
//        /// 悲剧的手写timer事件！ 
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        private void timer_tick(object sender, EventArgs e)
//        {
//            //游标跟随播放时间移动
//            double totalTime = myMusic.NaturalDuration.TimeSpan.TotalSeconds;
//            if (myMusic.Position.TotalSeconds == totalTime)
//            {
//                myMusic.Stop();
//                //播放完，游标还没跳到开始，按钮就变成“播放”了，阻断进程也不行。
//                //System.Threading.Thread.Sleep(2000);
//                btnPlayOrPause.Content = "播放";
//            }
//            else
//            {
//                //进度条位置等于音乐播放到的位置
//                slider1.Value = myMusic.Position.TotalSeconds;
//                //显示进度时间，后期要进行计算，符合时间格式
//                lbTime.Content = "00:" + Math.Floor(myMusic.Position.TotalSeconds);
//            }

//            //图片跟随播放时间变换
//            foreach (var item in imageAndTime)
//            {
//                if (item.Value == Math.Floor(slider1.Value))
//                {
//                    image1.Source = new BitmapImage(new Uri(imagePath + item.Key, UriKind.RelativeOrAbsolute));
//                }


//            }


//        }



//        /// <summary>
//        /// 播放器加载啦
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        private void myMusic_MediaOpened(object sender, RoutedEventArgs e)
//        {
//            //myMusic.Source = new Uri(@"E:\AppServ\www\github\MyWork\trunk\VideoToAudio\VideoToAudio\bin\Debug\LoveStory.mp3", UriKind.RelativeOrAbsolute);
//            //获取歌曲总时长
//            double totalTime = myMusic.NaturalDuration.TimeSpan.TotalSeconds;
//            slider1.Maximum = totalTime;
//            //下面3行是抄的，大意为：设置计时器时间间隔为1秒，把上面的timer_tick方法添加进timer.Tick事件，计时器启动
//            timer.Interval = TimeSpan.FromSeconds(1);
//            timer.Tick += new EventHandler(timer_tick);
//            timer.Start();
//            Console.WriteLine(totalTime);
//        }



//        /// <summary>
//        /// 获取文件夹里所有图片名，暂时使用字符串截取，后期加上正则识别。
//        /// 换了一个方法，直接获得文件名。
//        /// 使用字典存储图片名和时间，返回这个字典。
//        /// </summary>
//        private void GetImages()
//        {

//            //string imagePath = @"E:\MyWork\My\10视频转换\bin\Debug";
//            //string[] files = Directory.GetFiles(imagePath, "*.jpg");
//            //List<string> imageNames = new List<string>();
//            //foreach (var file in files)
//            //{
//            //    imageNames.Add(file.Substring(file.Length-10,10));

//            //}
//            //foreach (var imageName in imageNames)
//            //{
//            //    Console.WriteLine(imageName);
//            //}
//            //下面的方法可以直接获取文件名，不用正则或字符串处理。
//            DirectoryInfo folder = new DirectoryInfo(imagePath);
//            FileInfo[] imageNames = folder.GetFiles("*.jpg");
//            //获取秒数，存入imageTimes集合。后来发现不好，采用了字典。
//            //List<int> imageTimes = new List<int>();
//            //foreach (FileInfo file in imageNames)
//            //{
//            //    if (file.Name.Length == 10)
//            //    {
//            //        int min = Convert.ToInt32(file.Name.Substring(0, 2));
//            //        int sec = Convert.ToInt32(file.Name.Substring(2, 2));
//            //        imageTimes.Add(min * 60 + sec);
//            //    }
//            //}

//            //测试，输出每个图片文件名的秒数
//            //foreach (var imageTime in imageTimes)
//            //{
//            //    Console.WriteLine(imageTime);
//            //}
//            //需要把图片名和对应时间作为键值对存入json，例如{"001580.jpg":15, "010425":64}
//            //算了用字典吧！
//            //Dictionary<string, int> imageAndTime = new Dictionary<string, int>();
//            foreach (var imageName in imageNames)
//            {
//                //筛选一下，10个字符的才是图片名字的正确格式
//                if (imageName.Name.Length == 10)
//                {
//                    int min = Convert.ToInt32(imageName.Name.Substring(0, 2));
//                    int sec = Convert.ToInt32(imageName.Name.Substring(2, 2));
//                    int imageTime = min * 60 + sec;
//                    imageAndTime.Add(imageName.Name, imageTime);
//                }
//            }
//            //return imageAndTime;
//            //输出字典测试一下
//            //foreach (var item in imageAndTime)
//            //{
//            //    Console.WriteLine(item.Key);
//            //    Console.WriteLine(item.Value);
//            //}

//        }



//        /// <summary>
//        /// slider_valuechanged,拖动进度条，改变播放时间，不能采用！！
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
//        {
//            ////不能用这个slider_valuechanged，声音会卡，因为播放的时候本身value值就在变化，不是只有拖动才有变化！！
//            //myMusic.Position = TimeSpan.FromSeconds(slider1.Value);
//            ////拖动进度条的时候，判断该显示哪张图片，两个时间之间{"001580.jpg":15,"001974.jpg":19}


//            //foreach (var item in imageAndTime)
//            //{
//            //    if (item.Value == Math.Floor(slider1.Value))
//            //    {
//            //        image1.Source = new BitmapImage(new Uri(imagePath + item.Key, UriKind.RelativeOrAbsolute));
//            //    }
//            //    //此处有问题，提示索引不能为负或超出索引。slider_valuechange事件，是不是自己动也算？不仅仅是拖动？
//            //    //else
//            //    //{
//            //    //    for (int i = 0; i < imageTimes.Count; i++)
//            //    //    {
//            //    //        if (Math.Floor(slider1.Value) < imageTimes[i])
//            //    //        {
//            //    //            MessageBox.Show(imageTimes[i - 1].ToString());
//            //    //        }
//            //    //    }
//            //    //}

//            //}
//            //slider1.Value = myMusic.Position.TotalSeconds;
//            //myMusic.Position = slider1.Value;
//        }



//        /// <summary>
//        /// 播放和暂停按钮
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        private void btnPlayOrPause_Click(object sender, RoutedEventArgs e)
//        {
//            //要将loadedBehavior和unloadedBehavior设为manual手动
//            //如果设为开始就自动播放，播放和暂停按钮就不管用了。已解决！
//            if (btnPlayOrPause.Content.ToString() == "播放")
//            {
//                myMusic.Play();
//                btnPlayOrPause.Content = "暂停";
//            }
//            else
//            {
//                myMusic.Pause();
//                btnPlayOrPause.Content = "播放";

//            }

//        }
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
        private Dictionary<int, string> imageAndTime = new Dictionary<int, string>();
        //定义一个泛型存放时间，用于拖动进度条时前后比较
        List<int> imageTimes = new List<int>();
        private string imagePath = @"E:\AppServ\www\github\MyWork\trunk\VideoToAudio\VideoToAudio\bin\Debug\img\";
        //private string imagePath = @"F:\MyWork\MyWork\trunk\VideoToAudio\VideoToAudio\bin\Debug\img\";
        //音乐总时间
        private double totalTime;
        //开头视频时长
        private int videoTotalTime = 0;
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
                imageTimes.Add(item.Key);
            }
            //测试时间存进来没有
            //foreach (var item in imageTimes)
            //{
            //    Console.WriteLine(item);
            //}
            //string imageName = null;
            //string imagePath = @"E:\AppServ\www\github\MyWork\trunk\VideoToAudio\VideoToAudio\bin\Debug\" + imageName;
            //image1.Source = new BitmapImage(new Uri(@"E:\AppServ\www\github\MyWork\trunk\VideoToAudio\VideoToAudio\bin\Debug\1.jpg", UriKind.RelativeOrAbsolute));
            image1.Source = new BitmapImage(new Uri(imagePath + imageAndTime.Values.First(), UriKind.RelativeOrAbsolute));

            //大爷的！直接窗体加载就能自动播放！
            //两个不能一起play，好像有个优先级，都不play的话默认是视频。
            myMusic.Play();
            //myVideo.Play();
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
            //totalTime = myMusic.NaturalDuration.TimeSpan.TotalSeconds;
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
            int bingoTime = (int)Math.Floor(slider1.Value);
            if (imageAndTime.ContainsKey(bingoTime))
            {
                image1.Source = new BitmapImage(new Uri(imagePath + imageAndTime[bingoTime], UriKind.RelativeOrAbsolute));
            }
            //foreach (var item in imageAndTime)
            //{
            //if (item.Key == Math.Floor(slider1.Value))
            //    {
            //        image1.Source = new BitmapImage(new Uri(imagePath + item.Value, UriKind.RelativeOrAbsolute));
            //    }
            //}
        }



        /// <summary>
        /// 播放器加载啦
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myMusic_MediaOpened(object sender, RoutedEventArgs e)
        {
            //myMusic.Source = new Uri(@"F:\MyWork\MyWork\trunk\VideoToAudio\VideoToAudio\bin\Debug\LoveStory.mp3", UriKind.RelativeOrAbsolute);
            //获取歌曲总时长
            totalTime = myMusic.NaturalDuration.TimeSpan.TotalSeconds;
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
                    imageAndTime.Add(imageTime, imageName.Name);
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
        /// 拖动进度条，改变播放时间，保留一句代码，拖动时有用。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            myMusic.Position = TimeSpan.FromSeconds(slider1.Value);
            ////拖动进度条的时候，判断该显示哪张图片，两个时间之间{"001580.jpg":15,"001974.jpg":19}


            //foreach (var item in imageAndTime)
            //{
            //    if (item.Value == Math.Floor(slider1.Value))
            //    {
            //        image1.Source = new BitmapImage(new Uri(imagePath + item.Key, UriKind.RelativeOrAbsolute));
            //    }
            //    //此处有问题，提示索引不能为负或超出索引。slider_valuechange事件，是不是自己动也算？不仅仅是拖动？
            //    //else
            //    //{
            //    //    for (int i = 0; i < imageTimes.Count; i++)
            //    //    {
            //    //        if (Math.Floor(slider1.Value) < imageTimes[i])
            //    //        {
            //    //            MessageBox.Show(imageTimes[i - 1].ToString());
            //    //        }
            //    //    }
            //    //}

            //}
            //slider1.Value = myMusic.Position.TotalSeconds;
            //myMusic.Position = slider1.Value;
            //slider1.AddHandler();
        }

        /// <summary>
        /// 鼠标在slider上按下，处理播放进度的正确方法。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slider1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point p = Mouse.GetPosition(slider1);
            int pX = Convert.ToInt32(p.X);
            //MessageBox.Show(pX.ToString());
            //slider.width会随窗口自动改变，要用slider1.ActualWidth
            slider1.Value = pX / slider1.ActualWidth * slider1.Maximum;
            
            myMusic.Position = TimeSpan.FromSeconds(slider1.Value);
            //拖动进度条的时候，判断该显示哪张图片，两个时间之间{"001580.jpg":15,"001974.jpg":19}

            //Dictionary<string, int> imageAndTime的格式为{"001580.jpg":"15", "001974.jpg":"19"}


            //不需要在点击的时候判断是否正好等于图片的时间，在timer里已经判断了。
            //if (item.Value == Math.Floor(slider1.Value))
            //{
            //    image1.Source = new BitmapImage(new Uri(imagePath + item.Key, UriKind.RelativeOrAbsolute));
            //}
            //只需要判断范围
            for (int i = 1; i < imageTimes.Count; i++)
            {
                //i要从1开始，不然i-1会超出索引
                if ((Math.Floor(slider1.Value) < imageTimes[i]) && (Math.Floor(slider1.Value) > imageTimes[i - 1]))
                {
                    image1.Source = new BitmapImage(new Uri(imagePath + imageAndTime[imageTimes[i - 1]], UriKind.RelativeOrAbsolute));

                    //foreach (var item in imageAndTime)
                    //{
                    //    if (item.Value == imageTimes[i - 1])
                    //    {
                    //        image1.Source = new BitmapImage(new Uri(imagePath + item.Key, UriKind.RelativeOrAbsolute));
                    //    }
                    //}
                }
                else if (Math.Floor(slider1.Value) < imageTimes[0])
                {
                    image1.Source = new BitmapImage(new Uri(imagePath + imageAndTime.Values.First(), UriKind.RelativeOrAbsolute));
                }
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


        /// <summary>
        /// 加载视频
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myVideo_MediaOpened(object sender, RoutedEventArgs e)
        {
            //myVideo.Source = new Uri(@"E:\AppServ\www\github\MyWork\trunk\VideoToAudio\VideoToAudio\bin\Debug\LoveStory.mp4", UriKind.RelativeOrAbsolute);
            videoTotalTime = Convert.ToInt32(myVideo.NaturalDuration.TimeSpan.TotalSeconds);
            Console.WriteLine(videoTotalTime);
        }


        /// <summary>
        /// 视频结束时，xaml里缩小动画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            //myVideo.Visibility = System.Windows.Visibility.Hidden;
        }


        /// <summary>
        /// 音乐停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            myMusic.Stop();
            btnPlayOrPause.Content = "播放";
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
       











    }
}
