using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;


namespace LivestreamingE_commercePlatform.Models
{
    //秒杀
    public  class Seckill :AuditedAggregateRoot<Guid>
    {
        public string seckillName { get; set; }     //秒杀名称
        public Guid productId { get; set; }          //商品id
        public int seckillSatae { get; set; }       //秒杀是否开启
        public decimal seckillPrice { get; set; }   //秒杀价格
        public int seckillSum { get; set; }         //秒杀数量
        public int seckillastrict { get; set; }     //限制每人购买数量
        public DateTime seckillBegin { get; set; }  //开始时间
        public DateTime seckillEnd { get; set; }    //结束时间
        public int seckillWeight { get; set; }      //权重
    }
}
