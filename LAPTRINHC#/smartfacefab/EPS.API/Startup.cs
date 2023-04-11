using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using EPS.Data;
using EPS.API.Models;
using EPS.Data.Entities;
using EPS.API.Helpers;
using AutoMapper;
using EPS.Utils.Repository.Audit;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using EPS.Service;
using Microsoft.AspNetCore.Authorization;
using EPS.Service.Helpers;
using EPS.Service.Models;
using EPS.Service.Dtos.Event;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using DevExpress.AspNetCore;
using DevExpress.XtraReports.Web.Extensions;
using EPS.API.Services;
using System.Net.WebSockets;
using System.Threading;
using Newtonsoft.Json;

namespace EPS.API
{
    public class Startup
    {
        List<WSConnectionModel> lstWS = new List<WSConnectionModel>();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDevExpressControls();
            services.AddScoped<ReportStorageWebExtension, CustomReportStorageWebExtension>();
            services.AddAutoMapper(typeof(EPSBaseService));
            services.AddDbContext<EPSContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("EPS")));
            services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();

            // using Microsoft.AspNetCore.DataProtection;
            services.AddDataProtection().PersistKeysToDbContext<EPSContext>();

            services.Configure<Audience>(Configuration.GetSection("Audience"));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            //services.AddIdentity<USERS, MASTER_DATAS>();
            services.AddIdentityCore<User>(options =>
            {
                options.User.RequireUniqueEmail = false;
            })
            .AddEntityFrameworkStores<EPSContext>()
            .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 3;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                //options.User.RequireUniqueEmail = true;
            });
            //schedule
            //var scheduler = StdSchedulerFactory.GetDefaultScheduler().GetAwaiter().GetResult();
            //services.AddSingleton(scheduler);
            //services.AddSingleton<IJobFactory, CustomQuartzJobFactory>();
            //services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            //services.AddSingleton<NotificationJob>();

            ////cron expresssion 
            //const string EVERY_MINUTE = "0 * * ? * *";
            //const string EVERY_30_MINUTE = "0 */30 * ? * *";
            //const string EVERY_HOUR = "0 0 * ? * *";

            //services.AddSingleton(new JobMetadata(Guid.NewGuid(), typeof(NotificationJob), "Notification Job", EVERY_HOUR));
            //services.AddHostedService<CustomQuartzHostedService>();

            services.AddTransient(typeof(Lazy<>), typeof(Lazier<>));
            services.AddScoped(x => x.GetService<IHttpContextAccessor>().HttpContext.User);
            services.AddScoped<IUserIdentity<int>, UserIdentity>();
            services.AddScoped<EPSRepository>();
            services.AddScoped<EPSBaseService>();
            services.AddScoped<AuthorizationService>();
            services.AddScoped<LookupService>();
            services.AddScoped<CompanyService>();
            services.AddScoped<MachineService>();
            services.AddScoped<AppParamService>();
            services.AddScoped<GroupService>();
            services.AddScoped<DepartmentService>();
            services.AddScoped<GroupAccessService>();
            services.AddScoped<AccessTimeSegservice>();
            services.AddScoped<PersonService>();
            services.AddScoped<FaceService>();
            services.AddScoped<EventService>();
            services.AddScoped<ReportWorkingTimeService>();
            services.AddScoped<ShiftTimeService>();
            services.AddScoped<GuessService>();
            services.AddScoped<PersonsAccessService>();
            services.AddScoped<WorkingShiftTimeService>();
            services.AddScoped<NotificationSystemService>();
            services.AddScoped<HolidaysService>();
            services.AddScoped<DayOffService>();
            services.AddScoped<WorkCalendarService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<OverTimeService>();
            services.AddScoped<TimeKeepingService>();
            services.AddTransient<MailService>();
            services.AddScoped<RegisterWorkingShiftService>();
            services.AddScoped<CalculateWorkingHourService>();
            services.AddScoped<WorkingHoursService>();
            services.AddScoped<CheckDataService>();
            services.AddScoped<OverTimeHoursService>();
            services.AddScoped<FAQuestionsService>();
            services.AddScoped<GradesService>();
            services.AddScoped<ApiService>();
            services.AddScoped<AreasService>();
            services.AddScoped<QRCodePersonService>();
            services.AddScoped<PersonsService>();
            services.AddScoped<FaceMatchService>();
            services.AddScoped<HomeService>();
            services.AddScoped<DeviceService>();
            services.AddScoped<UpdateNotificationService>();
            services.AddScoped<MenuService>();
            services.AddScoped<PreOrderService>();

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;
            });

            ConfigureJwtAuthService(services);

            services.AddMvc(x => x.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddNewtonsoftJson();

            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    if (context.Request.Path.StartsWithSegments("/api") && context.Response.StatusCode == (int)HttpStatusCode.OK)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    }
                    else
                    {
                        context.Response.Redirect(context.RedirectUri);
                    }
                    return Task.FromResult(0);
                };
            });
            services.AddCors(options =>
            {
                options.AddPolicy("_myAllowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:8080", "https://smartfactory.atin.vn")
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .SetIsOriginAllowed((x) => true)
                               .AllowCredentials();
                    });
            });
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("_myAllowSpecificOrigins");
            app.UseRouting();
            app.UseAuthentication();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();

            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120),
            };
            app.UseEndpoints(endpoint =>
            {
                endpoint.MapHub<EventHub>("/eventHub");
            });

            app.UseWebSockets();

            // <snippet_AcceptWebSocket>
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/ws")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        using (WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync())
                        {
                            await Echo(context, webSocket, logger);
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    await next();
                }

            });

            DbInitializer.Initialize(app.ApplicationServices);
        }

        // <snippet_Echo>
        private async Task Echo(HttpContext context, WebSocket webSocket, ILogger<Startup> logger)
        {
            var buffer = new byte[1024 * 1024];

            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                string jsonStr = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);

                var messageModel = JsonConvert.DeserializeObject<WSMessageModel>(jsonStr);

                switch (messageModel.EVENT_ID)
                {
                    //Connect
                    case 1:
                        WSConnectionModel connectModel = new WSConnectionModel();
                        connectModel.Connection = webSocket;
                        connectModel.DEP_ID = messageModel.DEP_ID;
                        connectModel.DEVICE_CODE = messageModel.DEVICE_CODE;
                        lstWS.Add(connectModel);
                        break;
                    //Event checkin
                    case 2:
                        var deviceTarget = lstWS.Where(item => item.DEP_ID == messageModel.DEP_ID && item.DEVICE_CODE == messageModel.DEVICE_CODE && item.Connection.State.ToString() == "Open").FirstOrDefault();

                        if (deviceTarget != null)
                        {
                            await deviceTarget.Connection.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(jsonStr)), result.MessageType, result.EndOfMessage, CancellationToken.None);
                        }
                        break;
                    default:
                        break;
                }

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            var lstClosed = lstWS.Where(item => item.Connection.State.ToString() == "Closed").ToList();
            foreach (var i in lstClosed)
                lstWS.Remove(i);
        }

        // </snippet_Echo>

        public void ConfigureJwtAuthService(IServiceCollection services)
        {
            var audienceConfig = Configuration.GetSection("Audience");
            var symmetricKeyAsBase64 = audienceConfig["Secret"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Iss"],

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = audienceConfig["Aud"],

                // Validate the token expiry
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                //o.Authority = "";
                //o.Audience = "";
                o.TokenValidationParameters = tokenValidationParameters;
            });
        }
    }

    public class Lazier<T> : Lazy<T> where T : class
    {
        public Lazier(IServiceProvider provider)
            : base(() => provider.GetRequiredService<T>())
        {
        }
    }
}
