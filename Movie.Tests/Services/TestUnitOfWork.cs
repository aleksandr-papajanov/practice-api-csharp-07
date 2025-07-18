using Movie.Core.Abstractions;
using Movie.Core.Abstractions.Repositories;
using Movie.Data;
using Movie.Data.Repositories;

namespace Movie.Tests.Services
{
    public class TestUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IFilmRepository? _filmRepository;
        private IFilmGenreRepository? _filmGenreRepository;
        private IFilmDetailsRepository? _filmDetailsRepository;
        private IActorRepository? _actorRepository;
        private IFilmActorRepository? _filmActorRepository;
        private IReviewRepository? _reviewRepository;

        public IActorRepository ActorRepository => _actorRepository ??= new ActorRepository(_context);
        public IFilmRepository FilmRepository => _filmRepository ??= new FilmRepository(_context);
        public IFilmGenreRepository FilmGenreRepository => _filmGenreRepository ??= new FilmGenreRepository(_context);
        public IFilmDetailsRepository FilmDetailsRepository => _filmDetailsRepository ??= new FilmDetailsRepository(_context);
        public IFilmActorRepository FilmActorRepository => _filmActorRepository ??= new FilmActorRepository(_context);
        public IReviewRepository ReviewRepository => _reviewRepository ??= new ReviewRepository(_context);


        public TestUnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}