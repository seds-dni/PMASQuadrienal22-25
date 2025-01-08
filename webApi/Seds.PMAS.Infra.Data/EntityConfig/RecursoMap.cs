using Seds.PMAS.Dominio.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Seds.PMAS.Infra.Data.EntityConfig
{
    public class RecursoMap : EntityTypeConfiguration<RecursoEntity>
    {
        public RecursoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("TB_RECURSO");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.Nome).HasColumnName("NOME");
            this.Property(t => t.Pagina).HasColumnName("PAGINA");
            this.Property(t => t.IdPai).HasColumnName("ID_PAI");
            this.Property(t => t.Ordem).HasColumnName("ORDEM");

            // Relationships
            //this.HasOptional(t => t)
            //    .WithMany(t => t.TB_RECURSO1)
            //    .HasForeignKey(d => d.ID_PAI);

        }
    }
}
