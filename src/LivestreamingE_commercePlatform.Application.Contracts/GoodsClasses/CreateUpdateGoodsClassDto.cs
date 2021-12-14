using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

//在.Contracts项目中，添加CreateUpdateMemberDto.cs，主要对DTO进行验证，由ABP框架自动验证.
namespace LivestreamingE_commercePlatform.GoodsClasses
{
    //定义了数据注释属性(如[Required])来定义属性的验证. DTO由ABP框架自动验证.
    public class CreateUpdateGoodsClassDto
    {
        [Required]
        public string ClassName { get; set; }                                                               //分类名称
        public Nullable<System.Guid> ClassPId { get; set; }                                     //父级Id
        public string ClassStatus { get; set; }                                                               //分类状态
        public Guid CategoryDelete { get; set; } //逻辑删除
    }
}
