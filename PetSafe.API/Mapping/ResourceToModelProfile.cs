using AutoMapper;
using PetSafe.API.Domain.Models;
using PetSafe.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Mapping
{
    public class ResourceToModelProfile : AutoMapper.Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveCityResource,City>();
            CreateMap<SaveIllnessResource,Illness>();
            CreateMap<SaveOwnerProfileResource,OwnerProfile>();
            CreateMap<SavePetOwnerResource,PetOwner>();
            CreateMap<SavePetProfileResource,PetProfile>();
            CreateMap<SavePetTreatment,PetTreatment>();
            CreateMap<SavePlanResource,Plan>();
            CreateMap<SaveProfileResource, Domain.Models.Profile>();
            CreateMap<SaveProvinceResource,Province>();
            CreateMap<SaveSpecialtyResource,Specialty>();
            CreateMap<SaveTreatmentResource,Treatment>();
            CreateMap<SaveUserResource,User>();
            CreateMap<SaveVeterinaryProfileResource,VeterinaryProfile>();
            CreateMap<SaveVetProfileResource,VetProfile>();
            CreateMap<SaveVetVeterinaryResource,VetVeterinary>();
            CreateMap<SaveOwnerLocationResource, OwnerLocation>();
            CreateMap<SaveAppointmentResource,Appointment>();
            CreateMap<SaveCommentResource,Comment>();
            CreateMap<SaveMessageResource,Message>();
            CreateMap<SaveRecordatoryResource,Recordatory>();
            CreateMap<SaveRecordatoryTypeResource,RecordatoryType>();
        }
    }
}
