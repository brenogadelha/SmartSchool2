using SmartSchool.Aplicacao.Alunos.Adicionar;
using SmartSchool.Aplicacao.Alunos.Alterar;
using SmartSchool.Dto.Alunos;
using System;
using System.Collections.Generic;

namespace SmartSchool.Testes.Compartilhado.Builders
{
	public class AlunoDtoBuilder : Builder<AlunoDto>
	{
		private Guid _id;
		private Guid _cursoId;
		private string _nome;
		private bool _ativo;
		private string _sobrenome;
		private int _matricula;
		private List<AlunoDisciplinaDto> _alunosDisciplinas;
		private string _cpf;
		private string _email;
		private string _telefone;
		private string _endereco;
		private string _celular;
		private DateTime _dataNascimento;
		private DateTime _dataInicio;
		private DateTime _dataFim;
		private string _cidade;

		public static AlunoDtoBuilder Novo => new AlunoDtoBuilder();

		public AlunoDtoBuilder() { }

		public AlunoDtoBuilder ComId(Guid id)
		{
			this._id = id;
			return this;
		}

		public AlunoDtoBuilder ComCursoId(Guid id)
		{
			this._cursoId = id;
			return this;
		}

		public AlunoDtoBuilder ComNome(string nome)
		{
			this._nome = nome;
			return this;
		}

		public AlunoDtoBuilder ComAtivo(bool ativo)
		{
			this._ativo = ativo;
			return this;
		}

		public AlunoDtoBuilder ComSobrenome(string sobrenome)
		{
			this._sobrenome = sobrenome;
			return this;
		}

		public AlunoDtoBuilder ComEndereco(string endereco)
		{
			this._endereco = endereco;
			return this;
		}

		public AlunoDtoBuilder ComMatricula(int matricula)
		{
			this._matricula = matricula;
			return this;
		}

		public AlunoDtoBuilder ComAlunosDisciplinas(List<AlunoDisciplinaDto> alunosDisciplinas)
		{
			this._alunosDisciplinas = alunosDisciplinas;
			return this;
		}

		public AlunoDtoBuilder ComCpfCnpj(string cpf)
		{
			this._cpf = cpf;
			return this;
		}

		public AlunoDtoBuilder ComEmail(string email)
		{
			this._email = email;
			return this;
		}

		public AlunoDtoBuilder ComTelefone(string telefone)
		{
			this._telefone = telefone;
			return this;
		}

		public AlunoDtoBuilder ComCelular(string celular)
		{
			this._celular = celular;
			return this;
		}

		public AlunoDtoBuilder ComDataNascimento(DateTime dataNascimento)
		{
			this._dataNascimento = dataNascimento;
			return this;
		}

		public AlunoDtoBuilder ComDataInicio(DateTime dataInicio)
		{
			this._dataInicio = dataInicio;
			return this;
		}

		public AlunoDtoBuilder ComDataFim(DateTime dataFim)
		{
			this._dataFim = dataFim;
			return this;
		}

		public AlunoDtoBuilder ComCidade(string cidade)
		{
			this._cidade = cidade;
			return this;
		}

		public override AlunoDto Instanciar()
		{
			var dto = new AlunoDto()
			{
				Sobrenome = this._sobrenome,
				DataNascimento = this._dataNascimento,
				Cpf = this._cpf,
				Email = this._email,
				Nome = this._nome,
				Telefone = this._telefone,
				Celular = this._celular,
				Cidade = this._cidade,
				Matricula = this._matricula,
				CursoId = this._cursoId,
				AlunosDisciplinas = this._alunosDisciplinas,
				DataInicio = this._dataInicio,
				DataFim = this._dataFim,
				Endereco = this._endereco
			};

			return dto;
		}

		public AlterarAlunoDto InstanciarAlteracao()
		{
			var dto = new AlterarAlunoDto()
			{
				Sobrenome = this._sobrenome,
				DataNascimento = this._dataNascimento,
				Cpf = this._cpf,
				Email = this._email,
				Nome = this._nome,
				Telefone = this._telefone,
				Celular = this._celular,
				Cidade = this._cidade,
				Matricula = this._matricula,
				CursoId = this._cursoId,
				AlunosDisciplinas = this._alunosDisciplinas,
				DataInicio = this._dataInicio,
				DataFim = this._dataFim,
				Ativo = this._ativo,
				ID = this._id,
				Endereco = this._endereco
			};

			return dto;
		}

		public AdicionarAlunoCommand InstanciarCommand()
		{
			var dto = new AdicionarAlunoCommand()
			{
				Sobrenome = this._sobrenome,
				DataNascimento = this._dataNascimento,
				Cpf = this._cpf,
				Email = this._email,
				Nome = this._nome,
				Telefone = this._telefone,
				Celular = this._celular,
				Cidade = this._cidade,
				Matricula = this._matricula,
				CursoId = this._cursoId,
				AlunosDisciplinas = this._alunosDisciplinas,
				DataInicio = this._dataInicio,
				DataFim = this._dataFim,
				Endereco = this._endereco
			};

			return dto;
		}

		public AlterarAlunoCommand InstanciarCommandAlteracao()
		{
			var dto = new AlterarAlunoCommand()
			{
				ID = this._id,
				Sobrenome = this._sobrenome,
				DataNascimento = this._dataNascimento,
				Cpf = this._cpf,
				Email = this._email,
				Nome = this._nome,
				Telefone = this._telefone,
				Celular = this._celular,
				Cidade = this._cidade,
				Matricula = this._matricula,
				CursoId = this._cursoId,
				AlunosDisciplinas = this._alunosDisciplinas,
				DataInicio = this._dataInicio,
				DataFim = this._dataFim,
				Endereco = this._endereco
			};

			return dto;
		}

		//public ObterAlunoDto InstanciarListagem()
		//{
		//	var dto = new ObterAlunoDto()
		//	{
		//		ID = this._id,
		//		Sobrenome = this._sobrenome,
		//		DataNascimento = this._dataNascimento,
		//		CpfCnpj = this._cpfcnpj,
		//		Email = this._email,
		//		Nome = this._nome,
		//		Telefone = this._telefone,
		//		Celular = this._celular,
		//		Cidade = this._cidade,
		//		Ativo = this._ativo
		//	};

		//	return dto;
		//}
	}
}
