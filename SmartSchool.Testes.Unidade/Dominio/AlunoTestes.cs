﻿using FluentAssertions;
using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dto.Alunos;
using SmartSchool.Testes.Compartilhado.Builders;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartSchool.Testes.Unidade.Dominio
{
	public class AlunoTestes : TesteUnidade
	{
		private readonly AlunoDtoBuilder _alunoDtoBuilder;

		public AlunoTestes()
		{
			var alunoDisciplinaDto = new AlunoDisciplinaDto()
			{
				DisciplinaId = Guid.NewGuid(),
				Periodo = 1,
				SemestreId = Guid.NewGuid(),
				StatusDisciplina = StatusDisciplinaEnum.Cursando
			};

			List<AlunoDisciplinaDto> alunosDisciplinas = new List<AlunoDisciplinaDto>();
			alunosDisciplinas.Add(alunoDisciplinaDto);

			this._alunoDtoBuilder = AlunoDtoBuilder.Novo
				.ComCidade("Rio de Janeiro")
				.ComCpfCnpj("48340829033")
				.ComCelular("99999999")
				.ComAlunosDisciplinas(alunosDisciplinas)
				.ComDataNascimento(DateTime.Now.AddDays(-5000))
				.ComDataInicio(DateTime.Now)
				.ComEmail("estevao.pulante@unicarioca.com.br")
				.ComNome("Estevão")
				.ComSobrenome("Pulante")
				.ComTelefone("2131593159")
				.ComId(Guid.NewGuid());
		}

		[Fact(DisplayName = "Criação e Alteração de Aluno com Sucesso")]
		public void DeveCriarNovoUsuario()
		{
			var alunoDto = this._alunoDtoBuilder.Instanciar();

			var aluno = Aluno.Criar(alunoDto);

			aluno.Should().NotBeNull();
			aluno.ID.Should().NotBe(Guid.Empty);
			aluno.Nome.Should().Be(alunoDto.Nome);
			aluno.Ativo.Should().Be(true);
			aluno.Sobrenome.Should().Be(alunoDto.Sobrenome);
			aluno.Celular.Should().Be(alunoDto.Celular);
			aluno.Cidade.Should().Be(alunoDto.Cidade);
			aluno.Cpf.Should().Be(alunoDto.Cpf);
			aluno.DataNascimento.Should().Be(alunoDto.DataNascimento);
			aluno.Email.Should().Be(alunoDto.Email);
			aluno.Telefone.Should().Be(alunoDto.Telefone);
			aluno.DataNascimento.ToString().Should().Contain(alunoDto.DataNascimento.ToString("dd/MM/yyyy"));
			aluno.DataInicio.ToString().Should().Contain(alunoDto.DataInicio.ToString("dd/MM/yyyy"));

			// Alteração
			var dataNascimentoNova = DateTime.Now.AddDays(-6000);
			var dataInicioNova = DateTime.Now.AddDays(-5);

			aluno.AlterarAtivo(false);
			aluno.AlterarCelular("21981818181");
			aluno.AlterarCidade("Pindamonhangaba");
			aluno.AlterarCpf("71916890059");
			aluno.AlterarEmail("estevao.russo@unicarioca.com.br");
			aluno.AlterarNome("Estevan");
			aluno.AlterarSobrenome("Russo");
			aluno.AlterarTelefone("2132653265");
			aluno.AlterarDataNascimento(dataNascimentoNova);
			aluno.AlterarDataInicio(dataInicioNova);

			aluno.Nome.Should().Be("Estevan");
			aluno.Ativo.Should().Be(false);
			aluno.Celular.Should().Be("21981818181");
			aluno.Cidade.Should().Be("Pindamonhangaba");
			aluno.Cpf.Should().Be("71916890059");
			aluno.Email.Should().Be("estevao.russo@unicarioca.com.br");
			aluno.Sobrenome.Should().Be("Russo");
			aluno.Telefone.Should().Be("2132653265");
			aluno.DataNascimento.ToString().Should().Contain(dataNascimentoNova.ToString("dd/MM/yyyy"));
			aluno.DataInicio.ToString().Should().Contain(dataInicioNova.ToString("dd/MM/yyyy"));
		}

		public static IEnumerable<object[]> DadosPraTestesException =>
		new List<object[]>
		{
			new object[] { "Estevao", "Pulante", "110752520911", "estevao.pulante@unicarioca.com.br", "Rio de Janeiro", "2131313233", "21974440403", DateTime.Now.AddDays(-5000), "CPF de Aluno é inválido." },
			new object[] { "Estevao", "Pulante", "110752520911321", "estevao.pulanterussao@unicarioca.com.br", "Rio de Janeiro", "2131313233", "21974440403", DateTime.Now.AddDays(-5000), "CPF de Aluno é inválido." },
			new object[] { "Estevao", "Pulante", "", "estevao.pulanterussao@unicarioca.com.br", "Rio de Janeiro", "2131313233", "21974440403", DateTime.Now.AddDays(-5000), "CPF de Aluno é inválido." },
			new object[] { "Estevao", "Pulante", null, "estevao.pulanterussao@unicarioca.com.br", "Rio de Janeiro", "2131313233", "21974440403", DateTime.Now.AddDays(-5000), "CPF de Aluno é inválido." },
			new object[] { "Estevao", "Pulante", "11075252091", "estevao.pulanterussao%&unicarioca.com.br", "Rio de Janeiro", "2131313233", "21974440403", DateTime.Now.AddDays(-5000), "Email de Aluno é inválido." },
			new object[] { "Estevao", "Pulante", "11075252091", "estevao.pulanterussao@unicarioca.com.br", "Rio de Janeiro", "2131313233", "21974440403", DateTime.Now.AddDays(1), "Data de Nascimento deve ser anterior a hoje." },
			new object[] { "Estevao", "Pulante", "11075252091", null, "Rio de Janeiro", "2131313233", "21974440403", DateTime.Now.AddDays(-5000), "Email de Aluno deve ser informado." },
			new object[] { "", "Pulante", "11075252091", "estevao.pulanterussao@unicarioca.com.br", "Rio de Janeiro", "2131313233", "21974440403", DateTime.Now.AddDays(-5000), "Nome de Aluno deve ser informado." },
			new object[] { "Estevao", null, "11075252091", "estevao.pulanterussao@unicarioca.com.br", "Rio de Janeiro", "2131313233", "21974440403", DateTime.Now.AddDays(-5000), "Sobrenome de Aluno deve ser informado." },
			new object[] { "Estevao", "Pulante", "11075252091", "estevao.pulanterussao@unicarioca.com.br", "Rio de Janeiro", "2131313233", "21974440403", null, "Data de Nascimento deve ser informada." }

		};

		[Theory(DisplayName = "Obtém Exception ao Criar Usuario com valores Errados, Nulos ou Vazios")]
		[MemberData(nameof(DadosPraTestesException))]
		public void ObterExceptionAoCriarUsuarioComValoresNulosOuVazios(string nome, string sobrenome, string cpfCnpj, string email, string cidade, string telefone, string celular, DateTime dataNascimento, string erro)
		{
			var alunoDisciplinaDto = new AlunoDisciplinaDto()
			{
				DisciplinaId = Guid.NewGuid(),
				Periodo = 1,
				SemestreId = Guid.NewGuid(),
				StatusDisciplina = StatusDisciplinaEnum.Cursando
			};

			List<AlunoDisciplinaDto> alunosDisciplinas = new List<AlunoDisciplinaDto>();
			alunosDisciplinas.Add(alunoDisciplinaDto);

			var usuarioDto = AlunoDtoBuilder.Novo
				.ComCelular(celular)
				.ComCidade(cidade)
				.ComCpfCnpj(cpfCnpj)
				.ComDataNascimento(dataNascimento)
				.ComEmail(email)
				.ComNome(nome)
				.ComSobrenome(sobrenome)
				.ComTelefone(telefone)
				.ComAlunosDisciplinas(alunosDisciplinas);

			var exception = Assert.Throws<ErroNegocioException>(() => Aluno.Criar(usuarioDto.Instanciar()));
			Assert.Equal(erro, exception.Message);
		}
	}
}
