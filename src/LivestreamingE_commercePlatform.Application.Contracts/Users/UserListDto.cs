using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace LivestreamingE_commercePlatform.Users
{
    public class UserListDto : PagedAndSortedResultRequestDto
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
        /// 验证码
        /// </summary>
        public string ValidateCode { get; set; }
    }
}
