using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LivestreamingE_commercePlatform.Data;
using Volo.Abp.DependencyInjection;

namespace LivestreamingE_commercePlatform.EntityFrameworkCore
{
    public class EntityFrameworkCoreLivestreamingE_commercePlatformDbSchemaMigrator
        : ILivestreamingE_commercePlatformDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreLivestreamingE_commercePlatformDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the LivestreamingE_commercePlatformDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<LivestreamingE_commercePlatformDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
