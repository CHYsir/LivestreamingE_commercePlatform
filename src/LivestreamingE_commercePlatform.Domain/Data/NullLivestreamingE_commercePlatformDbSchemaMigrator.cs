using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace LivestreamingE_commercePlatform.Data
{
    /* This is used if database provider does't define
     * ILivestreamingE_commercePlatformDbSchemaMigrator implementation.
     */
    public class NullLivestreamingE_commercePlatformDbSchemaMigrator : ILivestreamingE_commercePlatformDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}