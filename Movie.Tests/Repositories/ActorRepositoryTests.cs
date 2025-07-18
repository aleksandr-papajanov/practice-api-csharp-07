using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Movie.Core.Entities;
using Movie.Data;
using Movie.Data.Repositories;

namespace Movie.Tests.Repositories
{
    public class ActorRepositoryTests
    {
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;

        public ActorRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // создаём уникальную базу для каждого теста
                .Options;
        }

        [Fact]
        public async Task GetAsync_ShouldReturnActor_WhenActorExists()
        {
            using var context = new AppDbContext(_dbContextOptions);
            var repository = new ActorRepository(context);
            var actor = new Actor { Id = 1, Name = "Actor 1", BirthYear = 1980 };
            context.Actors.Add(actor);
            await context.SaveChangesAsync();

            var result = await repository.GetAsync(1);

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Name.Should().Be("Actor 1");
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenActorDoesNotExist()
        {
            using var context = new AppDbContext(_dbContextOptions);
            var repository = new ActorRepository(context);

            var result = await repository.GetAsync(1);

            result.Should().BeNull();
        }

        [Fact]
        public async Task AddAsync_ShouldAddNewActor()
        {
            using var context = new AppDbContext(_dbContextOptions);
            var repository = new ActorRepository(context);
            var actor = new Actor { Name = "New Actor", BirthYear = 1990 };

            await repository.Add(actor);
            await context.SaveChangesAsync();

            var savedActor = await context.Actors.FirstOrDefaultAsync(a => a.Name == "New Actor");
            savedActor.Should().NotBeNull();
            savedActor.BirthYear.Should().Be(1990);
        }

        [Fact]
        public async Task UpdateAsync_ShouldModifyExistingActor()
        {
            using var context = new AppDbContext(_dbContextOptions);
            var repository = new ActorRepository(context);
            var actor = new Actor { Id = 1, Name = "Old Name", BirthYear = 1980 };
            context.Actors.Add(actor);
            await context.SaveChangesAsync();

            actor.Name = "Updated Name";
            await repository.UpdateAsync(actor);
            await context.SaveChangesAsync();

            var updatedActor = await context.Actors.FindAsync(1);
            updatedActor.Name.Should().Be("Updated Name");
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveActor()
        {
            using var context = new AppDbContext(_dbContextOptions);
            var repository = new ActorRepository(context);
            var actor = new Actor { Id = 1, Name = "Actor to delete", BirthYear = 1980 };
            context.Actors.Add(actor);
            await context.SaveChangesAsync();

            await repository.DeleteAsync(actor);
            await context.SaveChangesAsync();

            var deletedActor = await context.Actors.FindAsync(1);
            deletedActor.Should().BeNull();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllActors()
        {
            using var context = new AppDbContext(_dbContextOptions);
            var repository = new ActorRepository(context);

            context.Actors.AddRange(
                new Actor { Name = "Actor 1", BirthYear = 1980 },
                new Actor { Name = "Actor 2", BirthYear = 1990 });

            await context.SaveChangesAsync();

            var allActors = await repository.All.ToListAsync();

            allActors.Should().HaveCount(2);
            allActors.Select(a => a.Name).Should().Contain([ "Actor 1", "Actor 2" ]);
        }
    }
}