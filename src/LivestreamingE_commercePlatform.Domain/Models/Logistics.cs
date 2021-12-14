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

    //物流表
    public partial class Logistics : AuditedAggregateRoot<Guid>
    {
        public string OrderNumber { get; set; }                                                               //运单号
        public Nullable<System.Guid> AddressId { get; set; }                                        //收件人地址外键
        public Nullable<System.DateTime> DistributionTime { get; set; }                      //配送时间
        public Nullable<System.DateTime> DeliveryTime { get; set; }                            //送达时间
        public string LogisticsStatus { get; set; }                                                               //物流状态
        public string DistributionPersonnel { get; set; }                                                    //配送人员
        public string DistributionRecord { get; set; }                                                        //配送记录
        public string DistributionPhone { get; set; }                                                         //配送员电话
        public string orderInfoBarCode { get; set; }                                                         //配送方式
        public decimal orderInfoActivePrice { get; set; }                                                   //运费
    }
}