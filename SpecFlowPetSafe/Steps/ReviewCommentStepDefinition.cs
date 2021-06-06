using FluentAssertions;
using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Persistence.Repositories;
using PetSafe.API.Domain.Services;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SpecFlowPetSafe.Steps
{
    [Binding]
    class ReviewCommentStepDefinition
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IOwnerProfileRepository _ownerProfileRepository;
        private readonly IVeterinaryProfileRepository _veterinaryProfileRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommentService _commentService;

        public ReviewCommentStepDefinition(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        public ReviewCommentStepDefinition(IOwnerProfileRepository ownerProfileRepository, 
            IVeterinaryProfileRepository veterinaryProfileRepository, ICommentRepository commentRepository, 
            IUnitOfWork unitOfWork, ICommentService commentService)
        {
            _ownerProfileRepository = ownerProfileRepository;
            _veterinaryProfileRepository = veterinaryProfileRepository;
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
            _commentService = commentService;
        }

        [Given("el dueño se encuentra en el perfil de la veterinaria")]
        public void GivenElDueño()
        {

        }


        [Given("escriba un comentario con su respectiva cantidad de estrellas")]
        public Comment GivenEscribeUnComentario()
        {
            Comment comment = new Comment();
            comment.Text = "Muy buen servicio";
            comment.StarsQuantity = 5;
            return comment;
        }

        [When("le de click en comentar")]
        public async Task<CommentResponse> WhenClickOnCommentar(Comment comment)
        {
            return await _commentService.SaveAsync(1,1,comment);
        }

        [Then("el comentario se habrá publicado correctamente")]
        public async Task ThenElComentarioSeHaPublicadoCorrectamente()
        {
            Comment comment = GivenEscribeUnComentario();
            var result = await WhenClickOnCommentar(comment);

            result.Resource.Should().Be(comment);
        }

    }
}
