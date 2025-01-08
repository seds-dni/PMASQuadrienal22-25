using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    [DataContract]
    public class RedeServicoSocioassistencialRegionalizadosRelatorio
    {

        [DataMember]
        public Int32 CodigoUnidade { get; set; }
        
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        [DataMember]
        public Int32 IdMunicipio { get; set; }
        [DataMember]
        public string Municipio { get; set; }

        [DataMember]
        public Int32 IdDrads { get; set; }
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
        public Int32 IdDistritosSaoPaulo { get; set; }

        [DataMember]
        public String DistritoSaoPaulo { get; set; }

        [DataMember]
        public Int32 IdTipoUnidade { get; set; }
        [DataMember]
        public String TipoUnidade { get; set; }

        [DataMember]
        public String UnidadeResponsavel { get; set; }


        [DataMember]
        public String IdLocal { get; set; }
        
        [DataMember]
        public String LocalExecucao { get; set; }

        [DataMember]
        public String Cidade { get; set; }

        [DataMember]
        public Int32 IdUsuarioTipoServico { get; set; }
        
        [DataMember]
        public String Usuarios { get; set; }


        [DataMember]
        public Int16 IdTipoProtecao { get; set; }
        
        [DataMember]
        public String ProtecaoSocial { get; set; }

        [DataMember]
        public Int32 IdTipoServico { get; set; }
        
        [DataMember]
        public String TipoServico { get; set; }
        

        [DataMember]
        public String DataFuncionamentoServico { get; set; }
        
        [DataMember]
        public String DataDesativacao { get; set; }

        [DataMember]
        public Int32 IdAbrangencia { get; set; }
        
        [DataMember]
        public String Abrangencia { get; set; }

        [DataMember]
        public String MunicipioSedeServico { get; set; }
       
        [DataMember]
        public String SedeServico { get; set; }

        [DataMember]
        public String IndicaMunicipiosParticipamOfertaServico { get; set; }
        
        [DataMember]
        public String FormaJuridica { get; set; }
        
        [DataMember]
        public String NomeConsorcio { get; set; }
        
        [DataMember]
        public String CNPJ { get; set; }
        
        [DataMember]
        public String MunicipioSede { get; set; }
        
        [DataMember]
        public String MunicipiosEnvolvidos { get; set; }

        [DataMember]
        public Boolean? ServicoNaoTipificado { get; set; }

        [DataMember]
        public String CaracteristicasTerritorio { get; set; }

        [DataMember]
        public Int32 IdCaracteristicasTerritorio { get; set; }

        [DataMember]
        public Int32 IdRegiaoMoradia { get; set; }

        [DataMember]
        public Int32 IdSexo { get; set; }

        [DataMember]
        public String Sexo { get; set; }

    }
}
