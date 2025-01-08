using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class PrefeituraExercicioBloqueio
    {
        public readonly int GESTOR_REPROGRAMACAO_REDE_DIRETA = 1019;
        public readonly int GESTOR_REPROGRAMACAO_REDE_INDIRETA = 1020;
        public readonly int DRADS_REPROGRAMACAO = 1040;
        public readonly int GESTOR_PARLAMENTARES_REDE_DIRETA = 1041;
        public readonly int GESTOR_PARLAMENTARES_REDE_INDIRETA = 1042;
        public readonly int DRADS_PARLAMENTARES = 1043;


        private static IRepository<PrefeituraExercicioBloqueioInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PrefeituraExercicioBloqueioInfo>>();
            }
        }

        public IQueryable<PrefeituraExercicioBloqueioInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public PrefeituraExercicioBloqueioInfo GetById(int id)
        {
            return _repository.Single(m => m.IdPrefeitura == id);
        }

        public void UpdateDesbloqueiaTodosMenosReprogramados(Boolean? desbloqueado, int exercicio)
        {
            _repository.GetQuery().Where(x => 
                   x.Exercicio == exercicio 
                && x.IdRefBloqueio != this.GESTOR_REPROGRAMACAO_REDE_DIRETA 
                && x.IdRefBloqueio != this.GESTOR_REPROGRAMACAO_REDE_INDIRETA
                && x.IdRefBloqueio != this.DRADS_REPROGRAMACAO
                && x.IdRefBloqueio != this.GESTOR_PARLAMENTARES_REDE_DIRETA
                && x.IdRefBloqueio != this.GESTOR_PARLAMENTARES_REDE_INDIRETA
                && x.IdRefBloqueio != this.DRADS_PARLAMENTARES
                ).ToList()
            .ForEach(x => x.Desbloqueado = desbloqueado);
            _repository.SaveChanges();

        }

        public void UpdateDesbloqueiaReprogramados(Boolean? desbloqueado, int exercicio)
        {
            _repository.GetQuery().Where(x =>
                   x.Exercicio == exercicio
                && (x.IdRefBloqueio == this.GESTOR_REPROGRAMACAO_REDE_DIRETA
                || x.IdRefBloqueio == this.GESTOR_REPROGRAMACAO_REDE_INDIRETA
                || x.IdRefBloqueio == this.DRADS_REPROGRAMACAO)
                ).ToList()
            .ForEach(x => x.Desbloqueado = desbloqueado);
            _repository.SaveChanges();

        }

        public void UpdateDesbloqueiaDemandasParlamentares(Boolean? desbloqueado, int exercicio)
        {
            _repository.GetQuery().Where(x =>
                   x.Exercicio == exercicio
                && (x.IdRefBloqueio == this.GESTOR_PARLAMENTARES_REDE_DIRETA
                || x.IdRefBloqueio == this.GESTOR_PARLAMENTARES_REDE_INDIRETA
                || x.IdRefBloqueio == this.DRADS_PARLAMENTARES)
                ).ToList()
            .ForEach(x => x.Desbloqueado = desbloqueado);
            _repository.SaveChanges();

        }
    }
}
