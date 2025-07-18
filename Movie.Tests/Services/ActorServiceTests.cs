using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Movie.Core.DTOs.Actors;
using Movie.Core.Entities;
using Movie.Core.Exceptions;
using Movie.Data;
using Movie.Services;
using Movie.Tests.Services;

namespace Movie.Tests.Services
{
    public class ActorServiceTests
    {
        private readonly AppDbContext _context;
        private readonly ActorService _actorService;
        private readonly TestUnitOfWork _unitOfWork;

        public ActorServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // уникальная база для каждого теста
                .Options;

            _context = new AppDbContext(options);
            _unitOfWork = new TestUnitOfWork(_context);
            _actorService = new ActorService(_unitOfWork);
        }

        [Fact]
        public async Task GetAllActorsAsync_ShouldReturnPaginatedActors()
        {
            // Arrange
            for (int i = 1; i <= 15; i++)
            {
                _context.Actors.Add(new Actor { Name = $"Actor {i}", BirthYear = 1980 + i });
            }
            await _context.SaveChangesAsync();

            var request = new GetAllActorsDTO { PageNumber = 2, PageSize = 5 };

            // Act
            var result = await _actorService.GetAllActorsAsync(request);

            // Assert
            result.Items.Should().HaveCount(5);
            result.TotalCount.Should().Be(15);
            result.CurrentPage.Should().Be(2);
            result.PageSize.Should().Be(5);
            result.Items.First().Name.Should().Be("Actor 6");
        }

        [Fact]
        public async Task GetActorAsync_ShouldReturnActor_WhenExists()
        {
            // Arrange
            var actor = new Actor { Name = "John Doe", BirthYear = 1990 };
            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();

            // Act
            var dto = await _actorService.GetActorAsync(actor.Id);

            // Assert
            dto.Should().NotBeNull();
            dto.Name.Should().Be("John Doe");
        }

        [Fact]
        public async Task GetActorAsync_ShouldThrow_WhenNotFound()
        {
            // Act
            Func<Task> act = async () => await _actorService.GetActorAsync(999);

            // Assert
            await act.Should().ThrowAsync<ActorNotFoundAppException>();
        }

        [Fact]
        public async Task CreateActorAsync_ShouldAddActor_WhenNameUnique()
        {
            // Arrange
            var request = new CreateActorDTO { Name = "Unique Actor", BirthYear = 1985 };

            // Act
            var result = await _actorService.CreateActorAsync(request);

            // Assert
            result.Name.Should().Be("Unique Actor");
            (await _context.Actors.CountAsync()).Should().Be(1);
        }

        [Fact]
        public async Task CreateActorAsync_ShouldThrow_WhenNameConflict()
        {
            // Arrange
            _context.Actors.Add(new Actor { Name = "Duplicate", BirthYear = 1980 });
            await _context.SaveChangesAsync();

            var request = new CreateActorDTO { Name = "Duplicate", BirthYear = 1985 };

            // Act
            Func<Task> act = async () => await _actorService.CreateActorAsync(request);

            // Assert
            await act.Should().ThrowAsync<ActorNameConflictAppException>();
        }

        [Fact]
        public async Task UpdateActorAsync_ShouldUpdateProperties_WhenActorExists()
        {
            // Arrange
            var actor = new Actor { Name = "Old Name", BirthYear = 1980 };
            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();

            var updateDto = new UpdateActorDTO { Name = "New Name", BirthYear = 1990 };

            // Act
            await _actorService.UpdateActorAsync(actor.Id, updateDto);
            var updated = await _context.Actors.FindAsync(actor.Id);

            // Assert
            updated!.Name.Should().Be("New Name");
            updated.BirthYear.Should().Be(1990);
        }

        [Fact]
        public async Task UpdateActorAsync_ShouldThrow_WhenActorNotFound()
        {
            // Arrange
            var updateDto = new UpdateActorDTO { Name = "Any", BirthYear = 1990 };

            // Act
            Func<Task> act = async () => await _actorService.UpdateActorAsync(999, updateDto);

            // Assert
            await act.Should().ThrowAsync<ActorNotFoundAppException>();
        }

        [Fact]
        public async Task DeleteActorAsync_ShouldRemoveActor_WhenExists()
        {
            // Arrange
            var actor = new Actor { Name = "ToDelete", BirthYear = 1980 };
            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();

            // Act
            await _actorService.DeleteActorAsync(actor.Id);

            // Assert
            var exists = await _context.Actors.AnyAsync(a => a.Id == actor.Id);
            exists.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteActorAsync_ShouldThrow_WhenActorNotFound()
        {
            // Act
            Func<Task> act = async () => await _actorService.DeleteActorAsync(999);

            // Assert
            await act.Should().ThrowAsync<ActorNotFoundAppException>();
        }
    }
}