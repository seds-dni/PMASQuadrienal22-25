using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class FluxoPMAS
    {

        #region propriedades
        private static List<int> FluxoPMASExercicios = new List<int>{ 2022, 2023, 2024, 2025 };
        #endregion

        public void EnviarParaDrads(String comentario, Int32 idPrefeitura, Int32 idUsuario, Boolean commit)
        {
            var pre = new Prefeitura().GetById(idPrefeitura);
            var historico = new PlanoMunicipalHistoricoInfo();
            historico.IdPrefeitura = pre.Id;
            historico.IdSituacao = Convert.ToInt32(ESituacao.EmAnaliseDrads);
            historico.Data = DateTime.Now;
            historico.Descricao = String.IsNullOrEmpty(comentario) ? "Plano enviado para análise da DRADS" : comentario;
            historico.Revisao = pre.Revisao;
            historico.IdUsuario = idUsuario;
            new PlanoMunicipalHistorico().Add(historico, false);

            //Atualiza status da prefeitura
            pre.IdSituacao = Convert.ToInt32(ESituacao.EmAnaliseDrads);
            new Prefeitura().Update(pre, false, false);

            if (commit)
                ContextManager.Commit();
        }

        public void Aprovar(String comentario, Int32 idPrefeitura, Int32 idUsuario, Boolean commit)
        {
            var pre = new Prefeitura().GetById(idPrefeitura);
            var historico = new PlanoMunicipalHistoricoInfo();
            historico.IdPrefeitura = pre.Id;
            historico.IdSituacao = Convert.ToInt32(ESituacao.Aprovado);
            historico.Data = DateTime.Now;
            historico.Descricao = String.IsNullOrEmpty(comentario) ? "Plano Aprovado pelo CMAS" : comentario;
            historico.Revisao = pre.Revisao;
            historico.IdUsuario = idUsuario;
            new PlanoMunicipalHistorico().Add(historico, false);

            //Atualiza status da prefeitura
            pre.IdSituacao = Convert.ToInt32(ESituacao.Aprovado);
            pre.ValoresReprogramadosDrads = null;

            new Prefeitura().Update(pre, false, false);

            if (commit)
                ContextManager.Commit();
        }

        public void Rejeitar(String comentario, Int32 idPrefeitura, Int32 idUsuario, Boolean commit)
        {
            var pre = new Prefeitura().GetById(idPrefeitura);
            var historico = new PlanoMunicipalHistoricoInfo();
            historico.IdPrefeitura = pre.Id;
            historico.IdSituacao = Convert.ToInt32(ESituacao.Rejeitado);
            historico.Data = DateTime.Now;
            historico.Descricao = String.IsNullOrEmpty(comentario) ? "Plano Rejeitado pelo CMAS" : comentario;
            historico.Revisao = pre.Revisao;
            historico.IdUsuario = idUsuario;
            new PlanoMunicipalHistorico().Add(historico, false);

            //Atualiza status da prefeitura
            pre.IdSituacao = Convert.ToInt32(ESituacao.Rejeitado);
            new Prefeitura().Update(pre, false, false);

            if (commit)
                ContextManager.Commit();
        }

        public void Finalizar(String comentario, Int32 idPrefeitura, Int32 idUsuario, Boolean commit)
        {
            var pre = new Prefeitura().GetById(idPrefeitura);
            var historico = new PlanoMunicipalHistoricoInfo();
            historico.IdPrefeitura = pre.Id;
            historico.IdSituacao = Convert.ToInt32(ESituacao.EmAnalisedoCMAS);
            historico.Data = DateTime.Now;
            historico.Descricao = String.IsNullOrEmpty(comentario) ? "Plano finalizado e enviado para análise do CMAS" : comentario;
            historico.Revisao = pre.Revisao;
            historico.IdUsuario = idUsuario;

            new PlanoMunicipalHistorico().Add(historico, false);

            pre.IdSituacao = Convert.ToInt32(ESituacao.EmAnalisedoCMAS);//Atualiza status da prefeitura

            new Prefeitura().Update(pre, false, false);

            if (commit)
            {
                ContextManager.Commit();
            }
        }

        public void EnviarParaFinalizacao(String comentario, Int32 idPrefeitura, Int32 idUsuario, Boolean commit,
                                          Decimal ValorProtecaoSocialBasica = 0M, Decimal ValorProtecaoSocialMedia = 0M,
                                          Decimal ValorProtecaoSocialAlta = 0M, Decimal ValorBeneficioEventuais = 0M, Decimal ValorSPSolidario = 0M)
        {
            
            

            var pre = new Prefeitura().GetById(idPrefeitura);
            var historico = new PlanoMunicipalHistoricoInfo();
            int exercicio1 = FluxoPMAS.FluxoPMASExercicios[0];
            int exercicio2 = FluxoPMAS.FluxoPMASExercicios[1];
            int exercicio3 = FluxoPMAS.FluxoPMASExercicios[2];
            int exercicio4 = FluxoPMAS.FluxoPMASExercicios[3];

            historico.IdPrefeitura = pre.Id;
            historico.IdSituacao = Convert.ToInt32(ESituacao.Parafinalizacao);
            historico.Data = DateTime.Now;
            historico.Descricao = String.IsNullOrEmpty(comentario) ? "Plano enviado para ser finalizado pelo Órgão Gestor" : comentario;
            historico.Revisao = pre.Revisao;
            historico.IdUsuario = idUsuario;

            historico.PlanosMunicipaisHistoricoConsolidados = new List<PlanoMunicipalHistoricoConsolidadoInfo>();
            #region exercicio 1
            var historicoValoresExercicio1 = new PlanoMunicipalHistoricoConsolidadoInfo();
            historicoValoresExercicio1.Exercicio = exercicio1;

            historicoValoresExercicio1.ValorProtecaoSocialBasica = ValorProtecaoSocialBasica;
            historicoValoresExercicio1.ValorProtecaoSocialMediaComplexidade = ValorProtecaoSocialMedia;
            historicoValoresExercicio1.ValorProtecaoSocialAltaComplexidade = ValorProtecaoSocialAlta;
            historicoValoresExercicio1.ValorBeneficiosEventuais = ValorBeneficioEventuais;
            historicoValoresExercicio1.ValorProgramaProjetoSolidario = ValorSPSolidario;
            historico.PlanosMunicipaisHistoricoConsolidados.Add(historicoValoresExercicio1);

            #endregion
            #region exercicio 2
            var historicoValoresExercicio2 = new PlanoMunicipalHistoricoConsolidadoInfo();
            historicoValoresExercicio2.Exercicio = exercicio2;

            historicoValoresExercicio2.ValorProtecaoSocialBasica = ValorProtecaoSocialBasica;
            historicoValoresExercicio2.ValorProtecaoSocialMediaComplexidade = ValorProtecaoSocialMedia;
            historicoValoresExercicio2.ValorProtecaoSocialAltaComplexidade = ValorProtecaoSocialAlta;
            historicoValoresExercicio2.ValorBeneficiosEventuais = ValorBeneficioEventuais;
            historicoValoresExercicio2.ValorProgramaProjetoSolidario = ValorSPSolidario;
            historico.PlanosMunicipaisHistoricoConsolidados.Add(historicoValoresExercicio2);

            #endregion
            #region exercicio 3
            var historicoValoresExercicio3 = new PlanoMunicipalHistoricoConsolidadoInfo();
            historicoValoresExercicio3.Exercicio = exercicio3;

            historicoValoresExercicio3.ValorProtecaoSocialBasica = ValorProtecaoSocialBasica;
            historicoValoresExercicio3.ValorProtecaoSocialMediaComplexidade = ValorProtecaoSocialMedia;
            historicoValoresExercicio3.ValorProtecaoSocialAltaComplexidade = ValorProtecaoSocialAlta;
            historicoValoresExercicio3.ValorBeneficiosEventuais = ValorBeneficioEventuais;
            historicoValoresExercicio3.ValorProgramaProjetoSolidario = ValorSPSolidario;
            historico.PlanosMunicipaisHistoricoConsolidados.Add(historicoValoresExercicio3);

            #endregion
            #region exercicio 4
            var historicoValoresExercicio4 = new PlanoMunicipalHistoricoConsolidadoInfo();
            historicoValoresExercicio4.Exercicio = exercicio4;

            historicoValoresExercicio4.ValorProtecaoSocialBasica = ValorProtecaoSocialBasica;
            historicoValoresExercicio4.ValorProtecaoSocialMediaComplexidade = ValorProtecaoSocialMedia;
            historicoValoresExercicio4.ValorProtecaoSocialAltaComplexidade = ValorProtecaoSocialAlta;
            historicoValoresExercicio4.ValorBeneficiosEventuais = ValorBeneficioEventuais;
            historicoValoresExercicio4.ValorProgramaProjetoSolidario = ValorSPSolidario;
            historico.PlanosMunicipaisHistoricoConsolidados.Add(historicoValoresExercicio4);
            #endregion


            pre.IdSituacao = Convert.ToInt32(ESituacao.Parafinalizacao);//Atualiza status da prefeitura

            new PlanoMunicipalHistorico().Add(historico, false);

            new Prefeitura().Update(pre, false, false);

            if (commit)
            {
                ContextManager.Commit();
            }
        }

        public void EnviarParaFinalizacao(String comentario, Int32 idPrefeitura, Int32 idUsuario, Boolean commit, List<PlanoMunicipalHistoricoConsolidadoInfo> planos)
        {
           
            var pre = new Prefeitura().GetById(idPrefeitura);
            var historico = new PlanoMunicipalHistoricoInfo();
            int exercicio1 = FluxoPMAS.FluxoPMASExercicios[0];
            int exercicio2 = FluxoPMAS.FluxoPMASExercicios[1];
            int exercicio3 = FluxoPMAS.FluxoPMASExercicios[2];
            int exercicio4 = FluxoPMAS.FluxoPMASExercicios[3];

            historico.IdPrefeitura = pre.Id;
            historico.IdSituacao = Convert.ToInt32(ESituacao.Parafinalizacao);
            historico.Data = DateTime.Now;
            historico.Descricao = String.IsNullOrEmpty(comentario) ? "Plano enviado para ser finalizado pelo Órgão Gestor" : comentario;
            historico.Revisao = pre.Revisao;
            historico.IdUsuario = idUsuario;

            historico.PlanosMunicipaisHistoricoConsolidados = new List<PlanoMunicipalHistoricoConsolidadoInfo>();
            #region exercicio 1
            var historicoValoresExercicio1 = new PlanoMunicipalHistoricoConsolidadoInfo();
            var plano = planos.Where(x => x.Exercicio == exercicio1).FirstOrDefault();
            historicoValoresExercicio1.Exercicio = plano.Exercicio;
            historicoValoresExercicio1.IdPrefeitura = plano.IdPrefeitura;
            historicoValoresExercicio1.ValorProtecaoSocialBasica = plano.ValorProtecaoSocialBasica;
            historicoValoresExercicio1.ValorProtecaoSocialMediaComplexidade = plano.ValorProtecaoSocialMediaComplexidade;
            historicoValoresExercicio1.ValorProtecaoSocialAltaComplexidade = plano.ValorProtecaoSocialAltaComplexidade;
            historicoValoresExercicio1.ValorBeneficiosEventuais = plano.ValorBeneficiosEventuais;
            historicoValoresExercicio1.ValorProgramaProjetoSolidario = plano.ValorProgramaProjetoSolidario;
            historicoValoresExercicio1.ValorProtecaoSocialBasicaReprogramado = plano.ValorProtecaoSocialBasicaReprogramado;
            historicoValoresExercicio1.ValorProtecaoSocialMediaReprogramado = plano.ValorProtecaoSocialMediaReprogramado;
            historicoValoresExercicio1.ValorProtecaoSocialAltaReprogramado = plano.ValorProtecaoSocialAltaReprogramado;
            historicoValoresExercicio1.ValorProgramaProjetoReprogramado = plano.ValorProgramaProjetoReprogramado;
            historicoValoresExercicio1.ValorBeneficioEventuaisReprogramado = plano.ValorBeneficioEventuaisReprogramado;
            historicoValoresExercicio1.ValorProtecaoSocialBasicaDemandas = plano.ValorProtecaoSocialBasicaDemandas;
            historicoValoresExercicio1.ValorProtecaoSocialMediaDemandas = plano.ValorProtecaoSocialMediaDemandas;
            historicoValoresExercicio1.ValorProtecaoSocialAltaDemandas = plano.ValorProtecaoSocialAltaDemandas;
            historicoValoresExercicio1.ValorBeneficioEventuaisDemandas = plano.ValorBeneficioEventuaisDemandas;
            historicoValoresExercicio1.ValorProgramaProjetoDemandas = plano.ValorProgramaProjetoDemandas;
            historicoValoresExercicio1.ValorBeneficioEventuaisReprogramadoDemandas = plano.ValorBeneficioEventuaisReprogramadoDemandas;
            historicoValoresExercicio1.ValorProtecaoSocialBasicaReprogramadoDemandas = plano.ValorProtecaoSocialBasicaReprogramadoDemandas;
            historicoValoresExercicio1.ValorProtecaoSocialMediaReprogramadoDemandas = plano.ValorProtecaoSocialMediaReprogramadoDemandas;
            historicoValoresExercicio1.ValorProtecaoSocialAltaReprogramadoDemandas = plano.ValorProtecaoSocialAltaReprogramadoDemandas;

            historico.PlanosMunicipaisHistoricoConsolidados.Add(historicoValoresExercicio1); 
            #endregion
            #region exercicio 2
            var historicoValoresExercicio2 = new PlanoMunicipalHistoricoConsolidadoInfo();
            var plano2 = planos.Where(x => x.Exercicio == exercicio2).FirstOrDefault();
            historicoValoresExercicio2.Exercicio = plano2.Exercicio;
            historicoValoresExercicio2.IdPrefeitura = plano.IdPrefeitura;
            historicoValoresExercicio2.ValorProtecaoSocialBasica = plano2.ValorProtecaoSocialBasica;
            historicoValoresExercicio2.ValorProtecaoSocialMediaComplexidade = plano2.ValorProtecaoSocialMediaComplexidade;
            historicoValoresExercicio2.ValorProtecaoSocialAltaComplexidade = plano2.ValorProtecaoSocialAltaComplexidade;
            historicoValoresExercicio2.ValorBeneficiosEventuais = plano2.ValorBeneficiosEventuais;
            historicoValoresExercicio2.ValorProgramaProjetoSolidario = plano2.ValorProgramaProjetoSolidario;
            historicoValoresExercicio2.ValorProtecaoSocialBasicaReprogramado = plano2.ValorProtecaoSocialBasicaReprogramado;
            historicoValoresExercicio2.ValorProtecaoSocialMediaReprogramado = plano2.ValorProtecaoSocialMediaReprogramado;
            historicoValoresExercicio2.ValorProtecaoSocialAltaReprogramado = plano2.ValorProtecaoSocialAltaReprogramado;
            historicoValoresExercicio2.ValorProgramaProjetoReprogramado = plano2.ValorProgramaProjetoReprogramado;
            historicoValoresExercicio2.ValorBeneficioEventuaisReprogramado = plano2.ValorBeneficioEventuaisReprogramado;
            historicoValoresExercicio2.ValorProtecaoSocialBasicaDemandas = plano2.ValorProtecaoSocialBasicaDemandas;
            historicoValoresExercicio2.ValorProtecaoSocialMediaDemandas = plano2.ValorProtecaoSocialMediaDemandas;
            historicoValoresExercicio2.ValorProtecaoSocialAltaDemandas = plano2.ValorProtecaoSocialAltaDemandas;
            historicoValoresExercicio2.ValorBeneficioEventuaisDemandas = plano2.ValorBeneficioEventuaisDemandas;
            historicoValoresExercicio2.ValorBeneficioEventuaisReprogramadoDemandas = plano2.ValorBeneficioEventuaisReprogramadoDemandas;
            historicoValoresExercicio2.ValorProgramaProjetoDemandas = plano2.ValorProgramaProjetoDemandas;
            historicoValoresExercicio2.ValorProtecaoSocialBasicaReprogramadoDemandas = plano2.ValorProtecaoSocialBasicaReprogramadoDemandas;
            historicoValoresExercicio2.ValorProtecaoSocialMediaReprogramadoDemandas = plano2.ValorProtecaoSocialMediaReprogramadoDemandas;
            historicoValoresExercicio2.ValorProtecaoSocialAltaReprogramadoDemandas = plano2.ValorProtecaoSocialAltaReprogramadoDemandas;

            historico.PlanosMunicipaisHistoricoConsolidados.Add(historicoValoresExercicio2);
            #endregion
            #region exercicio 3

            var historicoValoresExercicio3 = new PlanoMunicipalHistoricoConsolidadoInfo();
            var plano3 = planos.Where(x => x.Exercicio == exercicio3).FirstOrDefault();
            historicoValoresExercicio3.Exercicio = plano3.Exercicio;
            historicoValoresExercicio3.IdPrefeitura = plano.IdPrefeitura;
            historicoValoresExercicio3.ValorProtecaoSocialBasica = plano3.ValorProtecaoSocialBasica;
            historicoValoresExercicio3.ValorProtecaoSocialMediaComplexidade = plano3.ValorProtecaoSocialMediaComplexidade;
            historicoValoresExercicio3.ValorProtecaoSocialAltaComplexidade = plano3.ValorProtecaoSocialAltaComplexidade;
            historicoValoresExercicio3.ValorBeneficiosEventuais = plano3.ValorBeneficiosEventuais;
            historicoValoresExercicio3.ValorProgramaProjetoSolidario = plano3.ValorProgramaProjetoSolidario;
            historicoValoresExercicio3.ValorProtecaoSocialBasicaReprogramado = plano3.ValorProtecaoSocialBasicaReprogramado;
            historicoValoresExercicio3.ValorProtecaoSocialMediaReprogramado = plano3.ValorProtecaoSocialMediaReprogramado;
            historicoValoresExercicio3.ValorProtecaoSocialAltaReprogramado = plano3.ValorProtecaoSocialAltaReprogramado;
            historicoValoresExercicio3.ValorProgramaProjetoReprogramado = plano3.ValorProgramaProjetoReprogramado;
            historicoValoresExercicio3.ValorBeneficioEventuaisReprogramado = plano3.ValorBeneficioEventuaisReprogramado;
            historicoValoresExercicio3.ValorProtecaoSocialBasicaDemandas = plano3.ValorProtecaoSocialBasicaDemandas;
            historicoValoresExercicio3.ValorProtecaoSocialMediaDemandas = plano3.ValorProtecaoSocialMediaDemandas;
            historicoValoresExercicio3.ValorProtecaoSocialAltaDemandas = plano3.ValorProtecaoSocialAltaDemandas;
            historicoValoresExercicio3.ValorBeneficioEventuaisDemandas = plano3.ValorBeneficioEventuaisDemandas;
            historicoValoresExercicio2.ValorBeneficioEventuaisReprogramadoDemandas = plano2.ValorBeneficioEventuaisReprogramadoDemandas;
            historicoValoresExercicio3.ValorProgramaProjetoDemandas = plano3.ValorProgramaProjetoDemandas;
            historicoValoresExercicio3.ValorProtecaoSocialBasicaReprogramadoDemandas = plano3.ValorProtecaoSocialBasicaReprogramadoDemandas;
            historicoValoresExercicio3.ValorProtecaoSocialMediaReprogramadoDemandas = plano3.ValorProtecaoSocialMediaReprogramadoDemandas;
            historicoValoresExercicio3.ValorProtecaoSocialAltaReprogramadoDemandas = plano3.ValorProtecaoSocialAltaReprogramadoDemandas;
            historico.PlanosMunicipaisHistoricoConsolidados.Add(historicoValoresExercicio3);
            #endregion
            #region exercicio 4

            var historicoValoresExercicio4 = new PlanoMunicipalHistoricoConsolidadoInfo();
            var plano4 = planos.Where(x => x.Exercicio == exercicio4).FirstOrDefault();
            historicoValoresExercicio4.Exercicio = plano4.Exercicio;
            historicoValoresExercicio4.IdPrefeitura = plano.IdPrefeitura;
            historicoValoresExercicio4.ValorProtecaoSocialBasica = plano4.ValorProtecaoSocialBasica;
            historicoValoresExercicio4.ValorProtecaoSocialMediaComplexidade = plano4.ValorProtecaoSocialMediaComplexidade;
            historicoValoresExercicio4.ValorProtecaoSocialAltaComplexidade = plano4.ValorProtecaoSocialAltaComplexidade;
            historicoValoresExercicio4.ValorBeneficiosEventuais = plano4.ValorBeneficiosEventuais;
            historicoValoresExercicio4.ValorProgramaProjetoSolidario = plano4.ValorProgramaProjetoSolidario;
            historicoValoresExercicio4.ValorProtecaoSocialBasicaReprogramado = plano4.ValorProtecaoSocialBasicaReprogramado;
            historicoValoresExercicio4.ValorProtecaoSocialMediaReprogramado = plano4.ValorProtecaoSocialMediaReprogramado;
            historicoValoresExercicio4.ValorProtecaoSocialAltaReprogramado = plano4.ValorProtecaoSocialAltaReprogramado;
            historicoValoresExercicio4.ValorProgramaProjetoReprogramado = plano4.ValorProgramaProjetoReprogramado;
            historicoValoresExercicio4.ValorBeneficioEventuaisReprogramado = plano4.ValorBeneficioEventuaisReprogramado;
            historicoValoresExercicio4.ValorProtecaoSocialBasicaDemandas = plano4.ValorProtecaoSocialBasicaDemandas;
            historicoValoresExercicio4.ValorProtecaoSocialMediaDemandas = plano4.ValorProtecaoSocialMediaDemandas;
            historicoValoresExercicio4.ValorProtecaoSocialAltaDemandas = plano4.ValorProtecaoSocialAltaDemandas;
            historicoValoresExercicio4.ValorBeneficioEventuaisDemandas = plano4.ValorBeneficioEventuaisDemandas;
            historicoValoresExercicio4.ValorBeneficioEventuaisReprogramadoDemandas = plano4.ValorBeneficioEventuaisReprogramadoDemandas;
            historicoValoresExercicio4.ValorProgramaProjetoDemandas = plano4.ValorProgramaProjetoDemandas;
            historicoValoresExercicio4.ValorProtecaoSocialBasicaReprogramadoDemandas = plano4.ValorProtecaoSocialBasicaReprogramadoDemandas;
            historicoValoresExercicio4.ValorProtecaoSocialMediaReprogramadoDemandas = plano4.ValorProtecaoSocialMediaReprogramadoDemandas;
            historicoValoresExercicio4.ValorProtecaoSocialAltaReprogramadoDemandas = plano4.ValorProtecaoSocialAltaReprogramadoDemandas;

            historico.PlanosMunicipaisHistoricoConsolidados.Add(historicoValoresExercicio4);
            #endregion

            pre.IdSituacao = Convert.ToInt32(ESituacao.Parafinalizacao);//Atualiza status da prefeitura

            new PlanoMunicipalHistorico().Add(historico, false);

            new Prefeitura().Update(pre, false, false);

            if (commit)
            {
                ContextManager.Commit();
            }
        }


        public void Devolver(ESituacao situacao, String motivo, Int32 idPrefeitura, Int32 idUsuario, Boolean commit)
        {
            var pre = new Prefeitura().GetById(idPrefeitura);
            var historico = new PlanoMunicipalHistoricoInfo();
            historico.IdPrefeitura = pre.Id;
            historico.IdSituacao = Convert.ToInt32(situacao);
            historico.Data = DateTime.Now;
            historico.Descricao = motivo;
            historico.Revisao = pre.Revisao;
            historico.IdUsuario = idUsuario;
            new PlanoMunicipalHistorico().Add(historico, false);

            //Atualiza status da prefeitura
            pre.IdSituacao = Convert.ToInt32(situacao);
            new Prefeitura().Update(pre, false, false);

            if (commit)
                ContextManager.Commit();
        }

        public void DevolverCAS(Int32 idPrefeitura,String motivo,Int32 idUsuario, Boolean commit)
        {
            var pre = new Prefeitura().GetById(idPrefeitura);
            var historico = new PlanoMunicipalHistoricoInfo();
            historico.IdPrefeitura = pre.Id;
            var aprovado = Convert.ToInt32(ESituacao.Aprovado);
            var rejeitado = Convert.ToInt32(ESituacao.Rejeitado);
            var h = new PlanoMunicipalHistorico().GetByPrefeitura(idPrefeitura).Where(t => t.Revisao == (pre.Revisao - 1) && (t.IdSituacao == aprovado || t.IdSituacao == rejeitado)).FirstOrDefault();
            if (h == null)
                return;
            historico.IdSituacao = Convert.ToInt32(ESituacao.Aprovado);
            historico.Data = DateTime.Now;
            historico.Descricao = motivo;
            historico.Revisao = pre.Revisao;
            historico.IdUsuario = idUsuario;
            new PlanoMunicipalHistorico().Add(historico, false);

            //Atualiza status da prefeitura
            pre.IdSituacao = h.IdSituacao;
            new Prefeitura().Update(pre, false, false);

            if (commit)
                ContextManager.Commit();
        }

        public void AutorizarDesbloqueio(ESituacao situacao, String motivo, Int32 idPrefeitura, Int32 idUsuario, Boolean commit, Boolean? valorReprogramado,Boolean? valorDemandas)
        {
            var pre = new Prefeitura().GetById(idPrefeitura);
            var historico = new PlanoMunicipalHistoricoInfo();
            historico.IdPrefeitura = pre.Id;
            historico.IdSituacao = Convert.ToInt32(situacao);
            historico.Data = DateTime.Now;
            historico.Descricao = motivo;
            historico.Revisao = pre.Revisao + 1;
            historico.IdUsuario = idUsuario;
            new PlanoMunicipalHistorico().Add(historico, false);

            //Atualiza status da prefeitura
            pre.IdSituacao = Convert.ToInt32(situacao);
            pre.Revisao = pre.Revisao + 1;
            pre.ValoresReprogramadosDrads = valorReprogramado.HasValue ? valorReprogramado : null;
            pre.ValoresDemandasDrads = valorDemandas.HasValue ? valorDemandas : null;
            new Prefeitura().Update(pre, false, false);

            if (commit)
                ContextManager.Commit();
        }
    }
}
