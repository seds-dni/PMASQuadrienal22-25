using Seds.PMAS.Services.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seds.PMAS.Services.Contracts.Messages.Prefeitura
{
    public class GetPrefeituraRequest : BaseRequest
    {
        public int Id { get; set; }
    }
}
