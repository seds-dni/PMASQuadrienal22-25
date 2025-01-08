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

                            if (item is ImageButton)
                            {
                                ((ImageButton)item).Visible = false;

                            }

                            item.Enabled = false;
                        }
                    }
                }
                else
                {
                    foreach (WebControl item in controles)
                    {
                        if (item == null)
                            continue;

                        if (item is ImageButton)
                        {
                            ((ImageButton)item).Visible = true;

                        }

                        item.Enabled = true;
                    }
                }
            }
        }


        #region Controles
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
        #endregion

        

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


        public static bool VerificarPermissaoEspecial(WebControl[] controles)
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

                            }
                            
                            if (item is RadioButtonList)
                            {
                                ((RadioButtonList)item).Enabled = true;
                            }

                            item.Enabled = false;
                        }
                    }
                }
            }
            return desbloqueio;
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

            public static bool VerificaPermissaoExercicioBloco0DradsDemandas(int exercicioSolicitado, PrefeituraInfo prefeitura)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (prefeitura != null)
                    {
                        var desbloqueadoExercicio = prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1043);

                        if (desbloqueadoExercicio != null)
                        {
                            return desbloqueadoExercicio.Desbloqueado.Value;
                        }
                        else
                        {
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


            public static bool VerificaPermissaoExercicioBloco0InicioDemandas(WebControl[] controles, int exercicioSolicitado, PrefeituraInfo prefeitura)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (prefeitura != null)
                    {

                        var desbloqueadoExercicio = prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado &&  x.IdRefBloqueio == 1043);

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
            public static bool VerificaPermissaoEPendenciasExercicioBlocoI(int exercicioSolicitado)
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

                        return desbloqueadoControles;
                    }
                }
                return false;
            }

            public static bool VerificaPendenciasExercicioBlocoI(int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS)));

                        var desbloqueadoExercicio = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 10);

                        bool desbloqueadoControles = false;

                        if (desbloqueadoExercicio != null)
                        {
                            desbloqueadoControles = (desbloqueadoPlano && desbloqueadoExercicio.Desbloqueado.Value);
                        }

                        return desbloqueadoControles;
                    }
                }
                return false;
            }

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
                                    ((TextBox)item).Enabled = true;
                                    
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Visible = true;
                                    ((Button)item).Enabled = true;
                                }
                                if (item is CheckBox)
                                {
                                    ((CheckBox)item).Enabled = true;
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
                                    ((TextBox)item).Enabled = false;
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Visible = true;
                                    ((Button)item).Enabled = false;
                                }
                                if (item is CheckBox)
                                {
                                    ((CheckBox)item).Enabled = false;
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

        #region Bloco 3
        public static class BlocoIII
        {
           
            #region permissao: servico rede direta

            public static Int32 ObterExercicioDesbloqueado(int recurso)
            {
                var prefeiturasExerciciosBloqueio = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio.Where(x => x.IdRefBloqueio == recurso);
                if (prefeiturasExerciciosBloqueio != null)
                {
                    var prefeituraExercioBloqueio = prefeiturasExerciciosBloqueio.Where(x => x.Desbloqueado.Value).FirstOrDefault();
                    return (prefeituraExercioBloqueio == null) ? 0 : prefeituraExercioBloqueio.Exercicio;
                }
                else {
                    return 0;
                }

            }
                        
            public static bool ObterRedeDiretaExercicioSituacaoBloqueio(int exercicioSolicitado)
            {
                bool desbloqueadoExercicio = false;

                var exercicioSelecionado = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 19);
                if (exercicioSelecionado != null)
                {
                    desbloqueadoExercicio = exercicioSelecionado.Desbloqueado.Value;
                }

                return desbloqueadoExercicio;
            }


            public static bool ObterRedeDiretaDemandasExercicioSituacaoBloqueio(int exercicioSolicitado)
            {
                bool desbloqueadoExercicio = false;

                var exercicioSelecionado = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                    .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1041);

                if (exercicioSelecionado != null)
                {
                    desbloqueadoExercicio = exercicioSelecionado.Desbloqueado.Value;
                }

                return desbloqueadoExercicio;
            }



            public static bool ObterProgramasEProjetosExercicioSituacaoBloqueio(int exercicioSolicitado)
            {
                bool desbloqueadoExercicio = false;

                var exercicioSelecionado = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 23);
                if (exercicioSelecionado != null)
                {
                    desbloqueadoExercicio = exercicioSelecionado.Desbloqueado.Value;
                }

                return desbloqueadoExercicio;
            }

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

            //Demandas
            public static bool VerificaPermissaoRedeDiretaBlocoIIIDemandas(WebControl[] controles, int exercicioSolicitado) 
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
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1041);
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

                        
                        var exercicioEscolhidoRedeDireta = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => (x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 19));
                        var exercicioEscolhidoRedeDiretaReprogramado = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => (x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1019));

                        var exercicioEscolhidoRedeDiretaDemandas = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => (x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1041));

                        bool desbloqueadoExercicioRedeDireta = false;
                        bool desbloqueadoExercicioReprogramado = false;
                        bool desbloqueadoExercicioDemandas = false;


                        if (exercicioEscolhidoRedeDireta != null)
                        {
                            desbloqueadoExercicioRedeDireta = exercicioEscolhidoRedeDireta.Desbloqueado.Value;
                        }

                        if (exercicioEscolhidoRedeDiretaReprogramado != null)
                        {
                            desbloqueadoExercicioReprogramado = exercicioEscolhidoRedeDiretaReprogramado.Desbloqueado.Value;
                        }

                        if (exercicioEscolhidoRedeDiretaDemandas != null)
                        {
                            desbloqueadoExercicioDemandas = exercicioEscolhidoRedeDiretaDemandas.Desbloqueado.Value;
                        }

                        bool desbloqueadoControles = (desbloqueadoPlano && (desbloqueadoExercicioRedeDireta || desbloqueadoExercicioReprogramado || desbloqueadoExercicioDemandas));

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

            public static void VerificarPermissaoBlocoIIICMAS(WebControl[] controles)
            {

                bool bloqueio = false;
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {

                        bloqueio = (SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.EmAnalisedoCMAS) && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS);

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

                            if (item is DropDownList)
                            {
                                ((DropDownList)item).Enabled = !bloqueio;
                                item.Enabled = bloqueio;
                                continue;
                            }

                            item.Enabled = bloqueio;
                        }
                    }
                }
            } 

            public static bool ObterRedeIndiretaExercicioSituacaoBloqueio(int exercicioSolicitado)
            {
                bool desbloqueadoExercicio = false;

                var exercicioSelecionado = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 20);
                if (exercicioSelecionado != null)
                {
                    desbloqueadoExercicio = exercicioSelecionado.Desbloqueado.Value;
                }

                return desbloqueadoExercicio;
            }

            public static bool ObterRedeIndiretaDemandasExercicioSituacaoBloqueio(int exercicioSolicitado)
            {
                bool desbloqueadoExercicio = false;

                var exercicioSelecionado = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                    .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1042);

                if (exercicioSelecionado != null)
                {
                    desbloqueadoExercicio = exercicioSelecionado.Desbloqueado.Value;
                }

                return desbloqueadoExercicio;
            }


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

            public static bool VerificaPermissaoRedeIndiretaBlocoIIIDemandas(WebControl[] controles, int exercicioSolicitado) 
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
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1042);

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

                        var desbloqueadoExercicioRedeIndiretaPorExercicio = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => (x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 20));

                        var desbloqueadoExercicioReprogramadoSelecionado = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => (x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1020));


                        var desbloqueadoExercicioDemandasSelecionado = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => (x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1042));

                        bool desbloqueadoControles = false;
                        bool desbloqueadoExercicioReprogramado = false;
                        bool desbloqueadoExercicioRedeIndireta = false;
                        bool desbloqueadoExercicioDemandas = false;


                        if (desbloqueadoExercicioRedeIndireta != null)
                        {
                            desbloqueadoExercicioRedeIndireta = desbloqueadoExercicioRedeIndiretaPorExercicio.Desbloqueado.Value;
                        }


                        if (desbloqueadoExercicioReprogramadoSelecionado != null)
                        {
                            desbloqueadoExercicioReprogramado = desbloqueadoExercicioReprogramadoSelecionado.Desbloqueado.Value;
                        }

                        if (desbloqueadoExercicioDemandas != null)
                        {
                            desbloqueadoExercicioDemandas = desbloqueadoExercicioDemandasSelecionado.Desbloqueado.Value;
                        }

                        desbloqueadoControles = (desbloqueadoPlano && (desbloqueadoExercicioRedeIndireta || desbloqueadoExercicioReprogramado || desbloqueadoExercicioDemandas));

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

                        bool desbloqueadoControles = false;

                        var desbloqueadoExercicio = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 23);

                        

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
                                    ((TextBox)item).Enabled = false;
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

            public static bool VerificaPermissaoExercicioProgramaProjetoBotaoSalvarBlocoIII(Button botaoSalvar)
            {
                bool Desbloqueia = false;

                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                        var exerciciosDesbloqueados = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .Where(x => x.IdRefBloqueio == 25);


                        foreach (var exercicioDesbloqueado in exerciciosDesbloqueados)
	                    {
                            if (exercicioDesbloqueado.Desbloqueado.Value == true && desbloqueadoPlano == true)
                            {
                                botaoSalvar.Enabled = true;
                                Desbloqueia = true;
                                break;
                            }
                            else 
                            {
                                botaoSalvar.Enabled = false;
                            }
	                    }
                    }
                }

                return Desbloqueia;
               
            }


            public static void VerificaPermissaoExercicioProgramaProjetoReprogramacaoBotaoSalvarBlocoIII(Button botaoSalvar)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                        var exerciciosDesbloqueados = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .Where(x => x.IdRefBloqueio == 1019);

                        foreach (var exercicioDesbloqueado in exerciciosDesbloqueados)
                        {
                            if (exercicioDesbloqueado.Desbloqueado.Value == true && desbloqueadoPlano == true)
                            {
                                botaoSalvar.Enabled = true;
                                break;
                            }
                            else
                            {
                                botaoSalvar.Enabled = false;
                            }
                        }
                    }
                }
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
            #endregion

            #region Lei orcamentaria
            public static bool VerificaPermissaoExercicioLOFLuxoAdministrativoBlocoV(WebControl[] controles, HtmlGenericControl[] htmlControls, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        List<PrefeituraExercicioBloqueioInfo> bloqueios = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio;
                        PrefeituraExercicioBloqueioInfo bloqueioDesbloqueio = bloqueios.SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 78);
                        bool desbloqueadoControles = false;
                        if (bloqueioDesbloqueio != null)
                        {
                            desbloqueadoControles = bloqueioDesbloqueio.Desbloqueado.Value;
                        }

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



            public static bool VerificaPermissaoExercicioLOFLuxoAdministrativoBlocoVQuadro(WebControl[] controles, HtmlGenericControl[] htmlControls, int bloqueioDesbloqueio)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {

                        bool desbloqueadoControles = false;
                        if (bloqueioDesbloqueio != 7)
                        {
                            desbloqueadoControles = true;
                        }
                        else
                        {
                            desbloqueadoControles = false;
                        }

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
            public static bool VerificaPermissaoReprogramacaoBotaoSalvarRedeDireta(WebControl controle, int exercicioSolicitado)
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

                        var  desbloqueadoExercicioReprogramado = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
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


            public static bool VerificaPermissaoDemandasBotaoSalvar(WebControl controle, int exercicioSolicitado)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        bool desbloqueadoPlano = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                           || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                           && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);


                        var desbloqueadoExercicioDemandasPublica = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => (x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1041));

                        var desbloqueadoExercicioDemandasPrivada = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .SingleOrDefault(x => (x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1042));

                        bool desbloqueadoControles = false;

                        if (desbloqueadoExercicioDemandasPublica != null || desbloqueadoExercicioDemandasPrivada != null)
                        {
                            desbloqueadoControles = (desbloqueadoPlano && (desbloqueadoExercicioDemandasPublica.Desbloqueado.Value || desbloqueadoExercicioDemandasPrivada.Desbloqueado.Value));
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

            public static bool VerificaPermissaoExercicioCrogramaDesembolsoPSBasicaBlocoVReprogramacaoPrivada(WebControl[] controles, int exercicioSolicitado)
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

            public static bool VerificaPermissaoExercicioCrogramaDesembolsoPSBasicaBlocoVDemandasPublicas(WebControl[] controles, int exercicioSolicitado)
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
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1041);

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

            public static bool VerificaPermissaoExercicioCrogramaDesembolsoPSBasicaBlocoVDemandasPrivadas(WebControl[] controles, int exercicioSolicitado)
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
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1042);

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
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 27);

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

            public static bool VerificaPermissaoExercicioCrogramaDesembolsoPSEMediaComplexidadeVDemandasPublicas(WebControl[] controles, int exercicioSolicitado)
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
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1041);

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

            public static bool VerificaPermissaoExercicioCrogramaDesembolsoPSEMediaComplexidadeVDemandasPrivadas(WebControl[] controles, int exercicioSolicitado)
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
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1042);

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

            public static bool VerificaPermissaoExercicioCrogramaDesembolsoPSEAltaComplexidadeVDemandasPublicas(WebControl[] controles, int exercicioSolicitado)
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
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1041);

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

            public static bool VerificaPermissaoExercicioCrogramaDesembolsoPSEAltaComplexidadeVDemandasPrivadas(WebControl[] controles, int exercicioSolicitado)
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
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1042);

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

            public static bool VerificaPermissaoExercicioCrogramaDesembolsoBeneficiosEventuaisBlocoVBtnSalvar(WebControl controles, int exercicioSolicitado)
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


                        var desbloqueadoPP = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                                                                .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 30);

                        var desbloqueadoDireta = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                                                                .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1019);

                        var desbloqueadoIndireta = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                                                                .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1020);

                        var desbloqueadoDrads = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                                                                .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1040);

                        if (desbloqueadoPP.Desbloqueado.Value == true || desbloqueadoDireta.Desbloqueado.Value == true || desbloqueadoIndireta.Desbloqueado.Value == true || desbloqueadoDrads.Desbloqueado.Value == true)
                        {
                            desbloqueadoExercicio = true;
                        }

                        bool desbloqueadoControles = false;

                        if (desbloqueadoExercicio != null)
                        {
                            desbloqueadoControles = (desbloqueadoPlano && desbloqueadoExercicio);
                        }

                        ((Button)controles).Enabled = desbloqueadoControles;

                        return desbloqueadoControles;
                    }
                }
                return false;
            }


            public static bool VerificaPermissaoExercicioCrogramaDesembolsoBeneficiosEventuaisBlocoVReprogramacao(WebControl[] controles, int exercicioSolicitado)
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

            public static bool VerificaPermissaoExercicioCrogramaDesembolsoBeneficiosEventuaisBlocoVReprogramacaoPrivada(WebControl[] controles, int exercicioSolicitado)
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


            public static bool VerificaPermissaoExercicioCrogramaProgramasProjetosBlocoVBtnSalvar(WebControl controles, int exercicioSolicitado)
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


                        var desbloqueadoPP = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                                                                .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 29);

                        var desbloqueadoDireta = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                                                                .SingleOrDefault(x => x.Exercicio == exercicioSolicitado &&  x.IdRefBloqueio == 1019);
                        
                        var desbloqueadoIndireta = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                                                                .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1020);

                        var desbloqueadoDrads = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                                                                .SingleOrDefault(x => x.Exercicio == exercicioSolicitado &&  x.IdRefBloqueio == 1040);

                        if (desbloqueadoPP.Desbloqueado.Value == true || desbloqueadoDireta.Desbloqueado.Value == true || desbloqueadoIndireta.Desbloqueado.Value == true || desbloqueadoDrads.Desbloqueado.Value == true)
                        {
                            desbloqueadoExercicio = true;
                        }

                        bool desbloqueadoControles = false;

                        if (desbloqueadoExercicio != null)
                        {
                            desbloqueadoControles = (desbloqueadoPlano && desbloqueadoExercicio);
                        }

                        ((Button)controles).Enabled = desbloqueadoControles;

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

            public static bool VerificaPermissaoRedeDiretaBlocoVIReprogramacao(WebControl[] controles, int exercicioSolicitado)
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
                            if (item is CheckBox)
                            {
                                ((CheckBox)item).Enabled = desbloqueadoControles;
                            }
                            if (item is DropDownList)
                            {
                                ((DropDownList)item).Enabled = desbloqueadoControles;
                            }
                        }
                        return desbloqueadoControles;
                    }
                }
                return false;
            }

            public static bool VerificaPermissaoRecursosAcaoPlanejamentoBlocoVI(WebControl[] controles, int exercicioSolicitado)
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
                            .SingleOrDefault(x => x.Exercicio == exercicioSolicitado && x.IdRefBloqueio == 1044);

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
                                if (item is Button)
                                {
                                    ((Button)item).Visible = true;
                                    ((Button)item).Enabled = true;
                                }
                                if (item is CheckBox)
                                {
                                    ((CheckBox)item).Enabled = true;
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
                                    ((TextBox)item).Enabled = false;
                                }
                                if (item is Button)
                                {
                                    ((Button)item).Visible = true;
                                    ((Button)item).Enabled = false;
                                }
                                if (item is CheckBox)
                                {
                                    ((CheckBox)item).Enabled = false;
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

            public static bool VerificarPermissaoSituacaoComentario(WebControl[] controles)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {

                        var desbloqueado = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                        || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                        || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                        && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);


                        foreach (WebControl item in controles)
                        {
                            if (desbloqueado)
                            {
                                if (item is TextBox)
                                {
                                    ((TextBox)item).ReadOnly = false;
                                }

                                if (item is RadioButton)
                                {
                                    ((RadioButton)item).Enabled = true;
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

                                if (item is RadioButton)
                                {
                                    ((RadioButton)item).Enabled = false;
                                }

                                if (item is Button)
                                {
                                    ((Button)item).Enabled = false;
                                    ((Button)item).Visible = false;
                                }
                            }

                        }
                        return desbloqueado;
                    }
                }
                return false;
            }
            public static bool VerificarPermissaoSituacaoComentarioAcaoEditar(WebControl controle)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {
                        var desbloqueado = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                        || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                        || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                        && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                        ((Panel)controle).Enabled = desbloqueado;
                        ((Panel)controle).Visible = true;
                        return desbloqueado;
                    }
                }
                return false;
            }
            public static bool VerificarPermissaoSituacaoComentarioAcaoExibir(WebControl controle)
            {
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {

                        var desbloqueado = ((SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.Desbloqueado)
                        || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidoDrads)
                        || SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.DevolvidopeloCMAS))
                        && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                        ((Panel)controle).Enabled = !desbloqueado;
                        ((Panel)controle).Visible = !desbloqueado;

                        return desbloqueado;
                    }
                }
                return false;
            }
        }
        #endregion

        #region Bloco 8
        public static class BlocoVIII
        {
            public static void VerificarPermissaoCMASParecer(WebControl[] controles)
            {

                bool bloqueio = false;
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {

                       /* bloqueio = (SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.EmAnalisedoCMAS) /*&& SessaoPmas.UsuarioLogado.Prefeitura.Revisao == 0*/
                           /* && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS);*/
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
            public static void VerificarPermissaoCMAS(WebControl[] controles)
            {

                bool bloqueio = false;
                if (SessaoPmas.UsuarioLogado != null)
                {
                    if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    {

                        bloqueio = (SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.EmAnalisedoCMAS) && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS);

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
        }

        #endregion
    }
}
