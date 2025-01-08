using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;

namespace Seds.PMAS.QUADRIENAL.Servicos.Contratos
{
    [ServiceContract]
    public interface IDradsPlanoMunicipalService
    {
        [OperationContract]
        DradsPlanoMunicipalRecursosInfo GetResumoCofinanciamentoDradsBy(Int32 idPrefeitura, Int32 exercicio);

        [OperationContract]
        DradsPlanoMunicipalRecursosReprogramadoInfo GetResumoCofinanciamentoReprogramadoDradsBy(int idPrefeitura, int exercicio);

        [OperationContract]
        DradsPlanoMunicipalBeneficioProgramaRecursosInfo GetResumoCofinanciamentoBeneficioProgramaDradsBy(Int32 idPrefeitura, Int32 exercicio);
    }
}
