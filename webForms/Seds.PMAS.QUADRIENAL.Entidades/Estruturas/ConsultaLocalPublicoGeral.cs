using System;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas
{
    public class ConsultaLocalPublicoGeral
    {
        public string TipoUnidade { get; set; }

        public Int32 Id { get; set; }

        public Int32 IdUnidade { get; set; }
        
        public String Nome { get; set; }

        public Boolean Desativado { get; set; }

        public Int32? IdMotivoDesativacao { get; set; }

        public String DescricaoDesativacao { get; set; }

        public DateTime? DataEncerramento { get; set; }

        public String DetalhamentoEncerramento { get; set; }

    }
}
