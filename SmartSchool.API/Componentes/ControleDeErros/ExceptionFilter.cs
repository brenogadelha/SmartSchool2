using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using SmartSchool.Comum.Serializacao;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dto.Dtos.TratamentoErros;
using System;
using System.Collections.Generic;
using System.Net;

namespace SmartSchool.API.Componentes.ControleDeErros
{
	public class ExceptionFilter : IExceptionFilter
	{
		public ExceptionFilter() { }

		public void OnException(ExceptionContext context)
		{
			string identificador = DateTime.Now.ToString("HHmmssfff");

			var url = string.Concat(context.HttpContext.Request.Scheme, "://", context.HttpContext.Request.Host, context.HttpContext.Request.Path);

			var trace = new Dictionary<string, string>();
			trace.Add("Identificador do Erro", identificador);
			trace.Add("Url Acessada", url);

			var exceptionType = context.Exception.GetType();
			var mensagemErro = context.Exception.Message;

			if (exceptionType == typeof(AggregateException) && ((AggregateException)context.Exception).InnerExceptions.Count == 1)
			{
				exceptionType = context.Exception.InnerException.GetType();
				mensagemErro = context.Exception.InnerException.Message;
			}

			var tratamentoErro = new TratamentoErroDto
			{
				Mensagem = mensagemErro
			};

			HttpStatusCode status;

			if (exceptionType == typeof(UnauthorizedAccessException))
				status = HttpStatusCode.Unauthorized;
			else if (exceptionType == typeof(RecursoInexistenteException))
				status = HttpStatusCode.NotFound;
			else if (exceptionType == typeof(ErroRequisicaoException) ||
					exceptionType == typeof(ArgumentException) ||
					exceptionType == typeof(ArgumentNullException))
				status = HttpStatusCode.BadRequest;
			else if (exceptionType == typeof(ErroNegocioException))
				status = HttpStatusCode.UnprocessableEntity;

			else if (exceptionType == typeof(ErroServicoExternoException))
			{
				status = HttpStatusCode.BadGateway;
				mensagemErro = ((ErroServicoExternoException)context.Exception).ObterMensagemErroCompleta();
			}
			else if (exceptionType == typeof(ErroDeSistemaException))
			{
				status = HttpStatusCode.InternalServerError;
				mensagemErro = ((ErroDeSistemaException)context.Exception).ObterMensagemErroCompleta();
			}
			else if (exceptionType == typeof(ErroExcessoDeRequisicoes))
			{
				status = HttpStatusCode.TooManyRequests;
			}
			else
			{
				tratamentoErro.Mensagem = $"Ocorreu um erro inesperado: '{context.Exception.InnerException.Message}'.";
				status = HttpStatusCode.InternalServerError;
			}

			tratamentoErro.Codigo = int.Parse(identificador);

			context.ExceptionHandled = true;
			HttpResponse response = context.HttpContext.Response;
			response.StatusCode = (int)status;
			response.ContentType = "application/json";

			response.WriteAsync(tratamentoErro.ConverterEmJson());
		}
	}
}
