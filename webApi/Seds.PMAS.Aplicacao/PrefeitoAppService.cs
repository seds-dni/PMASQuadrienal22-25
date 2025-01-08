using Seds.PMAS.Aplicacao.Interface;
using Seds.PMAS.Dominio.Entities;
using Seds.PMAS.Dominio.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seds.PMAS.Aplicacao
{
    public class PrefeitoAppService : IPrefeitoAppService
    {
        private readonly IPrefeitoService _prefeitoService;

        public PrefeitoAppService(IPrefeitoService prefeitoService)
        //: base(prefeitoService)
        {
            _prefeitoService = prefeitoService;
        }

        public void Add(PrefeitoEntity obj)
        {
            _prefeitoService.Create(obj);
        }

        public void Dispose()
        {
            _prefeitoService.Dispose();
        }

        public IEnumerable<PrefeitoEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public PrefeitoEntity GetById(int id)
        {
            return _prefeitoService.GetById(id);
        }

        public PrefeitoEntity GetByIdPrefeitura(int IdPrefeitura)
        {
            return _prefeitoService.GetByIdPrefeitura(IdPrefeitura);
        }

        public void Remove(PrefeitoEntity obj)
        {
            throw new NotImplementedException();
        }

        public void Update(PrefeitoEntity obj)
        {
            _prefeitoService.Update(obj);
        }
    }
}
