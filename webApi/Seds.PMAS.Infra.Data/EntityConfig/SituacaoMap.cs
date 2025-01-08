using Seds.PMAS.Dominio.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Seds.PMAS.Infra.Data.EntityConfig
{
    public class SituacaoMap : EntityTypeConfiguration<SituacaoEntity>
    {
        public SituacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TB_SITUACAO");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.Nome).HasColumnName("NOME");
        }
    }
}
