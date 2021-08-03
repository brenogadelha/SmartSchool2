//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SmartSchool.API.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace SmartSchool.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProfessorController : ControllerBase
//    {
//        private readonly IRepository _repositorio;

//        public ProfessorController(IRepository repositorio)
//        {
//            this._repositorio = repositorio;
//        }

//        // GET: api/<ProfessorController>
//        [HttpGet]
//        public IActionResult ObterTodos()
//        {
//            return Ok(_repositorio.ObterTodosProfessores(true));
//        }

//        // GET api/<ProfessorController>/5
//        [HttpGet("{id}")]
//        public IActionResult ObterPorId(int id)
//        {
//            var professor = _repositorio.ObterProfessorPorId(id);

//            if (professor == null)
//                throw new Exception($"Não existe Professor com o id {id} informado.");

//            return Ok(professor);
//        }

//        [HttpGet("ByName")]
//        public IActionResult ObterPorNome(string nome)
//        {
//            var professor = _repositorio.ObterTodosProfessores().FirstOrDefault(a => a.Nome.Contains(nome));

//            if (professor == null)
//                throw new Exception($"Não existe Professor com o nome {nome} informado.");

//            return Ok(professor);
//        }
//        // POST api/<ProfessorController>
//        [HttpPost]
//        public IActionResult Criar(Professor professor)
//        {
//            this._repositorio.Add(professor);
//            if (this._repositorio.SaveChanges())
//            {
//                return Ok(professor);
//            }

//            return BadRequest("Professor não cadastrado.");
//        }

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
//}
