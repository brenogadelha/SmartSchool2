using SmartSchool.Aplicacao.Disciplinas.Interface;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Comum.Validacao;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Disciplinas.Especificacao;
using SmartSchool.Dominio.Disciplinas.Validacao;
using SmartSchool.Dominio.Professores.Especificacao;
using SmartSchool.Dto.Disciplinas;
using SmartSchool.Dto.Disciplinas.Alterar;
using SmartSchool.Dto.Disciplinas.Obter;
using SmartSchool.Dto.Dtos.Professores;
using System;
using System.Collections.Generic;

namespace SmartSchool.Aplicacao.Disciplinas.Servico
{
    public class DisciplinaServico : IDisciplinaServico
    {
        private readonly IRepositorio<Disciplina> _disciplinaRepositorio;

        public DisciplinaServico(IRepositorio<Disciplina> disciplinaRepositorio)
        {
            this._disciplinaRepositorio = disciplinaRepositorio;
        }

        public IEnumerable<ObterDisciplinaDto> Obter()
        {
            var disciplina = this._disciplinaRepositorio.Procurar(new BuscaDeDisciplinaEspecificacao().IncluiInformacoesDeProfessor());

            return disciplina.MapearParaDto<ObterDisciplinaDto>();
        }

        public void CriarDisciplina(DisciplinaDto disciplinaDto)
        {
            this.VerificarExisteDisciplinaComMesmoNome(disciplinaDto.Nome, null);

            var disciplina = Disciplina.Criar(disciplinaDto);

            this._disciplinaRepositorio.Adicionar(disciplina);
        }

        public void AlterarDisciplina(Guid idDisciplina, AlterarDisciplinaDto disciplinaDto)
        {
            this.VerificarExisteDisciplinaComMesmoNome(disciplinaDto.Nome, disciplinaDto.ID);
            ValidacaoFabrica.Validar(disciplinaDto, new DisciplinaValidacao());

            var disciplina = this.ObterDisciplinaDominio(idDisciplina);

            disciplina.AlterarNome(disciplinaDto.Nome);
            disciplina.AlterarPeriodo(disciplinaDto.Periodo);

            this._disciplinaRepositorio.Atualizar(disciplina, true);
        }

        public ObterDisciplinaDto ObterPorId(Guid idDisciplina) => this.ObterDisciplinaDominio(idDisciplina).MapearParaDto<ObterDisciplinaDto>();

        public IEnumerable<ObterProfessorDto> ObterProfessores(Guid idDisciplina)
        {
            var disciplina = this.ObterDisciplinaDominio(idDisciplina);

            return disciplina.Professores.MapearParaDto<ObterProfessorDto>();
        }

        public void Remover(Guid id)
        {
            var disciplina = this.ObterDisciplinaDominio(id);

            this._disciplinaRepositorio.Remover(disciplina);
        }

        private Disciplina ObterDisciplinaDominio(Guid idDisciplina)
        {
            if (idDisciplina.Equals(Guid.Empty))
                throw new ArgumentNullException(null, "Id nulo da Disciplina (não foi informado).");


            var disciplina = this._disciplinaRepositorio.Obter(new BuscaDeDisciplinaPorIdEspecificacao(idDisciplina).IncluiInformacoesDeProfessor());

            if (disciplina == null)
                throw new RecursoInexistenteException($"Disciplina com ID '{idDisciplina}' não existe.");

            return disciplina;
        }

        private void VerificarExisteDisciplinaComMesmoNome(string nome, Guid? idAtual)
        {
            var alunoComMesmoNome = this._disciplinaRepositorio.Obter(new BuscaDeDisciplinaPorNomeEspecificacao(nome));
            if (alunoComMesmoNome != null && (!idAtual.HasValue || idAtual.HasValue && alunoComMesmoNome.ID != idAtual))
                throw new ErroNegocioException($"Já existe uma Disciplina com o mesmo nome '{nome}'.");
        }
    }
}
