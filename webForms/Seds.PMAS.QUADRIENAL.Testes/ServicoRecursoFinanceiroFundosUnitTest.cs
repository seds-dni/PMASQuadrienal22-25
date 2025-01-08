using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Seds.PMAS.QUADRIENAL.Negocio.RedeProtecaoSocial;
using System.Data.Objects;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using System.Linq;
using Seds.PMAS.QUADRIENAL.Negocio;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Collections.Generic;

namespace Seds.PMAS.QUADRIENAL.Testes
{
    [TestClass]
    public class ServicoRecursoFinanceiroFundosUnitTest
    {

        #region Orgão Privado 
        [TestMethod]
        public void ListServicoRecursoFinanceiroDosFundosPrivado()
        {
            ContextManager.Initialize();
            ServicoRecursoFinanceiroFundosPrivado repo = new ServicoRecursoFinanceiroFundosPrivado();
            List<ServicoRecursoFinanceiroFundosPrivadoInfo> entidades = repo.GetAll().ToList();

            Assert.IsNotNull(entidades);
        }

        [TestMethod]
        public void GetServicoRecursoFinanceiroDosFundosPrivadoById()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 1; //TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_PRIVADO
            ServicoRecursoFinanceiroFundosPrivado repo = new ServicoRecursoFinanceiroFundosPrivado();
            ServicoRecursoFinanceiroFundosPrivadoInfo entidade = repo.GetById(validoIdNoBanco);

            Assert.IsNotNull(entidade);
        }

        [TestMethod]
        public void UpdateServicoRecursoFinanceiroDosFundosPrivadoById()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 1; //TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_PRIVADO
            ServicoRecursoFinanceiroFundosPrivado repo = new ServicoRecursoFinanceiroFundosPrivado();
            ServicoRecursoFinanceiroFundosPrivadoInfo entidade = repo.GetById(validoIdNoBanco);
            entidade.ValorEstadualFEDCA = 76;
            repo.Update(entidade);
            ServicoRecursoFinanceiroFundosPrivadoInfo entidadeAtualizada = repo.GetById(validoIdNoBanco);

            Assert.IsTrue(entidadeAtualizada.ValorEstadualFEDCA == 76);
        }

	    #endregion

        #region Orgão Público Geral
        [TestMethod]
        public void ListServicoRecursoFinanceiroDosFundosPublico()
        {
            ContextManager.Initialize();
            ServicoRecursoFinanceiroFundosPublico repo = new ServicoRecursoFinanceiroFundosPublico();
            List<ServicoRecursoFinanceiroFundosPublicoInfo> entidades = repo.GetAll().ToList();

            Assert.IsNotNull(entidades);
        }

        [TestMethod]
        public void GetServicoRecursoFinanceiroDosFundosPublicoById()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 1; //TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_PUBLICO
            ServicoRecursoFinanceiroFundosPublico repo = new ServicoRecursoFinanceiroFundosPublico();
            ServicoRecursoFinanceiroFundosPublicoInfo entidade = repo.GetById(validoIdNoBanco);

            Assert.IsNotNull(entidade);
        }


        [TestMethod]
        public void UpdateServicoRecursoFinanceiroDosFundosPublicoById()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 1; //TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_PUBLICO
            ServicoRecursoFinanceiroFundosPublico repo = new ServicoRecursoFinanceiroFundosPublico();
            ServicoRecursoFinanceiroFundosPublicoInfo entidade = repo.GetById(validoIdNoBanco);
            entidade.ValorEstadualFEDCA = 106;
            repo.Update(entidade);
            ServicoRecursoFinanceiroFundosPublicoInfo entidadeAtualizada = repo.GetById(validoIdNoBanco);

            Assert.IsTrue(entidadeAtualizada.ValorEstadualFEDCA == 106);
        }

        #endregion

        #region Orgão Publico - Centro POP

        [TestMethod]
        public void ListServicoRecursoFinanceiroDosFundosCentroPOP()
        {
            ContextManager.Initialize();
            ServicoRecursoFinanceiroFundosCentroPOP repo = new ServicoRecursoFinanceiroFundosCentroPOP();
            List<ServicoRecursoFinanceiroFundosCentroPOPInfo> entidades = repo.GetAll().ToList();

            Assert.IsNotNull(entidades);

        }


        [TestMethod]
        public void GetServicoRecursoFinanceiroDosFundosCentroPOPByID()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 4; // TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_CENTRO_POP
            ServicoRecursoFinanceiroFundosCentroPOP repo = new ServicoRecursoFinanceiroFundosCentroPOP();
            ServicoRecursoFinanceiroFundosCentroPOPInfo entidade = repo.GetById(validoIdNoBanco);

            Assert.IsNotNull(entidade);

        }

        [TestMethod]
        public void UpdateServicoRecursoFinanceiroDosFundosCentroPOPById()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 1; //TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_CENTRO_POP
            ServicoRecursoFinanceiroFundosCentroPOP repo = new ServicoRecursoFinanceiroFundosCentroPOP();
            ServicoRecursoFinanceiroFundosCentroPOPInfo entidade = repo.GetById(validoIdNoBanco);
            entidade.ValorEstadualFEDCA = 106;
            repo.Update(entidade);
            ServicoRecursoFinanceiroFundosCentroPOPInfo entidadeAtualizada = repo.GetById(validoIdNoBanco);

            Assert.IsTrue(entidadeAtualizada.ValorEstadualFEDCA == 106);
        }
        #endregion

        #region Orgão Publico - CRAS

        [TestMethod]
        public void ListServicoRecursoFinanceiroDosFundosCRAS()
        {
            ContextManager.Initialize();
            ServicoRecursoFinanceiroFundosCRAS repo = new ServicoRecursoFinanceiroFundosCRAS();
            List<ServicoRecursoFinanceiroFundosCRASInfo> entidades = repo.GetAll().ToList();

            Assert.IsNotNull(entidades);

        }


        [TestMethod]
        public void GetServicoRecursoFinanceiroDosFundosCRASByID()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 4; // TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_CRAS

            ServicoRecursoFinanceiroFundosCRAS repo = new ServicoRecursoFinanceiroFundosCRAS();
            ServicoRecursoFinanceiroFundosCRASInfo entidade = repo.GetById(validoIdNoBanco);

            Assert.IsNotNull(entidade);

        }


        [TestMethod]
        public void UpdateServicoRecursoFinanceiroDosFundosCRASById()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 1; //TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_CRAS
            ServicoRecursoFinanceiroFundosCRAS repo = new ServicoRecursoFinanceiroFundosCRAS();
            ServicoRecursoFinanceiroFundosCRASInfo entidade = repo.GetById(validoIdNoBanco);
            entidade.ValorEstadualFEDCA = 106;
            repo.Update(entidade);
            ServicoRecursoFinanceiroFundosCRASInfo entidadeAtualizada = repo.GetById(validoIdNoBanco);

            Assert.IsTrue(entidadeAtualizada.ValorEstadualFEDCA == 106);
        }
        #endregion

        #region Orgão Publico - CREAS

        [TestMethod]
        public void ListServicoRecursoFinanceiroDosFundosCREAS()
        {
            ContextManager.Initialize();
            ServicoRecursoFinanceiroFundosCREAS repo = new ServicoRecursoFinanceiroFundosCREAS();
            List<ServicoRecursoFinanceiroFundosCREASInfo> entidades = repo.GetAll().ToList();

            Assert.IsNotNull(entidades);

        }


        [TestMethod]
        public void GetServicoRecursoFinanceiroDosFundosCREASByID()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 5; // TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_CREAS

            ServicoRecursoFinanceiroFundosCREAS repo = new ServicoRecursoFinanceiroFundosCREAS();
            ServicoRecursoFinanceiroFundosCREASInfo entidade = repo.GetById(validoIdNoBanco);

            Assert.IsNotNull(entidade);

        }



        [TestMethod]
        public void UpdateServicoRecursoFinanceiroDosFundosCREASById()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 1; //TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_CREAS
            ServicoRecursoFinanceiroFundosCREAS repo = new ServicoRecursoFinanceiroFundosCREAS();
            ServicoRecursoFinanceiroFundosCREASInfo entidade = repo.GetById(validoIdNoBanco);
            entidade.ValorEstadualFEDCA = 106;
            repo.Update(entidade);
            ServicoRecursoFinanceiroFundosCREASInfo entidadeAtualizada = repo.GetById(validoIdNoBanco);

            Assert.IsTrue(entidadeAtualizada.ValorEstadualFEDCA == 106);
        }
        #endregion

    }
}
