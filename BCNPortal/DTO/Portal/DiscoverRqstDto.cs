using System;
namespace BCNPortal.DTO.Portal
{
    public class DiscoverRqstDto
    {
        private Guid SourceNFId { get; set; }
        private Guid PortalId { get; set; }
        private Guid TargetNFId { get; set; }
        private string NFName { get; set; }
        private bool isPortal { get; set; }

        public DiscoverRqstDto(Guid portalId, string nfName, Guid targetNFId)
        {
            // this is because this is the portal, so SourceNFId is always GuidEmpty. In other NF (not portals) they should apply their own Id and let be PortalId as GuidEmpty instead
            SourceNFId = Guid.Empty;
            PortalId = portalId;
            TargetNFId = targetNFId;
            NFName = nfName;
            isPortal = true;
        }
    }
}

