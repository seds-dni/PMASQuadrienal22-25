using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Data.Objects;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using System.Transactions;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Entidades.Programas;
using Seds.PMAS.QUADRIENAL.Negocio.Programas;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class TransferenciaRenda
    {
        private static IRepository<TransferenciaRendaInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<TransferenciaRendaInfo>>();
            }
        }
        private static IRepository<ConsultaTransferenciaRendaInfo> _repositoryConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaTransferenciaRendaInfo>>();
            }
        }

        public IQueryable<TransferenciaRendaInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        private static List<int> Exercicios = new List<int>() { 2022, 2023, 2024, 2025 };

        public TransferenciaRendaInfo GetById(int id)
        {
            var p = _repository.Single(m => m.Id == id);
            if (p == null)
                return null;

            if (p.PossuiParceriaFormal)
                p.Parcerias = new TransferenciaRendaParceria().GetByTransferenciaRenda(p.Id).ToList();

            if (p.IdTipoTransferenciaRenda == 13)
                p.TecnicoReferencia = new TransferenciaRendaTecnicoReferencia().GetByTransferenciaRenda(p.Id).ToList();

            if (((ETipoTransferenciaRenda)p.IdTipoTransferenciaRenda) == ETipoTransferenciaRenda.SaoPauloSolidario && p.PossuiParceriaFormalSaoPauloSolidarioAgendaFamilia.HasValue && p.PossuiParceriaFormalSaoPauloSolidarioAgendaFamilia.Value)
                p.ParceriasSaoPauloSolidarioAgendaFamilia = new SPSolidarioAgendaFamiliaParceria().GetByTransferenciaRenda(p.Id).ToList();

            //if (p.IdTipoTransferenciaRenda == (Int32)ETipoTransferenciaRenda.BolsaFamilia)
            p.TransferenciaRendaPrevisaoAnual = new TransferenciaRendaPrevisaoAnual().GetByTransferenciaRenda(p.Id);

            if (p.IdTipoTransferenciaRenda == 4)
                p.AcoesPETI = new PETIAcao().GetByTransferenciaRenda(p.Id).ToList();

            return p;
        }

        public IQueryable<TransferenciaRendaInfo> GetByPrefeitura(int idPrefeitura)
        {
            return _repository.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura);
        }

        public IQueryable<TransferenciaRendaInfo> GetProgramasMunicipaisByPrefeitura(int idPrefeitura)
        {
            return _repository.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura && m.IdTipoTransferenciaRenda == 8);
        }

        public IQueryable<TransferenciaRendaInfo> GetProgramasFederaisByPrefeitura(int idPrefeitura)
        {
            return _repository.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura && (m.IdTipoTransferenciaRenda == 3 || m.IdTipoTransferenciaRenda == 4));
        }

        public IQueryable<TransferenciaRendaInfo> GetProgramasEstaduaisByPrefeitura(int idPrefeitura)
        {
            return _repository.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura && (m.IdTipoTransferenciaRenda == 5 || m.IdTipoTransferenciaRenda == 6));
        }

        public IQueryable<TransferenciaRendaInfo> GetBeneficiosContinuadosByPrefeitura(int idPrefeitura)
        {
            return _repository.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura && (m.IdTipoTransferenciaRenda == 1 || m.IdTipoTransferenciaRenda == 2));
        }

        public IQueryable<ConsultaTransferenciaRendaInfo> GetConsultaProgramasFederaisByPrefeitura(int idPrefeitura)
        {
            return _repositoryConsulta.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura && (m.IdTipoTransferenciaRenda == 3 || m.IdTipoTransferenciaRenda == 4));
        }

        public IQueryable<ConsultaTransferenciaRendaInfo> GetConsultaBeneficiosContinuadosByPrefeitura(int idPrefeitura)
        {
            return _repositoryConsulta.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura && (m.IdTipoTransferenciaRenda == 1 || m.IdTipoTransferenciaRenda == 2));
        }

        public IQueryable<ConsultaTransferenciaRendaInfo> GetConsultaProgramasEstaduaisByPrefeitura(int idPrefeitura)  
        {
            return _repositoryConsulta.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura && (m.IdTipoTransferenciaRenda == 5 || m.IdTipoTransferenciaRenda == 6 ));//|| m.IdTipoTransferenciaRenda == 10)); //|| m.IdTipoTransferenciaRenda == 9));
        }

        public IQueryable<ConsultaTransferenciaRendaInfo> GetConsultaProgramasMunicipaisByPrefeitura(int idPrefeitura)
        {
            return _repositoryConsulta.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura && m.IdTipoTransferenciaRenda == 8);
        }

        private int atualizaInsereTransferenciaDeRendaPrevisaoAnual(TransferenciaRendaPrevisaoAnualInfo transf,int idTransferenciaDeRenda) 
        {
            int idTranferenciaDeRendaPrevisaoAnual = 0;

            TransferenciaRendaPrevisaoAnual obj = new TransferenciaRendaPrevisaoAnual();

            var collection = obj.GetByTransferenciaRendaByIdPrefeitura(idTransferenciaDeRenda,transf.IdPrefeitura);

            if (collection == null)
            {
               obj.Add(transf, true);
               idTranferenciaDeRendaPrevisaoAnual = obj.GetByTransferenciaRendaByIdPrefeitura(idTransferenciaDeRenda, transf.IdPrefeitura).Id;
            }
            else
            {
                idTranferenciaDeRendaPrevisaoAnual = transf.Id = obj.GetByTransferenciaRendaByIdPrefeitura(idTransferenciaDeRenda, transf.IdPrefeitura).Id;
                obj.Update(transf,true);
            }

            return idTranferenciaDeRendaPrevisaoAnual;
        }

        public void Update(TransferenciaRendaInfo obj, Boolean commit)
        {
            new ValidadorTransferenciaRenda().Validar(obj);
            if (obj.IdTipoTransferenciaRenda != 9 || obj.IdTipoTransferenciaRenda != 8)
                obj.Ativo = true;


            if (obj.TransferenciaRendaPrevisaoAnual.Id == 0)
            {
                obj.TransferenciaRendaPrevisaoAnual.Id = atualizaInsereTransferenciaDeRendaPrevisaoAnual(obj.TransferenciaRendaPrevisaoAnual, obj.Id);
            }

            _repository.Update(obj);

            var propriedadesEntity = _repository.GetModifiedProperties(obj);
            var propriedades = GetLabelForInfo(propriedadesEntity, obj);

            if (obj.IdTipoTransferenciaRenda == 1 || obj.IdTipoTransferenciaRenda == 2)
            {
                var previsao = new TransferenciaRendaPrevisaoAnual();
                var previsaoAnual = previsao.GetByTransferenciaRenda(obj.Id);
                if (previsaoAnual == null)
                {
                    var objTransf = obj.TransferenciaRendaPrevisaoAnual;
                    previsaoAnual = new TransferenciaRendaPrevisaoAnualInfo();
                    previsaoAnual.IdPrefeitura = obj.IdPrefeitura;
                    previsaoAnual.IdTransferenciaRenda = obj.Id;


                    previsaoAnual.MetaPactuada2022 = objTransf.MetaPactuada2022;
                    previsaoAnual.MetaPactuada2023 = objTransf.MetaPactuada2023;
                    previsaoAnual.MetaPactuada2024 = objTransf.MetaPactuada2024;
                    previsaoAnual.MetaPactuada2025 = objTransf.MetaPactuada2025;

                    previsao.Add(previsaoAnual, true);

                }
                else
                {
                    var objTransf = obj.TransferenciaRendaPrevisaoAnual;

                    previsaoAnual.MetaPactuada2022 = objTransf.MetaPactuada2022;
                    previsaoAnual.MetaPactuada2023 = objTransf.MetaPactuada2023;
                    previsaoAnual.MetaPactuada2024 = objTransf.MetaPactuada2024;
                    previsaoAnual.MetaPactuada2025 = objTransf.MetaPactuada2025;

                    previsao.Update(previsaoAnual, true);
                }
            }



            if (obj.IdTipoTransferenciaRenda == 3)
            {
                obj.Ativo = true;
                var previsao = new TransferenciaRendaPrevisaoAnual();
                var previsaoAnual = previsao.GetByTransferenciaRenda(obj.Id);
                if (previsaoAnual == null)
                {
                    var objTransf = obj.TransferenciaRendaPrevisaoAnual;
                    previsaoAnual = new TransferenciaRendaPrevisaoAnualInfo();
                    previsaoAnual.IdPrefeitura = obj.IdPrefeitura;
                    previsaoAnual.IdTransferenciaRenda = obj.Id;

                    previsaoAnual.EstimativaFamilias2021 = objTransf.EstimativaFamilias2021;
                    previsaoAnual.EstimativaFamilias2022 = objTransf.EstimativaFamilias2022;
                    previsaoAnual.EstimativaFamilias2023 = objTransf.EstimativaFamilias2023;
                    previsaoAnual.EstimativaFamilias2024 = objTransf.EstimativaFamilias2024;

                    previsaoAnual.NumeroFamiliasBeneficiarias2021 = objTransf.NumeroFamiliasBeneficiarias2021;
                    previsaoAnual.NumeroFamiliasBeneficiarias2022 = objTransf.NumeroFamiliasBeneficiarias2022;
                    previsaoAnual.NumeroFamiliasBeneficiarias2023 = objTransf.NumeroFamiliasBeneficiarias2023;
                    previsaoAnual.NumeroFamiliasBeneficiarias2024 = objTransf.NumeroFamiliasBeneficiarias2024;

                    previsaoAnual.FamiliasCadastradas2021 = objTransf.FamiliasCadastradas2021;
                    previsaoAnual.FamiliasCadastradas2022 = objTransf.FamiliasCadastradas2022;
                    previsaoAnual.FamiliasCadastradas2023 = objTransf.FamiliasCadastradas2023;
                    previsaoAnual.FamiliasCadastradas2024 = objTransf.FamiliasCadastradas2024;

                    previsaoAnual.RepasseMensal2021 = objTransf.RepasseMensal2021;
                    previsaoAnual.RepasseMensal2022 = objTransf.RepasseMensal2022;
                    previsaoAnual.RepasseMensal2023 = objTransf.RepasseMensal2023;
                    previsaoAnual.RepasseMensal2024 = objTransf.RepasseMensal2024;

                    previsao.Add(previsaoAnual, true);

                }
                else
                {
                    var objTransf = obj.TransferenciaRendaPrevisaoAnual;
                    previsaoAnual.EstimativaFamilias2021 = objTransf.EstimativaFamilias2021;
                    previsaoAnual.EstimativaFamilias2022 = objTransf.EstimativaFamilias2022;
                    previsaoAnual.EstimativaFamilias2023 = objTransf.EstimativaFamilias2023;
                    previsaoAnual.EstimativaFamilias2024 = objTransf.EstimativaFamilias2024;

                    previsaoAnual.NumeroFamiliasBeneficiarias2021 = objTransf.NumeroFamiliasBeneficiarias2021;
                    previsaoAnual.NumeroFamiliasBeneficiarias2022 = objTransf.NumeroFamiliasBeneficiarias2022;
                    previsaoAnual.NumeroFamiliasBeneficiarias2023 = objTransf.NumeroFamiliasBeneficiarias2023;
                    previsaoAnual.NumeroFamiliasBeneficiarias2024 = objTransf.NumeroFamiliasBeneficiarias2024;

                    previsaoAnual.FamiliasCadastradas2021 = objTransf.FamiliasCadastradas2021;
                    previsaoAnual.FamiliasCadastradas2022 = objTransf.FamiliasCadastradas2022;
                    previsaoAnual.FamiliasCadastradas2023 = objTransf.FamiliasCadastradas2023;
                    previsaoAnual.FamiliasCadastradas2024 = objTransf.FamiliasCadastradas2024;

                    previsaoAnual.RepasseMensal2021 = objTransf.RepasseMensal2021;
                    previsaoAnual.RepasseMensal2022 = objTransf.RepasseMensal2022;
                    previsaoAnual.RepasseMensal2023 = objTransf.RepasseMensal2023;
                    previsaoAnual.RepasseMensal2024 = objTransf.RepasseMensal2024;

                    previsao.Update(previsaoAnual, true);
                }
            }
            if (obj.IdTipoTransferenciaRenda == 5 || obj.IdTipoTransferenciaRenda == 6)
            {
                obj.Ativo = true;
                var previsao = new TransferenciaRendaPrevisaoAnual();
                var previsaoAnual = previsao.GetByTransferenciaRenda(obj.Id);
                if (previsaoAnual == null)
                {
                    var objTransf = obj.TransferenciaRendaPrevisaoAnual;
                    previsaoAnual = new TransferenciaRendaPrevisaoAnualInfo();
                    previsaoAnual.IdPrefeitura = obj.IdPrefeitura;
                    previsaoAnual.IdTransferenciaRenda = obj.Id;

                    previsaoAnual.EstimativaFamilias2021 = objTransf.EstimativaFamilias2021;
                    previsaoAnual.EstimativaFamilias2022 = objTransf.EstimativaFamilias2022;
                    previsaoAnual.EstimativaFamilias2023 = objTransf.EstimativaFamilias2023;
                    previsaoAnual.EstimativaFamilias2024 = objTransf.EstimativaFamilias2024;

                    previsaoAnual.MetaPactuada2021 = objTransf.MetaPactuada2021;
                    previsaoAnual.MetaPactuada2022 = objTransf.MetaPactuada2022;
                    previsaoAnual.MetaPactuada2023 = objTransf.MetaPactuada2023;
                    previsaoAnual.MetaPactuada2024 = objTransf.MetaPactuada2024;
                    previsaoAnual.MetaPactuada2025 = objTransf.MetaPactuada2025;

                    previsao.Add(previsaoAnual, true);

                }
                else
                {
                    var objTransf = obj.TransferenciaRendaPrevisaoAnual;

                    previsaoAnual.EstimativaFamilias2021 = objTransf.EstimativaFamilias2021;
                    previsaoAnual.EstimativaFamilias2022 = objTransf.EstimativaFamilias2022;
                    previsaoAnual.EstimativaFamilias2023 = objTransf.EstimativaFamilias2023;
                    previsaoAnual.EstimativaFamilias2024 = objTransf.EstimativaFamilias2024;
                    previsaoAnual.EstimativaFamilias2025 = objTransf.EstimativaFamilias2025;

                    previsaoAnual.MetaPactuada2021 = objTransf.MetaPactuada2021;
                    previsaoAnual.MetaPactuada2022 = objTransf.MetaPactuada2022;
                    previsaoAnual.MetaPactuada2023 = objTransf.MetaPactuada2023;
                    previsaoAnual.MetaPactuada2024 = objTransf.MetaPactuada2024;
                    previsaoAnual.MetaPactuada2025 = objTransf.MetaPactuada2025;

                    previsao.Update(previsaoAnual, true);
                }
            }


            //AÇÕES PETI
            if (obj.IdTipoTransferenciaRenda == 4)
            {
                obj.Ativo = true;

                if (obj.PETIAderiuCofinanciamentoFederal.HasValue && obj.PETIAderiuCofinanciamentoFederal.Value)
                {

                    var gestor = new TransferenciaRendaGestorAcao();
                    if (!obj.NaoPossuiTecnicoAcao)
                    {
                        var gestorAcao = gestor.GetByIdTransferenciaRenda(obj.Id);
                        if (gestorAcao == null)
                        {
                            var objGestor = obj.GestorAcao;
                            var transferenciaGestorAcao = new TransferenciaRendaGestorAcaoInfo();
                            transferenciaGestorAcao.IdTransferenciaRenda = obj.Id;
                            transferenciaGestorAcao.Nome = obj.GestorAcao.Nome;
                            transferenciaGestorAcao.Telefone = obj.GestorAcao.Telefone;
                            transferenciaGestorAcao.Celular = obj.GestorAcao.Celular;
                            transferenciaGestorAcao.Email = obj.GestorAcao.Email;
                            gestor.Add(transferenciaGestorAcao, true);
                        }
                        else
                        {
                            var objGestor = obj.GestorAcao;
                            var transferenciaGestorAcao = new TransferenciaRendaGestorAcaoInfo();
                            transferenciaGestorAcao.Id = objGestor.Id;
                            transferenciaGestorAcao.IdTransferenciaRenda = obj.Id;
                            transferenciaGestorAcao.Nome = objGestor.Nome;
                            transferenciaGestorAcao.Telefone = objGestor.Telefone;
                            transferenciaGestorAcao.Celular = objGestor.Celular;
                            transferenciaGestorAcao.Email = objGestor.Email;
                            gestor.Update(transferenciaGestorAcao, true);
                        }
                    }
                    else
                        if (obj.GestorAcao != null)
                        {
                            var gestorAcao = new TransferenciaRendaGestorAcao().GetById(obj.GestorAcao.Id);
                            if (gestorAcao != null)
                            {
                                gestor.Delete(gestorAcao);
                            }
                        }

                    if (obj.PETIAcoesTrabalhoInfantil.Value)
                    {
                        var lstDeletedAcoesPETI = new List<PETIAcaoInfo>();
                        var petiAcao = new PETIAcao();
                        var lstAcoesPETI = petiAcao.GetByTransferenciaRenda(obj.Id);
                        obj.AcoesPETI = obj.AcoesPETI ?? new List<PETIAcaoInfo>();
                        var hasChangeAcoesPETI = false;

                        foreach (var p in lstAcoesPETI)
                            if (!obj.AcoesPETI.Any(t => t.Id == p.Id))
                            {
                                hasChangeAcoesPETI = true;
                                lstDeletedAcoesPETI.Add(p);
                            }

                        foreach (var p in lstDeletedAcoesPETI)
                            petiAcao.Delete(p, false);

                        foreach (var p in obj.AcoesPETI)
                        {
                            p.PETIEixoAtuacao = null;
                            p.PETITipoAcao = null;
                            p.PETISituacaoAcao = null;
                            p.TransferenciaRenda = null;
                            p.IdTransferenciaRenda = obj.Id;
                            if (p.Id == 0)
                            {
                                petiAcao.Add(p, false);
                                hasChangeAcoesPETI = true;
                            }
                            else
                                petiAcao.Update(p, false);
                        }

                        if (hasChangeAcoesPETI && !propriedades.Any(t => t == "acoespeti"))
                            propriedades.Add("acoespeti");
                    }
                }
            }

            if (obj.Parcerias != null)
            {
                var lstDeleted = new List<TransferenciaRendaParceriaInfo>();
                var ppp = new TransferenciaRendaParceria();
                var lst = ppp.GetByTransferenciaRenda(obj.Id);
                obj.Parcerias = obj.Parcerias ?? new List<TransferenciaRendaParceriaInfo>();
                var hasChangeParcerias = false;

                foreach (var p in lst)
                    if (!obj.Parcerias.Any(t => t.Id == p.Id))
                    {
                        hasChangeParcerias = true;
                        lstDeleted.Add(p);
                    }

                foreach (var p in lstDeleted)
                    ppp.Delete(p, false);

                foreach (var p in obj.Parcerias)
                {
                    p.TipoParceria = null;
                    p.Parceria = null;
                    p.IdTransferenciaRenda = obj.Id;
                    if (p.Id == 0)
                    {
                        ppp.Add(p, false);
                        hasChangeParcerias = true;
                    }
                    else
                        ppp.Update(p, false);
                }

                if (hasChangeParcerias && !propriedades.Any(t => t == "parcerias"))
                    propriedades.Add("parcerias");

            }

            if (obj.TecnicoReferencia != null)
            {
                var lstDeleted = new List<TransferenciaRendaTecnicoReferenciaInfo>();
                var ppp = new TransferenciaRendaTecnicoReferencia();
                var lst = ppp.GetByTransferenciaRenda(obj.Id);
                obj.TecnicoReferencia = obj.TecnicoReferencia ?? new List<TransferenciaRendaTecnicoReferenciaInfo>();
                var hasChangeTecnico = false;

                foreach (var p in lst)
                    if (!obj.TecnicoReferencia.Any(t => t.Id == p.Id))
                    {
                        hasChangeTecnico = true;
                        lstDeleted.Add(p);
                    }

                foreach (var p in lstDeleted)
                    ppp.Delete(p, false);

                foreach (var p in obj.TecnicoReferencia)
                {
                    
                    
                    p.IdTransferenciaRenda = obj.Id;
                    if (p.Id == 0)
                    {
                        ppp.Add(p, false);
                        hasChangeTecnico = true;
                    }
                    else
                        ppp.Update(p, false);
                }

                if (hasChangeTecnico && !propriedades.Any(t => t == "TecnicoReferencia"))
                    propriedades.Add("TecnicoReferencia");

            }

            if (propriedades.Count > 0)
            {
                String descricao = "Transferência de Renda: " + obj.Nome + System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
                var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Update, GetQuadroCadastro((ETipoTransferenciaRenda)obj.IdTipoTransferenciaRenda), descricao, obj.Id);
                if (log != null)
                    new Log().Add(log, false);
            }



            if (commit)
                ContextManager.Commit();
        }

        public void SaveRecursosSaoPauloSolidario(TransferenciaRendaInfo obj, Boolean commit)
        {
            _repository.Update(obj);

            var propriedadesEntity = _repository.GetModifiedProperties(obj);
            var propriedades = GetLabelForInfo(propriedadesEntity, obj);

            if (propriedades.Count > 0)
            {
                String descricao = "Transferência de Renda: " + obj.Nome + System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
                var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Update, GetQuadroCadastro((ETipoTransferenciaRenda)obj.IdTipoTransferenciaRenda), descricao, obj.Id);
                if (log != null)
                    new Log().Add(log, false);
            }

            if (commit)
                ContextManager.Commit();
        }

        public void Add(TransferenciaRendaInfo transferencia, Boolean commit)
        {
            new ValidadorTransferenciaRenda().Validar(transferencia);
            TransactionOptions tsOptions = new TransactionOptions();
            tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
            {
                ContextManager.OpenConnection();
                _repository.Add(transferencia);

                var log = Log.CreateLog(transferencia.IdPrefeitura, EAcao.Add, GetQuadro((ETipoTransferenciaRenda)transferencia.IdTipoTransferenciaRenda), "Incluída a Transferência de Renda " + transferencia.Nome + ".");
                if (log != null)
                    new Log().Add(log, false);

                ContextManager.Commit();
                if (transferencia.PossuiParceriaFormal && transferencia.Parcerias != null && transferencia.Parcerias.Count > 0)
                {
                    var ppp = new TransferenciaRendaParceria();
                    transferencia.Parcerias.ForEach(p =>
                    {
                        p.TipoParceria = null;
                        p.Parceria = null;
                        p.IdTransferenciaRenda = transferencia.Id;
                        ppp.Add(p, true);
                    });
                }

                if (transferencia.IdTipoTransferenciaRenda == 13 && transferencia.TecnicoReferencia != null)
                {
                    if (transferencia.TecnicoReferencia.Count > 0)
                    {
                        var ppp = new TransferenciaRendaTecnicoReferencia();
                        transferencia.TecnicoReferencia.ForEach(p =>
                        {
                            p.IdTransferenciaRenda = transferencia.Id;
                            ppp.Add(p, true);
                        });                        
                    }
                }


                if (transferencia.IdTipoTransferenciaRenda == 4)
                {
                    if (transferencia.PETIAcoesTrabalhoInfantil.Value)
                    {
                        var petiAcao = new PETIAcao();
                        transferencia.AcoesPETI.ForEach(a =>
                        {
                            a.PETIEixoAtuacao = null;
                            a.PETITipoAcao = null;
                            a.PETISituacaoAcao = null;
                            a.TransferenciaRenda = null;
                            a.IdTransferenciaRenda = transferencia.Id;
                            petiAcao.Add(a, true);
                        });
                    }
                }

                ContextManager.CloseConnection();
                ts.Complete();
            }
        }

        public void Delete(TransferenciaRendaInfo transferencia, Boolean commit)
        {
            //REMOVER AÇÕES PETI
            if (transferencia.IdTipoTransferenciaRenda == 4)
            {
                var petiAcao = new PETIAcao();
                var lst = petiAcao.GetByTransferenciaRenda(transferencia.Id).ToList();
                if (lst.Count > 0)
                    foreach (var a in lst)
                        petiAcao.Delete(a, false);
            }

            //REMOVER PARCERIAS
            var l = new TransferenciaRendaParceria();
            var parcerias = l.GetByTransferenciaRenda(transferencia.Id).ToList();
            if (parcerias.Count > 0)
                foreach (var p in parcerias)
                    l.Delete(p, false);


            var prev = new TransferenciaRendaPrevisaoAnual();
            var previsaoAnual = prev.GetByTransferenciaRenda(transferencia.Id);
            if (previsaoAnual != null)
                prev.Delete(previsaoAnual, false);
          

            if (((ETipoTransferenciaRenda)transferencia.IdTipoTransferenciaRenda) == ETipoTransferenciaRenda.SaoPauloSolidario)
            {
                var ptr = new SPSolidarioAgendaFamiliaParceria();
                var parceriasAgendaFamilia = ptr.GetByTransferenciaRenda(transferencia.Id).ToList();
                if (parceriasAgendaFamilia.Count > 0)
                    foreach (var pc in parceriasAgendaFamilia)
                        ptr.Delete(pc, false);
            }

            //REMOVER COFINANCIAMENTO
            var s = new TransferenciaRendaCofinanciamento();
            var servicos = s.GetByTransferenciaRenda(transferencia.Id).ToList();
            if (servicos.Count > 0)
                foreach (var c in servicos)
                    s.Delete(c, false, false);

            //Deletar peti indicadores
            Prefeitura prefeitura = new Prefeitura();
            var idMunicipio = prefeitura.GetById(transferencia.IdPrefeitura).IdMunicipio;

            PETIIndicadores petiIndicadores = new PETIIndicadores();

            var indicadoresPeti = new PETIIndicadoresInfo();


            indicadoresPeti = petiIndicadores.GetByMunicipio(idMunicipio);
            
            indicadoresPeti.Idade1013Ano2021 = 0;
            indicadoresPeti.Idade1013Ano2022 = 0;
            indicadoresPeti.Idade1013Ano2023 = 0;
            indicadoresPeti.Idade1013Ano2024 = 0;

            indicadoresPeti.Idade1415Ano2021 = 0;
            indicadoresPeti.Idade1415Ano2022 = 0;
            indicadoresPeti.Idade1415Ano2023 = 0;
            indicadoresPeti.Idade1415Ano2024 = 0;

            indicadoresPeti.Idade1617Ano2021 = 0;
            indicadoresPeti.Idade1617Ano2022 = 0;
            indicadoresPeti.Idade1617Ano2023 = 0;
            indicadoresPeti.Idade1617Ano2024 = 0;

            indicadoresPeti.MetaMunicipal2021 = 0;
            indicadoresPeti.MetaMunicipal2022 = 0;
            indicadoresPeti.MetaMunicipal2023 = 0;
            indicadoresPeti.MetaMunicipal2024 = 0;

            petiIndicadores.Update(indicadoresPeti,true);


            //DELETA GESTOR ACAO

            TransferenciaRendaGestorAcao acaoGestor = new TransferenciaRendaGestorAcao();

            var gestorAcao = acaoGestor.GetByIdTransferenciaRenda(transferencia.Id);
            if(gestorAcao != null)
            acaoGestor.Delete(gestorAcao);


            if (transferencia.IdTipoTransferenciaRenda == 11 || transferencia.IdTipoTransferenciaRenda == 10)
            {
                transferencia.ExecutaPrograma = false;
            }

            String descricao = "Excluída a Transferência de Renda " + transferencia.Nome + ".";

            var log = Log.CreateLog(transferencia.IdPrefeitura, EAcao.Remove, GetQuadro((ETipoTransferenciaRenda)transferencia.IdTipoTransferenciaRenda), descricao);
            if (log != null)
                new Log().Add(log, true);

            if (transferencia.IdTipoTransferenciaRenda != Convert.ToInt32(ETipoTransferenciaRenda.Outros))
            {
                transferencia.Ativo = true;
                transferencia.MunicipaisNumeroBeneficiarios = null;
                transferencia.MunicipaisRepasse = null;
                transferencia.PETINumeroBeneficiarios = null;
                transferencia.PETIPrevisaoMensal = null;
                transferencia.PossuiParceriaFormal = false;
                transferencia.AcaoRendaMeta = null;
                transferencia.BolsaFamiliaEstimativaFamilias = null;
                transferencia.BolsaFamiliaNumeroFamilias = null;
                transferencia.BolsaFamiliaRepasseMensal = null;
                transferencia.BPCNumeroBeneficiarios = null;
                transferencia.BeneficiarioAtendidoRedeSocioAssistencial = false;
                transferencia.IdFaseProgramaSaoPauloSolidario = null;
                transferencia.SaoPauloSolidarioMesInicioBuscaAtiva = null;
                transferencia.SaoPauloSolidarioAnoInicioBuscaAtiva = null;
                transferencia.SaoPauloSolidarioMesTerminoBuscaAtiva = null;
                transferencia.SaoPauloSolidarioAnoTerminoBuscaAtiva = null;
                transferencia.SaoPauloSolidarioOrgaoGestorExecutaBuscaAtiva = null;
                transferencia.SaoPauloSolidarioCRASExecutaBuscaAtiva = null;
                transferencia.SaoPauloSolidarioCREASExecutaBuscaAtiva = null;
                transferencia.SaoPauloSolidarioUnidadePrivadaExecutaBuscaAtiva = null;
                transferencia.SaoPauloSolidarioValorFMASBuscaAtiva = null;
                transferencia.SaoPauloSolidarioValorOrcamentoMunicipalBuscaAtiva = null;
                transferencia.SaoPauloSolidarioValorFEASRetidoFMAS2013 = null;
                transferencia.SaoPauloSolidarioMesRepasseFEASBuscaAtiva = null;
                transferencia.SaoPauloSolidarioAnoRepasseFEASBuscaAtiva = null;
                transferencia.SaoPauloSolidarioValorFEASBuscaAtiva = null;
                transferencia.SaoPauloSolidarioValorFNASBuscaAtiva = null;
                transferencia.SaoPauloSolidarioValorIGDPBFBuscaAtiva = null;
                transferencia.SaoPauloSolidarioValorIGDSUASBuscaAtiva = null;
                transferencia.SaoPauloSolidarioOrgaoGestorExecutaAgendaFamilia = null;
                transferencia.SaoPauloSolidarioCRASExecutaAgendaFamilia = null;
                transferencia.SaoPauloSolidarioCREASExecutaAgendaFamilia = null;
                transferencia.SaoPauloSolidarioNumeroFamiliasAgendaFamilia2012 = null;
                transferencia.SaoPauloSolidarioNumeroFamiliasAgendaFamilia2013 = null;
                transferencia.SaoPauloSolidarioValorFMASAgendaFamilia = null;
                transferencia.SaoPauloSolidarioValorOrcamentoMunicipalAgendaFamilia = null;
                transferencia.SaoPauloSolidarioMesRepasseFEASAgendaFamilia = null;
                transferencia.SaoPauloSolidarioAnoRepasseFEASAgendaFamilia = null;
                transferencia.SaoPauloSolidarioValorFEASAgendaFamilia = null;
                transferencia.SaoPauloSolidarioValorFNASAgendaFamilia = null;
                transferencia.SaoPauloSolidarioValorIGDPBFAgendaFamilia = null;
                transferencia.SaoPauloSolidarioValorIGDSUASAgendaFamilia = null;
                transferencia.SaoPauloSolidarioMeta = null;
                transferencia.SaoPauloSolidarioRepasseAnual = null;
                transferencia.PossuiParceriaFormalSaoPauloSolidarioAgendaFamilia = null;
                transferencia.PETINumeroBeneficiarioBolsaFamilia = null;
                transferencia.PETINumeroBeneficiarioPETIPuroRural = null;
                transferencia.PETINumeroBeneficiarioPETIPuroUrbano = null;
                transferencia.PETINumeroBeneficiarioProgramaMunicipal = null;
                transferencia.PETINumeroBeneficiarios = null;
                transferencia.PETINumeroTrabalhoInfantilCadUnico = null;
                transferencia.PossuiParceriaFormalSaoPauloSolidarioAgendaFamilia = false;
                transferencia.PossuiParceriaFormal = false;
                transferencia.AderiuBPCNaEscola = false;
                transferencia.DataAdesaoBPCNaEscola = null;
                transferencia.PETIDataAdesao = null;
                transferencia.ValorFEAS = null;
                transferencia.ValorFMAS = null;
                transferencia.ValorFNAS = null;
                transferencia.ValorFundoEstadual = null;
                transferencia.ValorFundoFederal = null;
                transferencia.ValorFundoMunicipal = null;
                transferencia.ValorOrcamentoEstadual = null;
                transferencia.ValorOrcamentoFederal = null;
                transferencia.ValorOrcamentoMunicipal = null;
                transferencia.ValorIGDPBF = null;
                transferencia.ValorIGDSUAS = null;
                transferencia.ValorAEPETI = null;
                transferencia.ValorAEPETI2 = null;
                transferencia.PETIAderiuCofinanciamentoFederal = false;
                transferencia.PETIBeneficiarioBolsaFamilia = false;
                transferencia.PETIBeneficiarioPETIPuro = false;
                transferencia.PETIBeneficiarioProgramaMunicipal = false;
                transferencia.PETIBeneficiarioTransferenciaRenda = false;
                transferencia.DataAdesaoPrograma = null;
                transferencia.NomeTecnico = String.Empty;
                transferencia.Telefone = String.Empty;
                transferencia.Celular = String.Empty;
                transferencia.Email = String.Empty;
                transferencia.ExecutaPrograma = false;
                transferencia.PossuiParceriaFormal = false;

                _repository.Update(transferencia);

                if (commit)
                    ContextManager.Commit();
                return;
            }

            _repository.Delete(transferencia);
            if (commit)
                ContextManager.Commit();
        }

        private Int32 GetQuadro(ETipoTransferenciaRenda transferenciaRenda)
        {
            switch (transferenciaRenda)
            {
                case ETipoTransferenciaRenda.BolsaFamilia:
                case ETipoTransferenciaRenda.PETIProgramaErradicacaoTrabalhoInfantil: return 43;
                case ETipoTransferenciaRenda.AcaoJovem:
                case ETipoTransferenciaRenda.RendaCidada:
                case ETipoTransferenciaRenda.SaoPauloSolidario: return 46;
                case ETipoTransferenciaRenda.Outros: return 50;
                case ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCPessoaDeficiencia:
                case ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCIdoso: return 57;
                case ETipoTransferenciaRenda.ProsperaFamilia: return 10;
                case ETipoTransferenciaRenda.AuxilioAluguel: return 13;
                case ETipoTransferenciaRenda.FCadUnico: return 11;
            }
            return 0;
        }

        private Int32 GetQuadroCadastro(ETipoTransferenciaRenda transferenciaRenda)
        {
            switch (transferenciaRenda)
            {
                case ETipoTransferenciaRenda.BolsaFamilia: return 44;
                case ETipoTransferenciaRenda.PETIProgramaErradicacaoTrabalhoInfantil: return 45;
                case ETipoTransferenciaRenda.AcaoJovem: return 47;
                case ETipoTransferenciaRenda.RendaCidada: return 48;
                case ETipoTransferenciaRenda.ProsperaFamilia: return 104;
                case ETipoTransferenciaRenda.FCadUnico: return 105;
                case ETipoTransferenciaRenda.FVigilancia: return 106;
                case ETipoTransferenciaRenda.AuxilioAluguel: return 107;
                case ETipoTransferenciaRenda.SaoPauloSolidario: return 49;
                case ETipoTransferenciaRenda.Outros: return 51;
                case ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCPessoaDeficiencia: return 59;
                case ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCIdoso: return 58;
            }
            return 0;
        }

        public Int32 GetNumeroBeneficiarios(TransferenciaRendaInfo transferenciaRenda,int? execicio)
        {
            TransferenciaRendaPrevisaoAnual transferenciaRendaPrevisaoAnual = new TransferenciaRendaPrevisaoAnual();
            var numeroBeneficiarios = 0;
            switch ((ETipoTransferenciaRenda)transferenciaRenda.IdTipoTransferenciaRenda)
            {
                case ETipoTransferenciaRenda.AcaoJovem:
                case ETipoTransferenciaRenda.RendaCidada:
                case ETipoTransferenciaRenda.ProJovemAdolescente:

                    if (execicio == 2021)
                    {
                        if (transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id) != null)
                        {
                            numeroBeneficiarios = transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).MetaPactuada2021.HasValue ? transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).MetaPactuada2021.Value : 0;    
                        }
                        
                    }

                    if (execicio == 2022)
                    {
                        if (transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id) != null)
                        {
                            numeroBeneficiarios = transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).MetaPactuada2022.HasValue ? transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).MetaPactuada2022.Value : 0;     
                        }
                        
                    }

                    if (execicio == 2023)
                    {
                        if (transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id) != null)
                        {
                            numeroBeneficiarios = transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).MetaPactuada2023.HasValue ? transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).MetaPactuada2023.Value : 0;    
                        }
                        
                    }

                    if (execicio == 2024)
                    {
                        if (transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id) != null)
                        {
                            numeroBeneficiarios = transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).MetaPactuada2024.HasValue ? transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).MetaPactuada2024.Value : 0;    
                        }
                        
                    }

                    if (execicio == 2025)
                    {
                        if (transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id) != null)
                        {
                            numeroBeneficiarios = transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).MetaPactuada2025.HasValue ? transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).MetaPactuada2025.Value : 0;    
                        }
                        
                    }

                    break;
                case ETipoTransferenciaRenda.SaoPauloSolidario:
                    numeroBeneficiarios = (transferenciaRenda.SaoPauloSolidarioNumeroFamiliasAgendaFamilia2012.HasValue ? transferenciaRenda.SaoPauloSolidarioNumeroFamiliasAgendaFamilia2012.Value : 0) + (transferenciaRenda.SaoPauloSolidarioNumeroFamiliasAgendaFamilia2013.HasValue ? transferenciaRenda.SaoPauloSolidarioNumeroFamiliasAgendaFamilia2013.Value : 0);
                    break;
                case ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCIdoso:
                case ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCPessoaDeficiencia:
                    if (execicio == 2021)
                    {
                        if (transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id) != null)
                        {
                            numeroBeneficiarios = transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).MetaPactuada2021.HasValue ? transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).MetaPactuada2021.Value : 0;    
                        }
                        
                    }

                    if (execicio == 2022)
                    {
                        if (transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id) != null)
                        {
                            numeroBeneficiarios = transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).MetaPactuada2022.HasValue ? transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).MetaPactuada2022.Value : 0;    
                        }
                        
                    }

                    if (execicio == 2023)
                    {
                        if (transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id) != null)
                        {
                            numeroBeneficiarios = transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).MetaPactuada2023.HasValue ? transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).MetaPactuada2023.Value : 0;    
                        }
                        
                    }

                    if (execicio == 2024)
                    {
                        if (transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id) != null)
                        {
                            numeroBeneficiarios = transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).MetaPactuada2024.HasValue ? transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).MetaPactuada2024.Value : 0;    
                        }
                        
                    }

                    if (execicio == 2025)
                    {
                        if (transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id) != null)
                        {
                            numeroBeneficiarios = transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).MetaPactuada2025.HasValue ? transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).MetaPactuada2025.Value : 0;    
                        }
                        
                    }
                    //numeroBeneficiarios = transferenciaRenda.BPCNumeroBeneficiarios.HasValue ? transferenciaRenda.BPCNumeroBeneficiarios.Value : 0;
                    break;
                case ETipoTransferenciaRenda.BolsaFamilia:

                    
                    if (execicio -1 == 2021)
                    {
                        if (transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id) != null)
                        {
                            numeroBeneficiarios = transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).NumeroFamiliasBeneficiarias2021.HasValue ? transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).NumeroFamiliasBeneficiarias2021.Value : 0;    
                        }
                        
                    }
                    if (execicio -1 == 2022)
                    {
                        if (transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id) != null)
                        {
                            numeroBeneficiarios = transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).NumeroFamiliasBeneficiarias2022.HasValue ? transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).NumeroFamiliasBeneficiarias2022.Value : 0;    
                        }
                        
                    }
                    if (execicio -1 == 2023)
                    {
                        if (transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id) != null)
                        {
                            numeroBeneficiarios = transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).NumeroFamiliasBeneficiarias2023.HasValue ? transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).NumeroFamiliasBeneficiarias2023.Value : 0;    
                        }
                        
                    }
                    if (execicio -1 == 2024)
                    {
                        if (transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id) != null)
                        {
                            numeroBeneficiarios = transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).NumeroFamiliasBeneficiarias2024.HasValue ? transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).NumeroFamiliasBeneficiarias2024.Value : 0;    
                        }
                        
                    }
                    if (execicio -1 == 2025)
                    {
                        if (transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id) != null)
                        {
                            numeroBeneficiarios = transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).NumeroFamiliasBeneficiarias2025.HasValue ? transferenciaRendaPrevisaoAnual.GetByTransferenciaRenda(transferenciaRenda.Id).NumeroFamiliasBeneficiarias2025.Value : 0;    
                        }
                        
                    }

                    break;

                //case ETipoTransferenciaRenda.PETIProgramaErradicacaoTrabalhoInfantil: numeroBeneficiarios = transferenciaRenda.PETINumeroBeneficiarios.HasValue ? transferenciaRenda.PETINumeroBeneficiarios.Value : 0 
                case ETipoTransferenciaRenda.PETIProgramaErradicacaoTrabalhoInfantil: numeroBeneficiarios = (transferenciaRenda.PETINumeroBeneficiarioPETIPuroRural.HasValue ? transferenciaRenda.PETINumeroBeneficiarioPETIPuroRural.Value : 0)
                    + (transferenciaRenda.PETINumeroBeneficiarioPETIPuroUrbano.HasValue ? transferenciaRenda.PETINumeroBeneficiarioPETIPuroUrbano.Value : 0)
                    + (transferenciaRenda.PETINumeroBeneficiarioBolsaFamilia.HasValue ? transferenciaRenda.PETINumeroBeneficiarioBolsaFamilia.Value : 0)
                    + (transferenciaRenda.PETINumeroBeneficiarioProgramaMunicipal.HasValue ? transferenciaRenda.PETINumeroBeneficiarioProgramaMunicipal.Value : 0);

                    if (execicio - 1 == 2021)
                    {
                        numeroBeneficiarios = transferenciaRenda.PetiIndicadores.MetaMunicipal2021.HasValue ? transferenciaRenda.PetiIndicadores.MetaMunicipal2021.Value : 0 ;
                    }

                    if (execicio - 1 == 2022)
                    {
                        numeroBeneficiarios = transferenciaRenda.PetiIndicadores.MetaMunicipal2022.HasValue ? transferenciaRenda.PetiIndicadores.MetaMunicipal2022.Value : 0;
                    }

                    if (execicio - 1 == 2023)
                    {
                        numeroBeneficiarios = transferenciaRenda.PetiIndicadores.MetaMunicipal2023.HasValue ? transferenciaRenda.PetiIndicadores.MetaMunicipal2023.Value : 0;
                    }

                    if (execicio - 1 == 2024)
                    {
                        numeroBeneficiarios = transferenciaRenda.PetiIndicadores.MetaMunicipal2024.HasValue ? transferenciaRenda.PetiIndicadores.MetaMunicipal2024.Value : 0;
                    }

                    break;
                case ETipoTransferenciaRenda.Outros: numeroBeneficiarios = transferenciaRenda.MunicipaisNumeroBeneficiarios.HasValue ? transferenciaRenda.MunicipaisNumeroBeneficiarios.Value : 0;
                    break;

            }
            return numeroBeneficiarios;
        }

        public Boolean ProgramaAderido(TransferenciaRendaInfo transferenciaRenda)
        {
            return transferenciaRenda.BPCNumeroBeneficiarios.HasValue || transferenciaRenda.BolsaFamiliaEstimativaFamilias.HasValue || transferenciaRenda.BolsaFamiliaNumeroFamilias.HasValue
                || transferenciaRenda.BolsaFamiliaRepasseMensal.HasValue || transferenciaRenda.PETINumeroBeneficiarioPETIPuroRural.HasValue || transferenciaRenda.PETINumeroBeneficiarioPETIPuroUrbano.HasValue
                || transferenciaRenda.AcaoRendaMeta.HasValue || transferenciaRenda.MunicipaisNumeroBeneficiarios.HasValue || transferenciaRenda.IdFaseProgramaSaoPauloSolidario.HasValue;
        }


        public List<String> GetLabelForInfo(List<String> propriedades, TransferenciaRendaInfo obj)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "Nome": labels.Add("nome"); break;
                    case "Objetivo": labels.Add("objetivo"); break;
                    case "IdUsuarioTransferenciaRenda": labels.Add("beneficiários"); break;
                    case "BPCNumeroBeneficiarios": labels.Add("número de beneficiários"); break;
                    case "BolsaFamiliaEstimativaFamilias": labels.Add("estimativa de famílias no perfil Bolsa Família"); break;
                    case "BolsaFamiliaNumeroFamilias": labels.Add("número de famílias beneficiárias"); break;
                    case "BolsaFamiliaRepasseMensal": labels.Add("repasse mensal"); break;
                    case "PETINumeroBeneficiarios": labels.Add("número de beneficiários"); break;
                    case "PETIPrevisaoMensal": labels.Add("previsão mensal do valor do repasse para ações socioeducativas"); break;
                    case "AcaoRendaMeta": labels.Add("meta pactuada para 2014"); break;
                    case "MunicipaisNumeroBeneficiarios": labels.Add("número de beneficiários"); break;
                    case "MunicipaisRepasse": labels.Add("previsão mensal do valor do repasse"); break;
                    case "IdFaseProgramaSaoPauloSolidario": labels.Add("fase do programa"); break;
                    case "SaoPauloSolidarioMesInicioBuscaAtiva":
                    case "SaoPauloSolidarioAnoInicioBuscaAtiva":
                        labels.Add("data de início de realização da busca ativa"); break;
                    case "SaoPauloSolidarioMesTerminoBuscaAtiva":
                    case "SaoPauloSolidarioAnoTerminoBuscaAtiva":
                        labels.Add("data de término de realização da busca ativa"); break;
                    case "SaoPauloSolidarioOrgaoGestorExecutaBuscaAtiva":
                    case "SaoPauloSolidarioCRASExecutaBuscaAtiva":
                    case "SaoPauloSolidarioCREASExecutaBuscaAtiva":
                    case "SaoPauloSolidarioUnidadePrivadaExecutaBuscaAtiva":
                        labels.Add("órgãos que executaram a busca ativa"); break;
                    case "SaoPauloSolidarioValorFMASBuscaAtiva": labels.Add("valor do FMAS para a busca ativa"); break;
                    case "SaoPauloSolidarioValorOrcamentoMunicipalBuscaAtiva": labels.Add("valor do orçamento municipal para a busca ativa"); break;
                    case "SaoPauloSolidarioValorFEASBuscaAtiva": labels.Add("valor do FEAS para a busca ativa"); break;
                    case "SaoPauloSolidarioValorFNASBuscaAtiva": labels.Add("valor do FNAS para a busca ativa"); break;
                    case "SaoPauloSolidarioValorIGDPBFBuscaAtiva": labels.Add("valor do IGD-PBF para a busca ativa"); break;
                    case "SaoPauloSolidarioValorIGDSUASBuscaAtiva": labels.Add("valor do IGD-SUAS para a busca ativa"); break;
                    case "SaoPauloSolidarioNumeroFamiliasAgendaFamilia2012": if (obj.IdFaseProgramaSaoPauloSolidario.HasValue && obj.IdFaseProgramaSaoPauloSolidario.Value == 2) labels.Add("quantidade de famílias que assinaram o termo da agenda da família em 2012"); break;
                    case "SaoPauloSolidarioNumeroFamiliasAgendaFamilia2013": if (obj.IdFaseProgramaSaoPauloSolidario.HasValue && obj.IdFaseProgramaSaoPauloSolidario.Value == 2) labels.Add("quantidade de famílias que assinaram o termo da Agenda da família em 2013"); break;
                    case "SaoPauloSolidarioValorFMASAgendaFamilia": if (obj.IdFaseProgramaSaoPauloSolidario.HasValue && obj.IdFaseProgramaSaoPauloSolidario.Value == 2) labels.Add("valor do FMAS para a agenda da família"); break;
                    case "SaoPauloSolidarioValorOrcamentoMunicipalAgendaFamilia": if (obj.IdFaseProgramaSaoPauloSolidario.HasValue && obj.IdFaseProgramaSaoPauloSolidario.Value == 2) labels.Add("valor do orçamento municipal para a agenda da família"); break;
                    case "SaoPauloSolidarioValorFEASAgendaFamilia": labels.Add("valor do FEAS para a agenda da família"); break;
                    case "SaoPauloSolidarioValorFNASAgendaFamilia": if (obj.IdFaseProgramaSaoPauloSolidario.HasValue && obj.IdFaseProgramaSaoPauloSolidario.Value == 2) labels.Add("valor do FNAS para a agenda da família"); break;
                    case "SaoPauloSolidarioValorIGDPBFAgendaFamilia": if (obj.IdFaseProgramaSaoPauloSolidario.HasValue && obj.IdFaseProgramaSaoPauloSolidario.Value == 2) labels.Add("valor do IGD-PBF para a agenda da família"); break;
                    case "SaoPauloSolidarioValorIGDSUASAgendaFamilia": if (obj.IdFaseProgramaSaoPauloSolidario.HasValue && obj.IdFaseProgramaSaoPauloSolidario.Value == 2) labels.Add("valor do IGD-SUAS para a agenda da família"); break;
                    case "SaoPauloSolidarioOrgaoGestorExecutaAgendaFamilia":
                    case "SaoPauloSolidarioCRASExecutaAgendaFamilia":
                    case "SaoPauloSolidarioCREASExecutaAgendaFamilia":
                        if (obj.IdFaseProgramaSaoPauloSolidario.HasValue && obj.IdFaseProgramaSaoPauloSolidario.Value == 2) labels.Add("órgãos que executam a agenda da família"); break;
                    case "SaoPauloSolidarioMeta": labels.Add("meta"); break;
                    case "SaoPauloSolidarioRepasseAnual": labels.Add("valor do repasse anual"); break;
                    case "PossuiParceriaFormal": labels.Add("parcerias"); break;
                    case "BeneficiarioAtendidoRedeSocioAssistencial": labels.Add("o beneficiário está sendo atendido na rede de serviços socioassistenciais"); break;
                    case "SaoPauloSolidarioValorFEASRetidoFMAS2013": labels.Add("valor do FEAS foi retido no FMAS para utilização em 2013 para a busca ativa"); break;
                    case "SaoPauloSolidarioMesRepasseFEASBuscaAtiva": labels.Add("mês em que foi realizado o repasse do FEAS para a busca ativa"); break;
                    case "SaoPauloSolidarioAnoRepasseFEASBuscaAtiva": labels.Add("ano em que foi realizado o repasse do FEAS para a busca ativa"); break;
                    case "SaoPauloSolidarioMesRepasseFEASAgendaFamilia": labels.Add("mês em que foi realizado o repasse do FEAS para a agenda da família"); break;
                    case "SaoPauloSolidarioAnoRepasseFEASAgendaFamilia": labels.Add("ano em que foi realizado o repasse do FEAS para a agenda da família"); break;

                    case "PETINumeroTrabalhoInfantilCadUnico": labels.Add("Número de crianças e adolescentes em situação de trabalho infantil identificadas no CAD Único:"); break;
                    case "PETIBeneficiarioTransferenciaRenda": labels.Add("beneficiários inseridos no CadÚnico recebem algum tipo de transferência de renda"); break;
                    case "PETIBeneficiarioBolsaFamilia": labels.Add("beneficiários inseridos no CadÚnico recebem transferência de renda do Bolsa Família"); break;
                    case "PETINumeroBeneficiarioBolsaFamilia": labels.Add("número de beneficiários no CadÚnico do Bolsa Família"); break;
                    case "PETIBeneficiarioPETIPuro": labels.Add("beneficiários inseridos no CadÚnico recebem transferência de renda do PETI Puro"); break;
                    case "PETINumeroBeneficiarioPETIPuroUrbano": labels.Add("número de beneficiários no CadÚnico do PETI Puro Urbano"); break;
                    case "PETINumeroBeneficiarioPETIPuroRural": labels.Add("número de beneficiários no CadÚnico do PETI Puro Rural"); break;
                    case "PETIBeneficiarioProgramaMunicipal": labels.Add("beneficiários inseridos no CadÚnico recebem transferência de renda de Programas Municipais"); break;
                    case "PETINumeroBeneficiarioProgramaMunicipal": labels.Add("número de beneficiários no CadÚnico dos Programas Municipais"); break;
                    case "PETIAderiuCofinanciamentoFederal": labels.Add("município aderiu ao cofinanciamento federal para desenvolver ações estratégicas do PETI"); break;
                    case "PETIDataAdesao": labels.Add("Data de adesão ao PETI"); break;
                    case "PETIAcoesTrabalhoInfantil": labels.Add("município realiza/planeja ações com vistas à erradicação do trabalho infantil"); break;
                    case "ValorFMAS": labels.Add("valor FMAS"); break;
                    case "ValorOrcamentoMunicipal": labels.Add("valor orçamento municipal"); break;
                    case "ValorFundoMunicipal": labels.Add("valor outros fundos municipais"); break;
                    case "ValorFEAS": labels.Add("valor FEAS"); break;
                    case "ValorOrcamentoEstadual": labels.Add("valor orçamento estadual"); break;
                    case "ValorFundoEstadual": labels.Add("valor outros fundos estaduais"); break;
                    case "ValorFNAS": labels.Add("valor FNAS"); break;
                    case "ValorOrcamentoFederal": labels.Add("valor orçamento federal"); break;
                    case "ValorFundoFederal": labels.Add("valor outros fundos nacionais"); break;
                    case "ValorIGDPBF": labels.Add("valor IGD-PBF"); break;
                    case "ValorIGDSUAS": labels.Add("valor IGD-SUAS"); break;
                }
            }
            return labels.Distinct().ToList();
        }
    }
}
