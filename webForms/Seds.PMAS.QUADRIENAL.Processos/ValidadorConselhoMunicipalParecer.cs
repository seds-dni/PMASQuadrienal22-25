using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS2017.Entidades;

namespace Seds.PMAS2017.Negocio.Validadores
{
    public class ValidadorConselhoMunicipalParecer
    {
        public void Validar(ConselhoMunicipalParecerInfo obj)
        {
            var lstMsg = new List<string>();

            if (String.IsNullOrEmpty(obj.ComentarioAvaliandoExecucao))
            {
                lstMsg.Add("O comentário do campo O CMAS acompanhou a execução do PMAS de 2012? é obrigatório");
            }

            if (String.IsNullOrEmpty(obj.ComentarioAcompanhaRepasseRecursoFinanceiro))
            {
                lstMsg.Add("O comentário do campo O CMAS acompanhou o repasse de recursos financeiros para a rede executora? é obrigatório");
            }

            if (String.IsNullOrEmpty(obj.ComentarioAcompanhaPrestacaoConta))
            {
                lstMsg.Add("O comentário do campo O CMAS acompanhou as prestações de contas? é obrigatório");
            }

            if (String.IsNullOrEmpty(obj.ComentarioMonitoraRedeExecutora))
            {
                lstMsg.Add("O comentário do campo O CMAS efetuou acompanhamento da rede executora? é obrigatório");
            }

            if (String.IsNullOrEmpty(obj.ParecerCMAS))
            {
                lstMsg.Add("O comentário do campo Parecer é obrigatório");
            }

            if (!Extensions.ValidaData(obj.Data.ToString()) || obj.Data == DateTime.MinValue || obj.Data == null)
            {
                lstMsg.Add("O campo Data em que foi emitido o parecer sobre o PMAS2013 é obrigatório");
            }

            if (obj.NumeroConselheiros == 0)
            {
                lstMsg.Add("O campo Número de conselheiros com direito a voto é obrigatório");
            }

            if (String.IsNullOrEmpty( obj.PresidenteRepresentanteLegal ))
            {
                lstMsg.Add("O campo Nome do presidente do CMAS ou de seu representante legal é obrigatório");
            }

            if (lstMsg.Count > 0)
                throw new Exception(Extensions.Concat(lstMsg, System.Environment.NewLine));
        }
    }
}
