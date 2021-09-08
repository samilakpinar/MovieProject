using Business.Abstract;
using Business.Concrete;
using Business.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.IO;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

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

            //Database perform database migration
            //var de =  services.AddDbContext<MovieStoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlConnection")));
            //services.BuildServiceProvider().GetService<MovieStoreContext>().Database.Migrate();

            

            services.AddDbContext<MovieStoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));

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
            services.AddTransient<IUserDal, EfUserDal>();
            services.AddTransient<IMenuDal, EfMenuDal>();


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

            


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {



            //Package managerda update-database gerek kalmaz.
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                try
                {
                    serviceScope.ServiceProvider.GetService<MovieStoreContext>().Database.Migrate();
                }
                catch (Exception ex)
                {
                    throw;
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
