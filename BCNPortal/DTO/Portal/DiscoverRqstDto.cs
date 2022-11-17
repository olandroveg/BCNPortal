using System;
namespace BCNPortal.DTO.Portal
{
    public class DiscoverRqstDto
    {
        public Guid SourceNFId { get; set; }
        public Guid PortalId { get; set; }
        public Guid TargetNFId { get; set; }
        public string NFName { get; set; }
        public bool isPortal { get; set; }

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

