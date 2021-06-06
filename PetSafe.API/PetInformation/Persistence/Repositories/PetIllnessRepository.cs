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
    public class PetIllnessRepository : BaseRepository, IPetIllnessRepository
    {
        public PetIllnessRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(PetIllness petIllness)
        {
            await _context.PetIllnesses.AddAsync(petIllness);
        }

        public async Task AssignPetIllness(int petId, int illnessId)
        {
            PetIllness petIllness = await _context.PetIllnesses.FindAsync(petId,illnessId);
            if (petIllness==null)
            {
                petIllness = new PetIllness { PetId = petId, IllnesstId = illnessId };
                await AddAsync(petIllness);
            }
        }

        public async Task<PetIllness> FindByPetIdAndIllnessId(int petId, int illnessId)
        {
            return await _context.PetIllnesses.FindAsync(illnessId,petId);
        }

        public async Task<IEnumerable<PetIllness>> ListAsync()
        {
            return await _context.PetIllnesses
                .Include(pi=>pi.Illness)
                .Include(pi => pi.PetProfile)
                .ToListAsync();
        }

        public async Task<IEnumerable<PetIllness>> ListByIllnessIdAsync(int illnessId)
        {
            return await _context.PetIllnesses
                .Where(pi => pi.IllnesstId==illnessId)
                .Include(pi => pi.Illness)
                .ToListAsync();
        }

        public async Task<IEnumerable<PetIllness>> ListByPetIdAsync(int petId)
        {
            return await _context.PetIllnesses
                .Where(pi => pi.PetId == petId)
                .Include(pi => pi.PetProfile)
                .ToListAsync();
        }

        public void Remove(PetIllness petIllness)
        {
           _context.PetIllnesses.Remove(petIllness);
        }

        public async void UnassignPetIllness(int petId, int illnessId)
        {
            PetIllness petIllness = await _context.PetIllnesses.FindAsync(illnessId, petId);
            if (petIllness != null)
            {
                Remove(petIllness);
            }
        }
    }
}
