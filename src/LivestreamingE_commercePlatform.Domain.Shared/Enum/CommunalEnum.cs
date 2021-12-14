using System;
using System.Collections.Generic;
using System.Text;

namespace LivestreamingE_commercePlatform.Enum
{
    public  class CommunalEnum
    {
        /// <summary>
        /// 性别
        /// </summary>
        public enum Gender
        {
            man = 0, //男
            woman = 2 //女
        }

        /// <summary>
        /// 图片类别
        /// </summary>
        public enum Img
        {
            usersImg = 1, //用户头像
            productImg = 2,//产品图片
        }

        /// <summary>
        /// 登录返回状态
        /// </summary>
        public enum Loggingstatus
        {
            codeexpired = 0,//验证码过期
            codeerror = 10,//验证码错误
            usersnotexist = 20,//用户不存在
            successfully = 30,//登录成功
            wrongpassword = 40 //密码错误
        }

        /// <summary>
        /// 订单状态
        /// </summary>
        public enum OrderType
        {
            Placedanorder = 0, //已下单（未支付）0
            Tosendthegoods = 10, //待发货（已支付）10
            Shipped = 20, //已发货（待收货）20
            offthestocks = 30, //已完成（已确认）30
            ordercancellation = 40 //订单取消（订单关闭）40
        }

        /// <summary>
        /// 返回状态
        /// </summary>
        public enum returnstate
        {
            succeed = 5, //成功
            defeated = 10 //失败
        }
    }
}
