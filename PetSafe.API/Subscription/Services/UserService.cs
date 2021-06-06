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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserPlanRepository _userPlanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUserPlanRepository userPlanRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _userPlanRepository = userPlanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserResponse> DeleteAsync(int id)
        {
            var existingUser = await _userRepository.FindByIdAsync(id);
            if (existingUser==null)
            {
                return new UserResponse("User not found");
            }
            try
            {
                IEnumerable<UserPlan> userPlans = await _userPlanRepository.ListByUserIdAsync(id);
                userPlans.ToList().ForEach(userPlan => { 
                    _userPlanRepository.Remove(userPlan); 
                });

                _userRepository.Remove(existingUser);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while deleting user: {ex.Message}");
            }
        }

        public async Task<UserResponse> GetByIdAsync(int id)
        {
            var existingUser = await _userRepository.FindByIdAsync(id);
            if (existingUser == null)
            {
                return new UserResponse("User not found");
            }
            return new UserResponse(existingUser);
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _userRepository.ListAsync();
        }

        public async Task<IEnumerable<User>> ListByPlanIdAsync(int planId)
        {
            var userPlan = await _userPlanRepository.ListByPlanIdAsync(planId);
            var users = userPlan.Select(up=>up.User).ToList();
            return users;
        }

        public async Task<UserResponse> SaveAsync(User user)
        {
            try
            {
                bool different = true;
                IEnumerable<User> users = await ListAsync();
                if (users != null)
                    users.ToList().ForEach(savedUser=>
                    {
                        if (savedUser.Mail == user.Mail)
                            different = false;
                    });

                if(!different)
                    return new UserResponse("No pueden existir dos users con el mismo mail");

                await _userRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while saving user: {ex.Message}");
            }
        }

        public async Task<UserResponse> UpdateAsync(int id, User user)
        {
            var existingUser = await _userRepository.FindByIdAsync(id);
            if (existingUser == null)
            {
                return new UserResponse("User not found");
            }
            existingUser.Mail = user.Mail;
            existingUser.Password = user.Password;
            try
            {
                _userRepository.Update(existingUser);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while updating user: {ex.Message}");
            }
        }
    }
}
