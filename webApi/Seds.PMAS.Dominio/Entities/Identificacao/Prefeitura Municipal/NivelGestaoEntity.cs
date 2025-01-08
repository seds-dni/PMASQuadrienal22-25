using System;
using System.Collections.Generic;

namespace Seds.PMAS.Dominio.Entities
{
    public partial class NivelGestaoEntity
    {
        public NivelGestaoEntity()
        {
            this.Prefeitura = new List<PrefeituraEntity>();
        }

        public Int32 Id { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<PrefeituraEntity> Prefeitura { get; set; }
    }
}
