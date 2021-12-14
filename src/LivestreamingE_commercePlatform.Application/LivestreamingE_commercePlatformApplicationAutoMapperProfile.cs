using AutoMapper;
using LivestreamingE_commercePlatform.GoodsClasses;
using LivestreamingE_commercePlatform.Goodses;
using LivestreamingE_commercePlatform.GoodsImgs;
using LivestreamingE_commercePlatform.GoodsInventorys;
using LivestreamingE_commercePlatform.Models;

namespace LivestreamingE_commercePlatform
{
    public class LivestreamingE_commercePlatformApplicationAutoMapperProfile : Profile
    {
        public LivestreamingE_commercePlatformApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            //商品分类，元组(根源)映射到dto(显示)
            CreateMap<GoodsClass, GoodsClassDto>();
            //dto到数据库(添加、修改)
            CreateMap<CreateUpdateGoodsClassDto, GoodsClass>();

            CreateMap<Goods, GoodsDto>();
            CreateMap<GoodsDto, Goods>();

        }
    }
}
