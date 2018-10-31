using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace AcFun
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MediaPlayer : Page
    {
        public MediaPlayer()
        {
            this.InitializeComponent();
            Play2_ClickAsync();//这里面到时候传参
        }
        public class MediaPlayerSource
        {
            public string Title;
            public string Hid;
            public int Part;
            public string PartTitle;
            //分类id
            public string Tid;
            public bool IsLocalFile;
            public List<string> PlayList;
        }
        public async void Play2_ClickAsync()
        {
            //https://github.com/amamiya/SYEngine/blob/master/!GUIDE/Segment/Segment.md 播放分段的 FLV\MP4

            

            //载入播放引擎
            SYEngine.Core.Initialize();
            //所有URL地址列表
            var url_list = new List<String>();//实例化新的 SYEngine.Playlist 对象；
            //url_list.Add("http://data.vod.itc.cn/?rb=1&key=jbZhEJhlqlUN-Wj_HEI8BjaVqKNFvDrn&prod=flash&pt=1&new=/51/116/UdKGIuSjQIO8dynrybyS1E.mp4");


            url_list.Add("http://player.acfun.cn/route_mp4?dt=0&uid=0&timestamp=1540371865&fid=040040020400005BCEED7100010002570000000000-0000-0000-0231-311400000000.mp4&ns=video.acfun.cn&ran=0&vid=5bceeca80cf2c691e00a68fb&customer_id=5859fdaee4b0eaf5dd325b91&sign=ct5bd035990cf22adc685b3ddc");
            //拒绝访问了 问题应该出在请求头


            //构造SYEngine的PlayList。
            var play_list = new SYEngine.Playlist(SYEngine.PlaylistTypes.NetworkHttp);//网络类型
            foreach (var url in url_list)
            {
                //获取每个URL文件的媒体时长和文件大小。
                //如果你的App能通过其他方法提供这些信息，也可以不执行这样的操作。
                // var file = new SYEngine.MediaInformation();
                //await file.OpenAsync(new Uri(url));//失败会有异常，这里忽略。


                play_list.Append(url, 0, 0); // 添加到PlayList
                //也可一〉play_ list. Append(url, D, 0)， 然后把cfgs的DetectDur ationF orParts设置为true。
                //播放引擎会自动连接所有 分段地址去获取文件大小和时长。
            }

            //设置网络参数。使用 SYEngine.PlaylistNetworkConfigs 结构体来设置网络参数。这个结构体的相关解释请拉到页面底部的 网络选项说明 来查看
            SYEngine.PlaylistNetworkConfigs cfgs = default(SYEngine.PlaylistNetworkConfigs);
            cfgs.HttpUserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.87 Safari/537.36";

            cfgs.HttpReferer = string.Empty;

            cfgs.HttpCookie = string.Empty;




            cfgs.UniqueId = string.Empty; //如果要使用动态更新URL，这个不能是Empty,这里不使用。
            cfgs.DownloadRetryOnFail = true; //启用断线重连，建议打开。
            cfgs.DetectDurationForParts = true;
            play_list.NetworkConfigs = cfgs;



            //设置到MedisElement。
            MediaPlayer1.Source = await play_list.SaveAndGetFileUriAsync();// 最后，使用 MediaElement 的 Play 方法来播放，或者设置 AutoPlay 属性为true
            MediaPlayer1.Play();

        }

        private void Play2_Click(object sender, RoutedEventArgs e)
        {
            Play2_ClickAsync();
        }
    }
}
