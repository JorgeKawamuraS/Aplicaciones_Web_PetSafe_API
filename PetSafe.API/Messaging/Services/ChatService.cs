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
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IPetProfileRepository _petProfileRepository;
        private readonly IPetOwnerRepository _petOwnerRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChatService(IChatRepository chatRepository, IProfileRepository profileRepository,
            IPetProfileRepository petProfileRepository, IUnitOfWork unitOfWork, IPetOwnerRepository petOwnerRepository, 
            IAppointmentRepository appointmentRepository)
        {
            _chatRepository = chatRepository;
            _profileRepository = profileRepository;
            _petProfileRepository = petProfileRepository;
            _unitOfWork = unitOfWork;
            _petOwnerRepository = petOwnerRepository;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<ChatResponse> DeleteAsync(int chatId)
        {
            var existingChat = await _chatRepository.FindById(chatId);
            if (existingChat==null)
            {
                return new ChatResponse("Chat not found");
            }
            try
            {
                _chatRepository.Remove(existingChat);
                await _unitOfWork.CompleteAsync();

                return new ChatResponse(existingChat);
            }
            catch (Exception ex)
            {
                return new ChatResponse($"An error ocurred while deleting chat: {ex.Message}");
            }
        }

        public async Task<ChatResponse> GetByIdAsync(int chatId)
        {
            var existingChat = await _chatRepository.FindById(chatId);
            if (existingChat == null)
            {
                return new ChatResponse("Chat not found");
            }
            return new ChatResponse(existingChat);
        }

        public async Task<IEnumerable<Chat>> ListByReceiverId(int receiverId)
        {
            return await _chatRepository.FindByReceiverId(receiverId);
        }

        public async Task<IEnumerable<Chat>> ListBySenderId(int senderId)
        {
            return await _chatRepository.ListBySenderId(senderId);
        }

        public async Task<ChatResponse> SaveAsync(int senderId, int receiverId,int petId)
        {
            var existingSender = await _profileRepository.FindByIdAsync(senderId);
            var existingReceiver = await _profileRepository.FindByIdAsync(receiverId);
            var existingPet = await _petProfileRepository.FindById(petId);
            bool attended = false;
            if (existingSender == null)
                return new ChatResponse("Sender not found");
            if (existingReceiver == null)
                return new ChatResponse("Receiver not found");
            if (existingPet == null)
                return new ChatResponse("Pet not found");

            var possibleOwner1 = await _petOwnerRepository.FindByPetIdAndOwnerId(petId,senderId);
            var possibleOwner2 = await _petOwnerRepository.FindByPetIdAndOwnerId(petId, receiverId);

            if (possibleOwner1==null && possibleOwner2==null)
                return new ChatResponse("Pet not associated with the owner");

            try
            {
                bool exist = false;

                IEnumerable<Chat> chatsReceiver = await _chatRepository.ListBySenderId(receiverId);
                if(chatsReceiver!=null)
                chatsReceiver.ToList().ForEach(chat => {
                    if (chat.ReceiverProfileId == senderId && chat.PetId==petId)
                        exist = true;
                });

                Chat chat = new Chat();

                if (!exist)
                {
                    chat.ReceiverProfileId= senderId;
                    chat.SenderProfileId = receiverId;
                    chat.PetId = petId;
                    await _chatRepository.AddAsync(chat);
                    await _unitOfWork.CompleteAsync();
                }

                chat.ReceiverProfileId = receiverId;
                chat.SenderProfileId = senderId;
                chat.PetId = petId;
                await _chatRepository.AddAsync(chat);
                await _unitOfWork.CompleteAsync();

                return new ChatResponse(chat);
            }
            catch (Exception ex)
            {
                return new ChatResponse($"An error ocurred while saving chat: {ex.Message}");
            }
        }
    }
}
