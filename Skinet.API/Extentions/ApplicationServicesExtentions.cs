using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Skinet.API.Errors;
using Skinet.API.Helpers;

namespace Skinet.API.Extentions
{
    public static  class ApplicationServicesExtentions
    {
        public static  IServiceCollection AddApplicaitonServices(this  IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GeneritcRespostory<>));
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var error = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();
                    var errorResponse = new ApiValidationErrorReponse
                    {
                        Errors = error
                    };
                    return new BadRequestObjectResult(errorResponse);
                };


            });
            return services;
        }
    }
}
