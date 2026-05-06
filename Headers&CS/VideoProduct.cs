using System;
using System.Collections.Generic;
using System.Text;

namespace TinyMart
{
    public class VideoProduct : Product
    {
        public NameType Director {  get; set; }
        public FilmRateType FilmRate {  get; set; }
        public int ReleaseYear { get; set; }
        public int RunTime { get; set; }

        public VideoProduct() : base()
        {
            Director = new NameType("Unknown", "Director");
            FilmRate = FilmRateType.NotRated;
            ReleaseYear = 2000;
            RunTime = 0;
        }

        public VideoProduct(string aProdName, double aPrice, NameType aDirectorName, int aReleaseYear, int aRunTime) : base (aProdName, aPrice)
        {
            Director =aDirectorName;
            ReleaseYear=aReleaseYear;
            RunTime = aRunTime;
            FilmRate = FilmRateType.NotRated;

        }

        public bool IsNewRelease(int theYear) { return ReleaseYear >= theYear; }

        public override string GetProdTypeStr() { return "Movie"; }

        public override void DisplayContentsInfo()
        {
            Console.WriteLine($"Release Year: {ReleaseYear}");
            Console.WriteLine($"Film Rating: {FilmRate}");
            Console.WriteLine($"Runtime: {RunTime}");
            Console.WriteLine($"Director Name: {Director.ToString()}");
        }
        
    }
}
