using System;
using System.Collections.Generic;
using System.Text;

namespace LivestreamingE_commercePlatform.Users
{
    public class CreateUpdateUserDto
    {
        public string Account { get; set; }      //登录名
        public string Password { get; set; }    //登录密码(MD5加密)
        public string LogStatus { get; set; }   //登录状态
        public Nullable<System.DateTime> LogTime { get; set; }   //登录时间
        public Nullable<System.DateTime> OutTime { get; set; }   //退出时间
        public string Phone { get; set; }         //手机号登录

        public DateTime UserslockLoginDate { get; set; } = DateTime.Now.AddDays(+2);      //锁定时间

        public bool UsersisDelete { get; set; } = false;     //是否锁定

        public int UsersSum { get; set; } = 0;                   //密码错误次数
    }
}
