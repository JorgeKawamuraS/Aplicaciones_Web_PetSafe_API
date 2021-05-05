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
    public class PetOwnerRepository : BaseRepository, IPetOwnerRepository
    {
        public PetOwnerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(PetOwner petOwner)
        {
            await _context.PetOwners.AddAsync(petOwner);
        }

        public async Task AssignPetOwner(int petId, int ownerId)
        {
            PetOwner petOwner = await FindByPetIdAndOwnerId(petId,ownerId);
            if (petOwner==null)
            {
                petOwner = new PetOwner { PetId=petId, OwnerId=ownerId};
                await AddAsync(petOwner);
            }
        }

        public async Task<PetOwner> FindByPetIdAndOwnerId(int petId, int ownerId)
        {
            return await _context.PetOwners.FindAsync(petId,ownerId);
        }

        public async Task<IEnumerable<PetOwner>> ListAsync()
        {
            return await _context.PetOwners
                .Include(po=>po.PetProfile)
                .Include(po=>po.OwnerProfile)
                .ToListAsync();
        }

        public async Task<IEnumerable<PetOwner>> ListByOnwerIdAsync(int ownerId)
        {
            return await _context.PetOwners
                .Where(po => po.OwnerId==ownerId)
                .Include(po => po.OwnerProfile)
                .ToListAsync();
        }

        public async Task<IEnumerable<PetOwner>> ListByPetIdAsync(int petId)
        {
            return await _context.PetOwners
                .Where(po => po.PetId == petId)
                .Include(po => po.PetProfile)
                .ToListAsync();
        }

        public void Remove(PetOwner petOwner)
        {
            _context.PetOwners.Remove(petOwner);
        }

        public async void UnassignPetOwner(int petId, int ownerId)
        {
            PetOwner petOwner = await FindByPetIdAndOwnerId(petId, ownerId);
            if (petOwner != null)
            {
                Remove(petOwner);
            }
        }
    }
}
