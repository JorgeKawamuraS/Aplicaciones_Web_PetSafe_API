using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services
{
    public interface IPetTreatmentService
    {
        Task<IEnumerable<PetTreatment>> ListAsync();
        Task<IEnumerable<PetTreatment>> ListByPetIdAsync(int petId);
        Task<IEnumerable<PetTreatment>> ListByTreatmentIdAsync(int treatmentId);
        Task<IEnumerable<PetTreatment>> ListByDateAsync(DateTime date);
        Task<PetTreatmentResponse> AssignPetTreatmentAsync(int petId, int treatmentId, DateTime date);
        Task<PetTreatmentResponse> UnassignPetTreatmentAsync(int petId, int treatmentId, DateTime date);
    }
}
