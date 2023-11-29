using System;
using GraphQLDemo.Database;
using GraphQLDemo.GraphQL.Commands;
using GraphQLDemo.GraphQL.Platforms;
using GraphQLDemo.Models;

namespace GraphQLDemo.GraphQL
{
	public class Mutation
	{
		[UseDbContext(typeof(AppDbContext))]
		//AddPlatformInput, AddPlatfromPayload are record types
		public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input, [ScopedService]AppDbContext appDbContext)
		{
			var platform = new Platform
			{
				Name = input.Name
			};

			await appDbContext.Platforms.AddAsync(platform);
			await appDbContext.SaveChangesAsync();

			return new AddPlatformPayload(platform);
		}

		[UseDbContext(typeof(AppDbContext))]
		public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input, [ScopedService] AppDbContext appDbContext)
		{
			var command = new Command
			{
				CommandLine = input.CommandLine,
				Howto = input.HowTo,
				PlatformId = input.PlatformId
			};

			await appDbContext.Commands.AddAsync(command);
			await appDbContext.SaveChangesAsync();
			return new AddCommandPayload(command);
		}
	}
}

