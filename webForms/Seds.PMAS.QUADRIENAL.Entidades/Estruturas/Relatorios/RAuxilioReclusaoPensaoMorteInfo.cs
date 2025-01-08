using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    [DataContract]
    public class RAuxilioReclusaoPensaoMorteInfo
    {
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        [DataMember]
        public Int32 IdDrads { get; set; }
        [DataMember]
        public Int32 IdMunicipio { get; set; }

        [DataMember]
        public string Municipio { get; set; }
        [DataMember]
        public string Drads { get; set; }
        [DataMember]
        public Int32 IdRegiaoMetropolitana { get; set; }
        [DataMember]
        public Int32 IdMacroRegiao { get; set; }
        [DataMember]
        public Int16 IdNivelGestao { get; set; }
        [DataMember]
        public Int32 IdPorte { get; set; }
        [DataMember]
        public String Porte { get; set; }
        [DataMember]
        public String TipoUnidade { get; set; }
        [DataMember]
        public Int32 IdTipoUnidade { get; set; }
        [DataMember]
        public Int32 CodigoUnidade { get; set; }
        [DataMember]
        public Int32 IdDistritoSaoPaulo { get; set; }
        [DataMember]
        public String DistritoSaoPaulo { get; set; }
        [DataMember]
        public String CNPJ { get; set; }
        [DataMember]
        public String UnidadeResponsavel { get; set; }
        [DataMember]
        public String IdLocal { get; set; }
        [DataMember]
        public String LocalExecucao { get; set; }
        [DataMember]
        public String Usuarios { get; set; }
        [DataMember]
        public Int32 IdUsuarioTipoServico { get; set; }
        [DataMember]
        public String TipoServico { get; set; }
        [DataMember]
        public Int32 IdTipoServico { get; set; }
        [DataMember]
        public String ProtecaoSocial { get; set; }
        [DataMember]
        public Int16 IdTipoProtecao { get; set; }
        [DataMember]
        public Int32 IdSexo { get; set; }
        [DataMember]
        public String Sexo { get; set; }
        [DataMember]
        public Int32 IdRegiaoMoradia { get; set; }
        [DataMember]
        public String RegiaoMoradia { get; set; }
        [DataMember]
        public Int32 IdCaracteristicaTerritorio { get; set; }
        [DataMember]
        public String CaracteristicasTerritorio { get; set; }
       
        [DataMember]
        public String AtendeCriancasAuxilioReclusao { get; set; }
        [DataMember]
        public Int32 CriancasAuxilioReclusaoFeitos { get; set; }
        [DataMember]
        public Int32 CriancasAuxilioReclusaoAprovados { get; set; }
        [DataMember]
        public Int32 CriancasAuxilioReclusaoNegado { get; set; }


        [DataMember]
        public String AtendeCriancasPensaoMorte { get; set; }
        [DataMember]
        public Int32 CriancasPensaoMorteFeitos { get; set; }
        [DataMember]
        public Int32 CriancasPensaoMorteAprovados { get; set; }
        [DataMember]
        public Int32 CriancasPensaoMorteNegado { get; set; }
        [DataMember]
        public Int32 Exercicio { get; set; }
    }
}
