using Seds.PMAS.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seds.PMAS.Infra.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        DBPMASContext dbContext;

        public DBPMASContext Init()
        {
            return dbContext ?? (dbContext = new DBPMASContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
