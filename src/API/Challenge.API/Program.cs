using Autofac.Extensions.DependencyInjection;
using Autofac;
using Challenge.API.Modules.Challenge;
using Serilog.Formatting.Compact;
using Serilog;
using Challenge.API.Configuration.ExecutionContext;
using Dinet.Module.Challenge.Infraestructure.Configuration;
using Dinet.Module.Challenge.Application;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Challenge.API.Configuration.Extensions;
using Challenge.API.Configuration;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Challenge.API.Modules.UsersAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Challenge.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Serilog.ILogger _logger = null;
            Serilog.ILogger _loggerForApi = null;

            var builder = WebApplication.CreateBuilder(args);

            var services = builder.Services;
            var host = builder.Host;
            var configuration = builder.Configuration;

            host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new ChallengeAutofacModule());
                });

            services.AddControllers();

            services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddEndpointsApiExplorer();

            services.AddSwaggerChallenge();

            
            ConfigureIdentityServerClient(services, configuration);
            var app = builder.Build();
            


            app.UseChallengeExceptionHandler();
            ConfigureLogger(app, ref _logger, ref _loggerForApi);

            app.UseMiddleware<CorrelationMiddleware>();

            var container = app.Services.GetAutofacRoot();
            InitializeModules(configuration, container, _logger);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();       // Luego autenticación
            app.UseAuthorization();        // Y luego autorización

            app.MapControllers();
            app.Run();
        }

        static void InitializeModules(IConfiguration configuration, ILifetimeScope container, Serilog.ILogger _logger)
        {
            string connectionstringkey = "ConnectionStrings:challenge";
            

            var httpContextAccessor = container.Resolve<IHttpContextAccessor>();
            var executionContextAccessor = new ExecutionContextAccessor(httpContextAccessor);
            var connectionstring = configuration[connectionstringkey];
            

            ChallengeStartup.Start(_logger, executionContextAccessor);
        }

        public static void ConfigureLogger(WebApplication app, ref Serilog.ILogger _logger, ref Serilog.ILogger _loggerForApi)
        {
            var con = new LoggerConfiguration()
                .Enrich.FromLogContext().WriteTo.File(new CompactJsonFormatter(), "logs/logs");

            _logger = con.CreateLogger();
            _loggerForApi = _logger.ForContext("Module", "API");
            _loggerForApi.Information("Logger configured");
        }

        public static void ConfigureIdentityServerClient(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = "https://localhost:5001";
                options.TokenValidationParameters.ValidateAudience = false;
            });

            services.AddAuthorization();

        }
        
    }
}
