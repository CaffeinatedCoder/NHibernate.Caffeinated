namespace NHibernate.Caffeinated.HiLoIndexesPerEntity.SqlProviders
{
    using FluentMigrator.Runner.Generators.Generic;
    using FluentMigrator.Runner.Generators.SqlServer;
    using NHibernate.Dialect;

    /// <summary>
    ///     Provides an instance of <see cref="GenericGenerator" /> specific for NHibernate's <see cref="MsSqlCeDialect" />.
    /// </summary>
    public class SqlServerCeGeneratorProvider : DbGeneratorProvider<MsSqlCeDialect>
    {
        /// <summary>
        /// </summary>
        public SqlServerCeGeneratorProvider() : base(new SqlServerCeGenerator())
        {
        }
    }
}