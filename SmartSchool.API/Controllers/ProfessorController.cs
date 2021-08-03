using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Aplicacao.Professores.Interface;
using SmartSchool.Dto.Dtos.Professores;
using SmartSchool.Dto.Dtos.TratamentoErros;
using System.Collections.Generic;
using System.Net;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorServico _professorServico;

        public ProfessorController(IProfessorServico professorServico)
        {
            this._professorServico = professorServico;
        }

        /// <summary>
        /// Obtem listagem de todos os Professores cadastrados
        /// </summary>
        /// <returns>Lista de todos os Professores</returns>
        /// <response code="200">Lista de Professores</response> 
        /// <response code="500">Erro inesperado</response>
        [ProducesResponseType(200, Type = typeof(IEnumerable<ObterProfessorDto>))]
        [ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
        [HttpGet]
        public OkObjectResult ObterTodos()
        {
            return Ok(_professorServico.Obter());
        }

        // GET api/<ProfessorController>/5
        //[HttpGet("{id}")]
        //public IActionResult ObterPorId(int id)
        //{
        //    var professor = _repositorio.ObterProfessorPorId(id);

        //    if (professor == null)
        //        throw new Exception($"Não existe Professor com o id {id} informado.");

        //    return Ok(professor);
        //}

        //        [HttpGet("ByName")]
        //        public IActionResult ObterPorNome(string nome)
        //        {
        //            var professor = _repositorio.ObterTodosProfessores().FirstOrDefault(a => a.Nome.Contains(nome));

        //            if (professor == null)
        //                throw new Exception($"Não existe Professor com o nome {nome} informado.");

        //            return Ok(professor);
        //        }

        /// <summary>
        /// Cria um novo Professor
        /// </summary>
        /// <returns>Http status 201(Created)</returns>
        /// <response code="201">Professor criado com sucesso</response>
        /// <response code="400">Dados inconsistentes para criação do Professor</response>
        /// <response code="500">Erro inesperado</response> 
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
        [ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
        public StatusCodeResult Criar([FromBody]ProfessorDto professorDto)
        {
            this._professorServico.CriarProfessor(professorDto);

            return this.StatusCode((int)HttpStatusCode.Created);
        }

        //        // PUT api/<ProfessorController>/5
        //        [HttpPut("{id}")]
        //        public IActionResult Put(int id, Professor professor)
        //        {
        //            var professorObtido = _repositorio.ObterProfessorPorId(id);

        //            if (professorObtido == null)
        //                throw new Exception($"Não foi encontrado o Professor com ID {id} informado.");

        //            this._repositorio.Update(professor);
        //            this._repositorio.SaveChanges();
        //            return Ok(professor);
        //        }

        //        [HttpPatch("{id}")]
        //        public IActionResult Patch(int id, Professor professor)
        //        {
        //            var professorObtido = _repositorio.ObterProfessorPorId(id);

        //            if (professorObtido == null)
        //                throw new Exception($"Não foi encontrado o Professor com ID {id} informado.");

        //            this._repositorio.Update(professor);
        //            this._repositorio.SaveChanges();
        //            return Ok(professor);
        //        }

        //        // DELETE api/<ProfessorController>/5
        //        [HttpDelete("{id}")]
        //        public IActionResult Delete(int id)
        //        {
        //            var professor = _repositorio.ObterProfessorPorId(id);

        //            if (professor == null)
        //                throw new Exception($"Não foi encontrado o Professor com ID {id} informado.");

        //            this._repositorio.Update(professor);
        //            this._repositorio.SaveChanges();
        //            return Ok();
        //        }
        //    }
    }
}
