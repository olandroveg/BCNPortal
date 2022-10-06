using BCNPortal.Services.ApiRequest;

namespace BCNPortal.Utility
{
    public static class ServiceCollectionExtension
    {
        public static void UseInjection(this IServiceCollection services)
        {
            services.AddTransient<IApiRequest, ApiRequest>();
        }
    }
}
