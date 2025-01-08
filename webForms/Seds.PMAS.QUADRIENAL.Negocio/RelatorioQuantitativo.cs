using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class RelatorioQuantitativo
    {
        public List<DistribuicaoPorteNivelGestaoInfo> GetDistribuicaoMunicipiosPorteNivelGestao(RelatorioFiltroInfo filtro)
        {
            try
            {
                var retorno = (from a in new RelatorioDescritivo().GetInformacoesMunicipaisBasicas(filtro).ToList()
                               select new
                               {
                                   IdNivelGestao = a.IdNivelGestao,
                                   Porte = (a.Habitantes <= 20000 ? "Pequeno I" : string.Empty)
                                   + (a.Habitantes >= 20001 && a.Habitantes <= 50000 ? "Pequeno II" : string.Empty)
                                   + (a.Habitantes >= 50001 && a.Habitantes <= 100000 ? "Médio" : string.Empty)
                                   + (a.Habitantes >= 100001 && a.Habitantes <= 900000 ? "Grande" : string.Empty)
                                   + (a.Habitantes > 900000 ? "Metrópole" : string.Empty),
                                   Populacao = a.Habitantes <= 20000 ? (int)EPorteMunicipio.PequenoI
                                   : a.Habitantes >= 20001 && a.Habitantes <= 50000 ? (int)EPorteMunicipio.PequenoII
                                   : a.Habitantes >= 50001 && a.Habitantes <= 100000 ? (int)EPorteMunicipio.Medio
                                   : a.Habitantes >= 100001 && a.Habitantes <= 900000 ? (int)EPorteMunicipio.Grande
                                   : (int)EPorteMunicipio.Metropole,
                               }).OrderBy(obj => obj.Populacao).ToList();

                var resultadoagrupado = (from p in retorno
                                         group p by new
                                         {
                                             p.Populacao,
                                             p.Porte,

                                         } into g
                                         select new DistribuicaoPorteNivelGestaoInfo()
                                         {
                                             Porte = g.Key.Porte,
                                             Inicial = g.Count(p => p.IdNivelGestao == (int)ENivelGestao.Inicial),
                                             Basica = g.Count(p => p.IdNivelGestao == (int)ENivelGestao.Basica),
                                             Plena = g.Count(p => p.IdNivelGestao == (int)ENivelGestao.Plena),
                                             NaoHabilitado = g.Count(p => p.IdNivelGestao == (int)ENivelGestao.Naohabilitado),
                                             Total = g.Count(p => p.IdNivelGestao == (int)ENivelGestao.Inicial) +
                                                     g.Count(p => p.IdNivelGestao == (int)ENivelGestao.Basica) +
                                                     g.Count(p => p.IdNivelGestao == (int)ENivelGestao.Plena) +
                                                     g.Count(p => p.IdNivelGestao == (int)ENivelGestao.Naohabilitado),
                                             Porcentagem = 0
                                         }).ToList();

                resultadoagrupado.ToList().ForEach(obj => obj.Porcentagem = ((obj.Total) / Convert.ToDecimal(resultadoagrupado.Sum(r => r.Total))));
                return resultadoagrupado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public IQueryable<QuantidadesServicosLocaisExecucaoInfo> GetQuantidadeServicosLocaisExecucao(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<QuantidadesServicosLocaisExecucaoInfo>>().GetQuery()
                               select c;


                if (filtro.MunIDs != null && filtro.MunIDs.Count > 0)
                    consulta = from c in consulta
                               where filtro.MunIDs.Contains(c.IdMunicipio)
                               select c;

                if (filtro.DrdIDs != null && filtro.DrdIDs.Count > 0)
                    consulta = from c in consulta
                               where filtro.DrdIDs.Contains(c.IdDrads)
                               select c;

                if (filtro.RegIDs != null && filtro.RegIDs.Count > 0)
                    consulta = from c in consulta
                               where filtro.RegIDs.Contains(c.IdRegiaoMetropolitana)
                               select c;

                if (filtro.MacroRegiaoIDs != null && filtro.MacroRegiaoIDs.Count > 0)
                    consulta = from c in consulta
                               where filtro.MacroRegiaoIDs.Contains(c.IdMacroRegiao)
                               select c;

                if (filtro.NiveisGestao != null && filtro.NiveisGestao.Count > 0)
                    consulta = from c in consulta
                               where filtro.NiveisGestao.Contains(c.IdNivelGestao)
                               select c;

                if (filtro.Portes != null && filtro.Portes.Count > 0)
                    consulta = from c in consulta
                               where filtro.Portes.Contains(c.IdPorte)
                               select c;

                return consulta.OrderBy(c => c.Municipio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DistribuicaoSituacaoVulnerabilidadeInfo> GetDistribuicaoSituacaoVulnerabilidade(RelatorioFiltroInfo filtro)
        {
            try
            {
                var query = from c in ObjectFactory.GetInstance<IRepository<DistribuicaoSituacaoVulnerabilidadeInfo>>().GetQuery()
                                select c;

                if (filtro.MunIDs != null && filtro.MunIDs.Count > 0)
                    query = from c in query
                               where filtro.MunIDs.Contains(c.IdMunicipio)
                               select c;

                if (filtro.DrdIDs != null && filtro.DrdIDs.Count > 0)
                    query = from c in query
                               where filtro.DrdIDs.Contains(c.IdDrads)
                               select c;

                if (filtro.RegIDs != null && filtro.RegIDs.Count > 0)
                    query = from c in query
                               where filtro.RegIDs.Contains(c.IdRegiaoMetropolitana)
                               select c;

                if (filtro.MacroRegiaoIDs != null && filtro.MacroRegiaoIDs.Count > 0)
                    query = from c in query
                               where filtro.MacroRegiaoIDs.Contains(c.IdMacroRegiao)
                               select c;

                if (filtro.NiveisGestao != null && filtro.NiveisGestao.Count > 0)
                    query = from c in query
                               where filtro.NiveisGestao.Contains(c.IdNivelGestao)
                               select c;

                if (filtro.Portes != null && filtro.Portes.Count > 0)
                    query = from c in query
                               where filtro.Portes.Contains(c.IdPorte)
                               select c;

                var lista = query.ToList();

                var somaTotal = lista.Sum(r => r.Total);

                var consulta = lista.Distinct(new DistribuicaoSituacaoVulnerabilidadeInfoComparer()).ToList();
                consulta.ForEach(x =>
                {
                    var r = lista.Where(c => x.Id == c.Id).ToList();
                    x.Gravidade1 = r.Sum(c => c.Gravidade1);
                    x.Gravidade2 = r.Sum(c => c.Gravidade2);
                    x.Gravidade3 = r.Sum(c => c.Gravidade3);
                    x.Gravidade4 = r.Sum(c => c.Gravidade4);
                    x.Gravidade5 = r.Sum(c => c.Gravidade5);
                    x.Gravidade6 = r.Sum(c => c.Gravidade6);
                    x.Gravidade7 = r.Sum(c => c.Gravidade7);
                    x.Gravidade8 = r.Sum(c => c.Gravidade8);
                    x.Gravidade9 = r.Sum(c => c.Gravidade9);
                    x.Gravidade10 = r.Sum(c => c.Gravidade10);
                    x.Total = r.Sum(c => c.Total);
                });

                consulta.ForEach(c => c.Porcentagem = ((c.Total) / Convert.ToDecimal((somaTotal > 0 ? somaTotal : 1))));

                return consulta.OrderBy(c => c.Id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
