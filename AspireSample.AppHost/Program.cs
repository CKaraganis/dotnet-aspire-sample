namespace AspireSample.AppHost;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = DistributedApplication.CreateBuilder(args);

        var cache = builder.AddRedis("cache");

        var sqlPassword = builder.AddParameter("LocalSqlServer-password", true);
        var sqlPort = builder.AddParameter("LocalSqlServer-port", true).Resource.Value;
        
        var sqlServer = builder.AddSqlServer("LocalSqlServer", sqlPassword, int.Parse(sqlPort))
            .WithDataVolume("AspireSampleDb.Volume")
            .AddDatabase("AspireSampleDb");

        var apiService = builder.AddProject<Projects.AspireSample_ApiService>("webapi-service")
            .WithReference(sqlServer);

        builder.AddProject<Projects.AspireSample_Web>("web-frontend")
            .WithExternalHttpEndpoints()
            .WithReference(cache)
            .WithReference(apiService);

        builder.Build().Run();
    }
}