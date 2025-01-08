using Seds.PMAS.QUADRIENAL.Entidades.Acoes;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio.Acoes
{
    public class RecursosPrefeituraAcaoPlanejamento
    {
        private static IRepository<RecursosPrefeituraAcaoPlanejamentoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<RecursosPrefeituraAcaoPlanejamentoInfo>>();
            }
        }


        public IQueryable<RecursosPrefeituraAcaoPlanejamentoInfo> GetById(int idAcaoPlanejamento,int idPrefeitura)
        {
            var p = _repository.GetQuery().Where(s => s.IdPrefeituraAcaoPlanejamento == idAcaoPlanejamento && s.IdPrefeitura == idPrefeitura);
            return p;
        }


        public void SaveRecursosPrefeituraAcaoPlanejamentoInfo(RecursosPrefeituraAcaoPlanejamentoInfo r, Boolean commit)
        {
            if (!_repository.GetQuery().Any(s => s.IdPrefeituraAcaoPlanejamento == r.IdPrefeituraAcaoPlanejamento && s.IdPrefeitura == r.IdPrefeitura && s.Exercicio == r.Exercicio))
            {
                _repository.Add(
                     new RecursosPrefeituraAcaoPlanejamentoInfo
                     {
                         IdPrefeitura = r.IdPrefeitura,
                         IdPrefeituraAcaoPlanejamento = r.IdPrefeituraAcaoPlanejamento,
                         FonteFMAS = r.FonteFMAS,
                         ValorFMAS = r.ValorFMAS,
                         FonteOrcamentoMunicipal = r.FonteOrcamentoMunicipal,
                         ValorOrcamentoMunicipal = r.ValorOrcamentoMunicipal,
                         FonteOutrosFundosMunicipais = r.FonteOutrosFundosMunicipais,
                         ValorOutrosFundosMunicipais = r.ValorOutrosFundosMunicipais,
                         FonteFEAS = r.FonteFEAS,
                         ValorFEAS = r.ValorFEAS,
                         FonteOrcamentoEstadual = r.FonteOrcamentoEstadual,
                         ValorOrcamentoEstadual = r.ValorOrcamentoEstadual,
                         FonteOutrosFundosEstaduais = r.FonteOutrosFundosEstaduais,
                         ValorOutrosFundosEstaduais = r.ValorOutrosFundosEstaduais,
                         FonteFNAS = r.FonteFNAS,
                         ValorFNAS = r.ValorFNAS,
                         FonteOrcamentoFederal = r.FonteOrcamentoFederal,
                         ValorOrcamentoFederal = r.ValorOrcamentoFederal,
                         FonteOutrosFundosFederais = r.FonteOutrosFundosFederais,
                         ValorOutrosFundosFederais = r.ValorOutrosFundosFederais,
                         FonteIGDPBF = r.FonteIGDPBF,
                         ValorIGDPBF = r.ValorIGDPBF,
                         FonteIGDSUAS = r.FonteIGDSUAS,
                         ValorIGDSUAS = r.ValorIGDSUAS,
                         Exercicio = r.Exercicio,

                     });
            }
            else
            {
                _repository.Update(r);
            }

            if (commit)
                ContextManager.Commit();
        }
    }
}
