namespace NHibernate.Caffeinated.HiLoIndexesPerEntity.SqlProviders
{
    using FluentMigrator.Runner.Generators.Generic;
    using FluentMigrator.Runner.Generators.SqlServer;
    using NHibernate.Dialect;

    /// <summary>
    ///     Provides an instance of <see cref="GenericGenerator" /> specific for NHibernate's <see cref="MsSql2008Dialect" />.
    /// </summary>
    public class SqlServer2008GeneratorProvider : DbGeneratorProvider<MsSql2008Dialect>
    {
        /// <summary>
        /// </summary>
        public SqlServer2008GeneratorProvider() : base(new SqlServer2008Generator())
        {
        }
    }
}