using SmartSchool.Dto.Dtos.Professores;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Aplicacao.Professores.Interface
{
    public interface IProfessorServico
    {
        IEnumerable<ObterProfessorDto> Obter();
        void CriarProfessor(ProfessorDto professorDto);
    }
}
