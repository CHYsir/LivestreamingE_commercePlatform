using HtmlAgilityPack;
using LivestreamingE_commercePlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace LivestreamingE_commercePlatform.Text
{
    public  class textCrawler:ApplicationService
    {
        ////写死的Url
        //public string _Url = "https://search.bilibili.com/all?keyword=.net%20core%20%E7%88%AC%E8%99%AB&from_source=webtop_search&spm_id_from=333.851";

        //[HttpGet, Route("text/GetUrl")]
        //public List<Goods> GetUrl()
        //{
        //    //*[@id="all-list"]/div[1]/div[2]/ul/li[1] 正则表达式
        //    //HtmlWeb htmlWeb = new HtmlWeb();
        //    //htmlWeb.Load(_Url);

        //    //获取一整页数据 从前台传过来的Url
        //    var html = Helper.Crawler.DownLoadUrl(_Url);//封装的静态类
        //    List<Goods> list = new List<Goods>();
        //    string listpage = "//*[@id='all-list']/div[1]/div[2]/ul/li"; //获取的是当前页面的父级节点
        //    HtmlDocument listhtml = new HtmlDocument();
        //    listhtml.LoadHtml(html);//加载 获取的html页面
        //    //HtmlNodeCollection 相当于一个集合 把获取的 listpage节点 循环赋值
        //    HtmlNodeCollection listnode = listhtml.DocumentNode.SelectNodes(listpage);
        //    foreach (HtmlNode Node in listnode)
        //    {
        //        var goods = Helper.Crawler.GoodsList(Node);//调用 当个节点的值
        //        list.Add(goods);//添加进自己的集合 也可以直接入库操作
        //    }
        //    return list;//返回这个集合 ，返回值可以是任意类型 看你喜欢
        //}

    }
}
