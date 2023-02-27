using AbsencesAPI.Business.Services;
using AbsencesAPI.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AbsencesAPI.Business;

public class DIConfiguration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DtoEntityMapperProfile));
        services.AddScoped<IManangementService, ManagementService>();
    }
}
