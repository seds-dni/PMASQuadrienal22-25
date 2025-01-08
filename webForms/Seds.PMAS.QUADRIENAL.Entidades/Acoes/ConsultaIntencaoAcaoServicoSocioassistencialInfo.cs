using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class ConsultaIntencaoAcaoServicoSocioassistencialInfo
    {
        public Int32 IdPrefeitura { get; set; }
        public String TipoRede { get; set; }
        public String Unidade { get; set; }
        public String TipoLocal { get; set; }
        public Int32 IdLocalExecucao { get; set; }
        public String LocalExecucao { get; set; }
        public String Usuario { get; set; }
        public Int32 IdUsuarioTipoServico { get; set; }
        public Int32 IdTipoServico { get; set; }
        public String TipoServico { get; set; }
        public Int16 IdTipoProtecao { get; set; }
        public String ProtecaoSocial { get; set; }
        public Int32 IdAvaliacaoServico { get; set; }
        public String AvaliacaoServico { get; set; }
        public Boolean Desativado { get; set; }
    }
}
