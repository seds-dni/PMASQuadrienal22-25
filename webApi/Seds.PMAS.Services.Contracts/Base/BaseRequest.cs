using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seds.PMAS.Services.Contracts.Base
{
    /// <summary>
    /// Classe de requisição dos serviços
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class BaseRequest
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public BaseRequest()
        {
            Protocol = Guid.NewGuid();
        }

        /// <summary>
        /// Cria um novo objeto response com o Guid do request
        /// </summary>
        /// <typeparam name="T">BaseResponse que será adicionado o guid</typeparam>
        /// <returns></returns>
        public T CreateResponse<T>() where T : BaseResponse, new()
        {
            return new T() { Protocol = Protocol };
        }

        /// <summary>
        /// Protocolo da requisicao
        /// </summary>
        public Guid Protocol { get; set; }

        /// <summary>
        /// Validar os filtros das buscas
        /// </summary>
        public bool ValidarFiltro { get; set; }
    }
}
