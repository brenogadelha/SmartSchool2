using SmartSchool.Dto.Disciplinas;
using SmartSchool.Dto.Disciplinas.Alterar;
using SmartSchool.Dto.Disciplinas.Obter;
using SmartSchool.Dto.Dtos.Professores;
using SmartSchool.Dto.Professores;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Aplicacao.Disciplinas.Interface
{
    public interface IDisciplinaServico
    {
        IEnumerable<ObterDisciplinaDto> Obter();
        void CriarDisciplina(DisciplinaDto disciplinaDto);
        void AlterarDisciplina(Guid idDisciplina, AlterarDisciplinaDto disciplinaDto);
        ObterDisciplinaDto ObterPorId(Guid idDisciplina);
        void Remover(Guid id);
    }
}
