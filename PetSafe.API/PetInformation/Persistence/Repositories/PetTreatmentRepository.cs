using Microsoft.EntityFrameworkCore;
using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Persistence.Context;
using PetSafe.API.Domain.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Persistence.Repositories
{
    public class PetTreatmentRepository : BaseRepository, IPetTreatmentRepository
    {
        public PetTreatmentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(PetTreatment petTreatment)
        {
            await _context.PetTreatments.AddAsync(petTreatment);
        }

        public async Task AssignPetTreatment(int petId, int treatmentId, DateTime date)
        {
            PetTreatment petTreatment = await FindByPetIdAndTreatmentIdAndDate(petId, treatmentId, date);
            if (petTreatment==null)
            {
                petTreatment = new PetTreatment { PetId=petId, TreatmentId=treatmentId, Date=date };
                await AddAsync(petTreatment);
            }
        }

        public async Task<PetTreatment> FindByPetIdAndTreatmentIdAndDate(int petId, int treatmentId, DateTime date)
        {
            return await _context.PetTreatments.FindAsync(petId,treatmentId,date);
        }

        public async Task<IEnumerable<PetTreatment>> ListAsync()
        {
            return await _context.PetTreatments
                .Include(pt=> pt.PetProfile)
                .Include(pt=> pt.Treatment)
                .ToListAsync();
        }

        public async Task<IEnumerable<PetTreatment>> ListByDateAsync(DateTime date)
        {
            return await _context.PetTreatments
                .Where(pt=>pt.Date==date)
                .Include(pt => pt.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<PetTreatment>> ListByPetIdAsync(int petId)
        {
            return await _context.PetTreatments
                .Where(pt => pt.PetId == petId)
                .Include(pt => pt.PetProfile)
                .ToListAsync();
        }

        public async Task<IEnumerable<PetTreatment>> ListByTreatmentIdAsync(int treatmentId)
        {
            return await _context.PetTreatments
                .Where(pt => pt.TreatmentId == treatmentId)
                .Include(pt => pt.Treatment)
                .ToListAsync();
        }

        public void Remove(PetTreatment petTreatment)
        {
            _context.PetTreatments.Remove(petTreatment);
        }

        public async void UnassignPetTreatment(int petId, int treatmentId, DateTime date)
        {
            PetTreatment petTreatment = await FindByPetIdAndTreatmentIdAndDate(petId, treatmentId, date);
            if (petTreatment != null)
            {
                Remove(petTreatment);
            }
        }
    }
}
