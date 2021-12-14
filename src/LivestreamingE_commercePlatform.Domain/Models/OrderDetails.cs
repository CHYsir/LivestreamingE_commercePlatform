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
    using Volo.Abp.Domain.Entities.Auditing;

    //订单详情表
    public partial class OrderDetails : AuditedAggregateRoot<Guid>
    {
        public string OrderNumber { get; set; }                                           //订单编号(雪花)
        public Nullable<System.Guid> GoodsId { get; set; }                      //商品名称
        public Nullable<int> GoodsNumber { get; set; }                            //购买数量
        public Nullable<double> GoodsPrice { get; set; }                           //商品总价
    }
}
