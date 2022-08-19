using SmartSchool.Comum.Dominio;
using System;

namespace SmartSchool.Dominio.TCCS
{
	public class tcc : IEntidadeAgregavel<Guid>
	{
		public Guid ID { get; private set; }
		public string Tema { get; set; }
		public string Descricao { get; set; }
		public string Objetivo { get; set; }
		public string Problematica { get; set; }

		// Fazer relacionamento tcc/aluno/professores (verificar onde fica melhor) com status: solicitado, aceito, negado.
		// Guardar data de solicitação.
		// Fazer métodos GET para tcc's com status "SOLICITADO".
		// Se negado, informar o motivo para o aluno.
	}
}


