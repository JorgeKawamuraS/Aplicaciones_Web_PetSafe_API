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
    public class RecordatoryService : IRecordatoryService
    {
        private readonly IVetProfileRepository _vetProfileRepository;
        private readonly IOwnerProfileRepository _ownerProfileRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IRecordatoryTypeRepository _recordatoryTypeRepository;
        private readonly IPetProfileRepository _petProfileRepository;
        private readonly IRecordatoryRepository _recordatoryRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RecordatoryService(IVetProfileRepository vetProfileRepository, IOwnerProfileRepository ownerProfileRepository,
            IScheduleRepository scheduleRepository, IRecordatoryTypeRepository recordatoryTypeRepository,
            IPetProfileRepository petProfileRepository, IRecordatoryRepository recordatoryRepository, 
            IUnitOfWork unitOfWork, IAppointmentRepository appointmentRepository)
        {
            _vetProfileRepository = vetProfileRepository;
            _ownerProfileRepository = ownerProfileRepository;
            _scheduleRepository = scheduleRepository;
            _recordatoryTypeRepository = recordatoryTypeRepository;
            _petProfileRepository = petProfileRepository;
            _recordatoryRepository = recordatoryRepository;
            _unitOfWork = unitOfWork;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<RecordatoryResponse> DeleteAsync(int recordatoryId)
        {
            var existingRecordatory = await _recordatoryRepository.FindById(recordatoryId);
            if (existingRecordatory==null)
            {
                return new RecordatoryResponse("Recordatory not found");
            }
            try
            {
                _recordatoryRepository.Remove(existingRecordatory);
                await _unitOfWork.CompleteAsync();

                return new RecordatoryResponse(existingRecordatory);
            }
            catch (Exception ex)
            {
                return new RecordatoryResponse($"An error ocurred while deleting recordatory: {ex.Message}");
            }
        }

        public async Task<RecordatoryResponse> GetByIdAsync(int recordatoryId)
        {
            var existingRecordatory = await _recordatoryRepository.FindById(recordatoryId);
            if (existingRecordatory == null)
            {
                return new RecordatoryResponse("Recordatory not found");
            }
            return new RecordatoryResponse(existingRecordatory);
        }

        public async Task<IEnumerable<Recordatory>> ListByRecordatoryTypeId(int recordatoryTypeId)
        {
            return await _recordatoryRepository.ListByRecordatoryTypeId(recordatoryTypeId);
        }

        public async Task<IEnumerable<Recordatory>> ListByScheduleId(int scheduleId)
        {
            return await _recordatoryRepository.ListByScheduleId(scheduleId);
        }

        public async Task<RecordatoryResponse> SaveOwnerAsync(int ownerId, int scheduleId, int recordyTypeId, int petId, Recordatory recordatory)
        {
            var existingOwner = await _ownerProfileRepository.FindById(ownerId);
            var existingSchedule = await _scheduleRepository.FindById(scheduleId);
            var existingRecordatoryType = await _recordatoryTypeRepository.FindById(recordyTypeId);
            var existingPet = await _petProfileRepository.FindById(petId);
            if (existingOwner == null)
                return new RecordatoryResponse("Owner not found");
            if (existingSchedule == null)
                return new RecordatoryResponse("Schedule not found");
            if (existingRecordatoryType == null)
                return new RecordatoryResponse("Recordatory Type not found");
            if (existingPet == null)
                return new RecordatoryResponse("Pet not found");
            try
            {
                recordatory.OwnerId = ownerId;
                recordatory.PetId = petId;
                recordatory.ScheduleId = scheduleId;
                recordatory.RecordatoryTypeId = recordyTypeId;
                recordatory.VetId = 0;
                await _recordatoryRepository.AddAsync(recordatory);
                await _unitOfWork.CompleteAsync();

                return new RecordatoryResponse(recordatory);
            }
            catch (Exception ex)
            {
                return new RecordatoryResponse($"An error ocurred while saving recordatory: {ex.Message}");
            }
        }

        public async Task<RecordatoryResponse> SaveVetAsync(int vetId, int ownerId, int scheduleId, int recordyTypeId, int petId, Recordatory recordatory)
        {
            var existingOwner = await _ownerProfileRepository.FindById(ownerId);
            var existingSchedule = await _scheduleRepository.FindById(scheduleId);
            var existingRecordatoryType = await _recordatoryTypeRepository.FindById(recordyTypeId);
            var existingPet = await _petProfileRepository.FindById(petId);
            var existingVet = await _vetProfileRepository.FindById(vetId);
            if (existingOwner == null)
                return new RecordatoryResponse("Owner not found");
            if (existingSchedule == null)
                return new RecordatoryResponse("Schedule not found");
            if (existingRecordatoryType == null)
                return new RecordatoryResponse("Recordatory Type not found");
            if (existingPet == null)
                return new RecordatoryResponse("Pet not found");
            if (existingVet == null)
                return new RecordatoryResponse("Vet not found");

            bool attended = false;
            IEnumerable<Appointment> appointments = await _appointmentRepository.ListByScheduleId(existingVet.ScheduleId);
            appointments.ToList().ForEach(appointment => {
                if (appointment.PetId == petId)
                    attended = true;
            });

            if(!attended)
                return new RecordatoryResponse("Solo puedes poner recordatorios sobre mascotas las cuales usted haya atendido");

            try
            {
                recordatory.OwnerId = ownerId;
                recordatory.PetId = petId;
                recordatory.ScheduleId = scheduleId;
                recordatory.RecordatoryTypeId = recordyTypeId;
                recordatory.VetId = vetId;
                await _recordatoryRepository.AddAsync(recordatory);
                await _unitOfWork.CompleteAsync();

                return new RecordatoryResponse(recordatory);
            }
            catch (Exception ex)
            {
                return new RecordatoryResponse($"An error ocurred while saving recordatory: {ex.Message}");
            }
        }

        public async Task<RecordatoryResponse> UpdateAsync(int recordatoryId, Recordatory recordatory)
        {
            var existingRecordatory = await _recordatoryRepository.FindById(recordatoryId);
            if (existingRecordatory == null)
            {
                return new RecordatoryResponse("Recordatory not found");
            }
            existingRecordatory.DateStart = recordatory.DateStart;
            existingRecordatory.DateEnd = recordatory.DateEnd;
            existingRecordatory.Description = recordatory.Description;
            existingRecordatory.Duration = recordatory.Duration;
            try
            {
                _recordatoryRepository.Update(existingRecordatory);
                await _unitOfWork.CompleteAsync();

                return new RecordatoryResponse(existingRecordatory);
            }
            catch (Exception ex)
            {
                return new RecordatoryResponse($"An error ocurred while updating recordatory: {ex.Message}");
            }
        }
    }
}
