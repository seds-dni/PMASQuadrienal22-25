using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Data.Objects;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class CronogramaDesembolso
    {
        private static IRepository<CronogramaDesembolsoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CronogramaDesembolsoInfo>>();
            }
        }

        public IQueryable<CronogramaDesembolsoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public CronogramaDesembolsoInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }

        public IQueryable<CronogramaDesembolsoInfo> GetByPrefeitura(int idPrefeitura)
        {
            return _repository.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura);
        }

        public CronogramaDesembolsoInfo GetByPrefeituraETipoProtecaoSocialETipoUnidade(int idPrefeitura, int idTipoProtecaoSocial, int idTipoUnidade, int exercicio)
        {
            return _repository.GetQuery()
                .Where(m => m.IdPrefeitura == idPrefeitura 
                         && m.IdTipoProtecaoSocial == idTipoProtecaoSocial 
                         && m.IdTipoUnidade == idTipoUnidade
                         && m.Exercicio == exercicio).FirstOrDefault();
        }

        public Boolean ValidarCronogramaDesembolso(int idPrefeitura, int idTipoProtecaoSocial, int idTipoUnidade, int exercicio)
        {
            var resultado = true;
            var c = GetByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, idTipoProtecaoSocial, idTipoUnidade, exercicio);
            if (c != null)
            {
                var totalDisponibilizados = c.ValorServicosTerceirosMes1 
                                            + c.ValorServicosTerceirosMes2 
                                            + c.ValorServicosTerceirosMes3 
                                            + c.ValorServicosTerceirosMes4 
                                            + c.ValorServicosTerceirosMes5 
                                            + c.ValorServicosTerceirosMes6 
                                            + c.ValorServicosTerceirosMes7 
                                            + c.ValorServicosTerceirosMes8 
                                            + c.ValorServicosTerceirosMes9 
                                            + c.ValorServicosTerceirosMes10 
                                            + c.ValorServicosTerceirosMes11 
                                            + c.ValorServicosTerceirosMes12;

                var totalRedePublica = c.ValorMaterialConsumoMes1
                                     +  c.ValorMaterialConsumoMes2
                                     +  c.ValorMaterialConsumoMes3
                                     +  c.ValorMaterialConsumoMes4
                                     +  c.ValorMaterialConsumoMes5
                                     +  c.ValorMaterialConsumoMes6
                                     +  c.ValorMaterialConsumoMes7
                                     +  c.ValorMaterialConsumoMes8
                                     +  c.ValorMaterialConsumoMes9
                                     +  c.ValorMaterialConsumoMes10
                                     +  c.ValorMaterialConsumoMes11
                                     +  c.ValorMaterialConsumoMes12
               
                                     +  c.ValorRHMes1
                                     +  c.ValorRHMes2
                                     +  c.ValorRHMes3
                                     +  c.ValorRHMes4
                                     +  c.ValorRHMes5
                                     +  c.ValorRHMes6
                                     +  c.ValorRHMes7
                                     +  c.ValorRHMes8
                                     +  c.ValorRHMes9
                                     +  c.ValorRHMes10
                                     +  c.ValorRHMes11
                                     +  c.ValorRHMes12
               
                                     + (c.ValorInvestimentoMes1.HasValue ? c.ValorInvestimentoMes1.Value : 0M)
                                     + (c.ValorInvestimentoMes2.HasValue ? c.ValorInvestimentoMes2.Value : 0M)
                                     + (c.ValorInvestimentoMes3.HasValue ? c.ValorInvestimentoMes3.Value : 0M)
                                     + (c.ValorInvestimentoMes4.HasValue ? c.ValorInvestimentoMes4.Value : 0M)
                                     + (c.ValorInvestimentoMes5.HasValue ? c.ValorInvestimentoMes5.Value : 0M)
                                     + (c.ValorInvestimentoMes6.HasValue ? c.ValorInvestimentoMes6.Value : 0M)
                                     + (c.ValorInvestimentoMes7.HasValue ? c.ValorInvestimentoMes7.Value : 0M)
                                     + (c.ValorInvestimentoMes8.HasValue ? c.ValorInvestimentoMes8.Value : 0M)
                                     + (c.ValorInvestimentoMes9.HasValue ? c.ValorInvestimentoMes9.Value : 0M)
                                     + (c.ValorInvestimentoMes10.HasValue ? c.ValorInvestimentoMes10.Value : 0M)
                                     + (c.ValorInvestimentoMes11.HasValue ? c.ValorInvestimentoMes11.Value : 0M)
                                     + (c.ValorInvestimentoMes12.HasValue ? c.ValorInvestimentoMes12.Value : 0M)

                                     + (c.ObrasMes1.HasValue ? c.ObrasMes1.Value : 0M)
                                     + (c.ObrasMes2.HasValue ? c.ObrasMes2.Value : 0M)
                                     + (c.ObrasMes3.HasValue ? c.ObrasMes3.Value : 0M)
                                     + (c.ObrasMes4.HasValue ? c.ObrasMes4.Value : 0M)
                                     + (c.ObrasMes5.HasValue ? c.ObrasMes5.Value : 0M)
                                     + (c.ObrasMes6.HasValue ? c.ObrasMes6.Value : 0M)
                                     + (c.ObrasMes7.HasValue ? c.ObrasMes7.Value : 0M)
                                     + (c.ObrasMes8.HasValue ? c.ObrasMes8.Value : 0M)
                                     + (c.ObrasMes9.HasValue ? c.ObrasMes9.Value : 0M)
                                     + (c.ObrasMes10.HasValue ? c.ObrasMes10.Value : 0M)
                                     + (c.ObrasMes11.HasValue ? c.ObrasMes11.Value : 0M)
                                     + (c.ObrasMes12.HasValue ? c.ObrasMes12.Value : 0M)

                                     +  (c.ValorOutrasDespesasCusteioMes01.HasValue ? c.ValorOutrasDespesasCusteioMes01.Value : 0M)
                                     +  (c.ValorOutrasDespesasCusteioMes02.HasValue ? c.ValorOutrasDespesasCusteioMes02.Value : 0M)
                                     +  (c.ValorOutrasDespesasCusteioMes03.HasValue ? c.ValorOutrasDespesasCusteioMes03.Value : 0M)
                                     +  (c.ValorOutrasDespesasCusteioMes04.HasValue ? c.ValorOutrasDespesasCusteioMes04.Value : 0M)
                 
                                     +  (c.ValorOutrasDespesasCusteioMes05.HasValue ? c.ValorOutrasDespesasCusteioMes05.Value : 0M)
                                     +  (c.ValorOutrasDespesasCusteioMes06.HasValue ? c.ValorOutrasDespesasCusteioMes06.Value : 0M)
                                     +  (c.ValorOutrasDespesasCusteioMes07.HasValue ? c.ValorOutrasDespesasCusteioMes07.Value : 0M)
                                     +  (c.ValorOutrasDespesasCusteioMes08.HasValue ? c.ValorOutrasDespesasCusteioMes08.Value : 0M)
                
                                     +  (c.ValorOutrasDespesasCusteioMes09.HasValue ? c.ValorOutrasDespesasCusteioMes09.Value : 0M)
                                     +  (c.ValorOutrasDespesasCusteioMes10.HasValue ? c.ValorOutrasDespesasCusteioMes10.Value : 0M)
                                     +  (c.ValorOutrasDespesasCusteioMes11.HasValue ? c.ValorOutrasDespesasCusteioMes11.Value : 0M)
                                     +  (c.ValorOutrasDespesasCusteioMes12.HasValue ? c.ValorOutrasDespesasCusteioMes12.Value : 0M);
                if (totalDisponibilizados != totalRedePublica)
                {
                    resultado = false;
                }
            }
            return resultado;
        }

        public Decimal GetValorOutrasDespesasReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(int idPrefeitura, int idTipoProtecaoSocial, int idTipoUnidade, int exercicio)
        {
            var c = GetByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, idTipoProtecaoSocial, idTipoUnidade, exercicio);
            if (c == null)
            {
                return 0m;
            }
            return Convert.ToDecimal(c.OutrasDespesasReprogramados);

        }


        public Decimal GetValorAquisicaoEquipamentosReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(int idPrefeitura, int idTipoProtecaoSocial, int idTipoUnidade, int exercicio)
        {
            var c = GetByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, idTipoProtecaoSocial, idTipoUnidade, exercicio);
            if (c == null)
            {
                return 0m;
            }
            return Convert.ToDecimal(c.ReprogramacaoEquipamentosInvestimento);

        }


        public Decimal GetValorObrasReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(int idPrefeitura, int idTipoProtecaoSocial, int idTipoUnidade, int exercicio)
        {
            var c = GetByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, idTipoProtecaoSocial, idTipoUnidade, exercicio);
            if (c == null)
            {
                return 0m;
            }
            return Convert.ToDecimal(c.ReprogramacaoObras);

        }


        public Decimal GetValorDisponibilizadosReprogramadosDemandasParlamentaresByPrefeituraETipoProtecaoSocialETipoUnidade(int idPrefeitura, int idTipoProtecaoSocial, int idTipoUnidade, int exercicio)
        {
            var c = GetByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, idTipoProtecaoSocial, idTipoUnidade, exercicio);
            if (c == null)
            {
                return 0m;
            }
            return Convert.ToDecimal(c.ReprogramacaoDemandasParlamentaresDisponibilizados);

        }


        public Decimal GetValorDemandasDisponibilizadosReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(int idPrefeitura, int idTipoProtecaoSocial, int idTipoUnidade, int exercicio)
        {
            var c = GetByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, idTipoProtecaoSocial, idTipoUnidade, exercicio);
            if (c == null)
            {
                return 0m;
            }
            return Convert.ToDecimal(c.ReprogramacaoDemandasParlamentaresDisponibilizados);

        }


        public Decimal GetValorRecursosHumanosReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(int idPrefeitura, int idTipoProtecaoSocial, int idTipoUnidade, int exercicio)
        {
            var c = GetByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, idTipoProtecaoSocial, idTipoUnidade, exercicio);
            if (c == null)
            {
                return 0m;
            }
            return Convert.ToDecimal(c.RecursosHumanosReprogramados);

        }

        public Decimal GetValorReprogramacaoRecursosDisponibilizadosByPrefeituraETipoProtecaoSocialETipoUnidade(int idPrefeitura, int idTipoProtecaoSocial, int idTipoUnidade, int exercicio)
        {
            var c = GetByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, idTipoProtecaoSocial, idTipoUnidade, exercicio);
            if (c == null)
            {
                return 0m;
            }
            return Convert.ToDecimal(c.ReprogramacaoRecursosDisponibilizados);

        }

        public Decimal GetValorByPrefeituraETipoProtecaoSocialETipoUnidade(int idPrefeitura, int idTipoProtecaoSocial, int idTipoUnidade, int exercicio)
        {
            var c = GetByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, idTipoProtecaoSocial, idTipoUnidade, exercicio);
            if (c == null)
            {
                return 0m;
            }

            return c.ValorMaterialConsumoMes1
                    +c.ValorMaterialConsumoMes2
                    +c.ValorMaterialConsumoMes3
                    +c.ValorMaterialConsumoMes4
                    +c.ValorMaterialConsumoMes5
                    +c.ValorMaterialConsumoMes6
                    +c.ValorMaterialConsumoMes7
                    +c.ValorMaterialConsumoMes8
                    +c.ValorMaterialConsumoMes9
                    +c.ValorMaterialConsumoMes10
                    +c.ValorMaterialConsumoMes11
                    +c.ValorMaterialConsumoMes12
              

                    +c.ValorRHMes1
                    +c.ValorRHMes2
                    +c.ValorRHMes3
                    +c.ValorRHMes4
                    +c.ValorRHMes5
                    +c.ValorRHMes6
                    +c.ValorRHMes7
                    +c.ValorRHMes8
                    +c.ValorRHMes9
                    +c.ValorRHMes10
                    +c.ValorRHMes11
                    +c.ValorRHMes12
              

                    +(c.ValorOutrasDespesasCusteioMes01.HasValue ? c.ValorOutrasDespesasCusteioMes01.Value : 0M)
                    +(c.ValorOutrasDespesasCusteioMes02.HasValue ? c.ValorOutrasDespesasCusteioMes02.Value : 0M)
                    +(c.ValorOutrasDespesasCusteioMes03.HasValue ? c.ValorOutrasDespesasCusteioMes03.Value : 0M)
                    +(c.ValorOutrasDespesasCusteioMes04.HasValue ? c.ValorOutrasDespesasCusteioMes04.Value : 0M)
                
                    +(c.ValorOutrasDespesasCusteioMes05.HasValue ? c.ValorOutrasDespesasCusteioMes05.Value : 0M)
                    +(c.ValorOutrasDespesasCusteioMes06.HasValue ? c.ValorOutrasDespesasCusteioMes06.Value : 0M)
                    +(c.ValorOutrasDespesasCusteioMes07.HasValue ? c.ValorOutrasDespesasCusteioMes07.Value : 0M)
                    +(c.ValorOutrasDespesasCusteioMes08.HasValue ? c.ValorOutrasDespesasCusteioMes08.Value : 0M)
               
                    +(c.ValorOutrasDespesasCusteioMes09.HasValue ? c.ValorOutrasDespesasCusteioMes09.Value : 0M)
                    +(c.ValorOutrasDespesasCusteioMes10.HasValue ? c.ValorOutrasDespesasCusteioMes10.Value : 0M)
                    +(c.ValorOutrasDespesasCusteioMes11.HasValue ? c.ValorOutrasDespesasCusteioMes11.Value : 0M)
                    +(c.ValorOutrasDespesasCusteioMes12.HasValue ? c.ValorOutrasDespesasCusteioMes12.Value : 0M)
              
                    +(c.ValorInvestimentoMes1.HasValue ? c.ValorInvestimentoMes1.Value : 0M)
                    +(c.ValorInvestimentoMes2.HasValue ? c.ValorInvestimentoMes2.Value : 0M)
                    +(c.ValorInvestimentoMes3.HasValue ? c.ValorInvestimentoMes3.Value : 0M)
                    +(c.ValorInvestimentoMes4.HasValue ? c.ValorInvestimentoMes4.Value : 0M)
              
                    +(c.ValorInvestimentoMes5.HasValue ? c.ValorInvestimentoMes5.Value : 0M)
                    +(c.ValorInvestimentoMes6.HasValue ? c.ValorInvestimentoMes6.Value : 0M)
                    +(c.ValorInvestimentoMes7.HasValue ? c.ValorInvestimentoMes7.Value : 0M)
                    +(c.ValorInvestimentoMes8.HasValue ? c.ValorInvestimentoMes8.Value : 0M)

                    +(c.ValorInvestimentoMes9.HasValue ? c.ValorInvestimentoMes9.Value : 0M)
                    +(c.ValorInvestimentoMes10.HasValue ? c.ValorInvestimentoMes10.Value : 0M)
                    +(c.ValorInvestimentoMes11.HasValue ? c.ValorInvestimentoMes11.Value : 0M)
                    +(c.ValorInvestimentoMes12.HasValue ? c.ValorInvestimentoMes12.Value : 0M)
          

                    +(c.ObrasMes1.HasValue ? c.ObrasMes1.Value : 0M)
                    +(c.ObrasMes2.HasValue ? c.ObrasMes2.Value : 0M)
                    +(c.ObrasMes3.HasValue ? c.ObrasMes3.Value : 0M)
                    +(c.ObrasMes4.HasValue ? c.ObrasMes4.Value : 0M)
                    +(c.ObrasMes5.HasValue ? c.ObrasMes5.Value : 0M)
                    +(c.ObrasMes6.HasValue ? c.ObrasMes6.Value : 0M)
                    +(c.ObrasMes7.HasValue ? c.ObrasMes7.Value : 0M)
                    +(c.ObrasMes8.HasValue ? c.ObrasMes8.Value : 0M)
               

                    +(c.ObrasMes9.HasValue ? c.ObrasMes9.Value : 0M)
                    +(c.ObrasMes10.HasValue ? c.ObrasMes10.Value : 0M)
                    +(c.ObrasMes11.HasValue ? c.ObrasMes11.Value : 0M)
                    +(c.ObrasMes12.HasValue ? c.ObrasMes12.Value : 0M)
               

                    +(c.RecursosHumanosDemandasParlamentares.HasValue ?  c.RecursosHumanosDemandasParlamentares.Value: 0M)
                    +(c.OutrasDespesasDemandasParlamentares.HasValue ? c.OutrasDespesasDemandasParlamentares.Value:0M)
                    +(c.DemandasParlamentaresObras.HasValue ? c.DemandasParlamentaresObras.Value : 0M)
                    +(c.DemandasParlamentaresEquipamentosInvestimento.HasValue ? c.DemandasParlamentaresEquipamentosInvestimento.Value : 0M)

                    +(c.RecursosHumanosReprogramacaoDemandasParlamentares.HasValue ? c.RecursosHumanosReprogramacaoDemandasParlamentares.Value : 0M)
                    +(c.OutrasDespesasReprogramacaoDemandasParlamentares.HasValue ? c.OutrasDespesasReprogramacaoDemandasParlamentares.Value : 0M)
                    +(c.ReprogramacaoDemandasParlamentaresObras.HasValue ? c.ReprogramacaoDemandasParlamentaresObras.Value : 0M)
                    +(c.ReprogramacaoDemandasParlamentaresEquipamentosInvestimento.HasValue ? c.ReprogramacaoDemandasParlamentaresEquipamentosInvestimento.Value : 0M)

                    +(c.RecursosHumanosReprogramados.HasValue ? c.RecursosHumanosReprogramados.Value : 0M)
                    +(c.OutrasDespesasReprogramados.HasValue ? c.OutrasDespesasReprogramados.Value : 0M)
                    +(c.ReprogramacaoObras.HasValue ? c.ReprogramacaoObras.Value : 0M)
                    +(c.ReprogramacaoEquipamentosInvestimento.HasValue ? c.ReprogramacaoEquipamentosInvestimento.Value : 0M);

                    

        }

        //Welington P.
        /// <summary>
        /// Gravar o Cronograma de Desembolso para Programa e Beneficios
        /// </summary>
        /// <param name="cronograma"></param>
        /// <param name="commit"></param>
        public void SaveCronogramaDesembolsoProgramaBeneficios(CronogramaDesembolsoInfo cronograma, Boolean commit)
        {
            if (!_repository.GetAll().Any(p => p.IdPrefeitura == cronograma.IdPrefeitura && p.IdTipoProtecaoSocial == cronograma.IdTipoProtecaoSocial && p.Id == cronograma.Id))
                _repository.Add(cronograma);
            if (commit)
                ContextManager.Commit();
        }

        public void Update(CronogramaDesembolsoInfo obj, Boolean commit)
        {
            //DBM:Adicionar Validação por tipo de protecao
            //DBM:Adicionar Validação por tipo de protecao
            //DBM:Adicionar Validação por tipo de protecao
            //DBM:Adicionar Validação por tipo de protecao
            //DBM:Adicionar Validação por tipo de protecao
            //ValidarCronogramaDesembolso(obj.IdPrefeitura, obj.IdTipoProtecaoSocial, obj.IdTipoUnidade, obj.Exercicio);

            _repository.Update(obj);

            var propriedadesEntity = _repository.GetModifiedProperties(obj);
            var propriedades = GetLabelForInfo(propriedadesEntity);

            if (propriedades.Count > 0)
            {
                var p = new TipoProtecaoSocial().GetById(obj.IdTipoProtecaoSocial);
                String descricao = "Cronograma de Desembolso (" + p.Nome + ") - " + (obj.IdTipoUnidade == 1 ? "Rede Pública" : "Rede Privada") + ": " + System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
                var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Update, GetQuadro((ETipoProtecao)obj.IdTipoProtecaoSocial), descricao);
                if (log != null)
                    new Log().Add(log, false);
            }

            if (commit)
                ContextManager.Commit();
        }

        public List<String> GetLabelForInfo(List<String> propriedades)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "ValorRHMes1": labels.Add("valor de recursos humanos (Mês 1)"); break;
                    case "ValorRHMes2": labels.Add("valor de recursos humanos (Mês 2)"); break;
                    case "ValorRHMes3": labels.Add("valor de recursos humanos (Mês 3)"); break;
                    case "ValorRHMes4": labels.Add("valor de recursos humanos (Mês 4)"); break;
                    case "ValorRHMes5": labels.Add("valor de recursos humanos (Mês 5)"); break;
                    case "ValorRHMes6": labels.Add("valor de recursos humanos (Mês 6)"); break;
                    case "ValorRHMes7": labels.Add("valor de recursos humanos (Mês 7)"); break;
                    case "ValorRHMes8": labels.Add("valor de recursos humanos (Mês 8)"); break;
                    case "ValorRHMes9": labels.Add("valor de recursos humanos (Mês 9)"); break;
                    case "ValorRHMes10": labels.Add("valor de recursos humanos (Mês 10)"); break;
                    case "ValorRHMes11": labels.Add("valor de recursos humanos (Mês 11)"); break;
                    case "ValorRHMes12": labels.Add("valor de recursos humanos (Mês 12)"); break;
                    case "ValorMaterialConsumoMes1": labels.Add("Despesas de custeio (Mês 1)"); break;
                    case "ValorMaterialConsumoMes2": labels.Add("Despesas de custeio (Mês 2)"); break;
                    case "ValorMaterialConsumoMes3": labels.Add("Despesas de custeio (Mês 3)"); break;
                    case "ValorMaterialConsumoMes4": labels.Add("Despesas de custeio (Mês 4)"); break;
                    case "ValorMaterialConsumoMes5": labels.Add("Despesas de custeio (Mês 5)"); break;
                    case "ValorMaterialConsumoMes6": labels.Add("Despesas de custeio (Mês 6)"); break;
                    case "ValorMaterialConsumoMes7": labels.Add("Despesas de custeio (Mês 7)"); break;
                    case "ValorMaterialConsumoMes8": labels.Add("Despesas de custeio (Mês 8)"); break;
                    case "ValorMaterialConsumoMes9": labels.Add("Despesas de custeio (Mês 9)"); break;
                    case "ValorMaterialConsumoMes10": labels.Add("Despesas de custeio (Mês 10)"); break;
                    case "ValorMaterialConsumoMes11": labels.Add("Despesas de custeio (Mês 11)"); break;
                    case "ValorMaterialConsumoMes12": labels.Add("Despesas de custeio (Mês 12)"); break;
                    case "ValorServicosTerceirosMes1": labels.Add("Outras despesas de custeio (Mês 1)"); break;
                    case "ValorServicosTerceirosMes2": labels.Add("Outras despesas de custeio (Mês 2)"); break;
                    case "ValorServicosTerceirosMes3": labels.Add("Outras despesas de custeio (Mês 3)"); break;
                    case "ValorServicosTerceirosMes4": labels.Add("Outras despesas de custeio (Mês 4)"); break;
                    case "ValorServicosTerceirosMes5": labels.Add("Outras despesas de custeio (Mês 5)"); break;
                    case "ValorServicosTerceirosMes6": labels.Add("Outras despesas de custeio (Mês 6)"); break;
                    case "ValorServicosTerceirosMes7": labels.Add("Outras despesas de custeio (Mês 7)"); break;
                    case "ValorServicosTerceirosMes8": labels.Add("Outras despesas de custeio (Mês 8)"); break;
                    case "ValorServicosTerceirosMes9": labels.Add("Outras despesas de custeio (Mês 9)"); break;
                    case "ValorServicosTerceirosMes10": labels.Add("Outras despesas de custeio (Mês 10)"); break;
                    case "ValorServicosTerceirosMes11": labels.Add("Outras despesas de custeio (Mês 11)"); break;
                    case "ValorServicosTerceirosMes12": labels.Add("Outras despesas de custeio (Mês 12)"); break;
                }
            }
            return labels.Distinct().ToList();
        }

        private Int32 GetQuadro(ETipoProtecao protecao)
        {
            switch (protecao)
            {
                case ETipoProtecao.Basica: return 62;
                case ETipoProtecao.EspecialMediaComplexidade: return 64;
                case ETipoProtecao.EspecialAltaComplexidade: return 66;
                case ETipoProtecao.ProgramasEProjetos: return 88;
                case ETipoProtecao.BeneficiosEventuais: return 90;
                //case ETipoProtecao.Basica: return 5;
                //case ETipoProtecao.EspecialMediaComplexidade: return 7;
                //case ETipoProtecao.EspecialAltaComplexidade: return 9;
                //case ETipoProtecao.ProgramasEProjetos: return 11;
                //case ETipoProtecao.BeneficiosEventuais: return 13;
            }
            return 0;
        }
    }
}
