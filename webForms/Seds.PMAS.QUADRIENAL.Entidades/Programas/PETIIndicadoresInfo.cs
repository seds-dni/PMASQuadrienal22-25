using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class PETIIndicadoresInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdMunicipio { get; set; }
        [DataMember]
        public Int32 NumCriancasAdolescentesTrabalhoInfantilCenso2010 { get; set; }
        [DataMember]
        public Decimal PctCriancasAdolescentesTrabalhoInfantilCenso2010 { get; set; }
        [DataMember]
        public int? Idade1013Ano2017 { get; set; }
        [DataMember]
        public int? Idade1415Ano2017 { get; set; }
        [DataMember]
        public int? Idade1617Ano2017 { get; set; }

        [DataMember]
        public int? Idade1013Ano2021 { get; set; }
        [DataMember]
        public int? Idade1415Ano2021 { get; set; }
        [DataMember]
        public int? Idade1617Ano2021 { get; set; }
        
        [DataMember]
        public int? Idade1013Ano2018 { get; set; }
        [DataMember]
        public int? Idade1415Ano2018 { get; set; }
        [DataMember]
        public int? Idade1617Ano2018 { get; set; }

        [DataMember]
        public int? Idade1013Ano2022 { get; set; }
        [DataMember]
        public int? Idade1415Ano2022 { get; set; }
        [DataMember]
        public int? Idade1617Ano2022 { get; set; }        
        
        [DataMember]
        public int? Idade1013Ano2019 { get; set; }
        [DataMember]
        public int? Idade1415Ano2019 { get; set; }
        [DataMember]
        public int? Idade1617Ano2019 { get; set; }

        [DataMember]
        public int? Idade1013Ano2023 { get; set; }
        [DataMember]
        public int? Idade1415Ano2023 { get; set; }
        [DataMember]
        public int? Idade1617Ano2023 { get; set; }        
        
        [DataMember]
        public int? Idade1013Ano2020 { get; set; }
        [DataMember]
        public int? Idade1415Ano2020 { get; set; }
        [DataMember]
        public int? Idade1617Ano2020 { get; set; }

        [DataMember]
        public int? Idade1013Ano2024 { get; set; }
        [DataMember]
        public int? Idade1415Ano2024 { get; set; }
        [DataMember]
        public int? Idade1617Ano2024 { get; set; }        
        
        [DataMember]
        public Int32? MetaMunicipal2017 { get; set; }
        [DataMember]
        public Int32? MetaMunicipal2018 { get; set; }
        [DataMember]
        public Int32? MetaMunicipal2019 { get; set; }
        [DataMember]
        public Int32? MetaMunicipal2020 { get; set; }

        [DataMember]
        public Int32? MetaMunicipal2021 { get; set; }
        [DataMember]
        public Int32? MetaMunicipal2022 { get; set; }
        [DataMember]
        public Int32? MetaMunicipal2023 { get; set; }
        [DataMember]
        public Int32? MetaMunicipal2024 { get; set; }

    }
}
