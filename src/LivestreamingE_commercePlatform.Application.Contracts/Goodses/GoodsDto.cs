using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace LivestreamingE_commercePlatform.Goodses
{
    //在.Contracts项目，添加Dto类。在ABP服务中，所有的操作基类都来自CrudAppService，它需要DTO类。继承AuditeEntityDto，具有 审计属性
    //手写方法不需要继承，dto只做数据转换

    public class GoodsDto:AuditedEntityDto<Guid>
    {
        public string GoodsName { get; set; }                           //商品名称
        public string GoodsDescribe { get; set; }                       //商品描述(富文本)
        public Nullable<System.Guid> ClassifiCationId { get; set; }            //分类Id
        public string GoodsStatus { get; set; }                                              //上下架状态
        public double GoodsPrice { get; set; }                                              //商品价格
        public double PreferentialPrice { get; set; }                                       //商品优惠价
        public Guid ProductDisplay { get; set; }   //逻辑删除
    }
}
