using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace LivestreamingE_commercePlatform.GoodsClasses
{
    public  class GoodsClassDto: AuditedEntityDto<Guid>
    {
        public string ClassName { get; set; }                                                               //分类名称
        public Nullable<System.Guid> ClassPId { get; set; }                                     //父级Id
        public string ClassStatus { get; set; }                                                               //分类状态
        public Guid CategoryDelete { get; set; } //逻辑删除
    }
}
