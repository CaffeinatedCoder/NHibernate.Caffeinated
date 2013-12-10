namespace NHibernate.Caffeinated.HiLoIndexesPerEntity
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using FluentMigrator.Expressions;
    using FluentMigrator.Model;
    using Iesi.Collections.Generic;
    using NHibernate.Dialect;
    using NHibernate.Mapping;

    internal class HiLoTableIndexPerEntityModifier<TDialect> where TDialect : Dialect
    {
        private readonly DbGeneratorProvider<TDialect> generatorProvider;
        private readonly IHiLoTableInfo tableInfo;

        public HiLoTableIndexPerEntityModifier(IHiLoTableInfo tableInfo, DbGeneratorProvider<TDialect> generatorProvider)
        {
            this.tableInfo = tableInfo;
            this.generatorProvider = generatorProvider;
        }

        private DeleteDataExpression DeleteDataFromHiLoTable
        {
            get
            {
                return new DeleteDataExpression
                       {
                           IsAllRows = true,
                           TableName = this.tableInfo.TableName,
                           SchemaName = this.tableInfo.SchemaName
                       };
            }
        }

        private CreateColumnExpression CreateColumnEntityName
        {
            get
            {
                return new CreateColumnExpression
                       {
                           SchemaName = this.tableInfo.SchemaName,
                           TableName = this.tableInfo.TableName,
                           Column = new ColumnDefinition
                                    {
                                        IsNullable = false,
                                        IsIndexed = true,
                                        TableName = this.tableInfo.TableName,
                                        Name = this.tableInfo.EntityColumnName,
                                        Type = DbType.String,
                                        DefaultValue = string.Empty,
                                        Size = 128,
                                    }
                       };
            }
        }

        private CreateIndexExpression CreateIndexOnEntityName
        {
            get
            {
                var index = new IndexDefinition
                            {
                                Name = this.tableInfo.IndexOnEntityColumnName,
                                TableName = this.tableInfo.TableName,
                                IsClustered = false,
                                SchemaName = this.tableInfo.SchemaName
                            };

                index.Columns.Add(new IndexColumnDefinition
                                  {
                                      Direction = Direction.Ascending,
                                      Name = this.tableInfo.EntityColumnName
                                  });

                return new CreateIndexExpression {Index = index};
            }
        }

        private InsertDataExpression GetInsertExpression(string entityName, Int64 highKey)
        {
            var insertExp = new InsertDataExpression
                            {
                                SchemaName = this.tableInfo.SchemaName,
                                TableName = this.tableInfo.TableName
                            };

            var insertData = new InsertionDataDefinition
                             {
                                 new KeyValuePair<string, object>(this.tableInfo.EntityColumnName, entityName),
                                 new KeyValuePair<string, object>(this.tableInfo.NextHighKeyColumnName, highKey)
                             };
            insertExp.Rows.Add(insertData);

            return insertExp;
        }

        public IAuxiliaryDatabaseObject AddEntityColumnToHiLoTable()
        {
            var createScript = new StringBuilder(4096);
            var sqlGenerator = this.generatorProvider.SqlGenerator;
            var dialectScopes = new List<string> {this.generatorProvider.DialectType.FullName};

            createScript.AppendLine(sqlGenerator.Generate(this.DeleteDataFromHiLoTable) + ";");
            createScript.AppendLine(sqlGenerator.Generate(this.CreateColumnEntityName) + ";");
            createScript.AppendLine(sqlGenerator.Generate(this.CreateIndexOnEntityName) + ";");

            var createSql = createScript.ToString();
            return new SimpleAuxiliaryDatabaseObject(createSql, null, new HashedSet<string>(dialectScopes));
        }

        public IAuxiliaryDatabaseObject InsertEntityNamesIntoHiLoTable(IEnumerable<PersistentClass> mappedClasses)
        {
            var script = new StringBuilder(4096);
            var sqlGenerator = this.generatorProvider.SqlGenerator;
            var dialectScopes = new List<string> {this.generatorProvider.DialectType.FullName};

            foreach (var classMapping in mappedClasses.Where(x => x.Identifier.IsSimpleValue))
            {
                var identifier = classMapping.Identifier as SimpleValue;
                if (identifier == null || identifier.IdentifierGeneratorStrategy != "hilo")
                {
                    continue;
                }

                var insertExpression = this.GetInsertExpression(classMapping.Table.Name, 1);
                script.AppendLine(sqlGenerator.Generate(insertExpression) + ";");
            }

            var sql = script.ToString();
            return new SimpleAuxiliaryDatabaseObject(sql, null, new HashedSet<string>(dialectScopes));
        }
    }
}