using MediatR;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Alunos.Servicos;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Cursos.Servicos;
using SmartSchool.Dominio.Disciplinas.Servicos;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dominio.Semestres.Servicos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Alunos.AdicionarAluno
{
	public class AdicionarAlunoHandler : IRequestHandler<AdicionarAlunoCommand, IResult>
	{
		private readonly IRepositorioTask<Aluno> _alunoRepositorio;
		private readonly IAlunoServicoDominio _alunoServicoDominio;
		private readonly ICursoServicoDominio _cursoServicoDominio;
		private readonly IDisciplinaServicoDominio _disciplinaServicoDominio;
		private readonly ISemestreServicoDominio _semestreServicoDominio;

		public AdicionarAlunoHandler(IRepositorioTask<Aluno> alunoRepositorio, IDisciplinaServicoDominio disciplinaServicoDominio,
			ISemestreServicoDominio semestreServicoDominio, ICursoServicoDominio cursoServicoDominio, IAlunoServicoDominio alunoServicoDominio)
		{
			this._alunoRepositorio = alunoRepositorio;
			this._cursoServicoDominio = cursoServicoDominio;
			this._alunoServicoDominio = alunoServicoDominio;
			this._disciplinaServicoDominio = disciplinaServicoDominio;
			this._semestreServicoDominio = semestreServicoDominio;
		}

		public async Task<IResult> Handle(AdicionarAlunoCommand request, CancellationToken cancellationToken)
		{

			if (await this._alunoServicoDominio.VerificarExisteAlunoComMesmoCpfCnpj(request.Cpf, null))
				throw new ErroNegocioException($"Já existe um Aluno com o mesmo CPF '{request.Cpf}'.");

			if (await this._alunoServicoDominio.VerificarExisteAlunoComMesmoEmail(request.Email, null))
				throw new ErroNegocioException($"Já existe um Aluno com o mesmo email '{request.Email}'.");

			// Verifica se o Curso existe
			await this._cursoServicoDominio.ObterAsync(request.CursoId);

			var alunoResult = Aluno.Criar(request.Nome, request.Sobrenome, request.Telefone, request.DataInicio, request.DataFim, request.DataNascimento,
				request.Matricula, request.Celular, request.Cidade, request.Cpf, request.Email, request.Endereco, request.CursoId, request.AlunosDisciplinas);

			await this._alunoRepositorio.Adicionar(alunoResult.Value);

			if (request.AlunosDisciplinas != null)
				this.GerarAssociacoes(alunoResult.Value, request);

			return Result<Guid>.Success(alunoResult.Value!.ID);
		}

		private void GerarAssociacoes(Aluno aluno, AdicionarAlunoCommand dto)
		{
			aluno.SemestresDisciplinas.Clear();

			foreach (var alunoDisciplina in dto.AlunosDisciplinas)
			{
				this._disciplinaServicoDominio.ObterAsync(alunoDisciplina.DisciplinaId);
				this._semestreServicoDominio.ObterAsync(alunoDisciplina.SemestreId);

				aluno.SemestresDisciplinas.Add(SemestreAlunoDisciplina.Criar(alunoDisciplina.Periodo, alunoDisciplina.SemestreId,
					alunoDisciplina.DisciplinaId, aluno.ID, alunoDisciplina.StatusDisciplina));
			}

			this._alunoRepositorio.Atualizar(aluno);
		}
	}
}
