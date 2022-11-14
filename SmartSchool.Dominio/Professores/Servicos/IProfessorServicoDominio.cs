using System;
using System.Threading.Tasks;

namespace SmartSchool.Dominio.Professores.Servicos
{
	public interface IProfessorServicoDominio
	{
		Task<Professor> ObterAsync(Guid idAtividade);
		Task<bool> VerificarExisteProfessorComMesmaMatricula(int matricula, Guid? idAtual);
		Task<bool> VerificarExisteProfessorComMesmoEmail(string email, Guid? idAtual);
	}
}
