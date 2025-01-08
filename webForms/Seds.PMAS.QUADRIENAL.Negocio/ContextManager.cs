using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Persistencia;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using System.Data.Objects;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class ContextManager
    {
        public static void Initialize()
        {
            // Hook up the interception
            ObjectFactory.Initialize(
                x =>
                {
                    x.ForRequestedType<IUnitOfWorkFactory>().TheDefaultIsConcreteType<EFUnitOfWorkFactory>();
                    x.ForRequestedType(typeof(IRepository<>)).TheDefaultIsConcreteType(typeof(EFRepository<>));                    
                }
            );

            // We tell the concrete factory what EF model we want to use
            EFUnitOfWorkFactory.SetObjectContext(() => new PMASContext());
        }

        public static ObjectContext GetContext()
        {
            return UnitOfWork.Current.Context;
        }

        public static void OpenConnection()
        {
            if (GetContext().Connection.State == System.Data.ConnectionState.Closed)
            {
                GetContext().Connection.Open();
            }
        }

        public static void CloseConnection()
        {
            if (GetContext().Connection != null && GetContext().Connection.State == System.Data.ConnectionState.Open)
                GetContext().Connection.Close();
        }

        public static void Commit()
        {
            UnitOfWork.Commit();
        }

       
        public static void Dispose()
        {
            UnitOfWork.Current.Dispose();
        }
    }
}
