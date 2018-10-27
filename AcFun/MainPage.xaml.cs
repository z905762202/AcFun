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
using System.Threading.Tasks;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace AcFun
{

    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static string ACFUN_url = "http://www.acfun.cn";

  
        public MainPage()
        {
           
            
            this.InitializeComponent();

            FlipView();//没有返回值的异步方法就不需要等待(await)






        }

        public async void FlipView()//显示flip的内容 没有返回值的异步方法就不需要等待(await)
        {
            DateInfoInFrist info = new DateInfoInFrist();
           
            var list =await  info.GetShouYeInfo();

            Flip.ItemsSource = list.FindAll((d) => d.i == 1);//检索的另一种格式List<Entity> ltEntity= entCollection.FindAll(delegate(Entity m) { return m.name== "aa"; })

            gridview.ItemsSource= list.FindAll((d) => d.i == 2);

        }






    }
}
