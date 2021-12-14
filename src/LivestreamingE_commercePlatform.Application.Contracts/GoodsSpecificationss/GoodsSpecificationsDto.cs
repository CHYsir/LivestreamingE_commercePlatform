using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace LivestreamingE_commercePlatform.Ids4
{
    public class GoodsSpecificationsDto : AuditedEntityDto<Guid>
    {
        public Nullable<System.Guid> GoodsId { get; set; }                   //商品Id
        public string GoodsWeight { get; set; }                                         //商品重量（kg）
        public string GoodsColor { get; set; }                                           //商品颜色
        public string GoodsTaste { get; set; }                                            //商品口味
    }
}
