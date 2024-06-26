﻿using BCNPortal.Models;

namespace BCNPortal.Services.ApiRequest
{
    public interface ITokenRequestService
    {
        public Task<TokenPlusId> ManageToken(string username, string password, Guid userId);
    }
}
