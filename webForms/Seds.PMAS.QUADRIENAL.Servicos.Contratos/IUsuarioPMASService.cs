using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using System.Data;

namespace Seds.PMAS.QUADRIENAL.Servicos.Contratos
{
    [ServiceContract]
    public interface IUsuarioPMASService
    {
        [OperationContract]
        UsuarioPMASInfo GetUsuarioLogado();

        [OperationContract]
        List<ConsultaUsuariosInfo> GetConsultaUsuariosCadastrados(string nome, string rg, int? idDrads, int? idPerfil, int? idMunicipio, string instituicao);

        [OperationContract]
        Int32 AddUsuario(UsuarioPMASInfo u);

        [OperationContract]
        void UpdateUsuario(UsuarioPMASInfo u);

        [OperationContract]
        UsuarioPMASInfo GetUsuarioById(int idUsuario);

        [OperationContract]
        DataTable GetConsulta(string idusuario);

        [OperationContract]
        void RunCadastro(string idusuario);
    }
}
