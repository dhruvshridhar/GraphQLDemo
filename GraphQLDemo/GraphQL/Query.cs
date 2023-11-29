using GraphQLDemo.Database;
using GraphQLDemo.Models;
using HotChocolate.Data;

namespace GraphQLDemo.GraphQL
{
    // Service attribute comes from HotChocolate
    // Dotnet default DI engine only supports constructor DI, but here Service attr allows method DI
    public class Query
	{
		[UseDbContext(typeof(AppDbContext))]
		//Need to add for showing child entities
		//Removed useProjection because now we are using Code first Approach (ObjectType)
		//For filtering and sorting we add these annotations
		//[UseProjection]
		[UseFiltering]
		[UseSorting]
		public IQueryable<Platform> GetPlatforms([ScopedService] AppDbContext appDbContext)
		{
			return appDbContext.Platforms;
		}

        [UseDbContext(typeof(AppDbContext))]
        //[UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Command> GetCommands([ScopedService] AppDbContext appContext)
		{
			return appContext.Commands;
		}
	}
}

