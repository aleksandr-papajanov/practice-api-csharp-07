using Movie.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IFilmService> _filmService;
        private readonly Lazy<IActorService> _actorService;
        private readonly Lazy<IReviewService> _reviewService;

        public IFilmService FilmService => _filmService.Value;
        public IActorService ActorService => _actorService.Value;
        public IReviewService ReviewService => _reviewService.Value;


        public ServiceManager(
            Lazy<IFilmService> filmService,
            Lazy<IActorService> actorService,
            Lazy<IReviewService> reviewService)
        {
            _filmService = filmService;
            _actorService = actorService;
            _reviewService = reviewService;
        }
    }

}
