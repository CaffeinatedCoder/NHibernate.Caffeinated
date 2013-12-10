### Introduction
A library that extends NHibernate's high-low key table to provide indexes per entity to be used together with NHibernate's own mapping-by-code functionalities.

At its core this library makes use of the fantastic open source framework **[FluentMigrator](https://github.com/schambers/fluentmigrator)** to generate dialect specific SQL statements. This limits compatibility of this library to those SQL dialects both of NHibernate and FluentMigrator are able to support. Currently those dialects are: **Firebird, SQLite, SQL Server 2000-2012, SQL Server CE, Oracle, MySQL and PostgreSQL**.

This library is also avalaible on **[NuGet](https://www.nuget.org/packages/NHibernate.Caffeinated.HiLoIndexesPerEntity/)**. To install this package, simply type

>**Install-Package NHibernate.Caffeinated.HiLoIndexesPerEntity**

in Visual Studio's package manager console and press enter.

### Usage
**1.** 
Provide an implementation for interface [IHiLoTableInfo](IHiLoTableInfo.cs). The implementing class must have a public, parameterless constructor. This type will be used in the following two steps, as it provides a container for all informations needed to create and modify NHibernate's high-low index table:

```csharp
using NHibernate.Caffeinated.HiLoIndexesPerEntity;

public class HiLoTableInfo : IHiLoTableInfo
{
  public string TableName { get { return "NH_Entities_NextHighKeys"; } }
  public string EntityColumnName { get { return "EntityName"; } }
  public string IndexOnEntityColumnName { get { return "IX_NH_Entities_NextHighKeys_Entity"; } }
  public string NextHighKeyColumnName { get { return "NextHighValue"; } }
  public string SchemaName { get { return "dbo"; } }
}
```

**2.**
Map your classes that should have an high-low index assigned by NHibernate by deriving from [Int64HiLoKeyedClassMapping&lt;TEntity, THiLoTableInfo&gt;](Int64HiLoKeyedClassMapping.cs):

```csharp
public class SampleEntity 
{
  public virtual Int64 Id { get; protected set; }
  ...
}
```
```csharp
using NHibernate.Caffeinated.HiLoIndexesPerEntity;
using NHibernate.Mapping.Code;

public class SampleEntityMapping : Int64HiLoKeyedClassMapping<SampleEntity, HiLoTableInfo>
{
  public SampleEntityMapping() : base(x => x.Id)
  {
    Table("SampleEntities");
  }
}
```

**3.**
Create an instance deriving from [DbGeneratorProvider&lt;TDialect&gt;](DbGeneratorProvider.cs). The library already hosts providers for all supported SQL dialects. These can be found in namespace *[NHibernate.Caffeinated.HiLoIndexesPerEntity.SqlProviders](SqlProviders)*. 
On your ***NHibernate.Cfg.Configuration*** instance call extension function ***ExtendHiLoTableForEntitySpecificKeys***  (this is available by importing namespace *NHibernate.Caffeinated.HiLoIndexesPerEntity*) providing the aforementioned *DbGeneratorProvider* instance as parameter after you added the mappings to your configuration:

```csharp
using NHibernate;
using NHibernate.Caffeinated.HiLoIndexesPerEntity;
using NHibernate.Caffeinated.HiLoIndexesPerEntity.SqlProviders;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.Code;

...

public ISessionFactory CreateSessionFactory
{
  var cfg = new Configuration();
  cfg.DatabaseIntegration(db => {
                                  db.ConnectionString = "Data Source=test.db";
                                  db.Dialect<SQLiteDialect>();
                                  db.Driver<SQLite20Driver>();
                                  db.SchemaAction = SchemaAutoAction.Create;
                                });
  
  var mapper = new ModelMapper();
  mapper.AddMapping<SampleEntityMapping>();
  ...
  
  var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
  cfg.AddMapping(mapping);
  cfg.ExtendHiLoTableForEntitySpecificKeys<HiLoTableInfo, SQLiteDialect>(new SqliteGeneratorProvider());
  
  return cfg.BuildSessionFactory();
}
```

### License
This library is licensed under the terms and conditions of the [MIT Open Source License](http://opensource.org/licenses/MIT), so feel free to use it in any project you like, be it commercial or open source.
