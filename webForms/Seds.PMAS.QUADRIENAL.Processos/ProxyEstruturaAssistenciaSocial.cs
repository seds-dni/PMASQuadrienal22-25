using System;
using System.Collections.Generic;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.UI.Processos
{
    public class ProxyEstruturaAssistenciaSocial : IDisposable
    {
       public Seds.PMAS.QUADRIENAL.Servicos.EstruturaAssistenciaSocialService Service { get; set; }
        public ProxyEstruturaAssistenciaSocial()
        {
            Service = new Servicos.EstruturaAssistenciaSocialService();
        }

        ~ProxyEstruturaAssistenciaSocial()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Service = null;
        }

        public List<TipoAtendimentoInfo> GetTipoAtendimento() 
        {
            var lst = new List<TipoAtendimentoInfo>();
            lst.Add(new TipoAtendimentoInfo() { Id = 1, TipoAtendimento = "Eventual"});
            lst.Add(new TipoAtendimentoInfo() { Id = 2, TipoAtendimento =  "Convênio ou outro tipo de acordo formal" });
                return lst;
        }

        public List<NivelGestaoInfo> GetNivelGestao()
        {
            var lst = new List<NivelGestaoInfo>();
            lst.Add(new NivelGestaoInfo() { Id = 0, Nome = "Básica" });
            lst.Add(new NivelGestaoInfo() { Id = 1, Nome = "Inicial" });
            lst.Add(new NivelGestaoInfo() { Id = 2, Nome = "Plena" });
            lst.Add(new NivelGestaoInfo() { Id = 3, Nome = "Não habilitado" });
            return lst;
        }

        public List<SexoInfo> GetSexo()
        {
            var lst = new List<SexoInfo>();
            lst.Add(new SexoInfo() { Id = 1, Nome = "Feminino" });
            lst.Add(new SexoInfo() { Id = 2, Nome = "Masculino" });
            lst.Add(new SexoInfo() { Id = 3, Nome = "Ambos os sexos" });            
            return lst;
        }

        public List<RegiaoMoradiaInfo> GetRegioesMoradia()
        {
            var lst = new List<RegiaoMoradiaInfo>();
            lst.Add(new RegiaoMoradiaInfo() { Id = 1, Nome = "Zona urbana" });
            lst.Add(new RegiaoMoradiaInfo() { Id = 2, Nome = "Zona rural" });
            lst.Add(new RegiaoMoradiaInfo() { Id = 3, Nome = "Ambas" });
            return lst;
        }

        public List<CaracteristicasTerritorioInfo> GetCaracteristicasTerritorio()
        {
            var lst = new List<CaracteristicasTerritorioInfo>();
            lst.Add(new CaracteristicasTerritorioInfo() { Id = 1, Nome = "Assentamentos" });
            lst.Add(new CaracteristicasTerritorioInfo() { Id = 2, Nome = "Comunidade indígena" });
            lst.Add(new CaracteristicasTerritorioInfo() { Id = 3, Nome = "Comunidade quilombola" });
            lst.Add(new CaracteristicasTerritorioInfo() { Id = 7, Nome = "Morador de habitação subnormal" });
            lst.Add(new CaracteristicasTerritorioInfo() { Id = 8, Nome = "Nenhuma das características citadas" });
            lst.Add(new CaracteristicasTerritorioInfo() { Id = 9, Nome = "População ribeirinha/calhas de rios" });
            lst.Add(new CaracteristicasTerritorioInfo() { Id = 10, Nome = "Adensamento populacional decorrente de instalação prisional" });
            lst.Add(new CaracteristicasTerritorioInfo() { Id = 11, Nome = "Adensamento populacional decorrente de trabalhos sazonais" });
            return lst;
        }
    
    
    
    }


}
