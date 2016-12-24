using EFCodeFirstSample.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCodeFirstSample.Mappings
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("User");

            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.LastUpdateDate)
                .IsRequired();

            Property(x => x.LastUpdateUser)
                .IsRequired()
                .HasMaxLength(160);

            Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(160);

            Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(32);

            Property(x => x.IsActive)
                .IsRequired();
        }
    }
}
