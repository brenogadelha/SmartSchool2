using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Alunos.ObterPorId;
using SmartSchool.Aplicacao.Alunos.ObterPorNome;
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
	public class ObterAlunoPorNomeTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly AlunoBuilder _alunoBuilder;
		private readonly Aluno _aluno;

		public ObterAlunoPorNomeTestes()
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
				.ComEndereco("Rua molina 423, Rio Comprido")
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
				.ComEndereco("Rua molina 423, Rio Comprido")
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
				.ComTelefone("2131593353")
				.ComId(Guid.NewGuid()).Instanciar();

			var aluno2 = Aluno.Criar(aluno2Dto);
			var aluno3 = Aluno.Criar(aluno3Dto);
			var aluno4 = Aluno.Criar(aluno4Dto);

			this._contextos.SmartContexto.Alunos.Add(aluno2);
			this._contextos.SmartContexto.Alunos.Add(aluno3);
			this._contextos.SmartContexto.Alunos.Add(aluno4);
			this._contextos.SmartContexto.SaveChangesAsync();
		}

		[Fact(DisplayName = "Obtém Alunos por nome total/parcial")]
		public async void DeveObterAlunosPorNome()
		{
			var requestAlunosPorNome = await this._mediator.Send(new ObterAlunoNomeQuery { Busca = "este" });
			var resultAlunosObtidosPorNomeParcial = requestAlunosPorNome.Should().BeOfType<Result<IEnumerable<ObterAlunoDto>>>().Subject;

			resultAlunosObtidosPorNomeParcial.Value.Should().NotBeNull();
			resultAlunosObtidosPorNomeParcial.Value.Count().Should().Be(2);
			resultAlunosObtidosPorNomeParcial.Value.Where(x => x.Nome == "Estevão").Count().Should().Be(1);
			resultAlunosObtidosPorNomeParcial.Value.Where(x => x.Nome == "Estevann").Count().Should().Be(1);

			//Busca por Sobrenome parcial
			var requestAlunosPorSobrenomeParcial = await this._mediator.Send(new ObterAlunoNomeQuery { Busca = "Pula" });
			var resultAlunosObtidosPorSobrenomeParcial = requestAlunosPorSobrenomeParcial.Should().BeOfType<Result<IEnumerable<ObterAlunoDto>>>().Subject;

			resultAlunosObtidosPorSobrenomeParcial.Value.Should().NotBeNull();
			resultAlunosObtidosPorSobrenomeParcial.Value.Count().Should().Be(2);
			resultAlunosObtidosPorSobrenomeParcial.Value.Where(x => x.Nome == "Estevão").Count().Should().Be(1);
			resultAlunosObtidosPorSobrenomeParcial.Value.Where(x => x.Nome == "Estevann").Count().Should().Be(1);

			//Busca por Nome e Sobrenome Parcial
			var requestAlunosPorNomeParcial2 = await this._mediator.Send(new ObterAlunoNomeQuery { Busca = "JORdania Minei" });
			var resultAlunosObtidosPorNomeParcial2 = requestAlunosPorNomeParcial2.Should().BeOfType<Result<IEnumerable<ObterAlunoDto>>>().Subject;

			resultAlunosObtidosPorNomeParcial2.Value.Should().NotBeNull();
			resultAlunosObtidosPorNomeParcial2.Value.Count().Should().Be(1);
			resultAlunosObtidosPorNomeParcial2.Value.Where(x => x.Nome == "Jordania").Count().Should().Be(1);
		}
	}
}
