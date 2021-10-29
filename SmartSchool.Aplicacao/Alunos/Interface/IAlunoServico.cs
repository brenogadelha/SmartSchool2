using SmartSchool.Dto.Alunos;
using SmartSchool.Dto.Alunos.Obter;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Aplicacao.Alunos.Interface
{
    public interface IAlunoServico
    {
        IEnumerable<ObterAlunoDto> Obter();
        void CriarAluno(AlunoDto alunoDto);
        void AlterarAluno(Guid idAluno, AlterarAlunoDto alunoDto, bool? atualizarDisciplinas = null);
        ObterAlunoDto ObterPorId(Guid idAluno);
        void Remover(Guid id);
        IEnumerable<ObterAlunoDto> ObterPorNomeSobrenomeParcial(string busca);
        IEnumerable<ObterHistoricoAlunoDto> ObterHistoricoPorIdAluno(Guid idAluno, int? periodo = null);
    }
}
