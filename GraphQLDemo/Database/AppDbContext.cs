using System;
using Microsoft.EntityFrameworkCore;
using GraphQLDemo.Models;

namespace GraphQLDemo.Database
{
	public class AppDbContext: DbContext
	{
		public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
		{
		}

		public DbSet<Platform> Platforms { get; set; }
		public DbSet<Command> Commands { get; set; }

		//Using is for custom defining relationship b/w tables
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			//From perspective of platform
			modelBuilder.Entity<Platform>()
				.HasMany(item => item.Commands)
				.WithOne(item => item.Platform!)
				.HasForeignKey(item => item.PlatformId);

			//From perspective of command
			modelBuilder.Entity<Command>()
				.HasOne(item => item.Platform)
				.WithMany(item => item.Commands)
				.HasForeignKey(item => item.PlatformId);
        }
    }
}

