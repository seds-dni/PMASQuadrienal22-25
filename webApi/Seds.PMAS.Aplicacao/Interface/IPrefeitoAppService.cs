using Seds.PMAS.Dominio.Entities;

namespace Seds.PMAS.Aplicacao.Interface
{
    public interface IPrefeitoAppService : IAppServiceBase<PrefeitoEntity>
    {
        PrefeitoEntity GetByIdPrefeitura(int IdPrefeitura);
    }
}
