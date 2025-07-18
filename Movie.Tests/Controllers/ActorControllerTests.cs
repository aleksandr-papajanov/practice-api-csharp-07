using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Movie.Contracts;
using Movie.Core.DTOs.Actors;
using Movie.Core.DTOs.Common;
using Movie.Core.Exceptions.NotFound;
using Movie.Presentation.Controllers;

namespace Movie.Tests.Controllers
{
    public class ActorControllerTests
    {
        private readonly Mock<IServiceManager> _serviceManagerMock;
        private readonly Mock<IActorService> _actorServiceMock;
        private readonly ActorController _controller;

        public ActorControllerTests()
        {
            _serviceManagerMock = new Mock<IServiceManager>();
            _actorServiceMock = new Mock<IActorService>();

            _serviceManagerMock.Setup(sm => sm.ActorService).Returns(_actorServiceMock.Object);

            _controller = new ActorController(_serviceManagerMock.Object);
        }

        [Fact]
        public async Task GetAllActors_ShouldReturnOkWithPaginatedActors()
        {
            // Arrange
            var actors = new List<ActorDTO>
            {
                new() { Id = 1, Name = "Actor 1", BirthYear = 1980 },
                new() { Id = 2, Name = "Actor 2", BirthYear = 1990 }
            };
            var paginatedResult = new PaginatedResult<ActorDTO>(
                items: actors,
                totalCount: actors.Count,
                pageSize: 10,
                currentPage: 1);

            _actorServiceMock.Setup(s => s.GetAllActorsAsync(It.IsAny<GetAllActorsDTO>()))
                .ReturnsAsync(paginatedResult);

            // Act
            var actionResult = await _controller.GetAll(new GetAllActorsDTO());

            // Assert
            actionResult.Result.Should().BeOfType<OkObjectResult>();
            var okResult = actionResult.Result as OkObjectResult;
            okResult!.Value.Should().BeEquivalentTo(paginatedResult);
        }

        [Fact]
        public async Task GetActor_ShouldReturnOkWithActor_WhenActorExists()
        {
            // Arrange
            var actorDto = new ActorDTO { Id = 1, Name = "Actor 1", BirthYear = 1980 };

            _actorServiceMock
                .Setup(s => s.GetActorAsync(It.IsAny<int>()))
                .ReturnsAsync(actorDto);

            // Act
            var actionResult = await _controller.Get(1);

            // Assert
            actionResult.Result.Should().BeOfType<OkObjectResult>();
            var okResult = actionResult.Result as OkObjectResult;
            okResult!.Value.Should().BeEquivalentTo(actorDto);
        }

        [Fact]
        public async Task GetActor_ShouldThrowActorNotFoundException_WhenActorDoesNotExist()
        {
            // Arrange
            _actorServiceMock
                .Setup(s => s.GetActorAsync(It.IsAny<int>()))
                .ThrowsAsync(new ActorNotFoundAppException(1));

            // Act
            Func<Task> act = async () => await _controller.Get(1);

            // Assert
            await act.Should().ThrowAsync<ActorNotFoundAppException>();
        }
    }
}
