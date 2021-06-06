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
    public class AppointmentRepository : BaseRepository, IAppointmentRepository
    {
        public AppointmentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
        }

        public async Task<Appointment> FindById(int appointmentId)
        {
            return await _context.Appointments.FindAsync(appointmentId);
        }

        public async Task<IEnumerable<Appointment>> ListByScheduleId(int scheduleId)
        {
            return await _context.Appointments
                .Where(a=>a.ScheduleId==scheduleId)
                .Include(a => a.Schedule)
                .ToListAsync();
        }

        public void Remove(Appointment appointment)
        {
            _context.Appointments.Remove(appointment);
        }

        public void Update(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
        }
    }
}
