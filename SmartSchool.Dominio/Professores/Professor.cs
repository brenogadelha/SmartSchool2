using SmartSchool.Comum.Dominio;
using SmartSchool.Dto.Professores;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SmartSchool.Dominio.Professores
{
    public class Professor : IEntidade
    {
        public Guid ID { get; private set; }
        public int Matricula { get; private set; }
        public string Nome { get; private set; }

        [JsonIgnore]
        public List<ProfessorDisciplina> Disciplinas { get; private set; } = new List<ProfessorDisciplina>();

        public Professor() { }

        public static Professor Criar(ProfessorDto professorDto)
        {
            var professor = new Professor()
            {
                ID = Guid.NewGuid(),
                Nome = professorDto.Nome,
                Matricula = professorDto.Matricula
            };

            return professor;
        }

        public void AlterarNome(string nome) => this.Nome = nome;
        public void AlterarMatricula(int matricula) => this.Matricula = matricula;
    }

    // CRIAR MÉTODO PARA ATUALIZAR DISCIPLINAS DO PROFESSOR

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
