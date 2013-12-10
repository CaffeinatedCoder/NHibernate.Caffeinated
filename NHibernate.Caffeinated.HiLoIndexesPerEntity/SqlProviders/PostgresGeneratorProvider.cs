namespace NHibernate.Caffeinated.HiLoIndexesPerEntity.SqlProviders
{
    using FluentMigrator.Runner.Generators.Generic;
    using FluentMigrator.Runner.Generators.Postgres;
    using NHibernate.Dialect;

    /// <summary>
    ///     Provides an instance of <see cref="GenericGenerator" /> specific for NHibernate's <see cref="PostgreSQLDialect" />.
    /// </summary>
    public class PostgresGeneratorProvider : DbGeneratorProvider<PostgreSQLDialect>
    {
        /// <summary>
        /// </summary>
        public PostgresGeneratorProvider() : base(new PostgresGenerator())
        {
        }
    }
}