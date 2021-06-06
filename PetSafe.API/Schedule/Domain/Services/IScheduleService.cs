using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface IScheduleService
    {
        Task<ScheduleResponse> GetByIdAsync(int scheduleId);
        Task<ScheduleResponse> GetByProfileIdAsync(int profileId);
        Task<ScheduleResponse> SaveAsync(int profileId);
    }
}
