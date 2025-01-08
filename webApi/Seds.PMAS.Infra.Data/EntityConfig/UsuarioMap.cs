using Seds.PMAS.Dominio.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Seds.PMAS.Infra.Data.EntityConfig
{
    public class UsuarioMap : EntityTypeConfiguration<UsuarioEntity>
    {
        public UsuarioMap()
        {
            // Primary Key
            this.HasKey(t => t.IdUsuario);

            // Properties
            this.Property(t => t.IdUsuario)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CPF)
                .HasMaxLength(15);

            this.Property(t => t.Instituicao)
                .HasMaxLength(200);

            this.Property(t => t.Cargo)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("TB_USUARIO");
            this.Property(t => t.IdUsuario).HasColumnName("ID_USUARIO");
            this.Property(t => t.IdPrefeitura).HasColumnName("ID_PREFEITURA");
            this.Property(t => t.CPF).HasColumnName("CPF");
            this.Property(t => t.IdDrads).HasColumnName("ID_DRADS");
            this.Property(t => t.IdStatus).HasColumnName("ID_STATUS");
            this.Property(t => t.Ativo).HasColumnName("ATIVO");
            this.Property(t => t.Instituicao).HasColumnName("INSTITUICAO");
            this.Property(t => t.Cargo).HasColumnName("CARGO");
            this.Property(t => t.IdPerfil).HasColumnName("ID_PERFIL");
            // Relationships
            //this.HasOptional(t => t.Nome)
            //    .WithMany(t => t.)
            //    .HasForeignKey(d => d.IdPrefeitura);
            this.HasRequired(t => t.Status)
                .WithMany(t => t.Usuarios)
                .HasForeignKey(d => d.IdStatus);

        }
    }
}
