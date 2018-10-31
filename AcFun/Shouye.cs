using AcFun.Tucao;
using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcFun
{
    class GetContent
    {
        public static string  url = "http://www.acfun.cn/";
        public static async void  ShouYeAsync()
        {
            var str =await GetIndex();

            // From String
            var doc = new HtmlDocument();
            doc.LoadHtml(str);

        }

        static public async Task<string> GetIndex()
        {
           
            var result = await Methods.HttpGetAsync(url);//可以随便访问的网站 可以不加请求头 
            //httpget返回的是html文档

            Stream stream = (await result.Content.ReadAsInputStreamAsync()).AsStreamForRead();
            StreamReader streamReader = new System.IO.StreamReader(stream, Encoding.UTF8);//防止乱码
            var str = streamReader.ReadToEnd();


           
            return str;


        }


        /// <summary>
        /// 推荐信息
        /// </summary>
        /// <param name="hid">HID</param>
        /// <param name="part">分p号(从0开始)</param>
        /// <returns></returns>
       














    }

}







