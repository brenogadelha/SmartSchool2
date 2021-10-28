using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace SmartSchool.Comum.Enums
{
    /// <summary>
    /// Métodos de extensão relativos a Enum's.
    /// </summary>
    public static class EnumExtensions
    {
        #region Public Methods
        /// <summary>
        /// Extension Method que retorna a lista de itens existentes em um Enum,
        /// em um formato que pode ser feito "Bind" com controles Data Bound.
        /// Esse formato é um DTO (<seealso cref="EnumItemDto">EnumItensDTO</seealso>)
        /// contendo o código do item (inteiro) e a descrição do item. Caso o item não
        /// contenha o atributo "Description", o valor do item é exibido. A lista é
        /// retornada ordenada pela descrição.
        /// </summary>
        ///
        /// <param name="value">O item do qual obter a descrição</param>
        /// <returns>Uma lista de <see cref="EnumItemDto"/> contendo os valores do enumerado</returns>
        ///
        /// <remarks>
        /// Útil para ser utilizado na camada de interface como DataSource para
        /// DropDownList's, por exemplo, exibindo valores amigáveis na mesma.<br/><br/>
        /// 
        /// Ex.: No seguinte Enum:<br/><br/>
        /// 
        /// <code>
        /// enum Situacao
        /// {
        ///     Aberto,
        ///     
        ///     Fechado,
        ///     
        ///     [Description("Em andamento")]
        ///     EmAndamento,
        ///     
        ///     EmAtraso
        /// }
        /// </code>
        /// 
        /// <br/>
        /// Teremos o seguinte comportamento:<br/><br/>
        /// 
        /// <code>
        /// DropDownList ddlSituacao;
        /// 
        /// ddlSituacao.DataValueField = "Id";
        /// ddlSituacao.DataTextField = "Description";
        /// ddlSituacao.DataSource = default(Situacao).ToEnumDescriptions();
        /// ddlSituacao.DataBind();
        /// 
        /// => A DropDownList exibirá os itens:
        ///     &lt;option value="0"&gt;Aberto&lt;/option&gt;
        ///     &lt;option value="2"&gt;Em andamento&lt;/option&gt;
        ///     &lt;option value="3"&gt;EmAtraso&lt;/option&gt;
        ///     &lt;option value="1"&gt;Fechado&lt;/option&gt;
        /// </code>
        /// </remarks>
        public static IList<EnumItemDto> GetEnumDescriptions(this System.Enum value)
        {
            return ObterEnumItens(value, true, true);
        }

        /// <summary>
        /// Extension Method que retorna a lista de itens existentes em um Enum,
        /// em um formato que pode ser feito "Bind" com controles Data Bound.
        /// Esse formato é um DTO (<seealso cref="EnumItemDto">EnumItemDTO</seealso>)
        /// contendo o código do item (inteiro) e a descrição do item. Caso o item não
        /// contenha o atributo "Description", o valor do item é exibido. A lista é
        /// retornada ordenada pela descrição.
        /// </summary>
        ///
        /// <param name="value">O item do qual obter a descrição</param>
        /// <param name="sort">Indica se a lista será ordenada por descrição</param>
        /// <returns>Uma lista de <see cref="EnumItemDto"/> contendo os valores do enumerado</returns>
        ///
        /// <remarks>
        /// Útil para ser utilizado na camada de interface como DataSource para
        /// DropDownList's, por exemplo, exibindo valores amigáveis na mesma.<br/><br/>
        /// 
        /// Ex.: No seguinte Enum:<br/><br/>
        /// 
        /// <code>
        /// enum Situacao
        /// {
        ///     Aberto,
        ///     
        ///     Fechado,
        ///     
        ///     [Description("Em andamento")]
        ///     EmAndamento,
        ///     
        ///     EmAtraso
        /// }
        /// </code>
        /// 
        /// <br/>
        /// Teremos o seguinte comportamento:<br/><br/>
        /// 
        /// <code>
        /// DropDownList ddlSituacao;
        /// 
        /// ddlSituacao.DataValueField = "Id";
        /// ddlSituacao.DataTextField = "Description";
        /// ddlSituacao.DataSource = default(Situacao).ToEnumDescriptions(false);
        /// ddlSituacao.DataBind();
        /// 
        /// => A DropDownList exibirá os itens:
        ///     &lt;option value="0"&gt;Aberto&lt;/option&gt;
        ///     &lt;option value="1"&gt;Fechado&lt;/option&gt;
        ///     &lt;option value="2"&gt;Em andamento&lt;/option&gt;
        ///     &lt;option value="3"&gt;EmAtraso&lt;/option&gt;
        /// </code>
        /// </remarks>
        public static IList<EnumItemDto> GetEnumDescriptions(this System.Enum value, bool sort)
        {
            return ObterEnumItens(value, sort, true);
        }

        /// <summary>
        /// Extension Method que retorna a lista de String's de um enumerado
        /// </summary>
        /// <param name="value">O enumerado a ser utilizado</param>
        /// <returns>Uma lista de <see cref="EnumItemDto"/> contendo os valores do enumerado</returns>
        public static IList<EnumItemDto> GetEnumStrings(this System.Enum value)
        {
            return ObterEnumItens(value, true);
        }

        /// <summary>
        /// Extension Method que retorna a lista de <see cref="EnumItemDto"/> de um enumerado.
        /// </summary>
        /// <param name="value">O enumerado a ser utilizado</param>
        /// <param name="sort">Informa se a lista deverá ser ordenada alfabeticamente, pelo valor da string do enumerado.</param>
        /// <returns>Uma lista de <see cref="EnumItemDto"/> contendo os valores do enumerado</returns>
        public static IList<EnumItemDto> GetEnumStrings(this System.Enum value, bool sort)
        {
            return ObterEnumItens(value, sort);
        }

        /// <summary>
        /// Extension Method que retorna a lista de <see cref="EnumItemDto"/> de um enumerado.
        /// </summary>
        /// <param name="value">O enumerado a ser utilizado</param>
        /// <returns>Uma lista de <see cref="EnumItemDto"/> contendo os valores do enumerado</returns>
        public static IList<EnumItemDto> GetEnumValues(this System.Enum value)
        {
            return GetEnumValues(value, true);
        }

        /// <summary>
        /// Extension Method que retorna a lista de <see cref="EnumItemDto"/> de um enumerado.
        /// </summary>
        /// <param name="value">O enumerado a ser utilizado</param>
        /// <param name="sort">Informa se a lista deverá ser ordenada pelo Valor (ID) do enumerado.</param>
        /// <returns>Uma lista de <see cref="EnumItemDto"/> contendo os valores do enumerado</returns>
        public static IList<EnumItemDto> GetEnumValues(this System.Enum value, bool sort)
        {
            var list = ObterEnumItens(value, sort);

            if (sort && (list != null && list.Any()))
            {
                list.ToList().Sort((enum1, enum2) => enum1.Id.CompareTo(enum2.Id));
            }

            return list;
        }

        /// <summary>
        /// Extension Method que converte uma string em um item de um enumerado
        /// </summary>
        /// <remarks>
        /// Ex.: No seguinte Enum:<br/><br/>
        /// 
        /// <code>
        /// enum Situacao
        /// {
        ///     Aberto,
        ///     Fechado,
        ///     EmAndamento,
        ///     EmAtraso
        /// }
        /// </code>
        /// 
        /// <br/>
        /// Teremos o seguinte comportamento:<br/><br/>
        /// 
        /// <code>
        /// string valor = "Aberto";
        /// Situacao valorConvertido = default(Situacao).ParseToEnum(valor); // Terá o valor Aberto do enum Situacao
        /// </code>
        /// </remarks>
        /// <typeparam name="T">O tipo do enumerado</typeparam>
        /// <param name="value">Um item do enumerado</param>
        /// <param name="valueToConvert">O valor a ser convertido</param>
        /// <returns>Um item do enumerado informado</returns>
        public static T ParseToEnum<T>(this System.Enum value, string valueToConvert)
        {
            return (T)System.Enum.Parse(typeof(T), valueToConvert, false);
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)System.Enum.Parse(typeof(T), value, true);
        }

        public static T ParseToEnum<T>(this int value)
        {
            return (T)System.Enum.Parse(typeof(T), value.ToString(CultureInfo.InvariantCulture), false);
        }

        public static T? ParseToEnum<T>(this int? value) where T : struct
        {
            return value == null ? null : (T?)System.Enum.Parse(typeof(T), value.ToString(), false);
        }

        public static string Descricao(this System.Enum value)
        {
            return ObterDescription(value);
        }

        #endregion

        #region Private Members

        private const char EnumSeparatorCharacter = ',';

        private static string ObterDescription(System.Enum value)
        {
            // Check for Enum that is marked with FlagAttribute
            var entries = value.ToString().Split(EnumSeparatorCharacter);
            var description = new string[entries.Length];

            for (var i = 0; i < entries.Length; i++)
            {
                var fieldInfo = value.GetType().GetField(entries[i].Trim());
                var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                description[i] = (attributes.Length > 0) ? attributes[0].Description : entries[i].Trim();
            }

            return String.Join(", ", description);
        }

        private static IList<EnumItemDto> ObterEnumItens<T>(T enumerator, bool sort, bool useDescriptionAttributeIfHasOne = false)
        {
            List<EnumItemDto> result = null;

            var enumType = enumerator.GetType();

            if (enumType.IsEnum)
            {
                var enumItens = enumType.GetFields()
                                .Where(field => field.IsLiteral);

                if (enumItens != null && enumItens.Any())
                {
                    var names = enumItens.Select(field => field.GetValue(enumType))
                                .Select(value => (T)value)
                                .ToArray();

                    if (names.Any())
                    {
                        result = new List<EnumItemDto>(names.Count());

                        foreach (var item in names)
                        {
                            var itemEnumType = enumType.Assembly.GetType(enumType.FullName);
                            var enumItem1 = itemEnumType.GetField(item.ToString());

                            var intValue = (int)enumItem1.GetValue(itemEnumType);
                            var stringValue = enumItem1.GetValue(itemEnumType).ToString();

                            var itemInstance = (T)System.Enum.ToObject(enumType, intValue);

                            // Get instance of the attribute.
                            var description = itemInstance.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);

                            if (useDescriptionAttributeIfHasOne && (description.Any()))
                            {
                                result.Add(new EnumItemDto { Id = intValue, Value = stringValue, Description = ((DescriptionAttribute)description.First()).Description });
                            }
                            else
                            {
                                result.Add(new EnumItemDto { Id = intValue, Value = stringValue, Description = item.ToString() });
                            }
                        }
                    }
                }

                if (sort && result != null)
                {
                    // Cria um método anônimo para fazer a comparação entre os itens da lista e ordená-la pela descrição
                    result = result.OrderBy(x => x.Description).ToList();
                }
            }

            return result;
        }

        #endregion
    }

    /// <summary>
    /// Classe que representa um item de um Enum,
    /// usado para fazer "Bind" dos itens existentes
    /// em um Enum com componentes "Data Bound".
    /// </summary>
    [Serializable]
    public class EnumItemDto
    {
        /// <summary>
        /// O id do item
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// O valor textual do item
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// O texto do atributo Description do item
        /// </summary>
        public string Description { get; set; }
    }
}
