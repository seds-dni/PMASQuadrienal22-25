using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;


namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class ComentarioPrestacaoDeContasDRADSInfo
    {
        [Key]
        [DataMember]
        public Int32 IdPrefeitura { get; set; }

        [Key]
        [DataMember]
        public Int32 Exercicio { get; set; }

        [DataMember]
        public String Comentario { get; set; }
        public Int32? IdSituacao { get; set; }
        public bool? Desbloqueado { get; set; }

    }
}
