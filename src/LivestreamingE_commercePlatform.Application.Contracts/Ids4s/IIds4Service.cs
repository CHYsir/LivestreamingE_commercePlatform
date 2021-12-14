using System.Threading.Tasks;

namespace LivestreamingE_commercePlatform.Ids4s
{
    /// <summary>
    /// ids4服务帮助类
    /// </summary>
    public interface IIds4Service
    {
        /// <summary>
        /// 通过用户名和密码获取Ids4的登录用户token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        public  Task<string> GetIdsTokenAsync(string userName,string userPwd);

    }
}
