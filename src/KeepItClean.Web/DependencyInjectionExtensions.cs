using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using KeepItClean.Server.Infrastructure;

namespace KeepItClean.Web;

public static class DependencyInjectionExtensions
{
    public static void AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorComponents()
            .AddInteractiveWebAssemblyComponents();

        // TODO: DynamoDB health checks.
        // TODO: Auth.
        // TODO: automapper.
        // TODO: test.

        builder.Services.AddDefaultAWSOptions(provider =>
        {
            var options = builder.Configuration.GetAWSOptions();
            options.Credentials = new BasicAWSCredentials(
                builder.Configuration.GetValue<string>("AWS:AccessKeyId"),
                builder.Configuration.GetValue<string>("AWS:SecretAccessKey"));

            return options;
        });
        builder.Services.AddAWSService<IAmazonDynamoDB>();
        builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
        builder.Services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddSingleton<InitializeDatabaseService>();
        builder.Services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining<Program>());
    }
}
