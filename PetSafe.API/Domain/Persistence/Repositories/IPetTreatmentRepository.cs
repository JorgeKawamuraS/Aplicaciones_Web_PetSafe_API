using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface IPetTreatmentRepository
    {
        Task<IEnumerable<PetTreatment>> ListAsync();
        Task<IEnumerable<PetTreatment>> ListByPetIdAsync(int petId);
        Task<IEnumerable<PetTreatment>> ListByTreatmentIdAsync(int treatmentId);
        Task<IEnumerable<PetTreatment>> ListByDateAsync(DateTime date);
        Task<PetTreatment> FindByPetIdAndTreatmentIdAndDate(int petId, int treatmentId, DateTime date);
        Task AddAsync(PetTreatment petTreatment);
        void Remove(PetTreatment petTreatment);
        Task AssignPetTreatment(int petId, int treatmentId, DateTime date);
        void UnassignPetTreatment(int petId, int treatmentId, DateTime date);

    }
}
