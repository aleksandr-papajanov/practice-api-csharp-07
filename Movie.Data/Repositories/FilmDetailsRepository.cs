using Movie.Core.Abstractions.Repositories;
using Movie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Data.Repositories
{
    public class FilmDetailsRepository : Repository<FilmDetails>, IFilmDetailsRepository
    {
        public FilmDetailsRepository(AppDbContext context) : base(context)
        {
        }
    }
}
