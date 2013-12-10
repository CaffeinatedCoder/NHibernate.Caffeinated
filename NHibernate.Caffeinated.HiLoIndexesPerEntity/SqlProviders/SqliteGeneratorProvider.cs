namespace NHibernate.Caffeinated.HiLoIndexesPerEntity.SqlProviders
{
    using FluentMigrator.Runner.Generators.Generic;
    using FluentMigrator.Runner.Generators.SQLite;
    using NHibernate.Dialect;

    /// <summary>
    ///     Provides an instance of <see cref="GenericGenerator" /> specific for NHibernate's <see cref="SQLiteDialect" />.
    /// </summary>
    public class SqliteGeneratorProvider : DbGeneratorProvider<SQLiteDialect>
    {
        /// <summary>
        /// </summary>
        public SqliteGeneratorProvider() : base(new SqliteGenerator())
        {
        }
    }
}