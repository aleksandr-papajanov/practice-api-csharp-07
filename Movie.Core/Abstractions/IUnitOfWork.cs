using Movie.Core.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Abstractions
{
    public interface IUnitOfWork
    {
        IFilmRepository FilmRepository { get; }
        IActorRepository ActorRepository { get; }
        IFilmActorRepository FilmActorRepository { get; }
        IFilmDetailsRepository FilmDetailsRepository { get; }
        IReviewRepository ReviewRepository { get; }

        Task CompleteAsync();
    }
}
