using Seds.PMAS.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seds.PMAS.Aplicacao.Interface
{
    public interface IUsuarioAppService : IAppServiceBase<UsuarioEntity>
    {
        UsuarioEntity GetByIdUsuario(int idUsuario);
        UsuarioEntity GetUsuarioByCPF(string cpf);
    }
}
