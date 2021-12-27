using HtmlAgilityPack;
using LivestreamingE_commercePlatform.Goodses;
using LivestreamingE_commercePlatform.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LivestreamingE_commercePlatform.Helper
{
    //封装的爬虫的方法
    public class Crawler
    {
        public static NLog.ILogger logger = NLog.LogManager.GetCurrentClassLogger();
        //调用DownLoadHtml方法
        public static string DownLoadUrl(string Url)
        {
            return DownLoadHtml(Url, Encoding.UTF8);
        }
        /// <summary>
        ///  该方法是处理传进来Url地址 进行一系列的判断 成功返回爬取的Html页面 否则记录错误休息保存到日志
        /// </summary>
        /// <param name="Url">所获取的Url</param>
        /// <param name="encode">编码格式</param>
        /// <returns></returns>
        public static string DownLoadHtml(string Url, Encoding encode)
        {
            string html = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(Url);
                request.Timeout = 30 * 1000;//设置时间为30s
                request.UserAgent = "Mozilla/5.0 (Windows Nt 10.0; WOW64)AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36";//用户代理

                request.ContentType = "text/html; charset=utf-8";
                request.CookieContainer = new CookieContainer();
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        logger.Warn(string.Format("抓取{0}地址返回失败，resphone.StatusCode为{1}", Url, response.StatusCode));  //错误信息输入日志（）
                    }
                    else
                    {
                        try
                        {
                            StreamReader sr = new StreamReader(response.GetResponseStream(), encode);
                            html = sr.ReadToEnd();
                            sr.Close();
                        }
                        catch (WebException ex)
                        {
                            if (ex.Message.Equals("远程服务器返回错误:(306)。"))
                            {
                                //错误信息输入日志（
                                logger.Error("远程服务器返回错误:(306)。", ex);
                                html = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //错误信息输入日志
                logger.Error("DownloadHtml抓取{0}出现异常", Url, ex);
                html = null;
            }

            return html;
        }

        public static GoodsJoinShowDto GoodsList(HtmlNode node)
        {
            //这是我所需要的赋值的实体类 用来接收 解析完成后的值
            var good = new GoodsJoinShowDto();

            //实例化一个 HtmlDocument对象
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(node.OuterHtml); //加载获取的 html节点

            //商品名称
            string GoodsName = "/*/div/div[2]/a/img";// 获取 //*[@id='all-list']/div[1]/div[2]/ul/li节点下  "/*/a" a标签
            HtmlNode GoodsNode = document.DocumentNode.SelectSingleNode(GoodsName);

            //Attributes 更够获取标签里的属性 例 title type 还有其他获取的方式 详细参考HtmlAgilityPack官网
            var goodstext = GoodsNode.Attributes["title"].Value;//获取到了 a标签里面 title 属性的值
            good.GoodsName = goodstext;//赋值给实体类属性 

            //商品价格
            var Price = "/*/div/div[3]/div"; //同理
            HtmlNode PriceNode = document.DocumentNode.SelectSingleNode(Price);
            var PriceText = PriceNode.InnerText;
            var splitPrice = PriceText.Split("￥")[1];
            good.GoodsPrice = Convert.ToDouble(splitPrice);


            //商品描述
            var GoodsDescribe= "/*/div/div[2]/a/img";  //同理
            HtmlNode GoodsDescribeNode = document.DocumentNode.SelectSingleNode(GoodsDescribe);
            var GoodsDescribeValue = GoodsDescribeNode.Attributes["alt"].Value;
            good.GoodsDescribe = GoodsDescribeValue;

            //商品图片
            var Img = "/*/div/div[2]/a/img";  //同理
            HtmlNode ImgNode = document.DocumentNode.SelectSingleNode(Img);
            var ImgText = ImgNode.Attributes["src"].Value;
            good.ImgUrl = ImgText;

            var ImgName = "/*/div/div[2]/a/img";   //同理
            HtmlNode ImgNameNode = document.DocumentNode.SelectSingleNode(ImgName);
            var ImgNameText = ImgNameNode.Attributes["title"].Value;
            good.ImgName = ImgNameText;
            //以上我只赋值了两个我所需要的，根据自己的需求 可以追加 实体 属性赋值

            return good;
        }

    }

}
