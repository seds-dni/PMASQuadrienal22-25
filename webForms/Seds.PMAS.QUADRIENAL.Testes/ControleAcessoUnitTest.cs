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
    public class ControleAcessoUnitTestUnitTest
    {
        [TestMethod]
        public void ObterPrefeituraExercicioBloqueio()
        {
            ContextManager.Initialize();
            PrefeituraExercicioBloqueio repositorio = new PrefeituraExercicioBloqueio();
            var prefeituraExercicios = repositorio.GetAll().Where(x => x.IdPrefeitura == 8297);
            bool exercicio1 = prefeituraExercicios.Single(x =>x.Exercicio == 2018).Desbloqueado.Value;
            bool exercicio2 = prefeituraExercicios.Single(x =>x.Exercicio == 2019).Desbloqueado.Value;
            Assert.IsTrue(!exercicio1 && exercicio2);
        }

        [TestMethod]
        public void ObterPrefeituraBloqueio()
        {
            ContextManager.Initialize();
            Prefeitura repositorio = new Prefeitura();
            PrefeituraInfo prefeitura = repositorio.GetById(8297);

            bool exercicio1 = prefeitura.PrefeiturasExerciciosBloqueio.Single(x => x.Exercicio == 2018).Desbloqueado.Value;
            bool exercicio2 = prefeitura.PrefeiturasExerciciosBloqueio.Single(x => x.Exercicio == 2019).Desbloqueado.Value;

            Assert.IsTrue(!exercicio1 && exercicio2);
        }
    }
}
