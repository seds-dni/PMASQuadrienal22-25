using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using Seds.Seguranca.Entidades;
using System.Collections;
using System.IO;
using System.Reflection;
using Microsoft.IdentityModel.Claims;
using System.Threading;
using System.Web.UI.HtmlControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;

namespace Seds.PMAS.QUADRIENAL.CA
{
    public class Permissao
    {

        public static void VerificarPermissaoExcluirBaseadoPerfil(WebControl controle, HttpSessionState session)
        {
            bool desbloqueado = false;
            if (SessaoPmas.UsuarioLogado != null)
            {
                if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                {
                    //desbloquear somente para o gestor e com plano desbloqueado
                    desbloqueado = (SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                                    && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                    ImageButton controleExcluir = ((ImageButton)controle);
                    controleExcluir.Visible = desbloqueado;
                }
            }
        }

        /// <summary>
        /// Verifica a permissão dos controles
        /// </summary>
        /// <param name="controles"></param>
        public static void VerificarPermissaoControles(WebControl[] controles, HttpSessionState session)
        {
            bool desbloqueio = false;
            if (SessaoPmas.UsuarioLogado != null)
            {
                if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                {
                    desbloqueio = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                        || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                        || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                        && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);
                    if (!desbloqueio)
                    {
                        foreach (WebControl item in controles)
                        {
                            if (item == null)
                                continue;

                            if (item is TextBox)
                            {
                                ((TextBox)item).ReadOnly = true;
                                //continue;
                            }
                            item.Enabled = false;
                        }
                    }
                }
            }
        }

        public static void VerificarPermissaoCMAS(WebControl[] controles)
        {

            bool bloqueio = false;
            if (SessaoPmas.UsuarioLogado != null)
            {
                if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                {

                    bloqueio = (SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.EmAnalisedoCMAS)
                        && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS);
                    foreach (WebControl item in controles)
                    {
                        if (item == null)
                            continue;
                        if (item is TextBox)
                        {

                            ((TextBox)item).ReadOnly = !bloqueio;
                            item.Enabled = bloqueio;
                            continue;
                        }
                        item.Enabled = bloqueio;
                    }
                }
            }
        }

        public static void VerificarPermissaoCMAS(WebControl[] controles, HttpSessionState session)
        {

            bool bloqueio = false;
            if (SessaoPmas.UsuarioLogado != null)
            {
                if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                {

                    bloqueio = (SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.EmAnalisedoCMAS)
                        && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS);
                    foreach (WebControl item in controles)
                    {
                        if (item == null)
                            continue;
                        if (item is TextBox)
                        {

                            ((TextBox)item).ReadOnly = !bloqueio;
                            // item.Enabled = bloqueio;
                            continue;
                        }
                        item.Enabled = bloqueio;
                    }
                }
            }
        }

        public static void VerificarPermissaoCMASParecer(WebControl[] controles)
        {

            bool bloqueio = false;
            if (SessaoPmas.UsuarioLogado != null)
            {
                if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                {

                    bloqueio = (SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.EmAnalisedoCMAS) && SessaoPmas.UsuarioLogado.Prefeitura.Revisao == 0
                        && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS);
                    foreach (WebControl item in controles)
                    {
                        if (item == null)
                            continue;
                        if (item is TextBox)
                        {
                            ((TextBox)item).ReadOnly = !bloqueio;
                            continue;
                        }
                        item.Enabled = bloqueio;
                    }
                }
            }
        }

        public static bool VerificarPermissao()
        {

            bool bloqueio = false;
            if (SessaoPmas.UsuarioLogado != null)
            {
                if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                {
                    bloqueio = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                        || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                        || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                        && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);
                }
            }
            return bloqueio;
        }


        public static void VerificaPermissaoDradsExercicio(WebControl[] controles, bool desbloqueado)
        {
            if (!desbloqueado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        foreach (WebControl item in controles)
                        {
                            if (item == null)
                                continue;

                            if (item is TextBox)
                            {
                                ((TextBox)item).ReadOnly = true;
                                //continue;
                            }
                            item.Enabled = false;
                        }
                    }
                }
            }
        }

        public static void VerificaPermissaoExercicio(WebControl[] controles, bool desbloqueado)
        {
            if (!desbloqueado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        foreach (WebControl item in controles)
                        {
                            if (item == null)
                                continue;

                            if (item is TextBox)
                            {
                                ((TextBox)item).ReadOnly = true;
                                //continue;
                            }
                            item.Enabled = false;
                        }
                    }
                }
            }
        }


        #region Bloco 0 - Inicio
        public static class Bloco0Inicio
        {
            public static bool VerificaPermissaoExercicioBloco0DradsValores(int exercicioSolicitado, PrefeituraInfo prefeitura)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (prefeitura != null)
                    {

                        var desbloqueadoExercicio = prefeitura.PrefeiturasExerciciosBloqueio
                                                               .SingleOrDefault(x => x.Exercicio == exercicioSolicitado 
                                                                && x.IdRefBloqueio == 40);

                        if (desbloqueadoExercicio != null)
                        {
                            return desbloqueadoExercicio.Desbloqueado.Value;
                        }
                        else {
                            return false;
                        }
                    }
                }
                return false;
            }
            public static bool VerificaPermissaoExercicioBloco0DradsReprogramado(int exercicioSolicitado, PrefeituraInfo prefeitura)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (prefeitura != null)
                    {
                        var desbloqueadoExercicio = prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1040);

                        if (desbloqueadoExercicio != null)
                        {
                            return desbloqueadoExercicio.Desbloqueado.Value;
                        }
                        else {
                            return false;
                        }
                    }
                }
                return false;
            }

            public static bool VerificaPermissaoExercicioBloco0Inicio(WebControl[] controles, int exercicioSolicitado, PrefeituraInfo prefeitura)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (prefeitura != null)
                    {
                        var desbloqueadoExercicio = prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 40);

                        bool desbloqueadoControles = false;

                        if (desbloqueadoExercicio != null)
                        {
                            desbloqueadoControles = (desbloqueadoExercicio.Desbloqueado.Value);
                        }
                  
                        foreach (WebControl item in controles)
                        {
                            if (desbloqueadoControles)
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = false;
                                    ((TextBox)item).Enabled = true;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = true;
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Enabled = true;
                                }
                            }
                            else
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = true;
                                    ((TextBox)item).Enabled = false;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = false;
                                }

                                if (item is Button)
                                {
                                    ((Button)item).Enabled = false;
                                }
                            }

                        }
                        return desbloqueadoControles;
                    }
                }
                return false;
            }
            public static bool VerificaPermissaoExercicioBloco0InicioReprogramacao(WebControl[] controles, int exercicioSolicitado, PrefeituraInfo prefeitura)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (prefeitura != null)
                    {

                        var desbloqueadoExercicio = prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1040);

                        bool controleDesbloqueado = false;
                        if (desbloqueadoExercicio != null)
                        { 
                            controleDesbloqueado = desbloqueadoExercicio.Desbloqueado.Value;
                        }


                        foreach (WebControl item in controles)
                        {
                            ((TextBox)item).Enabled = controleDesbloqueado;
                            ((TextBox)item).ReadOnly = !controleDesbloqueado;
                        }


                        return controleDesbloqueado;
                    }
                }
                return false;
            }
        }
        #endregion

        #region Bloco 1
        public static class BlocoI
        {
            public static bool VerificaPermissaoExercicioBlocoI(WebControl[] controles, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                        var desbloqueadoExercicio = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 10);

                        bool desbloqueadoControles = false;

                        if (desbloqueadoExercicio != null)
                        {
                            desbloqueadoControles = (desbloqueadoPlano && desbloqueadoExercicio.Desbloqueado.Value);
                        }

                        foreach (WebControl item in controles)
                        {
                            if (desbloqueadoControles)
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = false;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = true;
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Enabled = true;
                                }
                            }
                            else
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = true;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = false;
                                }

                                if (item is Button)
                                {
                                    ((Button)item).Enabled = false;
                                }
                            }

                        }
                        return desbloqueadoControles;
                    }
                }
                return false;
            }
        }
        #endregion

        #region Bloco 2
        public static class BlocoII
        {
            public static bool VerificaPermissaoExercicioBlocoII(WebControl[] controles, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                        var desbloqueadoExercicio = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 75);

                        bool desbloqueadoControles = false;

                        if (desbloqueadoExercicio != null)
                        {
                            desbloqueadoControles = (desbloqueadoPlano && desbloqueadoExercicio.Desbloqueado.Value);
                        }

                        foreach (WebControl item in controles)
                        {
                            if (desbloqueadoControles)
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = false;
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Visible = true;
                                }
                            }
                            else
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = true;
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Visible = false;
                                }
                            }

                        }
                        return desbloqueadoControles;
                    }
                }
                return false;
            }
        }
        #endregion

        #region Bloco 3
        public static class BlocoIII
        {
            #region permissao: servico rede direta
            //Fundos
            public static bool VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(WebControl[] controles, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);
                        bool desbloqueadoExercicio = false;
                        var exercicioSelecionado = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 19);
                        if (exercicioSelecionado != null)
                        {
                            desbloqueadoExercicio = exercicioSelecionado.Desbloqueado.Value;
                        }

                        bool desbloqueadoControles = (desbloqueadoPlano && desbloqueadoExercicio);

                        foreach (WebControl item in controles)
                        {
                            if (item is TextBox)
                            {
                                ((TextBox)item).ReadOnly = !desbloqueadoControles;
                            }

                            if (item is RadioButtonList)
                            {
                                ((RadioButtonList)item).Enabled = desbloqueadoControles;
                            }
                            if (item is Button)
                            {
                                ((Button)item).Enabled = desbloqueadoControles;
                            }
                        }
                        return desbloqueadoControles;
                    }
                }
                return false;
            }
            //Reprogramacao
            public static bool VerificaPermissaoRedeDiretaBlocoIIIReprogramacao(WebControl[] controles, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                        bool desbloqueadoExercicio = false;
                        var exercicioSelecionado = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1019);
                        if (exercicioSelecionado != null)
                        {
                            desbloqueadoExercicio = exercicioSelecionado.Desbloqueado.Value;
                        }


                        bool desbloqueadoControles = (desbloqueadoPlano && desbloqueadoExercicio);

                        foreach (WebControl item in controles)
                        {
                            if (item is TextBox)
                            {
                                ((TextBox)item).ReadOnly = !desbloqueadoControles;
                            }

                            if (item is RadioButtonList)
                            {
                                ((RadioButtonList)item).Enabled = desbloqueadoControles;
                            }
                            if (item is Button)
                            {
                                ((Button)item).Enabled = desbloqueadoControles;
                            }
                        }
                        return desbloqueadoControles;
                    }
                }
                return false;
            }
            //Salvar
            public static bool VerificaPermissaoRedeDiretaBlocoIIIBotaoSalvar(WebControl controle, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                        bool desbloqueadoExercicioRedeDireta = false;
                        var exercicioEscolhidoRedeDireta = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => (x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 19));
                        if (exercicioEscolhidoRedeDireta != null)
                        {
                            desbloqueadoExercicioRedeDireta = exercicioEscolhidoRedeDireta.Desbloqueado.Value;
                        }


                        bool desbloqueadoExercicioReprogramado = false;
                        var exercicioEscolhidoRedeDiretaReprogramado = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => (x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1019));
                        if (exercicioEscolhidoRedeDiretaReprogramado != null)
                        {
                            desbloqueadoExercicioReprogramado = exercicioEscolhidoRedeDiretaReprogramado.Desbloqueado.Value;
                        }

                        bool desbloqueadoControles = (desbloqueadoPlano && (desbloqueadoExercicioRedeDireta || desbloqueadoExercicioReprogramado));

                        ((Button)controle).Enabled = desbloqueadoControles;

                        return desbloqueadoControles;
                    }
                }
                return false;
            }
            //Excluir
            public static bool VerificaPermissaoRedeDiretaBlocoIIIBotaoExcluir(WebControl controle)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoControles = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                        ((ImageButton)controle).Enabled = desbloqueadoControles;
                        ((ImageButton)controle).Visible = desbloqueadoControles;

                        return desbloqueadoControles;
                    }
                    else { return false; }
                }
                else { return false; }
            }

            #endregion

            #region permissao: servico rede indireta
            //Fundos
            public static bool VerificaPermissaoRedeIndiretaBlocoIIIRecursosFinanceiros(WebControl[] controles, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                        var desbloqueadoExercicio = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 20);
                        bool desbloqueadoControles = false;

                        if (desbloqueadoExercicio != null)
                        {
                            desbloqueadoControles = (desbloqueadoPlano && desbloqueadoExercicio.Desbloqueado.Value);
                        }


                        foreach (WebControl item in controles)
                        {
                            if (desbloqueadoControles)
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = false;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = true;
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Enabled = true;
                                }
                            }
                            else
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = true;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = false;
                                }

                                if (item is Button)
                                {
                                    ((Button)item).Enabled = false;
                                }
                            }

                        }
                        return desbloqueadoControles;
                    }
                }
                return false;
            }
            //Reprogramacao
            public static bool VerificaPermissaoRedeIndiretaBlocoIIIReprogramacao(WebControl[] controles, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                        var desbloqueadoExercicio = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1020);

                        bool desbloqueadoControles = false;
                        if (desbloqueadoExercicio != null)
                        {
                            desbloqueadoControles = (desbloqueadoPlano && desbloqueadoExercicio.Desbloqueado.Value);
                        }

                        foreach (WebControl item in controles)
                        {
                            if (desbloqueadoControles)
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = false;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = true;
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Enabled = true;
                                }
                            }
                            else
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = true;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = false;
                                }

                                if (item is Button)
                                {
                                    ((Button)item).Enabled = false;
                                }
                            }

                        }
                        return desbloqueadoControles;
                    }
                }
                return false;
            }
            public static bool VerificaPermissaoRedeIndiretaBlocoIIIBotaoSalvar(WebControl controle, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                        var desbloqueadoExercicioRedeDireta = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => (x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 19));

                        var desbloqueadoExercicioReprogramado = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => (x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1019));

                        bool desbloqueadoControles = false;

                        if (desbloqueadoExercicioRedeDireta != null || desbloqueadoExercicioReprogramado != null)
                        {
                            desbloqueadoControles = (desbloqueadoPlano && (desbloqueadoExercicioRedeDireta.Desbloqueado.Value || desbloqueadoExercicioReprogramado.Desbloqueado.Value));
                        }

                        ((Button)controle).Enabled = desbloqueadoControles;

                        return desbloqueadoControles;
                    }
                }
                return false;
            }
            public static bool VerificaPermissaoRedeIndiretaBlocoIIIBotaoExcluir(WebControl controle)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoControles = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                        ((ImageButton)controle).Enabled = desbloqueadoControles;
                        ((ImageButton)controle).Visible = desbloqueadoControles;

                        return desbloqueadoControles;
                    }
                    else { return false; }
                }
                else { return false; }
            }
            public static bool VerificaPermissaoRedeIndiretaBlocoIIIBotaoExcluirEditar(WebControl[] controles)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoControles = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                        foreach (var controle in controles)
                        {
                            if (controle is ImageButton)
                            {
                                ((ImageButton)controle).Enabled = desbloqueadoControles;
                                ((ImageButton)controle).Visible = desbloqueadoControles;
                            }
                        }

                        return desbloqueadoControles;
                    }
                    else { return false; }
                }
                else { return false; }
            }
            //Botao adicionar local execucao
            public static bool VerificaPermissaoAdicionarLocalExecucao(Button btnLocalExecucao)
            {
                bool desbloqueado = SessaoPmas.UsuarioLogado.Prefeitura != null
                                    && (
                                           SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado) ||
                                           SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads) ||
                                           SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS)
                                        );
                return (desbloqueado && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);
            }

            #endregion

            #region permissao: programas e beneficios
            public static bool VerificaPermissaoExercicioProgramaProjetoBlocoIII(WebControl[] controles, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                        var desbloqueadoExercicio = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 23);

                        bool desbloqueadoControles = false;

                        if (desbloqueadoExercicio != null)
                        {
                            desbloqueadoControles = (desbloqueadoPlano && desbloqueadoExercicio.Desbloqueado.Value);
                        }

                        foreach (WebControl item in controles)
                        {
                            if (desbloqueadoControles)
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = false;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = true;
                                }
                                if (item is CheckBox)
                                {
                                    ((CheckBox)item).Enabled = true;
                                }
                                if (item is CheckBoxList)
                                {
                                    ((CheckBox)item).Enabled = true;
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Enabled = true;
                                    ((Button)item).Visible = true;
                                }
                            }
                            else
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = true;
                                }
                                if (item is CheckBox)
                                {
                                    ((CheckBox)item).Enabled = false;
                                }
                                if (item is CheckBoxList)
                                {
                                    ((CheckBox)item).Enabled = false;
                                }
                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = false;
                                }

                                if (item is Button)
                                {
                                    ((Button)item).Enabled = false;
                                    ((Button)item).Visible = false;
                                }
                            }

                        }
                        return desbloqueadoControles;
                    }
                }
                return false;
            }
            public static bool VerificaPermissaoExercicioBeneficiosEventuaisBlocoIII(WebControl[] controles, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                        var desbloqueadoExercicio = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 22);
                        bool desbloqueadoControles = false;
                        if (desbloqueadoExercicio != null)
                        {
                            desbloqueadoControles = (desbloqueadoPlano && desbloqueadoExercicio.Desbloqueado.Value);
                        }

                        foreach (WebControl item in controles)
                        {
                            if (desbloqueadoControles)
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = false;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = true;
                                }
                                if (item is DropDownList)
                                {
                                    ((DropDownList)item).Enabled = true;
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Enabled = true;
                                    ((Button)item).Visible = true;
                                }
                            }
                            else
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = true;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = false;
                                }
                                if (item is DropDownList)
                                {
                                    ((DropDownList)item).Enabled = false;
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Enabled = false;
                                    ((Button)item).Visible = false;
                                }
                            }

                        }
                        return desbloqueadoControles;
                    }
                }
                return false;
            }

            #endregion

            #region permissao: transferencia de renda
            public static bool VerificaPermissaoExercicioProgramaProjetoTransfRendaBlocoIII(WebControl[] controles, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                        var desbloqueadoExercicio = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 25);

                        bool desbloqueadoControles = false;

                        if (desbloqueadoExercicio != null)
                        {
                            desbloqueadoControles = (desbloqueadoPlano && desbloqueadoExercicio.Desbloqueado.Value);
                        }

                        foreach (WebControl item in controles)
                        {
                            if (desbloqueadoControles)
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = false;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = true;
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Enabled = true;
                                }
                            }
                            else
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = true;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = false;
                                }

                                if (item is Button)
                                {
                                    ((Button)item).Enabled = false;
                                }
                            }

                        }
                        return desbloqueadoControles;
                    }
                }
                return false;
            }

            public static bool VerificaPermissaoExercicioProgramaProjetoTransfRendaDatePicker(WebControl[] controles)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                        bool desbloqueadoControles = (desbloqueadoPlano);

                        foreach (WebControl item in controles)
                        {
                            if (desbloqueadoControles)
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = false;
                                    ((TextBox)item).Enabled = true;
                                }

                                if (item is ImageButton)
                                {
                                    ((ImageButton)item).Enabled = true;
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Enabled = true;
                                }
                            }
                            else
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = true;
                                    ((TextBox)item).Enabled = false;

                                }

                                if (item is ImageButton)
                                {
                                    ((ImageButton)item).Enabled = false;
                                }

                                if (item is Button)
                                {
                                    ((Button)item).Enabled = false;
                                }
                            }

                        }
                        return desbloqueadoControles;
                    }
                }
                return false;
            }
            #endregion


        }
        #endregion

        #region Bloco 5
        public static class BlocoV
        {
            #region Execucao Financeira
            public static bool VerificaPermissaoExercicioExecucaoFinanceiraFinalizarCalculoBlocoV(HtmlGenericControl[] controles, WebControl[] controles2, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                        var desbloqueadoExercicio = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 76);

                        bool desbloqueadoControles = false;

                        if (desbloqueadoExercicio != null)
                        {
                            desbloqueadoControles = (desbloqueadoPlano && desbloqueadoExercicio.Desbloqueado.Value);
                        }

                        foreach (var item in controles)
                        {
                            if (desbloqueadoControles)
                            {
                                ((HtmlGenericControl)item).Visible = true;
                            }
                            else
                            {
                                ((HtmlGenericControl)item).Visible = false;
                            }
                        }

                        foreach (WebControl item in controles2)
                        {
                            if (desbloqueadoControles)
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = false;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = true;
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Enabled = true;
                                }
                            }
                            else
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = true;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = false;
                                }

                                if (item is Button)
                                {
                                    ((Button)item).Enabled = false;
                                }
                            }

                        }

                        return desbloqueadoControles;
                    }

                }

                return false;

            }
            public static bool VerificaPermissaoExercicioExecucaoFinanceiraBlocoV(WebControl[] controles, HtmlGenericControl[] htmlControls, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        //bool desbloqueadoPlano = (SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado));
                        var desbloqueadoExercicio = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 76);

                        bool desbloqueadoControles = false;

                        if (desbloqueadoExercicio != null)
                        {
                            desbloqueadoControles = (desbloqueadoExercicio.Desbloqueado.Value);
                        }
                        //bool desbloqueadoControles = (desbloqueadoPlano && desbloqueadoExercicio);

                        #region web controls
                        if (controles != null)
                        {
                            foreach (WebControl item in controles)
                            {
                                if (desbloqueadoControles)
                                {
                                    if (item is TextBox)
                                    {
                                        ((TextBox)item).ReadOnly = false;
                                    }

                                    if (item is RadioButtonList)
                                    {
                                        ((RadioButtonList)item).Enabled = true;
                                    }
                                    if (item is Button)
                                    {
                                        ((Button)item).Enabled = true;
                                    }
                                }
                                else
                                {
                                    if (item is TextBox)
                                    {
                                        ((TextBox)item).ReadOnly = true;
                                    }

                                    if (item is RadioButtonList)
                                    {
                                        ((RadioButtonList)item).Enabled = false;
                                    }

                                    if (item is Button)
                                    {
                                        ((Button)item).Enabled = false;
                                    }
                                }

                            }
                        }
                        #endregion

                        #region html generics controls
                        if (htmlControls != null)
                        {
                            foreach (var item in htmlControls)
                            {
                                if (desbloqueadoControles)
                                {
                                    ((HtmlGenericControl)item).Visible = true;
                                }
                                else
                                {
                                    ((HtmlGenericControl)item).Visible = false;
                                }
                            }
                        }
                        #endregion

                        return desbloqueadoControles;
                    }
                }
                return false;
            }
            #endregion

            #region Lei orcamentaria
            public static bool VerificaPermissaoExercicioLOFLuxoAdministrativoBlocoV(WebControl[] controles, HtmlGenericControl[] htmlControls, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        List<PrefeituraExercicioBloqueioInfo> bloqueios = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio;
                        PrefeituraExercicioBloqueioInfo bloqueioDesbloqueio = bloqueios.Single(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 78);


                        bool desbloqueadoControles = bloqueioDesbloqueio.Desbloqueado.Value;

                        #region web controls
                        if (controles != null)
                        {
                            foreach (WebControl item in controles)
                            {
                                if (desbloqueadoControles)
                                {
                                    if (item is TextBox)
                                    {
                                        ((TextBox)item).ReadOnly = false;
                                    }

                                    if (item is RadioButtonList)
                                    {
                                        ((RadioButtonList)item).Enabled = true;
                                    }
                                    if (item is Button)
                                    {
                                        ((Button)item).Enabled = true;
                                    }
                                }
                                else
                                {
                                    if (item is TextBox)
                                    {
                                        ((TextBox)item).ReadOnly = true;
                                    }

                                    if (item is RadioButtonList)
                                    {
                                        ((RadioButtonList)item).Enabled = false;
                                    }

                                    if (item is Button)
                                    {
                                        ((Button)item).Enabled = false;
                                    }
                                }

                            }
                        }
                        #endregion

                        #region html generics controls
                        if (htmlControls != null)
                        {
                            foreach (var item in htmlControls)
                            {
                                if (desbloqueadoControles)
                                {
                                    ((HtmlGenericControl)item).Visible = true;
                                }
                                else
                                {
                                    ((HtmlGenericControl)item).Visible = false;
                                }
                            }
                        }
                        #endregion

                        return desbloqueadoControles;
                    }
                }
                return false;
            }

            #endregion

            public static bool VerificaPermissaoExercicioFundoMunicipalBlocoV(WebControl[] controles, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                        var desbloqueadoExercicio = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 70);

                        bool desbloqueadoControles = false;
                        if (desbloqueadoExercicio != null)
                        {
                            desbloqueadoControles = (desbloqueadoPlano && desbloqueadoExercicio.Desbloqueado.Value);
                        }

                        foreach (WebControl item in controles)
                        {
                            if (desbloqueadoControles)
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = false;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = true;
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Enabled = true;
                                }
                            }
                            else
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = true;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = false;
                                }

                                if (item is Button)
                                {
                                    ((Button)item).Enabled = false;
                                }
                            }

                        }
                        return desbloqueadoControles;
                    }
                }
                return false;
            }
            public static bool VerificaPermissaoReprogramacaoBotaoSalvar(WebControl controle, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                        var desbloqueadoExercicioRedeDireta = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => (x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1019));

                        var  desbloqueadoExercicioReprogramado = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => (x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1019));

                        bool desbloqueadoControles = false;

                        if (desbloqueadoExercicioRedeDireta != null && desbloqueadoExercicioReprogramado != null)
                        {
                            desbloqueadoControles = (desbloqueadoPlano && (desbloqueadoExercicioRedeDireta.Desbloqueado.Value || desbloqueadoExercicioReprogramado.Desbloqueado.Value));
                        }

                        ((Button)controle).Enabled = desbloqueadoControles;

                        return desbloqueadoControles;
                    }
                }
                return false;
            }

            #region Cronogramas
            public static bool VerificaPermissaoExercicioCrogramaDesembolsoPSBasicaBlocoV(WebControl[] controles, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);


                        var desbloqueadoExercicio = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 26);

                        bool desbloqueadoControles = false;

                        if (desbloqueadoExercicio != null)
                        {
                            desbloqueadoControles = (desbloqueadoPlano && desbloqueadoExercicio.Desbloqueado.Value);
                        }

                        foreach (WebControl item in controles)
                        {
                            if (desbloqueadoControles)
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = false;
                                    ((TextBox)item).Enabled = true;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = true;
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Enabled = true;
                                }
                            }
                            else
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = true;
                                    ((TextBox)item).Enabled = false;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = false;
                                }

                                if (item is Button)
                                {
                                    ((Button)item).Enabled = false;
                                }
                            }

                        }
                        return desbloqueadoControles;
                    }
                }
                return false;
            }
            public static bool VerificaPermissaoExercicioCrogramaDesembolsoPSBasicaBlocoVReprogramacao(WebControl[] controles, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);


                        var desbloqueadoExercicio = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1019);

                        bool desbloqueadoControles = false;
                        if (desbloqueadoExercicio != null)
                        {
                            desbloqueadoControles = (desbloqueadoPlano && desbloqueadoExercicio.Desbloqueado.Value);
                        }

                        foreach (WebControl item in controles)
                        {
                            if (desbloqueadoControles)
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = false;
                                    ((TextBox)item).Enabled = true;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = true;
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Enabled = true;
                                }
                            }
                            else
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = true;
                                    ((TextBox)item).Enabled = false;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = false;
                                }

                                if (item is Button)
                                {
                                    ((Button)item).Enabled = false;
                                }
                            }

                        }
                        return desbloqueadoControles;
                    }
                }
                return false;
            }
            public static bool VerificaPermissaoExercicioCrogramaDesembolsoPSEMediaComplexidadeBlocoV(WebControl[] controles, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);


                        var desbloqueadoExercicio = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1019);

                        bool desbloqueadoControles = false;

                        if (desbloqueadoExercicio != null)
                        {
                            desbloqueadoControles = (desbloqueadoPlano && desbloqueadoExercicio.Desbloqueado.Value);
                        }

                        foreach (WebControl item in controles)
                        {
                            if (desbloqueadoControles)
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = false;
                                    ((TextBox)item).Enabled = true;

                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = true;
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Enabled = true;
                                }
                            }
                            else
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = true;
                                    ((TextBox)item).Enabled = false;

                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = false;
                                }

                                if (item is Button)
                                {
                                    ((Button)item).Enabled = false;
                                }
                            }

                        }
                        return desbloqueadoControles;
                    }
                }
                return false;
            }
            public static bool VerificaPermissaoExercicioCrogramaDesembolsoPSEMediaComplexidadeVReprogramacao(WebControl[] controles, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);


                        var desbloqueadoExercicio = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1019);

                        bool desbloqueadoControles = false;

                        if (desbloqueadoExercicio != null)
                        {
                            desbloqueadoControles = (desbloqueadoPlano && desbloqueadoExercicio.Desbloqueado.Value);
                        }

                        foreach (WebControl item in controles)
                        {
                            if (desbloqueadoControles)
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = false;
                                    ((TextBox)item).Enabled = true;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = true;
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Enabled = true;
                                }
                            }
                            else
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = true;
                                    ((TextBox)item).Enabled = false;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = false;
                                }

                                if (item is Button)
                                {
                                    ((Button)item).Enabled = false;
                                }
                            }

                        }
                        return desbloqueadoControles;
                    }
                }
                return false;
            }
            public static bool VerificaPermissaoExercicioCrogramaDesembolsoPSEAltaComplexidadeBlocoV(WebControl[] controles, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);


                        var desbloqueadoExercicio = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 28);

                        bool desbloqueadoControles = false;

                        if (desbloqueadoExercicio != null)
                        {
                            desbloqueadoControles = (desbloqueadoPlano && desbloqueadoExercicio.Desbloqueado.Value);
                        }

                        foreach (WebControl item in controles)
                        {
                            if (desbloqueadoControles)
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = false;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = true;
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Enabled = true;
                                }
                            }
                            else
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = true;
                                    ((TextBox)item).Enabled = false;

                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = false;
                                }

                                if (item is Button)
                                {
                                    ((Button)item).Enabled = false;
                                }
                            }

                        }
                        return desbloqueadoControles;
                    }
                }
                return false;
            }
            public static bool VerificaPermissaoExercicioCrogramaDesembolsoBeneficiosEventuaisBlocoV(WebControl[] controles, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);


                        var desbloqueadoExercicio = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 30);

                        bool desbloqueadoControles = false;

                        if (desbloqueadoExercicio != null)
                        {
                            desbloqueadoControles = (desbloqueadoPlano && desbloqueadoExercicio.Desbloqueado.Value);
                        }

                        foreach (WebControl item in controles)
                        {
                            if (desbloqueadoControles)
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = false;
                                    ((TextBox)item).Enabled = true;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = true;
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Enabled = true;
                                }
                            }
                            else
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = true;
                                    ((TextBox)item).Enabled = false;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = false;
                                }

                                if (item is Button)
                                {
                                    ((Button)item).Enabled = false;
                                }
                            }

                        }
                        return desbloqueadoControles;
                    }
                }
                return false;
            }
            public static bool VerificaPermissaoExercicioCrogramaDesembolsoPSEAltaComplexidadeVReprogramacao(WebControl[] controles, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);


                        var desbloqueadoExercicio = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1019);

                        bool desbloqueadoControles = false;

                        if (desbloqueadoExercicio != null)
                        {
                            desbloqueadoControles = (desbloqueadoPlano && desbloqueadoExercicio.Desbloqueado.Value);
                        }

                        foreach (WebControl item in controles)
                        {
                            if (desbloqueadoControles)
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = false;
                                    ((TextBox)item).Enabled = true;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = true;
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Enabled = true;
                                }
                            }
                            else
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = true;
                                    ((TextBox)item).Enabled = false;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = false;
                                }

                                if (item is Button)
                                {
                                    ((Button)item).Enabled = false;
                                }
                            }

                        }
                        return desbloqueadoControles;
                    }
                }
                return false;
            }
            public static bool VerificaPermissaoExercicioCrogramaProgramasProjetosBlocoV(WebControl[] controles, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);


                        var desbloqueadoExercicio = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 29);

                        bool desbloqueadoControles = false;

                        if (desbloqueadoExercicio != null)
                        {
                            desbloqueadoControles = (desbloqueadoPlano && desbloqueadoExercicio.Desbloqueado.Value);
                        }

                        foreach (WebControl item in controles)
                        {
                            if (desbloqueadoControles)
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = false;
                                    ((TextBox)item).Enabled = true;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = true;
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Enabled = true;
                                }
                            }
                            else
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = true;
                                    ((TextBox)item).Enabled = false;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = false;
                                }

                                if (item is Button)
                                {
                                    ((Button)item).Enabled = false;
                                }
                            }

                        }
                        return desbloqueadoControles;
                    }
                }
                return false;
            }
            #endregion



        }
        #endregion

        #region Bloco 6
        public static class BlocoVI
        {
            public static bool VerificaPermissaoExercicioAcaoPlanejamentoBlocoVI(WebControl[] controles, bool desbloqueadoControles)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        foreach (WebControl item in controles)
                        {
                            if (desbloqueadoControles)
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = false;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = true;
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Enabled = true;
                                    ((Button)item).Visible = true;
                                }
                                if (item is DropDownList)
                                {
                                    ((DropDownList)item).Enabled = true;
                                }
                            }
                            else
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = true;
                                }

                                if (item is RadioButtonList)
                                {
                                    ((RadioButtonList)item).Enabled = false;
                                }

                                if (item is Button)
                                {
                                    ((Button)item).Enabled = false;
                                    ((Button)item).Visible = false;
                                }

                                if (item is DropDownList)
                                {
                                    ((DropDownList)item).Enabled = false;
                                }

                            }

                        }
                        return desbloqueadoControles;
                    }
                }
                return false;
            }
        }
        #endregion


    }
}
