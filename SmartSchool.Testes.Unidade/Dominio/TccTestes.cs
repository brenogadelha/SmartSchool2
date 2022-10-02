using FluentAssertions;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Tccs;
using SmartSchool.Dto.Tccs;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartSchool.Testes.Unidade.Dominio
{
	public class TccTestes : TesteUnidade
	{
		private const string maisDe3008 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi eget rutrum velit. Donec dignissim elementum velit, quis suscipit sem fermentum nec. Mauris a egestas felis, vel fermentum sem. Aliquam vitae sollicitudin elit, sit amet finibus neque. Praesent euismod diam purus, eget congue nunc dictum ac. Praesent condimentum, lectus et auctor suscipit, nunc eros maximus quam, eget auctor purus diam et lacus. Curabitur volutpat elit nec finibus semper.Morbi sit amet maximus sem.Integer enim quam, convallis et urna pharetra, finibus blandit leo.Phasellus vehicula nibh sapien.Duis enim nibh, auctor ac felis in, pellentesque dignissim metus.Sed rutrum elit eu semper scelerisque. Phasellus scelerisque semper sapien, quis rutrum urna fringilla ut.Vestibulum vitae nibh dictum, scelerisque nisl vel, gravida ipsum. Cras ac vehicula nunc. Mauris a purus eu lacus faucibus varius.Vestibulum eget dictum ex. Pellentesque id lacus pellentesque, fermentum elit id, aliquet erat. Sed finibus lorem sit amet quam laoreet sodales sit amet id tellus. Mauris pretium eget mi et lacinia. Morbi porta urna at libero lacinia fermentum.Nunc porta tellus vitae laoreet posuere. Proin nec lacus a nisl dictum tempus.Vestibulum ligula justo, pharetra et mollis non, laoreet vel odio.Quisque enim lacus, pharetra id blandit a, ultricies id justo.Vestibulum congue lectus id risus mattis tincidunt.Maecenas tristique interdum dolor, malesuada sollicitudin mi cursus sed.Nulla facilisi. Ut facilisis magna posuere orci tincidunt, ut aliquam erat tempus dui Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi eget rutrum velit. Donec dignissim elementum velit, quis suscipit sem fermentum nec. Mauris a egestas felis, vel fermentum sem. Aliquam vitae sollicitudin elit, sit amet finibus neque. Praesent euismod diam purus, eget congue nunc dictum ac. Praesent condimentum, lectus et auctor suscipit, nunc eros maximus quam, eget auctor purus diam et lacus. Curabitur volutpat elit nec finibus semper.Morbi sit amet maximus sem.Integer enim quam, convallis et urna pharetra, finibus blandit leo.Phasellus vehicula nibh sapien.Duis enim nibh, auctor ac felis in, pellentesque dignissim metus.Sed rutrum elit eu semper scelerisque. Phasellus scelerisque semper sapien, quis rutrum urna fringilla ut.Vestibulum vitae nibh dictum, scelerisque nisl vel, gravida ipsum. Cras ac vehicula nunc. Mauris a purus eu lacus faucibus varius.Vestibulum eget dictum ex. Pellentesque id lacus pellentesque, fermentum elit id, aliquet erat. Sed finibus lorem sit amet quam laoreet sodales sit amet id tellus. Mauris pretium eget mi et lacinia. Morbi porta urna at libero lacinia fermentum.Nunc porta tellus vitae laoreet posuere. Proin nec lacus a nisl dictum tempus.Vestibulum ligula justo, pharetra et mollis non, laoreet vel odio.Quisque enim lacus, pharetra id blandit a, ultricies id justo.Vestibulum congue lectus id risus mattis tincidunt.Maecenas tristique interdum dolor, malesuada sollicitudin mi cursus sed.Nulla facilisi. Ut";
		private const string maisDe160 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Vivamus ut sagittis purus. Morbi lectus ante Lorem ipsum dolor sit amet, consectetur adipiscing elit.Vivamus ut sagittis purus. Morbi lectus ante saasxsdx sssdcs";

		public TccTestes() { }

		[Fact(DisplayName = "Criação e Alteração de Tcc com Sucesso")]
		public void DeveCriarNovoUsuario()
		{
			var professores = new List<Guid>();
			professores.Add(Guid.NewGuid());

			var tccDto = new TccDto() { Tema = "Automação e Visualização de Dados", Descricao = "Descrição de tema", Professores = professores };

			var tcc = Tcc.Criar(tccDto);

			tcc.Should().NotBeNull();
			tcc.ID.Should().NotBe(Guid.Empty);
			tcc.Tema.Should().Be(tccDto.Tema);
			tcc.Ativo.Should().Be(true);
			tcc.Descricao.Should().Be(tccDto.Descricao);
			tcc.Professores.Should().NotBeEmpty();

			// Alteração
			tcc.AlterarTema("Metodologia ágil");
			tcc.AlterarDescricao("Nova Descrição tema");
			tcc.AlterarAtivo(false);

			tcc.Tema.Should().Be("Metodologia ágil");
			tcc.Ativo.Should().Be(false);
			tcc.Descricao.Should().Be("Nova Descrição tema");
		}

		public static IEnumerable<object[]> DadosPraTestesException =>
		new List<object[]>
		{
			new object[] { "", "Descrição tema tcc", new List<Guid>() { Guid.NewGuid() }, "Tema do TCC deve ser informado." },
			new object[] { string.Empty, "Descrição tema tcc", new List<Guid>() { Guid.NewGuid() }, "Tema do TCC deve ser informado." },
			new object[] { "Automação e Visualização de Dados", "Descrição tema tcc", null, "Deve ser informado pelo menos um professor para orientar sobre o Tema." },
			new object[] { maisDe160, "Descrição tema tcc", new List<Guid>() { Guid.NewGuid() }, "Tema do TCC não pode passar de 160 caracteres." },
			new object[] { "Automação e Visualização de Dados", maisDe3008, new List<Guid>() { Guid.NewGuid() }, "Descrição do TCC não pode passar de 3008 caracteres." }
		};

		[Theory(DisplayName = "Obtém Exception ao Criar Tcc com valores Errados, Nulos ou Vazios")]
		[MemberData(nameof(DadosPraTestesException))]
		public void ObterExceptionAoCriarTccComValoresNulosOuVazios(string tema, string descricao, List<Guid> professores, string erro)
		{
			var tccDto = new TccDto() { Tema = tema, Descricao = descricao, Professores = professores };

			var exception = Assert.Throws<ErroNegocioException>(() => Tcc.Criar(tccDto));
			Assert.Equal(erro, exception.Message);
		}
	}
}
