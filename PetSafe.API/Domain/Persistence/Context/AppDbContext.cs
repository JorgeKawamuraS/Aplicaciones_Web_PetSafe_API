using Microsoft.EntityFrameworkCore;
using PetSafe.API.Domain.Models;
using PetSafe.API.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Illness> Illnesses { get; set; }
        public DbSet<OwnerLocation> OwnerLocations { get; set; }
        public DbSet<OwnerProfile> OwnerProfiles { get; set; }
        public DbSet<PetIllness> PetIllnesses { get; set; }
        public DbSet<PetOwner> PetOwners { get; set; }
        public DbSet<PetProfile> PetProfiles { get; set; }
        public DbSet<PetTreatment> PetTreatments { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPlan> UserPlans { get; set; }
        public DbSet<VeterinaryProfile> VeterinaryProfiles { get; set; }
        public DbSet<VeterinarySpecialty> VeterinarySpecialties { get; set; }
        public DbSet<VetProfile> VetProfiles { get; set; }
        public DbSet<VetVeterinary> VetVeterinaries { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<RecordatoryType> RecordatoryTypes { get; set; }
        public DbSet<Recordatory> Recordatories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Appointment Entity
            builder.Entity<Appointment>().ToTable("Appointments");

            //Constraints
            builder.Entity<Appointment>().HasKey(a => a.Id);
            builder.Entity<Appointment>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Appointment>().Property(a => a.Date).IsRequired();

            //RelationShips
            builder.Entity<Appointment>().HasOne(a => a.Owner).WithMany(o => o.Appointments).HasForeignKey(a => a.OwnerId);
            builder.Entity<Appointment>().HasOne(a => a.Veterinary).WithMany(v => v.Appointments).HasForeignKey(a => a.VeterinaryId);
            builder.Entity<Appointment>().HasOne(a => a.Vet).WithMany(v => v.Appointments).HasForeignKey(a => a.VetId);
            builder.Entity<Appointment>().HasOne(a => a.PetProfile).WithMany(p => p.Appointments).HasForeignKey(a => a.PetId);
            builder.Entity<Appointment>().HasOne(a => a.Schedule).WithMany(os => os.Appointments).HasForeignKey(a => a.ScheduleId);


            //Chat Entity
            builder.Entity<Chat>().ToTable("Chats");

            //Constraints
            builder.Entity<Chat>().HasKey(c => c.Id);
            builder.Entity<Chat>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();

            //Relationships
            builder.Entity<Chat>().HasOne(c => c.ReceiverProfile).WithMany(pp => pp.ReceiverChats).HasForeignKey(c => c.ReceiverProfileId);
            builder.Entity<Chat>().HasOne(c => c.SenderProfile).WithMany(pp => pp.SenderChats).HasForeignKey(c => c.SenderProfileId);
            builder.Entity<Chat>().HasOne(c => c.Pet).WithMany(pp => pp.Chats).HasForeignKey(c => c.PetId);


            //City Entity
            builder.Entity<City>().ToTable("Cities");

            //Constraints
            builder.Entity<City>().HasKey(c => c.Id);
            builder.Entity<City>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<City>().Property(c => c.Name).IsRequired().HasMaxLength(30);

            //Relationships
            builder.Entity<City>().HasOne(pr => pr.Province).WithMany(pr => pr.Cities).HasForeignKey(pr => pr.ProvinceId);


            //Comment
            builder.Entity<Comment>().ToTable("Comments");

            //Contraints
            builder.Entity<Comment>().HasKey(c => c.Id);
            builder.Entity<Comment>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();

            //Relationships
            builder.Entity<Comment>().HasOne(c => c.OwnerProfile).WithMany(op => op.Comments).HasForeignKey(c => c.OwnerProfileId);
            builder.Entity<Comment>().HasOne(c => c.VeterinaryProfile).WithMany(vp => vp.Comments).HasForeignKey(c => c.VeterinaryProfileId);


            //Illness Entity
            builder.Entity<Illness>().ToTable("Illnesses");

            //Constraints
            builder.Entity<Illness>().HasKey(i => i.Id);
            builder.Entity<Illness>().Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Illness>().Property(i => i.Name).IsRequired().HasMaxLength(30);


            //Message Entity
            builder.Entity<Message>().ToTable("Messages");

            //Constraints
            builder.Entity<Message>().HasKey(m => m.Id);
            builder.Entity<Message>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();

            //Relationships
            builder.Entity<Message>().HasOne(m => m.Chat).WithMany(cr => cr.Messages).HasForeignKey(m=>m.ChatId);


            //OwnerLocation Entity
            builder.Entity<OwnerLocation>().ToTable("OwnerLocations");

            //Constraints
            builder.Entity<OwnerLocation>().HasKey(owl => new { owl.CityId, owl.Date, owl.OwnerId, owl.ProvinceId });
            builder.Entity<OwnerLocation>().Property(owl => owl.Date).IsRequired().ValueGeneratedOnAdd();

            //Relationships
            builder.Entity<OwnerLocation>().HasOne(owl => owl.City).WithMany(c => c.OwnerLocations).HasForeignKey(owl => owl.CityId);
            builder.Entity<OwnerLocation>().HasOne(owl => owl.OwnerProfile).WithMany(c => c.OwnerLocations).HasForeignKey(owl => owl.OwnerId);
            builder.Entity<OwnerLocation>().HasOne(owl => owl.Province).WithMany(c => c.OwnerLocations).HasForeignKey(owl => owl.ProvinceId);


            //OwnerProfile Entity
            builder.Entity<OwnerProfile>().ToTable("OwnerProfiles");

            //Constraints
            //builder.Entity<OwnerProfile>().HasKey(owp => owp.Id);
            builder.Entity<OwnerProfile>().Property(owp => owp.Id).IsRequired().ValueGeneratedOnAdd();

            //Relationships
            builder.Entity<OwnerProfile>().HasOne(owp => owp.User).WithOne(u => u.OwnerProfile).HasForeignKey<OwnerProfile>(owp => owp.UserId);//Doubt


            //PetIllness Entity
            builder.Entity<PetIllness>().ToTable("PetIllnesses");

            //Constraints
            builder.Entity<PetIllness>().HasKey(pi => new { pi.IllnesstId, pi.PetId });

            //Relationships
            builder.Entity<PetIllness>().HasOne(pi => pi.Illness).WithMany(i => i.PetIllnesses).HasForeignKey(pi => pi.IllnesstId);
            builder.Entity<PetIllness>().HasOne(pi => pi.PetProfile).WithMany(i => i.PetIllnesses).HasForeignKey(pi => pi.PetId);


            //PetOwner Entity
            builder.Entity<PetOwner>().ToTable("PetOwners");

            //Constraints
            builder.Entity<PetOwner>().HasKey(po => new { po.PetId, po.OwnerId });
            builder.Entity<PetOwner>().Property(po => po.Principal).IsRequired();

            //Relationships
            builder.Entity<PetOwner>().HasOne(po => po.PetProfile).WithMany(pep => pep.PetOwners).HasForeignKey(po => po.PetId);
            builder.Entity<PetOwner>().HasOne(po => po.OwnerProfile).WithMany(pep => pep.PetOwners).HasForeignKey(po => po.OwnerId);


            //PetProfile Entity
            builder.Entity<PetProfile>().ToTable("PetProfiles");

            //Constraints
            //builder.Entity<PetProfile>().HasKey(pep => pep.Id);
            builder.Entity<PetProfile>().Property(pep => pep.Id).IsRequired().ValueGeneratedOnAdd();


            //PetTreatment Entity
            builder.Entity<PetTreatment>().ToTable("PetTreatments");

            //Constraints
            builder.Entity<PetTreatment>().HasKey(pt => new { pt.PetId, pt.TreatmentId, pt.Date });
            builder.Entity<PetTreatment>().Property(pt => pt.Date).IsRequired();

            //Relationships
            builder.Entity<PetTreatment>().HasOne(pt => pt.PetProfile).WithMany(pep => pep.PetTreatments).HasForeignKey(pt => pt.PetId);
            builder.Entity<PetTreatment>().HasOne(pt => pt.Treatment).WithMany(pep => pep.PetTreatments).HasForeignKey(pt => pt.TreatmentId);


            //Plan Entity
            builder.Entity<Plan>().ToTable("Plans");

            //Constraints
            builder.Entity<Plan>().HasKey(p => p.Id);
            builder.Entity<Plan>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Plan>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Plan>().Property(p => p.Price).IsRequired();


            //Profile Entity
            builder.Entity<Profile>().ToTable("Profiles");

            //Constraints
            builder.Entity<Profile>().HasKey(pr => pr.Id);
            builder.Entity<Profile>().Property(pr => pr.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Profile>().Property(pr => pr.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Profile>().Property(pr => pr.TelephonicNumber);
            builder.Entity<Profile>().Property(pr => pr.BirthDate);

            //Relationships
            builder.Entity<Profile>().HasOne(pr => pr.City).WithMany(c => c.Profiles).HasForeignKey(pr =>  pr.CityId);
            builder.Entity<Profile>().HasOne(pr => pr.Province).WithMany(c => c.Profiles).HasForeignKey(pr => pr.ProvinceId);


            //Province Entity
            builder.Entity<Province>().ToTable("Provinces");

            //Constraints
            builder.Entity<Province>().HasKey(p => p.Id);
            builder.Entity<Province>().Property(pr => pr.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Province>().Property(pr => pr.Name).IsRequired().HasMaxLength(30);


            //Recordatory Entity
            builder.Entity<Recordatory>().ToTable("Recordatories");

            //Constraints
            builder.Entity<Recordatory>().HasKey(r=>r.Id);
            builder.Entity<Recordatory>().Property(r=>r.Id).IsRequired().ValueGeneratedOnAdd();

            //Relationships
            builder.Entity<Recordatory>().HasOne(r => r.Vet).WithMany(r => r.Recordatories).HasForeignKey(r => r.VetId);
            builder.Entity<Recordatory>().HasOne(r => r.Owner).WithMany(ow => ow.Recordatories).HasForeignKey(r => r.OwnerId);
            builder.Entity<Recordatory>().HasOne(r => r.Pet).WithMany(p => p.Recordatories).HasForeignKey(r=>r.PetId);
            builder.Entity<Recordatory>().HasOne(r => r.Schedule).WithMany(s => s.Recordatories).HasForeignKey(r=>r.ScheduleId);
            builder.Entity<Recordatory>().HasOne(r => r.RecordatoryType).WithMany(rt => rt.Recordatories).HasForeignKey(r=>r.RecordatoryTypeId);


            //RecordatoryType Entity
            builder.Entity<RecordatoryType>().ToTable("RecordatoryTypes");

            //Constraints
            builder.Entity<RecordatoryType>().HasKey(rt => rt.Id);
            builder.Entity<RecordatoryType>().Property(rt => rt.Id).IsRequired().ValueGeneratedOnAdd();


            //Schedule
            builder.Entity<Schedule>().ToTable("Schedules");

            //Constraints
            builder.Entity<Schedule>().HasKey(s => s.Id);
            builder.Entity<Schedule>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();

            //RelationShips
            builder.Entity<Schedule>().HasOne(s => s.Profile).WithOne(p => p.Schedule).HasForeignKey<Schedule>(s => s.ProfileId);


            //Specialty Entity
            builder.Entity<Specialty>().ToTable("Specialties");

            //Constraints
            builder.Entity<Specialty>().HasKey(p => p.Id);
            builder.Entity<Specialty>().Property(pr => pr.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Specialty>().Property(pr => pr.Name).IsRequired().HasMaxLength(30);


            //Treatment Entity
            builder.Entity<Treatment>().ToTable("Treatments");

            //Constraints
            builder.Entity<Treatment>().HasKey(p => p.Id);
            builder.Entity<Treatment>().Property(pr => pr.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Treatment>().Property(pr => pr.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Treatment>().Property(pr => pr.Description).HasMaxLength(300);


            //User Entity
            builder.Entity<User>().ToTable("User");

            //Constraints
            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>().Property(pr => pr.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(pr => pr.Mail).IsRequired();
            builder.Entity<User>().Property(pr => pr.Password).IsRequired();
            builder.Entity<User>().Property(pr => pr.UserTypeVet).IsRequired();


            //UserPlan Entity
            builder.Entity<UserPlan>().ToTable("UserPlans");

            //Constraint
            builder.Entity<UserPlan>().HasKey(p =>new { p.UserId, p.PlanId, p.DateOfUpdate });
            builder.Entity<UserPlan>().Property(p => p.DateOfUpdate).ValueGeneratedOnAdd();

            //Relationships
            builder.Entity<UserPlan>().HasOne(up => up.User).WithMany(u => u.UserPlans).HasForeignKey(up=>up.UserId);
            builder.Entity<UserPlan>().HasOne(up => up.Plan).WithMany(u => u.UserPlans).HasForeignKey(up => up.PlanId);


            //VeterinaryProfile Entity
            builder.Entity<VeterinaryProfile>().ToTable("VeterinaryProfiles");

            //Constraints
            //builder.Entity<VeterinaryProfile>().HasKey(owp => owp.Id);
            builder.Entity<VeterinaryProfile>().Property(owp => owp.Id).IsRequired().ValueGeneratedOnAdd();


            //VeterinarySpecialty Entity
            builder.Entity<VeterinarySpecialty>().ToTable("VeterinarySpecialties");

            //Constraints
            builder.Entity<VeterinarySpecialty>().HasKey(vsp=> new { vsp.VeterinaryId,vsp.SpecialtyId });

            //Relationships
            builder.Entity<VeterinarySpecialty>().HasOne(vsp => vsp.Specialty).WithMany(sp => sp.VeterinarySpecialties).HasForeignKey(vsp=>vsp.SpecialtyId);
            builder.Entity<VeterinarySpecialty>().HasOne(vsp => vsp.VeterinaryProfile).WithMany(sp => sp.VeterinarySpecialties).HasForeignKey(vsp => vsp.VeterinaryId);


            //VetProfile
            builder.Entity<VetProfile>().ToTable("VetProfiles");

            //Constraints
            //builder.Entity<VetProfile>().HasKey(vp => vp.Id);
            builder.Entity<VetProfile>().Property(vp => vp.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<VetProfile>().Property(vp => vp.Code);
            builder.Entity<VetProfile>().Property(vp => vp.ExperienceYear);

            //Relationships
            builder.Entity<VetProfile>().HasOne(owp => owp.User).WithOne(u => u.VetProfile).HasForeignKey<VetProfile>(owp => owp.UserId);


            //VetVeterinary Entity
            builder.Entity<VetVeterinary>().ToTable("VetVeterinaries");

            //Constraints
            builder.Entity<VetVeterinary>().HasKey(vv=>new {vv.VeterinaryId,vv.VetId });
            builder.Entity<VetVeterinary>().Property(vv => vv.Own).IsRequired();

            //Relationships
            builder.Entity<VetVeterinary>().HasOne(vv => vv.VetProfile).WithMany(vt => vt.VetVeterinaries).HasForeignKey(vv=>vv.VetId);
            builder.Entity<VetVeterinary>().HasOne(vv => vv.VeterinaryProfile).WithMany(vt => vt.VetVeterinaries).HasForeignKey(vv => vv.VeterinaryId);

            //Apply Naming Convention
            builder.ApplySnakeCaseNamingConvention();
        }
    }
}
