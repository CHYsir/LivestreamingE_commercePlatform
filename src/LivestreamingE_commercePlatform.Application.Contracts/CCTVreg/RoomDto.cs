using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace LivestreamingE_commercePlatform.CCTVreg
{
    public  class RoomDto: AuditedEntityDto<Guid>
    {
        public string RoomTitle { get; set; }    //直播间标题
        public string RoomDesc { get; set; }   //直播间描述/简介
        public string ChatroomId { get; set; } //聊天室Id   这个房间号，其实就是拼接到推流和拉流地址里面的StreamName，这个StreamName不需要手动去腾讯云注册，直接拼接在地址中使用就好，注意不同直播间的StreamName不能相同哈，如果你的推流地址中的StreamName相同，那么只有第一个开始推流的是有效的，也就是说同样的推流地址，谁先推谁的有效，拉流只能拉到第一个推流的那个

        public string PushUrl { get; set; } = "rtmp://push.iot.lcvue.com/live/test_1?txSecret=151c0584d6278f0aec9930c5e9ea9816&txTime=61C02B93";                 //推流地址
        public string FlvUrl { get; set; } = "rtmp://broad.iot.lcvue.com/live/test_1";        //拉流地址
        public string Status { get; set; }          //直播状态：0——初始化   1——直播中    2——已结束
        public DateTime CreateTime { get; set; } = DateTime.Now;   //创建时间
    }
}
