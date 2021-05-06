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
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _planRepository;
        private readonly IUserPlanRepository _userPlanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PlanService(IPlanRepository planRepository, IUserPlanRepository userPlanRepository, IUnitOfWork unitOfWork)
        {
            _planRepository = planRepository;
            _userPlanRepository = userPlanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PlanResponse> DeleteAsync(int id)
        {
            var existingPlan = await _planRepository.FindById(id);
            if (existingPlan==null)
            {
                return new PlanResponse("Plan not found");
            }
            try
            {
                _planRepository.Remove(existingPlan);
                await _unitOfWork.CompleteAsync();

                return new PlanResponse(existingPlan);
            }
            catch(Exception ex)
            {
                return new PlanResponse($"An error ocurred while deleting plan: {ex.Message}");
            }
        }

        public async Task<PlanResponse> GetByIdAsync(int id)
        {
            var existingPlan = await _planRepository.FindById(id);
            if (existingPlan == null)
            {
                return new PlanResponse("Plan not found");
            }
            return new PlanResponse(existingPlan);
        }

        public async Task<IEnumerable<Plan>> ListAsync()
        {
            return await _planRepository.ListAsync();
        }

        public async Task<IEnumerable<Plan>> ListByUserIdAsync(int userId)
        {
            var userPlan = await _userPlanRepository.ListByUserIdAsync(userId);
            var plans = userPlan.Select(up => up.Plan).ToList();
            return plans;
        }

        public async Task<PlanResponse> SaveAsync(Plan plan)
        {
            try
            {
                await _planRepository.AddAsync(plan);
                await _unitOfWork.CompleteAsync();

                return new PlanResponse(plan);
            }
            catch(Exception ex)
            {
                return new PlanResponse($"An error ocurred while saving plan: {ex.Message}");
            }
        }

        public async Task<PlanResponse> UpdateAsync(int id, Plan plan)
        {
            var existingPlan = await _planRepository.FindById(id);
            if (existingPlan == null)
            {
                return new PlanResponse("Plan not found");
            }
            existingPlan.Name = plan.Name;
            existingPlan.Price = plan.Price;
            try
            {
                _planRepository.Update(existingPlan);
                await _unitOfWork.CompleteAsync();

                return new PlanResponse(existingPlan);
            }
            catch(Exception ex)
            {
                return new PlanResponse($"An error ocurred while updating plan: {ex.Message}");
            }
        }
    }
}
