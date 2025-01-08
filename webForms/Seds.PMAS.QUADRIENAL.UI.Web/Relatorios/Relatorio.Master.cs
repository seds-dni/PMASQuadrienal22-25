using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.UI.Web.Relatorios
{
    public partial class Relatorio : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public String Titulo
        {
            get { return litTitulo.Text; }
            set { litTitulo.Text = value; }
        }

        public String Filtros
        {
            get { return litFiltros.Text; }
            set { litFiltros.Text = value; }
        }

        public String WidthRelatorio
        {
            get { return tbRelatorio.Width; }
            set { tbRelatorio.Width = value;  }
        }

        public String MainWidthRelatorio
        {
            get { return FormMain.Attributes.Keys.ToString(); }   
            set { FormMain.Attributes.CssStyle.Add("Width", value ); }
        }

        public Button GerarExcel
        {
            get { return btnGerarExcel; }
        }

        public Button GeraXLSX 
        {
            get { return btnGeraXLSX;}
            
        }

        public HtmlTable Report { get { return tbRelatorio; } }


        public string filtroEstado(Boolean? estado)
        {
            return estado.HasValue && estado.Value ? "Estado." : "";
        }


        public String filtroDRADS(List<int> ids)
        {
            var filtro = new StringBuilder();
            if (ids != null && ids.Count > 0)
            {
                var drads = ProxyDivisaoAdministrativa.Drads.Where(t => ids.Any(i => t.Id == i));
                filtro.Append(" DRADS: ");
                drads.OrderBy(d => d.Nome).ToList().ForEach(d => filtro.Append(d.Nome + ","));
                filtro.Remove(filtro.Length - 1, 1);
                filtro.Append(".");
            }
            return filtro.ToString();
        }

        public String filtroMunicipios(List<int> ids)
        {
            var filtro = new StringBuilder();
            if (ids != null && ids.Count > 0)
            {
                var municipios = ProxyDivisaoAdministrativa.MunicipiosEstaduais.Where(t => ids.Any(i => t.Id == i));
                filtro.Append(" Municípios: ");
                if (ids.Count == 645)
                    filtro.Append("Todos");
                else
                {
                    municipios.OrderBy(d => d.Nome).ToList().ForEach(d => filtro.Append(d.Nome + ","));
                    filtro.Remove(filtro.Length - 1, 1);
                }
                filtro.Append(".");
            }
            return filtro.ToString();
        }

        public String filtroMunicipio(int id)
        {
            var filtro = new StringBuilder();
            if (id > 0)
            {
                var municipio = ProxyDivisaoAdministrativa.MunicipiosEstaduais.Where(i => i.Id == id);
                filtro.Append(" Município: ");
                municipio.OrderBy(d => d.Nome).ToList().ForEach(d => filtro.Append(d.Nome));
                filtro.Append(".");
            }
            return filtro.ToString();
        }
        public String filtroDRADSId(int id)
        {
            var filtro = new StringBuilder();
            var drads = ProxyDivisaoAdministrativa.Drads.Where(t => t.Id == id);
            filtro.Append(" DRADS: ");
            drads.OrderBy(d => d.Nome).ToList().ForEach(d => filtro.Append(d.Nome + ","));
            filtro.Remove(filtro.Length - 1, 1);
            filtro.Append(".");

            return filtro.ToString();
        }
        public String filtroRegioes(List<int> ids)
        {
            var filtro = new StringBuilder();
            if (ids != null && ids.Count > 0)
            {
                using (var proxy = new ProxyDivisaoAdministrativa())
                {
                    var regioes = proxy.Service.GetRegioesMetropolitanasByIds(ids);
                    filtro.Append(" Regiões Metropolitanas: ");
                    regioes.OrderBy(d => d.Nome).ToList().ForEach(d => filtro.Append(d.Nome + ","));
                    filtro.Remove(filtro.Length - 1, 1);
                    filtro.Append(".");
                }
            }
            return filtro.ToString();
        }

        public String filtroMacroRegioes(List<int> ids)
        {
            var filtro = new StringBuilder();
            if (ids != null && ids.Count > 0)
            {
                using (var proxy = new ProxyDivisaoAdministrativa())
                {
                    var regioes = proxy.Service.GetMacroRegioesByIds(ids.ToArray());
                    filtro.Append(" Macrorregiões: ");
                    regioes.ToList().ForEach(d => filtro.Append(d.Nome + ","));
                    filtro.Remove(filtro.Length - 1, 1);
                    filtro.Append(".");
                }
            }
            return filtro.ToString();
        }

        public String filtroNiveisGestoes(List<int> ids)
        {
            var filtro = new StringBuilder();
            if (ids != null && ids.Count > 0)
            {
                using (var proxy = new ProxyEstruturaAssistenciaSocial())
                {
                    var gestoes = proxy.GetNivelGestao().Where(t => ids.Any(i => i == t.Id));
                    filtro.Append(" Níveis de Gestão: ");
                    gestoes.ToList().ForEach(d => filtro.Append(d.Nome + ","));
                    filtro.Remove(filtro.Length - 1, 1);
                    filtro.Append(".");
                }
            }
            return filtro.ToString();
        }

        public String filtroPublicoAlvo(int? id)
        {
            var filtro = new StringBuilder();
            if (id.HasValue)
            {
                filtro.Append(" Usuários: ");
                using (var proxy = new ProxyEstruturaAssistenciaSocial())
                {
                    var publicoAlvo = proxy.Service.GetUsuarioById(id.Value);
                    filtro.Append(publicoAlvo.Nome);
                    filtro.Append(".");
                }
            }

            return filtro.ToString();
        }

        public String filtroUnidade(int? id)
        {

            var filtro = new StringBuilder();
            if (id.HasValue && id > 0)
            {
                filtro.Append(" Tipo de unidade: ");
                if (id == 1)
                {
                    filtro.Append("Unidades publicas");
                }
                else
                {
                    filtro.Append("Organizações da Sociedade Civil");
                }
                filtro.Append(".");
            }
            return filtro.ToString();
        }

        public String filtroSexo(int? id)
        {
            var filtro = new StringBuilder();
            if (id.HasValue)
            {
                filtro.Append(" Sexo: ");
                using (var proxy = new ProxyEstruturaAssistenciaSocial())
                {
                    var sexo = proxy.GetSexo().FirstOrDefault(p => p.Id == id);
                    filtro.Append(sexo.Nome);
                    filtro.Append(".");
                }
            }
            return filtro.ToString();
        }

        public String filtroRegiaoMoradia(int? id)
        {
            var filtro = new StringBuilder();
            if (id.HasValue)
            {
                using (var proxy = new ProxyEstruturaAssistenciaSocial())
                {
                    filtro.Append(" Região de moradia: ");
                    var regiaoMoradia = proxy.GetRegioesMoradia().FirstOrDefault(p => p.Id == id);
                    filtro.Append(regiaoMoradia.Nome);
                    filtro.Append(".");
                }
            }
            return filtro.ToString();
        }

        public String filtroCaracteristicasTerritorio(int? id)
        {
            var filtro = new StringBuilder();
            if (id.HasValue)
            {
                filtro.Append(" Características ligadas ao território: ");
                using (var proxy = new ProxyEstruturaAssistenciaSocial())
                {
                    var caracteristicasTerritorio = proxy.GetCaracteristicasTerritorio().FirstOrDefault(p => p.Id == id.Value);
                    filtro.Append(caracteristicasTerritorio.Nome);
                    filtro.Append(".");
                }
            }
            return filtro.ToString();
        }

        public String filtroProtecaoSocial(int? id)
        {
            var filtro = new StringBuilder();
            if (id.HasValue)
            {
                filtro.Append(" Proteção Social: ");
                using (var proxy = new ProxyEstruturaAssistenciaSocial())
                {
                    var protecao = proxy.Service.GetTiposProtecaoSocial().FirstOrDefault(p => p.Id == id);
                    filtro.Append(protecao.Nome);
                    filtro.Append(".");
                }
            }

            return filtro.ToString();
        }

        public String filtroTipoBeneficioEventual(int? id)
        {
            var filtro = new StringBuilder();
            if (id.HasValue && id.Value != 0)
            {
                filtro.Append(" Tipo de Benefício: ");
                using (var proxy = new ProxyEstruturaAssistenciaSocial())
                {
                    var tipoBeneficio = proxy.Service.GetTiposBeneficiosEventuais().FirstOrDefault(p => p.Id == id);
                    filtro.Append(tipoBeneficio.Nome);
                    filtro.Append(".");
                }
            }

            return filtro.ToString();
        }

        public String filtroTipoConselho(int? id)
        {
            var filtro = new StringBuilder();
            if (id.HasValue)
            {
                filtro.Append(" Tipo de Conselho: ");
                if (id.Value == 10)
                {

                    filtro.Append("CMAS - Conselho Municipal de Assistência Social");
                    filtro.Append(".");
                    return filtro.ToString();
                }

                using (var proxy = new ProxyEstruturaAssistenciaSocial())
                {
                    var tipo = proxy.Service.GetTiposConselhos().FirstOrDefault(p => p.Id == id);
                    filtro.Append(tipo.Nome);
                    filtro.Append(".");
                }
            }

            return filtro.ToString();
        }

        public String filtroProblemaSocial(int? id)
        {
            var filtro = new StringBuilder();
            if (id.HasValue)
            {
                filtro.Append(" Situação de Vulnerabilidade ou risco: ");
                using (var proxy = new ProxyEstruturaAssistenciaSocial())
                {
                    var problema = proxy.Service.GetSituacoesVulnerabilidade().FirstOrDefault(t => t.Id == id);
                    filtro.Append(problema.Nome);
                    filtro.Append(".");
                }
            }

            return filtro.ToString();
        }

        public String filtroProblemaSocialCategoria(int? id)
        {
            var filtro = new StringBuilder();
            if (id.HasValue)
            {
                filtro.Append(" Situação específica: ");
                using (var proxy = new ProxyEstruturaAssistenciaSocial())
                {
                    var problemaCategoria = proxy.Service.GetSituacoesEspecificas().FirstOrDefault(t => t.Id == id);
                    filtro.Append(problemaCategoria.Nome);
                    filtro.Append(".");
                }
            }

            return filtro.ToString();
        }

        public String filtroSituacaoVulnerabilidadeCondicao(String condicao)
        {
            var filtro = new StringBuilder();
            if (!String.IsNullOrEmpty(condicao))
            {
                filtro.Append(" Condição: " + condicao);
                filtro.Append(".");
            }

            return filtro.ToString();
        }

        public String filtroTipoFinanciamento(int? tipoFinanciamento)
        {
            var filtro = new StringBuilder();

            if (tipoFinanciamento == 1)
            {
                filtro.Append(" Financiamento: FEAS");
            }
            return filtro.ToString();
        }

        public String filtroSituacoesVulnerabilidade(List<int> ids)
        {
            var filtro = new StringBuilder();
            if (ids != null && ids.Count > 0)
            {
                filtro.Append(" Situação de Vulnerabilidade ou risco: ");
                using (var proxy = new ProxyEstruturaAssistenciaSocial())
                {
                    foreach (var sv in proxy.Service.GetSituacoesVulnerabilidade().Where(t => ids.Contains(t.Id)))
                    {
                        filtro.Append(sv.Nome + ",");
                    }

                    filtro.Remove(filtro.Length - 1, 1);
                    filtro.Append(".");
                }
            }

            return filtro.ToString();
        }

        public String filtroSituacoesEspecificas(List<int> ids)
        {
            var filtro = new StringBuilder();
            if (ids != null && ids.Count > 0)
            {
                filtro.Append(" Situação específica: ");
                using (var proxy = new ProxyEstruturaAssistenciaSocial())
                {
                    foreach (var se in proxy.Service.GetSituacoesEspecificas().Where(t => ids.Contains(t.Id)))
                    {
                        filtro.Append(se.Nome + ",");
                    }

                    filtro.Remove(filtro.Length - 1, 1);
                    filtro.Append(".");
                }
            }

            return filtro.ToString();
        }

        public String filtroTipoServico(int? id)
        {
            var filtro = new StringBuilder();
            if (id.HasValue)
            {
                filtro.Append(" Tipo de Serviço: ");
                using (var proxy = new ProxyEstruturaAssistenciaSocial())
                {
                    var servico = proxy.Service.GetTiposServicoById(id.Value);
                    filtro.Append(servico.Nome);
                    if (servico.Id == 138 || servico.Id == 145)
                        filtro.Append("");
                    else
                        filtro.Append(".");
                }
            }

            return filtro.ToString();
        }

        public String filtroServicoSubtificado(int? id)
        {
            var filtro = new StringBuilder();
            if (id.HasValue)
            {
                filtro.Append(" - ");
                using (var proxy = new ProxyEstruturaAssistenciaSocial())
                {
                    var servico = proxy.Service.GetTiposServicoById(id.Value);
                    filtro.Append(servico.Nome);
                    filtro.Append(".");
                }
            }
            return filtro.ToString();
        }

        public String filtroExecutora(List<ETipoUnidade> ids)
        {
            var filtro = new StringBuilder();
            if (ids != null && ids.Count > 0)
            {
                // filtro.Append(" Tipo de Unidade: ");
                filtro.Append(" Locais de execução selecionados: ");
                ids.ToList().ToList().ForEach(d => filtro.Append(executora((ETipoUnidade)d) + ","));
                filtro.Remove(filtro.Length - 1, 1);
                filtro.Append(".");
            }
            return filtro.ToString();
        }

        public String filtroAbrangenciaPrograma(List<String> abrangencias)
        {
            var filtro = new StringBuilder();
            if (abrangencias != null && abrangencias.Count > 0)
            {
                filtro.Append(" Abrangência do Programa/projeto: ");
                abrangencias.ToList().ToList().ForEach(d => filtro.Append(d + ","));
                filtro.Remove(filtro.Length - 1, 1);
                filtro.Append(".");
            }
            return filtro.ToString();
        }

        public String filtroTipoAbrangenciaPrograma(List<int> ids)
        {
            var filtro = new StringBuilder();
            if (ids != null && ids.Count > 0)
            {
                filtro.Append(" Tipo de Programa: ");
                ids.ToList().ToList().ForEach(d =>
                {
                    var abrangenciaPrograma = "";
                    switch (d)
                    {

                        case 1: abrangenciaPrograma = "BPC - Idoso"; break;
                        case 2: abrangenciaPrograma = "BPC - PCD"; break;
                        case 3: abrangenciaPrograma = "Bolsa Família"; break;

                    }
                    filtro.Append(abrangenciaPrograma + ",");
                });
                filtro.Remove(filtro.Length - 1, 1);
                filtro.Append(".");
            }
            return filtro.ToString();
        }


        public String filtroPortes(List<int> ids)
        {
            var filtro = new StringBuilder();
            if (ids != null && ids.Count > 0)
            {
                filtro.Append(" Portes: ");
                ids.ToList().ToList().ForEach(d => filtro.Append(porte((EPorteMunicipio)d) + ","));
                filtro.Remove(filtro.Length - 1, 1);
                filtro.Append(".");
            }
            return filtro.ToString();
        }

        public String filtroTotalCronogramas(int? totalCronograma)
        {
            var filtro = new StringBuilder();

            if (totalCronograma == 1)
            {
                filtro.Append("Todos os tipos de Proteção");
            }
            return filtro.ToString();
        }

        public String filtroFormaAtuacao(List<int> ids)
        {
            var filtro = new StringBuilder();
            if (ids != null && ids.Count > 0)
            {
                filtro.Append(" Forma de Atuação: ");
                using (var proxy = new ProxyEstruturaAssistenciaSocial())
                {
                     foreach (var formaAtuacao in proxy.Service.GetFormaAtuacao().Where(t => ids.Contains(t.Id)))
                    {
                        filtro.Append(formaAtuacao.Nome + ",");
                    }
                     filtro.Remove(filtro.Length - 1, 1);
                     filtro.Append(".");
                }
            }
            return filtro.ToString();
        }

        public String filtroTipoPrograma(List<int> ids)
        {
            var filtro = new StringBuilder();
            if (ids != null && ids.Count > 0)
            {
                filtro.Append(" Tipo de Programa: ");
                ids.ToList().ToList().ForEach(d =>
                {
                    var programa = "";
                    switch (d)
                    {
                        case 5: programa = "Ação Jovem"; break;
                        case 6: programa = "Renda Cidadã"; break;
                        case 1: programa = "BPC - Idoso"; break;
                        case 2: programa = "BPC - PCD"; break;
                        case 3: programa = "Bolsa Família"; break;
                        case 4: programa = "PETI"; break;
                        case 7: programa = "Pro-Jovem Adolescente"; break;
                        case 8: programa = "Outros"; break;
                    }
                    filtro.Append(programa + ",");
                });
                filtro.Remove(filtro.Length - 1, 1);
                filtro.Append(".");
            }
            return filtro.ToString();
        }

        public String filtroAbrangenciaServico(int id)
        {
            var filtro = new StringBuilder();
            if (id == 1 || id == 2)
                filtro.Append(" Abrangência: Intermunicipal.");
            if (id == 3)
                filtro.Append(" Abrangência: Municipal.");
            return filtro.ToString();
        }

        public String filtroExercicio(Int32 Exercicio)
        {
            var filtro = new StringBuilder();
            if (Exercicio != null && Exercicio != 0)
            {
                filtro.Append(String.Format(" Exercicio {0} ", Exercicio));
            }
            return filtro.ToString();
        }


        public void mostrarFiltrosDescritivo(RelatorioFiltroInfo relatorio)
        {
            var filtro = new StringBuilder();
            filtro.Append(filtroEstado(relatorio.Estado));
            filtro.Append(filtroDRADS(relatorio.DrdIDs));
            //filtro.Append(filtroDRADSId(relatorio.IdDrad));
            filtro.Append(filtroMunicipios(relatorio.MunIDs));
            filtro.Append(filtroRegioes(relatorio.RegIDs));
            filtro.Append(filtroMacroRegioes(relatorio.MacroRegiaoIDs));
            filtro.Append(filtroNiveisGestoes(relatorio.NiveisGestao));
            filtro.Append(filtroPortes(relatorio.Portes));
            filtro.Append(filtroProtecaoSocial(relatorio.TipoProtecaoSocial));
            filtro.Append(filtroTipoServico(relatorio.TipoServico));
            filtro.Append(filtroServicoSubtificado(relatorio.ServicoSubtificado));
            filtro.Append(filtroPublicoAlvo(relatorio.Usuario));
            filtro.Append(filtroExecutora(relatorio.TipoExecutora));
            filtro.Append(filtroSexo(relatorio.Sexo));
            filtro.Append(filtroCaracteristicasTerritorio(relatorio.CaracteristicasTerritorio));
            filtro.Append(filtroRegiaoMoradia(relatorio.RegiaoMoradia));
            filtro.Append(filtroProblemaSocial(relatorio.SituacaoVulnerabilidade));
            filtro.Append(filtroProblemaSocialCategoria(relatorio.SituacaoEspecifica));
            filtro.Append(filtroTipoPrograma(relatorio.TipoProgramas));
            filtro.Append(filtroAbrangenciaServico(relatorio.Abrangencias != null && relatorio.Abrangencias.Count > 0 ? relatorio.Abrangencias.First() : 0));
            filtro.Append(filtroTipoBeneficioEventual(relatorio.TipoBeneficioEventual));
            filtro.Append(filtroSituacoesVulnerabilidade(relatorio.SituacoesVulnerabilidade));
            filtro.Append(filtroSituacoesEspecificas(relatorio.SituacoesEspecificas));
            filtro.Append(filtroSituacaoVulnerabilidadeCondicao(relatorio.SituacoesVulnerabilidade != null && relatorio.SituacoesVulnerabilidade.Count > 1 ? relatorio.SituacaoVulnerabilidadeCondicao : ""));
            filtro.Append(filtroTipoFinanciamento(relatorio.TipoFinanciamento));
            filtro.Append(filtroTotalCronogramas(relatorio.TotalCronogramas));
            filtro.Append(filtroUnidade(relatorio.TipoUnidade));
            filtro.Append(filtroFormaAtuacao(relatorio.FormasAtuacoes));
            filtro.Append(filtroAbrangenciaPrograma(relatorio.AbrangenciasProgramas));
            filtro.Append(filtroMunicipio(relatorio.IdMunicipio.HasValue ? relatorio.IdMunicipio.Value : 0));
            filtro.Append(filtroExercicio(relatorio.Exercicio.HasValue ? relatorio.Exercicio.Value :0));

            if (relatorio.DataImplantacao == null || relatorio.DataImplantacao != "")
            {
                lblDataReferencia.Visible = false;
            }
            else
            {
                lblDataReferencia.Visible = true; 
            }
            
            this.Filtros = String.IsNullOrEmpty(filtro.ToString()) ? "sem filtro" : replaceCaracteresEspeciais(filtro.ToString());
        }

        public void DataDeReferencia(string data) 
        {
            DateTime dt = Convert.ToDateTime(data.ToString());
            lblDataReferencia.Visible = true;
            lblData.Text = dt.ToString("dd/MM/yyyy");
        }

        public void mostrarFiltros(RelatorioFiltroInfo relatorio, ETipoRelatorio tipo)
        {
            switch (tipo)
            {
                case ETipoRelatorio.Cadastral: mostrarFiltrosCadastral(relatorio); break;
                case ETipoRelatorio.Descritivo: mostrarFiltrosDescritivo(relatorio); break;
                case ETipoRelatorio.Quantitativo: mostrarFiltrosQuantitativo(relatorio); break;
            }
        }

        public void mostrarFiltrosCadastral(RelatorioFiltroInfo relatorio)
        {
            var filtro = new StringBuilder();
            filtro.Append(filtroEstado(relatorio.Estado));
            filtro.Append(filtroDRADS(relatorio.DrdIDs));
            filtro.Append(filtroMunicipios(relatorio.MunIDs));
            filtro.Append(filtroRegioes(relatorio.RegIDs));
            filtro.Append(filtroMacroRegioes(relatorio.MacroRegiaoIDs));
            filtro.Append(filtroNiveisGestoes(relatorio.NiveisGestao));
            filtro.Append(filtroPortes(relatorio.Portes));
            filtro.Append(filtroProtecaoSocial(relatorio.TipoProtecaoSocial));
            filtro.Append(filtroTipoServico(relatorio.TipoServico));
            filtro.Append(filtroServicoSubtificado(relatorio.ServicoSubtificado));
            filtro.Append(filtroPublicoAlvo(relatorio.Usuario));
            filtro.Append(filtroExecutora(relatorio.TipoExecutora));
            filtro.Append(filtroTipoConselho(relatorio.TipoConselho));


            this.Filtros = String.IsNullOrEmpty(filtro.ToString()) ? "sem filtro" : replaceCaracteresEspeciais(filtro.ToString());
        }

        public void mostrarFiltrosQuantitativo(RelatorioFiltroInfo relatorio)
        {
            var filtro = new StringBuilder();
            filtro.Append(filtroEstado(relatorio.Estado));
            filtro.Append(filtroDRADS(relatorio.DrdIDs));
            filtro.Append(filtroMunicipios(relatorio.MunIDs));
            filtro.Append(filtroRegioes(relatorio.RegIDs));
            filtro.Append(filtroMacroRegioes(relatorio.MacroRegiaoIDs));
            filtro.Append(filtroNiveisGestoes(relatorio.NiveisGestao));
            filtro.Append(filtroPortes(relatorio.Portes));
            filtro.Append(filtroProblemaSocial(relatorio.SituacaoVulnerabilidade));
            filtro.Append(filtroProblemaSocialCategoria(relatorio.SituacaoEspecifica));
            this.Filtros = String.IsNullOrEmpty(filtro.ToString()) ? "sem filtro" : replaceCaracteresEspeciais(filtro.ToString());
        }



        String porte(EPorteMunicipio e)
        {
            switch (e)
            {
                case EPorteMunicipio.PequenoI: return "Pequeno I";
                case EPorteMunicipio.PequenoII: return "Pequeno II";
                case EPorteMunicipio.Medio: return "Médio";
                case EPorteMunicipio.Grande: return "Grande";
                case EPorteMunicipio.Metropole: return "Metrópole";
            }

            return "";
        }

        String executora(ETipoUnidade e)
        {
            switch (e)
            {
                case ETipoUnidade.Publica: return "Pública";
                case ETipoUnidade.Privada: return "Privada";
                case ETipoUnidade.CRAS: return "CRAS";
                case ETipoUnidade.CREAS: return "CREAS";
                case ETipoUnidade.CentroPOP: return "Centro POP";

            }
            return "";
        }

        //public String filtroTipoAbrangenciaPrograma(List<int> ids)
        //{
        //    var filtro = new StringBuilder();
        //    if (ids != null && ids.Count > 0)
        //    {
        //        filtro.Append(" Tipo de Programa: ");
        //        ids.ToList().ToList().ForEach(d =>
        //        {
        //            var abrangenciaPrograma = "";
        //            switch (d)
        //            {

        //                case 1: abrangenciaPrograma = "BPC - Idoso"; break;
        //                case 2: abrangenciaPrograma = "BPC - PCD"; break;
        //                case 3: abrangenciaPrograma = "Bolsa Família"; break;

        //            }
        //            filtro.Append(abrangenciaPrograma + ",");
        //        });
        //        filtro.Remove(filtro.Length - 1, 1);
        //        filtro.Append(".");
        //    }
        //    return filtro.ToString();
        //}

        public String replaceCaracteresEspeciais(String s)
        {
            var sb = new StringBuilder(s);
            sb = sb.Replace("á", "&#225;").Replace("ã", "&#227;").Replace("ÃƒÂ£", "&#227;").Replace("Ã", "&#195;").Replace("Á", "&#193;").Replace("â", "&#226;").Replace("Â", "&#194;").Replace("à", "&#224;").Replace("À", "&#192;");
            sb = sb.Replace("é", "&#233;").Replace("É", "&#201;").Replace("ê", "&#234;").Replace("Ê", "&#202;");
            sb = sb.Replace("í", "&#237;").Replace("Í", "&#205;");
            sb = sb.Replace("ó", "&#243;").Replace("õ", "&#245;").Replace("Ó", "&#211;").Replace("ô", "&#244;").Replace("Ô", "&#212;");
            sb = sb.Replace("ú", "&#250;").Replace("Ú", "&#218;").Replace("ÃƒÂº", "&#250;").Replace("ÃÆ’ÂÂº", "&#250");
            sb = sb.Replace("ç", "&#231;").Replace("Ç", "&#199;");
            sb = sb.Replace("ª", "&#170;");
            sb = sb.Replace("Âº", "&#186;");
            sb = sb.Replace("º", "&#186;");
            sb = sb.Replace("¼", "&#188;");
            return sb.ToString();
        }
    }
}