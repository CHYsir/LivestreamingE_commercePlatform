using IdentityModel.Client;
using LivestreamingE_commercePlatform.Ids4s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace LivestreamingE_commercePlatform.Ids4
{
    /// <summary>
    /// ids4服务注册类
    /// </summary>
    public class Ids4Service : Volo.Abp.Application.Services.ApplicationService, IIds4Service
    {
        private IConfiguration _configuration;
        public Ids4Service(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        /// <summary>
        /// 获取系统token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        public async Task<string> GetIdsTokenAsync(string userName, string userPwd)
        {
            var client = new HttpClient();
            var idsTokenUrl = this._configuration.GetSection("AuthServer:Authority").Value;
            var AppClientId = this._configuration.GetSection("AuthServer:AppClientId").Value;
            var AppClientSecret = this._configuration.GetSection("AuthServer:AppClientSecret").Value;
            var disco = client.GetDiscoveryDocumentAsync(idsTokenUrl);

            var tokenResponse = await client.RequestPasswordTokenAsync(
                new PasswordTokenRequest
                {
                    Address = disco.Result.TokenEndpoint,
                    ClientId = AppClientId,
                    ClientSecret = AppClientSecret,
                    UserName = userName,
                    Password = userPwd
                }
                );

            if (tokenResponse.IsError)
            {
                return string.Empty;
            }
            return tokenResponse.AccessToken;
        }
    }
}
