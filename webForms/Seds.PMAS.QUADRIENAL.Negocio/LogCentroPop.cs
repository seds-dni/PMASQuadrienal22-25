using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class LogCentroPop
    {
        private static IRepository<LogCentroPopInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<LogCentroPopInfo>>();
            }
        }

        public static LogCentroPopInfo CreateLog(Int32 IdLog, Int32 IdCentroPop, Int32 IdUnidadePublica, DateTime DataCriacao)
        {
            var logCentroPopInfo = new LogCentroPopInfo();

            logCentroPopInfo.IdLog = IdLog;
            logCentroPopInfo.IdCentroPop = IdCentroPop;
            logCentroPopInfo.IdUnidade = IdUnidadePublica;
            logCentroPopInfo.DataCriacao = DataCriacao;

            return logCentroPopInfo;
        }

        public void Add(LogCentroPopInfo obj, Boolean commit)
        {
            try
            {
                _repository.Add(obj);

                if (commit)
                    ContextManager.Commit();
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }


        }

    }
}
