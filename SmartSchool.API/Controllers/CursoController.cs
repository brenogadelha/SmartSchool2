using Microsoft.AspNetCore.Mvc;
using SmartSchool.Aplicacao.Cursos.Interface;
using SmartSchool.Dto.Curso;
using SmartSchool.Dto.Dtos.TratamentoErros;
using System;
using System.Collections.Generic;
using System.Net;

namespace SmartSchool.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CursoController : Controller
	{
		private readonly ICursoServico _cursoServico;

		public CursoController(ICursoServico cursoServico)
		{
			this._cursoServico = cursoServico;
		}

		/// <summary>
		/// Obtem listagem de todos os Cursos cadastrados
		/// </summary>
		/// <returns>Lista de todos os Cursos</returns>
		/// <response code="200">Lista de Cursos</response> 
		/// <response code="500">Erro inesperado</response>
		[ProducesResponseType(200, Type = typeof(IEnumerable<CursoDto>))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		[HttpGet]
		public OkObjectResult ObterTodos() => Ok(_cursoServico.Obter());

		/// <summary>
		/// Obtém dados do Curso específico por ID
		/// </summary>
		/// <returns>Dados do Curso solicitado</returns>
		/// <response code="200">Obtem dados do Curso solicitado</response>
		/// <response code="404">Curso inexistente</response>
		/// <response code="500">Erro inesperado</response>
		[ProducesResponseType(200, Type = typeof(CursoDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		[HttpGet("{id}")]
		public OkObjectResult ObterPorId(Guid id) => Ok(this._cursoServico.ObterPorId(id));


		/// <summary>
		/// Cria um novo Curso
		/// </summary>
		/// <returns>Http status 201(Created)</returns>
		/// <response code="201">Curso criado com sucesso</response>
		/// <response code="400">Dados inconsistentes para criação do Curso</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public StatusCodeResult CriarCurso([FromBody] CursoDto cursoDto)
		{
			this._cursoServico.CriarCurso(cursoDto);

			return this.StatusCode((int)HttpStatusCode.Created);
		}

		/// <summary>
		/// Efetua alteração de Curso
		/// </summary>
		/// <returns>Http status 204(No Content)</returns>
		/// <response code="204">Curso alterado com Sucesso</response>
		/// <response code="400">Dados para alteração de Curso inconsistentes.</response>
		/// <response code="404">Curso inexistente</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpPut("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public StatusCodeResult AlterarCurso(Guid id, [FromBody] AlterarCursoDto cursoDto, [FromQuery(Name = "atualizarDisciplinas")] bool? atualizarDisciplinas = null)
		{
			if (cursoDto == null)
				throw new ArgumentNullException(null, "Objeto Curso nulo (não foi informado).");

			if (id.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Identificador de Curso é inválido ou nulo");

			cursoDto.ID = id;

			this._cursoServico.AlterarCurso(id, cursoDto, atualizarDisciplinas);

			return this.StatusCode((int)HttpStatusCode.Created);
		}

		/// <summary>
		/// Exclui um Curso específico
		/// </summary>
		/// <response code="204">Curso excluído com Sucesso</response>
		/// <response code="400">Dados para exclusão da Curso inconsistentes.</response>
		/// <response code="404">Curso inexistente</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpDelete("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public StatusCodeResult ExcluirCurso(Guid id)
		{
			this._cursoServico.Remover(id);
			return this.StatusCode(204);
		}
	}
}
