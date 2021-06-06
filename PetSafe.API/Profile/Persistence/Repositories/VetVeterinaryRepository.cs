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
    public class VetVeterinaryRepository : BaseRepository, IVetVeterinaryRepository
    {
        public VetVeterinaryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(VetVeterinary vetVeterinary)
        {
            await _context.VetVeterinaries.AddAsync(vetVeterinary);
        }

        public async Task AssignVetVeterinary(int vetId, int veterinaryId)
        {
            VetVeterinary vetVeterinary = await FindByVetIdAndVeterinaryIdAsync(vetId,veterinaryId);
            if (vetVeterinary==null)
            {
                vetVeterinary = new VetVeterinary { VetId=vetId, VeterinaryId=veterinaryId };
                await AddAsync(vetVeterinary);
            }
        }

        public async Task<VetVeterinary> FindByVetIdAndVeterinaryIdAsync(int vetId, int veterinaryId)
        {
            return await _context.VetVeterinaries.FindAsync(veterinaryId,vetId);
        }

        public async Task<IEnumerable<VetVeterinary>> ListAsync()
        {
            return await _context.VetVeterinaries
                .Include(vv=> vv.VetProfile)
                .Include(vv => vv.VeterinaryProfile)
                .ToListAsync();
        }

        public async Task<IEnumerable<VetVeterinary>> ListByVeterinaryIdAsync(int veterinaryId)
        {
            return await _context.VetVeterinaries
                .Where(vv => vv.VeterinaryId==veterinaryId)
                .Include(vv => vv.VeterinaryProfile)
                .ToListAsync();
        }

        public async Task<IEnumerable<VetVeterinary>> ListByVetIdAsync(int vetId)
        {
            return await _context.VetVeterinaries
                .Where(vv => vv.VetId == vetId)
                .Include(vv => vv.VetProfile)
                .ToListAsync();
        }

        public void Remove(VetVeterinary vetVeterinary)
        {
            _context.VetVeterinaries.Remove(vetVeterinary);
        }

        public async void UnassignVetVeterinary(int vetId, int veterinaryId)
        {
            VetVeterinary vetVeterinary = await FindByVetIdAndVeterinaryIdAsync(vetId, veterinaryId);
            if (vetVeterinary != null)
            {
                Remove(vetVeterinary);
            }
        }
    }
}
