using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MyTestProjectAPIBaseLayer.Filters;
using System.IO;

namespace MyTestProjectAPIBaseLayer
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseStartup
    {        /// <summary>
             /// Property for Configuration.
             /// </summary>
        public IConfiguration Configuration { get; set; }

        string AllowedSpecificOrigins = "_allowSpecificOrigins";

        public string ProjectTitle { get; set; }
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
        /// <summary>
        /// Constructure with Dependency inject of configurator and environment.
        /// </summary>
        /// <param name="configuration"></param>
        public BaseStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(AllowedSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });
            });
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
            services.AddControllers().AddJsonOptions(options => {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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
                services.AddSwaggerGen(swaggeroptions =>
                {
                    swaggeroptions.SwaggerDoc("v1", new OpenApiInfo { Title = ProjectTitle + " API Documentation", Version = "v1" });
                    swaggeroptions.IncludeXmlComments(ProjectXMLFilePath);
                    swaggeroptions.OperationFilter<SwaggerCustomFilter>();
                    swaggeroptions.SchemaFilter<SwaggerExcludeFilter>();
                });
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var host = new WebHostBuilder()
               .UseKestrel()
               .UseContentRoot(Directory.GetCurrentDirectory())
               .UseIISIntegration()
               .Build();
            app.UseRouting();
            app.UseAuthorization();

            if (EnableDebuggingAndDocumentation)
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseSwagger();
                app.UseSwaggerUI(swaggerUIOpations =>
                {
                    swaggerUIOpations.SwaggerEndpoint("/swagger/v1/swagger.json", ProjectTitle + " API Documentation");
                    swaggerUIOpations.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseDefaultFiles();
                app.UseStaticFiles();
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

        }
    }
}
