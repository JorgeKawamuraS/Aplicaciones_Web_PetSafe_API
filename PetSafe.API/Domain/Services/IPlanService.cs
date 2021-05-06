using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services
{
    public interface IPlanService
    {
        Task<IEnumerable<Plan>> ListAsync();
        Task<IEnumerable<Plan>> ListByUserIdAsync(int userId);
        Task<PlanResponse> GetByIdAsync(int id);
        Task<PlanResponse> SaveAsync(Plan plan);
        Task<PlanResponse> UpdateAsync(int id,Plan plan);
        Task<PlanResponse> DeleteAsync(int id);

    }
}
