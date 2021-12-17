using Microsoft.AspNetCore.Mvc;
using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace LivestreamingE_commercePlatform.Uploads
{
    //文件上传七牛云
    public   class QiniuService:ApplicationService
    {
        [HttpPost,Route("uploadqiniu")]
        public string UploadQiniu(Microsoft.AspNetCore.Http.IFormFile file)
        {
            string AccessKey = "284F_taC0W2aj-wk0ohoQQUJ4DDhK33BeUbBZJtx";
            string SecretKey = "dnwisRaAyxxPv03FlfJil-Q9CK2SV2wq6VDYrMwJ";
            string Bucket = "chy1216";
            Mac mac = new Mac(AccessKey, SecretKey);
            //获取根目录
            var rootdir = AppContext.BaseDirectory;
            //给上传文件 改了个名 根据当前时间
            string key = DateTime.Now.ToString("yyyyMMddHHmmssffff") + Path.GetExtension(file.FileName);
            //拼接 绝对路径+文件名
            string path = rootdir + @"\Image\" + key;
            using (FileStream fl = File.Create(path))
            {
                file.CopyTo(fl);
                fl.Flush();
            }
            //实例化一个上传策略
            PutPolicy putPolicy = new PutPolicy();
            // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY" 七牛存储名加传进来的文件名 如: bucket + ":" + saveKey
            putPolicy.Scope = Bucket; // 
            //上传策略有效期 我这边是三天
            putPolicy.SetExpires(7200);
            // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除 一天删除
            putPolicy.DeleteAfterDays = 1;
            //完整的文件名 (绝对路径加文件名)
            string loafile = path;
            //根据 ak sk 加密生成的toten
            string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
            //实例化配置类 配置一些我们需要的东西 如:所属地区 过期时间 是否CDN加速
            Config config = new Config();
            // 设置上传区域 我这里是华东 ZONE_CN_East 其他区域见官网
            config.Zone = Zone.ZONE_CN_East;
            //使用前请确保AK和BUCKET正确，否则此函数会抛出异常(比如code612 / 631等错误)
            config.ApiHost(AccessKey, Bucket);
            // 设置 http 或者 https 上传
            config.UseHttps = true;
            //CDN加速
            config.UseCdnDomains = true;
            //上传文件的大小
            config.ChunkSize = ChunkUnit.U512K;
            //实例化一个上传管理类 七牛云 自带
            UploadManager um = new UploadManager(config);
            //发起请求 至七牛云 存储 图片的本地路径(loafile) ,图片文件名(key)
            HttpResult result = um.UploadFile(loafile, key, token, null);
            #region 下载文件
            //下载保存在云端的 文件 
            //HttpResult result1 = DownloadManager.Download("http://kwb.lcvue.com/202112171136224363.jpeg", rootdir);
            //if (result1.Code==200)
            //{
            //    return "下载成功";
            //}
            //else
            //{
            //    return "出现了错误";
            //}
            #endregion
            // return result;
            //返回 云端文件 url;//http://kwb.lcvue.com/ 该地址不会变 key 是我们的文件名

            string BucketUrl = "http://kwb.lcvue.com/";
            var Url = BucketUrl + key;
            if (result.Code == 200)
            {
                return Url;
            }
            else
            {
                return "似乎遇到了点小问题";
            }
        }

    }
}
