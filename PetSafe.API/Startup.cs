using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PetSafe.API.Domain.Persistence.Context;
using PetSafe.API.Domain.Persistence.Repositories;
using PetSafe.API.Domain.Services;
using PetSafe.API.Persistence.Repositories;
using PetSafe.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            // Database Connection Configuration

            services.AddDbContext<AppDbContext>(options =>
            {
                //options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"));
                options.UseMySQL(Configuration.GetConnectionString("AzureMySQLConnection"));
            });

            // Dependency Injection Configuration
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IIllnessRepository, IllnessRepository>();
            services.AddScoped<IOwnerLocationRepository, OwnerLocationRepository>();
            services.AddScoped<IOwnerProfileRepository, OwnerProfileRepository>();
            services.AddScoped<IPetIllnessRepository, PetIllnessRepository>();
            services.AddScoped<IPetOwnerRepository, PetOwnerRepository>();
            services.AddScoped<IPetProfileRepository, PetProfileRepository>();
            services.AddScoped<IPetTreatmentRepository, PetTreatmentRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
            services.AddScoped<ITreatmentRepository, TreatmentRepository>();
            services.AddScoped<IUserPlanRepository, UserPlanRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVeterinaryProfileRepository, VeterinaryProfileRepository>();
            services.AddScoped<IVeterinarySpecialtyRepository, VeterinarySpecialtyRepository>();
            services.AddScoped<IVetProfileRepository, VetProfileRepository>();
            services.AddScoped<IVetVeterinaryRepository, VetVeterinaryRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IRecordatoryRepository, RecordatoryRepository>();
            services.AddScoped<IRecordatoryTypeRepository, RecordatoryTypeRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();


            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IIllnessService, IllnessService>();
            services.AddScoped<IOwnerLocationService, OwnerLocationService>();
            services.AddScoped<IOwnerProfileService, OwnerProfileService>();
            services.AddScoped<IPetIllnessService, PetIllnessService>();
            services.AddScoped<IPetOwnerService, PetOwnerService>();
            services.AddScoped<IPetProfileService, PetProfileService>();
            services.AddScoped<IPetTreatmentService, PetTreatmentService>();
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IProvinceService, ProvinceService>();
            services.AddScoped<ISpecialtyService, SpecialtyService>();
            services.AddScoped<ITreatmentService, TreatmentService>();
            services.AddScoped<IUserPlanService, UserPlanService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVeterinaryProfileService, VeterinaryProfileService>();
            services.AddScoped<IVeterinarySpecialtyService, VeterinarySpecialtyService>();
            services.AddScoped<IVetProfileService, VetProfileService>();
            services.AddScoped<IVetVeterinaryService, VetVeterinaryService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IRecordatoryService, RecordatoryService>();
            services.AddScoped<IRecordatoryTypeService, RecordatoryTypeService>();
            services.AddScoped<IScheduleService, ScheduleService>();

            //Apply Endpoint Naming Convention
            services.AddRouting(options => options.LowercaseUrls = true);

            // AutoMapper Setup
            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PetSafe.API", Version = "v1" });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PetSafe.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
