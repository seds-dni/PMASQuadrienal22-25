using Seds.PMAS.Dominio.Entities;

namespace Seds.PMAS.Aplicacao.Interface
{
    public interface IPrefeituraAppService : IAppServiceBase<PrefeituraEntity>
    {
        PrefeituraEntity GetById(int id);
        void Create(PrefeitoEntity prefeito);
        void Update(PrefeitoEntity prefeito);
    }
}
