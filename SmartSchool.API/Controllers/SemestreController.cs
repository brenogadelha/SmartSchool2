using Microsoft.AspNetCore.Mvc;
using SmartSchool.Aplicacao.Semestres.Interface;
using SmartSchool.Dto.Curso;
using SmartSchool.Dto.Dtos.TratamentoErros;
using SmartSchool.Dto.Semestres;
using System;
using System.Collections.Generic;
using System.Net;

namespace SmartSchool.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SemestreController : Controller
	{
		private readonly ISemestreServico _semestreServico;

		public SemestreController(ISemestreServico semestreServico)
		{
			this._semestreServico = semestreServico;
		}

		/// <summary>
		/// Obtem listagem de todos os Semestre cadastrados
		/// </summary>
		/// <returns>Lista de todos os Semestre</returns>
		/// <response code="200">Lista de Semestre</response> 
		/// <response code="500">Erro inesperado</response>
		[ProducesResponseType(200, Type = typeof(IEnumerable<CursoDto>))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		[HttpGet]
		public OkObjectResult ObterTodos() => Ok(_semestreServico.Obter());

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
		public OkObjectResult ObterPorId(Guid id) => Ok(this._semestreServico.ObterPorId(id));

		/// <summary>
		/// Cria um novo Semestre
		/// </summary>
		/// <returns>Http status 201(Created)</returns>
		/// <response code="201">Semestre criado com sucesso</response>
		/// <response code="400">Dados inconsistentes para criação do Semestre</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public StatusCodeResult Criar([FromBody] SemestreDto semestreDto)
		{
			this._semestreServico.CriarSemestre(semestreDto);

			return this.StatusCode((int)HttpStatusCode.Created);
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
		public StatusCodeResult AlterarSemestre(Guid id, [FromBody] AlterarSemestreDto semestreDto)
		{
			if (semestreDto == null)
				throw new ArgumentNullException(null, "Objeto Semestre nulo (não foi informado).");

			if (id.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Identificador de Semestre é inválido ou nulo");

			semestreDto.ID = id;

			this._semestreServico.AlterarSemestre(id, semestreDto);

			return this.StatusCode((int)HttpStatusCode.Created);
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
		public StatusCodeResult ExcluirSemestre(Guid id)
		{
			this._semestreServico.Remover(id);
			return this.StatusCode(204);
		}
	}
}
