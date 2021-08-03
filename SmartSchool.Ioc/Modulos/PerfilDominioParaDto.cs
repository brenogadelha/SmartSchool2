using AutoMapper;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dto.Dtos.Alunos.Obter;

namespace SmartSchool.Ioc.Modulos
{
    public class PerfilDominioParaDto : Profile
    {
        public PerfilDominioParaDto()
        {
            // Aluno
            this.CreateMap<Aluno, ObterAlunoDto>();
        }
    }
}
