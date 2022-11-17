using System;
using BCNPortal.Models;

namespace BCNPortal.Services.Discovery
{
    public interface IDiscoveryService
    {
        public Task DiscoverAllApiOfNF(TokenPlusId tokenPlusId, string targetNFName, Guid targetNFId);
    }
}

