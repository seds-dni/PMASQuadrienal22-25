using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class ProgramaProjetoPrevisaoAnualBeneficiariosInfo
    {
        public Int32 Id { get; set; }
        public Int32 IdPrograma { get; set; }

        public Int32 MetaPactuadaExercicio1 { get; set; }
        public Decimal PrevisaoAnualRepasseExercicio1 { get; set; }

        public Int32 MetaPactuadaExercicio2 { get; set; }
        public Decimal PrevisaoAnualRepasseExercicio2 { get; set; }

        public Int32 MetaPactuadaExercicio3 { get; set; }
        public Decimal PrevisaoAnualRepasseExercicio3 { get; set; }

        public Int32 MetaPactuadaExercicio4 { get; set; }
        public Decimal PrevisaoAnualRepasseExercicio4 { get; set; }

        public Int32 MetaPactuadaExercicio2018 { get; set; }
        public Decimal PrevisaoAnualRepasseExercicio2018 { get; set; }

        public Int32 MetaPactuadaExercicio2019 { get; set; }
        public Decimal PrevisaoAnualRepasseExercicio2019 { get; set; }

        public Int32 MetaPactuadaExercicio2020 { get; set; }
        public Decimal PrevisaoAnualRepasseExercicio2020 { get; set; }

        public Int32 MetaPactuadaExercicio2021 { get; set; }
        public Decimal PrevisaoAnualRepasseExercicio2021 { get; set; }


        public Decimal PrevisaoAnualMunicipalExercicio1 { get { return PrevisaoAnualRepasseExercicio1 * 12; } }
        public Decimal PrevisaoAnualMunicipalExercicio2 { get { return PrevisaoAnualRepasseExercicio2 * 12; } }
        public Decimal PrevisaoAnualMunicipalExercicio3 { get { return PrevisaoAnualRepasseExercicio3 * 12; } }
        public Decimal PrevisaoAnualMunicipalExercicio4 { get { return PrevisaoAnualRepasseExercicio4 * 12; } }
       
    }
}
