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
    public class CommentService : ICommentService
    {
        private readonly IOwnerProfileRepository _ownerProfileRepository;
        private readonly IVeterinaryProfileRepository _veterinaryProfileRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IOwnerProfileRepository ownerProfileRepository,
            IVeterinaryProfileRepository veterinaryProfileRepository, IUnitOfWork unitOfWork, 
            ICommentRepository commentRepository, IAppointmentRepository appointmentRepository)
        {
            _ownerProfileRepository = ownerProfileRepository;
            _veterinaryProfileRepository = veterinaryProfileRepository;
            _unitOfWork = unitOfWork;
            _commentRepository = commentRepository;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<CommentResponse> DeleteAsync(int commentId)
        {
            var existingComment = await _commentRepository.FindById(commentId);
            if (existingComment==null)
            {
                return new CommentResponse("Comment not found");
            }
            try
            {
                _commentRepository.Remove(existingComment);
                await _unitOfWork.CompleteAsync();

                return new CommentResponse(existingComment);
            }
            catch (Exception ex)
            {
                return new CommentResponse($"An error ocurred while deleting comment: {ex.Message}");
            }
        }

        public async Task<CommentResponse> GetByIdAsync(int commentId)
        {
            var existingComment = await _commentRepository.FindById(commentId);
            if (existingComment == null)
            {
                return new CommentResponse("Comment not found");
            }
            return new CommentResponse(existingComment);
        }

        public async Task<IEnumerable<Comment>> ListByVeterinaryProfileId(int veterinaryProfileId)
        {
            return await _commentRepository.ListByVeterinaryProfileId(veterinaryProfileId);
        }

        public async Task<CommentResponse> SaveAsync(int ownerId, int veterinaryId, Comment comment)
        {
            var existingOwner = await _ownerProfileRepository.FindById(ownerId);
            var existingVeterinary = await _veterinaryProfileRepository.FindById(veterinaryId);
            if (existingOwner == null)
                return new CommentResponse("Owner not found");
            if (existingVeterinary == null)
                return new CommentResponse("Veterinary not found");
            try
            {
                bool attended = false;
                IEnumerable<Appointment> appointments = await _appointmentRepository.ListByScheduleId(ownerId);

                if(appointments!=null)
                    appointments.ToList().ForEach(appointment=>{
                        if (appointment.VeterinaryId == veterinaryId && appointment.OwnerId == ownerId)
                            attended = true;
                    });

                if(!attended)
                    return new CommentResponse("Solo puedes calificar una veterinaria en la cual tu mascota haya sido atendida");

                comment.OwnerProfileId = ownerId;
                comment.VeterinaryProfileId = veterinaryId;

                await _commentRepository.AddAsync(comment);
                await _unitOfWork.CompleteAsync();

                return new CommentResponse(comment);

            }
            catch (Exception ex)
            {
                return new CommentResponse($"An error ocurred while saving comment: {ex.Message}");
            }
        }

        public async Task<CommentResponse> UpdateAsync(int commentId, Comment comment)
        {
            var existingComment = await _commentRepository.FindById(commentId);
            if (existingComment == null)
            {
                return new CommentResponse("Comment not found");
            }
            existingComment.Text = comment.Text;
            try
            {
                _commentRepository.Update(existingComment);
                await _unitOfWork.CompleteAsync();

                return new CommentResponse(existingComment);
            }
            catch (Exception ex)
            {
                return new CommentResponse($"An error ocurred while updating comment: {ex.Message}");
            }
        }
    }
}
