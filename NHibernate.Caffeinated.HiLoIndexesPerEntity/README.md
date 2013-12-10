### Introduction
A library that extends NHibernate's high-low key table to provide indexes per entity to be used together with NHibernate's own mapping-by-code functionalities.

At its core this library makes use of the fantastic open source framework **[FluentMigrator](https://github.com/schambers/fluentmigrator)** to generate dialect specific SQL statements. This limits compatibility of this library to those SQL dialects both of NHibernate and FluentMigrator are able to support. Currently those dialects are: **Firebird, SQLite, SQL Server 2000-2012, SQL Server CE, Oracle, MySQL and PostgreSQL**.

This library is also avalaible on **[NuGet](https://www.nuget.org/packages/NHibernate.Caffeinated.HiLoIndexesPerEntity/)**. To install this package, simply type

>**Install-Package NHibernate.Caffeinated.HiLoIndexesPerEntity**

in Visual Studio's package manager console and press enter.

### Usage
**1.** 
Provide an implementation for interface [IHiLoTableInfo](IHiLoTableInfo.cs). The implementing class must have a public, parameterless constructor. This type will be used in the following two steps, as it provides a container for all informations needed to create and modify NHibernate's high-low index table.

**2.**
Map your classes that should have an high-low index assigned by NHibernate by deriving from [Int64HiLoKeyedClassMapping&lt;TEntity, THiLoTableInfo&gt;](Int64HiLoKeyedClassMapping.cs).

**3.**
Create an instance deriving from [DbGeneratorProvider&lt;TDialect&gt;](DbGeneratorProvider.cs). The library already hosts providers for all supported SQL dialects. These can be found in namespace *[NHibernate.Caffeinated.HiLoIndexesPerEntity.SqlProviders](SqlProviders)*. 
On your ***NHibernate.Cfg.Configuration*** instance call extension function ***ExtendHiLoTableForEntitySpecificKeys***  (this is available by importing namespace *NHibernate.Caffeinated.HiLoIndexesPerEntity*) providing the aforementioned *DbGeneratorProvider* instance as parameter.

### License
This library is licensed under the terms and conditions of the [MIT Open Source License](http://opensource.org/licenses/MIT), so feel free to use it in any project you like, be it commercial or open source.

### Some sample code

- **TBD**
