using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;

namespace SmartSchool.Comum.TratamentoErros
{
	public class ErroDeSistemaException : Exception
    {
        public ErroDeSistemaException(string message) : base(message)
        {
        }

		public HttpStatusCode HttpStatusCode { get; private set; }
		public object ObjetoErro { get; private set; }

		public ErroDeSistemaException(string message, object objeto, HttpStatusCode httpStatusCode) : base(message)
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
