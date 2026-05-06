// Foundations.cs
using System;

namespace TinyMart
{
    public struct NameType
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public NameType(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }

    public enum GenreType
    {
        Blues, Classical, Country, Folk, Jazz, Metal, Pop, RnB, Rock
    }

    public enum FilmRateType
    {
        NotRated, G, PG, PG_13, R, NC_17
    }
}