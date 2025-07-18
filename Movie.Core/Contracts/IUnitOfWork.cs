using Movie.Core.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Contracts
{
    public interface IUnitOfWork
    {
        IFilmRepository FilmRepository { get; }
        IFilmGenreRepository FilmGenreRepository { get; }
        IFilmDetailsRepository FilmDetailsRepository { get; }
        IActorRepository ActorRepository { get; }
        IFilmActorRepository FilmActorRepository { get; }
        IReviewRepository ReviewRepository { get; }

        Task CompleteAsync();
    }
}
