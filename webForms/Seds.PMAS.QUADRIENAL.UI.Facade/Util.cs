using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.Seguranca.Entidades;

namespace Seds.PMAS.QUADRIENAL.UI.Processos
{
    public static class Util
    {
        public static Logradouro ConsultaCEP(string cep)
        {
            Logradouro logradouro;
            CadastroCep.Cep wsCEP;

            try
            {
                wsCEP = new CadastroCep.Cep(cep);
                wsCEP.ConsultaCEP();

                var end = !String.IsNullOrEmpty(wsCEP.CEP_LOGR_TIPO.Trim()) ? wsCEP.CEP_LOGR_TIPO.Trim() + " " : "";
                end += (!String.IsNullOrEmpty(wsCEP.CEP_LOGR_TITULO.Trim()) ? wsCEP.CEP_LOGR_TITULO.Trim() + " " : "");
                end += (!String.IsNullOrEmpty(wsCEP.CEP_LOGR_PREPOSICAO.Trim()) ? wsCEP.CEP_LOGR_PREPOSICAO.Trim() + " " : "");
                end += wsCEP.CEP_LOGR_NOME.Trim();

                logradouro = new Logradouro(end,
                    "",
                    "",
                    wsCEP.CEP_LOGR_BAIRRO.Trim(),
                    cep, wsCEP.CEP_LOC_NOME_MUNICIPIO.Trim());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return logradouro;
        }

        public static String FormatarCNPJ(String cnpj)
        {
            cnpj = "00000000000000" + cnpj;
            cnpj = cnpj.Substring(cnpj.Length - 14, 14);
            cnpj = cnpj.Insert(2, ".");
            cnpj = cnpj.Insert(6, ".");
            cnpj = cnpj.Insert(10, "/");
            cnpj = cnpj.Insert(15, "-");
            return cnpj;
        }


        public static void InserirItemEscolha(DropDownList ddl, String texto = "[Escolha uma Opção]")
        {
            try
            {
                ddl.Items.Insert(0, new ListItem(texto, "0"));
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<PerfilInfo> GetPerfis()  // (Alteração de perfis)
        {
            var lst = new List<PerfilInfo>();
            lst.Add(new PerfilInfo() { Id = 64, Nome = "Orgão Gestor" });
            lst.Add(new PerfilInfo() { Id = 65, Nome = "DRADS Administrador" });
            lst.Add(new PerfilInfo() { Id = 66, Nome = "SEDS" });
            lst.Add(new PerfilInfo() { Id = 67, Nome = "CAS" });
            lst.Add(new PerfilInfo() { Id = 68, Nome = "Administrador" });
            lst.Add(new PerfilInfo() { Id = 69, Nome = "Consulta" });
            lst.Add(new PerfilInfo() { Id = 70, Nome = "DRADS" });
            lst.Add(new PerfilInfo() { Id = 71, Nome = "CMAS" });
            return lst.OrderBy(p => p.Nome).ToList();
        }

        public static List<PerfilInfo> GetPerfisDradsAdministrador()
        {
            List<short> lista = new List<short>();
            lista.Add((short)EPerfil.CMAS);
            lista.Add((short)EPerfil.DRADS);
            lista.Add((short)EPerfil.OrgaoGestor);
            lista.Add((short)EPerfil.Convidados);

            return GetPerfis().Where(p => lista.Any(i => i == p.Id)).OrderBy(p => p.Nome).ToList();
        }

        public static void CarregarPrefeitura()
        {
            using (var proxy = new ProxyPrefeitura())
            {
                if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                {
                    SessaoPmas.UsuarioLogado.Prefeitura = proxy.Service.GetPrefeituraById(SessaoPmas.UsuarioLogado.Prefeitura.Id);    
              
                    SessaoPmas.UsuarioLogado.Prefeitura.Municipio = ProxyDivisaoAdministrativa.MunicipiosEstaduais.FirstOrDefault(m => m.Id == SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio);    
          
                    SessaoPmas.UsuarioLogado.Prefeitura.Municipio.Drads = ProxyDivisaoAdministrativa.Drads.FirstOrDefault(d => d.Id == SessaoPmas.UsuarioLogado.Prefeitura.Municipio.IdDrads);    
                } 
            }
        }

        public static Boolean VerificarAlteracoes()
        {

            if (SessaoPmas.UsuarioLogado == null)
                return false;

            if (SessaoPmas.UsuarioLogado.Prefeitura != null)
            {
                return SessaoPmas.UsuarioLogado.Prefeitura.Revisao > 0 && SessaoPmas.UsuarioLogado.EnumPerfil != EPerfil.SEDS && SessaoPmas.UsuarioLogado.EnumPerfil != EPerfil.Convidados &&
                SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao != Convert.ToInt32(ESituacao.Aprovado)
                    && SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao != Convert.ToInt32(ESituacao.Rejeitado);

            }
            return false;
        }

        #region MENU
        public static string RetonaDescricaoPagina(List<RecursoInfo> recursos, int recursoId)
        {
            try
            {
                string descricao = string.Empty;
                var recurso = recursos.Where(a => a.Id == recursoId).FirstOrDefault();
                if (recurso == null)
                    return String.Empty;
                if (recurso.IdPai.HasValue)
                {
                    var recursoPai = recursos.Where(a => a.Id == recurso.IdPai.Value).FirstOrDefault();
                    //descricao += "<li><a href=''>" + RetonaDescricaoPagina(recursos, recursoPai.Id) + "</a></li>";
                    descricao += RetonaDescricaoPagina(recursos, recursoPai.Id);
                }
                descricao += "<li><a href=''>" + recurso.Nome + "</a></li>";

                return descricao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string RetonaDescricaoPagina(List<RecursoInfo> recursos, string pagina)
        {
            try
            {
                string descricao = string.Empty;
                var recurso = recursos.Where(a => a.Pagina == pagina).FirstOrDefault();
                if (recurso == null)
                    return String.Empty;
                if (recurso.IdPai.HasValue)
                {
                    var recursoPai = recursos.Where(a => a.Id == recurso.IdPai.Value).FirstOrDefault();
                    descricao = RetonaDescricaoPagina(recursos, recursoPai.Id);
                }
                descricao = descricao + "<li><a href=''>" + recurso.Nome + "</a></li>";

                return descricao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string RetornaRecursoProximo(List<RecursoInfo> recursos, string url)
        {
            try
            {
                var recursoAtual = SelecionaPagina(recursos, url);
                string retorno = string.Empty;
                if (recursoAtual != null)
                {
                    var proximoRecurso = (from a in recursos
                                          where a.IdPai == recursoAtual.IdPai
                                          && a.Ordem == (recursoAtual.Ordem + 1)
                                          orderby a.Ordem
                                          select a).FirstOrDefault();

                    if (proximoRecurso != null)
                    {
                        if (string.IsNullOrEmpty(proximoRecurso.Pagina))
                        {
                            proximoRecurso = (from a in recursos
                                              where a.IdPai == proximoRecurso.Id
                                              orderby a.Ordem
                                              select a).FirstOrDefault();
                        }
                        retorno = proximoRecurso.Pagina;
                    }
                    else
                    {
                        recursoAtual = (from a in recursos
                                        where a.Id == recursoAtual.IdPai
                                        select a).FirstOrDefault();

                        if (recursoAtual == null)
                            return retorno;

                        proximoRecurso = (from a in recursos
                                          where a.IdPai == recursoAtual.IdPai
                                          && a.Ordem == (recursoAtual.Ordem + 1)
                                          orderby a.Ordem
                                          select a).FirstOrDefault();

                        if (proximoRecurso != null)
                            retorno = proximoRecurso.Pagina;
                    }
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string RetornaRecursoAnterior(List<RecursoInfo> recursos, string url)
        {
            try
            {
                RecursoInfo recursoAtual = SelecionaPagina(recursos, url);
                string retorno = string.Empty;
                if (recursoAtual != null)
                {
                    var anteriorRecurso = (from a in recursos
                                           where a.IdPai == recursoAtual.IdPai
                                           && a.Ordem == (recursoAtual.Ordem - 1)
                                           orderby a.Ordem
                                           select a).FirstOrDefault();

                    if (anteriorRecurso != null)
                    {
                        if (string.IsNullOrEmpty(anteriorRecurso.Pagina))
                        {
                            anteriorRecurso = (from a in recursos
                                               where a.IdPai == anteriorRecurso.Id
                                               orderby a.Ordem descending
                                               select a).FirstOrDefault();
                        }
                        retorno = anteriorRecurso.Pagina;
                    }
                    else
                    {
                        recursoAtual = (from a in recursos
                                        where a.Id == recursoAtual.IdPai
                                        select a).FirstOrDefault();

                        if (recursoAtual == null)
                            return retorno;

                        anteriorRecurso = (from a in recursos
                                           where a.IdPai == recursoAtual.IdPai
                                           && a.Ordem == (recursoAtual.Ordem - 1)
                                           orderby a.Ordem
                                           select a).FirstOrDefault();

                        if (anteriorRecurso != null)
                            retorno = anteriorRecurso.Pagina;
                    }
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static RecursoInfo SelecionaPagina(List<RecursoInfo> recursos, string recursoPagina)
        {
            try
            {
                var resultado = (from a in recursos
                                 where a.Pagina == recursoPagina
                                 orderby a.Ordem
                                 select a).FirstOrDefault();
                return resultado;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        public static string GetJavaScriptDialogWarning(string msg)
        {
            //<div data-role="dialog" id="dialog3" class="padding20 dialog success" data-close-button="true" data-overlay="true" data-overlay-color="op-dark" data-overlay-click-close="false" style="width: auto; height: auto; visibility: visible; left: 637.5px; top: 78px;">
            var script = "$('#warning').dialog({ autoOpen: false, autoHeight: true, buttons: { 'Ok': function () { $(this).dialog('close'); } } });";
            script += "$('#msgWarning').html('" + msg.Replace("'", "").Replace(System.Environment.NewLine, "<br/>") + "');";
            script += "$('#warning').dialog('open');";

            return script;
        }

        public static string GetJavascriptDialogError(string msg)
        {
            // "class="padding20 dialog warning" data-close-button="true" data-type="warning" data-overlay="true" data-overlay-color="op-dark" data-overlay-click-close="false" style="width: auto; height: auto; visibility: visible; left: 637.5px; top: 78px;"

            var script = "$('#alerta').dialog({ autoOpen: false, autoHeight: true,   modal: true,  actions: { 'Ok': function () { $(this).dialog('close'); } } });";
            script += "$('#msgAlerta').html('<h4>Por favor, verifique as inconsistências.</h4> </br>" + msg.Replace("'", "").Replace(System.Environment.NewLine, "<br/>") + "');";
            script += "$('#alerta').dialog('open');";
            return script;
        }


        public static string GetJavaScriptDialogOK(string msg)
        {
            //<div data-role="dialog" id="dialog3" class="padding20 dialog success" data-close-button="true" data-overlay="true" data-overlay-color="op-dark" data-overlay-click-close="false" style="width: auto; height: auto; visibility: visible; left: 637.5px; top: 78px;">
            var script = "$('#sucesso').dialog({ autoOpen: false, autoHeight: true, buttons: { 'Ok': function () { $(this).dialog('close'); } } });";
            script += "$('#msg').html('" + msg.Replace("'", "").Replace(System.Environment.NewLine, "<br/>") + "');";
            script += "$('#sucesso').dialog('open');";

            return script;
        }

        public static string Concat(this IList list, string separator)
        {
            var s = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                s.Append(item);
                if (i < list.Count - 1)
                    s.Append(separator);
            }
            return s.ToString();
        }

        #region parse
        public static int? TryParseInt32(string inteiro)
        {
            try
            {
                int? valor = null;
                int parametro;
                if (int.TryParse(inteiro, out parametro))
                    valor = parametro;

                return valor;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int? TryParseInt16(string inteiro)
        {
            try
            {
                short? valor = null;
                short parametro;
                if (short.TryParse(inteiro, out parametro))
                    valor = parametro;

                return valor;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static decimal? TryParseDecimal(string inteiro)
        {
            try
            {
                decimal? valor = null;
                decimal parametro;
                if (decimal.TryParse(inteiro, out parametro))
                    valor = parametro;

                return valor;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public static void MoveAllItems(ListBox source, ListBox target)
        {
            while (source.Items.Count != 0)
            {
                target.Items.Add(source.Items[0]);
                source.Items.RemoveAt(0);
            }
        }

        public static void MoveItemSelected(ListBox _Left, ListBox _Right)
        {
            foreach (ListItem item in _Right.Items)
            {
                ListItem _leftitem = _Left.Items.FindByValue(item.Value);
                _Left.Items.Remove(_leftitem);
            }
        }

        public static void MoveSelectedItems(ListBox source, ListBox target)
        {
            while (source.SelectedIndex != -1)
            {
                target.Items.Add(source.SelectedItem);
                source.Items.Remove(source.SelectedItem);
            }
        }

        public static decimal CalcularPercentualDecimal(decimal maximo, decimal valor)
        {
            return (valor / maximo) * 100;
        }



        ///// <summary>
        ///// Verifica a permissão dos controles
        ///// </summary>
        ///// <param name="controles"></param>
        //public static void VerificaPermissao(WebControl[] controles, HttpSessionState session)
        //{

        //    bool desbloqueio = false;
        //    if (SessaoPmas.UsuarioLogado != null)
        //    {
        //        if (SessaoPmas.UsuarioLogado.Prefeitura != null)
        //        {
        //            desbloqueio = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
        //                || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
        //                || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
        //                && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);
        //            if (!desbloqueio)
        //            {
        //                foreach (WebControl item in controles)
        //                {
        //                    if (item == null)
        //                        continue;

        //                    if (item is TextBox)
        //                    {
        //                        ((TextBox)item).ReadOnly = true;
        //                        //continue;
        //                    }
        //                    item.Enabled = false;
        //                }
        //            }
        //        }
        //    }
        //}

        //public static void VerificaPermissaoCMAS(WebControl[] controles)
        //{

        //    bool bloqueio = false;
        //    if (SessaoPmas.UsuarioLogado != null)
        //    {
        //        if (SessaoPmas.UsuarioLogado.Prefeitura != null)
        //        {

        //            bloqueio = (SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.EmAnalisedoCMAS)
        //                && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS);
        //            foreach (WebControl item in controles)
        //            {
        //                if (item == null)
        //                    continue;
        //                if (item is TextBox)
        //                {

        //                    ((TextBox)item).ReadOnly = !bloqueio;
        //                    item.Enabled = bloqueio;
        //                    continue;
        //                }
        //                item.Enabled = bloqueio;
        //            }
        //        }
        //    }
        //}

        //public static void VerificaPermissaoCMAS(WebControl[] controles, HttpSessionState session)
        //{

        //    bool bloqueio = false;
        //    if (SessaoPmas.UsuarioLogado != null)
        //    {
        //        if (SessaoPmas.UsuarioLogado.Prefeitura != null)
        //        {

        //            bloqueio = (SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.EmAnalisedoCMAS)
        //                && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS);
        //            foreach (WebControl item in controles)
        //            {
        //                if (item == null)
        //                    continue;
        //                if (item is TextBox)
        //                {

        //                    ((TextBox)item).ReadOnly = !bloqueio;
        //                    // item.Enabled = bloqueio;
        //                    continue;
        //                }
        //                item.Enabled = bloqueio;
        //            }
        //        }
        //    }
        //}

        //public static void VerificaPermissaoCMASParecer(WebControl[] controles)
        //{

        //    bool bloqueio = false;
        //    if (SessaoPmas.UsuarioLogado != null)
        //    {
        //        if (SessaoPmas.UsuarioLogado.Prefeitura != null)
        //        {

        //            bloqueio = (SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.EmAnalisedoCMAS) && SessaoPmas.UsuarioLogado.Prefeitura.Revisao == 0
        //                && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS);
        //            foreach (WebControl item in controles)
        //            {
        //                if (item == null)
        //                    continue;
        //                if (item is TextBox)
        //                {
        //                    ((TextBox)item).ReadOnly = !bloqueio;
        //                    continue;
        //                }
        //                item.Enabled = bloqueio;
        //            }
        //        }
        //    }
        //}

        //public static bool VerificaPermissao()
        //{

        //    bool bloqueio = false;
        //    if (SessaoPmas.UsuarioLogado != null)
        //    {
        //        if (SessaoPmas.UsuarioLogado.Prefeitura != null)
        //        {
        //            bloqueio = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
        //                || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
        //                || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
        //                && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);
        //        }
        //    }
        //    return bloqueio;
        //}

        //#region fluxo bloqueio exercicio
        //public static void VerificaPermissaoDesbloquear(WebControl[] controles, bool desbloqueado)
        //{
        //    if (!desbloqueado)
        //    {
        //        if (SessaoPmas.UsuarioLogado != null)
        //        {
        //            if (SessaoPmas.UsuarioLogado.Prefeitura != null)
        //            {
        //                foreach (WebControl item in controles)
        //                {
        //                    if (item == null)
        //                        continue;

        //                    if (item is TextBox)
        //                    {
        //                        ((TextBox)item).ReadOnly = true;
        //                        //continue;
        //                    }
        //                    item.Enabled = false;
        //                }
        //            }
        //        }
        //    }
        //}
        //#endregion


    }
}
