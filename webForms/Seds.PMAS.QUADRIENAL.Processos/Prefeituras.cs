using System;
using System.Collections.Generic;
using System.Linq;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.UI.Processos
{
    public class Prefeituras
    {

        #region prefeitura
        private ProxyPrefeitura ProxyPrefeitura { get; set; }

        public Prefeituras(ProxyPrefeitura proxy)
        {
            ProxyPrefeitura = proxy;
        }

        public List<PrefeituraInfo> GetPrefeituras()
        {
            return ProxyPrefeitura.Service.GetPrefeituras().ToList();
        }

        public PrefeituraInfo GetPrefeitura(Int32 id)
        {
            var pre = ProxyPrefeitura.Service.GetPrefeituraById(id);
            if (pre == null)
                return null;

            pre.Municipio = ProxyDivisaoAdministrativa.MunicipiosEstaduais.FirstOrDefault(m => m.Id == pre.IdMunicipio);
            pre.Municipio.Drads = ProxyDivisaoAdministrativa.Drads.FirstOrDefault(d => d.Id == pre.Municipio.IdDrads.Value);
            return pre;
        }

        public void UpdatePrefeitura(PrefeituraInfo pre, bool validar = true)
        {
            ProxyPrefeitura.Service.UpdatePrefeitura(pre, validar);
        }

        #endregion

        #region prefeito
        public PrefeitoInfo GetPrefeitoAtual(Int32 idPrefeitura)
        {
            return ProxyPrefeitura.Service.GetAtualPrefeitoByPrefeitura(idPrefeitura);
        }

        public List<PrefeitoInfo> GetPrefeitoAnteriores(Int32 idPrefeitura)
        {
            return ProxyPrefeitura.Service.GetPrefeitosAnterioresByPrefeitura(idPrefeitura).ToList();
        }

        public void UpdatePrefeito(PrefeitoInfo pre)
        {
            ProxyPrefeitura.Service.UpdatePrefeito(pre);
        }

        public void AddPrefeito(PrefeitoInfo pre)
        {
            ProxyPrefeitura.Service.AddPrefeito(pre);
        }

        public void DeletePrefeito(Int32 idPrefeito)
        {
            ProxyPrefeitura.Service.DeletePrefeito(idPrefeito);
        }

        public void SubstituirPrefeito(Int32 idPrefeitura)
        {
            ProxyPrefeitura.Service.SubstituirPrefeito(idPrefeitura);
        } 
        #endregion


        #region AtualizacaoAnual
        public List<PrefeituraAtualizacaoAnualInfo> GetPrefeituraAtualizacaoAnual()
        {
            return ProxyPrefeitura.Service.GetPrefeituraAtualizacaoAnual();
        }

        public void SavePrefeituraAtualizacaoAnual(PrefeituraAtualizacaoAnualInfo prefeituraAtualizacaoAnualInfo, bool commit)
        {
            ProxyPrefeitura.Service.SavePrefeituraAtualizacaoAnual(prefeituraAtualizacaoAnualInfo, commit);
        }

        #endregion

        #region FMAS
        public FundoMunicipalInfo GetFMAS(Int32 idPrefeitura)
        {
            return ProxyPrefeitura.Service.GetFMAS(idPrefeitura);
        }

        public void SaveFMAS(FundoMunicipalInfo fmas)
        {
            ProxyPrefeitura.Service.SaveFMAS(fmas);
        }

        public void SaveFontesRecursosFMAS(FundoMunicipalInfo fmas, List<PrevisaoOrcamentariaInfo > previsoesOrcamentarias, int exercicio)
        {
            ProxyPrefeitura.Service.SaveFontesRecursosFMAS(fmas, previsoesOrcamentarias, exercicio);
        }
        #endregion

        #region FMAS: [Gestor "FUNDO" FMAS]
        public GestorFundoMunicipalInfo GetGestorFundoMunicipal(Int32 idPrefeitura)
        {
            return ProxyPrefeitura.Service.GetGestorFundoMunicipalByPrefeitura(idPrefeitura);
        }
        public List<GestorFundoMunicipalInfo> GetGestoresFundoMunicipalAnteriores(Int32 idPrefeitura)
        {
            return ProxyPrefeitura.Service.GetGestoresFundoMunicipalAnterioresByPrefeitura(idPrefeitura).ToList();
        }
        public void AddGestorFundoMunicipal(GestorFundoMunicipalInfo ges)
        {
            ProxyPrefeitura.Service.SaveGestorFundoMunicipal(ges);
        }
        public void UpdateGestorFundoMunicipal(GestorFundoMunicipalInfo ges)
        {
            ProxyPrefeitura.Service.UpdateGestorFundoMunicipal(ges);
        }
        public void DeleteGestorFundoMunicipal(Int32 idPrefeito)
        {
            ProxyPrefeitura.Service.DeleteGestorFundoMunicipal(idPrefeito);
        }
        public void SubstituirGestorFundoMunicipal(Int32 idPrefeitura, DateTime dataTerminoGestao)
        {
            ProxyPrefeitura.Service.SubstituirGestorFundoMunicipal(idPrefeitura, dataTerminoGestao);
        }
        public List<TipoGestorMunicipalInfo> GetTiposGestoresMunicipal()
        {
            return ProxyPrefeitura.Service.GetTiposGestoresMunicipal();
        }

        #endregion

        #region Conselho Municipal
        public ConselhoMunicipalInfo GetConselhoMunicipal(Int32 idPrefeitura)
        {
            return ProxyPrefeitura.Service.GetConselhoMunicipalByPrefeitura(idPrefeitura);
        }

        public void SaveConselhoMunicipal(ConselhoMunicipalInfo fmas, Boolean ignorarValidacao = false)
        {
            ProxyPrefeitura.Service.SaveConselhoMunicipal(fmas, ignorarValidacao);
        }

        public List<ConselhoMunicipalPresidenteAnteriorInfo> GetPresidentesAnteriores(Int32 idConselhoMunicipal)
        {
            return ProxyPrefeitura.Service.GetPresidentesAnterioresByConselhoMunicipal(idConselhoMunicipal).ToList();
        }

        public void SavePresidenteAnteriorConselhoMunicipal(ConselhoMunicipalPresidenteAnteriorInfo fmas)
        {
            ProxyPrefeitura.Service.SavePresidenteAnteriorConselhoMunicipal(fmas);
        }

        public void DeletePresidenteAnteriorConselhoMunicipal(ConselhoMunicipalPresidenteAnteriorInfo fmas)
        {
            ProxyPrefeitura.Service.DeletePresidenteAnteriorConselhoMunicipal(fmas);
        }

        #endregion

        #region Gestor Municipal
        public GestorMunicipalInfo GetAtualGestorMunicipal(Int32 idPrefeitura)
        {
            return ProxyPrefeitura.Service.GetAtualGestorMunicipalByPrefeitura(idPrefeitura);
        }
        public List<GestorMunicipalInfo> GetGestoresAnteriores(Int32 idPrefeitura)
        {
            return ProxyPrefeitura.Service.GetGestoresMunicipaisAnterioresByPrefeitura(idPrefeitura).ToList();
        }

        public void UpdateGestor(GestorMunicipalInfo ges)
        {
            ProxyPrefeitura.Service.UpdateGestorMunicipal(ges);
        }

        public Int32 AddGestor(GestorMunicipalInfo ges)
        {
          return  ProxyPrefeitura.Service.AddGestorMunicipal(ges);
        }

        public void DeleteGestor(Int32 idPrefeito)
        {
            ProxyPrefeitura.Service.DeleteGestorMunicipal(idPrefeito);
        }

        public void SubstituirGestor(Int32 idPrefeitura, DateTime dataTerminoGestao)
        {
            ProxyPrefeitura.Service.SubstituirGestorMunicipal(idPrefeitura, dataTerminoGestao);
        }
        #endregion

        #region Orgao Gestor
        public OrgaoGestorInfo GetOrgaoGestor(Int32 idPrefeitura)
        {
            return ProxyPrefeitura.Service.GetOrgaoGestorByPrefeitura(idPrefeitura);
        }

        public void SaveOrgaoGestor(OrgaoGestorInfo org)
        {
            ProxyPrefeitura.Service.SaveOrgaoGestor(org);
        }
        #endregion

        #region Conselhos Existentes
        public ConselhoExistenteInfo GetConselhoExistenteById(int id)
        {
            return ProxyPrefeitura.Service.GetConselhoExistenteById(id);
        }

        public ConselhoMunicipalExistentePresidenteInfo GetPresidenteConselhoByIdConselho(int idConselho)
        {
            return ProxyPrefeitura.Service.GetPresidenteConselhoByIdConselho(idConselho);
        }

        public List<ConselhoMunicipalExistentePresidenteInfo> GetPresidentesConselhoExistenteByIdConselho(Int32 idConselho)
        {
            return ProxyPrefeitura.Service.GetPresidentesByIdConselhoExistente(idConselho);
        }

        public List<ConselhoMunicipalExistentePresidenteInfo> GetPresidentesConselhoExistenteByIdConselhoPrefeitura(Int32 idConselho, Int32 idPrefeitura)
        {
            return ProxyPrefeitura.Service.GetPresidentesConselhoExistenteByIdConselhoPrefeitura(idConselho, idPrefeitura).ToList();
        }

        public List<IdentificacaoConselhoExistenteInfo> GetIdentificacaoConselhosExistentesByPrefeitura(Int32 idPrefeitura)
        {
            return ProxyPrefeitura.Service.GetIdentificacaoConselhosExistentesByPrefeitura(idPrefeitura).ToList();
        }

        public void UpdateConselhoExistente(ConselhoExistenteInfo conselho)
        {
            ProxyPrefeitura.Service.UpdateConselhoExistente(conselho);
        }

        public Int32 AddConselhoExistente(ConselhoExistenteInfo conselho)
        {
            return ProxyPrefeitura.Service.AddConselhoExistente(conselho);
        }

        public void DeleteConselhoExistente(Int32 idConselho)
        {
            ProxyPrefeitura.Service.DeleteConselhoExistente(idConselho);
        }

        public void UpdatePresidenteConselhoExistente(ConselhoMunicipalExistentePresidenteInfo presidente)
        {
            ProxyPrefeitura.Service.UpdatePresidenteConselhoExistente(presidente);
        }

        public void DeletePresidenteConselhoExistente(int idPresidente)
        {
            ProxyPrefeitura.Service.DeletePresidenteConselhoExistente(idPresidente);
        }
        public void AddPresidenteConselhoExistente(ConselhoMunicipalExistentePresidenteInfo presidente)
        {
            ProxyPrefeitura.Service.AddPresidenteConselhoExistente(presidente);
        }

        public void SubstituirPresidenteConselhoExistente(ConselhoMunicipalExistentePresidenteInfo presidente)
        {
            ProxyPrefeitura.Service.SubstituirPresidenteConselhoExistente(presidente);
        }

        #endregion

        #region previsao orcamentaria

        public List<PrevisaoOrcamentaria2016Info> GetPrevisaoOrcamentaria2016(Int32 idPrefeitura)
        {
            return ProxyPrefeitura.Service.GetPrevisaoOrcamentaria2016ByPrefeitura(idPrefeitura).ToList();
        }

        public List<PrevisaoOrcamentariaInfo> GetPrevisaoOrcamentaria(Int32 idPrefeitura, int exercicio)
        {
            return ProxyPrefeitura.Service.GetPrevisaoOrcamentariaByPrefeitura(idPrefeitura, exercicio).ToList();
        }

        public List<PrevisaoOrcamentariaMunicipalInfo> GetPrevisaoOrcamentariaMunicipal(Int32 idPrefeitura)
        {
            return ProxyPrefeitura.Service.GetPrevisaoOrcamentariaMunicipalByPrefeitura(idPrefeitura).ToList();
        } 
        #endregion

        #region Lei orcamentaria
        public LeiOrcamentariaInfo GetLeiOrcamentaria2016(Int32 idPrefeitura)
        {
            return ProxyPrefeitura.Service.GetLeiOrcamentaria2016ByPrefeitura(idPrefeitura);
        }

        public LeiOrcamentariaInfo GetLeiOrcamentaria(Int32 idPrefeitura, Int32 exercicio)
        {
            return ProxyPrefeitura.Service.GetLeiOrcamentariaByPrefeitura(idPrefeitura, exercicio);
        }

        public void SaveLeiOrcamentaria(LeiOrcamentariaInfo lei, Int32 idPrefeitura)
        {
            ProxyPrefeitura.Service.SaveLeiOrcamentaria(lei);
        } 
        #endregion

        #region beneficio eventual
        public BeneficioEventual2016Info GetBeneficioEventual2016(Int32 idPrefeitura)
        {
            return ProxyPrefeitura.Service.GetBeneficioEventual2016ByPrefeitura(idPrefeitura);
        }

        public List<BeneficioEventualAnualInfo> GetBeneficioEventual(Int32 idPrefeitura)
        {
            return ProxyPrefeitura.Service.GetBeneficioEventualByPrefeitura(idPrefeitura);
        } 
        #endregion

        #region execucao financeira

        public List<ExecucaoFinanceiraInfo> GetExecucaoFinanceira(Int32 idPrefeitura)
        {
            return ProxyPrefeitura.Service.GetExecucaoFinanceiraByPrefeitura(idPrefeitura).ToList();
        }

        public List<ComentarioExecucaoFinanceiraInfo>  GetComentarioExecucaoFinanceira(Int32 idPrefeitura)
        {
            return ProxyPrefeitura.Service.GetComentarioExecucaoFinanceiraByPrefeitura(idPrefeitura);
        }

        public void SaveExecucaoFinanceira(ComentarioExecucaoFinanceiraInfo comentario
                                             , ExecucaoFinanceiraInfo basica
                                             , ExecucaoFinanceiraInfo especialMedia
                                             , ExecucaoFinanceiraInfo especialAlta)
        {
            ProxyPrefeitura.Service.SaveExecucaoFinanceira(comentario, basica, especialMedia, especialAlta);
        }

        #endregion

        #region transferencia de renda
        public List<TransferenciaRenda2016Info> GetTransferenciaRenda2016(Int32 idPrefeitura)
        {
            return ProxyPrefeitura.Service.GetTransferenciaRenda2016ByPrefeitura(idPrefeitura).ToList();
        }

        public List<TransferenciaRendaAnualInfo> GetTransferenciaRenda(Int32 idPrefeitura)
        {
            return ProxyPrefeitura.Service.GetTransferenciaRendaByPrefeitura(idPrefeitura).ToList();
        } 
        #endregion

        public int GetMetaPrevista(Int32 idPrograma, string CNPJ)
        {
            return ProxyPrefeitura.Service.GetMetaPrevista(idPrograma, CNPJ);
        }

        #region indices de gestao
        public IndiceGestaoDescentralizadaInfo GetIndiceGestaoDescentralizada(Int32 idPrefeitura, int exercicio)
        {
            return ProxyPrefeitura.Service.GetIndiceGestaoDescentralizadaByPrefeitura(idPrefeitura, exercicio);
        }

        public IndiceGestaoDescentralizadaInfo GetIndiceGestaoDescentralizada2016(Int32 idPrefeitura)
        {
            return ProxyPrefeitura.Service.GetIndiceGestaoDescentralizada2016ByPrefeitura(idPrefeitura);
        }

        #endregion
        
        public ProgramasDesenvolvidosMunicipio2016Info GetProgramasDesenvolvidosMunicipio2016(Int32 idPrefeitura)
        {
            return ProxyPrefeitura.Service.GetProgramasDesenvolvidosMunicipio2016(idPrefeitura);
        }
    }
}
