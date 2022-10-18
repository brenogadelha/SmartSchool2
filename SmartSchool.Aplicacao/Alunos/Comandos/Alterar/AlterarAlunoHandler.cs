using MediatR;
using SmartSchool.Aplicacao.Alunos.Alterar.Validacao;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Comum.Validacao;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Alunos.Servicos;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Cursos.Servicos;
using SmartSchool.Dominio.Disciplinas.Servicos;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dominio.Semestres.Servicos;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Alunos.Alterar
{
	public class AlterarAlunoHandler : IRequestHandler<AlterarAlunoCommand, IResult>
	{
		private readonly IRepositorio<Aluno> _alunoRepositorio;
		private readonly IAlunoServicoDominio _alunoServicoDominio;
		private readonly ICursoServicoDominio _cursoServicoDominio;
		private readonly IDisciplinaServicoDominio _disciplinaServicoDominio;
		private readonly ISemestreServicoDominio _semestreServicoDominio;

		public AlterarAlunoHandler(IRepositorio<Aluno> alunoRepositorio, IDisciplinaServicoDominio disciplinaServicoDominio,
			ISemestreServicoDominio semestreServicoDominio, ICursoServicoDominio cursoServicoDominio, IAlunoServicoDominio alunoServicoDominio)
		{
			this._alunoRepositorio = alunoRepositorio;
			this._cursoServicoDominio = cursoServicoDominio;
			this._alunoServicoDominio = alunoServicoDominio;
			this._disciplinaServicoDominio = disciplinaServicoDominio;
			this._semestreServicoDominio = semestreServicoDominio;
		}

		public async Task<IResult> Handle(AlterarAlunoCommand request, CancellationToken cancellationToken)
		{
			var aluno = await this._alunoServicoDominio.ObterPorIdAsync(request.ID);

			ValidacaoFabrica.Validar(request, new AlunoValidacaoRequestAlteracao());

			if (await this._alunoServicoDominio.VerificarExisteAlunoComMesmoCpfCnpj(request.Cpf, request.ID))
				return Result.UnprocessableEntity($"Já existe um Aluno com o mesmo CPF '{request.Cpf}'.");

			if (await this._alunoServicoDominio.VerificarExisteAlunoComMesmoEmail(request.Email, request.ID))
				return Result.UnprocessableEntity($"Já existe um Aluno com o mesmo email '{request.Email}'.");

			// Verifica se o Curso existe
			await this._cursoServicoDominio.ObterAsync(request.CursoId);

			aluno.AlterarNome(request.Nome);
			aluno.AlterarSobrenome(request.Sobrenome);
			aluno.AlterarCelular(request.Celular);
			aluno.AlterarCidade(request.Cidade);
			aluno.AlterarEndereco(request.Endereco);
			aluno.AlterarCpf(request.Cpf);
			aluno.AlterarEmail(request.Email);
			aluno.AlterarTelefone(request.Telefone);
			aluno.AlterarAtivo(request.Ativo);
			aluno.AlterarDataNascimento(request.DataNascimento);
			aluno.AlterarDataInicio(request.DataInicio);
			aluno.AlterarDataFim(request.DataFim);
			aluno.AlterarCursoId(request.CursoId);

			if (request.AlunosDisciplinas != null && request.AlunosDisciplinas.Any())
			{
				aluno.AtualizarDisciplinas(request.AlunosDisciplinas.Select(ad => ad.DisciplinaId).ToList());

				this.GerarAssociacoes(aluno, request);
			}

			await this._alunoRepositorio.Atualizar(aluno);

			return Result.Success();
		}

		private void GerarAssociacoes(Aluno aluno, AlterarAlunoCommand dto)
		{
			aluno.SemestresDisciplinas.Clear();

			foreach (var alunoDisciplina in dto.AlunosDisciplinas)
			{
				this._disciplinaServicoDominio.ObterAsync(alunoDisciplina.DisciplinaId);
				this._semestreServicoDominio.ObterAsync(alunoDisciplina.SemestreId);

				aluno.SemestresDisciplinas.Add(SemestreAlunoDisciplina.Criar(alunoDisciplina.Periodo, alunoDisciplina.SemestreId,
					alunoDisciplina.DisciplinaId, aluno.ID, alunoDisciplina.StatusDisciplina));
			}

			this._alunoRepositorio.Atualizar(aluno, false);
		}
	}
}
