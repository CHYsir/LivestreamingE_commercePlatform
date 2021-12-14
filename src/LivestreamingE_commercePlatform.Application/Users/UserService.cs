using LivestreamingE_commercePlatform.Enum;
using LivestreamingE_commercePlatform.Helper;
using LivestreamingE_commercePlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace LivestreamingE_commercePlatform.Users
{
    public class UserService : ApplicationService, IUserService
    {
        public IDistributedCache Cache { get; set; }

        private readonly IRepository<User, Guid> _users;

        public UserService(IRepository<User,Guid> users)
        {
            _users = users;
        }

        //显示
        [HttpGet]
        public PagedResultDto<UserDto> GetUsersDto(UserListDto usersList)
        {
            var data = (from a in _users
                        select new UserDto
                        {
                            Account = a.Account,
                            LogStatus = a.LogStatus
                        });

            var list = data.WhereIf(!string.IsNullOrWhiteSpace(usersList.usersName), x => x.Account.Contains(usersList.usersName)).ToList();

            int totalCount = list.Count();
            return new PagedResultDto<UserDto>(totalCount, list);
        }

        //注册
        [HttpPost]
        public int UsersRegister(CreateUpdateUserDto usersList)
        {
            var list = _users.FirstOrDefault(x => x.Phone == usersList.Phone);
            if (list != null)
            {
                return 0;   //手机号已存在
            }

            if(usersList.Account==null || usersList.Account == "")
            {
                usersList.Account = usersList.Account;
            }

            var data = _users.FirstOrDefault(x => x.Account == usersList.Account);

            if (data!=null)
            {
                return 10;  //用户名已存在
            }

            usersList.Password = StringHelper.MD5Hash(usersList.Password);

            var users = new User
            {
                Account = usersList.Account,
                Password = usersList.Password,
            };

            var i = _users.InsertAsync(users);
            return i != null ? 20 : 30;   //20添加成功，30添加失败
        }

        //账号和密码登录
        [HttpGet]
        public ResultT<int> UsersRegistration(UserLoginDto usersLogin)
        {
            //取值和转化 redis中的value是byte[]类型 我们要使用的是 string 就需要转化
            string code = Encoding.Default.GetString(Cache.Get("code" + usersLogin.snowflakeId));

            if (code == null || code == "")
            {
                return new ResultT<int>
                {
                    State = (int)CommunalEnum.Loggingstatus.codeexpired,
                    Message = "验证码过期"
                };//验证码过期
            }

            if (!string.IsNullOrEmpty(usersLogin.ValidateCode) && code.ToLower() != usersLogin.ValidateCode.ToLower())
            {
                return new ResultT<int>
                {
                    State = (int)CommunalEnum.Loggingstatus.codeerror,
                    Message = "验证码错误"
                };//验证码错误
            }

            var data = _users.FirstOrDefault(x => x.Account == usersLogin.usersName);

            if (data == null)
            {
                return new ResultT<int>
                {
                    State = (int)CommunalEnum.Loggingstatus.usersnotexist,
                    Message = "用户名不存在"
                }; //用户名不存在
            }

            if (data.Password == StringHelper.MD5Hash(usersLogin.usersPassword))
            {
                return new ResultT<int>
                {
                    State = (int)CommunalEnum.Loggingstatus.successfully,
                    Message = "登录成功",
                    Data = Convert.ToInt32(data.Id)
                }; //登录成功
            }

            else
            {
                return new ResultT<int>
                {
                    State = (int)CommunalEnum.Loggingstatus.wrongpassword,
                    Message = "密码错误"
                }; //密码错误
            }
        }


        //手机登录
        public ResultT<int> phonelogin(UserLoginDto usersLogin)
        {
            //取值
            string code = Encoding.Default.GetString(Cache.Get("PhoneCode" + usersLogin.usersPhone));

            if (code == "" || code == null)
            {
                return new ResultT<int>
                {
                    State = (int)CommunalEnum.Loggingstatus.codeexpired,
                    Message = "验证码过期"
                }; //验证码过期
            }
            if (code != usersLogin.ValidateCode)
            {
                return new ResultT<int>
                {
                    State = (int)CommunalEnum.Loggingstatus.codeerror,
                    Message = "验证码错误"
                }; //验证码错误
            }

            return new ResultT<int>
            {
                State = (int)CommunalEnum.Loggingstatus.successfully,
                Message = "登录成功"
            }; //登录成功

        }


    }
}
