using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Data;
using Seds.PMAS.Dominio.Entities;

namespace Seds.PMAS.Servicos.Contratos
{
    [ServiceContract]
    public interface IUsuarioPMASService
    {
        [OperationContract]
        UsuarioEntity GetUsuarioLogado();

        [OperationContract]
        List<UsuarioEntity> GetConsultaUsuariosCadastrados(string nome, string rg, int? idDrads, int? idPerfil, int? idMunicipio, string instituicao);

        [OperationContract]
        Int32 AddUsuario(UsuarioEntity u);

        [OperationContract]
        void UpdateUsuario(UsuarioEntity u);

        [OperationContract]
        UsuarioEntity GetUsuarioById(int idUsuario);

        [OperationContract]
        DataTable GetConsulta(string idusuario);

        [OperationContract]
        void RunCadastro(string idusuario);
    }
}
