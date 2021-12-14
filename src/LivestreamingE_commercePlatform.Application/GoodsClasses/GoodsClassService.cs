using LivestreamingE_commercePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace LivestreamingE_commercePlatform.GoodsClasses
{
    //在.Application项目中，添加服务实现类，实现上一步定义的业务接口
    public class GoodsClassService: CrudAppService<GoodsClass, GoodsClassDto,Guid, PagedAndSortedResultRequestDto, CreateUpdateGoodsClassDto>, IGoodsClassService
    {
        public GoodsClassService(IRepository<GoodsClass,Guid> repository) : base(repository)
        {
            //
        }
    }
}
