using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seds.PMAS.Dominio.Entities
{
    public class ConsultaUsuarioEntity
    {
        public Int32 IdUsuario { get; set; }

        public Int32 IdPerfil { get; set; }

        public String Perfil { get; set; }
        
        public String Nome { get; set; }
        
        public String Login { get; set; }
        
        public String RG { get; set; }
        
        public String Municipio { get; set; }
        
        public String Drads { get; set; }
        
        public Int32? IdMunicipio { get; set; }
        
        public Int32? IdDrads { get; set; }
        
        public String Situacao { get; set; }
        
        public String Instituicao { get; set; }
        
        public String Cargo { get; set; }
    }
}
