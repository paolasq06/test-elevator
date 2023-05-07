using Application.Core.Exceptions;
using Application.Interfaces;
using Core.Enums;
using Domain.Interfaces;
using Domain.Models.Elevator;
using EasyCaching.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FloorService : IFloorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private const string KEYREDISQUERY = "FloorService";
        private readonly IDistributedCache _distributedCache;

        public FloorService(IUnitOfWork unitOfWork, IDistributedCache distributedCache)
        {
            _unitOfWork = unitOfWork;
            _distributedCache = distributedCache;
        }

        public async Task<bool> CheckByFloorIdAndElevatorId(int floorId, int elevatorId)
        {
            string key = String.Concat(KEYREDISQUERY, floorId, elevatorId);
            var cachingSteps = await _distributedCache.GetAsync(key);
            if (cachingSteps != null)
            {
                string serializedCheck= Encoding.UTF8.GetString(cachingSteps);

                return JsonConvert.DeserializeObject<bool>(serializedCheck);
            }
            var checkedFloor = await _unitOfWork.floor
                .Get()
                .CountAsync(x => x.Number == floorId && x.ElevatorId == elevatorId) == 0 ?
                false : true;
            var serilized = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(checkedFloor));
            await _distributedCache.SetAsync(key, serilized);


            return checkedFloor;

        }
    }
}
