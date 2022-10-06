using BCNPortal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BCNPortal.Data
{
    public class BcnUserAccountConfiguration : IEntityTypeConfiguration<BcnUserAccount>
    {
        public void Configure(EntityTypeBuilder<BcnUserAccount> builder)
        {
            builder.ToTable("BcnUserAccount");
            builder.HasKey(e => e.Id);

        }
    }
}
