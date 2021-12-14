using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace LivestreamingE_commercePlatform.Specials
{
    public  interface ISpecialService: IApplicationService
    {
        /// <summary>
        /// 上图上传
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        string Upload(IFormFile file);

        ///// <summary>
        ///// 生成验证码
        ///// </summary>
        ///// <returns></returns>
        //SpecialsHeiper<string> GetValidateCode();

        /// <summary>
        /// 生成手机号验证码
        /// </summary>
        /// <param name="userPhone"></param>
        /// <returns></returns>
        string phoneValidateCode(string userPhone);
    }
}
