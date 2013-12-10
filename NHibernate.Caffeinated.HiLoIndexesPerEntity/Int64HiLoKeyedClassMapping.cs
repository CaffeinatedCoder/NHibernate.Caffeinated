namespace NHibernate.Caffeinated.HiLoIndexesPerEntity
{
    using System;
    using System.Linq.Expressions;
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;
    using NHibernate.Util;

    /// <summary>
    ///     Maps a type providing automatic per-entity high-low indexes.
    /// </summary>
    /// <typeparam name="TEntity">The type to map.</typeparam>
    /// <typeparam name="THiLoTableInfo">Implementation of <see cref="IHiLoTableInfo" />.</typeparam>
    public abstract class Int64HiLoKeyedClassMapping<TEntity, THiLoTableInfo> : ClassMapping<TEntity>
        where TEntity : class
        where THiLoTableInfo : IHiLoTableInfo, new()
    {
        private readonly IHiLoTableInfo hiLoTableInfo = Activator.CreateInstance<THiLoTableInfo>();
        private string tableName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idProperty">Expression for the property representing the identifier.</param>
        protected Int64HiLoKeyedClassMapping(Expression<Func<TEntity, Int64>> idProperty)
            : this(idProperty, ExpressionsHelper.DecodeMemberAccessExpression(idProperty).Name)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idProperty">Expression for the property representing the identifier.</param>
        /// <param name="idColumnName">Name for the identifier's column.</param>
        protected Int64HiLoKeyedClassMapping(Expression<Func<TEntity, Int64>> idProperty, string idColumnName)
        {
            this.Table(typeof (TEntity).Name);

            Id(idProperty,
               m =>
               {
                   m.Generator(Generators.HighLow,
                               g => g.Params(new
                                             {
                                                 table = this.hiLoTableInfo.TableName,
                                                 column = this.hiLoTableInfo.NextHighKeyColumnName,
                                                 max_lo = this.MaxLoValue,
                                                 where = string.Format("{0} = '{1}'",
                                                                       this.hiLoTableInfo.EntityColumnName,
                                                                       this.tableName)
                                             }));
                   m.Column(idColumnName);
               });
        }

        /// <summary>
        ///     Defaults to 50. Override this to provide your own default.
        /// </summary>
        protected virtual Int64 MaxLoValue
        {
            get { return 50; }
        }

        /// <summary>
        /// Sets the type's table name.
        /// </summary>
        /// <param name="name"></param>
        public new void Table(string name)
        {
            this.tableName = name;
            base.Table(this.tableName);
        }
    }
}