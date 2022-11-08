using BCNPortal.Services.ApiRequest;

namespace BCNPortal.Services.ApiMapping
{
    public class ApiMapService : IApiMapService
    {
        //private readonly string _NRF_Address;
        // below api are compared to NRF Api answer for UDRF
        private readonly string _UDRF_Address;
        private readonly string _UDRF_getBcNodes;
        private readonly string _UDRF_sendBcNode;
        private readonly string _UDRF_getAllBcNodes;
        private readonly string _UDRF_getSingleBcNode;
        private readonly string _UDRF_getBcNodeContents;
        private readonly string _UDRF_getServicesDto;
        private readonly string _UDRF_getBcNodeContentDto;
        private readonly string _UDRF_sendBcNodeContents;
        private readonly string _UDRF_deleteRangeBcNode;
        private readonly string _UDRF_deleteRangeBcNodeContents;
        private readonly string _UDRF_getContents;
        private readonly string _UDRF_getLocations;
        private readonly string _UDRF_getAllLocations;
        private readonly string _UDRF_getSingleLocation;
        private readonly string _UDRF_sendLocation;
        private readonly string _UDRF_deleteRangeLocations;
        private readonly string _UDRF_getAllContents;
        private readonly string _UDRF_getAllServices;
        private readonly string _UDRF_getSingleContent;
        private readonly string _UDRF_sendContent;
        private readonly string _UDRF_deleteRangeContents;

        private readonly ITokenRequestService _tokenRequestService;
    }
}
