using System;
using System.Collections.Generic;
using System.Text;

namespace TinyMart
{
    public class PaperBook : BookProduct
    {
        public PaperBook() : base() { }
        public PaperBook(string aProdName, double aPrice, NameType anAuthor, int pageNum) : base(aProdName, aPrice, anAuthor, pageNum) { }
        public override string GetProdTypeStr()
        {
            return "Paper Book";
        }
    }
}
