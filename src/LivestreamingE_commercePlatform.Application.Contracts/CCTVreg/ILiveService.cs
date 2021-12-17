using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LivestreamingE_commercePlatform.CCTVreg
{
    public    interface ILiveService: Volo.Abp.Application.Services.IApplicationService
    {
       RoomDto Show();
    } 
}
