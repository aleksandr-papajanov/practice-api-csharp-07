using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Contracts
{
    public interface IServiceManager
    {
        IFilmService FilmService { get; }
        IActorService ActorService { get; }
        IReviewService ReviewService { get; }
    }
}
