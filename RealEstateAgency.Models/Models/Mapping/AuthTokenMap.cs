using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace RealEstateAgency.Models.Mapping
{
    public class AuthTokenMap:EntityTypeConfiguration<AuthToken>
    {
        public AuthTokenMap()
        {
            this.HasKey(t => t.TokenID);

            // Properties
            this.Property(t => t.Token)
                .IsRequired()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("AuthToken","Sec");
            this.Property(t => t.TokenID).HasColumnName("TokenID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.Token).HasColumnName("Token");
            this.Property(t => t.IssuedOn).HasColumnName("IssuedOn");
            this.Property(t => t.ExpiresOn).HasColumnName("ExpiresOn");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.AuthTokens)
                .HasForeignKey(d => d.UserID);
        }
    }
}
