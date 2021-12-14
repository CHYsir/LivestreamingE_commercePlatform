using System;
using System.Collections.Generic;
using System.Text;

namespace LivestreamingE_commercePlatform.Users
{
    public class UserLoginDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string usersName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string usersPassword { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string usersPhone { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string ValidateCode { get; set; }

        /// <summary>
        /// 雪花Id
        /// </summary>
        public string snowflakeId { get; set; }
    }
}
