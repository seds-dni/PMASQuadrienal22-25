
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class InterfacePublicaOutraPolitica
    {
        private static IRepository<InterfacePublicaOutraPoliticaInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<InterfacePublicaOutraPoliticaInfo>>();
            }
        }

        public InterfacePublicaOutraPoliticaInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }

        public InterfacePublicaOutraPoliticaInfo GetByPrefeitura(int idPrefeitura)
        {
            return _repository.GetObjectSet().Include("OutrosServicos").FirstOrDefault(m => m.IdPrefeitura == idPrefeitura);
        }

        public void Add(InterfacePublicaOutraPoliticaInfo obj, Boolean commit)
        {
            new ValidadorInterfacePublica().ValidarOutraPolitica(obj);
            _repository.Add(obj);

            if (obj.ExisteOutraPoliticaPublica.Value && obj.OutrosServicos != null && obj.OutrosServicos.Count > 0)
            {
                var outraPolitica = new OutraPolitica();
                obj.OutrosServicos.ForEach(r =>
                {
                    r.IdInterfacePublicaOutraPolitica = obj.Id;
                    outraPolitica.Add(r, false);
                });
            }

            if (commit)
                ContextManager.Commit();
        }

        public void Update(InterfacePublicaOutraPoliticaInfo obj, Boolean commit)
        {
            new ValidadorInterfacePublica().ValidarOutraPolitica(obj);


            //var propriedadesEntity = _repository.GetModifiedProperties(obj);
            var propriedades = new List<string>();
            //   propriedades = GetLabelForInfo(propriedadesEntity);
            var lstDeletedOutrasPoliticas = new List<InterfacePublicaOutroServicoInfo>();
            var outraPolitica = new OutraPolitica();
            var lstOutrosServicos = outraPolitica.GetByInterfacePolitica(obj.Id).ToList();
            var hasChangeOutrasPoliticas = false;

            foreach (var p in lstOutrosServicos)
            {
                if (!obj.OutrosServicos.Any(r => r.Id == p.Id))
                {
                    hasChangeOutrasPoliticas = true;
                    lstDeletedOutrasPoliticas.Add(p);
                }
            }

            foreach (var p in lstDeletedOutrasPoliticas)
                outraPolitica.Delete(p, true);

            if (obj.OutrosServicos != null)
                foreach (var p in obj.OutrosServicos)
                {
                    p.IdInterfacePublicaOutraPolitica = obj.Id;
                    if (p.Id == 0)
                    {
                        outraPolitica.Add(p, true);
                        hasChangeOutrasPoliticas = true;
                    }
                    if (hasChangeOutrasPoliticas)
                        propriedades.Add("Outros Serviços");
                }


            _repository.Update(obj);

            if (commit)
                ContextManager.Commit();
        }

        private List<string> GetLabelForInfo(List<string> propriedades)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "Restaurantes Populares": labels.Add("Restaurantes Populares cadastrados"); break;

                }
            }
            return labels.Distinct().ToList();
        }
    }
}
