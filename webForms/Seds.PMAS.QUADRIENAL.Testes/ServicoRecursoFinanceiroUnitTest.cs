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

namespace Seds.PMAS.QUADRIENAL.Testes
{
    [TestClass]
    public class ServicoRecursoFinanceiroUnitTest
    {

        #region Orgão Privado
        [TestMethod]
        public void ListServicoRecursoFinanceiroPrivado()
        {
            ContextManager.Initialize();
            ServicoRecursoFinanceiroPrivado repo = new ServicoRecursoFinanceiroPrivado();
            List<ServicoRecursoFinanceiroPrivadoInfo> entidades = repo.GetAll().ToList();

            Assert.IsNotNull(entidades);
        }

        [TestMethod]
        public void GetServicoRecursoFinanceiroById()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 18884; //TB_SERVICOS_RECURSOS_FINANCEIROS_PRIVADO
            ServicoRecursoFinanceiroPrivado repo = new ServicoRecursoFinanceiroPrivado();
            ServicoRecursoFinanceiroPrivadoInfo entidade = repo.GetById(validoIdNoBanco);

            Assert.IsNotNull(entidade);
        }

        [TestMethod]
        public void UpdateServicoRecursoFinanceiroById()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 18884; //TB_SERVICOS_RECURSOS_FINANCEIROS_PRIVADO
            ServicoRecursoFinanceiroPrivado repo = new ServicoRecursoFinanceiroPrivado();
            ServicoRecursoFinanceiroPrivadoInfo entidade = repo.GetById(validoIdNoBanco);
            entidade.NumeroAtendidosServico = 40;
            repo.Update(entidade, true);
            ServicoRecursoFinanceiroPrivadoInfo entidadeAtualizada = repo.GetById(validoIdNoBanco);
            //select NUMERO_ATENDIDOS_SERVICO from TB_SERVICOS_RECURSOS_FINANCEIROS_PRIVADO where id= 18884 
            Assert.IsTrue(entidadeAtualizada.NumeroAtendidosServico == 40);
        }

        [TestMethod]
        public void UpdateServicoRecursoFinanceiroFundosPrivadoById()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 18884; //TB_SERVICOS_RECURSOS_FINANCEIROS_PRIVADO
            ServicoRecursoFinanceiroPrivado repo = new ServicoRecursoFinanceiroPrivado();
            ServicoRecursoFinanceiroPrivadoInfo entidade = repo.GetById(validoIdNoBanco);
            var fundo = entidade.ServicosRecursosFinanceirosFundosPrivadoInfo.FirstOrDefault();
            fundo.ValorEstadualAssistencia = 18601.00m;
            repo.Update(entidade, true);
            ServicoRecursoFinanceiroPrivadoInfo entidadeAtualizada = repo.GetById(validoIdNoBanco);
            var fundoAtualizado = entidadeAtualizada.ServicosRecursosFinanceirosFundosPrivadoInfo.FirstOrDefault();
            //select * from TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_PRIVADO where ID_SERVICOS_RECURSOS_FINANCEIROS_PRIVADO = 18884 
            Assert.IsTrue(fundoAtualizado.ValorEstadualAssistencia == 18601.00m);
        }


        [TestMethod]
        public void AddServicoRecursoFinanceiroFundosPrivadoById()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 18884; //TB_SERVICOS_RECURSOS_FINANCEIROS_PRIVADO
            ServicoRecursoFinanceiroPrivado repo = new ServicoRecursoFinanceiroPrivado();

            ServicoRecursoFinanceiroPrivadoInfo entidade = repo.GetById(validoIdNoBanco);
            int countAntes = entidade.ServicosRecursosFinanceirosFundosPrivadoInfo.Count();
            ServicoRecursoFinanceiroFundosPrivadoInfo fundo = entidade
                                    .ServicosRecursosFinanceirosFundosPrivadoInfo
                                    .SingleOrDefault(x => x.Exercicio == 2018 && x.ServicoRecursoFinanceiroPrivadoInfoId == validoIdNoBanco);

            if (fundo == null)
            {
                //Novo
                ServicoRecursoFinanceiroFundosPrivadoInfo fundoNovo = new ServicoRecursoFinanceiroFundosPrivadoInfo();
                fundoNovo.ServicoRecursoFinanceiroPrivadoInfoId = entidade.Id;
                fundoNovo.ValorEstadualAssistencia = 7.90m;
                fundoNovo.ValorEstadualFEDCA = 7.80m;
                fundoNovo.ValorEstadualFEI = 7.70m;
                fundoNovo.ValorFederalAssistencia = 7.60m;
                fundoNovo.ValorFederalFNDCA = 7.50m;
                fundoNovo.ValorFederalFNI = 7.40m;
                fundoNovo.ValorMunicipalAssistencia = 7.30m;
                fundoNovo.ValorMunicipalFMDCA = 7.20m;
                fundoNovo.ValorMunicipalFMI = 7.10m;
                fundoNovo.Exercicio = 2018;
                entidade.ServicosRecursosFinanceirosFundosPrivadoInfo.Add(fundoNovo);
                if (fundoNovo.Exercicio == 0)
                {
                    throw new Exception("Exercicio nao pode ser null"); //restricao tirada do banco para migracao
                }

                repo.Update(entidade, true);

                ServicoRecursoFinanceiroPrivadoInfo entidadeAtualizada = repo.GetById(validoIdNoBanco);
                int countDepois = entidadeAtualizada.ServicosRecursosFinanceirosFundosPrivadoInfo.Count();

                //select * from TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_PRIVADO where ID_SERVICOS_RECURSOS_FINANCEIROS_PRIVADO = 18884 
                Assert.IsTrue((countAntes + 1) == countDepois);
            }
            else
            {
                Assert.IsTrue(fundo.ServicoRecursoFinanceiroPrivadoInfoId == entidade.Id && fundo.Exercicio == 2018);
            }
        }

        #endregion

        #region Orgão Público Geral
        [TestMethod]
        public void ListServicoRecursoFinanceiroPublico()
        {
            ContextManager.Initialize();
            ServicoRecursoFinanceiroPublico repo = new ServicoRecursoFinanceiroPublico();
            List<ServicoRecursoFinanceiroPublicoInfo> entidades = repo.GetAll().ToList();

            Assert.IsNotNull(entidades);
        }

        [TestMethod]
        public void GetServicoRecursoFinanceiroPublicoById()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 11386; //TB_SERVICOS_RECURSOS_FINANCEIROS_PUBLICO
            ServicoRecursoFinanceiroPublico repo = new ServicoRecursoFinanceiroPublico();
            ServicoRecursoFinanceiroPublicoInfo entidade = repo.GetById(validoIdNoBanco);

            Assert.IsNotNull(entidade);
        }

        [TestMethod]
        public void UpdateServicoRecursoFinanceiroPublicoById()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 11386; //TB_SERVICOS_RECURSOS_FINANCEIROS_PUBLICO
            ServicoRecursoFinanceiroPublico repo = new ServicoRecursoFinanceiroPublico();
            ServicoRecursoFinanceiroPublicoInfo entidade = repo.GetById(validoIdNoBanco);
            entidade.NumeroAtendidosServico = 40;
            repo.Update(entidade, true);
            ServicoRecursoFinanceiroPublicoInfo entidadeAtualizada = repo.GetById(validoIdNoBanco);
            //select NUMERO_ATENDIDOS_SERVICO from TB_SERVICOS_RECURSOS_FINANCEIROS_PUBLICO where id= 11386 
            Assert.IsTrue(entidadeAtualizada.NumeroAtendidosServico == 40);
        }

        [TestMethod]
        public void UpdateServicoRecursoFinanceiroFundosPublicoById()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 11386; //TB_SERVICOS_RECURSOS_FINANCEIROS_PUBLICO
            ServicoRecursoFinanceiroPublico repo = new ServicoRecursoFinanceiroPublico();
            ServicoRecursoFinanceiroPublicoInfo entidade = repo.GetById(validoIdNoBanco);
            var fundo = entidade.ServicosRecursosFinanceirosFundosPublicoInfo.FirstOrDefault();
            fundo.ValorEstadualAssistencia = 17601.00m;
            repo.Update(entidade, true);
            ServicoRecursoFinanceiroPublicoInfo entidadeAtualizada = repo.GetById(validoIdNoBanco);
            var fundoAtualizado = entidadeAtualizada.ServicosRecursosFinanceirosFundosPublicoInfo.FirstOrDefault();
            //select * from TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_PUBLICO WHERE ID_SERVICOS_RECURSOS_FINANCEIROS_PUBLICO = 11386
            Assert.IsTrue(fundoAtualizado.ValorEstadualAssistencia == 17601.00m);
        }


        [TestMethod]
        public void AddServicoRecursoFinanceiroFundosPublicoById()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 11386; //TB_SERVICOS_RECURSOS_FINANCEIROS_PUBLICO
            int anoTeste = 2018;
            ServicoRecursoFinanceiroPublico repo = new ServicoRecursoFinanceiroPublico();

            ServicoRecursoFinanceiroPublicoInfo entidade = repo.GetById(validoIdNoBanco);
            int countAntes = entidade.ServicosRecursosFinanceirosFundosPublicoInfo.Count();
            ServicoRecursoFinanceiroFundosPublicoInfo fundo = entidade
                                    .ServicosRecursosFinanceirosFundosPublicoInfo
                                    .SingleOrDefault(x => x.Exercicio == anoTeste && x.ServicoRecursoFinanceiroPublicoInfoId == validoIdNoBanco);

            if (fundo == null)
            {
                //Novo
                ServicoRecursoFinanceiroFundosPublicoInfo fundoNovo = new ServicoRecursoFinanceiroFundosPublicoInfo();
                fundoNovo.ServicoRecursoFinanceiroPublicoInfoId = entidade.Id;
                fundoNovo.ValorEstadualAssistencia = 7.90m;
                fundoNovo.ValorEstadualFEDCA = 7.80m;
                fundoNovo.ValorEstadualFEI = 7.70m;
                fundoNovo.ValorFederalAssistencia = 7.60m;
                fundoNovo.ValorFederalFNDCA = 7.50m;
                fundoNovo.ValorFederalFNI = 7.40m;
                fundoNovo.ValorMunicipalAssistencia = 7.30m;
                fundoNovo.ValorMunicipalFMDCA = 7.20m;
                fundoNovo.ValorMunicipalFMI = 7.10m;
                fundoNovo.Exercicio = anoTeste;
                entidade.ServicosRecursosFinanceirosFundosPublicoInfo.Add(fundoNovo);
                if (fundoNovo.Exercicio == 0)
                {
                    throw new Exception("Exercicio nao pode ser null"); //restricao tirada do banco para migracao
                }

                repo.Update(entidade, true);

                ServicoRecursoFinanceiroPublicoInfo entidadeAtualizada = repo.GetById(validoIdNoBanco);
                int countDepois = entidadeAtualizada.ServicosRecursosFinanceirosFundosPublicoInfo.Count();

                //select * from TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_Publico where ID_SERVICOS_RECURSOS_FINANCEIROS_PUBLICO = 11386 
                Assert.IsTrue((countAntes + 1) == countDepois);
            }
            else
            {
                Assert.IsTrue(fundo.ServicoRecursoFinanceiroPublicoInfoId == entidade.Id && fundo.Exercicio == anoTeste);
            }
        }
        #endregion

        #region Orgão Publico - Centro POP

        [TestMethod]
        public void ListServicoRecursoFinanceiroCentroPOP()
        {
            ContextManager.Initialize();
            ServicoRecursoFinanceiroCentroPOP repo = new ServicoRecursoFinanceiroCentroPOP();
            List<ServicoRecursoFinanceiroCentroPOPInfo> entidades = repo.GetAll().ToList();

            Assert.IsNotNull(entidades);

        }

        [TestMethod]
        public void GetServicoRecursoFinanceiroCentroPOPByID()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 293; //TB_SERVICOS_RECURSOS_FINANCEIROS_CENTRO_POP
            ServicoRecursoFinanceiroCentroPOP repo = new ServicoRecursoFinanceiroCentroPOP();
            ServicoRecursoFinanceiroCentroPOPInfo entidade = repo.GetById(validoIdNoBanco);

            Assert.IsNotNull(entidade);

        }

        [TestMethod]
        public void UpdateServicoRecursoFinanceiroCentroPOPByID()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 293; //TB_SERVICOS_RECURSOS_FINANCEIROS_CENTRO_POP
            ServicoRecursoFinanceiroCentroPOP repo = new ServicoRecursoFinanceiroCentroPOP();
            ServicoRecursoFinanceiroCentroPOPInfo entidade = repo.GetById(validoIdNoBanco);
            entidade.PrevisaoMensalNumeroAtendidos = 290;
            repo.Update(entidade, true);
            ServicoRecursoFinanceiroCentroPOPInfo entidadeAtualizada = repo.GetById(validoIdNoBanco);
            //select NUMERO_ATENDIDOS_SERVICO from TB_SERVICOS_RECURSOS_FINANCEIROS_CENTRO_POP where id= 11386 
            Assert.IsTrue(entidadeAtualizada.PrevisaoMensalNumeroAtendidos == 290);
        }


        [TestMethod]
        public void UpdateServicoRecursoFinanceiroFundosCentroPOPById()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 293; //TB_SERVICOS_RECURSOS_FINANCEIROS_CentroPOP
            ServicoRecursoFinanceiroCentroPOP repo = new ServicoRecursoFinanceiroCentroPOP();
            ServicoRecursoFinanceiroCentroPOPInfo entidade = repo.GetById(validoIdNoBanco);
            var fundo = entidade.ServicosRecursosFinanceirosFundosCentroPOPInfo.FirstOrDefault();
            fundo.ValorEstadualAssistencia = 16621.90m;
            repo.Update(entidade, true);
            ServicoRecursoFinanceiroCentroPOPInfo entidadeAtualizada = repo.GetById(validoIdNoBanco);
            var fundoAtualizado = entidadeAtualizada.ServicosRecursosFinanceirosFundosCentroPOPInfo.FirstOrDefault();
            //select * from TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_CENTRO_POP WHERE ID_SERVICOS_RECURSOS_FINANCEIROS_CENTRO_POP = 293
            Assert.IsTrue(fundoAtualizado.ValorEstadualAssistencia == 16621.90m);
        }


        [TestMethod]
        public void AddServicoRecursoFinanceiroFundosCentroPOPById()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 293; //TB_SERVICOS_RECURSOS_FINANCEIROS_CentroPOP
            int anoTeste = 2019;
            ServicoRecursoFinanceiroCentroPOP repo = new ServicoRecursoFinanceiroCentroPOP();

            ServicoRecursoFinanceiroCentroPOPInfo entidade = repo.GetById(validoIdNoBanco);
            int countAntes = entidade.ServicosRecursosFinanceirosFundosCentroPOPInfo.Count();
            ServicoRecursoFinanceiroFundosCentroPOPInfo fundo = entidade
                                    .ServicosRecursosFinanceirosFundosCentroPOPInfo
                                    .SingleOrDefault(x => x.Exercicio == anoTeste && x.ServicoRecursoFinanceiroCentroPOPInfoId == validoIdNoBanco);

            if (fundo == null)
            {
                //Novo
                ServicoRecursoFinanceiroFundosCentroPOPInfo fundoNovo = new ServicoRecursoFinanceiroFundosCentroPOPInfo();
                fundoNovo.ServicoRecursoFinanceiroCentroPOPInfoId = entidade.Id;
                fundoNovo.ValorEstadualAssistencia = 7.90m;
                fundoNovo.ValorEstadualFEDCA = 7.80m;
                fundoNovo.ValorEstadualFEI = 7.70m;
                fundoNovo.ValorFederalAssistencia = 7.60m;
                fundoNovo.ValorFederalFNDCA = 7.50m;
                fundoNovo.ValorFederalFNI = 7.40m;
                fundoNovo.ValorMunicipalAssistencia = 7.30m;
                fundoNovo.ValorMunicipalFMDCA = 7.20m;
                fundoNovo.ValorMunicipalFMI = 7.10m;
                fundoNovo.Exercicio = anoTeste;
                entidade.ServicosRecursosFinanceirosFundosCentroPOPInfo.Add(fundoNovo);
                if (fundoNovo.Exercicio == 0)
                {
                    throw new Exception("Exercicio nao pode ser null"); //restricao tirada do banco para migracao
                }

                repo.Update(entidade, true);

                ServicoRecursoFinanceiroCentroPOPInfo entidadeAtualizada = repo.GetById(validoIdNoBanco);
                int countDepois = entidadeAtualizada.ServicosRecursosFinanceirosFundosCentroPOPInfo.Count();

                //select * from TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_CENTRO_POP where ID_SERVICOS_RECURSOS_FINANCEIROS_CENTRO_POP= 293 
                Assert.IsTrue((countAntes + 1) == countDepois);
            }
            else
            {
                Assert.IsTrue(fundo.ServicoRecursoFinanceiroCentroPOPInfoId == entidade.Id && fundo.Exercicio == anoTeste);
            }
        }


        #endregion

        #region Orgão Publico - CRAS

        [TestMethod]
        public void ListServicoRecursoFinanceiroCRAS()
        {
            ContextManager.Initialize();
            ServicoRecursoFinanceiroCRAS repo = new ServicoRecursoFinanceiroCRAS();
            List<ServicoRecursoFinanceiroCRASInfo> entidades = repo.GetAll().ToList();

            Assert.IsNotNull(entidades);

        }


        [TestMethod]
        public void GetServicoRecursoFinanceiroCRASByID()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 11608; //TB_SERVICOS_RECURSOS_FINANCEIROS_CRAS
            ServicoRecursoFinanceiroCRAS repo = new ServicoRecursoFinanceiroCRAS();
            ServicoRecursoFinanceiroCRASInfo entidade = repo.GetById(validoIdNoBanco);

            Assert.IsNotNull(entidade);

        }

        [TestMethod]
        public void UpdateServicoRecursoFinanceiroCRASByID()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 11608; //TB_SERVICOS_RECURSOS_FINANCEIROS_CRAS
            ServicoRecursoFinanceiroCRAS repo = new ServicoRecursoFinanceiroCRAS();
            ServicoRecursoFinanceiroCRASInfo entidade = repo.GetById(validoIdNoBanco);
            entidade.PrevisaoMensalNumeroAtendidos = 290;
            repo.Update(entidade, true);
            ServicoRecursoFinanceiroCRASInfo entidadeAtualizada = repo.GetById(validoIdNoBanco);
            //select NUMERO_ATENDIDOS_SERVICO from TB_SERVICOS_RECURSOS_FINANCEIROS_CRAS where id= 11608 
            Assert.IsTrue(entidadeAtualizada.PrevisaoMensalNumeroAtendidos == 290);
        }


        [TestMethod]
        public void UpdateServicoRecursoFinanceiroFundosCRASById()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 11608; //TB_SERVICOS_RECURSOS_FINANCEIROS_CRAS
            ServicoRecursoFinanceiroCRAS repo = new ServicoRecursoFinanceiroCRAS();
            ServicoRecursoFinanceiroCRASInfo entidade = repo.GetById(validoIdNoBanco);
            var fundo = entidade.ServicosRecursosFinanceirosFundosCRASInfo.FirstOrDefault();
            fundo.ValorEstadualAssistencia = 12621.90m;
            repo.Update(entidade, true);
            ServicoRecursoFinanceiroCRASInfo entidadeAtualizada = repo.GetById(validoIdNoBanco);
            var fundoAtualizado = entidadeAtualizada.ServicosRecursosFinanceirosFundosCRASInfo.FirstOrDefault();
            //select * from TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_CRAS WHERE ID_SERVICOS_RECURSOS_FINANCEIROS_CRAS = 11608
            Assert.IsTrue(fundoAtualizado.ValorEstadualAssistencia == 12621.90m);
        }



        [TestMethod]
        public void AddServicoRecursoFinanceiroFundosCRASById()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 11608; //TB_SERVICOS_RECURSOS_FINANCEIROS_CRAS
            int anoTeste = 2019;
            ServicoRecursoFinanceiroCRAS repo = new ServicoRecursoFinanceiroCRAS();

            ServicoRecursoFinanceiroCRASInfo entidade = repo.GetById(validoIdNoBanco);
            int countAntes = entidade.ServicosRecursosFinanceirosFundosCRASInfo.Count();
            ServicoRecursoFinanceiroFundosCRASInfo fundo = entidade
                                    .ServicosRecursosFinanceirosFundosCRASInfo
                                    .SingleOrDefault(x => x.Exercicio == anoTeste && x.ServicoRecursoFinanceiroCRASInfoId == validoIdNoBanco);

            if (fundo == null)
            {
                //Novo
                ServicoRecursoFinanceiroFundosCRASInfo fundoNovo = new ServicoRecursoFinanceiroFundosCRASInfo();
                fundoNovo.ServicoRecursoFinanceiroCRASInfoId = entidade.Id;
                fundoNovo.ValorEstadualAssistencia = 7.90m;
                fundoNovo.ValorEstadualFEDCA = 7.80m;
                fundoNovo.ValorEstadualFEI = 7.70m;
                fundoNovo.ValorFederalAssistencia = 7.60m;
                fundoNovo.ValorFederalFNDCA = 7.50m;
                fundoNovo.ValorFederalFNI = 7.40m;
                fundoNovo.ValorMunicipalAssistencia = 7.30m;
                fundoNovo.ValorMunicipalFMDCA = 7.20m;
                fundoNovo.ValorMunicipalFMI = 7.10m;
                fundoNovo.Exercicio = anoTeste;
                entidade.ServicosRecursosFinanceirosFundosCRASInfo.Add(fundoNovo);
                if (fundoNovo.Exercicio == 0)
                {
                    throw new Exception("Exercicio nao pode ser null"); //restricao tirada do banco para migracao
                }

                repo.Update(entidade, true);

                ServicoRecursoFinanceiroCRASInfo entidadeAtualizada = repo.GetById(validoIdNoBanco);
                int countDepois = entidadeAtualizada.ServicosRecursosFinanceirosFundosCRASInfo.Count();

                //select * from TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_CRAS where ID_SERVICOS_RECURSOS_FINANCEIROS_CRAS= 11608 
                Assert.IsTrue((countAntes + 1) == countDepois);
            }
            else
            {
                Assert.IsTrue(fundo.ServicoRecursoFinanceiroCRASInfoId == entidade.Id && fundo.Exercicio == anoTeste);
            }
        }

        #endregion

        #region Orgão Publico - CREAS

        [TestMethod]
        public void ListServicoRecursoFinanceiroCREAS()
        {
            ContextManager.Initialize();
            ServicoRecursoFinanceiroCREAS repo = new ServicoRecursoFinanceiroCREAS();
            List<ServicoRecursoFinanceiroCREASInfo> entidades = repo.GetAll().ToList();

            Assert.IsNotNull(entidades);

        }


        [TestMethod]
        public void GetServicoRecursoFinanceiroCREASByID()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 3321; //TB_SERVICOS_RECURSOS_FINANCEIROS_CREAS
            ServicoRecursoFinanceiroCREAS repo = new ServicoRecursoFinanceiroCREAS();
            ServicoRecursoFinanceiroCREASInfo entidade = repo.GetById(validoIdNoBanco);

            Assert.IsNotNull(entidade);

        }


        [TestMethod]
        public void UpdateServicoRecursoFinanceiroCREASByID()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 3321; //TB_SERVICOS_RECURSOS_FINANCEIROS_CREAS
            ServicoRecursoFinanceiroCREAS repo = new ServicoRecursoFinanceiroCREAS();
            ServicoRecursoFinanceiroCREASInfo entidade = repo.GetById(validoIdNoBanco);
            entidade.PrevisaoMensalNumeroAtendidos = 380;
            repo.Update(entidade, true);
            ServicoRecursoFinanceiroCREASInfo entidadeAtualizada = repo.GetById(validoIdNoBanco);
            //select NUMERO_ATENDIDOS_SERVICO from TB_SERVICOS_RECURSOS_FINANCEIROS_CREAS where id= 3321 
            Assert.IsTrue(entidadeAtualizada.PrevisaoMensalNumeroAtendidos == 380);
        }

        [TestMethod]
        public void UpdateServicoRecursoFinanceirosFundosCREASById()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 3321; //TB_SERVICOS_RECURSOS_FINANCEIROS_CREAS
            ServicoRecursoFinanceiroCREAS repo = new ServicoRecursoFinanceiroCREAS();
            ServicoRecursoFinanceiroCREASInfo entidade = repo.GetById(validoIdNoBanco);
            var fundo = entidade.ServicosRecursosFinanceirosFundosCREASInfo.FirstOrDefault();
            fundo.ValorEstadualAssistencia = 11121.90m;
            repo.Update(entidade, true);
            ServicoRecursoFinanceiroCREASInfo entidadeAtualizada = repo.GetById(validoIdNoBanco);
            var fundoAtualizado = entidadeAtualizada.ServicosRecursosFinanceirosFundosCREASInfo.FirstOrDefault();
            //select * from TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_CREAS where ID_SERVICOS_RECURSOS_FINANCEIROS_CREAS = 3321 
            Assert.IsTrue(fundoAtualizado.ValorEstadualAssistencia == 11121.90m);
        }



        [TestMethod]
        public void AddServicoRecursoFinanceiroFundosCREASById()
        {
            ContextManager.Initialize();
            int validoIdNoBanco = 3321; //TB_SERVICOS_RECURSOS_FINANCEIROS_CREAS
            int anoTeste = 2019;
            ServicoRecursoFinanceiroCREAS repo = new ServicoRecursoFinanceiroCREAS();

            ServicoRecursoFinanceiroCREASInfo entidade = repo.GetById(validoIdNoBanco);
            int countAntes = entidade.ServicosRecursosFinanceirosFundosCREASInfo.Count();
            ServicoRecursoFinanceiroFundosCREASInfo fundo = entidade
                                    .ServicosRecursosFinanceirosFundosCREASInfo
                                    .SingleOrDefault(x => x.Exercicio == anoTeste && x.ServicoRecursoFinanceiroCREASInfoId == validoIdNoBanco);

            if (fundo == null)
            {
                //Novo
                ServicoRecursoFinanceiroFundosCREASInfo fundoNovo = new ServicoRecursoFinanceiroFundosCREASInfo();
                fundoNovo.ServicoRecursoFinanceiroCREASInfoId = entidade.Id;
                fundoNovo.ValorEstadualAssistencia = 10.90m;
                fundoNovo.ValorEstadualFEDCA = 10.80m;
                fundoNovo.ValorEstadualFEI = 10.70m;
                fundoNovo.ValorFederalAssistencia = 10.60m;
                fundoNovo.ValorFederalFNDCA = 10.50m;
                fundoNovo.ValorFederalFNI = 10.40m;
                fundoNovo.ValorMunicipalAssistencia = 10.30m;
                fundoNovo.ValorMunicipalFMDCA = 10.20m;
                fundoNovo.ValorMunicipalFMI = 10.10m;
                fundoNovo.Exercicio = anoTeste;
                entidade.ServicosRecursosFinanceirosFundosCREASInfo.Add(fundoNovo);
                if (fundoNovo.Exercicio == 0)
                {
                    throw new Exception("Exercicio nao pode ser null"); //restricao tirada do banco para migracao
                }

                repo.Update(entidade, true);

                ServicoRecursoFinanceiroCREASInfo entidadeAtualizada = repo.GetById(validoIdNoBanco);
                int countDepois = entidadeAtualizada.ServicosRecursosFinanceirosFundosCREASInfo.Count();

                //select * from TB_SERVICOS_RECURSOS_FINANCEIROS_FUNDOS_CREAS where ID_SERVICOS_RECURSOS_FINANCEIROS_CREAS= 3321 
                Assert.IsTrue((countAntes + 1) == countDepois);
            }
            else
            {
                Assert.IsTrue(fundo.ServicoRecursoFinanceiroCREASInfoId == entidade.Id && fundo.Exercicio == anoTeste);
            }
        }

        #endregion

    }
}
