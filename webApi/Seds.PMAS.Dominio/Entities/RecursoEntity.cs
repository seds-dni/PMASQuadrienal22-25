using System;
using System.Collections.Generic;

namespace Seds.PMAS.Dominio.Entities
{
    public class RecursoEntity
    {
        public RecursoEntity()
        {
            //this.TB_RECURSO1 = new List<TB_RECURSO>();
            Perfis = new List<RecursoPerfilEntity>();
        }

        public Int32 Id { get; set; }
        public string Nome { get; set; }
        public string Pagina { get; set; }
        public Int32? IdPai { get; set; }
        public Int32 Ordem { get; set; }
        //public virtual ICollection<TB_RECURSO> TB_RECURSO1 { get; set; }
        //public virtual TB_RECURSO TB_RECURSO2 { get; set; }
        public virtual ICollection<RecursoPerfilEntity> Perfis { get; set; }
    }
}
