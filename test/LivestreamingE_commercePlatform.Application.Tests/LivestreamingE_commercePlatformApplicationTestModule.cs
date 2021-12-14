using Volo.Abp.Modularity;

namespace LivestreamingE_commercePlatform
{
    [DependsOn(
        typeof(LivestreamingE_commercePlatformApplicationModule),
        typeof(LivestreamingE_commercePlatformDomainTestModule)
        )]
    public class LivestreamingE_commercePlatformApplicationTestModule : AbpModule
    {

    }
}