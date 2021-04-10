using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using MyTestProjectAPI.ServiceInstallers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MyTestProjectAPIBaseLayer.Filters;
using MyTestProjectDomainLayer.BaseClasses;

namespace MyTestProjectAPI
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// Property for Configuration.
        /// </summary>
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string AllowedSpecificOrigins = "_allowSpecificOrigins";

        /// <summary>
        /// 
        /// </summary>
        public string ProjectTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProjectXMLFilePath { get; set; }

        /// <summary>
        /// To Decide the Run-environment
        /// </summary>
        public bool EnableDebuggingAndDocumentation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CORSUrls { get; set; }
        private const string const_enableDebuggingAndDocumentation = "enableDebuggingAndDocumentation";
        private const string const_CORSURLKey = "corsURL";

        static string environment = "";
        /// <summary>
        /// Startup Initiation
        /// </summary>
        /// <param name="configuration">configuration variable</param>
        public Startup(IConfiguration configuration)
        {
            ProjectXMLFilePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "MyTestProjectAPI.xml");
            Configuration = configuration;
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            environment = config.GetSection("Environment").Value;
            if (environment == "Development")
            {
                EnableDebuggingAndDocumentation = true;
            }
            else
            {
                EnableDebuggingAndDocumentation = false;
            }
            ProjectTitle = "MyTestProject";
            // EnableDebuggingAndDocumentation = Configuration.GetValue<String>("enableDebuggingAndDocumentation").ToString().ToLower() == "yes";
            //CORSUrls = Configuration.GetValue<String>("corsURL").ToString();
        }

        /// <summary>
        /// Configure Services
        /// </summary>
        /// <param name="services">service variable</param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add functionality to inject IOptions<T>
            services.AddOptions();
            services.Configure<AppConfigurationSettings>(Configuration.GetSection("AppConfigurationSettings"));

            services.InstallServicesInAssemblies(Configuration);
            services.AddCors(options =>
            {

                options.AddPolicy(AllowedSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowCredentials();
                    if (EnableDebuggingAndDocumentation == true)
                    {
                        builder.WithOrigins("http://localhost:4200", "www.mytestproject.net");
                    }
                    else
                    {
                        builder.WithOrigins("http://mytestproject.net/", "www.mytestproject.net");
                    }

                });
            });
            services.AddSignalR()
                .AddJsonProtocol(options =>
                {
                    options.PayloadSerializerOptions.PropertyNamingPolicy = null;
                });
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(Options =>
            {
                Options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = EnableDebuggingAndDocumentation == true ? Configuration.GetValue<string>("AppConfigurationSettings:DevCORSURL") : Configuration.GetValue<string>("AppConfigurationSettings:ProdCORSURL"),
                    ValidAudience = EnableDebuggingAndDocumentation == true ? Configuration.GetValue<string>("AppConfigurationSettings:DevCORSURL") : Configuration.GetValue<string>("AppConfigurationSettings:ProdCORSURL"),
                  
                };
            });
            services.AddControllers();

            services.AddMvc(setupAction =>
            {
                setupAction.EnableEndpointRouting = false;
            }).AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.UseMemberCasing();
            });
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });


            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession();

            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie("Cookies", options =>
            {
                options.LoginPath = new PathString("/auth");
                options.AccessDeniedPath = new PathString("/auth");
            });
            if (EnableDebuggingAndDocumentation)
            {
                #region Swagger
                services.AddSwaggerGen(swagger =>
                {
                    swagger.SwaggerDoc("UserManagement", new OpenApiInfo
                    {
                        Version = "UserManagement",
                        Title = ProjectTitle + " - User Management Module",
                        Description = $""
                    });
                    swagger.ResolveConflictingActions(a => a.First());
                    swagger.OperationFilter<RemoveVersionFromParameter>();
                    swagger.DocumentFilter<ReplaceVersionWithExactValueInPath>();
                    swagger.IncludeXmlComments(ProjectXMLFilePath);
                    swagger.OperationFilter<SwaggerCustomFilter>();
                    swagger.SchemaFilter<SwaggerExcludeFilter>();

                });
                #endregion

                //#region Swagger Json property Support
                //services.AddSwaggerGenNewtonsoftSupport();
                //#endregion


            }
            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

        }

        /// <summary>
        /// Configure Method
        /// </summary>
        /// <param name="app">App variable</param>
        /// <param name="env">Env variable</param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();

            if (EnableDebuggingAndDocumentation)
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseSwagger();
                app.UseSwaggerUI(swaggerUIOpations =>
                {
                      swaggerUIOpations.SwaggerEndpoint("/swagger/UserManagement/swagger.json", "User Management");

                    swaggerUIOpations.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseDefaultFiles();

            }
         
            app.UseCors(AllowedSpecificOrigins);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/echo",
                    context => context.Response.WriteAsync("echo"))
                        .RequireCors(AllowedSpecificOrigins);

                endpoints.MapControllers()
                         .RequireCors(AllowedSpecificOrigins);

                endpoints.MapGet("/echo2",
                    context => context.Response.WriteAsync("echo2"));

                endpoints.MapRazorPages();
                
            });

            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup("MyTestProjectAPI")
                .UseIISIntegration()
                .UseUrls()
                .Build();
        }
    }
}
