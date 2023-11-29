using GraphQLDemo.Database;
using GraphQLDemo.GraphQL;
using GraphQLDemo.GraphQL.Commands;
using GraphQLDemo.GraphQL.Platforms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.TryAddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();

//Adding Postgres EF
builder.Services.AddEntityFrameworkNpgsql().AddPooledDbContextFactory<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("default"));
});

//Adding GQL server service
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddType<PlatformType>()
    .AddType<CommandType>()
    //For filtering and sorting
    .AddFiltering()
    .AddSorting();
//Removed AddProjection because now we are using Code first Approach (ObjectType)
//.AddProjections();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGraphQL();

app.MapControllers();

app.Run();

