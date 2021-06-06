using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services
{
    public interface IPetOwnerService
    {
        Task<IEnumerable<PetOwner>> ListAsync();
        Task<IEnumerable<PetOwner>> ListByPetIdAsync(int petId);
        Task<IEnumerable<PetOwner>> ListByOnwerIdAsync(int ownerId);
        Task<PetOwnerResponse> AssingPetOwner(int petId, int ownerId, bool principal);
        Task<PetOwnerResponse> UnassingPetOwner(int petId, int ownerId);
    }
}
