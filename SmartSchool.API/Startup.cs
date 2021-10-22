using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using SmartSchool.Comum.Infra;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Dados.Contextos;
using SmartSchool.Ioc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//services.AddDbContext<SmartContexto>(options =>
			//    options.UseSqlServer(AppSettings.Data.DefaultConnectionString));

			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("SmartApi", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "SmartApi", Version = "1.0" });

				var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "SmartSchool.API.xml");
				options.IncludeXmlComments(filePath);
				options.CustomSchemaIds(x => x.FullName);
			});

			services.AddControllers().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

			services.AddAutoMapper(typeof(Startup).Assembly);
			services.AddMemoryCache();

			InjecaoDependencia.Cadastrar(services, Configuration);

			Mapeador.SetMapper(ConfiguracaoAutoMap.Inicializar().CreateMapper());
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			//app.UseHttpsRedirection();

			app.UseRouting();

			app.UseSwagger()
				.UseSwaggerUI(options =>
				{
					options.SwaggerEndpoint("/swagger/SmartApi/swagger.json", "SmartApi");
					options.RoutePrefix = "";
				});


			//app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
