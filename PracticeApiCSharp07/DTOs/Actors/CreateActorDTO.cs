using PracticeApiCSharp07.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PracticeApiCSharp07.DTOs.Actors
{
    /// <summary>
    /// Represents the data required to create a new actor.
    /// </summary>
    public class CreateActorDTO
    {
        /// <summary>
        /// The full name of the actor.
        /// </summary>
        /// <example>John Doe</example>
        [StringLength(256)]
        public required string Name { get; set; }

        /// <summary>
        /// The year the actor was born. Must be between 1850 and the current year.
        /// </summary>
        /// <example>1985</example>
        [YearUntilNow(1850)]
        public int BirthYear { get; set; }
    }

}
