namespace NHibernate.Caffeinated.HiLoIndexesPerEntity.SqlProviders
{
    using FluentMigrator.Runner.Generators.Generic;
    using FluentMigrator.Runner.Generators.SqlServer;
    using NHibernate.Dialect;

    /// <summary>
    ///     Provides an instance of <see cref="GenericGenerator" /> specific for NHibernate's <see cref="MsSql2005Dialect" />.
    /// </summary>
    public class SqlServer2005GeneratorProvider : DbGeneratorProvider<MsSql2005Dialect>
    {
        /// <summary>
        /// </summary>
        public SqlServer2005GeneratorProvider() : base(new SqlServer2005Generator())
        {
        }
    }
}