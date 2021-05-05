using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services
{
    public interface IPetIllnessService
    {
        Task<IEnumerable<PetIllness>> ListAsync();
        Task<IEnumerable<PetIllness>> ListByPetIdAsync(int petId);
        Task<IEnumerable<PetIllness>> ListByIllnessIdAsync(int illnessId);
        Task<PetIllnessResponse> AssignPetIllness(int petId, int illnessId);
        Task<PetIllnessResponse> UnassignPetIllness(int petId, int illnessId);
    }
}
