using SmartSchool.Aplicacao.Professores.Interface;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dominio.Professores.Especificacao;
using SmartSchool.Dto.Dtos.Professores;
using SmartSchool.Dto.Professores;
using System;
using System.Collections.Generic;

namespace SmartSchool.Aplicacao.Professores.Servico
{
    public class ProfessorServico : IProfessorServico
    {
        private readonly IRepositorio<Professor> _professorRepositorio;

        public ProfessorServico(IRepositorio<Professor> professorRepositorio)
        {
            this._professorRepositorio = professorRepositorio;
        }

        public IEnumerable<ObterProfessorDto> Obter()
        {
            var professor = this._professorRepositorio.Procurar(new BuscaDeProfessorEspecificacao());

            return professor.MapearParaDto<ObterProfessorDto>();
        }

        public void CriarProfessor(ProfessorDto professorDto)
        {
            var professor = Professor.Criar(professorDto);

            this._professorRepositorio.Adicionar(professor);
        }

        public void AlterarProfessor(Guid idProfessor, AlterarProfessorDto professorDto)
        {
            var professor = this.ObterProfessorDominio(idProfessor);

            professor.AlterarNome(professorDto.Nome);
            professor.AlterarMatricula(professorDto.Matricula);

            this._professorRepositorio.Atualizar(professor, true);
        }

        public ObterProfessorDto ObterPorId(Guid idProfessor) => this.ObterProfessorDominio(idProfessor).MapearParaDto<ObterProfessorDto>();

        public void Remover(Guid id)
        {
            var professor = this.ObterProfessorDominio(id);

            this._professorRepositorio.Remover(professor);
        }

        private Professor ObterProfessorDominio(Guid idProfessor)
        {
            if (idProfessor.Equals(Guid.Empty))
                throw new ArgumentNullException(null, "Id nulo do Professor (não foi informado).");


            var professor = this._professorRepositorio.Obter(new BuscaDeProfessorPorIdEspecificacao(idProfessor));

            if (professor == null)
                throw new RecursoInexistenteException($"Professor com ID '{idProfessor}' não existe.");

            return professor;
        }
    }
}
