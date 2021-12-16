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

namespace LivestreamingE_commercePlatform.Goodses
{
    public class GoodsService : Volo.Abp.Application.Services.ApplicationService, IGoodsService
    {
        //依赖注入
        private readonly IRepository<Goods, Guid> _goods;
        private readonly IRepository<GoodsClass, Guid>  _goodsClasses;
        private readonly IRepository<GoodsImg, Guid>    _goodsImgs;
        private readonly IRepository<GoodsInventory, Guid>   _goodsInventories;
        private readonly IRepository<GoodsSpecifications, Guid>  _goodsSpecifications;

        public GoodsService(
            IRepository<Goods,Guid> goods,
            IRepository<GoodsClass, Guid>  goodsClasses,
            IRepository<GoodsImg, Guid>    goodsImgs,
            IRepository<GoodsInventory, Guid>  goodsInventories,
            IRepository<GoodsSpecifications, Guid>  goodsSpecifications
            )
        {
            _goods = goods;
            _goodsClasses = goodsClasses;
            _goodsImgs = goodsImgs;
            _goodsInventories = goodsInventories;
            _goodsSpecifications = goodsSpecifications;
        }


        //商品表显示
        [HttpGet,Route("show")]
        //[Authorize]
        public async Task<List<GoodsDto>> Show()
        {
            var items=await _goods.GetListAsync();
            return items.Select(item => new GoodsDto
            {
                GoodsName = item.GoodsName,
                GoodsDescribe = item.GoodsDescribe,
                GoodsStatus = item.GoodsStatus,
                GoodsPrice = item.GoodsPrice,
                PreferentialPrice=item.PreferentialPrice        
            }).ToList();
        }

        //分页、查询、联查
        [HttpGet, Route("selectlist")]
        public Tuple<List<TemporaryDto>, int, string> SelectList(string name, int pageIndex, int pageSize)
        {
            var s = _goods.GetListAsync().Result;
            var f = _goodsClasses.GetListAsync().Result;
            var i = _goodsImgs.GetListAsync().Result;
            var k = _goodsInventories.GetListAsync().Result;
            var g = _goodsSpecifications.GetListAsync().Result;

            var lc = (from a in _goods
                      join b in _goodsClasses on a.ClassifiCationId equals b.Id
                      join c in _goodsImgs on a.Id equals c.GoodsId
                      join d in _goodsSpecifications on a.Id equals d.GoodsId
                      join e in _goodsInventories on d.Id equals e.SpecificationsId
                      select new TemporaryDto
                      {
                          Id = a.Id,
                          GoodsName = a.GoodsName,
                          GoodsDescribe = a.GoodsDescribe,
                          GoodsPrice = a.GoodsPrice,
                          PreferentialPrice = a.PreferentialPrice,
                          GoodsStatus = a.GoodsStatus,
                          ImgName = c.ImgName

                      }).ToList();

            //模糊查询
            if (!string.IsNullOrEmpty(name))
            {
                lc = lc.Where(x => x.GoodsName.Contains(name)).ToList();
            }
            int count = lc.Count();
            var list = lc.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return Tuple.Create(list, count,name);

        }


        //添加
        [HttpPost, Route("createasync")]
        public async Task<GoodsDto> CreateAsync(GoodsDto dto)
        {
            await _goods.InsertAsync(ObjectMapper.Map<GoodsDto, Goods>(dto));
            return dto;
        }

        //删除
        [HttpDelete, Route("deleteasync")]
        public async Task<string> DeleteAsync(Guid id)
        {
            await _goods.DeleteAsync(id);
            return "删除成功";
        }

        //反填
        [HttpGet, Route("ftasync")]
        public async Task<GoodsDto> FtAsync(Guid id)
        {
            var ft=await _goods.GetAsync(id);
            return ObjectMapper.Map<Goods, GoodsDto>(ft);

        }

        //修改
        [HttpPut, Route("editasync")]
        public async Task<GoodsDto> EditAsync(GoodsDto dto)
        {
            var upd = await _goods.UpdateAsync(ObjectMapper.Map<GoodsDto, Goods>(dto));
            return ObjectMapper.Map<Goods, GoodsDto>(upd);

        }

        //上传图片
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

        
    }
}
