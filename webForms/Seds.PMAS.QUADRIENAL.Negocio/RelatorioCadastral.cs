using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class RelatorioCadastral
    {
        public IQueryable<InformacoesCadastraisPrefeiturasInfo> GetInformacoesCadastraisPrefeituras(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<InformacoesCadastraisPrefeiturasInfo>>().GetQuery()
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

                return consulta.OrderBy(c => c.Municipio).ThenBy(c => c.Drads);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<InformacoesCadastraisOrgaoGestorInfo> GetInformacoesCadastraisOrgaoGestor(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<InformacoesCadastraisOrgaoGestorInfo>>().GetQuery()
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

                return consulta.OrderBy(c => c.Municipio).ThenBy(c => c.Drads);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<InformacoesCadastraisConselhoMunicipalInfo> GetInformacoesCadastraisConselhoMunicipal(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<InformacoesCadastraisConselhoMunicipalInfo>>().GetQuery()
                               select c;

                if (filtro.TipoConselho.HasValue)
                    consulta = from c in consulta
                               where c.IdTipoConselho == filtro.TipoConselho.Value
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

                return consulta.OrderBy(c => c.Municipio).ThenBy(c => c.Drads);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public List<InformacoesCadastraisConselhoMunicipalInfo> GetInformacoesCadastraisConselhoMunicipal(RelatorioFiltroInfo filtro)
        //{

        //    try
        //    {
        //        var consulta = from c in ObjectFactory.GetInstance<IRepository<InformacoesCadastraisConselhoMunicipalInfo>>().GetQuery()
        //                       select c;

        //        if (filtro.TipoConselho.HasValue)
        //            consulta = from c in consulta
        //                       where c.IdTipoConselho == filtro.TipoConselho.Value
        //                       select c;

        //        if (filtro.MunIDs != null && filtro.MunIDs.Count > 0)
        //            consulta = from c in consulta
        //                       where filtro.MunIDs.Contains(c.IdMunicipio)
        //                       select c;

        //        if (filtro.DrdIDs != null && filtro.DrdIDs.Count > 0)
        //            consulta = from c in consulta
        //                       where filtro.DrdIDs.Contains(c.IdDrads)
        //                       select c;

        //        if (filtro.RegIDs != null && filtro.RegIDs.Count > 0)
        //            consulta = from c in consulta
        //                       where filtro.RegIDs.Contains(c.IdRegiaoMetropolitana)
        //                       select c;

        //        if (filtro.MacroRegiaoIDs != null && filtro.MacroRegiaoIDs.Count > 0)
        //            consulta = from c in consulta
        //                       where filtro.MacroRegiaoIDs.Contains(c.IdMacroRegiao)
        //                       select c;

        //        if (filtro.NiveisGestao != null && filtro.NiveisGestao.Count > 0)
        //            consulta = from c in consulta
        //                       where filtro.NiveisGestao.Contains(c.IdNivelGestao)
        //                       select c;

        //        if (filtro.Portes != null && filtro.Portes.Count > 0)
        //            consulta = from c in consulta
        //                       where filtro.Portes.Contains(c.IdPorte)
        //                       select c;

        //        return consulta.OrderBy(c => c.Municipio).ThenBy(c => c.Drads);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<InformacoesCadastraisLocalExecucaoInfo> GetInformacoesCadastraisLocalExecucao(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<InformacoesCadastraisLocalExecucaoInfo>>().GetQuery()
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

                if (filtro.TipoExecutora != null && filtro.TipoExecutora.Count > 0)
                {
                    var lstTipoExecutora = filtro.TipoExecutora.Select(t => Convert.ToInt32(t)).ToList();
                    consulta = from c in consulta
                               where lstTipoExecutora.Contains(c.IdTipoUnidade)
                               select c;
                }



                //if (filtro.TipoServico.HasValue)
                //    consulta = from c in consulta
                //               where c.IdTipoServico == filtro.TipoServico.Value
                //               select c;

                if (filtro.TipoProtecaoSocial.HasValue)
                    consulta = from c in consulta
                               where c.IdTipoProtecao == filtro.TipoProtecaoSocial.Value
                               select c;


                if (filtro.ServicoSubtificado.HasValue)
                {
                    consulta = from c in consulta
                               where c.IdTipoServico == filtro.ServicoSubtificado.Value
                               select c;
                }
                else if (filtro.TipoServico.HasValue)
                {
                    //Filtrar todos os serviços não tipificados por Tipo de Proteção Basica
                    if (filtro.TipoServico == 138)
                    {
                        consulta = from c in consulta
                                   where c.IdTipoProtecao == 1 && c.ServicoNaoTipificado == true
                                   select c;
                    }
                    //Filtrar todos os serviços não tipificados por Tipo de Proteção Media
                    else if (filtro.TipoServico == 145)
                    {
                        consulta = from c in consulta
                                   where c.IdTipoProtecao == 2 && c.ServicoNaoTipificado == true
                                   select c;
                    }
                    else
                    {
                        consulta = from c in consulta
                                   where c.IdTipoServico == filtro.TipoServico.Value
                                   select c;
                    }

                }

                if (filtro.Usuario.HasValue)
                    consulta = from c in consulta
                               where c.IdUsuarioTipoServico == filtro.Usuario.Value
                               select c;

                var retorno = consulta.OrderBy(c => c.Municipio).ThenBy(c => c.LocalExecucao).ToList();
                foreach (var c in retorno)
                {
                    c.IdTipoProtecao = new Nullable<Int16>();
                    c.IdTipoServico = new Nullable<Int32>();
                    // c.IdUsuarioTipoServico = new Nullable<Int32>();
                }

                return retorno.Distinct().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
