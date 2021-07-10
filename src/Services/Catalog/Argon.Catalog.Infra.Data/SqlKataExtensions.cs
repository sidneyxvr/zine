using SqlKata.Compilers;

namespace SqlKata
{
    public static class SqlKataExtentions
    {
        public static SqlResult? GetSqlResult(this Query query)
        {
            return new SqlServerCompiler().Compile(query);
        }
    }
}
