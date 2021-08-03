using System.Collections.Generic;
using AutoMapper;
using SmartSchool.Comum.Dominio;

namespace SmartSchool.Comum.Mapeador
{
	public static class Mapeador
	{
        public static void SetMapper(IMapper mapper)
		{
            autoMapper = mapper;
		}

        private static IMapper autoMapper; // = ConfiguracaoAutoMap.Inicializar().CreateMapper();

        public static TRetorno MapearParaDto<TRetorno>(this IEntidade objeto)
        {
            return autoMapper.Map<TRetorno>(objeto);
            // return Mapper.Map<TRetorno>(objeto);
        }

        public static IEnumerable<TRetorno> MapearParaDto<TRetorno>(this IEnumerable<IEntidade> objeto)
        {
            return autoMapper.Map<IEnumerable<TRetorno>>(objeto);
        }

        public static TRetorno MapearParaDominio<TRetorno>(this object objeto) where TRetorno : IEntidade
        {
            return autoMapper.Map<TRetorno>(objeto);
        }

        public static IEnumerable<TRetorno> MapearParaDominio<TRetorno>(this IEnumerable<object> objeto) where TRetorno : IEntidade
        {
            return autoMapper.Map<IEnumerable<TRetorno>>(objeto);
        }
    }
}
