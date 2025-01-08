using System;

namespace Seds.PMAS.Dominio.Entities
{
    public class RecursoPerfilEntity
    {
        public Int32 IdRecurso { get; set; }
        public Int32 IdPerfil { get; set; }
        public RecursoEntity Recurso { get; set; }
    }
}
