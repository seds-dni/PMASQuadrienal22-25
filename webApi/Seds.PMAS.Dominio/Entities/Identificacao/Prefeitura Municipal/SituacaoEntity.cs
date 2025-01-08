using System;
using System.Collections.Generic;

namespace Seds.PMAS.Dominio.Entities
{
    public partial class SituacaoEntity
    {
        public SituacaoEntity()
        {
            this.Prefeituras = new List<PrefeituraEntity>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<PrefeituraEntity> Prefeituras { get; set; }
    }
}
