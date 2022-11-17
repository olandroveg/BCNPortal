using BCNPortal.Data;
using BCNPortal.Models;
using BCNPortal.Services.ApiRequest;
using BCNPortal.Utility;

namespace BCNPortal.Services.ApiMapping
{
    public class ApiMapService : IApiMapService
    {
        //private readonly string _NRF_Address;
        // below api are compared to NRF Api answer for UDRF
        private readonly string _UDRF_Address = StaticConfigurationManager.AppSetting["ApiAddress:NRF_Address"];
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

        private readonly ApplicationDbContext _context;

        public ApiMapService (ApplicationDbContext context)
        {
            _context = context;
            
        }
        public void DeleteRange (IEnumerable<APImapping> apiMappings)
        {
            try
            {
                if (!_context.APImapping.All(x => x != null))
                    throw new ArgumentException(nameof(apiMappings));
                _context.APImapping.RemoveRange(apiMappings);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
