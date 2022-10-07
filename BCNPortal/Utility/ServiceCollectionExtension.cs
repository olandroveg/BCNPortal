using BCNPortal.Services.ApiRequest;
using BCNPortal.Services.BcnUser;

namespace BCNPortal.Utility
{
    public static class ServiceCollectionExtension
    {
        public static void UseInjection(this IServiceCollection services)
        {
            services.AddTransient<IApiRequestService, ApiRequestService>();
            services.AddTransient<IBcnUserService, BcnUserService>();
        }
    }
}
