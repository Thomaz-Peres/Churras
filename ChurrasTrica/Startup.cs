using ChurrasTrica.Data.Repository;
using ChurrasTrinca.Domain.Interfaces.Repository;
using ChurrasTrinca.Domain.Interfaces.Services;
using ChurrasTrinca.Domain.Tokens;
using ChurrasTrinca.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace ChurrasTrica
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
            services.AddCors();

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );


            #region configurando swagger
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Entre com o Token JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        }, new List<string>()
                    }
                });
            });
            #endregion


            #region configurando token
            var signingConfiguration = new SigningConfiguration();

            var tokenConfiguration = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(Configuration.GetSection("TokenConfigurations")).Configure(tokenConfiguration);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidations = bearerOptions.TokenValidationParameters;
                paramsValidations.IssuerSigningKey = signingConfiguration.Key;
                paramsValidations.ValidateAudience = false;
                paramsValidations.ValidateIssuer = false;
                paramsValidations.ValidateIssuerSigningKey = true;
                paramsValidations.ValidateLifetime = true;
                paramsValidations.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(s =>
            {
                s.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
            #endregion


            services.AddDbContext<Data.Data.DataContext>(options => options.UseSqlite("Data Source=ChurrasTrinca.db"));
            //services.AddScoped<Data.Data.DataContext, Data.Data.DataContext>();


            services.AddScoped(typeof(IRepositoryBase<>), typeof(BaseRepository<>));
            services.AddScoped<ILoginRepository, LoginRepository>();


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IChurrasService, ChurrasService>();
            services.AddScoped<ILoginService, LoginService>();

            services.AddSingleton(tokenConfiguration);
            services.AddSingleton(signingConfiguration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Churras Trinca");
                s.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
