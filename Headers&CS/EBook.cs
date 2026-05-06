using System;
using System.Collections.Generic;
using System.Text;

namespace TinyMart
{
    public class EBook : BookProduct
    {
        public EBook() : base() { }
        public EBook(string aProdName, double aPrice, NameType anAuthor, int pageNum) : base(aProdName, aPrice, anAuthor, pageNum) { }
        public override string GetProdTypeStr()
        {
            return "Ebook";
        }
    }
}
