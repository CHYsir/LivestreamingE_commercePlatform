using LivestreamingE_commercePlatform.CCTVreg;
using LivestreamingE_commercePlatform.Models.Directbroadcastingroom;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace LivestreamingE_commercePlatform.CCTVregs
{
    public class LiveService : Volo.Abp.Application.Services.ApplicationService, ILiveService
    {

        private readonly IRepository<Room, Guid>  _repository;

        public LiveService(IRepository<Room, Guid> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public RoomDto Show()
        {
            //var items = await  _repository.GetListAsync();
            RoomDto roomdtolist = new RoomDto();
            return roomdtolist;
            //return ;
            //return items.Select(item => new RoomDto
            //{

            //}).ToList();
        }
    }
}