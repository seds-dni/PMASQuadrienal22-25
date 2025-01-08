using Seds.PMAS.Dominio.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Seds.PMAS.Infra.Data.EntityConfig
{
    public class PrefeitoMap : EntityTypeConfiguration<PrefeitoEntity>
    {
        public PrefeitoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.RG)
                .HasMaxLength(20);

            this.Property(t => t.RGDigito)
                .HasMaxLength(2);

            this.Property(t => t.SiglaEmissor)
                .HasMaxLength(10);

            this.Property(t => t.CPF)
                .HasMaxLength(15);

            this.Property(t => t.Email)
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("TB_PREFEITO");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.IdPrefeitura).HasColumnName("ID_PREFEITURA");
            this.Property(t => t.Nome).HasColumnName("NOME");
            this.Property(t => t.RG).HasColumnName("RG");
            this.Property(t => t.RGDigito).HasColumnName("RG_DIGITO");
            this.Property(t => t.DataEmissao).HasColumnName("RG_DT_EMISSAO");
            this.Property(t => t.IdUFRG).HasColumnName("ID_UF_RG");
            this.Property(t => t.SiglaEmissor).HasColumnName("SIGLA_EMISSOR");
            this.Property(t => t.CPF).HasColumnName("CPF");
            this.Property(t => t.InicioMandato).HasColumnName("MANDATO_INICIO");
            this.Property(t => t.TerminoMandato).HasColumnName("MANDATO_TERMINO");
            this.Property(t => t.Email).HasColumnName("EMAIL");
            this.Property(t => t.Status).HasColumnName("ID_STATUS");
        }
    }
}
