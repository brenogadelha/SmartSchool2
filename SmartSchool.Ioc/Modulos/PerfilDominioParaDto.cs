using AutoMapper;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dto.Alunos.Obter;
using SmartSchool.Dto.Disciplinas.Obter;
using SmartSchool.Dto.Dtos.Professores;

namespace SmartSchool.Ioc.Modulos
{
    public class PerfilDominioParaDto : Profile
    {
        public PerfilDominioParaDto()
        {
            // Aluno
            this.CreateMap<Aluno, ObterAlunoDto>();

            // Professor
            this.CreateMap<Professor, ObterProfessorDto>();

            // Disciplina
            this.CreateMap<Disciplina, ObterDisciplinaDto>();
        }
    }
}
