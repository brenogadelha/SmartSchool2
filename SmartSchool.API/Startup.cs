using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using SmartSchool.API.Componentes.ControleDeErros;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Ioc;
using System;
using System.Globalization;
using System.IO;

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
			services.AddMyMediatR();

			services.AddHsts(options =>
			{
				options.Preload = true;
				options.IncludeSubDomains = true;
				options.MaxAge = TimeSpan.FromDays(365);
			});

			var culture = "pt-BR";
			var supportedCultures = new[] { new CultureInfo(culture) };
			services.Configure<RequestLocalizationOptions>(options =>
			{
				options.DefaultRequestCulture = new RequestCulture(culture: culture, uiCulture: culture);
				options.SupportedCultures = supportedCultures;
				options.SupportedUICultures = supportedCultures;
			});

			services.AddCors(options =>
			{
				options.AddPolicy(name: "PoliticaSmartSchool", p => p
																	.AllowAnyMethod()
																	.AllowAnyHeader()
																	.SetIsOriginAllowed(origin => true)
																	.AllowCredentials());
			});

			services.AddMvc(options =>
			{
				options.Filters.Add(typeof(ExceptionFilter));
				options.EnableEndpointRouting = false;
			});

			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("SmartApi", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "SmartApi", Version = "1.0" });

				var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "SmartSchool.API.xml");
				options.IncludeXmlComments(filePath);
				options.CustomSchemaIds(x => x.FullName);
			});

			services.AddControllers()
			   .AddNewtonsoftJson(options =>
			   {
				   options.SerializerSettings.DateFormatString = "dd/MM/yyyy HH:mm:ss";
				   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
			   });

			services.AddAutoMapper(typeof(Startup).Assembly);
			services.AddMemoryCache();

			InjecaoDependencia.Cadastrar(services, Configuration);

			Mapeador.SetMapper(ConfiguracaoAutoMap.Inicializar().CreateMapper());
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			var supportedCultures = new[] { new CultureInfo("pt-BR") };
			app.UseRequestLocalization(new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture("pt-BR"),
				SupportedCultures = supportedCultures,
				SupportedUICultures = supportedCultures
			});

			app.UseSwagger()
				.UseSwaggerUI(options =>
				{
					options.SwaggerEndpoint("/swagger/SmartApi/swagger.json", "SmartApi");
					options.RoutePrefix = "";
				});

			//			if (env.IsDevelopment())
			//			{
			//				app.UseSwaggerUI(c =>
			//				{
			//					string path = AppSettings.Data.UrlBase;

			//					if (!string.IsNullOrEmpty(path))
			//						path = $"/{path}";

			//#if DEBUG
			//					path = string.Empty;
			//#endif

			//					c.SwaggerEndpoint("/swagger/SmartApi/swagger.json", "SmartApi");
			//					c.RoutePrefix = "";
			//				});
			//			}

			//app.UseEndpoints(endpoints =>
			//{
			//	endpoints.MapControllers();
			//});

			app.UseHsts();
			app.UseCors("PoliticaSmartSchool");
			app.UseMvc();
		}
	}
}
