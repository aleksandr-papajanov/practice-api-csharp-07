using PracticeApiCSharp07.Entities;

namespace PracticeApiCSharp07.Helpers
{
    internal static class RandomMovieDataGenerator
    {
        private static readonly Random _random = new();

        private static readonly string[] MovieTitleAdjectives = { "Dark", "Silent", "Broken", "Hidden", "Last", "First", "Red", "Golden", "Eternal", "Secret" };
        private static readonly string[] MovieTitleNouns = { "Empire", "Dream", "Memory", "Warrior", "Forest", "Shadow", "Game", "Promise", "Ocean", "Fire" };
        private static readonly string[] MovieTitleModifiers = { "of Time", "Returns", "Awakens", "Rising", "Legacy", "Chronicles", "Reborn", "Fury", "Beyond", "Revenge" };
        private static readonly string[] MovieGenres = { "Action", "Comedy", "Drama", "Horror", "Sci-Fi", "Romance", "Thriller", "Fantasy", "Animation", "Mystery", "Documentary", "Adventure", "Crime", "Musical", "Western" };
        private static readonly string[] Languages = { "English", "Spanish", "French", "German", "Japanese", "Swedish", "Italian", "Chinese", "Korean", "Hindi" };
        private static readonly string[] FirstNames = { "John", "Emma", "Liam", "Olivia", "Noah", "Ava", "James", "Sophia", "Lucas", "Mia" };
        private static readonly string[] LastNames = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Martinez", "Taylor" };
        private static readonly string[] ReviewOpenings = { "Absolutely loved it.", "Not what I expected.", "A pleasant surprise.", "Could have been better.", "An instant classic.", "Left me speechless." };
        private static readonly string[] ReviewMiddles = { "The acting was top-notch.", "The story kept me engaged.", "Visuals were stunning.", "The pacing felt off at times.", "Music fit the scenes perfectly.", "Dialogues were a bit weak." };
        private static readonly string[] ReviewEndings = { "Would definitely recommend.", "Might watch it again.", "Wouldn't watch it twice.", "Totally worth it.", "It was just okay.", "Exceeded my expectations." };
        private static readonly string[] SynopsisOpenings = { "In a world torn by conflict,", "Amidst rising tension,", "When everything falls apart,", "In a small forgotten town,", "During a time of uncertainty," };
        private static readonly string[] SynopsisMiddles = { "a lone hero rises.", "two strangers meet by chance.", "a secret threatens to change everything.", "an ancient power is awakened.", "a mission turns deadly." };
        private static readonly string[] SynopsisEndings = { "Fate has other plans.", "Their journey has just begun.", "Nothing will ever be the same.", "Hope is all that remains.", "The clock is ticking." };

        public static string MovieTitle
        {
            get
            {
                var pattern = _random.Next(0, 3);
                return pattern switch
                {
                    0 => string.Join(" ", Pick(MovieTitleAdjectives), Pick(MovieTitleNouns)),
                    1 => string.Join(" ", Pick(MovieTitleNouns), Pick(MovieTitleModifiers)),
                    _ => string.Join(" ", Pick(MovieTitleAdjectives), Pick(MovieTitleNouns), Pick(MovieTitleModifiers))
                };
            }
        }

        public static string FullName => string.Join(" ", Pick(FirstNames), Pick(LastNames));
        public static string Review => string.Join(" ", Pick(ReviewOpenings), Pick(ReviewMiddles), Pick(ReviewEndings));
        public static string Synopsis => string.Join(" ", Pick(SynopsisOpenings), Pick(SynopsisMiddles), Pick(SynopsisEndings));
        public static string Genre => Pick(MovieGenres);
        public static string Language => Pick(Languages);
        public static int Year => _random.Next(1888, DateTime.UtcNow.Year + 1);
        public static int Duration => _random.Next(60, 241);
        public static decimal Budget => Math.Round((decimal)_random.NextDouble() * (1000_000_000 - 1) + 1, 2);
        public static int ActorCount => _random.Next(1, 11);
        public static int ReviewCount => _random.Next(1, 11);
        public static int Rating => _random.Next(1, 6);

        private static string Pick(string[] array) => array[_random.Next(array.Length)];
    }
}
