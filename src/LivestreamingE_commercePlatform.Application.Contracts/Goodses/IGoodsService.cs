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
        Tuple<List<TemporaryDto>, int,string> SelectList(string name, int pageIndex, int pageSize);

        //添加
        Task<GoodsDto> CreateAsync(GoodsDto dto);

        //删除
        Task<string>  DeleteAsync(Guid id);

        //反填
        Task<GoodsDto> FtAsync(Guid id);

        //修改
        Task<GoodsDto> EditAsync(GoodsDto dto);

        //上传图片
        string Img(IFormFile file);
    }
}
