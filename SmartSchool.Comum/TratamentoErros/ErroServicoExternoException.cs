using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace SmartSchool.Comum.TratamentoErros
{
    public class ErroServicoExternoException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; private set; }
        public object ObjetoErro { get; private set; }
        public ErroServicoExternoException(string message, object objeto, HttpStatusCode httpStatusCode) : base(message)
        {
            this.HttpStatusCode = httpStatusCode;
            this.ObjetoErro = objeto;
        }

        public string ObterMensagemErroCompleta()
        {
            var builder = new StringBuilder();

            builder.Append(this.Message);
            builder.Append(Environment.NewLine);
            builder.Append($"StatusCode: {HttpStatusCode}");

            if (ObjetoErro != null)
            {
                builder.Append(Environment.NewLine);
                var objeto = JsonConvert.SerializeObject(ObjetoErro);
                builder.Append(objeto);
            }
            
            return builder.ToString();
        }
    }
}
