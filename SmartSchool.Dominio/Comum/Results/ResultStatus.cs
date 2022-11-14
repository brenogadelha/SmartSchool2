namespace SmartSchool.Dominio.Comum.Results
{
    public enum ResultStatus
    {
        /// <summary>
        /// 200 OK
        /// Utilizado quando uma operação ou consulta é executada com sucesso.
        /// Ex: Atualizações de cadastro, consulta de CEP, exclusões, etc.
        /// </summary>
        Ok,

        /// <summary>
        /// 201 Created
        /// Utilizado quando um novo recurso é criado.
        /// Ex: Cadastro de um usuário, criação de uma nova venda, etc.
        /// </summary>
        Created,

        /// <summary>
        /// 400 Bad Request
        /// Erro no formato da requisição ou nos parametros da consulta.
        /// Ex: Formato do json inválido, parametro obrigatório de um consulta não informado, etc.
        /// </summary>
        BadRequest,

        /// <summary>
        /// 404 Not Found
        /// Utilizado para retornar um erro de quando um recurso não é encontrado em uma consulta.
        /// Ex: Busca de CEP que não existe, busca por certificado que não existe, etc.
        /// </summary>
        NotFound,

        /// <summary>
        /// 422 Unprocessable Entity
        /// Utilizado para retornar um erro de negócio.
        /// Ex: Representante legal não informado para proponente menor de idade, valor da proposta abaixo do mínimo necessário para o produto, etc.
        /// </summary>
        UnprocessableEntity,

        /// <summary>
        /// 401 Unauthorized
        /// Utilizado quando o usuário não está autenticado ou a sessão está expirada.
        /// Ex: Chamada a API sem token ou com token invalido ou expirado.
        /// </summary>
        Unauthorized,

        /// <summary>
        /// 403 Forbidden
        /// Utilizado quando o usuário está autenticação mas não tem permissão para executar uma consulta ou operação.
        /// Ex: Um terceiro tenta fazer uma requisição num recurso gerencial.
        /// </summary>
        Forbidden,

        /// <summary>
        /// 500 Internal Server Error
        /// Erro quando ocorre uma falha inesperada no servidor.
        /// Ex: Falha de conexão de rede com recursos externos, eventuais exceções não tratadas, indisponibilidade em serviços de terceiros, etc.
        /// </summary>
        InternalServerError,
    }
}