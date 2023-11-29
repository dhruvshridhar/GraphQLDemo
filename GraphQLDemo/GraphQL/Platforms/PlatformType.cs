using System;
using GraphQLDemo.Database;
using GraphQLDemo.Models;

namespace GraphQLDemo.GraphQL.Platforms
{
	//ObjectType class is used to build an object upon specified model(used for code first approach).
	//Here we want to make our models clean and move GQL related stuff here
	//Read Hotchocolate docs
	public class PlatformType : ObjectType<Platform>
	{
        //We use Configure method from ObjectType to configure our model
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
            //This is class level
            descriptor.Description("Represents any software or service that has CLI");

            //For not including this field in GQL schema
            descriptor.Field(f => f.LicenseKey).Description("License field").Ignore();

            //Adding bit of business logic
            descriptor.Field(f => f.Commands)
                .ResolveWith<Resolvers>(p => p.GetCommands(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("This is the list of availalble commands for this platform");
        }

        private class Resolvers
        {
            //Need to add this parent annotation here. It wasnt there in tutorial
            //Need to use ScopedService annotation only though its deprecated
            public IQueryable<Command> GetCommands([Parent]Platform platform, [ScopedService] AppDbContext appDbContext)
            {
                return appDbContext.Commands.Where(item => item.PlatformId == platform.Id);
            }
        }
    }
}

