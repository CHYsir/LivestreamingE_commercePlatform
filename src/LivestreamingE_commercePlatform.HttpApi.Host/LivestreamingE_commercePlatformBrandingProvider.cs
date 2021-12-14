using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace LivestreamingE_commercePlatform
{
    [Dependency(ReplaceServices = true)]
    public class LivestreamingE_commercePlatformBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "LivestreamingE_commercePlatform";
    }
}
