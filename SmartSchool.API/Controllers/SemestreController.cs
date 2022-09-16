using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Componentes;
using SmartSchool.Aplicacao.Semestres.Adicionar;
using SmartSchool.Aplicacao.Semestres.Alterar;
using SmartSchool.Aplicacao.Semestres.Listar;
using SmartSchool.Aplicacao.Semestres.ObterPorId;
using SmartSchool.Aplicacao.Semestres.Remover;
using SmartSchool.Dto.Dtos.TratamentoErros;
using SmartSchool.Dto.Semestres;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSchool.API.Controllers
{
	[Produces("application/json")]
	[Route("api/Semestres")]
	[EnableCors("PoliticaSmartSchool")]
	public class SemestreController : Controller
	{
		private readonly IMediator _mediator;

		public SemestreController(IMediator mediator)
		{
			this._mediator = mediator;
		}

		/// <summary>
		/// Obtem listagem de todos os Semestre cadastrados
		/// </summary>
		/// <returns>Lista de todos os Semestre</returns>
		/// <response code="200">Lista de Semestre</response> 
		/// <response code="500">Erro inesperado</response>
		[ProducesResponseType(200, Type = typeof(IEnumerable<ObterSemestreDto>))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		[HttpGet]
		public async Task<IActionResult> ObterTodos()
		{
			var response = await _mediator.Send(new ListarSemestresCommand());

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Obtém dados do Semestresespecífico por ID
		/// </summary>
		/// <returns>Dados do Semestre solicitado</returns>
		/// <response code="200">Obtem dados do Semestre solicitado</response>
		/// <response code="404">Semestre inexistente</response>
		/// <response code="500">Erro inesperado</response>
		[ProducesResponseType(200, Type = typeof(SemestreDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		[HttpGet("{id}")]
		public async Task<IActionResult> ObterPorId(Guid id)
		{
			var response = await _mediator.Send(new ObterSemestreCommand { Id = id });

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Cria um novo Semestre
		/// </summary>
		/// <returns>Http status 201(Created)</returns>
		/// <response code="200">Semestre criado com sucesso</response>
		/// <response code="400">Dados inconsistentes para criação do Semestre</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public async Task<IActionResult> CriarSemestre([FromBody] AdicionarSemestreCommand semestreDto)
		{
			var response = await _mediator.Send(semestreDto);

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Efetua alteração de Semestre
		/// </summary>
		/// <returns>Http status 204(No Content)</returns>
		/// <response code="204">Semestre alterado com Sucesso</response>
		/// <response code="400">Dados para alteração de Semestre inconsistentes.</response>
		/// <response code="404">Semestre inexistente</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpPut("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public async Task<IActionResult> AlterarSemestre(Guid id, [FromBody] AlterarSemestreCommand semestreDto)
		{
			if (semestreDto == null)
				throw new ArgumentNullException(null, "Objeto Semestre nulo (não foi informado).");

			if (id.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Identificador de Semestre é inválido ou nulo");
			semestreDto.ID = id;

			var response = await _mediator.Send(semestreDto);

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Exclui um Semestre específico
		/// </summary>
		/// <response code="204">Semestre excluído com Sucesso</response>
		/// <response code="400">Dados para exclusão da Semestre inconsistentes.</response>
		/// <response code="404">Semestre inexistente</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpDelete("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public async Task<IActionResult> RemoverSemestre(Guid id)
		{
			var response = await this._mediator.Send(new RemoverSemestreCommand { ID = id });
			return this.ProcessResult(response);
		}
	}
}
