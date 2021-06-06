using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> ListByScheduleId(int scheduleId);
        Task<AppointmentResponse> GetByIdAsync(int appointmentId);
        Task<AppointmentResponse> SaveAsync(int ownerId, int veterinaryId, int vetId, int petId, Appointment appointment);
        Task<AppointmentResponse> UpdateAsync(int appointmentId, Appointment appointment);
        Task<AppointmentResponse> DeleteAsync(int appointmentId);
    }
}
