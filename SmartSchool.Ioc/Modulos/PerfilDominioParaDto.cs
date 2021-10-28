using AutoMapper;
using SmartSchool.Comum.Enums;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dto.Alunos.Obter;
using SmartSchool.Dto.Curso;
using SmartSchool.Dto.Disciplinas.Obter;
using SmartSchool.Dto.Dtos.Professores;
using SmartSchool.Dto.Semestres;

namespace SmartSchool.Ioc.Modulos
{
	public class PerfilDominioParaDto : Profile
	{
		public PerfilDominioParaDto()
		{
			// Aluno
			this.CreateMap<Aluno, ObterAlunoDto>();
			this.CreateMap<SemestreAlunoDisciplina, ObterHistoricoAlunoDto>()
				.ForMember(destino => destino.StatusDisciplinaDescricao, opt => opt.MapFrom(origem => origem.StatusDisciplina.Descricao()))
				.ForMember(destino => destino.NomeDisciplina, opt => opt.MapFrom(origem => origem.AlunoDisciplina.Disciplina.Nome));

			// Professor
			this.CreateMap<Professor, ObterProfessorDto>();

			// Disciplina
			this.CreateMap<Disciplina, ObterDisciplinaDto>();

			// Curso
			this.CreateMap<Curso, CursoDto>();

			// Semestre
			this.CreateMap<Semestre, SemestreDto>();
		}
	}
}
