using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LivestreamingE_commercePlatform.Users
{
    public interface IUserService:IApplicationService
    {
        ////用户密码登录
        //int UsersRegistration(UserLoginDto usersLogin);

        ////手机号登录
        //int phonelogin(UserLoginDto usersLogin);

        //注册
        int UsersRegister(CreateUpdateUserDto usersList);


        /// 显示+查询+分页
        PagedResultDto<UserDto> GetUsersDto(UserListDto usersList);

       
    }
}
