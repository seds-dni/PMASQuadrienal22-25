using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class MantenedoraProSocialInfo
    {
        [DataMember]
        public String CNPJ { get; set; }
        [DataMember]
        public String Situacao { get; set; }
        
        private String _motivoSituacao;
        [DataMember]
        public String MotivoSituacao { get { return !String.IsNullOrWhiteSpace(_motivoSituacao) ? _motivoSituacao : ""; } set { _motivoSituacao = value; } }

        [DataMember]
        public String RazaoSocial { get; set; }
        [DataMember]
        public String NomeFantasia { get; set; }
        [DataMember]
        public String Endereco { get; set; }
        [DataMember]
        public String Municipio { get; set; }

        private String _cep;
        [DataMember]
        public String CEP { get { return !String.IsNullOrWhiteSpace(_cep) ? _cep : ""; } set { _cep = value; } }

        private String _numero;
        [DataMember]
        public String Numero { get { return !String.IsNullOrWhiteSpace(_numero) ? _numero : ""; } set { _numero = value; } }

        private String _complemento;
        [DataMember]
        public String Complemento { get { return !String.IsNullOrWhiteSpace(_complemento) ? _complemento : ""; } set { _complemento = value; } }

        private String _bairro;
        [DataMember]
        public String Bairro { get { return !String.IsNullOrWhiteSpace(_bairro) ? _bairro : ""; } set { _bairro = value; } }

        private String _telefone;
        [DataMember]
        public String Telefone { get { return !String.IsNullOrWhiteSpace(_telefone) ? _telefone : ""; } set { _telefone = value; } }

        private String _fax;
        [DataMember]
        public String Celular { get { return !String.IsNullOrWhiteSpace(_fax) ? _fax : ""; } set { _fax = value; } }

        private String _email;
        [DataMember]
        public String Email { get { return !String.IsNullOrWhiteSpace(_email) ? _email : ""; } set { _email = value; } }

        private String _homePage;
        [DataMember]
        public String HomePage { get { return !String.IsNullOrWhiteSpace(_homePage) ? _homePage : ""; } set { _homePage = value; } }

        private String _responsavel;
        [DataMember]
        public String Responsavel { get { return !String.IsNullOrWhiteSpace(_responsavel) ? _responsavel : ""; } set { _responsavel = value; } }

        private String _cargo;
        [DataMember]
        public String Cargo { get { return !String.IsNullOrWhiteSpace(_cargo) ? _cargo : ""; } set { _cargo = value; } }

        [DataMember]
        public String DataInicioMandato { get; set; }
        [DataMember]
        public String DataTerminoMandato { get; set; }
        [DataMember]
        public Int32? IdAreaAtuacao { get; set; }
        [DataMember]
        public Int32? IdFormaAtuacao { get; set; }

        private String _inscricaoCMAS;
        [DataMember]
        public String InscricaoCMAS { get { return !String.IsNullOrWhiteSpace(_inscricaoCMAS) ? _inscricaoCMAS : ""; } set { _inscricaoCMAS = value; } }
        [DataMember]
        public DateTime? DataPublicacao { get; set; }
        [DataMember]
        public DateTime? DataValidade { get; set; }
    }
}