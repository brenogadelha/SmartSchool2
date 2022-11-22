using AutoMapper;
using SmartSchool.Comum.Enums;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dominio.Tccs;
using SmartSchool.Dto.Alunos.Obter;
using SmartSchool.Dto.Curso;
using SmartSchool.Dto.Disciplinas.Obter;
using SmartSchool.Dto.Dtos.Professores;
using SmartSchool.Dto.Semestres;
using SmartSchool.Dto.Tccs;
using System.Linq;

namespace SmartSchool.Ioc.Modulos
{
	public class PerfilDominioParaDto : Profile
	{
		public PerfilDominioParaDto()
		{
			// Aluno
			this.CreateMap<Aluno, ObterAlunoDto>()
				.ForMember(destino => destino.Curso, opt => opt.MapFrom(origem => origem.Curso.Nome));
			this.CreateMap<SemestreAlunoDisciplina, ObterHistoricoAlunoDto>()
				.ForMember(destino => destino.StatusDisciplinaDescricao, opt => opt.MapFrom(origem => origem.StatusDisciplina.Descricao()))
				.ForMember(destino => destino.NomeDisciplina, opt => opt.MapFrom(origem => origem.AlunoDisciplina.Disciplina.Nome));

			// Professor
			this.CreateMap<Professor, ObterProfessorDto>()
				.ForMember(destino => destino.Disciplinas, opt => opt.MapFrom(origem => origem.Disciplinas.Select(d => d.Nome).ToList()));
			this.CreateMap<Professor, ObterProfessorLightDto>();

			// Disciplina
			this.CreateMap<Disciplina, ObterDisciplinaDto>()
				.ForMember(destino => destino.Professores, opt => opt.MapFrom(origem => origem.Professores.Select(d => d.Nome).ToList()));

			// Curso
			this.CreateMap<Curso, CursoDto>();
			this.CreateMap<Curso, ObterCursoDto>()
				.ForMember(destino => destino.Disciplinas, opt => opt.MapFrom(origem => origem.Disciplinas.Select(d => d.Nome).ToList()));

			// Semestre
			this.CreateMap<Semestre, SemestreDto>();
			this.CreateMap<Semestre, ObterSemestreDto>();

			// Tcc
			this.CreateMap<Tcc, ObterTccDto>()
				.ForMember(destino => destino.Professores, opt => opt.MapFrom(origem => origem.TccProfessores.Select(tp => tp.Professor)));
			this.CreateMap<Tcc, ObterTccsDto>()
				.ForMember(destino => destino.Professor, opt => opt.MapFrom(origem => origem.TccProfessores.FirstOrDefault().Professor.Nome));

			this.CreateMap<TccAlunoProfessor, ObterSolicitacoesTccsDto>()
				.ForMember(destino => destino.Tema, opt => opt.MapFrom(origem => origem.ProfessorTcc.Tcc.Tema))
				.ForMember(destino => destino.Status, opt => opt.MapFrom(origem => origem.Status.Descricao()))
				.ForMember(destino => destino.NomeAluno, opt => opt.MapFrom(origem => origem.Aluno.Nome))
				.ForMember(destino => destino.EmailAluno, opt => opt.MapFrom(origem => origem.Aluno.Email))
				.ForMember(destino => destino.MatriculaAluno, opt => opt.MapFrom(origem => origem.Aluno.Matricula));

			this.CreateMap<TccAlunoProfessor, ObterStatusSolicitacaoTccDto>()
				.ForMember(destino => destino.NomeProfessor, opt => opt.MapFrom(origem => origem.ProfessorTcc.Professor.Nome))
				.ForMember(destino => destino.Tema, opt => opt.MapFrom(origem => origem.ProfessorTcc.Tcc.Tema))
				.ForMember(destino => destino.EmailProfessor, opt => opt.MapFrom(origem => origem.ProfessorTcc.Professor.Email))
				.ForMember(destino => destino.Status, opt => opt.MapFrom(origem => origem.Status.Descricao()));
		}
	}
}
