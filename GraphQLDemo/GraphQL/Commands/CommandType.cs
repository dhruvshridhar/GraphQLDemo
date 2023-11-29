using System;
using GraphQLDemo.Database;
using GraphQLDemo.Models;

namespace GraphQLDemo.GraphQL.Commands
{
	public class CommandType : ObjectType<Command>
	{
        protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
        {
            descriptor.Description("Represents any executable command");
            descriptor.Field(f => f.Platform)
                .ResolveWith<Resolvers>(r => r.GetPlatform(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("This is platform to which command belongs");
        }

        private class Resolvers
        {
            public Platform GetPlatform([Parent]Command command, [ScopedService] AppDbContext appDbContext)
            {
                return appDbContext.Platforms.FirstOrDefault(item => item.Id == command.PlatformId);
            }
        }
    }
}

