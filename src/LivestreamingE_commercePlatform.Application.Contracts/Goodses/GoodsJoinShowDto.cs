
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace LivestreamingE_commercePlatform.Goodses
{
   public  class GoodsJoinShowDto : AuditedEntityDto<Guid>
    {
        public string GoodsName { get; set; }                           //商品名称
        public string GoodsDescribe { get; set; }                       //商品描述(富文本)
        public Nullable<System.Guid> ClassifiCationId { get; set; }            //分类Id
        public string GoodsStatus { get; set; }                                              //上下架状态
        public double GoodsPrice { get; set; }                                              //商品价格
        public double PreferentialPrice { get; set; }                                       //商品优惠价

        public string ClassName { get; set; }                                                               //分类名称
        public Nullable<System.Guid> ClassPId { get; set; }                                     //父级Id
        public string ClassStatus { get; set; }                                                               //分类状态

        public string ImgName { get; set; }                                             //图片名称
        public string ImgUrl { get; set; }                                                 //图片路径
        public Nullable<System.Guid> GoodsId { get; set; }                 //商品Id

        public Nullable<int> InventotyRemaining { get; set; }                           //库存剩余
        public Nullable<System.Guid> SpecificationsId { get; set; }                  //规格Id

        public string GoodsWeight { get; set; }                                         //商品重量（kg）
        public string GoodsColor { get; set; }                                           //商品颜色
        public string GoodsTaste { get; set; }                                            //商品口味



    }
}
