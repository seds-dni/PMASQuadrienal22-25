using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS2017.Entidades;
using Seds.PMAS2017.Negocio.Validadores;

namespace Seds.PMAS2017.Negocio
{
    public class Pendencia
    {
        public PendenciaInfo ValidarPlanoMunicipal(Int32 idPrefeitura)
        {
            var pendencia = new PendenciaInfo();
            pendencia = ValidarBlocoI(idPrefeitura, pendencia);

            pendencia = ValidarBlocoII(idPrefeitura, pendencia);

            pendencia = ValidarBlocoIII(idPrefeitura, pendencia);

            pendencia = ValidarBlocoIV(idPrefeitura, pendencia);

            pendencia = ValidarBlocoV(idPrefeitura, pendencia);

            pendencia = ValidarBlocoVI(idPrefeitura, pendencia);

            pendencia = ValidarBlocoVII(idPrefeitura, pendencia);                       

            return pendencia;
        }

        public Boolean PlanoMunicipalPossuiPendencia(Int32 idPrefeitura)
        {
            var pendencia = ValidarPlanoMunicipal(idPrefeitura);
            var ok = pendencia.InformacoesPrefeitura && pendencia.InformacoesPrefeito && pendencia.InformacoesOrgaoGestor && pendencia.InformacoesGestorMunicipal && pendencia.InformacoesFundoMunicipal && pendencia.InformacoesConselhosMunicipais
                && pendencia.RedeProtecaoSocialPublica && pendencia.RedeProtecaoSocialPrivada && pendencia.CRAS && pendencia.CREAS && pendencia.CentroPOP && pendencia.AnaliseDiagnostica
                && pendencia.ProgramasProjetos && pendencia.TransferenciaRenda && pendencia.BeneficiosEventuais
                && pendencia.AcoesPlanejadas
                && pendencia.CronogramaDesembolsoProtecaoAlta && pendencia.CronogramaDesembolsoProtecaoBasica && pendencia.CronogramaDesembolsoProtecaoMedia
                && pendencia.Monitoramento && pendencia.VigilanciaSocioAssistencial && pendencia.Avaliacao && pendencia.AspectosGerais
                && pendencia.ConselhoMunicipal && pendencia.ParecerConselhoMunicipal;
            return !ok;
        }

        public Boolean PlanoMunicipalPossuiPendenciaOrgaoGestor(Int32 idPrefeitura)
        {
            var pendencia = ValidarPlanoMunicipal(idPrefeitura);
            var ok = pendencia.InformacoesPrefeitura && pendencia.InformacoesPrefeito && pendencia.InformacoesOrgaoGestor && pendencia.InformacoesGestorMunicipal && pendencia.InformacoesFundoMunicipal && pendencia.InformacoesConselhosMunicipais
                && pendencia.RedeProtecaoSocialPublica && pendencia.RedeProtecaoSocialPrivada && pendencia.CRAS && pendencia.CREAS && pendencia.CentroPOP && pendencia.AnaliseDiagnostica
                && pendencia.ProgramasProjetos && pendencia.TransferenciaRenda && pendencia.BeneficiosEventuais
                && pendencia.AcoesPlanejadas
                && pendencia.CronogramaDesembolsoProtecaoAlta && pendencia.CronogramaDesembolsoProtecaoBasica && pendencia.CronogramaDesembolsoProtecaoMedia
                && pendencia.Monitoramento && pendencia.VigilanciaSocioAssistencial && pendencia.Avaliacao && pendencia.AspectosGerais;
            return !ok;
        } 
      
        public PendenciaInfo ValidarBlocoI(Int32 idPrefeitura, PendenciaInfo pendencia)
        {
            pendencia = pendencia ?? new PendenciaInfo();
            pendencia.Pendencias = pendencia.Pendencias ?? new List<string>();
            
            #region Prefeitura
            var prefeitura = new Prefeitura().GetById(idPrefeitura);
            pendencia.InformacoesPrefeitura = true;
            if (prefeitura == null)
            {
                pendencia.Pendencias.Add("I - Informações sobre a Prefeitura Municipal: Não existe Informações sobre a Prefeitura Municipal");
                pendencia.InformacoesPrefeitura = false;
            }
            else
            {
                try
                {
                    new ValidadorPrefeitura().Validar(prefeitura);
                }
                catch (Exception ex)
                {
                    pendencia.InformacoesPrefeitura = false;
                    foreach (var s in ex.Message.Split(new string[]{System.Environment.NewLine},StringSplitOptions.None))
                        pendencia.Pendencias.Add("I - Informações sobre a Prefeitura Municipal: " + s);
                }
            }
            #endregion

            #region Prefeito
            var prefeito = new Prefeito().GetByPrefeitura(idPrefeitura);
            pendencia.InformacoesPrefeito = true;
            if (prefeito == null)
            {
                pendencia.Pendencias.Add("I - Identificação do Prefeito: Não existe Cadastro na Identificação do Prefeito em Exercício");
                pendencia.InformacoesPrefeito = false;
            }
            else
            {
                try
                {
                    new ValidadorPrefeito().Validar(prefeito);
                }
                catch (Exception ex)
                {
                    pendencia.InformacoesPrefeito = false;
                    foreach (var s in ex.Message.Split(new string[]{System.Environment.NewLine},StringSplitOptions.None))
                        pendencia.Pendencias.Add("I - Identificação do Prefeito: " + s);
                }
            }
            #endregion

            #region Orgao Gestor
            var orgao = new OrgaoGestor().GetByPrefeitura(idPrefeitura);
            pendencia.InformacoesOrgaoGestor = true;

            if (orgao == null)
            {
                pendencia.Pendencias.Add("I - Informações sobre o Órgão Gestor: Não existe Cadastro nas Informações sobre o Órgão Gestor");
                pendencia.InformacoesOrgaoGestor = false;
            }
            else
            {
                try
                {
                    new ValidadorOrgaoGestor().Validar(orgao);
                }
                catch (Exception ex)
                {
                    pendencia.InformacoesOrgaoGestor = false;
                    foreach (var s in ex.Message.Split(new string[]{System.Environment.NewLine},StringSplitOptions.None))
                        pendencia.Pendencias.Add("I - Informações sobre o Órgão Gestor: " + s);
                }

            }
            #endregion

            #region Informações sobre o Gestor Municipal

            var gestormunicipal = new GestorMunicipal().GetByPrefeitura(idPrefeitura);
            pendencia.InformacoesGestorMunicipal = true;
            if (gestormunicipal == null)
            {
                pendencia.Pendencias.Add("I - Informações sobre o Gestor Municipal: Não existe Cadastro nas Informações sobre o Gestor Municipal");
                pendencia.InformacoesGestorMunicipal = true;
            }
            else
            {
               try
                {
                    new ValidadorGestorMunicipal().Validar(gestormunicipal);
                }
                catch (Exception ex)
                {
                    pendencia.InformacoesGestorMunicipal = false;
                    foreach (var s in ex.Message.Split(new string[]{System.Environment.NewLine},StringSplitOptions.None))
                        pendencia.Pendencias.Add("I - Informações sobre o Gestor Municipal: " + s);
                }
            }

            #endregion

            #region Informações sobre o Fundo Municipal

            var fmas = new FundoMunicipal().GetByPrefeitura(idPrefeitura);            
            pendencia.InformacoesFundoMunicipal = true;
            if (fmas == null)
            {
                pendencia.Pendencias.Add("I - Informações sobre o Fundo Municipal: Não existe Cadastro nas Informações sobre o Fundo Municipal");
                pendencia.InformacoesFundoMunicipal = false;
            }
            else
            {
                try
                {
                    new ValidadorGestorMunicipal().Validar(gestormunicipal);
                }
                catch (Exception ex)
                {
                    pendencia.InformacoesFundoMunicipal = false;
                    foreach (var s in ex.Message.Split(new string[]{System.Environment.NewLine},StringSplitOptions.None))
                        pendencia.Pendencias.Add("I - Informações sobre o Fundo Municipal: " + s);
                }
            }

            #endregion

            #region Informações sobre os Conselhos Municipais

            var conselhos = new ConselhoExistente().GetByPrefeitura(idPrefeitura).ToList();  
            if(conselhos.Count == 0)
            {
                pendencia.Pendencias.Add("I - Informações sobre os Conselhos existentes: Não existe Cadastro nas Informações sobre o Conselho Municipal");
                pendencia.InformacoesConselhosMunicipais = false;
            }
            else
            {
                foreach(var c in conselhos)
                {
                    try
                    {
                        new ValidadorConselhoExistente().Validar(c);
                    }
                    catch(Exception ex)
                    {
                         pendencia.InformacoesConselhosMunicipais = false;
                         foreach (var s in ex.Message.Split(new string[]{System.Environment.NewLine},StringSplitOptions.None))
                            pendencia.Pendencias.Add("I - Informações sobre os Conselhos existentes: " + s);
                    }
                }
            }           
            #endregion

            return pendencia;
        }

        public PendenciaInfo ValidarBlocoII(Int32 idPrefeitura, PendenciaInfo pendencia)
        {
            pendencia = pendencia ?? new PendenciaInfo();
            pendencia.Pendencias = pendencia.Pendencias ?? new List<string>();           

            #region Análise Diagnóstica

            var analise = new AnaliseDiagnostica().GetByPrefeitura(idPrefeitura).OrderBy(t=> t.Classificacao).ToList();
            pendencia.AnaliseDiagnostica = true;

            if (analise.Count == 0)
            {
                pendencia.Pendencias.Add("II - Análise Diagnóstica : Não existe Cadastro da Análise Diagnóstica");
                pendencia.AnaliseDiagnostica = false;    
            }
            else
            {
                var prefeitura = new Prefeitura().GetById(idPrefeitura);
                if(prefeitura != null)
                {
                    try
                    {
                        new ValidadorPrefeitura().ValidarCaracterizacao(prefeitura);
                    }
                    catch(Exception ex)
                    {
                         pendencia.AnaliseDiagnostica = false;
                         foreach (var s in ex.Message.Split(new string[]{System.Environment.NewLine},StringSplitOptions.None))
                            pendencia.Pendencias.Add("II - Análise Diagnóstica: " + s);
                    }
                }
                
                for(int i = 0; i< analise.Count;i++)
                {
                    if(analise[i].Classificacao != (i + 1))
                    {
                        pendencia.AnaliseDiagnostica = false;
                        pendencia.Pendencias.Add("II - Análise Diagnóstica : A sequência de classificação das vulnerabilidades estão incorretas");
                        break;
                    }
                }                
            }

            #endregion

            #region Unidade Pública
            var unidadesPublicas = new UnidadePublica().GetByPrefeitura(idPrefeitura);            
            pendencia.RedeProtecaoSocialPublica = true;
            foreach (var unidade in unidadesPublicas)
            {
                var locais = new LocalExecucaoPublico().GetByUnidade(unidade.Id);
                if(locais.Count() == 0)
                {
                    pendencia.Pendencias.Add("II - Rede Proteção Social Pública - Unidade Pública " + unidade.RazaoSocial + " não possui locais de execução");
                    pendencia.RedeProtecaoSocialPublica = false;
                    continue;
                }
                
                foreach(var local in locais)
                {
                   var servicos = new ServicoRecursoFinanceiroPublico().GetByLocalExecucao(local.Id);
                    if(servicos.Count() == 0)
                    {
                        pendencia.Pendencias.Add("II - Rede Proteção Social Pública - Unidade Pública " + unidade.RazaoSocial + ": Local de Execução "+ local.Nome +" não possui serviços e recursos financeiros cadastrados");
                        pendencia.RedeProtecaoSocialPublica = false;
                    }
                }
            }
            #endregion

            #region Unidade Privada
            var unidadesPrivadas = new UnidadePrivada().GetByPrefeitura(idPrefeitura);            
            pendencia.RedeProtecaoSocialPrivada = true;
            foreach (var unidade in unidadesPrivadas)
            {
                var locais = new LocalExecucaoPrivado().GetByUnidade(unidade.Id);
                if(locais.Count() == 0)
                {
                    pendencia.Pendencias.Add("II - Rede Proteção Social Privada - Unidade Privada " + unidade.RazaoSocial + " não possui locais de execução");
                    pendencia.RedeProtecaoSocialPrivada = false;
                    continue;
                }
                
                foreach(var local in locais)
                {
                   var servicos = new ServicoRecursoFinanceiroPrivado().GetByLocalExecucao(local.Id);
                    if(servicos.Count() == 0)
                    {
                        pendencia.Pendencias.Add("II - Rede Proteção Social Privada - Unidade Privada " + unidade.RazaoSocial + ": Local de Execução "+ local.Nome +" não possui serviços e recursos financeiros cadastrados");
                        pendencia.RedeProtecaoSocialPrivada = false;
                    }
                }
            }
            #endregion

            #region CRAS
            var cras = new CRAS().GetByPrefeitura(idPrefeitura);
            pendencia.CRAS = true;
            foreach (var unidade in cras)
            {                
                var servicos = new ServicoRecursoFinanceiroCRAS().GetByCRAS(unidade.Id);
                if (servicos.Count() == 0)
                {
                    pendencia.Pendencias.Add("II - Rede Proteção Social Pública - CRAS " + unidade.Nome + " não possui serviços e recursos financeiros cadastrados");
                    pendencia.CRAS = false;
                }                
            }
            #endregion

            #region CREAS
            var creas = new CREAS().GetByPrefeitura(idPrefeitura);
            pendencia.CREAS = true;
            foreach (var unidade in creas)
            {
                var servicos = new ServicoRecursoFinanceiroCREAS().GetByCREAS(unidade.Id);
                if (servicos.Count() == 0)
                {
                    pendencia.Pendencias.Add("II - Rede Proteção Social Pública - CREAS " + unidade.Nome + " não possui serviços e recursos financeiros cadastrados");
                    pendencia.CREAS = false;
                }
            }
            #endregion

            #region Centro POP
            var centroPOP = new CentroPOP().GetByPrefeitura(idPrefeitura);
            pendencia.CentroPOP = true;
            foreach (var unidade in centroPOP)
            {
                var servicos = new ServicoRecursoFinanceiroCentroPOP().GetByCentroPOP(unidade.Id);
                if (servicos.Count() == 0)
                {
                    pendencia.Pendencias.Add("II - Rede Proteção Social Pública - Centro POP " + unidade.Nome + " não possui serviços e recursos financeiros cadastrados");
                    pendencia.CentroPOP = false;
                }
            }
            #endregion
                                
            return pendencia;
        }

        public PendenciaInfo ValidarBlocoIII(Int32 idPrefeitura, PendenciaInfo pendencia)
        {
            pendencia = pendencia ?? new PendenciaInfo();
            pendencia.Pendencias = pendencia.Pendencias ?? new List<string>();
            
            #region Programas / Projetos

            var programas = new ProgramaProjeto().GetByPrefeitura(idPrefeitura);
            pendencia.ProgramasProjetos = true;
            if (programas.Count() == 0)
            {
                pendencia.Pendencias.Add("III : Não existe Cadastro de Programas / Projetos");
                pendencia.ProgramasProjetos = false;
            }
            else
            {
                foreach (var programa in programas)
                {
                    var servicos = new ProgramaProjetoCofinanciamento().GetByProgramaProjeto(programa.Id);
                    if (servicos.Count() == 0)
                    {
                        pendencia.Pendencias.Add("III - Programas/Projetos: " + programa.Nome + " não possui serviços e recursos financeiros vinculados");
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }
                }
            }            
            #endregion
            #region Transferência de Renda
            var transferenciasFederais = new TransferenciaRenda().GetConsultaProgramasFederaisByPrefeitura(idPrefeitura);
            pendencia.TransferenciaRenda = true;

            foreach (var t in transferenciasFederais)
            {
                if (t.Aderiu == 0)
                    continue;
                var servicos = new TransferenciaRendaCofinanciamento().GetByTransferenciaRenda(t.Id);
                foreach (var s in servicos)
                {
                    if (s.ServicosRecursosFinanceirosPrivado != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosPrivado.NumeroAtendidos)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPrivado.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : "+ usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                        pendencia.TransferenciaRenda = false;                            
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosPublico != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosPublico.NumeroAtendidos)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPublico.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                        pendencia.TransferenciaRenda = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosCRAS != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosCRAS.NumeroAtendidos)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCRAS.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                        pendencia.TransferenciaRenda = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosCREAS != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosCREAS.NumeroAtendidos)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPrivado.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                        pendencia.TransferenciaRenda = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosCentroPOP != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosCentroPOP.NumeroAtendidos)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCentroPOP.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                        pendencia.TransferenciaRenda = false;
                        continue;
                    }
                }
            }

            var transferenciasEstaduais = new TransferenciaRenda().GetConsultaProgramasEstaduaisByPrefeitura(idPrefeitura);
            foreach (var t in transferenciasEstaduais)
            {
                if (t.Aderiu == 0)
                    continue;
                var servicos = new TransferenciaRendaCofinanciamento().GetByTransferenciaRenda(t.Id);
                foreach (var s in servicos)
                {
                    if (s.ServicosRecursosFinanceirosPrivado != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosPrivado.NumeroAtendidos)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPrivado.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                        pendencia.TransferenciaRenda = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosPublico != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosPublico.NumeroAtendidos)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPublico.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                        pendencia.TransferenciaRenda = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosCRAS != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosCRAS.NumeroAtendidos)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCRAS.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                        pendencia.TransferenciaRenda = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosCREAS != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosCREAS.NumeroAtendidos)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPrivado.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                        pendencia.TransferenciaRenda = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosCentroPOP != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosCentroPOP.NumeroAtendidos)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCentroPOP.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                        pendencia.TransferenciaRenda = false;
                        continue;
                    }
                }
            }

            var transferenciasMunicipais = new TransferenciaRenda().GetConsultaProgramasMunicipaisByPrefeitura(idPrefeitura);
            foreach (var t in transferenciasMunicipais)
            {
                if (t.Aderiu == 0)
                    continue;
                var servicos = new TransferenciaRendaCofinanciamento().GetByTransferenciaRenda(t.Id);
                foreach (var s in servicos)
                {
                    if (s.ServicosRecursosFinanceirosPrivado != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosPrivado.NumeroAtendidos)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPrivado.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                        pendencia.TransferenciaRenda = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosPublico != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosPublico.NumeroAtendidos)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPublico.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                        pendencia.TransferenciaRenda = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosCRAS != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosCRAS.NumeroAtendidos)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCRAS.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                        pendencia.TransferenciaRenda = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosCREAS != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosCREAS.NumeroAtendidos)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPrivado.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                        pendencia.TransferenciaRenda = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosCentroPOP != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosCentroPOP.NumeroAtendidos)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCentroPOP.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                        pendencia.TransferenciaRenda = false;
                        continue;
                    }
                }
            }
           

            #endregion
            #region Beneficios Eventuais

            var beneficios = new PrefeituraBeneficioEventual().GetByPrefeitura(idPrefeitura);
            pendencia.BeneficiosEventuais = true;

            if (beneficios.Count() == 0)
            {
                pendencia.Pendencias.Add("III -  Benefícios Eventuais : Não existe Cadastro de Benefícios Eventuais!");
                pendencia.BeneficiosEventuais = false;
            }
            else
            {
                foreach (var b in beneficios)
                {
                    if (!b.BeneficiarioAtendidoRedeSocioAssistencial)
                        continue;
                    var servicos = new PrefeituraBeneficioEventualServico().GetByBeneficioEventual(b.Id);
                    if (servicos.Count() == 0)
                    {
                        pendencia.Pendencias.Add("III -  Benefícios Eventuais - " + b.TipoBeneficioEventual.Nome + ": Não existe serviços vinculados");
                        pendencia.BeneficiosEventuais = false;
                        continue;
                    }

                    foreach (var s in servicos)
                    {
                        if (s.ServicosRecursosFinanceirosPrivado != null && s.NumeroBeneficiarios > s.ServicosRecursosFinanceirosPrivado.NumeroAtendidos)
                        {
                            var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPrivado.IdUsuarioTipoServico);
                            pendencia.Pendencias.Add("III - Benefícios Eventuais - " + b.TipoBeneficioEventual.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de beneficiários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                            pendencia.BeneficiosEventuais = false;
                            continue;
                        }
                        if (s.ServicosRecursosFinanceirosPublico != null && s.NumeroBeneficiarios > s.ServicosRecursosFinanceirosPublico.NumeroAtendidos)
                        {
                            var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPublico.IdUsuarioTipoServico);
                            pendencia.Pendencias.Add("III - Benefícios Eventuais - " + b.TipoBeneficioEventual.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de beneficiários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                            pendencia.BeneficiosEventuais = false;
                            continue;
                        }
                        if (s.ServicosRecursosFinanceirosCRAS != null && s.NumeroBeneficiarios > s.ServicosRecursosFinanceirosCRAS.NumeroAtendidos)
                        {
                            var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCRAS.IdUsuarioTipoServico);
                            pendencia.Pendencias.Add("III - Benefícios Eventuais - " + b.TipoBeneficioEventual.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de beneficiários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                            pendencia.BeneficiosEventuais = false;
                            continue;
                        }
                        if (s.ServicosRecursosFinanceirosCREAS != null && s.NumeroBeneficiarios > s.ServicosRecursosFinanceirosCREAS.NumeroAtendidos)
                        {
                            var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPrivado.IdUsuarioTipoServico);
                            pendencia.Pendencias.Add("III - Benefícios Eventuais - " + b.TipoBeneficioEventual.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de beneficiários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                            pendencia.BeneficiosEventuais = false;
                            continue;
                        }
                        if (s.ServicosRecursosFinanceirosCentroPOP != null && s.NumeroBeneficiarios > s.ServicosRecursosFinanceirosCentroPOP.NumeroAtendidos)
                        {
                            var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCentroPOP.IdUsuarioTipoServico);
                            pendencia.Pendencias.Add("III - Benefícios Eventuais - " + b.TipoBeneficioEventual.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de beneficiários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                            pendencia.BeneficiosEventuais = false;
                            continue;
                        }
                    }
                }
            }

            #endregion            

            return pendencia;
        }

        public PendenciaInfo ValidarBlocoIV(Int32 idPrefeitura, PendenciaInfo pendencia)
        {
            pendencia = pendencia ?? new PendenciaInfo();
            pendencia.Pendencias = pendencia.Pendencias ?? new List<string>();

            var acoes = new PrefeituraAcaoPlanejamento().GetByPrefeitura(idPrefeitura);
            pendencia.AcoesPlanejadas = true;
            if (acoes.Count() == 0)
            {
                pendencia.Pendencias.Add("IV - Planejamento de Ações: Não existe ações cadastradas");
                pendencia.AcoesPlanejadas = false;
            }

            return pendencia;
        }

        public PendenciaInfo ValidarBlocoV(Int32 idPrefeitura, PendenciaInfo pendencia)
        {
            pendencia = pendencia ?? new PendenciaInfo();
            pendencia.Pendencias = pendencia.Pendencias ?? new List<string>();

            #region V Cronograma Desembolso - Básica
            pendencia.CronogramaDesembolsoProtecaoBasica = true;
            if (new RecursosFinanceiros().GetValorCofinanciamentoEstadualPrefeituraByTipoProtecaoSocialRedePublica(idPrefeitura,1) != new CronogramaDesembolso().GetValorByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura,1,1))
            {
                pendencia.Pendencias.Add("IV Cronograma de Desembolso : O valor total de desembolso da Proteção Social Básica para Rede Pública deve ser igual ao Total do Cofinanciamento");
                pendencia.CronogramaDesembolsoProtecaoBasica = false;
            }

            if (new RecursosFinanceiros().GetValorCofinanciamentoEstadualPrefeituraByTipoProtecaoSocialRedePrivada(idPrefeitura, 1) != new CronogramaDesembolso().GetValorByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 1, 2))
            {
                pendencia.Pendencias.Add("IV Cronograma de Desembolso : O valor total de desembolso da Proteção Social Básica para Rede Privada deve ser igual ao Total do Cofinanciamento");
                pendencia.CronogramaDesembolsoProtecaoBasica = false;
            }            
            #endregion

            #region V Cronograma Desembolso - Especial Média Complexidade
            pendencia.CronogramaDesembolsoProtecaoMedia = true;
            if (new RecursosFinanceiros().GetValorCofinanciamentoEstadualPrefeituraByTipoProtecaoSocialRedePublica(idPrefeitura,2) != new CronogramaDesembolso().GetValorByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 2, 1))
            {
                pendencia.Pendencias.Add("IV Cronograma de Desembolso : O valor total de desembolso da Proteção Social Especial de Média Complexidade para Rede Pública deve ser igual ao Total do Cofinanciamento");
                pendencia.CronogramaDesembolsoProtecaoMedia = false;
            }

            if (new RecursosFinanceiros().GetValorCofinanciamentoEstadualPrefeituraByTipoProtecaoSocialRedePrivada(idPrefeitura, 2) != new CronogramaDesembolso().GetValorByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 2, 2))
            {
                pendencia.Pendencias.Add("IV Cronograma de Desembolso : O valor total de desembolso da Proteção Social Especial de Média Complexidade para Rede Privada deve ser igual ao Total do Cofinanciamento");
                pendencia.CronogramaDesembolsoProtecaoMedia = false;
            }   
            
            #endregion
            
            #region IV Cronograma Desembolso - Especial Alta Complexidade
            pendencia.CronogramaDesembolsoProtecaoAlta = true;
            if (new RecursosFinanceiros().GetValorCofinanciamentoEstadualPrefeituraByTipoProtecaoSocialRedePublica(idPrefeitura, 3) != new CronogramaDesembolso().GetValorByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 3, 1))
            {
                pendencia.Pendencias.Add("IV Cronograma de Desembolso : O valor total de desembolso da Proteção Social Especial de Alta Complexidade para Rede Pública deve ser igual ao Total do Cofinanciamento");
                pendencia.CronogramaDesembolsoProtecaoAlta = false;
            }

            if (new RecursosFinanceiros().GetValorCofinanciamentoEstadualPrefeituraByTipoProtecaoSocialRedePrivada(idPrefeitura, 3) != new CronogramaDesembolso().GetValorByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 3, 2))
            {
                pendencia.Pendencias.Add("IV Cronograma de Desembolso : O valor total de desembolso da Proteção Social Especial de Alta Complexidade para Rede Privada deve ser igual ao Total do Cofinanciamento");
                pendencia.CronogramaDesembolsoProtecaoAlta = false;
            }  
            #endregion           
            
            return pendencia;
        }

        public PendenciaInfo ValidarBlocoVI(Int32 idPrefeitura, PendenciaInfo pendencia)
        {
            pendencia = pendencia ?? new PendenciaInfo();
            pendencia.Pendencias = pendencia.Pendencias ?? new List<string>();

            bool deveTerAspectoGeral = false;
            
            var vigilancia = new VigilanciaSocioAssistencial().GetByPrefeitura(idPrefeitura);
            pendencia.VigilanciaSocioAssistencial = true;

            if (vigilancia == null)
            {
                pendencia.Pendencias.Add("VI - Vigilância, Monitoramento e Avaliação: Não existe Cadastro de Vigilância Socioassistencial");
                pendencia.VigilanciaSocioAssistencial = false;
            }
            else if (vigilancia.OfereceVigilancia)
                deveTerAspectoGeral = true;            

            var monitoramento = new Monitoramento().GetByPrefeitura(idPrefeitura);
            pendencia.Monitoramento = true;
            
            if (monitoramento == null)
            {
                pendencia.Pendencias.Add("VI - Vigilância, Monitoramento e Avaliação: Não existe Cadastro de Monitoramento");
                pendencia.Monitoramento = false;
            }
            else if(monitoramento.RealizaMonitoramento)            
                deveTerAspectoGeral = true;

            var avaliacao = new Avaliacao().GetByPrefeitura(idPrefeitura);
            pendencia.Avaliacao = true;

            if (avaliacao == null)
            {
                pendencia.Pendencias.Add("VI - Vigilância, Monitoramento e Avaliação: Não existe Cadastro de Avaliação");
                pendencia.Avaliacao = false;
            }
            else if (avaliacao.AvaliaAcoes)
                deveTerAspectoGeral = true;

            pendencia.AspectosGerais = true;
            if (deveTerAspectoGeral)
            {
                var aspectoGeral = new PrefeituraVigilanciaMonitoramentoAvaliacao().GetByPrefeitura(idPrefeitura);
                if (aspectoGeral == null)
                {
                    pendencia.Pendencias.Add("VI - Vigilância, Monitoramento e Avaliação: Não existe Cadastro de Aspectos Gerais");
                    pendencia.AspectosGerais = false;
                }
            }
            return pendencia;
        }

        public PendenciaInfo ValidarBlocoVII(Int32 idPrefeitura, PendenciaInfo pendencia)
        {
            pendencia = pendencia ?? new PendenciaInfo();
            pendencia.Pendencias = pendencia.Pendencias ?? new List<string>();

            var cmas = new ConselhoMunicipal().GetByPrefeitura(idPrefeitura);
            pendencia.ConselhoMunicipal = true;
            if (cmas == null)
            {
                pendencia.Pendencias.Add("VII - CMAS: Não existe Cadastro do Conselho Municipal");
                pendencia.ConselhoMunicipal = false;
            }
            return pendencia;
        }
    }
}
