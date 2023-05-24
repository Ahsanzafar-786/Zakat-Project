//using System;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Reflection;
//using System.Security.Principal;
//using System.Text;
//using AutoMapper;
//using Focus.Business;
//using Focus.Business.Infrastructure.AutoMapper;
//using Focus.Business.Users;
//using Focus.Domain.Interface;
//using Focus.External;
//using Focus.Persistence;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;

//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.IdentityModel.Tokens;
//using Noble.Api.Middlewares;
//using Rotativa.AspNetCore;
//using ScheduledTasks;

//namespace Noble.Api
//{
//    public class Startup
//    {
//        private IServiceCollection _services;
//        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

//        public Startup(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }

//        // This method gets called by the runtime. Use this method to add services to the container.
//        public void ConfigureServices(IServiceCollection services)
//        {
//            services.AddControllersWithViews().AddNewtonsoftJson();
//            services.AddCors(options =>
//            {
//                options.AddPolicy(name: MyAllowSpecificOrigins,
//                                  builder =>
//                                  {

//                                      //builder.WithOrigins(Configuration.GetSection("frontend:IpAndServerAddress").Value, "app://.").AllowAnyMethod()
//                                      //.AllowAnyHeader();

//                                      builder.WithOrigins(
//                                          Configuration.GetSection("frontend:IpAndServerAddress").Value.Split(",", StringSplitOptions.RemoveEmptyEntries).ToArray()
//                                          ).AllowAnyHeader();
//                                  });
//            });

//            services.Configure<IdentityOptions>(opts =>
//            {
//                opts.Password.RequiredLength = 3;
//                opts.Password.RequireNonAlphanumeric = false;
//                opts.Password.RequireLowercase = false;
//                opts.Password.RequireUppercase = false;
//                opts.Password.RequireDigit = false;
//            });

//            services.AddAutoMapper(typeof(AutoMapperProfile).GetTypeInfo().Assembly);
//            //  services.AddSingleton<IHttpContextAccessor, tHtpContextAccessor>();
//            services.AddPersistence(Configuration);
//            services.AddApplication();
//            services.AddInfrastructure();
//            services.AddScheduledTasks();

//            services.AddIdentity<ApplicationUser, IdentityRole>()

//                .AddEntityFrameworkStores<ApplicationDbContext>()
//                .AddDefaultTokenProviders();
//            //services.AddDefaultIdentity<ApplicationUser, ApplicationRole>()
//            //    .AddDefaultUI(UIFramework.Bootstrap4)
//            //    .AddEntityFrameworkStores<ApplicationDbContext>();
//            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//            services.AddTransient<IUserHttpContextProvider, UserHttpContextProvider>();
//            services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);
//            services.AddHttpClient();
//            // ===== Add Jwt Authentication ========
//            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
//            services
//                .AddAuthentication(options =>
//                {
//                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


//                })
//                .AddJwtBearer(cfg =>
//                {
//                    cfg.RequireHttpsMetadata = false;
//                    cfg.SaveToken = true;
//                    cfg.TokenValidationParameters = new TokenValidationParameters
//                    {
//                        ValidIssuer = Configuration.GetSection("jwt:JwtIssuer").Value,
//                        ValidAudience = Configuration.GetSection("jwt:JwtIssuer").Value,
//                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("jwt:JwtKey").Value)),
//                        ValidateLifetime = true,
//                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
//                    };
//                });


//            //services.AddSwaggerGen(c =>
//            //{
//            //    c.SwaggerDoc("V1", new OpenApiInfo
//            //    {
//            //        Version = "V1",
//            //        Title = "MY API",
//            //        Description = "Asp.net core"
//            //    });

//            //});
//            services.ConfigureApplicationCookie(options =>
//            {
//                // Cookie settings
//                options.Cookie.HttpOnly = true;
//                options.ExpireTimeSpan = TimeSpan.FromHours(1);

//                options.LoginPath = "/Identity/Account/Login";
//                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
//                options.SlidingExpiration = true;
//            });
//            //Add custom services
//            _services = services;
//        }

//        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext dbContext)
//        {

//            app.Use(async (ctx, next) =>
//            {
//                await next();
//                if (ctx.Response.StatusCode == 204)
//                {
//                    ctx.Response.ContentLength = 0;
//                }
//            });
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//                //app.UseDatabaseErrorPage();
//            }
//            else
//            {
//                app.UseExceptionHandler("/Home/Error");
//                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//                app.UseHsts();
//            }
//            //app.UseSwagger();
//            //app.UseSwaggerUI(c =>
//            //{
//            //    c.SwaggerEndpoint("/swagger/V1/swagger.json", "MY API");
//            //    //c.RoutePrefix = string.Empty;
//            //});

//            app.UseStaticFiles();
//            app.UseCookiePolicy();
//            app.UseRequestLocalization();
//            app.UseCors(MyAllowSpecificOrigins);
//            app.UseHttpsRedirection();
//            //  app.UseAuthentication();
//            app.UseCustomMiddleware();
//            app.UseRouting();
//            app.UseAuthentication();
//            app.UseAuthorization();
//            app.UseEndpoints(endpoints =>
//            {

//                endpoints.MapControllerRoute(
//                    name: "default",
//                    pattern: "{controller=Home}/{action=Index}/{id?}");
//            });
//            RotativaConfiguration.Setup(env);
//        }
//    }
//}
