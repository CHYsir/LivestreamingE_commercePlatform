using Hangfire;
using LivestreamingE_commercePlatform.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace LivestreamingE_commercePlatform
{
    public class HangFireService : BackgroundService
    {
        private readonly IRepository<Goods, Guid>  _goods;
        public HangFireService(IRepository<Goods, Guid>  goods )
        {
             _goods =goods ;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            RecurringJob.AddOrUpdate(() => HangfireJob(), Cron.Minutely());
            return Task.CompletedTask;
        }

        public void HangfireJob()
        {
            //var product = new Product() { productName = "自动生成", productSalePrice = 100 };
            //_products.InsertAsync(product);
        }
    }
}
