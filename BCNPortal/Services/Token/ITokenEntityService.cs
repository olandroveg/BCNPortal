﻿namespace BCNPortal.Services.Token
{
    public interface ITokenEntityService
    {
        public Task<BCNPortal.Models.Token> AddOrUpdate(BCNPortal.Models.Token token);
        public void Delete(BCNPortal.Models.Token token);
        public bool TokenAvailability();
        public string GetToken();
    }
}