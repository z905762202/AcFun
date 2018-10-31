using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcFun
{
    public class Tuijian
    {
        //封面地址
        public Uri Picture { get; set; }
        //视屏地址
        public string Link { get; set; }
        //番剧名字
        public string Name { get; set; }
        //番剧时间
        public string Time { get; set; }
        //up名字
        public string UPName { get; set; }


        public static async Task<List<Tuijian>> GetTuijian()
        {
            List<Tuijian> result = new List<Tuijian>();
            try
            {
                //获取首页内容的字符串
                string html = await GetContent.GetIndex();
                //解析上面的字符串
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);
                //通过ID查找
                var Tuijian = doc.DocumentNode.SelectSingleNode(".//@m-id='4'");
                var list = Tuijian.ChildNodes;

                int i = 0;
                foreach (var item in list)
                {
                    foreach (var it in item.ChildNodes)
                    {
                        var v = new Tuijian();

                        v.Link = it.FirstChild.Attributes["href"].Value;
                        v.Picture = new Uri(it.FirstChild.FirstChild.Attributes["src"].Value);
                        v.Name = it.FirstChild.Attributes["title"].Value;
                        v.Time = it.FirstChild.FirstChild.ChildNodes[2].InnerText;
                        v.UPName = it.FirstChild.Attributes["userName"].Value;

                        result.Add(v);
                    }
                    i++;
                }
            }

            catch (Exception ex)
            {

            }

            return result;


        }

    }
}