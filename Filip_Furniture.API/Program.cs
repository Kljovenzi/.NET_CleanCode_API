using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Serilog;
using Filip_Furniture.Data;
using Filip_Furniture.Service;
using Asp.Versioning;
using Filip_Furniture.Service.Helpers.Implementations;
using Filip_Furniture.Service.Helpers.Interfaces;

namespace Filip_Furniture.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //Serilog
            Log.Logger = new LoggerConfiguration()
                           .MinimumLevel.Debug()
                           .WriteTo.Console()
                           .WriteTo.File("logs/PPEPDV.txt", rollingInterval: RollingInterval.Day)
                           .CreateLogger();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Filip.Furniture", Version = "v1" });

                }
            );


            builder.Services.AddHealthChecks();


            builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
            {
                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));
            //builder.Configuration.AddConfiguration(builder.Configuration);
            builder.Services.AddDataDependencies(builder.Configuration);
            builder.Services.AddServiceDependencies(builder.Configuration);


            //builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IRestHelper, RestHelper>();

            builder.Services.AddControllersWithViews();


            builder.Services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;

            });


            builder.Services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(builder.Configuration.GetValue<int>("HealthCheckConfig:EvaluationTimeInSeconds")); //time in seconds between check    
                opt.MaximumHistoryEntriesPerEndpoint(builder.Configuration.GetValue<int>("HealthCheckConfig:MaxHistoryPerEndpoint")); //maximum history of checks    
                opt.SetApiMaxActiveRequests(builder.Configuration.GetValue<int>("HealthCheckConfig:ApiMaxActiveRequest")); //api requests concurrency    
                opt.AddHealthCheckEndpoint("default api", builder.Configuration.GetValue<string>("HealthCheckConfig:HealthCheckEndpoint")); //map health check api  

                //bypass ssl
                opt.UseApiEndpointHttpMessageHandler(sp =>
                {
                    return new HttpClientHandler
                    {
                        ClientCertificateOptions = ClientCertificateOption.Manual,
                        ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
                    };
                });
            });

            var app = builder.Build();



            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint("/swagger/v1/swagger.json", "Filip.Home");

                    x.RoutePrefix = string.Empty;
                });
            }

            //app.UseMiddleware<EncryptionMiddleware>();

            app.UseCors("corsapp");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseHealthChecksUI();

            app.MapHealthChecks(builder.Configuration.GetValue<string>("HealthCheckConfig:HealthCheckEndpoint"), new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.Run();
        }
    }
}
