using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Componentes;
using SmartSchool.Aplicacao.Cursos.Adicionar;
using SmartSchool.Aplicacao.Cursos.Alterar;
using SmartSchool.Aplicacao.Cursos.Listar;
using SmartSchool.Aplicacao.Cursos.ObterPorId;
using SmartSchool.Aplicacao.Cursos.Remover;
using SmartSchool.Dto.Curso;
using SmartSchool.Dto.Dtos.TratamentoErros;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSchool.API.Controllers
{
	[Produces("application/json")]
	[Route("api/Cursos")]
	[EnableCors("PoliticaSmartSchool")]
	public class CursoController : Controller
	{
		private readonly IMediator _mediator;

		public CursoController(IMediator mediator)
		{
			this._mediator = mediator;
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
		public async Task<IActionResult> ObterTodos()
		{
			var response = await _mediator.Send(new ListarCursosCommand());

			return this.ProcessResult(response);
		}

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
		public async Task<IActionResult> ObterPorId([FromRoute(Name = "id")] Guid id)
		{
			var response = await _mediator.Send(new ObterCursoCommand { Id = id });

			return this.ProcessResult(response);
		}


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
		public async Task<IActionResult> CriarCurso([FromBody] AdicionarCursoCommand cursoDto)
		{
			var response = await _mediator.Send(cursoDto);

			return this.ProcessResult(response);
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
		public async Task<IActionResult> AlterarCurso(Guid id, [FromBody] AlterarCursoCommand cursoDto)
		{
			if (cursoDto == null)
				throw new ArgumentNullException(null, "Objeto Curso nulo (não foi informado).");

			if (id.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Identificador de Curso é inválido ou nulo");

			cursoDto.ID = id;

			var response = await _mediator.Send(cursoDto);

			return this.ProcessResult(response);
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
		public async Task<IActionResult> RemoverCurso(Guid id)
		{
			var response = await this._mediator.Send(new RemoverCursoCommand { ID = id });
			return this.ProcessResult(response);
		}
	}
}
