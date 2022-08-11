using MediatR;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Alunos.Especificacao;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dto.Alunos.Obter;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Alunos.ObterAlunoNome
{
	public class ObterAlunoNomeHandler : IRequestHandler<ObterAlunoNomeCommand, IResult>
    {
        private readonly IRepositorioTask<Aluno> _alunoRepositorio;

        public ObterAlunoNomeHandler(IRepositorioTask<Aluno> alunoRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
        }

        public async Task<IResult> Handle(ObterAlunoNomeCommand request, CancellationToken cancellationToken)
        {
            var alunos = await this._alunoRepositorio.Procurar(new BuscaDeAlunoPorNomeParcialEspecificacao(request.Busca));

            if (!alunos.Any())
                throw new RecursoInexistenteException($"Não foi encontrado nenhum aluno com o parametro '{request.Busca}' informado.");

            return Result<IEnumerable<ObterAlunoDto>>.Success(alunos.MapearParaDto<ObterAlunoDto>());
        }
    }
}
