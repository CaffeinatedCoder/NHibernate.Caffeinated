namespace NHibernate.Caffeinated.HiLoIndexesPerEntity
{
    /// <summary>
    /// Provides an interface containing all informations needed to create and modify NHibernate's high-low index table.
    /// </summary>
    public interface IHiLoTableInfo
    {
        /// <summary>
        ///     Name of NHibernate's high-low index table.
        /// </summary>
        string TableName { get; }

        /// <summary>
        ///     Name of column to store entities' table names.
        /// </summary>
        string EntityColumnName { get; }

        /// <summary>
        ///     Name of index to create on entity column.
        /// </summary>
        string IndexOnEntityColumnName { get; }

        /// <summary>
        ///     Name of column to store the next high key values.
        /// </summary>
        string NextHighKeyColumnName { get; }

        /// <summary>
        ///     Name of schema for the table to reside in.
        /// </summary>
        string SchemaName { get; }
    }
}