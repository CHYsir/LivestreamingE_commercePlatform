//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace LivestreamingE_commercePlatform.Models
{
    using System;
    using System.Collections.Generic;
    using Volo.Abp.Domain.Entities;
    using Volo.Abp.Domain.Entities.Auditing;

    //图片表
    public partial class GoodsImg: AuditedAggregateRoot<Guid>
    {
        public string ImgName { get; set; }                                             //图片名称
        public string ImgUrl { get; set; }                                                 //图片路径
        public Nullable<System.Guid> GoodsId { get; set; }                 //商品Id

        public Guid Imagedelete { get; set; } //逻辑删除
    }
}