using Seds.PMAS.Common.Enums;
using Seds.PMAS.Common.Object;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Seds.PMAS.Services.Contracts.Base
{
    /// <summary>
    /// Classe resposta de uma requisição
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class BaseResponse
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public BaseResponse()
        {
            Mensagem = new List<ClientMessage>();
        }

        /// <summary>
        /// Resposta de uma requisição
        /// </summary>
        /// <param name="requestProtocol">Protocolo do request a que esse response representa</param>
        public BaseResponse(Guid requestProtocol) : this()
        {
            Protocol = requestProtocol;
        }

        /// <summary>
        /// Adiciona nova mensagem
        /// </summary>
        /// <param name="codigo">Código da mensagem</param>
        /// <param name="conteudo">Conteúdo da mensagem</param>
        /// <param name="tipo">Tipo da mensagem</param>
        public void AddMesage(string codigo, string conteudo, EnumMessage tipo)
        {
            Mensagem.Add(new ClientMessage { Codigo = codigo, Conteudo = conteudo, Tipo = tipo });
        }

        private bool? IsValid { get; set; }

        /// <summary>
        /// Indica se a resposta é valida
        /// </summary>
        public virtual bool Valid
        {
            get
            {
                if (!IsValid.HasValue)
                {
                    return Mensagem.Count == 0 || (Mensagem.Any(x => x != null && x.Sucesso));
                }

                return IsValid.Value;
            }

            set
            {
                IsValid = value;
            }
        }

        /// <summary>
        /// Lista de possíveis mensagens de resposta
        /// </summary>
        public virtual List<ClientMensagem> Mensagem { get; set; }

        /// <summary>
        /// Protocolo da resposta
        /// </summary>
        public virtual Guid Protocol { get; internal set; }
    }
}
