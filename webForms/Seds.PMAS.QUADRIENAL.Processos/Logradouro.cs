using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seds.PMAS.QUADRIENAL.UI.Processos
{
    /// <summary>
    /// Representa um Endereço.
    /// </summary>
    [Serializable]
    public struct Logradouro
    {

        /// <summary>
        /// Campo tipo do logradouro.
        /// </summary>
        private string nome;
        private string numero;
        private string complemento;
        private string bairro;
        private string cep;
        private string cidade;

        /// <summary>
        /// Contrutor Logradouro completo.
        /// </summary>
        /// <param name="nome">Nome do logradouro.</param>
        /// <param name="numero">Número do logradouro.</param>
        /// <param name="complemento">Complemento do logradouro.</param>
        /// <param name="bairro">Bairro do logradouro.</param>
        /// <param name="cep">CEP do logradouro.</param>
        public Logradouro(string nome,
            string numero,
            string complemento,
            string bairro,
            string cep, string cidade)
        {
            this.nome = nome;
            this.numero = numero;
            this.complemento = complemento;
            this.bairro = bairro;
            this.cep = cep;
            this.cidade = cidade;
        }

        /// <summary>
        /// Construtor Logradouro com os campos obrigatórios.
        /// </summary>
        /// <param name="nome">Nome do logradouro.</param>
        /// <param name="cep">CEP do logradouro.</param>
        public Logradouro(string cep)
        {
            this.nome = "";
            this.numero = "";
            this.complemento = "";
            this.bairro = "";
            this.cep = cep;
            this.cidade = "";
        }

        /// <summary>
        /// Nome do logradouro.
        /// </summary>
        public string Nome
        {
            get
            {
                return nome;
            }
            set
            {
                nome = value;
            }
        }

        /// <summary>
        /// Número do logradouro.
        /// </summary>
        public string Numero
        {
            get
            {
                return numero;
            }
            set
            {
                numero = value;
            }
        }

        /// <summary>
        /// Complemento do logradouro.
        /// </summary>
        public string Complemento
        {
            get
            {
                return complemento;
            }
            set
            {
                complemento = value;
            }
        }

        /// <summary>
        /// Bairro do logradouro.
        /// </summary>
        public string Bairro
        {
            get
            {
                return bairro;
            }
            set
            {
                bairro = value;
            }
        }

        /// <summary>
        /// CEP do logradouro.
        /// </summary>
        public string Cep
        {
            get
            {
                return cep;
            }
            set
            {
                /*if( value.Trim().Length == 9 )
                    cep = value;
                else
                    throw new Exception("Formato do CEP está inválido. Formato Padrão: (99999-999)");
                    */
                cep = value;
            }
        }

        public string Cidade
        {
            get { return cidade; }
            set { cidade = value; }
        }
                
    }
}