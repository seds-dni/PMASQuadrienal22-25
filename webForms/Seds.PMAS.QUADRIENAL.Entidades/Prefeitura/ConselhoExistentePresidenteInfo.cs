using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class ConselhoMunicipalExistentePresidenteInfo
    {
        public Int32 Id { get; set; }

        public Int32 IdPrefeitura { get; set; }

        public Int32 IdConselho { get; set; }

        public String Nome { get; set; }

        public String Cpf { get; set; }

        public String RG { get; set; }

        public String RGDigito { get; set; }

        public Int16 IDUFRG { get; set; }

        public String SiglaEmissor { get; set; }

        public DateTime? DataEmissao { get; set; }

        public DateTime? MandatoInicio { get; set; }

        public DateTime? MandatoTermino { get; set; }

        public Int16 IdStatus { get; set; }

        public ConselhosInfo Conselho { get; set; }

        public PrefeituraInfo Prefeitura { get; set; }

        public StatusInfo Status { get; set; }

    }
}
