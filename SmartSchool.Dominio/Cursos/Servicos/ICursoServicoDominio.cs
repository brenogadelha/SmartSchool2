using System;
using System.Threading.Tasks;

namespace SmartSchool.Dominio.Cursos.Servicos
{
	public interface ICursoServicoDominio
	{
		Task<Curso> ObterAsync(Guid idAtividade);
		Task<bool> VerificarExisteCursoComMesmoNome(string cpfCnpj, Guid? idAtual);
	}
}
