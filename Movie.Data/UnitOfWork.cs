using Movie.Core.Contracts;
using Movie.Core.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private readonly Lazy<IFilmRepository> _filmRepository;
        private readonly Lazy<IFilmGenreRepository> _filmGenreRepository;
        private readonly Lazy<IFilmDetailsRepository> _filmDetailsRepository;
        private readonly Lazy<IActorRepository> _actorRepository;
        private readonly Lazy<IFilmActorRepository> _filmActorRepository;
        private readonly Lazy<IReviewRepository> _reviewRepository;

        public IFilmRepository FilmRepository => _filmRepository.Value;
        public IActorRepository ActorRepository => _actorRepository.Value;
        public IFilmActorRepository FilmActorRepository => _filmActorRepository.Value;
        public IFilmDetailsRepository FilmDetailsRepository => _filmDetailsRepository.Value;
        public IReviewRepository ReviewRepository => _reviewRepository.Value;
        public IFilmGenreRepository FilmGenreRepository => _filmGenreRepository.Value;

        public UnitOfWork(
            AppDbContext context,
            Lazy<IFilmRepository> filmRepository,
            Lazy<IFilmGenreRepository> filmGenreRepository,
            Lazy<IFilmDetailsRepository> filmDetailsRepository,
            Lazy<IActorRepository> actorRepository,
            Lazy<IFilmActorRepository> filmActorRepository,
            Lazy<IReviewRepository> reviewRepository)
        {
            _context = context;

            _filmRepository = filmRepository;
            _filmGenreRepository = filmGenreRepository;
            _filmDetailsRepository = filmDetailsRepository;
            _actorRepository = actorRepository;
            _filmActorRepository = filmActorRepository;
            _reviewRepository = reviewRepository;
        }


        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
