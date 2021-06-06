using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Persistence.Repositories;
using PetSafe.API.Domain.Services;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IOwnerProfileRepository _ownerProfileRepository;
        private readonly IPetProfileRepository _petProfileRepository;
        private readonly IVeterinaryProfileRepository _veterinaryProfileRepository;
        private readonly IVetProfileRepository _vetProfileRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IOwnerProfileRepository ownerProfileRepository, IPetProfileRepository petProfileRepository, 
            IVeterinaryProfileRepository veterinaryProfileRepository, IVetProfileRepository vetProfileRepository, 
            IScheduleRepository scheduleRepository, IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork)
        {
            _ownerProfileRepository = ownerProfileRepository;
            _petProfileRepository = petProfileRepository;
            _veterinaryProfileRepository = veterinaryProfileRepository;
            _vetProfileRepository = vetProfileRepository;
            _scheduleRepository = scheduleRepository;
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AppointmentResponse> DeleteAsync(int appointmentId)
        {
            var existingAppointment = await _appointmentRepository.FindById(appointmentId);
            if (existingAppointment==null)
            {
                return new AppointmentResponse("Appointment not found");
            }
            try
            {
                _appointmentRepository.Remove(existingAppointment);
                await _unitOfWork.CompleteAsync();

                return new AppointmentResponse(existingAppointment);
            }
            catch (Exception ex)
            {
                return new AppointmentResponse($"An error ocurred while deleting appointment: {ex.Message}");
            }
        }

        public async Task<AppointmentResponse> GetByIdAsync(int appointmentId)
        {
            var existingAppointment = await _appointmentRepository.FindById(appointmentId);
            if (existingAppointment == null)
            {
                return new AppointmentResponse("Appointment not found");
            }
            return new AppointmentResponse(existingAppointment);
        }

        public async Task<IEnumerable<Appointment>> ListByScheduleId(int scheduleId)
        {
            return await _appointmentRepository.ListByScheduleId(scheduleId);
        }

        public async Task<AppointmentResponse> SaveAsync(int ownerId, int veterinaryId, int vetId, int petId, Appointment appointment)
        {
            var existingOwner = await _ownerProfileRepository.FindById(ownerId);
            var existingVeterinary = await _veterinaryProfileRepository.FindById(veterinaryId);
            var existingVet = await _vetProfileRepository.FindById(vetId);
            var existingPet = await _petProfileRepository.FindById(petId);
            var existingScheduleVet = await _scheduleRepository.FindByProfileId(veterinaryId);
            var existingScheduleOwner = await _scheduleRepository.FindByProfileId(ownerId);
            if (existingOwner==null)
            {
                return new AppointmentResponse("Owner not found");
            }
            if (existingPet == null)
                return new AppointmentResponse("Pet not found");
            if (existingVet == null)
                return new AppointmentResponse("Vet not found");
            if (existingVeterinary == null)
                return new AppointmentResponse("Veterinary not found");
            try
            {
                bool differentDate = true;
                IEnumerable<Appointment> appointments = await _appointmentRepository.ListByScheduleId(existingScheduleVet.First().Id);
                appointments.ToList().ForEach(appoint => {
                    if ((appoint.Date==appointment.Date) && appoint.Accepted==true)
                        differentDate = false;
                });
                if (differentDate == false)
                    return new AppointmentResponse("The vet already have an appointment at the date, please try a different date");

                appointment.OwnerId = ownerId;
                appointment.VeterinaryId = veterinaryId;
                appointment.VetId = vetId;
                appointment.PetId = petId;
                appointment.ScheduleId = existingScheduleOwner.First().Id;
                await _appointmentRepository.AddAsync(appointment);
                await _unitOfWork.CompleteAsync();
                appointment.ScheduleId = existingScheduleVet.First().Id;
                await _appointmentRepository.AddAsync(appointment);
                await _unitOfWork.CompleteAsync();

                return new AppointmentResponse(appointment);
            }
            catch (Exception ex)
            {
                return new AppointmentResponse($"An error ocurred while updating appointment: {ex.Message}");
            }
        }

        public async Task<AppointmentResponse> UpdateAsync(int appointmentId, Appointment appointment)
        {
            var existingAppointment = await _appointmentRepository.FindById(appointmentId);
            if (existingAppointment == null)
            {
                return new AppointmentResponse("Appointment not found");
            }
            existingAppointment.Accepted = appointment.Accepted;
            existingAppointment.Date = appointment.Date;
            existingAppointment.Description = appointment.Description;
            existingAppointment.Virtual = appointment.Virtual;
            try
            {
                IEnumerable<Appointment> appointments = await _appointmentRepository.ListByScheduleId(existingAppointment.ScheduleId);
                bool differentDate = true;
                if(appointments!=null)
                appointments.ToList().ForEach(appointment => {
                    if ((appointment.Date == existingAppointment.Date) && appointment.Accepted == true && appointment.Accepted==true)
                        differentDate = false;
                });

                if(!differentDate)
                    return new AppointmentResponse("Ya existe otra cita a la misma hora");


                _appointmentRepository.Update(existingAppointment);
                await _unitOfWork.CompleteAsync();

                return new AppointmentResponse(existingAppointment);
            }
            catch (Exception ex)
            {
                return new AppointmentResponse($"An error ocurred while updating appointment: {ex.Message}");
            }
        }
    }
}
