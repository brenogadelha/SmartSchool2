using SmartSchool.Aplicacao.Alunos.Interface;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Alunos.Especificacao;
using SmartSchool.Dto.Dtos.Alunos;
using SmartSchool.Dto.Dtos.Alunos.Obter;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Aplicacao.Alunos.Servico
{
    public class AlunoServico : IAlunoServico
    {
        private readonly IRepositorio<Aluno> _alunoRepositorio;
        public AlunoServico(IRepositorio<Aluno> alunoRepositorio)
        {
            this._alunoRepositorio = alunoRepositorio;
        }

        public IEnumerable<ObterAlunoDto> Obter()
        {
            var alunos = this._alunoRepositorio.Procurar(new BuscaDeAlunoPorAtivoEspecificacao(true));

            return alunos.MapearParaDto<ObterAlunoDto>();
        }

        public void CriarAluno(AlunoDto alunoDto)
        {
            var aluno = Aluno.Criar(alunoDto);

            this._alunoRepositorio.Adicionar(aluno);
        }

        public void AlterarAluno(Guid idAluno, AlterarAlunoDto alunoDto)
        {
            var aluno = this.ObterAlunoDominio(idAluno);

            aluno.AlterarNome(alunoDto.Nome);
            aluno.AlterarSobrenome(alunoDto.Sobrenome);
            aluno.AlterarTelefone(alunoDto.Telefone);
            aluno.AlterarAtivo(alunoDto.Ativo);
            aluno.AlterarDataNascimento(alunoDto.DataNascimento);
            aluno.AlterarDataInicio(alunoDto.DataInicio);
            aluno.AlterarDataFim(alunoDto.DataFim);

            this._alunoRepositorio.Atualizar(aluno, true);
        }

        public ObterAlunoDto ObterPorId(Guid idAluno) => this.ObterAlunoDominio(idAluno).MapearParaDto<ObterAlunoDto>();

        public void Remover(Guid id)
        {
            var alunoExistente = this.ObterAlunoDominio(id);

            alunoExistente.AlterarAtivo(false);

            this._alunoRepositorio.Atualizar(alunoExistente);
        }

        private Aluno ObterAlunoDominio(Guid idAluno)
        {
            if (idAluno.Equals(Guid.Empty))
                throw new ArgumentNullException(null, "Id nulo do Aluno (não foi informado).");


            var aluno = this._alunoRepositorio.Obter(new BuscaDeAlunoPorIdEspecificacao(idAluno));

            if (aluno == null)
                throw new RecursoInexistenteException($"Aluno com ID '{idAluno}' não existe.");

            return aluno;
        }
    }
}
