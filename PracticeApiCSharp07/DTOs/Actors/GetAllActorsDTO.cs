using System.ComponentModel.DataAnnotations;

namespace PracticeApiCSharp07.DTOs.Actors
{
    /// <summary>
    /// Represents pagination parameters for retrieving a list of actors.
    /// </summary>
    public class GetAllActorsDTO
    {
        /// <summary>
        /// The number of actors to skip. Must be zero or greater.
        /// </summary>
        [Range(0, int.MaxValue)]
        public int Skip { get; set; } = 0;

        /// <summary>
        /// The number of actors to take. Must be between 1 and 100.
        /// </summary>
        [Range(1, 100)]
        public int Take { get; set; } = 50;
    }

}
