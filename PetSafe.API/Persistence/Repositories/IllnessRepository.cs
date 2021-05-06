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
    public class IllnessRepository : BaseRepository, IIllnessRepository
    {
        public IllnessRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Illness illness)
        {
            await _context.Illnesses.AddAsync(illness);
        }

        public async Task<Illness> FindById(int id)
        {
            return await _context.Illnesses.FindAsync(id);
        }

        public async Task<IEnumerable<Illness>> ListAsync()
        {
            return await _context.Illnesses.ToListAsync();
        }

        public void Remove(Illness illness)
        {
            _context.Illnesses.Remove(illness);
        }

        public void Update(Illness illness)
        {
            _context.Illnesses.Update(illness);
        }
    }
}