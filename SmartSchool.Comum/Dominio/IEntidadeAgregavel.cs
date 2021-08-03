namespace SmartSchool.Comum.Dominio
{
    public interface IEntidadeAgregavel<out TType> : IEntidade
    {
        TType ID { get; }
    }
}
