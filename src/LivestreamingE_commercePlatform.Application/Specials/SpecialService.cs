using Snowflake.Core;
using LivestreamingE_commercePlatform.Helper;
using LivestreamingE_commercePlatform.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq;

namespace LivestreamingE_commercePlatform.Specials
{
    public class SpecialService : ApplicationService, ISpecialService
    {
        public IDistributedCache Cache { get; set; }

        private readonly IRepository<User, Guid> _users;
        public SpecialService(IRepository<User, Guid> users)
        {
            _users = users;
        }

       //图片上传
        [HttpPost,Route("upload")]
        public string Upload(IFormFile file)
        {
            string rootdir = AppContext.BaseDirectory.Split(@"\bin\")[0];
            string fname = DateTime.Now.ToString("yyyyMMddHHmmssffff") + System.IO.Path.GetExtension(file.FileName);
            var path = rootdir + @"\Image\" + fname;
            using (System.IO.FileStream fs = System.IO.File.Create(path))
            {
                file.CopyTo(fs);
                fs.Flush();//清空文件流
            }
            string newFileName = "http://localhost:90/" + fname;
            return newFileName;//将能访问新文件的网址回传给前端
        }

        //给手机发送验证码
        [HttpGet]
        public string phoneValidateCode(string userPhone)
        {
            var data = _users.FirstOrDefault(x => x.Phone == userPhone);

            if (data == null)
            {
                return "手机号不存在";
            }

            //生成验证码
            string code = ValidateCode.CreateRandomCode(6);

            //生成短信验证码
            //string ret = CloudInfDemo.sendSmsCode(userPhone, 1, code);

            //保存验证码 设置过期时间
            Cache.Set("code" + userPhone, Encoding.UTF8.GetBytes(code), new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) //设置过期时间
            });

            //生成验证码 保存到数据库

            return "验证码回去成功请等待";
        }


        //验证码
        [HttpGet]
        public SpecialsHeiper<string> GetValidateCode()
        {

            //生成验证码
            string code = ValidateCode.CreateRandomCode(4);

            //生成 雪花id
            var worker = new IdWorker(1, 1);
            string id = worker.NextId().ToString();

            //保存验证码保存到redis中 设置过期时间
            //这里的 redis中的value需要的是byte[]类型 这里需要转化
            Cache.Set("code" + id, Encoding.UTF8.GetBytes(code), new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) //设置过期时间
            });

            //生成图片文件流
            var result = ValidateCode.CreateValidateGraphic(code);

            //把图片文件流转化Base64
            var Base = ("data:image/jpeg;base64," + Convert.ToBase64String(result));

            //data:image/jpeg;base64,
            return new SpecialsHeiper<string>
            {
                Id = id,
                Base = Base
            };
        }

    }
}
