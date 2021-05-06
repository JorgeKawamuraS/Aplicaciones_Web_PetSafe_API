using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface IPetIllnessRepository
    {
        Task<IEnumerable<PetIllness>> ListAsync();
        Task<IEnumerable<PetIllness>> ListByPetIdAsync(int petId);
        Task<IEnumerable<PetIllness>> ListByIllnessIdAsync(int illnessId);
        Task<PetIllness> FindByPetIdAndIllnessId(int petId,int illnessId);
        Task AddAsync(PetIllness petIllness);
        void Remove(PetIllness petIllness);
        Task AssignPetIllness(int petId, int illnessId);
        void UnassignPetIllness(int petId, int illnessId);
    }
}
