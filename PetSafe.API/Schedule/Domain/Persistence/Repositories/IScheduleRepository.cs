using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface IScheduleRepository
    {
        Task AddAsync(Schedule schedule);
        Task<Schedule> FindById(int id);
        Task<IEnumerable<Schedule>> FindByProfileId(int profileId);
        void Update(Schedule schedule);
        void Remove(Schedule schedule);
    }
}
