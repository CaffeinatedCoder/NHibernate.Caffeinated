namespace NHibernate.Caffeinated.HiLoIndexesPerEntity.SqlProviders
{
    using FluentMigrator.Runner.Generators.Generic;
    using FluentMigrator.Runner.Generators.SqlServer;
    using NHibernate.Dialect;

    /// <summary>
    ///     Provides an instance of <see cref="GenericGenerator" /> specific for NHibernate's <see cref="MsSql2000Dialect" />.
    /// </summary>
    public class SqlServer2000GeneratorProvider : DbGeneratorProvider<MsSql2000Dialect>
    {
        /// <summary>
        /// </summary>
        public SqlServer2000GeneratorProvider() : base(new SqlServer2000Generator())
        {
        }
    }
}