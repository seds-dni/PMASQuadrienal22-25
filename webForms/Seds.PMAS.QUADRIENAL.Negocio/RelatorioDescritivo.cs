using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using System.Data.Objects.SqlClient;
using Seds.PMAS.QUADRIENAL.Persistencia;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class RelatorioDescritivo
    {
        public IQueryable<InformacoesMunicipaisBasicasInfo> GetInformacoesMunicipaisBasicas(RelatorioFiltroInfo filtro)
        {
            try
            {

                DateTime dt = Convert.ToDateTime(filtro.DataImplantacao.ToString());

                var lista = (ContextManager.GetContext() as PMASContext).GetInformacoesMunicipaisBasicas(dt);

                var consulta = from c in lista //ObjectFactory.GetInstance<IRepository<InformacoesMunicipaisBasicasInfo>>().GetQuery()
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

                return consulta.OrderBy(c => c.Municipio).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<InformacoesBasicasDradsInfo> GetInformacoesBasicasDrads(RelatorioFiltroInfo filtro)
        {

            PMASContext pmas = new PMASContext();
            DateTime dt = Convert.ToDateTime(filtro.DataImplantacao.ToString());

            var lista = (ContextManager.GetContext() as PMASContext).GetInformacoesBasicasDrads(dt);


            try
            {
                var consulta = from c in lista //ObjectFactory.GetInstance<IRepository<InformacoesBasicasDradsInfo>>().GetQuery()
                               select c;


                if (filtro.DrdIDs != null && filtro.DrdIDs.Count > 0)
                    consulta = from c in consulta
                               where filtro.DrdIDs.Contains(c.IdDrads)
                               select c;

                if (filtro.MacroRegiaoIDs != null && filtro.MacroRegiaoIDs.Count > 0)
                    consulta = from c in consulta
                               where filtro.MacroRegiaoIDs.Contains(c.IdMacroRegiao)
                               select c;

                return consulta.OrderBy(c => c.Drads);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<InformacoesFMASInfo> GetInformacoesFMAS(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<InformacoesFMASInfo>>().GetQuery()
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


                if (filtro.Exercicio != null && filtro.Exercicio.Value > 0)
                    consulta = from c in consulta
                               where filtro.Exercicio.Value == c.EXERCICIO.Value
                               select c;


                return consulta.OrderBy(c => c.Municipio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<FuncionamentoCRASInfo> GetFuncionamentoCRAS(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<FuncionamentoCRASInfo>>().GetQuery()
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

                return consulta.OrderBy(c => c.Municipio).ThenBy(c => c.Nome);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<FuncionamentoCREASInfo> GetFuncionamentoCREAS(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<FuncionamentoCREASInfo>>().GetQuery()
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

                return consulta.OrderBy(c => c.Municipio).ThenBy(c => c.Nome);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<FuncionamentoCentroPOPInfo> GetFuncionamentoCentroPOP(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<FuncionamentoCentroPOPInfo>>().GetQuery()
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

                return consulta.OrderBy(c => c.Municipio).ThenBy(c => c.Nome);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<ConselhosMunicipaisExistentesInfo> GetConselhosMunicipaisExistentes(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<ConselhosMunicipaisExistentesInfo>>().GetQuery()
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

        public IQueryable<OrganizacaoOrgaoGestorInfo> GetOrganizacaoOrgaoGestor(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<OrganizacaoOrgaoGestorInfo>>().GetQuery()
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

                if (filtro.Exercicio != null && filtro.Exercicio.Value > 0)
                    consulta = from c in consulta
                               where filtro.Exercicio.Value == c.Exercicio
                               select c;

                return consulta.OrderBy(c => c.Municipio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<RHOrgaoGestorInfo> GetRHOrgaoGestor(RelatorioFiltroInfo filtro)
        {
            try
            {

                var lista = (ContextManager.GetContext() as PMASContext).GetRHOrgaoGestor(Convert.ToInt32(filtro.Exercicio));

                var consulta = from c in lista
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
                if (filtro.Exercicio != null && filtro.Exercicio.Value > 0)
                    consulta = from c in consulta
                               where filtro.Exercicio.Value == c.Exercicio
                               select c;



                return consulta.OrderBy(c => c.Municipio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RHRedeExecutoraInfo> GetRHRedeExecutora(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<RHRedeExecutoraInfo>>().GetQuery()
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
                    if (filtro.TipoExecutora.Contains(ETipoUnidade.Publica))
                    {
                        if (!filtro.TipoExecutora.Contains(ETipoUnidade.CRAS))
                        {
                            filtro.TipoExecutora.Add(ETipoUnidade.CRAS);
                        }
                        if (!filtro.TipoExecutora.Contains(ETipoUnidade.CREAS))
                        {
                            filtro.TipoExecutora.Add(ETipoUnidade.CREAS);
                        }
                        if (!filtro.TipoExecutora.Contains(ETipoUnidade.CentroPOP))
                        {
                            filtro.TipoExecutora.Add(ETipoUnidade.CentroPOP);
                        }
                    }
                    var lstTipoExecutora = filtro.TipoExecutora.Select(t => Convert.ToInt32(t)).ToList();
                    consulta = from c in consulta
                               where lstTipoExecutora.Contains(c.IdTipoUnidade)
                               select c;
                }

                if (filtro.Usuario.HasValue)
                    consulta = from c in consulta
                               where c.IdUsuarioTipoServico == filtro.Usuario.Value
                               select c;

                //if (filtro.TipoServico.HasValue)
                //    consulta = from c in consulta
                //               where c.IdTipoServico == filtro.TipoServico.Value
                //               select c;


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


                if (filtro.TipoProtecaoSocial.HasValue)
                    consulta = from c in consulta
                               where c.IdTipoProtecao == filtro.TipoProtecaoSocial.Value
                               select c;

                var retorno = consulta.OrderBy(c => c.Municipio).ThenBy(c => c.LocalExecucao).ToList();
                foreach (var c in retorno)
                {
                    c.IdTipoProtecao = new Nullable<Int16>();
                    c.IdTipoServico = new Nullable<Int32>();
                    c.IdUsuarioTipoServico = new Nullable<Int32>();
                }

                return retorno.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<ProgramasMunicipaisTransferenciaRendaInfo> GetProgramasMunicipaisTransferenciaRenda(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<ProgramasMunicipaisTransferenciaRendaInfo>>().GetQuery()
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

        public IQueryable<ProgramaProjetoGeralInfo> GetProgramaProjetoGeral(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<ProgramaProjetoGeralInfo>>().GetQuery()
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

                if (filtro.AbrangenciasProgramas != null && filtro.AbrangenciasProgramas.Count > 0)
                    consulta = from c in consulta
                               where filtro.AbrangenciasProgramas.Contains(c.NivelAbrangencia)
                               select c;

                return consulta.OrderBy(c => c.Municipio).Where(c => c.Aderiu == 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<SaoPauloAmigoIdosoInfo> GetProgramaSaoPauloAmigoIdoso(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<SaoPauloAmigoIdosoInfo>>().GetQuery()
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

        public IQueryable<SaoPauloSolidarioInfo> GetProgramaSaoPauloSolidario(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<SaoPauloSolidarioInfo>>().GetQuery()
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

        public IQueryable<IntegracaoServicoInfo> GetIntegracaoServico(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<IntegracaoServicoInfo>>().GetQuery()
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

        public IQueryable<AcaoMonitoramentoInfo> GetAcoesMonitoramento(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<AcaoMonitoramentoInfo>>().GetQuery()
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

        public IQueryable<AcaoAvaliacaoInfo> GetAcoesAvaliacao(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<AcaoAvaliacaoInfo>>().GetQuery()
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

        public List<RelAcaoVigilanciaInfo> GetAcoesVigilanciaSocioassistencial(RelatorioFiltroInfo filtro)
        {
            try
            {

                var lista = (ContextManager.GetContext() as PMASContext).GetRelatorioAcaoVigilancia(Convert.ToInt32(filtro.Exercicio));

                var consulta = from c in lista
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

                return consulta.OrderBy(c => c.Municipio).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DistribuicaoEstadualProtecaoSocialInfo> GetDistribuicaoEstadualProtecaoSocial(RelatorioFiltroInfo filtro)
        {
            try
            {

                var lista = (ContextManager.GetContext() as PMASContext).GetDistribuicaoEstadualProtecaoSocialProc(filtro.Exercicio.Value);

                var consulta = from c in lista //ObjectFactory.GetInstance<IRepository<DistribuicaoEstadualProtecaoSocialInfo>>().GetQuery()
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

                return consulta.OrderBy(c => c.Municipio).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<DistribuicaoEstadualProgramaTrabalhoInfo> GetDistribuicaoEstadualProgramaTrabalho(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<DistribuicaoEstadualProgramaTrabalhoInfo>>().GetQuery()
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

                if (filtro.Exercicio != null && filtro.Exercicio.Value > 0)
                    consulta = from c in consulta
                               where filtro.Exercicio.Value == c.Exercicio.Value
                               select c;

                return consulta.OrderBy(c => c.Municipio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<ResumoTransferenciaRendaInfo> GetResumoTransferenciaRenda(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<ResumoTransferenciaRendaInfo>>().GetQuery()
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

        public IQueryable<RelatorioAnaliseDiagnosticaProcInfo> GetAnaliseDiagnostica(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = (ContextManager.GetContext() as PMASContext).GetAnaliseDiagnosticaExercicio(Convert.ToInt32(filtro.Exercicio));

                if (filtro.MunIDs != null && filtro.MunIDs.Count > 0)
                    consulta = from c in consulta
                               where filtro.MunIDs.Contains(c.IdMunicipio)
                               select c;

                if (filtro.DrdIDs != null && filtro.DrdIDs.Count > 0)
                    consulta = from c in consulta
                               where filtro.DrdIDs.Contains(c.IdDrads)
                               select c;

                if (filtro.Exercicio != null && filtro.Exercicio != 0)
                    consulta = from c in consulta
                               where filtro.Exercicio == c.Exercicio
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

                if (filtro.SituacoesVulnerabilidade != null && filtro.SituacoesVulnerabilidade.Count > 0)
                {
                    consulta = from c in consulta
                               where filtro.SituacoesVulnerabilidade.Contains(c.IdSituacaoVulnerabilidade)
                               select c;

                    if (filtro.SituacaoVulnerabilidadeCondicao == "E")
                    {
                        consulta = consulta.GroupBy(t => t.IdPrefeitura).Where(t => t.Count() == 2).SelectMany(t => t);
                    }
                }

                return consulta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<ComunidadesPovosGruposEspecificosInfo> GetAnaliseDiagnosticaComunidades(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<ComunidadesPovosGruposEspecificosInfo>>().GetQuery()
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

        public IQueryable<ServicoEstadualizadoInfo> GetServicosEstadualizados(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<ServicoEstadualizadoInfo>>().GetQuery()
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

        public List<InformacoesProgramaFamiliaPaulistaInfo> GetInformacoesProgramaFamiliaPaulista(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<InformacoesProgramaFamiliaPaulistaInfo>>().GetQuery()
                               select c;

                if (filtro.MunIDs != null && filtro.MunIDs.Count > 0)
                    consulta = from c in consulta
                               where filtro.MunIDs.Contains(c.IdMunicipio)
                               select c;

                if (filtro.DrdIDs != null && filtro.DrdIDs.Count > 0)
                    consulta = from c in consulta
                               where filtro.DrdIDs.Contains(c.IdDrads)
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


                var retorno = consulta.OrderBy(c => c.Municipio).ToList();

                var returnList = (from pro in retorno
                                  select new
                                  {
                                      pro.IdPrefeitura,
                                      pro.IdIdentificacao,
                                      pro.NomeResponsavel,
                                      pro.NumeroFamilias,
                                      pro.NumeroIdentificacao,
                                      pro.Bairros
                                  }).ToList();

                foreach (var c in retorno)
                {
                    var listaBairros = returnList.Where(t => t.IdPrefeitura == c.IdPrefeitura).ToList();
                    c.IdentificacoesTerritorios = new List<IdentificacaoTerritorioInfo>();
                    foreach (var p in listaBairros)
                    {
                        IdentificacaoTerritorioInfo objIdentificacao = new IdentificacaoTerritorioInfo();
                        objIdentificacao.Id = p.IdIdentificacao;
                        objIdentificacao.NumeroIdentificacao = p.NumeroIdentificacao;
                        objIdentificacao.NomeResponsavel = p.NomeResponsavel;
                        objIdentificacao.IdentificacaoTerritorio = p.Bairros;
                        objIdentificacao.NumeroBeneficiarios = p.NumeroFamilias;
                        c.IdentificacoesTerritorios.Add(objIdentificacao);
                    }

                }


                var identificacaoBairros = retorno.Distinct(new GenericEqualityComparer<InformacoesProgramaFamiliaPaulistaInfo>((x, y) =>
                {
                    return x.IdPrefeitura == y.IdPrefeitura;
                })).ToList();

                foreach (var c in identificacaoBairros)
                {
                    var s = retorno.Where(x => x.IdPrefeitura == c.IdPrefeitura);
                    // c.IdentificacoesTerritorios = s.Select(t => new IdentificacaoTerritorioInfo{ Id = t.IdIdentificacao, NumeroIdentificacao = t.NumeroIdentificacao, NumeroBeneficiarios = t.NumeroFamilias, IdentificacaoTerritorio = t.Bairros }).Where;
                }



                return identificacaoBairros;

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public IQueryable<DiagnosticoSocioterritorialInfo> GetGetDiagnosticoSocioterritorial(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<DiagnosticoSocioterritorialInfo>>().GetQuery()
                               select c;
                filtro.DrdIDs = new List<int>();
                filtro.DrdIDs.Add(177);
                if (filtro.MunIDs != null && filtro.MunIDs.Count > 0)
                    consulta = from c in consulta
                               where filtro.MunIDs.Contains(c.IdMunicipio)
                               select c;

                if (filtro.DrdIDs != null && filtro.DrdIDs.Count > 0)
                    consulta = from c in consulta
                               where filtro.DrdIDs.Contains(c.IdDrads)
                               select c;

                return consulta.Where(c => c.VersaoSistema == c.VersaoSistemaDrads && c.VersaoSistemaDrads == 2017).OrderBy(c => c.Municipio);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ExecucaoRecursosCofinanciamentoEstadualInfo> GetExecucaoRecursosCofinanciamentoEstadual(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<ExecucaoRecursosCofinanciamentoEstadualInfo>>().GetQuery()
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

                var retorno = consulta.OrderBy(c => c.Municipio).ToList();

                var servicos = retorno.Distinct(new GenericEqualityComparer<ExecucaoRecursosCofinanciamentoEstadualInfo>((x, y) =>
                {
                    return x.IdPrefeitura == y.IdPrefeitura &&
                        x.ProtecaoSocial == y.ProtecaoSocial;
                })).ToList();

                foreach (var c in servicos)
                {
                    var s = retorno.Where(x => x.IdPrefeitura == c.IdPrefeitura &&
                        x.ProtecaoSocial == c.ProtecaoSocial);
                }

                return servicos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RedeServicoSocioassistencialRelatorio> GetRedeServicoSocioassistencialRelatorio(RelatorioFiltroInfo filtro)
        {
            try
            {
                IEnumerable<RedeServicoSocioassistencialRelatorio> redeServicoSocioassistencialRelatorio = (ContextManager.GetContext() as PMASContext)
                    .GetRedeServicoSocioassistencialRelatorio(filtro.Exercicio.Value);


                if (filtro.ehAtivo)
                {
                    redeServicoSocioassistencialRelatorio = redeServicoSocioassistencialRelatorio.Where(c => c.DataDesativacao == "Em Funcionamento");
                }
                else if(filtro.ehDesativo)
                {
                    redeServicoSocioassistencialRelatorio = redeServicoSocioassistencialRelatorio.Where(c => c.DataDesativacao != "Em Funcionamento");
                }


                if (filtro.MunIDs != null && filtro.MunIDs.Count > 0)
                    redeServicoSocioassistencialRelatorio = redeServicoSocioassistencialRelatorio.Where(c => filtro.MunIDs.Contains(c.IdMunicipio));

                if (filtro.DrdIDs != null && filtro.DrdIDs.Count > 0)
                    redeServicoSocioassistencialRelatorio = redeServicoSocioassistencialRelatorio.Where(c => filtro.DrdIDs.Contains(c.IdDrads));

                if (filtro.RegIDs != null && filtro.RegIDs.Count > 0)
                    redeServicoSocioassistencialRelatorio = redeServicoSocioassistencialRelatorio.Where(c => filtro.RegIDs.Contains(c.IdRegiaoMetropolitana));

                if (filtro.MacroRegiaoIDs != null && filtro.MacroRegiaoIDs.Count > 0)
                    redeServicoSocioassistencialRelatorio = redeServicoSocioassistencialRelatorio.Where(c => filtro.MacroRegiaoIDs.Contains(c.IdMacroRegiao));

                if (filtro.NiveisGestao != null && filtro.NiveisGestao.Count > 0)
                    redeServicoSocioassistencialRelatorio = redeServicoSocioassistencialRelatorio.Where(c => filtro.NiveisGestao.Contains(c.IdNivelGestao));

                if (filtro.Portes != null && filtro.Portes.Count > 0)
                    redeServicoSocioassistencialRelatorio = redeServicoSocioassistencialRelatorio.Where(c => filtro.Portes.Contains(c.IdPorte));

                if (filtro.Abrangencias != null && filtro.Abrangencias.Count > 0)
                    redeServicoSocioassistencialRelatorio = redeServicoSocioassistencialRelatorio.Where(c => filtro.Abrangencias.Contains(c.IdAbrangencia));

                if (filtro.TipoExecutora != null && filtro.TipoExecutora.Count > 0)
                {
                    var lstTipoExecutora = filtro.TipoExecutora.Select(t => Convert.ToInt32(t)).ToList();
                    redeServicoSocioassistencialRelatorio = redeServicoSocioassistencialRelatorio.Where(c => lstTipoExecutora.Contains(c.IdTipoUnidade));
                }
                    if (filtro.Usuario.HasValue)
                        redeServicoSocioassistencialRelatorio = redeServicoSocioassistencialRelatorio.Where(c => c.IdUsuarioTipoServico == filtro.Usuario.Value);
                
                    if (filtro.TipoProtecaoSocial.HasValue)
                        redeServicoSocioassistencialRelatorio = redeServicoSocioassistencialRelatorio.Where(c => c.IdTipoProtecao == filtro.TipoProtecaoSocial.Value);

                    if (filtro.ServicoSubtificado.HasValue)
                    {
                        redeServicoSocioassistencialRelatorio = redeServicoSocioassistencialRelatorio.Where(c => c.IdTipoServico == filtro.ServicoSubtificado.Value);
                    }
                    else if (filtro.TipoServico.HasValue)
                    {
                        //Filtrar todos os serviços não tipificados por Tipo de Proteção Basica
                        if (filtro.TipoServico == 138)
                        {
                            redeServicoSocioassistencialRelatorio = from c in redeServicoSocioassistencialRelatorio
                                       where c.IdTipoProtecao == 1 && c.ServicoNaoTipificado == true
                                       select c;
                        }
                        //Filtrar todos os serviços não tipificados por Tipo de Proteção Media
                        else if (filtro.TipoServico == 145)
                        {
                            redeServicoSocioassistencialRelatorio = from c in redeServicoSocioassistencialRelatorio
                                       where c.IdTipoProtecao == 2 && c.ServicoNaoTipificado == true
                                       select c;
                        }
                        else
                        {
                            redeServicoSocioassistencialRelatorio = from c in redeServicoSocioassistencialRelatorio
                                       where c.IdTipoServico == filtro.TipoServico.Value
                                       select c;
                        }
                    }

                

                if (filtro.Sexo.HasValue)
                    redeServicoSocioassistencialRelatorio = from c in redeServicoSocioassistencialRelatorio
                               where c.IdSexo == filtro.Sexo.Value
                               select c;

                if (filtro.RegiaoMoradia.HasValue)
                    redeServicoSocioassistencialRelatorio = from c in redeServicoSocioassistencialRelatorio
                               where c.IdRegiaoMoradia == filtro.RegiaoMoradia.Value
                               select c;

                if (filtro.CaracteristicasTerritorio.HasValue)
                    redeServicoSocioassistencialRelatorio = from c in redeServicoSocioassistencialRelatorio
                               where c.IdCaracteristicasTerritorio == filtro.CaracteristicasTerritorio.Value
                               select c;

                if (filtro.TipoFinanciamento.HasValue)
                    redeServicoSocioassistencialRelatorio = from c in redeServicoSocioassistencialRelatorio
                               where c.ValorFEAS > 0M
                               select c;



                var retorno = redeServicoSocioassistencialRelatorio.OrderBy(c => c.Municipio).ThenBy(c => c.LocalExecucao).ToList();

                retorno.ForEach(c =>
                {
                    if (c.CaracteristicasTerritorio.Contains("Nenhuma das características citadas"))
                        c.CaracteristicasTerritorio = "Não há uma demanda prioritária";
                });

                return retorno.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<RPrestacaoDeContasBasica> GetPrestacaDeContasProtecaoBasica(RelatorioFiltroInfo filtro)
        {
            try
            {
                IEnumerable<RPrestacaoDeContasBasica> PrestacaDeContasBasicaRelatorio = (ContextManager.GetContext() as PMASContext).GetPretacaoDeContasBasicaRelatorio(filtro.Exercicio.Value);


                if (filtro.MunIDs != null && filtro.MunIDs.Count > 0)
                    PrestacaDeContasBasicaRelatorio = PrestacaDeContasBasicaRelatorio.Where(c => filtro.MunIDs.Contains(c.IdMunicipio));
                
                if (filtro.DrdIDs != null && filtro.DrdIDs.Count > 0)
                    PrestacaDeContasBasicaRelatorio = from c in PrestacaDeContasBasicaRelatorio
                               where filtro.DrdIDs.Contains(c.IdDrads)
                               select c;


                if (filtro.RegIDs != null && filtro.RegIDs.Count > 0)
                    PrestacaDeContasBasicaRelatorio = from c in PrestacaDeContasBasicaRelatorio
                               where filtro.RegIDs.Contains(c.IdRegiaoMetropolitana)
                               select c;

                if (filtro.MacroRegiaoIDs != null && filtro.MacroRegiaoIDs.Count > 0)
                    PrestacaDeContasBasicaRelatorio = from c in PrestacaDeContasBasicaRelatorio
                               where filtro.MacroRegiaoIDs.Contains(c.IdMacroRegiao)
                               select c;

                if (filtro.NiveisGestao != null && filtro.NiveisGestao.Count > 0)
                    PrestacaDeContasBasicaRelatorio = from c in PrestacaDeContasBasicaRelatorio
                               where filtro.NiveisGestao.Contains(c.IdNivelGestao)
                               select c;

                if (filtro.Portes != null && filtro.Portes.Count > 0)
                    PrestacaDeContasBasicaRelatorio = from c in PrestacaDeContasBasicaRelatorio
                               where filtro.Portes.Contains(c.IdPorte)
                               select c;
 
                var retorno = PrestacaDeContasBasicaRelatorio.OrderBy(c => c.Municipio).ToList();

                return retorno;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }

        public List<RPrestacaoDeContasMedia> GetPrestacaDeContasProtecaoMedia(RelatorioFiltroInfo filtro)
        {
            try
            {
                IEnumerable<RPrestacaoDeContasMedia> PrestacaDeContasMediaRelatorio = (ContextManager.GetContext() as PMASContext).GetPretacaoDeContasMediaRelatorio(filtro.Exercicio.Value);


                if (filtro.MunIDs != null && filtro.MunIDs.Count > 0)
                    PrestacaDeContasMediaRelatorio = PrestacaDeContasMediaRelatorio.Where(c => filtro.MunIDs.Contains(c.IdMunicipio));


                if (filtro.DrdIDs != null && filtro.DrdIDs.Count > 0)
                    PrestacaDeContasMediaRelatorio = from c in PrestacaDeContasMediaRelatorio
                                                       where filtro.DrdIDs.Contains(c.IdDrads)
                                                       select c;

                if (filtro.RegIDs != null && filtro.RegIDs.Count > 0)
                    PrestacaDeContasMediaRelatorio = from c in PrestacaDeContasMediaRelatorio
                                                       where filtro.RegIDs.Contains(c.IdRegiaoMetropolitana)
                                                       select c;

                if (filtro.MacroRegiaoIDs != null && filtro.MacroRegiaoIDs.Count > 0)
                    PrestacaDeContasMediaRelatorio = from c in PrestacaDeContasMediaRelatorio
                                                       where filtro.MacroRegiaoIDs.Contains(c.IdMacroRegiao)
                                                       select c;

                if (filtro.NiveisGestao != null && filtro.NiveisGestao.Count > 0)
                    PrestacaDeContasMediaRelatorio = from c in PrestacaDeContasMediaRelatorio
                                                       where filtro.NiveisGestao.Contains(c.IdNivelGestao)
                                                       select c;

                if (filtro.Portes != null && filtro.Portes.Count > 0)
                    PrestacaDeContasMediaRelatorio = from c in PrestacaDeContasMediaRelatorio
                                                       where filtro.Portes.Contains(c.IdPorte)
                                                       select c;

                var retorno = PrestacaDeContasMediaRelatorio.OrderBy(c => c.Municipio).ToList();

                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<RPrestacaoDeContasAlta> GetPrestacaDeContasProtecaoAlta(RelatorioFiltroInfo filtro)
        {
            try
            {
                IEnumerable<RPrestacaoDeContasAlta> PrestacaDeContasAltaRelatorio = (ContextManager.GetContext() as PMASContext).GetPretacaoDeContasAltaRelatorio(filtro.Exercicio.Value);

                if (filtro.MunIDs != null && filtro.MunIDs.Count > 0)
                    PrestacaDeContasAltaRelatorio = PrestacaDeContasAltaRelatorio.Where(c => filtro.MunIDs.Contains(c.IdMunicipio));

                if (filtro.DrdIDs != null && filtro.DrdIDs.Count > 0)
                    PrestacaDeContasAltaRelatorio = from c in PrestacaDeContasAltaRelatorio
                                                      where filtro.DrdIDs.Contains(c.IdDrads)
                                                      select c;

                if (filtro.RegIDs != null && filtro.RegIDs.Count > 0)
                    PrestacaDeContasAltaRelatorio = from c in PrestacaDeContasAltaRelatorio
                                                      where filtro.RegIDs.Contains(c.IdRegiaoMetropolitana)
                                                      select c;

                if (filtro.MacroRegiaoIDs != null && filtro.MacroRegiaoIDs.Count > 0)
                    PrestacaDeContasAltaRelatorio = from c in PrestacaDeContasAltaRelatorio
                                                      where filtro.MacroRegiaoIDs.Contains(c.IdMacroRegiao)
                                                      select c;

                if (filtro.NiveisGestao != null && filtro.NiveisGestao.Count > 0)
                    PrestacaDeContasAltaRelatorio = from c in PrestacaDeContasAltaRelatorio
                                                      where filtro.NiveisGestao.Contains(c.IdNivelGestao)
                                                      select c;

                if (filtro.Portes != null && filtro.Portes.Count > 0)
                    PrestacaDeContasAltaRelatorio = from c in PrestacaDeContasAltaRelatorio
                                                      where filtro.Portes.Contains(c.IdPorte)
                                                      select c;

                var retorno = PrestacaDeContasAltaRelatorio.OrderBy(c => c.Municipio).ToList();

                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<RPrestacaoDeContasBeneficiosEventuais> GetPrestacaDeContasBeneficiosEventuais(RelatorioFiltroInfo filtro)
        {
            try
            {
                IEnumerable<RPrestacaoDeContasBeneficiosEventuais> PrestacaDeContasBeneficiosEventuaisRelatorio = (ContextManager.GetContext() as PMASContext).GetPretacaoDeContasBeneficiosEventuaisRelatorio(filtro.Exercicio.Value);


                if (filtro.MunIDs != null && filtro.MunIDs.Count > 0)
                    PrestacaDeContasBeneficiosEventuaisRelatorio = PrestacaDeContasBeneficiosEventuaisRelatorio.Where(c => filtro.MunIDs.Contains(c.IdMunicipio));

                if (filtro.DrdIDs != null && filtro.DrdIDs.Count > 0)
                    PrestacaDeContasBeneficiosEventuaisRelatorio = from c in PrestacaDeContasBeneficiosEventuaisRelatorio
                                                     where filtro.DrdIDs.Contains(c.IdDrads)
                                                     select c;

                if (filtro.RegIDs != null && filtro.RegIDs.Count > 0)
                    PrestacaDeContasBeneficiosEventuaisRelatorio = from c in PrestacaDeContasBeneficiosEventuaisRelatorio
                                                     where filtro.RegIDs.Contains(c.IdRegiaoMetropolitana)
                                                     select c;

                if (filtro.MacroRegiaoIDs != null && filtro.MacroRegiaoIDs.Count > 0)
                    PrestacaDeContasBeneficiosEventuaisRelatorio = from c in PrestacaDeContasBeneficiosEventuaisRelatorio
                                                     where filtro.MacroRegiaoIDs.Contains(c.IdMacroRegiao)
                                                     select c;

                if (filtro.NiveisGestao != null && filtro.NiveisGestao.Count > 0)
                    PrestacaDeContasBeneficiosEventuaisRelatorio = from c in PrestacaDeContasBeneficiosEventuaisRelatorio
                                                     where filtro.NiveisGestao.Contains(c.IdNivelGestao)
                                                     select c;

                if (filtro.Portes != null && filtro.Portes.Count > 0)
                    PrestacaDeContasBeneficiosEventuaisRelatorio = from c in PrestacaDeContasBeneficiosEventuaisRelatorio
                                                     where filtro.Portes.Contains(c.IdPorte)
                                                     select c;

                var retorno = PrestacaDeContasBeneficiosEventuaisRelatorio.OrderBy(c => c.Municipio).ToList();

                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public List<RPrestacaoDeContasProgramasProjetos> GetPrestacaDeContasProtecaoProgramasProjetos(RelatorioFiltroInfo filtro)
        {
            try
            {
                IEnumerable<RPrestacaoDeContasProgramasProjetos> PrestacaDeContasProgramasProjetosRelatorio = (ContextManager.GetContext() as PMASContext).GetPretacaoDeContasProgramasProjetosRelatorio(filtro.Exercicio.Value);


                if (filtro.MunIDs != null && filtro.MunIDs.Count > 0)
                    PrestacaDeContasProgramasProjetosRelatorio = PrestacaDeContasProgramasProjetosRelatorio.Where(c => filtro.MunIDs.Contains(c.IdMunicipio));

                if (filtro.DrdIDs != null && filtro.DrdIDs.Count > 0)
                    PrestacaDeContasProgramasProjetosRelatorio = from c in PrestacaDeContasProgramasProjetosRelatorio
                                                                    where filtro.DrdIDs.Contains(c.IdDrads)
                                                                    select c;

                if (filtro.RegIDs != null && filtro.RegIDs.Count > 0)
                    PrestacaDeContasProgramasProjetosRelatorio = from c in PrestacaDeContasProgramasProjetosRelatorio
                                                                    where filtro.RegIDs.Contains(c.IdRegiaoMetropolitana)
                                                                    select c;

                if (filtro.MacroRegiaoIDs != null && filtro.MacroRegiaoIDs.Count > 0)
                    PrestacaDeContasProgramasProjetosRelatorio = from c in PrestacaDeContasProgramasProjetosRelatorio
                                                                    where filtro.MacroRegiaoIDs.Contains(c.IdMacroRegiao)
                                                                    select c;

                if (filtro.NiveisGestao != null && filtro.NiveisGestao.Count > 0)
                    PrestacaDeContasProgramasProjetosRelatorio = from c in PrestacaDeContasProgramasProjetosRelatorio
                                                                    where filtro.NiveisGestao.Contains(c.IdNivelGestao)
                                                                    select c;

                if (filtro.Portes != null && filtro.Portes.Count > 0)
                    PrestacaDeContasProgramasProjetosRelatorio = from c in PrestacaDeContasProgramasProjetosRelatorio
                                                                    where filtro.Portes.Contains(c.IdPorte)
                                                                    select c;

                var retorno = PrestacaDeContasProgramasProjetosRelatorio.OrderBy(c => c.Municipio).ToList();

                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public IQueryable<RStatusPrestacaoDeContasInfo> GetStatusPrestacaoDeContas(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<RStatusPrestacaoDeContasInfo>>().GetQuery()
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

        public IQueryable<RAuxilioReclusaoPensaoMorteInfo> GetAuxilioReclusaoPensaoMorte(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<RAuxilioReclusaoPensaoMorteInfo>>().GetQuery()
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

                if (filtro.Exercicio != null && filtro.Exercicio.Value > 0)
                    consulta = from c in consulta
                               where filtro.Exercicio.Value == c.Exercicio
                               select c;


                return consulta.OrderBy(c => c.Municipio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<RStatusLeiOrcamentariaInfo> GetStatusLeiOrcamentaria(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<RStatusLeiOrcamentariaInfo>>().GetQuery()
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

        public IQueryable<RStatusExecucaoFinanceiraInfo> GetStatusExecucaoFinanceira(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<RStatusExecucaoFinanceiraInfo>>().GetQuery()
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

        public List<RedeServicoSocioassistencialRegionalizadosRelatorio> GetRedeServicoRegionalizadosRelatorio(RelatorioFiltroInfo filtro)
        {
            try
            {
                IEnumerable<RedeServicoSocioassistencialRegionalizadosRelatorio> redeServicoRegionalizadosRelatorio = (ContextManager.GetContext() as PMASContext)
                    .GetRedeServicosRegionalizadosRelatorio(filtro.Exercicio.Value);


                if (filtro.ehAtivo)
                {
                    redeServicoRegionalizadosRelatorio = redeServicoRegionalizadosRelatorio.Where(c => c.DataDesativacao == "Em Funcionamento");
                }
                else if (filtro.ehDesativo)
                {
                    redeServicoRegionalizadosRelatorio = redeServicoRegionalizadosRelatorio.Where(c => c.DataDesativacao != "Em Funcionamento");
                }


                if (filtro.MunIDs != null && filtro.MunIDs.Count > 0)
                    redeServicoRegionalizadosRelatorio = redeServicoRegionalizadosRelatorio.Where(c => filtro.MunIDs.Contains(c.IdMunicipio));

                if (filtro.DrdIDs != null && filtro.DrdIDs.Count > 0)
                    redeServicoRegionalizadosRelatorio = redeServicoRegionalizadosRelatorio.Where(c => filtro.DrdIDs.Contains(c.IdDrads));

                if (filtro.RegIDs != null && filtro.RegIDs.Count > 0)
                    redeServicoRegionalizadosRelatorio = redeServicoRegionalizadosRelatorio.Where(c => filtro.RegIDs.Contains(c.IdRegiaoMetropolitana));

                if (filtro.MacroRegiaoIDs != null && filtro.MacroRegiaoIDs.Count > 0)
                    redeServicoRegionalizadosRelatorio = redeServicoRegionalizadosRelatorio.Where(c => filtro.MacroRegiaoIDs.Contains(c.IdMacroRegiao));

                if (filtro.NiveisGestao != null && filtro.NiveisGestao.Count > 0)
                    redeServicoRegionalizadosRelatorio = redeServicoRegionalizadosRelatorio.Where(c => filtro.NiveisGestao.Contains(c.IdNivelGestao));

                if (filtro.Portes != null && filtro.Portes.Count > 0)
                    redeServicoRegionalizadosRelatorio = redeServicoRegionalizadosRelatorio.Where(c => filtro.Portes.Contains(c.IdPorte));

                if (filtro.Abrangencias != null && filtro.Abrangencias.Count > 0)
                    redeServicoRegionalizadosRelatorio = redeServicoRegionalizadosRelatorio.Where(c => filtro.Abrangencias.Contains(c.IdAbrangencia));

                if (filtro.TipoExecutora != null && filtro.TipoExecutora.Count > 0)
                {
                    var lstTipoExecutora = filtro.TipoExecutora.Select(t => Convert.ToInt32(t)).ToList();
                    redeServicoRegionalizadosRelatorio = redeServicoRegionalizadosRelatorio.Where(c => lstTipoExecutora.Contains(c.IdTipoUnidade));
                }
                if (filtro.Usuario.HasValue)
                    redeServicoRegionalizadosRelatorio = redeServicoRegionalizadosRelatorio.Where(c => c.IdUsuarioTipoServico == filtro.Usuario.Value);

                if (filtro.TipoProtecaoSocial.HasValue)
                    redeServicoRegionalizadosRelatorio = redeServicoRegionalizadosRelatorio.Where(c => c.IdTipoProtecao == filtro.TipoProtecaoSocial.Value);

                if (filtro.ServicoSubtificado.HasValue)
                {
                    redeServicoRegionalizadosRelatorio = redeServicoRegionalizadosRelatorio.Where(c => c.IdTipoServico == filtro.ServicoSubtificado.Value);
                }
                else if (filtro.TipoServico.HasValue)
                {
                    //Filtrar todos os serviços não tipificados por Tipo de Proteção Basica
                    if (filtro.TipoServico == 138)
                    {
                        redeServicoRegionalizadosRelatorio = from c in redeServicoRegionalizadosRelatorio
                                                                where c.IdTipoProtecao == 1 && c.ServicoNaoTipificado == true
                                                                select c;
                    }
                    //Filtrar todos os serviços não tipificados por Tipo de Proteção Media
                    else if (filtro.TipoServico == 145)
                    {
                        redeServicoRegionalizadosRelatorio = from c in redeServicoRegionalizadosRelatorio
                                                                where c.IdTipoProtecao == 2 && c.ServicoNaoTipificado == true
                                                                select c;
                    }
                    else
                    {
                        redeServicoRegionalizadosRelatorio = from c in redeServicoRegionalizadosRelatorio
                                                                where c.IdTipoServico == filtro.TipoServico.Value
                                                                select c;
                    }
                }



                if (filtro.Sexo.HasValue)
                    redeServicoRegionalizadosRelatorio = from c in redeServicoRegionalizadosRelatorio
                                                            where c.IdSexo == filtro.Sexo.Value
                                                            select c;

                if (filtro.RegiaoMoradia.HasValue)
                    redeServicoRegionalizadosRelatorio = from c in redeServicoRegionalizadosRelatorio
                                                            where c.IdRegiaoMoradia == filtro.RegiaoMoradia.Value
                                                            select c;

                if (filtro.CaracteristicasTerritorio.HasValue)
                    redeServicoRegionalizadosRelatorio = from c in redeServicoRegionalizadosRelatorio
                                                            where c.IdCaracteristicasTerritorio == filtro.CaracteristicasTerritorio.Value
                                                            select c;




                var retorno = redeServicoRegionalizadosRelatorio.OrderBy(c => c.Municipio).ThenBy(c => c.LocalExecucao).ToList();

                retorno.ForEach(c =>
                {
                    if (c.CaracteristicasTerritorio.Contains("Nenhuma das características citadas"))
                        c.CaracteristicasTerritorio = "Não há uma demanda prioritária";
                });

                return retorno.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RelatorioAEPETIInfo> GetAEPETIRelatorio(RelatorioFiltroInfo filtro)
        {
            try
            {
                IEnumerable<RelatorioAEPETIInfo> consulta = (ContextManager.GetContext() as PMASContext).GetAEPETIRelatorio();


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

                return consulta.OrderBy(c => c.Municipio).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RedeServicoSocioassistencialInfo> GetRedeServicoSocioassistencial(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<RedeServicoSocioassistencialInfo>>().GetQuery()
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

                if (filtro.Abrangencias != null && filtro.Abrangencias.Count > 0)
                    consulta = from c in consulta
                               where filtro.Abrangencias.Contains(c.IdAbrangencia)
                               select c;

                if (filtro.TipoExecutora != null && filtro.TipoExecutora.Count > 0)
                {
                    if (filtro.TipoExecutora.Contains(ETipoUnidade.Publica))
                    {
                        if (!filtro.TipoExecutora.Contains(ETipoUnidade.CRAS))
                        {
                            filtro.TipoExecutora.Add(ETipoUnidade.CRAS);
                        }
                        if (!filtro.TipoExecutora.Contains(ETipoUnidade.CREAS))
                        {
                            filtro.TipoExecutora.Add(ETipoUnidade.CREAS);
                        }
                        if (!filtro.TipoExecutora.Contains(ETipoUnidade.CentroPOP))
                        {
                            filtro.TipoExecutora.Add(ETipoUnidade.CentroPOP);
                        }
                    }
                    var lstTipoExecutora = filtro.TipoExecutora.Select(t => Convert.ToInt32(t)).ToList();
                    consulta = from c in consulta
                               where lstTipoExecutora.Contains(c.IdTipoUnidade)
                               select c;
                }

                if (filtro.Usuario.HasValue)
                    consulta = from c in consulta
                               where c.IdUsuarioTipoServico == filtro.Usuario.Value
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

                if (filtro.TipoProtecaoSocial.HasValue)
                    consulta = from c in consulta
                               where c.IdTipoProtecao == filtro.TipoProtecaoSocial.Value
                               select c;

                if (filtro.SituacaoVulnerabilidade.HasValue)
                    consulta = from c in consulta
                               where c.IdSituacaoVulnerabilidade == filtro.SituacaoVulnerabilidade.Value
                               select c;

                if (filtro.SituacaoEspecifica.HasValue)
                    consulta = from c in consulta
                               where c.IdSituacaoEspecifica == filtro.SituacaoEspecifica.Value
                               select c;

                if (filtro.SituacoesVulnerabilidade != null && filtro.SituacoesVulnerabilidade.Count > 0)
                    consulta = from c in consulta
                               where filtro.SituacoesVulnerabilidade.Contains(c.IdSituacaoVulnerabilidade)
                               select c;

                if (filtro.SituacoesEspecificas != null && filtro.SituacoesEspecificas.Count > 0)
                    consulta = from c in consulta
                               where filtro.SituacoesEspecificas.Contains(c.IdSituacaoEspecifica)
                               select c;

                if (filtro.Sexo.HasValue)
                    consulta = from c in consulta
                               where c.IdSexo == filtro.Sexo.Value
                               select c;

                if (filtro.RegiaoMoradia.HasValue)
                    consulta = from c in consulta
                               where c.IdRegiaoMoradia == filtro.RegiaoMoradia.Value
                               select c;

                if (filtro.CaracteristicasTerritorio.HasValue)
                    consulta = from c in consulta
                               where c.IdCaracteristicasTerritorio == filtro.CaracteristicasTerritorio.Value
                               select c;

                //var retorno = consulta.OrderBy(c => c.Municipio).ThenBy(c => c.LocalExecucao).ToList();

                var retorno = consulta.OrderBy(c => c.Municipio).ThenBy(c => c.LocalExecucao).ToList();

                retorno.ForEach(c =>
                {
                    if (c.CaracteristicasTerritorio.Contains("Nenhuma das características citadas"))
                        c.CaracteristicasTerritorio = "Não há uma demanda prioritária";
                });


                var servicos = retorno.Distinct(new GenericEqualityComparer<RedeServicoSocioassistencialInfo>((x, y) =>
                {
                    return x.IdPrefeitura == y.IdPrefeitura &&
                        x.CodigoUnidade == y.CodigoUnidade &&
                        x.IdTipoUnidade == y.IdTipoUnidade &&
                        x.IdLocal == y.IdLocal &&
                        x.IdTipoProtecao == y.IdTipoProtecao &&
                        x.IdTipoServico == y.IdTipoServico &&
                        x.IdUsuarioTipoServico == y.IdUsuarioTipoServico &&
                        x.IdSexo == y.IdSexo &&
                        x.IdRegiaoMoradia == y.IdRegiaoMoradia &&
                        x.IdCaracteristicasTerritorio == y.IdCaracteristicasTerritorio &&
                        x.ValorFMAS == y.ValorFMAS &&
                        x.ValorFEAS == y.ValorFEAS &&
                        x.ValorFNAS == y.ValorFNAS &&
                        x.ValorFMDCA == y.ValorFMDCA &&
                        x.ValorFEDCA == y.ValorFEDCA &&
                        x.ValorFNDCA == y.ValorFNDCA &&
                        x.ValorPrivado == y.ValorPrivado &&
                        x.NumeroAtendidosMensal == y.NumeroAtendidosMensal &&
                        x.NumeroAtendidosAnual == y.NumeroAtendidosAnual &&
                        x.Abrangencia == y.Abrangencia;
                })).ToList();

                foreach (var c in servicos)
                {
                    var s = retorno.Where(x => x.IdPrefeitura == c.IdPrefeitura &&
                        x.CodigoUnidade == c.CodigoUnidade &&
                        x.IdTipoUnidade == c.IdTipoUnidade &&
                        x.IdLocal == c.IdLocal &&
                        x.IdTipoProtecao == c.IdTipoProtecao &&
                        x.IdTipoServico == c.IdTipoServico &&
                        x.IdUsuarioTipoServico == c.IdUsuarioTipoServico);
                    c.SituacoesEspecificas = s.Select(t => t.SituacaoEspecifica).Distinct().ToList();
                    c.SituacoesVulnerabilidade = s.Select(t => t.SituacaoVulnerabilidade).Distinct().ToList();
                    c.AtividadesSocioassistenciais = s.Select(t => t.AtividadeSocioassistencial).Distinct().ToList();
                }

                return servicos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RedeServicoSocioassistencialDetalhamentoInfo> GetRedeServicoSocioassistencialDetalhamento(RelatorioFiltroInfo filtro)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(filtro.DataImplantacao.ToString());

                var lista = (ContextManager.GetContext() as PMASContext).GetRedeServicoSocioassistencialRelatorioDetalhamento(dt);

                var consulta = lista.AsQueryable();

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

                if (filtro.Abrangencias != null && filtro.Abrangencias.Count > 0)
                    consulta = from c in consulta
                               where filtro.Abrangencias.Contains(c.IdAbrangencia)
                               select c;

                if (filtro.TipoExecutora != null && filtro.TipoExecutora.Count > 0)
                {
                    if (filtro.TipoExecutora.Contains(ETipoUnidade.Publica))
                    {
                        if (!filtro.TipoExecutora.Contains(ETipoUnidade.CRAS))
                        {
                            filtro.TipoExecutora.Add(ETipoUnidade.CRAS);
                        }
                        if (!filtro.TipoExecutora.Contains(ETipoUnidade.CREAS))
                        {
                            filtro.TipoExecutora.Add(ETipoUnidade.CREAS);
                        }
                        if (!filtro.TipoExecutora.Contains(ETipoUnidade.CentroPOP))
                        {
                            filtro.TipoExecutora.Add(ETipoUnidade.CentroPOP);
                        }
                    }
                    var lstTipoExecutora = filtro.TipoExecutora.Select(t => Convert.ToInt32(t)).ToList();
                    consulta = from c in consulta
                               where lstTipoExecutora.Contains(c.IdTipoUnidade)
                               select c;
                }

                if (filtro.Usuario.HasValue)
                    consulta = from c in consulta
                               where c.IdUsuarioTipoServico == filtro.Usuario.Value
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

                if (filtro.TipoProtecaoSocial.HasValue)
                    consulta = from c in consulta
                               where c.IdTipoProtecao == filtro.TipoProtecaoSocial.Value
                               select c;

                if (filtro.SituacaoVulnerabilidade.HasValue)
                    consulta = from c in consulta
                               where c.IdSituacaoVulnerabilidade == filtro.SituacaoVulnerabilidade.Value
                               select c;

                if (filtro.SituacaoEspecifica.HasValue)
                    consulta = from c in consulta
                               where c.IdSituacaoEspecifica == filtro.SituacaoEspecifica.Value
                               select c;

                if (filtro.SituacoesVulnerabilidade != null && filtro.SituacoesVulnerabilidade.Count > 0)
                    consulta = from c in consulta
                               where filtro.SituacoesVulnerabilidade.Contains(c.IdSituacaoVulnerabilidade)
                               select c;

                if (filtro.SituacoesEspecificas != null && filtro.SituacoesEspecificas.Count > 0)
                    consulta = from c in consulta
                               where filtro.SituacoesEspecificas.Contains(c.IdSituacaoEspecifica)
                               select c;

                if (filtro.Sexo.HasValue)
                    consulta = from c in consulta
                               where c.IdSexo == filtro.Sexo.Value
                               select c;

                if (filtro.RegiaoMoradia.HasValue)
                    consulta = from c in consulta
                               where c.IdRegiaoMoradia == filtro.RegiaoMoradia.Value
                               select c;

                if (filtro.CaracteristicasTerritorio.HasValue)
                    consulta = from c in consulta
                               where c.IdCaracteristicasTerritorio == filtro.CaracteristicasTerritorio.Value
                               select c;

                //var retorno = consulta.OrderBy(c => c.Municipio).ThenBy(c => c.LocalExecucao).ToList();

                var retorno = consulta.OrderBy(c => c.Municipio).ThenBy(c => c.LocalExecucao).ToList();

                retorno.ForEach(c =>
                {
                    if (c.CaracteristicasTerritorio.Contains("Nenhuma das características citadas"))
                        c.CaracteristicasTerritorio = "Não há uma demanda prioritária";
                });


                var servicos = retorno.Distinct(new GenericEqualityComparer<RedeServicoSocioassistencialDetalhamentoInfo>((x, y) =>
                {
                    return x.IdPrefeitura == y.IdPrefeitura &&
                        x.CodigoUnidade == y.CodigoUnidade &&
                        x.IdTipoUnidade == y.IdTipoUnidade &&
                        x.IdLocal == y.IdLocal &&
                        x.IdTipoProtecao == y.IdTipoProtecao &&
                        x.IdTipoServico == y.IdTipoServico &&
                        x.IdUsuarioTipoServico == y.IdUsuarioTipoServico &&
                        x.IdSexo == y.IdSexo &&
                        x.IdRegiaoMoradia == y.IdRegiaoMoradia &&
                        x.IdCaracteristicasTerritorio == y.IdCaracteristicasTerritorio &&
                        x.ValorFMAS == y.ValorFMAS &&
                        x.ValorFEAS == y.ValorFEAS &&
                        x.ValorFNAS == y.ValorFNAS &&
                        x.ValorFMDCA == y.ValorFMDCA &&
                        x.ValorFEDCA == y.ValorFEDCA &&
                        x.ValorFNDCA == y.ValorFNDCA &&
                        x.ValorPrivado == y.ValorPrivado &&
                        x.NumeroAtendidosMensal == y.NumeroAtendidosMensal &&
                        x.NumeroAtendidosAnual == y.NumeroAtendidosAnual &&
                        x.Abrangencia == y.Abrangencia;
                })).ToList();

                foreach (var c in servicos)
                {
                    var s = retorno.Where(x => x.IdPrefeitura == c.IdPrefeitura &&
                        x.CodigoUnidade == c.CodigoUnidade &&
                        x.IdTipoUnidade == c.IdTipoUnidade &&
                        x.IdLocal == c.IdLocal &&
                        x.IdTipoProtecao == c.IdTipoProtecao &&
                        x.IdTipoServico == c.IdTipoServico &&
                        x.IdUsuarioTipoServico == c.IdUsuarioTipoServico);
                    c.SituacoesEspecificas = s.Select(t => t.SituacaoEspecifica).Distinct().ToList();
                    c.SituacoesVulnerabilidade = s.Select(t => t.SituacaoVulnerabilidade).Distinct().ToList();
                    c.AtividadesSocioassistenciais = s.Select(t => t.AtividadeSocioassistencial).Distinct().ToList();
                }

                return servicos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RedeServicoSocioassistencialInfo> GetServicosIntermunicipais(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<RedeServicoSocioassistencialInfo>>().GetQuery()
                               where (c.IdAbrangencia == 1 || c.IdAbrangencia == 2) && c.IdTipoUnidade != 3 && c.IdTipoUnidade != 5
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

                //if (filtro.Abrangencias != null && filtro.Abrangencias.Count > 0)
                //    consulta = from c in consulta
                //               where filtro.Abrangencias.Contains(c.IdAbrangencia)
                //               select c;

                if (filtro.TipoExecutora != null && filtro.TipoExecutora.Count > 0)
                {
                    var lstTipoExecutora = filtro.TipoExecutora.Select(t => Convert.ToInt32(t)).ToList();
                    consulta = from c in consulta
                               where lstTipoExecutora.Contains(c.IdTipoUnidade)
                               select c;
                }

                if (filtro.Usuario.HasValue)
                    consulta = from c in consulta
                               where c.IdUsuarioTipoServico == filtro.Usuario.Value
                               select c;

                if (filtro.TipoServico.HasValue)
                    consulta = from c in consulta
                               where c.IdTipoServico == filtro.TipoServico.Value
                               select c;

                if (filtro.TipoProtecaoSocial.HasValue)
                    consulta = from c in consulta
                               where c.IdTipoProtecao == filtro.TipoProtecaoSocial.Value
                               select c;

                if (filtro.SituacaoVulnerabilidade.HasValue)
                    consulta = from c in consulta
                               where c.IdSituacaoVulnerabilidade == filtro.SituacaoVulnerabilidade.Value
                               select c;

                if (filtro.SituacaoEspecifica.HasValue)
                    consulta = from c in consulta
                               where c.IdSituacaoEspecifica == filtro.SituacaoEspecifica.Value
                               select c;

                if (filtro.SituacoesVulnerabilidade != null && filtro.SituacoesVulnerabilidade.Count > 0)
                    consulta = from c in consulta
                               where filtro.SituacoesVulnerabilidade.Contains(c.IdSituacaoVulnerabilidade)
                               select c;

                if (filtro.SituacoesEspecificas != null && filtro.SituacoesEspecificas.Count > 0)
                    consulta = from c in consulta
                               where filtro.SituacoesEspecificas.Contains(c.IdSituacaoEspecifica)
                               select c;

                if (filtro.Sexo.HasValue)
                    consulta = from c in consulta
                               where c.IdSexo == filtro.Sexo.Value
                               select c;

                if (filtro.RegiaoMoradia.HasValue)
                    consulta = from c in consulta
                               where c.IdRegiaoMoradia == filtro.RegiaoMoradia.Value
                               select c;

                if (filtro.CaracteristicasTerritorio.HasValue)
                    consulta = from c in consulta
                               where c.IdCaracteristicasTerritorio == filtro.CaracteristicasTerritorio.Value
                               select c;

                // var retorno = consulta.OrderBy(c => c.Municipio).ThenBy(c => c.LocalExecucao).ToList();

                //var retorno = consulta.GroupBy(c => c.Municipio).ToList();


                var retorno = consulta.OrderBy(c => c.Municipio).ThenBy(c => c.LocalExecucao).ToList();

                retorno.ForEach(c =>
                {
                    if (c.CaracteristicasTerritorio.Contains("Nenhuma das características citadas"))
                        c.CaracteristicasTerritorio = "Não há uma demanda prioritária";
                });

                var servicos = retorno.Distinct(new GenericEqualityComparer<RedeServicoSocioassistencialInfo>((x, y) =>
                {
                    return x.IdPrefeitura == y.IdPrefeitura &&
                        x.CodigoUnidade == y.CodigoUnidade &&
                        x.IdTipoUnidade == y.IdTipoUnidade &&
                        x.IdLocal == y.IdLocal &&
                        x.IdTipoProtecao == y.IdTipoProtecao &&
                        x.IdTipoServico == y.IdTipoServico &&
                        x.IdUsuarioTipoServico == y.IdUsuarioTipoServico &&
                        x.IdSexo == y.IdSexo &&
                        x.IdRegiaoMoradia == y.IdRegiaoMoradia &&
                        x.IdCaracteristicasTerritorio == y.IdCaracteristicasTerritorio &&
                        x.ValorFMAS == y.ValorFMAS &&
                        x.ValorFEAS == y.ValorFEAS &&
                        x.ValorFNAS == y.ValorFNAS &&
                        x.ValorFMDCA == y.ValorFMDCA &&
                        x.ValorFEDCA == y.ValorFEDCA &&
                        x.ValorFNDCA == y.ValorFNDCA &&
                        x.ValorPrivado == y.ValorPrivado &&
                        x.NumeroAtendidosAnual == y.NumeroAtendidosAnual &&
                        x.Abrangencia == y.Abrangencia;
                })).ToList();

                foreach (var c in servicos)
                {
                    var s = retorno.Where(x => x.IdPrefeitura == c.IdPrefeitura &&
                        x.CodigoUnidade == c.CodigoUnidade &&
                        x.IdTipoUnidade == c.IdTipoUnidade &&
                        x.IdLocal == c.IdLocal &&
                        x.IdTipoProtecao == c.IdTipoProtecao &&
                        x.IdTipoServico == c.IdTipoServico &&
                        x.IdUsuarioTipoServico == c.IdUsuarioTipoServico);
                    c.SituacoesEspecificas = s.Select(t => t.SituacaoEspecifica).Distinct().ToList();
                    c.SituacoesVulnerabilidade = s.Select(t => t.SituacaoVulnerabilidade).Distinct().ToList();
                    c.AtividadesSocioassistenciais = s.Select(t => t.AtividadeSocioassistencial).Distinct().ToList();

                }

                //consulta = consulta.GroupBy(t => t.IdPrefeitura).Where(t => t.Count() == 2).SelectMany(t => t);

                return servicos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<AtividadeServicoInfo> GetAtividadesServicosSocioassistenciais(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<AtividadeServicoInfo>>().GetQuery()
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

                if (filtro.Usuario.HasValue)
                    consulta = from c in consulta
                               where c.IdUsuarioTipoServico == filtro.Usuario.Value
                               select c;

                if (filtro.TipoServico.HasValue)
                    consulta = from c in consulta
                               where c.IdTipoServico == filtro.TipoServico.Value
                               select c;

                if (filtro.TipoProtecaoSocial.HasValue)
                    consulta = from c in consulta
                               where c.IdTipoProtecao == filtro.TipoProtecaoSocial.Value
                               select c;


                return consulta.OrderBy(c => c.Municipio).ThenBy(c => c.LocalExecucao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<InformacoesBeneficiosEventuaisInfo> GetInformacoesBeneficiosEventuais(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<InformacoesBeneficiosEventuaisInfo>>().GetQuery()
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

                if (filtro.TipoBeneficioEventual.HasValue && filtro.TipoBeneficioEventual.Value != 0)
                    consulta = from c in consulta
                               where c.IdTipoBeneficio == filtro.TipoBeneficioEventual.Value
                               select c;

                if (filtro.Exercicio.HasValue && filtro.Exercicio.Value != 0)
                    consulta = from c in consulta
                               where c.Exercicio == filtro.Exercicio.Value
                               select c;

                return consulta.OrderBy(c => c.Municipio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<AcaoPlanejadaInfo> GetAcoesPlanejadas(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<AcaoPlanejadaInfo>>().GetQuery()
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

        public IQueryable<CronogramaDesembolsoRelatorio22Info> GetCronogramaDesembolso(RelatorioFiltroInfo filtro)
        {

            try
            {

                var consulta = (ContextManager.GetContext() as PMASContext).GetCronogramaDesembolso(Convert.ToInt32(filtro.Exercicio),Convert.ToInt32(filtro.IdPrefeitura));
             

                if (filtro.CronogramasEscolhidos != null && filtro.CronogramasEscolhidos.Count > 0)
                {
                    var lstTipoExecutora = filtro.CronogramasEscolhidos.Select(t => Convert.ToInt32(t)).ToList();

                    consulta = from c in consulta
                               where filtro.CronogramasEscolhidos.Contains(c.IdTipoProtecao)
                               select c;
                }

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

                if (filtro.Portes != null && filtro.Portes.Count > 0)
                    consulta = from c in consulta
                               where filtro.Portes.Contains(c.IdPorte)
                               select c;

                if (filtro.MunIDs != null && filtro.MunIDs.Count > 0)
                    consulta = from c in consulta

                               where filtro.MunIDs.Contains(c.IdMunicipio)
                               select c;

                return consulta.OrderBy(c => c.Municipio);
            }
            catch (Exception)
            {

                throw;
            }




        }

        public List<CronogramaDesembolsoRelatorio22Info> GetTotalCronogramaDesembolso(RelatorioFiltroInfo filtro)
        {
            try
            {

                var retorno = (from a in new RelatorioDescritivo().GetCronogramaDesembolso(filtro).ToList()
                               select new
                               {
                                   IdMunicipio = a.IdMunicipio,
                                   IdPrefeitura = a.IdPrefeitura,
                                   Municipio = a.Municipio,
                                   IdDrads = a.IdDrads,
                                   Drads = a.Drads,
                                   TipoProtecao = a.TipoProtecao,
                                   CusteioPublica = a.CusteioPublica,
                                   CusteioPrivada = a.CusteioPrivada,
                                   InvestimentoAquisicaoEquipamentosPublica = a.InvestimentoAquisicaoDeEquipamentosPublico,
                                   InvestimentoAquisicaoEquipamentosPrivada = a.InvestimentoAquisicaoDeEquipamentosPrivado,
                                   RecursosHumanos = a.RecursosHumanos,
                                   Total = a.Total
                               }).OrderBy(obj => obj.Municipio).ToList();

                var resultadoagrupado = (from p in retorno
                                         group p by new
                                         {
                                             p.Municipio,
                                             p.Drads
                                         } into g
                                         select new CronogramaDesembolsoRelatorio22Info()
                                         {
                                             Municipio = g.Key.Municipio,
                                             Drads = g.Key.Drads,
                                             TipoProtecao = "Todas as proteções",
                                             CusteioPublica = g.Sum(p => p.CusteioPublica),
                                             CusteioPrivada = g.Sum(p => p.CusteioPrivada),
                                             InvestimentoAquisicaoDeEquipamentosPublico = g.Sum(p => p.InvestimentoAquisicaoEquipamentosPublica),
                                             InvestimentoAquisicaoDeEquipamentosPrivado = g.Sum(p => p.InvestimentoAquisicaoEquipamentosPrivada),
                                             RecursosHumanos = g.Sum(p => p.RecursosHumanos),
                                         }).ToList();
                return resultadoagrupado;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<UnidadeGeralInfo> GetUnidades(RelatorioFiltroInfo filtro)
        {
            try
            {
                var consulta = from c in ObjectFactory.GetInstance<IRepository<UnidadeGeralInfo>>().GetQuery()
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

                if (filtro.FormasAtuacoes.Count > 0)
                {
                    consulta = from c in consulta
                               where filtro.FormasAtuacoes.Contains(c.IdFormaAtuacao)
                               select c;

                }

                if (filtro.TipoUnidade.HasValue)
                    consulta = from c in consulta
                               where c.IdTipoRede == filtro.TipoUnidade
                               select c;

                var retorno = consulta.OrderBy(c => c.Municipio).ToList();

                var unidades = retorno.Distinct(new GenericEqualityComparer<UnidadeGeralInfo>((x, y) =>
                {
                    return x.IdUnidade == y.IdUnidade &&
                        x.IdFormaAtuacao == y.IdFormaAtuacao;
                })).ToList();

                foreach (var c in unidades)
                {
                    var s = retorno.Where(x => x.IdUnidade == c.IdUnidade);
                }

                return unidades;


                //return consulta.OrderBy(c => c.Municipio);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
