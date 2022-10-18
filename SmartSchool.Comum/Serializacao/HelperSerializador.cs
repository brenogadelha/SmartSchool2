using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SmartSchool.Comum.Serializacao
{
	public static class HelperSerializador
	{
		public static string ConverterEmJson(this object objeto)
		{
			var objetoSerializado = JsonConvert.SerializeObject(objeto, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
#if DEBUG
			if (objetoSerializado.Length > 512000) // se passou de 500kb tem algo errado, normal é ser 3-5-10kb
			{
				throw new OverflowException($"Serialização do objeto {objeto.ToString()} ultrapassou 500kb, rever Domain dele, pode estar faltando [JsonIgnore] em algum campo");
			}
#endif
			return objetoSerializado;
		}

		public static T ConverterEmObjeto<T>(this string objeto)
		{
			return JsonConvert.DeserializeObject<T>(objeto, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
		}
	}
}
