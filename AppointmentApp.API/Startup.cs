using AppointmentApp.API.AppConfig;
using AppointmentApp.API.EmailServices;
using AppointmentApp.Business.Abstract;
using AppointmentApp.Business.Concrete;
using AppointmentApp.Data.Abstract;
using AppointmentApp.Data.Concrete;
using AppointmentApp.Data.Concrete.Ado;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentApp.API
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
            services.AddScoped<IUnitOfWork,UnitOfWork>();

       
            services.AddScoped<IDoctorService, BusinessDoctorManager>();
            services.AddScoped<IDoctorDetailService, BusinessDoctorDetailManager>();
            services.AddScoped<IUserService, BusinessUserManager>();
            services.AddScoped<IAppointmentsService, BusinessAppointmentsManager>();
            services.AddScoped<IBlogCommentsService, BusinessBlogCommentsManager>();
            services.AddScoped<IBlogService, BusinessBlogManager>();
            services.AddScoped<IDoctorVotingService, BusinessDoctorVotingManager>();
            services.AddScoped<IEmailSender, SmtpEmailSender>(i =>
                 new SmtpEmailSender(
                     Configuration["EmailSender:Host"],
                     Configuration.GetValue<int>("EmailSender:Port"),
                     Configuration.GetValue<bool>("EmailSender:EnableSSL"),
                     Configuration["EmailSender:UserName"],
                     Configuration["EmailSender:Password"])
                );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AppointmentApp", Version = "1.0.1",Description= "This page was created for front end",
                Contact = new OpenApiContact()
                {
                    Name = "Test Name",
                    Email = "admin@admin.com",
                    Url = new Uri("http://www.test.com/")

            }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id ="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

       

            var key = Encoding.ASCII.GetBytes(AppConfiguration.SecretKeyString());
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               

            }
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("v1/swagger.json", "AppointmentApp V1");
            });
            app.UseSwagger(options =>
            {
                options.SerializeAsV2 = false;
            });

            app.UseRouting();
          



            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
