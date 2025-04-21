using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Dinet.Module.Challenge.Application;
using Dinet.Module.Challenge.Domain;
using Dinet.Module.Challenge.Infraestructure.Configuration.Processing;
using System.Linq;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Abstractions;

namespace Challenge.API.Configuration.Extensions
{
    internal static class ExceptionHandlerExtensions
    {
        public static void UseChallengeExceptionHandler(
            this IApplicationBuilder app)
        {
            Dictionary<Type, Func<HttpContext, Exception, Task>> handlers = new Dictionary<Type, Func<HttpContext, Exception, Task>>();

            handlers.Add(typeof(BusinessRuleValidationException), BusinessRuleValidationExceptionHandle);

            handlers.Add(typeof(InvalidCommandException), InvalidCommandExceptionHandle);

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {

                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    

                    if (handlers.ContainsKey(exceptionHandlerPathFeature.Error.GetType()))
                    {
                        try
                        {
                            await handlers[exceptionHandlerPathFeature.Error.GetType()].Invoke(context, exceptionHandlerPathFeature.Error);
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                        
                    }
                    else
                    {
                        context.Response.StatusCode = 500;

                        context.Response.ContentType = "application/json";

                        Exception ex = exceptionHandlerPathFeature.Error ;

                        var or = new { message = "Internal server error.", exception = ex };

                        await context.Response.WriteAsync(ToJson(or));
                    }


                });
            });
        }

        private static async Task BusinessRuleValidationExceptionHandle(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;

            context.Response.ContentType = "application/json";

            var result = OperationResult.WithError(((BusinessRuleValidationException)ex).Message);

            await context.Response.WriteAsync(ToJson(result));
        }
        private static async Task InvalidCommandExceptionHandle(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            context.Response.ContentType = "application/json";

            var error = string.Join(Environment.NewLine,((InvalidCommandException)ex).Errors.ToArray());
            var result = OperationResult.WithError(error);
            
            await context.Response.WriteAsync(ToJson(result));
        }


        private static string ToJson(object value)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            return JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
        }
    }
}
