namespace NHibernate.Caffeinated.HiLoIndexesPerEntity
{
    using System;
    using FluentMigrator.Runner.Generators.Generic;
    using NHibernate.Dialect;

    /// <summary>
    ///     Provides a mapping for a <see cref="Dialect" /> from NHibernate to an instance of <see cref="GenericGenerator" />
    ///     from FluentMigrator.
    /// </summary>
    /// <typeparam name="TDialect"></typeparam>
    public abstract class DbGeneratorProvider<TDialect> where TDialect : Dialect
    {
        private readonly GenericGenerator sqlGenerator;

        /// <summary>
        /// </summary>
        /// <param name="sqlGenerator"></param>
        protected DbGeneratorProvider(GenericGenerator sqlGenerator)
        {
            this.sqlGenerator = sqlGenerator;
        }

        /// <summary>
        ///     Gets the generator used to create SQL commands.
        /// </summary>
        public GenericGenerator SqlGenerator
        {
            get { return this.sqlGenerator; }
        }

        /// <summary>
        ///     Gets the type of NHibernate's <see cref="Dialect" />.
        /// </summary>
        public Type DialectType
        {
            get { return typeof (TDialect); }
        }
    }
}