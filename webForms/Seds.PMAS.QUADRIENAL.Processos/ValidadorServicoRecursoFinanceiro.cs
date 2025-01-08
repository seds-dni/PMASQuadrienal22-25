using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS2017.Entidades;

namespace Seds.PMAS2017.Negocio.Validadores
{
    public class ValidadorServicoRecursoFinanceiro
    {       
        public void Validar(ServicoRecursoFinanceiroInfo obj)
        {

            var lstMsg = new List<string>();

            if (obj.UsuarioTipoServico != null && obj.UsuarioTipoServico.TipoServico != null && obj.UsuarioTipoServico.TipoServico.IdTipoProtecaoSocial == 0)
            {
                lstMsg.Add("O campo Selecione o tipo de proteção social é obrigatório!");
            }


            if (obj.UsuarioTipoServico != null && obj.UsuarioTipoServico.IdTipoServico == 0)
            {
                lstMsg.Add("O campo Tipo de serviço é obrigatório!");
            }

            if (obj.IdUsuarioTipoServico == 0)
            {
                lstMsg.Add("O campo Usuários é obrigatório!");
            }

            if (obj.SituacoesEspecificas.Count == 0)
            {
                lstMsg.Add("O campo Situações específicas atendidas por este serviço é obrigatório!");
            }

            if (obj.IdAbrangenciaServico == 0)
            {
                lstMsg.Add("O Campo Abrangência do Serviço é obrigatório!");
            }

            if (obj.PrevisaoMensalNumeroAtendidos == 0)
            {
                lstMsg.Add("O campo Previsão mensal do número de " + (obj.UsuarioTipoServico != null && obj.UsuarioTipoServico.IdTipoServico == 135 ? "famílias" : "pessoas") + " atendidas é obrigatório!");
            }

            if (obj.PrevisaoAnualNumeroAtendidos == 0)
            {
                lstMsg.Add("O campo Previsão anual do número de " + (obj.UsuarioTipoServico != null && obj.UsuarioTipoServico.IdTipoServico == 135 ? "famílias" : "pessoas")+ " atendidas é obrigatório!");
            }            

            if (obj.TotalFuncionarios == 0)
            {
                lstMsg.Add("O campo Total de profissionais que atuam neste serviço é obrigatório!");
            }

            if (obj.AtividadesSocioAssistenciais == null || obj.AtividadesSocioAssistenciais.Count == 0)
            {
                lstMsg.Add("O campo Trabalho Social essencial desse serviço é obrigatório!");
            }

            if (obj.IdSexo == 0)
            {
                lstMsg.Add("O campo Sexo é obrigatório");
            }

            if (obj.IdRegiaoMoradia == 0)
            {
                lstMsg.Add("O campo Região de moradia dos usuários é obrigatório!");
            }

            if (obj.IdCaracteristicasTerritorio == 0)
            {
                lstMsg.Add("O campo Demanda prioritária do serviço é obrigatório!");
            }

            if (obj.ServicoEstadualizado && (obj.ValorEstadualizado == 0 || String.IsNullOrEmpty(obj.ValorEstadualizado.ToString())))
            {
                lstMsg.Add("O campo Valor anual do convênio obrigatório!");
            }
            else if (!(obj is ServicoRecursoFinanceiroPrivadoInfo) && (obj.ValorEstadualAssistencia + obj.ValorEstadualFEDCA + obj.ValorFederalAssistencia + obj.ValorFederalFNDCA + obj.ValorMunicipalAssistencia + obj.ValorMunicipalFMDCA + obj.ValorMunicipalFMI.Value + obj.ValorEstadualFEI.Value + obj.ValorFederalFNI.Value) == 0m) //Bruno V.
            {
                lstMsg.Add("É obrigatório informar algum recurso financeiro!");
            }

            //Bruno V.
            if (obj.UsuarioTipoServico != null)
            {
                if ((obj.UsuarioTipoServico.IdTipoServico == 138 || obj.UsuarioTipoServico.IdTipoServico == 145 || obj.UsuarioTipoServico.IdTipoServico == 153)
                    && (string.IsNullOrEmpty(obj.DescricaoServicoNaoTipificado.Trim()) || string.IsNullOrEmpty(obj.ObjetivoServicoNaoTipificado.Trim())))
                {
                    lstMsg.Add("Quando for feito cadastro de Serviço Não Tipificado os campos de tipo de serviço e objetivo do tipo de serviço devem ser de preenchimento obrigatório.");
                }
                obj.UsuarioTipoServico = null;
            }

            if (lstMsg.Count > 0)
                throw new Exception(Extensions.Concat(lstMsg, System.Environment.NewLine));
        }

        public void ValidarCRAS(ServicoRecursoFinanceiroCRASInfo obj)
        {
            var lstMsg = new List<string>();
            try
            {
                Validar(obj);
            }
            catch (Exception ex)
            {
                lstMsg.Add(ex.Message);
            }

            if (lstMsg.Count > 0)
                throw new Exception(Extensions.Concat(lstMsg, System.Environment.NewLine));
        }

        public void ValidarCREAS(ServicoRecursoFinanceiroCREASInfo obj)
        {
            var lstMsg = new List<string>();
            try
            {
                Validar(obj);
            }
            catch (Exception ex)
            {
                lstMsg.Add(ex.Message);
            }

            if (lstMsg.Count > 0)
                throw new Exception(Extensions.Concat(lstMsg, System.Environment.NewLine));
        }

        public void ValidarCentroPOP(ServicoRecursoFinanceiroCentroPOPInfo obj)
        {
            var lstMsg = new List<string>();
            try
            {
                Validar(obj);
            }
            catch (Exception ex)
            {
                lstMsg.Add(ex.Message);
            }

            if (lstMsg.Count > 0)
                throw new Exception(Extensions.Concat(lstMsg, System.Environment.NewLine));
        }

        public void ValidarPublico(ServicoRecursoFinanceiroPublicoInfo obj)
        {
            var lstMsg = new List<string>();
            try
            {
                Validar(obj);
            }
            catch (Exception ex)
            {
                lstMsg.Add(ex.Message);
            }

            if (lstMsg.Count > 0)
                throw new Exception(Extensions.Concat(lstMsg, System.Environment.NewLine));
        }

        public void ValidarPrivado(ServicoRecursoFinanceiroPrivadoInfo obj)
        {
            var lstMsg = new List<string>();
            try
            {
                Validar(obj);

                if (!obj.ServicoEstadualizado && (obj.ValorEstadualAssistencia + obj.ValorEstadualFEDCA + obj.ValorFederalAssistencia + 
                    obj.ValorFederalFNDCA + obj.ValorMunicipalAssistencia + obj.ValorMunicipalFMDCA + 
                    obj.ValorPrivadoEmpresas + obj.ValorPrivadoOrganizacoes + obj.ValorPrivadoPessoasFisicas + obj.ValorPrivadoProprios + 
                    obj.ValorMunicipalFMI.Value + obj.ValorEstadualFEI.Value + obj.ValorFederalFNI.Value) == 0m) //Bruno V.
                {
                    lstMsg.Add("É obrigatório informar algum recurso financeiro!");
                }

            }
            catch (Exception ex)
            {
                lstMsg.Add(ex.Message);
            }

            if (lstMsg.Count > 0)
                throw new Exception(Extensions.Concat(lstMsg, System.Environment.NewLine));
        }
    }
}
