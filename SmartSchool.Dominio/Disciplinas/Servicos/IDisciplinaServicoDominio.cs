using System;
using System.Threading.Tasks;

namespace SmartSchool.Dominio.Disciplinas.Servicos
{
	public interface IDisciplinaServicoDominio
	{
		Task<Disciplina> ObterAsync(Guid idDisciplina);
		Task<bool> VerificarExisteDisciplinaComMesmoNome(string nome, Guid? idAtual);
	}
}
