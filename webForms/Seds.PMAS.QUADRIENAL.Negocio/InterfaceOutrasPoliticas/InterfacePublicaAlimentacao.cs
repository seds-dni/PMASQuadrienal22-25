using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class InterfacePublicaAlimentacao
    {
        private static IRepository<InterfacePublicaAlimentacaoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<InterfacePublicaAlimentacaoInfo>>();
            }
        }

        public InterfacePublicaAlimentacaoInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }

        public InterfacePublicaAlimentacaoInfo GetByPrefeitura(int idPrefeitura)
        {
            return _repository.GetObjectSet().Include("Restaurantes").Include("DistribuicoesAlimentos").Include("FormasDistribuicoesAlimentos").Include("OutrasAcoes").SingleOrDefault(m => m.IdPrefeitura == idPrefeitura);
        }

        public void Add(InterfacePublicaAlimentacaoInfo obj, Boolean commit)
        {
            new ValidadorInterfacePublica().ValidarAlimentacao(obj);
            _repository.Add(obj);

            if (obj.RestaurantePopular != false)
            {
                if (obj.NaoPossuiInformacaoRestaurante == false)
                {
                    var restaurante = new RestaurantePopular();
                    obj.Restaurantes.ForEach(r =>
                    {
                        r.IdInterfacePublicaAlimentacao = obj.Id;
                        restaurante.Add(r, false);
                    });
                }
            }

            if (obj.ExecutaDistribuicaoVivaleite && obj.GestaoVivaleiteOrgaoGestor.HasValue && obj.GestaoVivaleiteOrgaoGestor.Value)
            {
                if (obj.DistribuicoesAlimentos.Count > 0)
                {
                    var formaDistribuicao = new InterfaceFormaDistribuicao();
                    obj.DistribuicoesAlimentos.ForEach(r =>
                    {
                        r.IdInterfacePublicaAlimentacao = obj.Id;
                        formaDistribuicao.Add(r, false);
                    });
                }
            }

            if (commit)
                ContextManager.Commit();
        }

        public void Update(InterfacePublicaAlimentacaoInfo obj, Boolean commit)
        {
            new ValidadorInterfacePublica().ValidarAlimentacao(obj);


            //var propriedadesEntity = _repository.GetModifiedProperties(obj);
            var propriedades = new List<string>();
            //   propriedades = GetLabelForInfo(propriedadesEntity);
            var lstDeletedRestaurantes = new List<InterfacePublicaAlimentacaoRestauranteInfo>();
            var restaurantepopular = new RestaurantePopular();
            var lstRestaurantes = restaurantepopular.GetByInterfaceAlimentacao(obj.Id);
            var hasChangeRestaurantes = false;

            foreach (var p in lstRestaurantes)
            {
                if (!obj.Restaurantes.Any(r => r.Id == p.Id))
                {
                    hasChangeRestaurantes = true;
                    lstDeletedRestaurantes.Add(p);
                }
            }

            foreach (var p in lstDeletedRestaurantes)
                restaurantepopular.Delete(p, true);

            if (obj.Restaurantes != null)
                foreach (var p in obj.Restaurantes)
                {
                    p.IdInterfacePublicaAlimentacao = obj.Id;
                    if (p.Id == 0)
                    {
                        restaurantepopular.Add(p, true);
                        hasChangeRestaurantes = true;
                    }
                    else
                    {
                        restaurantepopular.Update(p, true);
                    }
                    if (hasChangeRestaurantes)
                        propriedades.Add("Restaurantes Populares");
                }


            var lstDeletedUnidades = new List<InterfacePublicaDistribuicaoAlimentoInfo>();
            var unidadeDistribuicao = new InterfaceFormaDistribuicao();
            var lstUnidadesDistribuicao = unidadeDistribuicao.GetByInterfaceAlimentacao(obj.Id);
            var hasChangeUnidadesDistribuicao = false;

            foreach (var u in lstUnidadesDistribuicao)
            {
                if (!obj.DistribuicoesAlimentos.Any(r => r.Id == u.Id))
                {
                    hasChangeUnidadesDistribuicao = true;
                    lstDeletedUnidades.Add(u);
                }
            }


            foreach (var p in lstDeletedUnidades)
                unidadeDistribuicao.Delete(p, true);

            if (obj.DistribuicoesAlimentos != null)
                foreach (var p in obj.DistribuicoesAlimentos)
                {
                    p.IdInterfacePublicaAlimentacao = obj.Id;
                    if (p.Id == 0)
                    {
                        unidadeDistribuicao.Add(p, true);
                    }
                }


            var lstDeletedFormaDistribuicao = new List<InterfacePublicaAlimentacaoFormaDistribuicaoAlimentosInfo>();
            var formaDistribuicao = new FormaDistribuicaoAlimento();
            var lstFormaDistribuicao = formaDistribuicao.GetByInterfaceAlimentacao(obj.Id);
            var hasChangeFormaDistribuicao = false;

            foreach (var p in lstFormaDistribuicao)
            {
                if (!obj.FormasDistribuicoesAlimentos.Any(r => r.Id == p.Id))
                {
                    hasChangeFormaDistribuicao = true;
                    lstDeletedFormaDistribuicao.Add(p);
                }
            }

            foreach (var p in lstDeletedFormaDistribuicao)
                formaDistribuicao.Delete(p, true);

            if (obj.FormasDistribuicoesAlimentos != null)
                foreach (var p in obj.FormasDistribuicoesAlimentos)
                {
                    p.IdInterfacePublicaAlimentacao = obj.Id;
                    if (p.Id == 0)
                    {
                        formaDistribuicao.Add(p, true);
                        hasChangeFormaDistribuicao = true;
                    }
                    if (hasChangeRestaurantes)
                        propriedades.Add("Outra forma de distribuição de alimentos");
                }

            var lstDeletedFormaAcao = new List<InterfacePublicaAlimentacaoOutraAcaoInfo>();
            var formaAcao= new OutraAcao();
            var lstFormaAcao = formaAcao.GetByInterfaceAlimentacao(obj.Id);
            var hasChangeFormaAcao = false;

            foreach (var p in lstFormaAcao)
            {
                if (!obj.OutrasAcoes.Any(r => r.Id == p.Id))
                {
                    hasChangeFormaDistribuicao = true;
                    lstDeletedFormaAcao.Add(p);
                }
            }

            foreach (var p in lstDeletedFormaAcao)
                formaAcao.Delete(p, true);


            if (obj.OutrasAcoes != null)
                foreach (var p in obj.OutrasAcoes)
                {
                    p.IdInterfacePublicaAlimentacao = obj.Id;
                    if (p.Id == 0)
                    {
                        formaAcao.Add(p, true);
                        hasChangeFormaAcao = true;
                    }
                    if (hasChangeRestaurantes)
                        propriedades.Add("Outro tipo de ação");
                }


          


            _repository.Update(obj);

            if (commit)
                ContextManager.Commit();
        }

        private List<string> GetLabelForInfo(List<string> propriedades)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "Restaurantes Populares": labels.Add("Restaurantes Populares cadastrados"); break;

                }
            }
            return labels.Distinct().ToList();
        }
    }
}
