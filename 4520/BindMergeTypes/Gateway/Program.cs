using Common;
using Gateway;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("service", client => {
    client.BaseAddress = new Uri("http://localhost:5284/graphql");
});

builder.Services.AddGraphQLServer()
    .AddRemoteSchema("service")
    .AddType<QueryExtension>()
    .AddType<MutationExtension>()
    .BindRuntimeType<IValidationError>()
    .BindRuntimeType<ValidationErrors>()
    .BindRuntimeType<StringLengthValidationError>();

var app = builder.Build();

app.UseRouting();

app.MapGraphQL();

app.Run();
