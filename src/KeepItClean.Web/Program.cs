using KeepItClean.Server.Infrastructure;
using KeepItClean.Web;
using KeepItClean.Web.Client.Components;
using KeepItClean.Web.Features;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    var connectionString = await DynamoDbContainerFactory.CreateAsync();

    builder.Configuration.AddInMemoryCollection(new List<KeyValuePair<string, string?>>
    {
        new("AWS:ServiceUrl", connectionString),
        new("AWS:AccessKeyId", "TestUser"),
        new("AWS:SecretAccessKey", "TestSecret")
    });
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(StubComponent).Assembly);

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    var initializeDatabaseService = scope.ServiceProvider.GetRequiredService<InitializeDatabaseService>();

    await initializeDatabaseService.InitializeAsync();
}

app.Run();

