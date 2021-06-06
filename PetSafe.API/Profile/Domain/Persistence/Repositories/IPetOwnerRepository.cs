using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface IPetOwnerRepository
    {
        Task<IEnumerable<PetOwner>> ListAsync();
        Task<IEnumerable<PetOwner>> ListByPetIdAsync(int petId);
        Task<IEnumerable<PetOwner>> ListByOnwerIdAsync(int ownerId);
        Task<PetOwner> FindByPetIdAndOwnerId(int petId, int ownerId);
        Task AddAsync(PetOwner petOwner);
        void Remove(PetOwner petOwner);
        Task AssignPetOwner(int petId, int ownerId, bool principal);
        void UnassignPetOwner(int petId, int ownerId);
    }
}
