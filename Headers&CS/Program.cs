using System;
using System.Collections.Generic;
using System.Linq;

namespace TinyMart
{
    class Program
    {
        static void Main(string[] args)
        {
            NameType ownerName = new NameType("John", "Smith");
            Cart myCart = new Cart(ownerName);

            // Audio
            var song1 = new AudioProduct("Yesterday", 16.5, new NameType("Beetles", ""));
            song1.Genre = GenreType.Pop;
            song1.ReviewRate = 9.8f;

            var song2 = new AudioProduct("We are the World", 13.75, new NameType("Michael", "Jackson"));
            song2.Genre = GenreType.Country;
            song2.ReviewRate = 9.1f;

            // Video
            var movie1 = new VideoProduct("Sound of Music", 22.0, new NameType("Robert", "Wise"), 1965, 175);
            movie1.FilmRate = FilmRateType.G;
            movie1.ReviewRate = 9.2f;

            var movie2 = new VideoProduct("Star Wars", 22.0, new NameType("George", "Lucas"), 1977, 120);
            movie2.FilmRate = FilmRateType.PG;
            movie2.ReviewRate = 8.5f;

            // Ebook
            var ebook = new EBook("The old Man and the Sea", 8.3, new NameType("Ernest", "Hemmingway"), 127);
            ebook.ReviewRate = 9.5f;

            // PaperBook
            var paper = new PaperBook("The Great Gatsby", 10.5, new NameType("F. Scott", "Fitzgerald"), 200);
            paper.ReviewRate = 8.9f;

            // Add items (max 7 allowed)
            myCart.AddItem(song1);
            myCart.AddItem(song2);
            myCart.AddItem(movie1);
            myCart.AddItem(movie2);
            myCart.AddItem(ebook);
            myCart.AddItem(paper);

            // Try overflow
            myCart.AddItem(new AudioProduct("Extra Item", 5.0, new NameType("Test", "User")));

            myCart.DisplayCart();

            Console.ReadLine();
        }
    }
}