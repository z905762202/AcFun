using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcFun
{//一键整理 CTRL+k +d
    class DateInfoInFrist
    {

       


        //static方法就是没有this的方法。在static方法内部不能调用非静态方法，反过来是可以的。而且可以在没有创建任何对象的前提下，仅仅通过类本身来调用static方法。这实际上正是static方法的主要用途
        //简单来说 方便在没有创建对象的情况下来进行调用（方法/变量）
        //很显然，被static关键字修饰的方法或者变量不需要依赖于对象来进行访问，只要类被加载了，就可以通过类名去进行访问

        //static变量也称作静态变量，静态变量和非静态变量的区别是：静态变量被所有的同一个的对象所共享，在内存中只有一个副本，它当且仅当在类初次加载时会被初始化。
        //而非静态变量是对象所拥有的，在创建对象的时候被初始化，存在多个副本，各个对象拥有的副本互不影响
        public    List<ShouYeInfo> result = new List<ShouYeInfo>();//用于FlipView
      

        //static方法一般称作静态方法，由于静态方法不依赖于任何对象就可以进行访问，因此对于静态方法来说，
        //是没有this的，因为它不依附于任何对象，既然都没有对象，就谈不上this了。
        //并且由于这个特性，在静态方法中不能访问类的非静态成员变量和非静态成员方法，
        //因为非静态成员方法/变量都是必须依赖具体的对象才能够被调用。
        //非静态的方法必须创建对象才能用 而这个静态的方法
        //静态方法必须使用静态变量
        //但是要注意的是，虽然在静态方法中不能访问非静态成员方法和非静态成员变量，但是在非静态成员方法中是可以访问静态成员方法/变量的。
        //因此，如果说想在不创建对象的情况下调用某个方法，就可以将这个方法设置为static。我们最常见的static方法就是main方法，至于为什么main方法必须是static的，
        //现在就很清楚了。因为程序在执行main方法的时候没有创建任何对象，因此只接通过类名来访问。

        //这些跟Java里的类类变量和类方法一样  参考资料http://www.cnblogs.com/dolphin0520/p/3799052.html
        public  async Task<List<ShouYeInfo>> GetShouYeInfo()//没有返回值的异步不需要等待
        {
            string html = await GetContent.GetIndex();


            //获取含有首页内容的字符串

            //解析上面的字符串
            HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);
                //找到一个符合条件的标签 括号内参数的格式是//标签名[@属性名='值']
                //var bangumi = doc.DocumentNode.SelectSingleNode("//section");
                //获得所有的子节点
                //var list = bangumi.ChildNodes;
                //list可以用foreach遍历得到每个子节点
                //一个节点的结构:<标签名 属性名1="属性值1" 属性名2="属性值2">内容</标签名>
                //               <ldh class="rbq" price="1">真舒服</ldh>
                //    节点.Attitudes["节点属性名"].Value可以得到属性值
                //    节点.InnerText可以得到内容的值
                //如果内容是其他节点的话可以和上面一样用  节点.SelectSingleNode(string xpath)
                //                          或者用  节点.Child[索引(0开始)]   来得到子节点
                //list=ul


                //c.Lch = list[i].SelectSingleNode(".//em[@class='lch']").InnerText.Replace('楼', '\0');  通过标签与类名查找


                var FlipView = doc.DocumentNode.SelectSingleNode(".//ul[@class='slider-con']");  //获得//ul[@class='slider-con' 这个的节点
                var FlipViewlist = FlipView.ChildNodes;

                foreach (var F in FlipViewlist)
                {
                    var v = new ShouYeInfo();
                    v.Link = F.FirstChild.Attributes["href"].Value;
                    v.Image = new Uri(F.FirstChild.FirstChild.Attributes["src"].Value);
                    v.Name = F.FirstChild.FirstChild.Attributes["alt"].Value;
                v.i = 1;

                    result.Add(v);

                }


                var FlipViewBesid = doc.DocumentNode.SelectSingleNode(".//ul[@m-id='302']");//通过id 
                var FlipViewBesidlist = FlipViewBesid.ChildNodes;

                foreach (var F in FlipViewBesidlist)
                {
                    var v2 = new ShouYeInfo();//实例化一个对象
                    v2.Link=F.FirstChild.Attributes["href"].Value;
                    v2.Image= new Uri(F.FirstChild.FirstChild.Attributes["src"].Value);
                    v2.Name = F.FirstChild.FirstChild.Attributes["alt"].Value;
                v2.i = 2;
                    result.Add(v2);
                }

            return result;






        }
    }

    public class ShouYeInfo
    {
        public string Link { get; set; }
        public string Name { get; set; }
        public Uri Image { get; set; }
        public int i { get; set; }
      
    }


    









}
