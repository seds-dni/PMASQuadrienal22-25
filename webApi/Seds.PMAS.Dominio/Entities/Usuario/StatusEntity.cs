using System;
using System.Collections.Generic;

namespace  Seds.PMAS.Dominio.Entities
{
    public partial class StatusEntity
    {
        public StatusEntity()
        {
            this.Usuarios = new List<UsuarioEntity>();
        }

        public Int16 Id { get; set; }
        public String Nome { get; set; }
        public virtual ICollection<UsuarioEntity> Usuarios { get; set; }
    }
}
