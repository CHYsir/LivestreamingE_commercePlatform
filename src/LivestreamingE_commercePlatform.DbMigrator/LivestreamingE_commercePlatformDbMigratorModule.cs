using LivestreamingE_commercePlatform.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace LivestreamingE_commercePlatform.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(LivestreamingE_commercePlatformEntityFrameworkCoreModule),
        typeof(LivestreamingE_commercePlatformApplicationContractsModule)
        )]
    public class LivestreamingE_commercePlatformDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
