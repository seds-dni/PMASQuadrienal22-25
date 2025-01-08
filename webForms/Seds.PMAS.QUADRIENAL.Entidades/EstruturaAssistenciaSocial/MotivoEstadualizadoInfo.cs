using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    /// <summary>
    /// Classe de Dados do Motivo do serviço ser estadualizado
    /// </summary>
    [DataContract]
    public class MotivoEstadualizadoInfo  
    {
        /// <summary>
        /// Id do Motivo de ser estadualizado
        /// </summary>
        [DataMember]
        public Int32 Id { get; set; }
        /// <summary>
        /// Nome do Motivo
        /// </summary>
        [DataMember]
        public String Nome { get; set; }

        public virtual List<ServicoRecursoFinanceiroFundosPrivadoInfo> ServicoRecursoFinanceiroFundosPrivadoInfo { get; set; }
    }
}
