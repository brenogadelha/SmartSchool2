namespace SmartSchool.Dados.Contextos
{
    public static class ContextoFactory
    {
        public static Contextos Criar() =>
            new Contextos(new SmartContextoBuilder().CreateDbContext(null));
    }
}
