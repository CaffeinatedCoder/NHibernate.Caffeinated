namespace NHibernate.Caffeinated.HiLoIndexesPerEntity.SqlProviders
{
    using FluentMigrator.Runner.Generators.Generic;
    using FluentMigrator.Runner.Generators.SqlServer;
    using NHibernate.Dialect;

    /// <summary>
    ///     Provides an instance of <see cref="GenericGenerator" /> specific for NHibernate's <see cref="MsSql2012Dialect" />.
    /// </summary>
    public class SqlServer2012GeneratorProvider : DbGeneratorProvider<MsSql2012Dialect>
    {
        /// <summary>
        /// </summary>
        public SqlServer2012GeneratorProvider() : base(new SqlServer2012Generator())
        {
        }
    }
}