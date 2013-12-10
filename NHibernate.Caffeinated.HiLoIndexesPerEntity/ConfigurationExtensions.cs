namespace NHibernate.Caffeinated.HiLoIndexesPerEntity
{
    using System;
    using NHibernate.Cfg;
    using NHibernate.Dialect;

    /// <summary>
    ///     Provides some extension functions to <see cref="Configuration" />
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Modifies NHibernate's high-low index table to maintain separate keys per entity type.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="generatorProvider"></param>
        /// <typeparam name="THiLoTableInfo"></typeparam>
        /// <typeparam name="TDialect"></typeparam>
        public static void ExtendHiLoTableForEntitySpecificKeys<THiLoTableInfo, TDialect>(
            this Configuration configuration,
            DbGeneratorProvider<TDialect> generatorProvider)
            where THiLoTableInfo : IHiLoTableInfo, new() where TDialect : Dialect
        {
            var hiLoTableInfo = Activator.CreateInstance<THiLoTableInfo>();
            var hiLoTableModifier = new HiLoTableIndexPerEntityModifier<TDialect>(hiLoTableInfo, generatorProvider);

            var alterTableScript = hiLoTableModifier.AddEntityColumnToHiLoTable();
            var insertEntitiesScript = hiLoTableModifier.InsertEntityNamesIntoHiLoTable(configuration.ClassMappings);

            configuration.AddAuxiliaryDatabaseObject(alterTableScript);
            configuration.AddAuxiliaryDatabaseObject(insertEntitiesScript);
        }
    }
}