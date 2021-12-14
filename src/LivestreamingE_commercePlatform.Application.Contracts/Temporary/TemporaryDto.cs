using System;
using System.Collections.Generic;
using System.Text;

namespace LivestreamingE_commercePlatform.Temporary
{
    public  class TemporaryDto
    {
        //商品
        public Guid Id { get; set; }                                             //商品Id
        public string GoodsName { get; set; }                           //商品名称
        public string GoodsDescribe { get; set; }                       //商品描述(富文本)
        public Nullable<System.Guid> ClassifiCationId { get; set; }            //分类Id
        public string GoodsStatus { get; set; }                                              //上下架状态
        public double GoodsPrice { get; set; }                                              //商品价格
        public double PreferentialPrice { get; set; }                                       //商品优惠价
        public Guid ProductDisplay { get; set; }   //逻辑删除


        //商品分类
        public string ClassName { get; set; }                                                               //分类名称
        public Nullable<System.Guid> ClassPId { get; set; }                                     //父级Id
        public string ClassStatus { get; set; }                                                               //分类状态
        public Guid CategoryDelete { get; set; } //逻辑删除

        //商品图片
        public string ImgName { get; set; }                                             //图片
        public string ImgUrl { get; set; }                                                 //图片路径
        public Nullable<System.Guid> GoodsId { get; set; }                 //商品Id
        public Guid Imagedelete { get; set; } //逻辑删除

        //商品规格
        //public Nullable<System.Guid> GoodssId { get; set; }                   //商品Id(为避免冲突，改GoodsId为GoodssId)
        public string GoodsWeight { get; set; }                                         //商品重量（kg）
        public string GoodsColor { get; set; }                                           //商品颜色
        public string GoodsTaste { get; set; }                                            //商品口味

        //商品库存
        public Nullable<int> InventotyRemaining { get; set; }                           //库存剩余
        public Nullable<System.Guid> SpecificationsId { get; set; }                  //规格Id

    }
}
