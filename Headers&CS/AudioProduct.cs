using System;
using System.Collections.Generic;
using System.Text;

namespace TinyMart
{

    public class AudioProduct : Product
    {
        public NameType Singer { get; set; }
        public GenreType Genre { get; set; }


        public AudioProduct() : base()
        {
            Singer = new NameType("Unkown", "Singer");
            Genre = GenreType.Pop;
        }

        public AudioProduct(string eProdName, double ePrice, NameType aSinger)
            : base(eProdName, ePrice)
        {
            Singer = aSinger;
            Genre = GenreType.Pop;
        }

        public override string GetProdTypeStr()
        {
            return "Music";
        }

        public override void DisplayContentsInfo()
        {
            Console.WriteLine($"Singer Name: {Singer.ToString()}");
            Console.WriteLine($"Genre: {Genre}");
        }
    }
}