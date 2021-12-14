using LivestreamingE_commercePlatform.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace LivestreamingE_commercePlatform
{
    [DependsOn(
        typeof(LivestreamingE_commercePlatformEntityFrameworkCoreTestModule)
        )]
    public class LivestreamingE_commercePlatformDomainTestModule : AbpModule
    {

    }
}