namespace NHibernate.Caffeinated.HiLoIndexesPerEntity.SqlProviders
{
    using FluentMigrator.Runner.Generators.Generic;
    using FluentMigrator.Runner.Generators.MySql;
    using NHibernate.Dialect;

    /// <summary>
    ///     Provides an instance of <see cref="GenericGenerator" /> specific for NHibernate's <see cref="MySQLDialect" />.
    /// </summary>
    public class MysqlGeneratorProvider : DbGeneratorProvider<MySQLDialect>
    {
        /// <summary>
        /// </summary>
        public MysqlGeneratorProvider() : base(new MySqlGenerator())
        {
        }
    }
}