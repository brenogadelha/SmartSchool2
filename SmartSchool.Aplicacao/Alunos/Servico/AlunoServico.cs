using SmartSchool.Aplicacao.Alunos.Interface;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Comum.Validacao;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Alunos.Especificacao;
using SmartSchool.Dominio.Alunos.Validacao;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Cursos.Especificacao;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Disciplinas.Especificacao;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dominio.Semestres.Especificacao;
using SmartSchool.Dto.Alunos;
using SmartSchool.Dto.Alunos.Obter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartSchool.Aplicacao.Alunos.Servico
{
	public class AlunoServico : IAlunoServico
	{
		private readonly IRepositorio<Aluno> _alunoRepositorio;
		private readonly IRepositorio<Disciplina> _disciplinaRepositorio;
		private readonly IRepositorio<Semestre> _semestreRepositorio;
		private readonly IRepositorio<Curso> _cursoRepositorio;

		public AlunoServico(IRepositorio<Aluno> alunoRepositorio, IRepositorio<Disciplina> disciplinaRepositorio, IRepositorio<Semestre> semestreRepositorio, IRepositorio<Curso> cursoRepositorio)
		{
			this._alunoRepositorio = alunoRepositorio;
			this._disciplinaRepositorio = disciplinaRepositorio;
			this._semestreRepositorio = semestreRepositorio;
			this._cursoRepositorio = cursoRepositorio;
		}

		public IEnumerable<ObterAlunoDto> Obter()
		{
			var alunos = this._alunoRepositorio.Procurar(new BuscaDeAlunoPorAtivoEspecificacao(true).IncluiInformacoesDeCurso());

			return alunos.MapearParaDto<ObterAlunoDto>();
		}

		public void CriarAluno(AlunoDto alunoDto)
		{
			this.VerificarExisteAlunoComMesmoCpfCnpj(alunoDto.Cpf, null);
			this.VerificarExisteAlunoComMesmoEmail(alunoDto.Email, null);

			var aluno = Aluno.Criar(alunoDto);

			this.ObterCursoDominio(alunoDto.CursoId);
			this._alunoRepositorio.Adicionar(aluno);

			if (alunoDto.AlunosDisciplinas != null)
			{
				this.GerarAssociacoes(aluno, alunoDto);
				return;
			}
		}

		public void AlterarAluno(Guid idAluno, AlterarAlunoDto alunoDto, bool? atualizarDisciplinas = null)
		{			
			var aluno = this.ObterAlunoDominio(idAluno);

			if (atualizarDisciplinas.HasValue)
			{
				aluno.AtualizarDisciplinas(alunoDto.AlunosDisciplinas.Select(ad => ad.DisciplinaId).ToList());

				this.GerarAssociacoes(aluno, alunoDto);

				return;
			}

			ValidacaoFabrica.Validar(alunoDto, new AlunoValidacao());

			this.VerificarExisteAlunoComMesmoCpfCnpj(alunoDto.Cpf, alunoDto.ID);
			this.VerificarExisteAlunoComMesmoEmail(alunoDto.Email, alunoDto.ID);

			aluno.AlterarNome(alunoDto.Nome);
			aluno.AlterarSobrenome(alunoDto.Sobrenome);
			aluno.AlterarCelular(alunoDto.Celular);
			aluno.AlterarCidade(alunoDto.Cidade);
			aluno.AlterarEndereco(alunoDto.Endereco);
			aluno.AlterarCpf(alunoDto.Cpf);
			aluno.AlterarEmail(alunoDto.Email);
			aluno.AlterarTelefone(alunoDto.Telefone);
			aluno.AlterarAtivo(alunoDto.Ativo);
			aluno.AlterarDataNascimento(alunoDto.DataNascimento);
			aluno.AlterarDataInicio(alunoDto.DataInicio);
			aluno.AlterarDataFim(alunoDto.DataFim);

			if (alunoDto.AlunosDisciplinas != null)
			{
				aluno.AtualizarDisciplinas(alunoDto.AlunosDisciplinas.Select(ad => ad.DisciplinaId).ToList());

				this.GerarAssociacoes(aluno, alunoDto);

				return;
			}

			this._alunoRepositorio.Atualizar(aluno);
		}

		public ObterAlunoDto ObterPorId(Guid idAluno) => this.ObterAlunoDominio(idAluno).MapearParaDto<ObterAlunoDto>();
		public ObterAlunoDto ObterPorMatricula(int matricula) => this.ObterAlunoDominioMatricula(matricula).MapearParaDto<ObterAlunoDto>();

		public void Remover(Guid id)
		{
			var alunoExistente = this.ObterAlunoDominio(id);

			alunoExistente.AlterarAtivo(false);

			this._alunoRepositorio.Atualizar(alunoExistente);
		}

		public IEnumerable<ObterAlunoDto> ObterPorNomeSobrenomeParcial(string busca)
		{
			var alunos = this._alunoRepositorio.Procurar(new BuscaDeAlunoPorNomeParcialEspecificacao(busca));

			if (alunos.ToList().Count == 0)
				throw new RecursoInexistenteException($"Não foi encontrado nenhum aluno com o parametro '{busca}' informado.");

			return alunos.MapearParaDto<ObterAlunoDto>();
		}

		public IEnumerable<ObterHistoricoAlunoDto> ObterHistoricoPorIdAluno(Guid idAluno, int? periodo = null)
		{
			var aluno = this.ObterAlunoDominio(idAluno);

			if (periodo.HasValue)
				return aluno.SemestresDisciplinas.MapearParaDto<ObterHistoricoAlunoDto>().OrderByDescending(historico => historico.Periodo).Where(s => s.Periodo == periodo);

			else return aluno.SemestresDisciplinas.MapearParaDto<ObterHistoricoAlunoDto>().OrderByDescending(historico => historico.Periodo);
		}

		private void GerarAssociacoes(Aluno aluno, AlunoDto dto)
		{
			aluno.SemestresDisciplinas.Clear();

			foreach (var alunoDisciplina in dto.AlunosDisciplinas)
			{
				// Obter Disciplina pelo Curso
				this.ObterDisciplinaDominio(alunoDisciplina.DisciplinaId);
				this.ObterSemestreDominio(alunoDisciplina.SemestreId);

				aluno.SemestresDisciplinas.Add(SemestreAlunoDisciplina.Criar(alunoDisciplina.Periodo, alunoDisciplina.SemestreId,
					alunoDisciplina.DisciplinaId, aluno.ID, alunoDisciplina.StatusDisciplina));
			}

			this._alunoRepositorio.Atualizar(aluno);
		}

		private Aluno ObterAlunoDominio(Guid idAluno)
		{
			if (idAluno.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Id nulo do Aluno (não foi informado).");


			var aluno = this._alunoRepositorio.Obter(new BuscaDeAlunoPorIdEspecificacao(idAluno).IncluiInformacoesDeHistorico().IncluiInformacoesDeDisciplina().IncluiInformacoesDeCurso());

			if (aluno == null)
				throw new RecursoInexistenteException($"Aluno com ID '{idAluno}' não existe.");

			return aluno;
		}

		private Aluno ObterAlunoDominioMatricula(int matricula)
		{
			if (matricula < 0)
				throw new ArgumentNullException(null, "Matricula do Aluno (não foi informado).");


			var aluno = this._alunoRepositorio.Obter(new BuscaDeAlunoPorMatriculaEspecificacao(matricula).IncluiInformacoesDeHistorico().IncluiInformacoesDeDisciplina().IncluiInformacoesDeCurso());

			if (aluno == null)
				throw new RecursoInexistenteException($"Aluno com matrícula '{matricula}' não existe.");

			return aluno;
		}

		private Disciplina ObterDisciplinaDominio(Guid idDisciplina)
		{
			if (idDisciplina.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Id nulo da Disciplina (não foi informado).");


			var disciplina = this._disciplinaRepositorio.Obter(new BuscaDeDisciplinaPorIdEspecificacao(idDisciplina));

			if (disciplina == null)
				throw new RecursoInexistenteException($"Disciplina com ID '{idDisciplina}' não existe.");

			return disciplina;
		}

		private Semestre ObterSemestreDominio(Guid idSemestre)
		{
			if (idSemestre.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Id nulo do Semestre (não foi informado).");


			var semestre = this._semestreRepositorio.Obter(new BuscaDeSemestrePorIdEspecificacao(idSemestre));

			if (semestre == null)
				throw new RecursoInexistenteException($"Semestre com ID '{idSemestre}' não existe.");

			return semestre;
		}

		private Curso ObterCursoDominio(Guid idCurso)
		{
			if (idCurso.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Id nulo do Curso (não foi informado).");


			var curso = this._cursoRepositorio.Obter(new BuscaDeCursoPorIdEspecificacao(idCurso));

			if (curso == null)
				throw new RecursoInexistenteException($"Curso com ID '{idCurso}' não existe.");

			return curso;
		}
		private void VerificarExisteAlunoComMesmoCpfCnpj(string cpfCnpj, Guid? idAtual)
		{
			var alunoComMesmoCpfCnpj = this._alunoRepositorio.Obter(new BuscaDeAlunoPorCpfCnpjEspecificacao(cpfCnpj));
			if (alunoComMesmoCpfCnpj != null && (!idAtual.HasValue || idAtual.HasValue && alunoComMesmoCpfCnpj.ID != idAtual))
				throw new ErroNegocioException($"Já existe um Aluno com o mesmo CPF '{cpfCnpj}'.");
		}

		private void VerificarExisteAlunoComMesmoEmail(string email, Guid? idAtual)
		{
			var alunoComMesmoEmail = this._alunoRepositorio.Obter(new BuscaDeAlunoPorEmailEspecificacao(email));
			if (alunoComMesmoEmail != null && (!idAtual.HasValue || idAtual.HasValue && alunoComMesmoEmail.ID != idAtual))
				throw new ErroNegocioException($"Já existe um Aluno com o mesmo email '{email}'.");
		}
	}
}
