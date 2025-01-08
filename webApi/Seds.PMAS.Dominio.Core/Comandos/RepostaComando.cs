using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seds.PMAS.Dominio.Core.Comandos
{
   public class RepostaComando
    {
         public static RepostaComando Ok = new RepostaComando { Successo = true };
        public static RepostaComando Fail = new RepostaComando { Successo = false };

        public RepostaComando(bool successo = false)
        {
            Successo = successo;
        }

        public bool Successo { get; private set; }
    }
}
