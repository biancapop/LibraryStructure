using Library.DBRepositories;
using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Text;

namespace Library
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseMySql("Server=127.0.0.1;Database=db;User=usr;Password=pass;",
                mySqlOptions =>
                {
                    mySqlOptions.ServerVersion(new Version(5, 7, 17),
                        ServerType.MySql); // replace with your Server Version and Type
                    });
            }, ServiceLifetime.Scoped);

            //Singleton - pila de llamadas
            services.AddSingleton<RequestPile>();
            services.AddScoped<UnitOfWork>();

            services.AddHttpClient();

            // Inyecta un objeto que se creara una vez para toda la app
            //services.AddSingleton<User>(new User());
            // Inyecta un objeto que se creara cada vez que se inyecte
            //services.AddTransient<IUserService, UserService>();
            // Inyecta un objeto que se creara cada vez que se inyecte pero solo una instanacia para cada petición HTTP
            //services.AddScoped<IUserService, UserService>();

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    // The signing key must match!
                    //ValidateIssuerSigningKey = true,
                    //ValidateAudience = false,
                    //ValidateIssuer = false,
                    //IssuerSigningKeys = new List<SecurityKey> { signingKey },
                    // Validate the token expiry
                    ValidateLifetime = false,
                    RequireExpirationTime = false
                };
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    RequireExpirationTime = false
                };
            });

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });


            app.UseMiddleware<RequestLoggingMiddleware>();

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}