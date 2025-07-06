using PracticeApiCSharp07.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PracticeApiCSharp07.DTOs.Actors
{
    public class CreateActorDTO
    {
        [StringLength(256)]
        public required string Name { get; set; }

        [YearUntilNow(1850)]
        public int BirthYear { get; set; }
    }
}
