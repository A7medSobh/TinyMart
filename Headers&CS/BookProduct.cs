using System;
using System.Collections.Generic;
using System.Text;

namespace TinyMart
{
    public abstract class BookProduct : Product
    {
        public NameType Author {  get; set; }
        public int Pages { get; set; }

        public BookProduct() : base()
        {
            Author = new NameType("Unknown", "Author");
            Pages = 0;
        }

        public BookProduct(string aProdName, double aPrice, NameType anAuthor, int pageNum) : base(aProdName, aPrice)
        {
            Author = anAuthor;
            Pages = pageNum;
        }

        public abstract override string GetProdTypeStr();

        public override void DisplayContentsInfo()
        {
            Console.WriteLine($"Author: {Author.ToString()}");
            Console.WriteLine($"Pages: {Pages}");
        }
    }
}
