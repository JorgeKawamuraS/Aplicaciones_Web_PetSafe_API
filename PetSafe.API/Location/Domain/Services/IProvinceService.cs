using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services
{
    public interface IProvinceService
    {
        Task<IEnumerable<Province>> ListAsync();
        Task<ProvinceResponse> GetByIdAsync(int id);
        Task<ProvinceResponse> SaveAsync(Province province);
        Task<ProvinceResponse> UpdateAsync(int id,Province province);
        Task<ProvinceResponse> DeleteAsync(int id);

    }
}
