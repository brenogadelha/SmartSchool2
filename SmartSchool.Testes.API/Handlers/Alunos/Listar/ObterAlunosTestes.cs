using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Alunos.Listar;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Alunos;
using SmartSchool.Dados.Modulos.Cursos;
using SmartSchool.Dados.Modulos.Disciplinas;
using SmartSchool.Dados.Modulos.Semestres;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Alunos.Servicos;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Cursos.Servicos;
using SmartSchool.Dominio.Disciplinas.Servicos;
using SmartSchool.Dominio.Semestres.Servicos;
using SmartSchool.Dto.Alunos;
using SmartSchool.Dto.Alunos.Obter;
using SmartSchool.Testes.Compartilhado.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Alunos
{
	public class ObterAlunosTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly AlunoBuilder _alunoBuilder;
		private readonly Aluno _aluno;

		public ObterAlunosTestes()
		{
			this._alunoBuilder = new AlunoBuilder();
			this._contextos = ContextoFactory.Criar();

			var alunoRepositorioTask = new AlunoRepositorio(this._contextos);
			var cursoRepositorioTask = new CursoRepositorio(this._contextos);
			var disciplinaRepositorioTask = new DisciplinaRepositorio(this._contextos);
			var semestreRepositorioTask = new SemestreRepositorio(this._contextos);

			var cursoDominio = new CursoServicoDominio(cursoRepositorioTask);
			var alunoDominio = new AlunoServicoDominio(alunoRepositorioTask);
			var disciplinaDominio = new DisciplinaServicoDominio(disciplinaRepositorioTask);
			var semestreDominio = new SemestreServicoDominio(semestreRepositorioTask);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Aluno>), alunoRepositorioTask), (typeof(IDisciplinaServicoDominio), disciplinaDominio),
			(typeof(ISemestreServicoDominio), semestreDominio), (typeof(ICursoServicoDominio), cursoDominio), (typeof(IAlunoServicoDominio), alunoDominio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();

			this._aluno = this._alunoBuilder.ObterAluno();

			var aluno2Dto = AlunoDtoBuilder.Novo
				.ComCelular("21912388899")
				.ComEndereco("Rua molina 421, Rio Comprido")
				.ComCidade("Espirito Santo")
				.ComCursoId(this._aluno.CursoId)
				.ComAlunosDisciplinas(new List<AlunoDisciplinaDto> { new AlunoDisciplinaDto { DisciplinaId = this._aluno.AlunosDisciplinas.FirstOrDefault().DisciplinaID,
					SemestreId = this._aluno.SemestresDisciplinas.FirstOrDefault().SemestreID, Periodo = 2, StatusDisciplina = Comum.Dominio.Enums.StatusDisciplina.Cursando } })
				.ComCpfCnpj("51886437076")
				.ComDataNascimento(DateTime.Now.AddYears(-30))
				.ComDataInicio(DateTime.Now.AddDays(-50))
				.ComDataFim(DateTime.Now.AddYears(4))
				.ComEmail("estevao.russao@unicarioca.com.br")
				.ComNome("Estevann")
				.ComSobrenome("Pulante")
				.ComTelefone("2131592222")
				.ComId(Guid.NewGuid()).Instanciar();

			var aluno3Dto = AlunoDtoBuilder.Novo
				.ComCelular("21912388877")
				.ComCursoId(this._aluno.CursoId)
				.ComAlunosDisciplinas(new List<AlunoDisciplinaDto> { new AlunoDisciplinaDto { DisciplinaId = this._aluno.AlunosDisciplinas.FirstOrDefault().DisciplinaID,
					SemestreId = this._aluno.SemestresDisciplinas.FirstOrDefault().SemestreID, Periodo = 2, StatusDisciplina = Comum.Dominio.Enums.StatusDisciplina.Cursando } })
				.ComEndereco("Rua molina 422, Rio Comprido")
				.ComCidade("Minas Gerais")
				.ComCpfCnpj("84012584057")
				.ComDataNascimento(DateTime.Now.AddYears(-40))
				.ComDataInicio(DateTime.Now.AddDays(-10))
				.ComDataFim(DateTime.Now.AddYears(2))
				.ComEmail("jordania.mineiro@unicarioca.com.br")
				.ComNome("Jordania")
				.ComSobrenome("Mineiro")
				.ComTelefone("2131593353")
				.ComId(Guid.NewGuid()).Instanciar();

			var aluno4Dto = AlunoDtoBuilder.Novo
				.ComCelular("21912399997")
				.ComCursoId(this._aluno.CursoId)
				.ComAlunosDisciplinas(new List<AlunoDisciplinaDto> { new AlunoDisciplinaDto { DisciplinaId = this._aluno.AlunosDisciplinas.FirstOrDefault().DisciplinaID,
					SemestreId = this._aluno.SemestresDisciplinas.FirstOrDefault().SemestreID, Periodo = 2, StatusDisciplina = Comum.Dominio.Enums.StatusDisciplina.Cursando } })
				.ComEndereco("Rua molina 423, Rio Comprido")
				.ComCidade("Minas Gerais")
				.ComCpfCnpj("78755724019")
				.ComDataNascimento(DateTime.Now.AddYears(-50))
				.ComDataInicio(DateTime.Now.AddDays(-20))
				.ComDataFim(DateTime.Now.AddYears(4))
				.ComEmail("estevao.mineiro@unicarioca.com.br")
				.ComNome("Jordan")
				.ComSobrenome("Cartman")
				.ComTelefone("2131593354")
				.ComId(Guid.NewGuid()).Instanciar();

			var aluno2 = Aluno.Criar(aluno2Dto);
			var aluno3 = Aluno.Criar(aluno3Dto);
			var aluno4 = Aluno.Criar(aluno4Dto);

			this._contextos.SmartContexto.Alunos.Add(aluno2);
			this._contextos.SmartContexto.Alunos.Add(aluno3);
			this._contextos.SmartContexto.Alunos.Add(aluno4);
			this._contextos.SmartContexto.SaveChangesAsync();
		}

		[Fact(DisplayName = "Obtém lista de Alunos")]
		public async void DeveObterAlunosPorNome()
		{
			var requestAlunos = await this._mediator.Send(new ListarAlunosQuery());
			var resultAlunosObtidos = requestAlunos.Should().BeOfType<Result<IEnumerable<ObterAlunoDto>>>().Subject;

			resultAlunosObtidos.Value.Should().NotBeNull();
			resultAlunosObtidos.Value.Count().Should().Be(4);
			resultAlunosObtidos.Value.Where(x => x.Nome == "Jordan").Count().Should().Be(1);
			resultAlunosObtidos.Value.Where(x => x.Nome == "Jordania").Count().Should().Be(1);
			resultAlunosObtidos.Value.Where(x => x.Nome == "Estevann").Count().Should().Be(1);
			resultAlunosObtidos.Value.Where(x => x.Nome == "Estevão").Count().Should().Be(1);
			resultAlunosObtidos.Value.Where(x => x.Celular == "21912399997").Count().Should().Be(1);
			resultAlunosObtidos.Value.Where(x => x.Telefone == "2131593354").Count().Should().Be(1);
			resultAlunosObtidos.Value.Where(x => x.Curso == "Engenharia da Computação").Count().Should().Be(4);
			resultAlunosObtidos.Value.Where(x => x.Endereco == "Rua molina 422, Rio Comprido").Count().Should().Be(1);
			resultAlunosObtidos.Value.Where(x => x.Email == "estevao.mineiro@unicarioca.com.br").Count().Should().Be(1);
			resultAlunosObtidos.Value.Where(x => x.Ativo == true).Count().Should().Be(4);
			resultAlunosObtidos.Value.Where(x => x.Cidade == "Minas Gerais").Count().Should().Be(2);
			resultAlunosObtidos.Value.Where(x => x.Cpf == "78755724019").Count().Should().Be(1);
			resultAlunosObtidos.Value.Where(x => x.DataInicio.ToString("dd/MM/yyyy") == DateTime.Now.AddDays(-20).ToString("dd/MM/yyyy")).Count().Should().Be(2);
			resultAlunosObtidos.Value.Where(x => x.DataFim.ToString("dd/MM/yyyy") == DateTime.Now.AddYears(4).ToString("dd/MM/yyyy")).Count().Should().Be(3);
			resultAlunosObtidos.Value.Where(x => x.DataNascimento.ToString("dd/MM/yyyy") == DateTime.Now.AddYears(-50).ToString("dd/MM/yyyy")).Count().Should().Be(1);
		}
	}
}
