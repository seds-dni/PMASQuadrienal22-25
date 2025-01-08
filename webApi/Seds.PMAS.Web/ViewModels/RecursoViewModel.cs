using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seds.PMAS.Web.ViewModels
{
    public class RecursoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Pagina { get; set; }
        public int? IdPai { get; set; }
        public int Ordem { get; set; }
        //public virtual IEnumerable<RecursoPerfilEntity> Perfis { get; set; }
    }
}