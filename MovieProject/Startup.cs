using Business.Abstract;
using Business.Concrete;
using Business.Models;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.Context;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MovieProject.Caching;
using MovieProject.Extensions;
using NLog;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace MovieProject
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


            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "localhost:6379";
            });


            //get all validater
            services.AddMvc()
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());


            services.AddDbContext<MovieStoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));

            //appsettings tanýmlama 1.yöntem.
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);


            //appsetting tanýmlama 2. yöntem.
            AppSettings appSettings = new AppSettings();
            Configuration.GetSection("AppSettings").Bind(appSettings);

            //token içindeki secret kullanmak için 
            TokenSettings tokenSettings1 = new TokenSettings();
            Configuration.GetSection("TokenSettings").Bind(tokenSettings1);

            //Create singleton from instance
            services.AddSingleton<AppSettings>(appSettings);


            //for dependency injection
            //Bu yapýyý auto fact gibi bir yapýya taþýyarak kullanmamýzda fayda vardýr. Bussiness katmaný içinde kullanýlabilir.
            //Auto fact yapýsý aop'yi desteklediðinden dolayý bu yapýyý auto factte taþýnýr.
            services.AddTransient<IJwtAuthenticationService, JwtAuthenticationManager>();
            services.AddTransient<IAuthenticationService, AuthenticationManager>();
            services.AddTransient<IMovieService, MovieManager>();
            services.AddTransient<ICastService, CastManager>();
            services.AddTransient<IUserService, UserManager>();
            services.AddTransient<IMenuService, MenuManager>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMenuRepository, MenuRepository>();
            services.AddTransient<ICacheService, CacheService>();




            //JWT token settings
            var tokenSettingsSection = Configuration.GetSection("TokenSettings");
            services.Configure<TokenSettings>(tokenSettingsSection);

            //JWT authentication settings
            var tokenSettings = tokenSettingsSection.Get<TokenSettings>();
            var key = Encoding.ASCII.GetBytes(tokenSettings.Secret);
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
                        ValidateAudience = false
                    };
                });



            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://localhost:4200"));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieProject", Version = "v1" });


                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = Configuration["CacheConnection"];
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            //update database ile zaten deðiþiklik olduðunda kendisi deðiþikliði algýlýyor ve her defasýnda update database yapmýyor.
            //Package managerda update-database gerek kalmaz.
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                try
                {
                    serviceScope.ServiceProvider.GetService<MovieStoreContext>().Database.Migrate();
                }
                catch (Exception ex)
                {

                    var logger = LogManager.GetCurrentClassLogger();
                    logger.Error("Veritabaný güncellenirken hata oluþtu.Detay:" + ex);
                }
            }

            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieProject v1"));
            }

            app.UseHttpsRedirection();


            app.UseRouting();

            app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials());

            app.UseAuthentication(); //user login

            app.UseAuthorization();

            app.UseLogging();

            //app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
