using LivestreamingE_commercePlatform.Temporary;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LivestreamingE_commercePlatform.Goodses
{
    public interface IGoodsService:Volo.Abp.Application.Services.IApplicationService
    {
        //显示
        Task<List<GoodsDto>> Show();

        //显示查询分页
        Tuple<List<TemporaryDto>, int> SelectList(int pageIndex, int pageSize, string name="");

        //添加
        Task<GoodsDto> CreateAsync(GoodsDto dto);

        //批量添加
        string GetUrl();

        //删除
        Task<string> Delete(Guid id);

        //批量删除
        string Deleteps(string ids);  

        //反填
        Task<GoodsDto> FtAsync(Guid id);

        //修改
        Task<GoodsDto> Update(GoodsDto dto);

        //上传图片
        string Img(IFormFile file);

    }
}
