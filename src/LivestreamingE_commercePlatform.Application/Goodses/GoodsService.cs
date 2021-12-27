using HtmlAgilityPack;
using LivestreamingE_commercePlatform.Models;
using LivestreamingE_commercePlatform.Temporary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Newtonsoft.Json;

namespace LivestreamingE_commercePlatform.Goodses
{
    public class GoodsService : Volo.Abp.Application.Services.ApplicationService, IGoodsService
    {
        #region 定义的全局变量
        //爬取商品的url百联
        public string _Url = "https://search.bl.com/c-9999082454.html?blad=641-363596-1&blmmc=YXTF-baiduPC-MD1630484533757-0";
        #endregion

        #region 构造函数注入
        private readonly IRepository<Goods, Guid> _goods;
        private readonly IRepository<GoodsClass, Guid> _goodsClasses;
        private readonly IRepository<GoodsImg, Guid> _goodsImgs;
        private readonly IRepository<GoodsInventory, Guid> _goodsInventories;
        private readonly IRepository<GoodsSpecifications, Guid> _goodsSpecifications;


        //连接redis
        CSRedis.CSRedisClient _client = new CSRedis.CSRedisClient("127.0.0.1:6379");

        public GoodsService(
            IRepository<Goods, Guid> goods,
            IRepository<GoodsClass, Guid> goodsClasses,
            IRepository<GoodsImg, Guid> goodsImgs,
            IRepository<GoodsInventory, Guid> goodsInventories,
            IRepository<GoodsSpecifications, Guid> goodsSpecifications
            )
        {

            //初始化帮助类
            RedisHelper.Initialization(_client);

            _goods = goods;
            _goodsClasses = goodsClasses;
            _goodsImgs = goodsImgs;
            _goodsInventories = goodsInventories;
            _goodsSpecifications = goodsSpecifications;
        }

        #endregion

        #region 商品增删改查

        //商品表显示
        [HttpGet, Route("show")]
        //[Authorize]
        public async Task<List<GoodsDto>> Show()
        {
            var items = await _goods.GetListAsync();
            return items.Select(item => new GoodsDto
            {
                Id=item.Id,
                GoodsName = item.GoodsName,
                GoodsDescribe = item.GoodsDescribe,
                GoodsStatus = item.GoodsStatus,
                GoodsPrice = item.GoodsPrice,
                PreferentialPrice = item.PreferentialPrice
            }).ToList();
        }

        //分页、查询、联查
        [HttpGet, Route("selectlist")]
        public Tuple<List<TemporaryDto>, int> SelectList( int pageIndex, int pageSize, string name = "")
        {
            var ss = _goods.GetListAsync().Result;
            var ff = _goodsClasses.GetListAsync().Result;
            var ii = _goodsImgs.GetListAsync().Result;
            var kk = _goodsInventories.GetListAsync().Result;
            var gg = _goodsSpecifications.GetListAsync().Result;

            var lc = (from a in ff join b in ss on a.Id  equals b.ClassifiCationId
                           join  c in ii   on   b.Id equals c.GoodsId join d in gg on b.Id equals d.GoodsId join e in kk on d.Id equals e.SpecificationsId select new TemporaryDto
                           {
                        Id=b.Id,
                        GoodsName=b.GoodsName,
                        ClassName=a.ClassName

                      }).ToList();

            //模糊查询
            if (!string.IsNullOrEmpty(name))
            {
                lc = lc.Where(x => x.GoodsName.Contains(name)).ToList();
            }
            int count = lc.Count();
            var list = lc.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return Tuple.Create(list, count);

        }

        //商品添加，采用 爬虫，批量添加
        [HttpGet, Route("geturl")]
        public string GetUrl()
        {
            //*[@id="all-list"]/div[1]/div[2]/ul/li[1] 正则表达式
            //HtmlWeb htmlWeb = new HtmlWeb();
            //htmlWeb.Load(_Url);

            //获取一整页数据 从前台传过来的url
            var html = Helper.Crawler.DownLoadUrl(_Url);   //封装的静态类

            List<GoodsJoinShowDto> Joinlist = new List<GoodsJoinShowDto>();

            string listpage = "/html/body/div[5]/div[4]/ul/li";   //获取的是当前页面的父级节点 
            HtmlDocument listhtml = new HtmlDocument();
            listhtml.LoadHtml(html);    //加载 获取的html页面
                                        //HtmlNodeCollection 相当于一个集合 把获取的 listpage节点 循环赋值
            HtmlNodeCollection listnode = listhtml.DocumentNode.SelectNodes(listpage);
            foreach (HtmlNode Node in listnode)
            {
                var goodsJoin = Helper.Crawler.GoodsList(Node);  //调用 当个节点的值

                Joinlist.Add(goodsJoin); //添加进自己的集合 也可以直接入库操作
            }

            var GoodsClass = _goodsClasses.GetListAsync().Result.Where(x => x.ClassPId.Equals(new Guid("df627812-6366-9b0b-a7c4-3a010e515c3b"))).ToList();

            foreach (var item in Joinlist)
            {
                //商品
                Goods goods = new Goods();
                goods.GoodsPrice = item.GoodsPrice;
                goods.GoodsName = item.GoodsName;
                foreach (var ClassPidId in GoodsClass)
                {
                    goods.ClassifiCationId = ClassPidId.ClassPId;
                }
                _goods.InsertAsync(goods);

                //商品图片
                GoodsImg goodsImg = new GoodsImg();
                goodsImg.ImgName = item.ImgName;
                goodsImg.ImgUrl = item.ImgUrl;
                goodsImg.GoodsId = goods.Id;
                _goodsImgs.InsertAsync(goodsImg);

                //商品规格
                GoodsSpecifications goodsSpecification = new GoodsSpecifications();
                goodsSpecification.GoodsId = goods.Id;
                goodsSpecification.GoodsColor = "中国红";
                goodsSpecification.GoodsWeight = "60";
                goodsSpecification.GoodsTaste = "好吃";
                _goodsSpecifications.InsertAsync(goodsSpecification);

                //商品库存
                GoodsInventory goodsInventory = new GoodsInventory();
                goodsInventory.SpecificationsId = goodsSpecification.Id;
                goodsInventory.InventotyRemaining = new Random().Next(1000, 9999);
                _goodsInventories.InsertAsync(goodsInventory);
            }
            //将集合里的数据添加入库
            //_GoodsRepository.InsertManyAsync(list);
            if (Joinlist.Count > 0 && Joinlist != null)
            {
                return "爬取成功";
            }
            else
            {
                return "似乎受到了一点点阻碍";
            }
        }

        //添加
        [HttpPost, Route("createasync")]
        public async Task<GoodsDto> CreateAsync(GoodsDto dto)
        {
            await _goods.InsertAsync(ObjectMapper.Map<GoodsDto, Goods>(dto));
            return dto;
        }

        //删除
        [HttpDelete,Route("delete")]
        public async Task<string> Delete(Guid id)
        {
            await _goods.DeleteAsync(id);
            return "删除成功！";
        }


        //批删 
        [HttpDelete, Route("deleteps")]
        public string Deleteps(string ids)
        {
            string[] List = ids.Split(',');

            var goodlist = new List<Guid>();

            List.ToList().ForEach(x =>
            {
                goodlist.Add(new Guid(x));
            });

            _goods.DeleteManyAsync(goodlist);
            return "删除成功！";
        }



        //反填
        [HttpGet, Route("ftasync")]
        public async Task<GoodsDto> FtAsync(Guid id)
        {
            var ft = await _goods.GetAsync(id);
            return ObjectMapper.Map<Goods, GoodsDto>(ft);

        }


        //修改  待更正
        [HttpPost, Route("update")]
        public async Task<GoodsDto> Update(GoodsDto dto)
        {
            var info = await _goods.UpdateAsync(ObjectMapper.Map<GoodsDto, Goods>(dto));
            return ObjectMapper.Map<Goods, GoodsDto>(info);
       
        }


        #endregion

        #region 上传图片
        public string Img(IFormFile file)
        {
            string rootdir = AppContext.BaseDirectory.Split(@"\bin\")[0];
            string fname = DateTime.Now.ToString("yyyyMMddHHmmssffff") + System.IO.Path.GetExtension(file.FileName);
            var path = rootdir + @"\Image\" + fname;
            using (System.IO.FileStream fs = System.IO.File.Create(path))
            {
                file.CopyTo(fs);
                fs.Flush();  //清空文件流
            }
            return "http://localhost:8067/" + fname;//将能访问新文件的网址回传给前端
        }
        #endregion

        #region 加入购物车
        [HttpPost,Route("addcar")]
        public Tuple<bool,string> AddCar(CarViewModel model)
        {
            string msg;
            bool status;
            var key = $"ds{model.UserId}";
            var getkey = _client.Get(key);

            //防止被覆盖，定义一个集合 放很多商品
            var list = new List<CarViewModel>();

            //判断购物车是否为空，如果为空直接放入商品 
            if (getkey == null)
            {
                list.Add(model);
                _client.Set(key, list);
            }
            else
            {
                //数据库实体
                var goods = _goods.GetListAsync().Result;
                var goodskc = _goodsInventories.GetListAsync().Result;
                var goodsgg = _goodsSpecifications.GetListAsync().Result;

                var redislist = JsonConvert.DeserializeObject<List<CarViewModel>>(getkey);

                //在购物车中查找该商品
                var selectredis = redislist.Where(x => x.GoodsId == model.GoodsId).FirstOrDefault();

                var selectdb = (from a in goods
                                join b in goodsgg on a.Id equals b.GoodsId
                                join c in goodskc on b.Id equals c.SpecificationsId
                                select new
                                {
                                    Inventory = c.InventotyRemaining,
                                    GoodsId = a.Id
                                }).FirstOrDefault();

                if (selectdb.Inventory < model.GoodsNumber)
                {
                    msg = "商品库存不足！请看看其它商品";
                    status = false;
                    return Tuple.Create(status, msg);
                }
                else
                {
                    //判断购物车中如果没有该商品那就放入
                    if (selectredis == null)
                    {
                        redislist.Add(model);
                    }
                    //有的话就数量+1
                    else
                    {
                        selectredis.GoodsNumber += model.GoodsNumber;
                    }
                }

                _client.Set(key, redislist);
            }

            msg = "加入购物车成功";
            status = true;
            return Tuple.Create(status, msg);
        }
        #endregion

        #region 显示购物车
        [HttpGet,Route("showcar")]
        public Tuple<List<ShowModel>,string> ShowCar(string userId)
        {
            string msg="";
            var key = $"ds{userId}";
            var getlist = _client.Get(key);

            //redis里的集合
            var redislist = JsonConvert.DeserializeObject<List<CarViewModel>>(getlist);

            if (redislist == null || redislist.Count <= 0)
            {
                 msg = "购物车里还没有商品";
            }

            //数据库里的集合
            var goodslist = (from e in _goods
                             join f in _goodsImgs on e.Id equals f.GoodsId join d in _goodsSpecifications on e.Id equals d.GoodsId join k in _goodsInventories on d.Id equals k.SpecificationsId
                             select new
                             {
                                 Id = e.Id,
                                 GoodsName = e.GoodsName,
                                 GoodsPrice = e.GoodsPrice,
                                 ImgName = f.ImgName,
                                 ImgUrl = f.ImgUrl,
                                 //InventotyRemaining=k.InventotyRemaining
                             }).ToList();

            //联查
            var lc = (from a in redislist
                      join b in goodslist on a.GoodsId equals b.Id
                      select new ShowModel
                      {
                          Id=b.Id,
                          GoodsName = b.GoodsName,
                          GoodsPrice = b.GoodsPrice,
                          ImgName=b.ImgName,
                          ImgUrl=b.ImgUrl,
                          GoodsNumber = a.GoodsNumber
                      }).ToList();
            return Tuple.Create(lc,msg);
        }

        #endregion

        #region 购物车临时表
        public class CarViewModel
        {
            public Guid UserId { get; set; }
            public Guid GoodsId { get; set; }
            public int GoodsNumber { get; set; }
        }
        #endregion

        public class ShowModel
        {
            public Guid Id { get; set; }
            public string   GoodsName { get; set; }                                        //商品名称
            public double GoodsPrice { get; set; }                                          //商品价格
            public string   ImgName { get; set; }                                             //图片名称
            public string   ImgUrl { get; set; }                                                 //图片路径
            public int GoodsNumber { get; set; }

        }



    }
}
