using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Seds.PMAS.QUADRIENAL.Negocio.RedeProtecaoSocial;
using System.Data.Objects;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using System.Linq;
using Seds.PMAS.QUADRIENAL.Negocio;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;
using Seds.PMAS.QUADRIENAL.UI.Processos;

namespace Seds.PMAS.QUADRIENAL.Testes
{
    [TestClass]
    public class ExecucaoFinanceiraUnitTest
    {
        [TestMethod]
        public void ExecucaoFinanceira()
        {
            ContextManager.Initialize();
            Prefeituras prefeituras = null;
            using (var proxy = new ProxyPrefeitura())
            {
                prefeituras = new Prefeituras(proxy);
                List<ExecucaoFinanceiraInfo> execucoes = prefeituras.GetExecucaoFinanceira(8297);
            }
            Assert.IsNotNull(prefeituras);
        }

    }
}
