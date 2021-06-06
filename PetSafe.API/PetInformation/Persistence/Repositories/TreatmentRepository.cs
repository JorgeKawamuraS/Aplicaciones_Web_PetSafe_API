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
    public class TreatmentRepository : BaseRepository, ITreatmentRepository
    {
        public TreatmentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Treatment treatment)
        {
            await _context.Treatments.AddAsync(treatment);
        }

        public async Task<Treatment> FindById(int id)
        {
            return await _context.Treatments.FindAsync(id);
        }

        public async Task<IEnumerable<Treatment>> ListAsync()
        {
            return await _context.Treatments.ToListAsync();
        }

        public void Remove(Treatment treatment)
        {
            _context.Treatments.Remove(treatment);
        }

        public void Update(Treatment treatment)
        {
            _context.Treatments.Update(treatment);
        }
    }
}
