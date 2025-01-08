using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class InterfacePublicaAlimentacaoRestauranteInfo
    {
        public Int32 Id { get; set; }

        public Int32 IdInterfacePublicaAlimentacao { get; set; }

        public String Nome { get; set; }

        public String CEP { get; set; }
        
        public String Logradouro { get; set; }
        
        public String Numero { get; set; }
        
        public String Complemento { get; set; }
        
        public String Bairro { get; set; }
        
        public String Cidade { get; set; }
        
        public String TelefoneFixo { get; set; }
        
        public String UnidadeAtendimento { get; set; }
        
        public DateTime? DataInicioAtividade { get; set; }
        
        public Boolean ConvenioBomPrato { get; set; }

        public Boolean? PossuiParceria { get; set; }
        
        public InterfacePublicaAlimentacaoInfo InterfacePublicaAlimentacao { get; set; }

    }
}
