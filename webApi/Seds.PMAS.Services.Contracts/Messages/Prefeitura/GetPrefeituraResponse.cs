using Seds.PMAS.Dominio.Entities;
using Seds.PMAS.Services.Contracts.Base;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Seds.PMAS.Services.Contracts.Messages.Prefeitura
{
    [ExcludeFromCodeCoverage]
    public class GetPrefeituraResponse : BaseResponse
    {
        public GetPrefeituraResponse()
        {

        }


        public GetPrefeituraResponse(Guid requestProtocol) : base(requestProtocol)
        {

        }

        public PrefeituraEntity Prefeitura { get; set; }
    }
}
