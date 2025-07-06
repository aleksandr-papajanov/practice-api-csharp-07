using PracticeApiCSharp07.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PracticeApiCSharp07.DTOs.Actors
{
    /// <summary>
    /// Represents the data used to update an existing actor.
    /// Only non-null properties will be updated.
    /// </summary>
    public class UpdateActorDTO
    {
        /// <summary>
        /// The new name of the actor. Optional. Max length is 256 characters.
        /// </summary>
        /// <example>John Doe</example>
        [StringLength(256)]
        public string? Name { get; set; }

        /// <summary>
        /// The new birth year of the actor. Optional. Must be between 1850 and the current year if provided.
        /// </summary>
        /// <example>1985</example>
        [YearUntilNow(1850)]
        public int? BirthYear { get; set; }
    }

}
