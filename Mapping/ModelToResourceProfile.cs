using AutoMapper;
using PetSafe.API.Domain.Models;
using PetSafe.API.Extensions;
using PetSafe.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Mapping
{
    public class ModelToResourceProfile : AutoMapper.Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<City, CityResource>();
            CreateMap<Illness, IllnessResource>();
            CreateMap<OwnerLocation, OwnerLocationResource>();
            CreateMap<OwnerProfile, OwnerProfileResource>();
            CreateMap<PetIllness, PetIllnessResource>();
            CreateMap<PetOwner, PetOwnerResource>();
            CreateMap<PetProfile, PetProfileResource>();
            CreateMap<PetTreatment, PetTreatmentResource>();
            CreateMap<Plan, PlanResource>();
            CreateMap<Domain.Models.Profile, ProfileResource>();
            CreateMap<Province, ProvinceResource>();
            CreateMap<Specialty, SpecialtyResource>();
            CreateMap<Treatment, TreatmentResource>();
            CreateMap<User, UserResource>()
                .ForMember(src=>src.UserType,
                opt=>opt.MapFrom(src=>src.UserType.ToDescriptionString()));
            CreateMap<UserPlan, UserPlanResource>();
            CreateMap<VeterinaryProfile, VeterinaryProfileResource>();
            CreateMap<VeterinarySpecialty, VeterinarySpecialtyResource>();
            CreateMap<VetProfile, VetProfile>();
            CreateMap<VetVeterinary, VetVeterinaryResource>();
        }
    }
}
