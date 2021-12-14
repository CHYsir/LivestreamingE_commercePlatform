using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace LivestreamingE_commercePlatform.GoodsInventorys
{
     public class GoodsInventoryDto : AuditedEntityDto<Guid>
    {
        public Nullable<int> InventotyRemaining { get; set; }                           //库存剩余
        public Nullable<System.Guid> SpecificationsId { get; set; }                  //规格Id
    }
}
