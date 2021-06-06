using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Persistence.Repositories;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ScheduleService(IScheduleRepository scheduleRepository, IUnitOfWork unitOfWork, IProfileRepository profileRepository)
        {
            _scheduleRepository = scheduleRepository;
            _unitOfWork = unitOfWork;
            _profileRepository = profileRepository;
        }

        public async Task<ScheduleResponse> GetByIdAsync(int scheduleId)
        {
            var existingSchedule = await _scheduleRepository.FindById(scheduleId);
            if (existingSchedule == null)
            {
                return new ScheduleResponse("Schedule not found");
            }
            return new ScheduleResponse(existingSchedule);
        }

        public async Task<ScheduleResponse> GetByProfileIdAsync(int profileId)
        {
            var schedules = await _scheduleRepository.FindByProfileId(profileId);
            return new ScheduleResponse(schedules.First());
        }

        public async Task<ScheduleResponse> SaveAsync(int profileId)
        {
            var existingProfile = await _profileRepository.FindByIdAsync(profileId);
            if (existingProfile == null)
            {
                return new ScheduleResponse("Profile not found");
            }
            try
            {
                Schedule schedule = new Schedule();
                schedule.ProfileId = profileId;

                await _scheduleRepository.AddAsync(schedule);
                await _unitOfWork.CompleteAsync();

                return new ScheduleResponse(schedule);
            }
            catch (Exception ex)
            {
                return new ScheduleResponse($"An error ocurred while saving schedule: {ex.Message}");
            }
        }

    }
}
