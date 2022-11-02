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
    public class TokenConfiguration : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.ToTable("Token");
            builder.HasKey(e => e.Id);

        }
    }
    public class APImappingConfiguration : IEntityTypeConfiguration<APImapping>
    {
        public void Configure(EntityTypeBuilder<APImapping> builder)
        {
            builder.ToTable("APImapping");
            builder.HasKey(e => e.Id);

        }
    }
    public class NFmappingConfiguration : IEntityTypeConfiguration<NFmapping>
    {
        public void Configure(EntityTypeBuilder<NFmapping> builder)
        {
            builder.ToTable("NFmapping");
            builder.HasKey(e => e.Id);
            builder.HasMany(e=> e.Apis).WithOne(e=> e.NF).HasForeignKey(e=>e.NFId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
