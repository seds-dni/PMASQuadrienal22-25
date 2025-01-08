using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class LogCreas
    {
        private static IRepository<LogCreasInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<LogCreasInfo>>();
            }
        }

        public static LogCreasInfo CreateLog(Int32 IdLog, Int32 IdCreas, Int32 IdUnidadePublica, DateTime DataCriacao)
        {
            var logCreasInfo = new LogCreasInfo();

            logCreasInfo.IdLog = IdLog;
            logCreasInfo.IdCreas = IdCreas;
            logCreasInfo.IdUnidade = IdUnidadePublica;
            logCreasInfo.DataCriacao = DataCriacao;

            return logCreasInfo;
        }

        public void Add(LogCreasInfo obj, Boolean commit)
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
