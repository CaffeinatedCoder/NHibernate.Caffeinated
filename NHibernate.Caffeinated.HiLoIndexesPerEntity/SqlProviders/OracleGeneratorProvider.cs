namespace NHibernate.Caffeinated.HiLoIndexesPerEntity.SqlProviders
{
    using FluentMigrator.Runner.Generators.Generic;
    using FluentMigrator.Runner.Generators.Oracle;
    using NHibernate.Dialect;

    /// <summary>
    ///     Provides an instance of <see cref="GenericGenerator" /> specific for NHibernate's <see cref="Oracle8iDialect" />.
    /// </summary>
    public class OracleGeneratorProvider : DbGeneratorProvider<Oracle8iDialect>
    {
        /// <summary>
        /// </summary>
        public OracleGeneratorProvider() : base(new OracleGenerator())
        {
        }
    }
}