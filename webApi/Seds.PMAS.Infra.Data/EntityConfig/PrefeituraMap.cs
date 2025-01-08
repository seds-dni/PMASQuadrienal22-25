using Seds.PMAS.Dominio.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Seds.PMAS.Infra.Data.EntityConfig
{
    public class PrefeituraMap : EntityTypeConfiguration<PrefeituraEntity>
    {
        public PrefeituraMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.CNPJ)
                .IsRequired()
                .HasMaxLength(18);

            this.Property(t => t.Cep)
                .IsRequired()
                .HasMaxLength(9);

            this.Property(t => t.Logradouro)
                .IsRequired()
                .HasMaxLength(70);

            this.Property(t => t.Numero)
                .HasMaxLength(20);

            this.Property(t => t.Complemento)
                .HasMaxLength(20);

            this.Property(t => t.Bairro)
                .HasMaxLength(40);

            this.Property(t => t.Telefone)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.WebSite)
                .HasMaxLength(60);

            this.Property(t => t.Email)
                .HasMaxLength(200);

            this.Property(t => t.Celular)
                .HasMaxLength(11);

            // Table & Column Mappings
            this.ToTable("TB_PREFEITURA");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.IdMunicipio).HasColumnName("ID_MUNICIPIO");
            this.Property(t => t.IdNivelGestao).HasColumnName("ID_NIVEL_GESTAO");
            this.Property(t => t.IdSituacao).HasColumnName("ID_SITUACAO");
            this.Property(t => t.CNPJ).HasColumnName("CNPJ");
            this.Property(t => t.DataPublicacao).HasColumnName("DT_PUBLICACAO");
            this.Property(t => t.Cep).HasColumnName("CEP");
            this.Property(t => t.Logradouro).HasColumnName("LOGRADOURO");
            this.Property(t => t.Numero).HasColumnName("NUMERO");
            this.Property(t => t.Complemento).HasColumnName("COMPLEMENTO");
            this.Property(t => t.Bairro).HasColumnName("BAIRRO");
            this.Property(t => t.Telefone).HasColumnName("TEL");
            this.Property(t => t.WebSite).HasColumnName("WEB_SITE");
            this.Property(t => t.Email).HasColumnName("EMAIL");
            this.Property(t => t.Populacao).HasColumnName("POPULACAO");
            this.Property(t => t.IdPrefeituraAnoAnterior).HasColumnName("ID_PREFEITURA_ANO_ANTERIOR");
            this.Property(t => t.Bloqueado).HasColumnName("BLOQUEADA");
            this.Property(t => t.Caracterizacao).HasColumnName("CARACTERIZACAO");
            this.Property(t => t.PossuiSite).HasColumnName("POSSUI_SITE");
            this.Property(t => t.Revisao).HasColumnName("REVISAO");
            this.Property(t => t.Cidade).HasColumnName("CIDADE");
            this.Property(t => t.CaracterizacaoPopulacao).HasColumnName("CARACTERIZACAO_POPULACAO");
            this.Property(t => t.CaracterizacaoRedeSocioassistencial).HasColumnName("CARACTERIZACAO_REDE_SOCIOASSISTENCIAL");
            this.Property(t => t.CaracterizacaoAnaliseInterpretacao).HasColumnName("CARACTERIZACAO_ANALISE_INTERPRETACAO");
            this.Property(t => t.JustificativaAcaoPlanejamento).HasColumnName("JUSTIFICATIVA_ACAO_PLANEJAMENTO");
            this.Property(t => t.DesbloquearValoresDrads).HasColumnName("DESBLOQUEAR_VALORES_DRADS");
            this.Property(t => t.ValoresReprogramadosDrads).HasColumnName("VALORES_REPROGRAMADOS_DRADS");
            this.Property(t => t.Celular).HasColumnName("CELULAR");

            // Relationships
            this.HasRequired(t => t.NivelGestao)
                .WithMany(t => t.Prefeitura)
                .HasForeignKey(d => d.IdNivelGestao);
            this.HasRequired(t => t.Situacao)
                .WithMany(t => t.Prefeituras)
                .HasForeignKey(d => d.IdSituacao);

        }
    }
}
