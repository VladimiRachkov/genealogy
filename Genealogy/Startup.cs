using System.IO;
using System.Text;
using AutoMapper;
using Genealogy.Models;
using Genealogy.Repository.Abstract;
using Genealogy.Repository.Concrete;
using Genealogy.Service.Astract;
using Genealogy.Service.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace SpaPrerendering
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
            var appSettings = Configuration.GetSection("AppSettings").GetChildren().AsEnumerable();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // ConfigureServices
            // Correct RootPath to your angular app build, based on angular.json prop
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist/browser";
            });

            services.AddSingleton<IConfiguration>(provider => Configuration);
            services.AddScoped<IGenealogyService, GenealogyService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DbContext, GenealogyContext>();

            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<GenealogyContext>(options => options.UseNpgsql(connection));

            services.AddHostedService<PurchaseManageService>();
            // Auto Mapper Configurations
            
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            IMapper mapper = mappingConfig.CreateMapper();


            services.AddSingleton(mapper);

            var secretKey = appSettings.Where(i => i.Key == "Secret").FirstOrDefault().Value;
            var key = Encoding.ASCII.GetBytes(appSettings.Where(i => i.Key == "Secret").FirstOrDefault().Value);
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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

            // services.AddAuthorization(options =>
            // {
            //     options.AddPolicy("Admins", policy =>
            //                       policy.RequireClaim("Администратор"));
            // });

            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                spa.UseSpaPrerendering(options =>
                {
                    options.BootModulePath = $"{spa.Options.SourcePath}/dist/server/main.js";
                    options.BootModuleBuilder = env.IsDevelopment() ?
                        new AngularCliBuilder(npmScript: "build:ssr") :
                        null;
                    options.ExcludeUrls = new[] { "/sockjs-node" };

                    options.SupplyData = (context, data) =>
                    {
                        data.Add("message", "Message from the server");
                    };
                });

                if (env.IsDevelopment())
                {

                    spa.UseAngularCliServer(npmScript: "start");
                }
                else
                {
                    // Configure
                    // In order to serve files
                    app.UseStaticFiles(new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "dist/browser")),
                        RequestPath = ""
                    });
                }
            });
        }
    }
}