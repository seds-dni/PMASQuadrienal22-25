using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    public class RelatorioAEPETIInfo
    {
        public Int32 IdPrefeitura { get; set; }
        
        public Int32 IdMunicipio { get; set; }
        
        public String Municipio { get; set; }

        public String Drads { get; set; }

        public Int32 IdRegiaoMetropolitana { get; set; }

        public Int32 IdDrads { get; set; }

        public Int16 IdNivelGestao { get; set; }

        public Int32 IdMacroRegiao { get; set; }

        public Int32 IdPorte { get; set; }

        public String Porte { get; set; }

        public String PetiAderiuCofinanciamentoFederal { get; set; }

        public DateTime PetiDataAdesao { get; set; }

        public Decimal ValorAepeti { get; set; }

        public String NomeGestorAcao { get; set; }

        public String Telefone { get; set; }

        public String Email { get; set; }

        public Int32 Idade10a13Ano2021 { get; set; }

        public Int32 Idade14a15Ano2021 { get; set; }

        public Int32 Idade16a17Ano2021 { get; set; }


        public Int32 Idade10a13Ano2022 { get; set; }

        public Int32 Idade14a15Ano2022 { get; set; }

        public Int32 Idade16a17Ano2022 { get; set; }


        public Int32 Idade10a13Ano2023 { get; set; }

        public Int32 Idade14a15Ano2023 { get; set; }

        public Int32 Idade16a17Ano2023 { get; set; }


        public Int32 Idade10a13Ano2024 { get; set; }

        public Int32 Idade14a15Ano2024 { get; set; }

        public Int32 Idade16a17Ano2024 { get; set; }


        public Int32 MetaMunicipal2021 { get; set; }

        public Int32 MetaMunicipal2022 { get; set; }
        
        public Int32 MetaMunicipal2023 { get; set; }

        public Int32 MetaMunicipal2024 { get; set; }

        public String PetiAcoesTrabalhoInfantil { get; set; }

        public String Eixo { get; set; }

        public String Acao { get; set; }

        public String PeriodoRealizacao { get; set; }

    }
}
