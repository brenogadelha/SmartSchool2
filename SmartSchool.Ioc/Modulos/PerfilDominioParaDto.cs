using AutoMapper;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dto.Alunos.Obter;
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
        }
    }
}
