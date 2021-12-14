using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace LivestreamingE_commercePlatform.GoodsImgs
{
    public  class GoodsImgDto : AuditedEntityDto<Guid>
    {
        public string ImgName { get; set; }                                             //图片名称
        public string ImgUrl { get; set; }                                                 //图片路径
        public Nullable<System.Guid> GoodsId { get; set; }                 //商品Id
        public Guid Imagedelete { get; set; } //逻辑删除
    }
}
