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
    public class VeterinarySpecialtyRepository : BaseRepository, IVeterinarySpecialtyRepository
    {
        public VeterinarySpecialtyRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(VeterinarySpecialty veterinarySpecialty)
        {
            await _context.VeterinarySpecialties.AddAsync(veterinarySpecialty);
        }

        public async Task AssignVeterinarySpecialty(int veterinaryId, int specialtyId)
        {
            VeterinarySpecialty veterinarySpecialty = await FindByVeterinaryIdAndSpecialtyId(veterinaryId,specialtyId);
            if (veterinarySpecialty==null)
            {
                veterinarySpecialty = new VeterinarySpecialty { VeterinaryId=veterinaryId,SpecialtyId=specialtyId };
                await AddAsync(veterinarySpecialty);
            }
        }

        public async Task<VeterinarySpecialty> FindByVeterinaryIdAndSpecialtyId(int veterinaryId, int specialtyId)
        {
            return await _context.VeterinarySpecialties.FindAsync(veterinaryId,specialtyId);
        }

        public async Task<IEnumerable<VeterinarySpecialty>> ListAsync()
        {
            return await _context.VeterinarySpecialties
                .Include(vs=>vs.Specialty)
                .Include(vs => vs.VeterinaryProfile)
                .ToListAsync();
        }

        public async Task<IEnumerable<VeterinarySpecialty>> ListBySpecialtyIdAsync(int specialtyId)
        {
            return await _context.VeterinarySpecialties
                .Where(vs=>vs.SpecialtyId==specialtyId)
                .Include(vs => vs.Specialty)
                .ToListAsync();
        }

        public async Task<IEnumerable<VeterinarySpecialty>> ListByVeterinaryIdAsync(int veterinaryId)
        {
            return await _context.VeterinarySpecialties
                .Where(vs => vs.VeterinaryId== veterinaryId)
                .Include(vs => vs.VeterinaryProfile)
                .ToListAsync();
        }

        public void Remove(VeterinarySpecialty veterinarySpecialty)
        {
            _context.VeterinarySpecialties.Remove(veterinarySpecialty);
        }

        public async void UnassignVeterinarySpecialty(int veterinaryId, int specialtyId)
        {
            VeterinarySpecialty veterinarySpecialty = await FindByVeterinaryIdAndSpecialtyId(veterinaryId, specialtyId);
            if (veterinarySpecialty != null)
            {
                Remove(veterinarySpecialty);
            }
        }
    }
}
