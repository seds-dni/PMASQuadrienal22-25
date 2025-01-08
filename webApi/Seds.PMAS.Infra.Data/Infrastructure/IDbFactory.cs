using Seds.PMAS.Infra.Data.Context;
using System;

namespace Seds.PMAS.Infra.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        DBPMASContext Init();
    }
}
