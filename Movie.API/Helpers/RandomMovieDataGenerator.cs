using Microsoft.EntityFrameworkCore;
using System;

namespace Movie.API.Helpers
{
    internal static class RandomMovieDataGenerator
    {
        private const int MaxRetries = 20;

        private static readonly Random _random = new();

        private static readonly string[] FilmTitleAdjectives = { "Dark", "Silent", "Broken", "Hidden", "Last", "First", "Red", "Golden", "Eternal", "Secret" };
        private static readonly string[] FilmTitleNouns = { "Empire", "Dream", "Memory", "Warrior", "Forest", "Shadow", "Game", "Promise", "Ocean", "Fire" };
        private static readonly string[] FilmTitleModifiers = { "of Time", "Returns", "Awakens", "Rising", "Legacy", "Chronicles", "Reborn", "Fury", "Beyond", "Revenge" };
        private static readonly string[] FilmGenres = { "Action", "Comedy", "Drama", "Horror", "Sci-Fi", "Romance", "Thriller", "Fantasy", "Animation", "Mystery", "Documentary", "Adventure", "Crime", "Musical", "Western" };
        private static readonly string[] Languages = { "English", "Spanish", "French", "German", "Japanese", "Swedish", "Italian", "Chinese", "Korean", "Hindi" };
        private static readonly string[] ReviewOpenings = { "Absolutely loved it.", "Not what I expected.", "A pleasant surprise.", "Could have been better.", "An instant classic.", "Left me speechless." };
        private static readonly string[] ReviewMiddles = { "The acting was top-notch.", "The story kept me engaged.", "Visuals were stunning.", "The pacing felt off at times.", "Music fit the scenes perfectly.", "Dialogues were a bit weak." };
        private static readonly string[] ReviewEndings = { "Would definitely recommend.", "Might watch it again.", "Wouldn't watch it twice.", "Totally worth it.", "It was just okay.", "Exceeded my expectations." };
        private static readonly string[] SynopsisOpenings = { "In a world torn by conflict,", "Amidst rising tension,", "When everything falls apart,", "In a small forgotten town,", "During a time of uncertainty," };
        private static readonly string[] SynopsisMiddles = { "a lone hero rises.", "two strangers meet by chance.", "a secret threatens to change everything.", "an ancient power is awakened.", "a mission turns deadly." };
        private static readonly string[] SynopsisEndings = { "Fate has other plans.", "Their journey has just begun.", "Nothing will ever be the same.", "Hope is all that remains.", "The clock is ticking." };

        private static readonly List<string> _usedTitles = [];

        public static string FilmTitle
        {
            get
            {
                var title = string.Empty;
                var retries = 0;

                do
                {
                    var pattern = _random.Next(0, 3);
                    title = pattern switch
                    {
                        0 => string.Join(" ", Pick(FilmTitleAdjectives), Pick(FilmTitleNouns)),
                        1 => string.Join(" ", Pick(FilmTitleNouns), Pick(FilmTitleModifiers)),
                        _ => string.Join(" ", Pick(FilmTitleAdjectives), Pick(FilmTitleNouns), Pick(FilmTitleModifiers))
                    };
                } while (retries++ < MaxRetries && _usedTitles.Contains(title));

                _usedTitles.Add(title);

                return title;
            }
        }

        public static int Year
        {
            get
            {
                double u = _random.NextDouble();
                double p = 4.0;
                double biased = Math.Pow(u, 1 / p); // shift probability to the right. if not 1 / p, it will be biased towards the lower end
                int min = 1888;
                int max = DateTime.UtcNow.Year;
                return min + (int)((max - min) * biased);
            }
        }

        public static string Review => string.Join(" ", Pick(ReviewOpenings), Pick(ReviewMiddles), Pick(ReviewEndings));
        public static string Synopsis => string.Join(" ", Pick(SynopsisOpenings), Pick(SynopsisMiddles), Pick(SynopsisEndings));
        public static string Genre => Pick(FilmGenres);
        public static string Language => Pick(Languages);

        private static string Pick(string[] array) => array[_random.Next(array.Length)];
    }
}
