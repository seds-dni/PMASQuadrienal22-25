using Seds.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Seds.PMAS.Dominio.Entities
{
    public partial class UsuarioEntity
    {
        public Int32 IdUsuario { get; set; }

        public Int32? IdPrefeitura { get; set; }

        public String CPF { get; set; }

        public Int16? IdDrads { get; set; }

        public Int32 IdPerfil { get; set; }

        //public DradsInfo Drads { get; set; }

        public Int16? IdStatus { get; set; }

        public StatusEntity Status { get; set; }

        public Int32 Ativo { get; set; }

        public String Instituicao { get; set; }

        public String Cargo { get; set; }
        [NotMapped]
        public PrefeituraEntity Prefeitura { get; set; }
        [NotMapped]
        public DradsInfo Drads { get; set; }
        [NotMapped]
        public String Nome { get; set; }
        [NotMapped]
        public String Email { get; set; }
        [NotMapped]
        public String RG { get; set; }
        [NotMapped]
        public String OrgaoEmissor { get; set; }
        [NotMapped]
        public String UFRG { get; set; }
        [NotMapped]
        public String Telefone { get; set; }
        [NotMapped]
        public String Celular { get; set; }
        [NotMapped]
        public String Endereco { get; set; }
        [NotMapped]
        public String Numero { get; set; }
        [NotMapped]
        public String Complemento { get; set; }
        [NotMapped]
        public String Bairro { get; set; }
        [NotMapped]
        public String Cidade { get; set; }
        [NotMapped]
        public String CEP { get; set; }
        [NotMapped]
        public String UFCidade { get; set; }
        [NotMapped]
        public String Login { get; set; }
        [NotMapped]
        public Int32? IdMunicipio { get; set; }
        [NotMapped]
        public MunicipioInfo Municipio { get; set; }
        [NotMapped]
        public EPerfil Perfil { get; set; }
        [NotMapped]
        public EPerfil? EnumPerfil { get; set; }
        [NotMapped]
        public List<RecursoEntity> Funcionalidades { get; set; }
        [NotMapped]
        public Boolean TrocarSenha { get; set; }

    }
}
