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

    //商品规格表
    //BasicAggregateRoot<Guid>  不生成多余字段
    //AuditedAggregateRoot<Guid> 生成封装的字段
    public partial class GoodsSpecifications: AuditedAggregateRoot<Guid>
    {
        public Nullable<System.Guid> GoodsId { get; set; }                   //商品Id
        public string GoodsWeight { get; set; }                                         //商品重量（kg）
        public string GoodsColor { get; set; }                                           //商品颜色
        public string GoodsTaste { get; set; }                                            //商品口味
    }
}
