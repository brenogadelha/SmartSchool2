using AutoMapper;
using SmartSchool.Ioc.Modulos;

namespace SmartSchool.Ioc
{
	public static class ConfiguracaoAutoMap
	{
		public static MapperConfiguration Inicializar()
		{
			MapperConfiguration config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new PerfilDtoParaDominio());
				cfg.AddProfile(new PerfilDominioParaDto());
			});

			// novidade necessária para Mapper 7.0
			//Mapper.Initialize(cfg =>
			//{
			//	cfg.AddProfile(new PerfilDtoParaDominio());
			//	cfg.AddProfile(new PerfilDominioParaDto());
			//});

			return config;
		}
	}
}
