using Seds.PMAS.Dominio.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Seds.PMAS.Infra.Data.EntityConfig
{
    public class RecursoPerfilMap : EntityTypeConfiguration<RecursoPerfilEntity>
    {
        public RecursoPerfilMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IdRecurso, t.IdPerfil });

            // Propertiesk
            this.Property(t => t.IdRecurso)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IdPerfil)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("TB_RECURSOxPERFIL");
            this.Property(t => t.IdRecurso).HasColumnName("ID_RECURSO");
            this.Property(t => t.IdPerfil).HasColumnName("ID_PERFIL");

            // Relationships
            this.HasRequired(t => t.Recurso)
                .WithMany(t => t.Perfis)
                .HasForeignKey(d => d.IdRecurso);

        }
    }
}
