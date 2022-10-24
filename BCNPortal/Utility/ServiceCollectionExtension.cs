﻿using BCNPortal.Services.ApiRequest;
using BCNPortal.Services.BcNodeRqst;
using BCNPortal.Services.BcnUser;
using BCNPortal.Services.Location;
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
            services.AddTransient<IBcNodeService, BcNodeService>();
            services.AddTransient<ILocationService, LocationService>();

        }
    }
}
