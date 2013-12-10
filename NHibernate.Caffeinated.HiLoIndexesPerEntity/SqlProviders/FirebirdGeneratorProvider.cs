namespace NHibernate.Caffeinated.HiLoIndexesPerEntity.SqlProviders
{
    using FluentMigrator.Runner.Generators.Firebird;
    using FluentMigrator.Runner.Generators.Generic;
    using FluentMigrator.Runner.Processors.Firebird;
    using NHibernate.Dialect;

    /// <summary>
    ///     Provides an instance of <see cref="GenericGenerator" /> specific for NHibernate's <see cref="FirebirdDialect" />.
    /// </summary>
    public class FirebirdGeneratorProvider : DbGeneratorProvider<FirebirdDialect>
    {
        /// <summary>
        /// </summary>
        public FirebirdGeneratorProvider() : base(new FirebirdGenerator(FirebirdOptions.StandardBehaviour()))
        {
        }
    }
}