using Microsoft.EntityFrameworkCore.Design;
using MySql.EntityFrameworkCore.Extensions;

//Used this to resolve following error when trying
//to connect database: Unable to resolve service for type
/// <summary>
/// 'Microsoft.EntityFrameworkCore.Storage.TypeMappingSourceDependencies'
/// while attempting to activate 'MySql.EntityFrameworkCore.Storage.Internal.MySQLTypeMappingSource'.
/// </summary>
public class MysqlEntityFrameworkDesignTimeServices : IDesignTimeServices
{
    public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddEntityFrameworkMySQL();
        new EntityFrameworkRelationalDesignServicesBuilder(serviceCollection)
            .TryAddCoreServices();
    }
}
