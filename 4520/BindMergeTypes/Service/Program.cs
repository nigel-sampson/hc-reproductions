using Common;
using Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddType<StringLengthValidationError>();

var app = builder.Build();

app.UseRouting();

app.MapGraphQL();

app.Run();
