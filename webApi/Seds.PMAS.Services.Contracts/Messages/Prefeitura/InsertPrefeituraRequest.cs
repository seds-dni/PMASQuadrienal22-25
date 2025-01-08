using Seds.PMAS.Dominio.Entities;
using Seds.PMAS.Services.Contracts.Base;
using System.Diagnostics.CodeAnalysis;

namespace Seds.PMAS.Services.Contracts.Messages.Prefeitura
{
    [ExcludeFromCodeCoverage]
    public class InsertPrefeituraRequest : BaseRequest
    {
        public PrefeituraEntity prefeitura { get; set; }
    }
}
