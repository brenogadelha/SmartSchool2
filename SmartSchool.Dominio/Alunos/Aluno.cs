using Newtonsoft.Json;
using SmartSchool.Comum.Dominio;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dto.Alunos;
using System;
using System.Collections.Generic;

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

        [JsonIgnore]
        public List<AlunoDisciplina> Disciplinas { get; private set; } = new List<AlunoDisciplina>();

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

    // CRIAR MÉTODO PARA ATUALIZAR DISCIPLINAS DO ALUNO

    //private void AtualizarListaUsuarios(List<Guid> novosIdUsuario, List<Usuario> usuariosAtuais, TipoUsuarioProjetoEnum tipoUsuario)
    //{
    //    // Verifica se foram incluídos novos usuários. Caso não, são removidos os atuais.
    //    if (novosIdUsuario == null || !novosIdUsuario.Any())
    //    {
    //        this.Usuarios.Clear();
    //        return;
    //    }

    //    // Excluir do Projeto os usuários que não estão presentes na nova lista
    //    if (usuariosAtuais != null && usuariosAtuais.Any())
    //        for (int i = usuariosAtuais.Count - 1; i > -1; i--)
    //        {
    //            if (!novosIdUsuario.Any(idNovo => idNovo == usuariosAtuais[i].ID))
    //            {
    //                this.Usuarios.Remove(this.Usuarios.FirstOrDefault(p => p.UsuarioID == usuariosAtuais[i].ID));
    //            }
    //        }

    //    // Adicionar ao Projeto os usuários da lista que são diferentes dos atuais
    //    foreach (Guid guid in novosIdUsuario)
    //        if (!usuariosAtuais.Any(usuario => usuario.ID == guid))
    //        {
    //            this.Usuarios.Add(ProjetoUsuario.Criar(this.ID, guid, tipoUsuario));
    //        }
    //}
}


