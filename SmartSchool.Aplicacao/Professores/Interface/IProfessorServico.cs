using SmartSchool.Dto.Dtos.Professores;
using SmartSchool.Dto.Professores;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Aplicacao.Professores.Interface
{
    public interface IProfessorServico
    {
        IEnumerable<ObterProfessorDto> Obter();
        void CriarProfessor(ProfessorDto professorDto);
        void AlterarProfessor(Guid idProfessor, AlterarProfessorDto professorDto, bool? atualizarDisciplinas = null);
        ObterProfessorDto ObterPorId(Guid idProfessor);
        void Remover(Guid id);
    }
}
