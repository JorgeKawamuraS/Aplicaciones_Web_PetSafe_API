using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> ListByScheduleId(int scheduleId);
        Task<Appointment> FindById(int appointmentId);
        Task AddAsync(Appointment appointment);
        void Update(Appointment appointment);
        void Remove(Appointment appointment);

    }
}
