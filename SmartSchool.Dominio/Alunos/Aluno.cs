using Newtonsoft.Json;
using SmartSchool.Comum.Dominio;
using SmartSchool.Dto.Alunos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.Dominio.Alunos
{
    public class Aluno : IEntidade
    {
        public Guid ID { get; private set; }
        public int Matricula { get; private set; }
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public string Telefone { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataFim { get; private set; }
        public bool Ativo { get; private set; }

        //[JsonIgnore]
        //public IEnumerable<AlunoDisciplina> AlunosDisciplinas { get; set; }

        public Aluno() { }
        public static Aluno Criar(AlunoDto alunoDto)
        {
            var aluno = new Aluno()
            {
                ID = Guid.NewGuid(),
                Nome = alunoDto.Nome,
                Sobrenome = alunoDto.Sobrenome,
                Telefone = alunoDto.Telefone,
                DataInicio = alunoDto.DataInicio,
                Ativo = alunoDto.Ativo,
                DataNascimento = alunoDto.DataNascimento,
                Matricula = alunoDto.Matricula
            };

            return aluno;
        }

        public void AlterarNome(string nome) => this.Nome = nome;
        public void AlterarSobrenome(string sobrenome) => this.Sobrenome = sobrenome;
        public void AlterarTelefone(string telefone) => this.Telefone = telefone;
        public void AlterarAtivo(bool ativo) => this.Ativo = ativo;
        public void AlterarDataNascimento(DateTime dataNascimento) => this.DataNascimento = dataNascimento;
        public void AlterarDataInicio(DateTime dataInicio) => this.DataInicio = dataInicio;
        public void AlterarDataFim(DateTime dataFim) => this.DataFim = dataFim;
    }
}


