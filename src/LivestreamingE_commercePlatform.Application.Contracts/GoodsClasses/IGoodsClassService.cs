using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LivestreamingE_commercePlatform.GoodsClasses
{
    //添加服务接口
    //在.Contracts项目中，添加服务接口 这一步不是必须，但是解藕有效的
    //在这个接口中，继承是ICrudAbpService接口，需要固定的几个参数。（也可以直接继承IApplicationService接口，自定义方法）

    public interface IGoodsClassService: ICrudAppService<GoodsClassDto,Guid, PagedAndSortedResultRequestDto, CreateUpdateGoodsClassDto>
    {
        //这里定义的是具体业务服务接口

    }
}
