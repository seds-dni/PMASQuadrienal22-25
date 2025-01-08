using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Data.Objects;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using System.Transactions;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.Seguranca.Token;
using System.Threading;
using Microsoft.IdentityModel.Claims;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class LogCras
    {
        private static IRepository<LogCrasInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<LogCrasInfo>>();
            }
        }

        public static LogCrasInfo CreateLog(Int32 IdLog, Int32 IdCras, Int32 IdUnidadePublica, DateTime DataCriacao)
        {
            var logCrasInfo = new LogCrasInfo();


            logCrasInfo.IdLog = IdLog;
            logCrasInfo.IdCras = IdCras;
            logCrasInfo.IdUnidade = IdUnidadePublica;
            logCrasInfo.DataCriacao = DataCriacao;

            return logCrasInfo;
        }

        public void Add(LogCrasInfo obj, Boolean commit)
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
