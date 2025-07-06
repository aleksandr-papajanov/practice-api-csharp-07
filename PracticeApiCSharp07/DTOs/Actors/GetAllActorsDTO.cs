using System.ComponentModel.DataAnnotations;

namespace PracticeApiCSharp07.DTOs.Actors
{
    public class GetAllActorsDTO
    {
        [Range(0, int.MaxValue)]
        public int Skip { get; set; } = 0;

        [Range(1, 100)]
        public int Take { get; set; } = 50;
    }
}
