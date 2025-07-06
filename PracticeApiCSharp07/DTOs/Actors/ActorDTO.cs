namespace PracticeApiCSharp07.DTOs.Actors
{
    /// <summary>
    /// Represents actor data returned to the client.
    /// </summary>
    public class ActorDTO
    {
        /// <summary>
        /// The unique identifier of the actor.
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// The full name of the actor.
        /// </summary>
        /// <example>John Doe</example>
        public required string Name { get; set; }

        /// <summary>
        /// The year the actor was born.
        /// </summary>
        /// <example>1980</example>
        public int BirthYear { get; set; }

        /// <summary>
        /// A list of movie titles the actor has appeared in.
        /// </summary>
        /// <example>["Inception", "The Dark Knight", "Interstellar"]</example>
        public IEnumerable<string> Movies { get; set; } = [];
    }

}
