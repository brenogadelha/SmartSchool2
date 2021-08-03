using SmartSchool.Aplicacao.Professores.Interface;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dominio.Alunos.Especificacao;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dto.Dtos.Professores;
using System;
using System.Collections.Generic;
using System.Text;

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
            var professor = this._professorRepositorio.Procurar(new BuscaDeProfessorEspecificacao(true));

            return professor.MapearParaDto<ObterProfessorDto>();
        }

        public void CriarProfessor(ProfessorDto professorDto)
        {
            var professor = Professor.Criar(professorDto);

            this._professorRepositorio.Adicionar(professor);
        }

        //public void AlterarAluno(Guid idAluno, AlterarAlunoDto alunoDto)
        //{
        //    var aluno = this.ObterAlunoDominio(idAluno);

        //    aluno.AlterarNome(alunoDto.Nome);
        //    aluno.AlterarSobrenome(alunoDto.Sobrenome);
        //    aluno.AlterarTelefone(alunoDto.Telefone);
        //    aluno.AlterarAtivo(alunoDto.Ativo);
        //    aluno.AlterarDataNascimento(alunoDto.DataNascimento);
        //    aluno.AlterarDataInicio(alunoDto.DataInicio);
        //    aluno.AlterarDataFim(alunoDto.DataFim);

        //    this._alunoRepositorio.Atualizar(aluno, true);
        //}

        //public ObterAlunoDto ObterPorId(Guid idAluno) => this.ObterAlunoDominio(idAluno).MapearParaDto<ObterAlunoDto>();

        //public void Remover(Guid id)
        //{
        //    var alunoExistente = this.ObterAlunoDominio(id);

        //    alunoExistente.AlterarAtivo(false);

        //    this._alunoRepositorio.Atualizar(alunoExistente);
        //}

        //private Aluno ObterAlunoDominio(Guid idAluno)
        //{
        //    if (idAluno.Equals(Guid.Empty))
        //        throw new ArgumentNullException(null, "Id nulo do Aluno (não foi informado).");


        //    var aluno = this._alunoRepositorio.Obter(new BuscaDeAlunoPorIdEspecificacao(idAluno));

        //    if (aluno == null)
        //        throw new RecursoInexistenteException($"Aluno com ID '{idAluno}' não existe.");

        //    return aluno;
        //}
    }
}
