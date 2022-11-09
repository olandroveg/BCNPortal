using BCNPortal.Services.ApiMapping;
using BCNPortal.Services.ApiRequest;
using BCNPortal.Services.BcNodeContent;
using BCNPortal.Services.BcNodeRqst;
using BCNPortal.Services.BcnUser;
using BCNPortal.Services.Content;
using BCNPortal.Services.IdNRF;
using BCNPortal.Services.Location;
using BCNPortal.Services.NFMapping;
using BCNPortal.Services.RegisterService;
using BCNPortal.Services.Token;

namespace BCNPortal.Utility
{
    public static class ServiceCollectionExtension
    {
        public static void UseInjection(this IServiceCollection services)
        {
            services.AddTransient<ITokenRequestService, TokenRequestService>();
            services.AddTransient<ITokenEntityService, TokenEntityService>();
            services.AddTransient<IBcnUserService, BcnUserService>();
            services.AddTransient<IBcNodeContentService, BcNodeContentService>();
            services.AddTransient<IBcNodeService, BcNodeService>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<IContentService, ContentService>();
            services.AddTransient<IIdNRFService, IdNRFService>();
            services.AddTransient<INFMapService, NFMapService>();
            services.AddTransient<IRegisterService, RegisterService>();
            services.AddTransient<IApiMapService, ApiMapService>();
        }
    }
}
