using System;
using System.Collections.Generic;
using System.Text;
using LivestreamingE_commercePlatform.Localization;
using Volo.Abp.Application.Services;

namespace LivestreamingE_commercePlatform
{
    /* Inherit your application services from this class.
     */
    public abstract class LivestreamingE_commercePlatformAppService : ApplicationService
    {
        protected LivestreamingE_commercePlatformAppService()
        {
            LocalizationResource = typeof(LivestreamingE_commercePlatformResource);
        }
    }
}
