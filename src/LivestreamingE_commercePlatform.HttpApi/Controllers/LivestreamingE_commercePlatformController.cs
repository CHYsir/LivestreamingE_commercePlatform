using LivestreamingE_commercePlatform.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace LivestreamingE_commercePlatform.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class LivestreamingE_commercePlatformController : AbpController
    {
        protected LivestreamingE_commercePlatformController()
        {
            LocalizationResource = typeof(LivestreamingE_commercePlatformResource);
        }
    }
}