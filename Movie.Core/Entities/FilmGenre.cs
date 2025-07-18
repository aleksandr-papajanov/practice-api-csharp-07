#pragma warning disable CS1591

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Entities
{
    public class FilmGenre : EntityBase
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<Film> Films { get; set; } = [];
    }
}
