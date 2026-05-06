using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Reflection.Metadata.Ecma335;

namespace TinyMart
{
    public class Cart
    {
        public const int MAX_ITEMS = 7;
        public int ItemNum { get; private set; }
        public NameType Owner { get; set; }

        private List<Product> PurchasedItems;

        public Cart(NameType theOwner)
        {
            Owner = theOwner;
            PurchasedItems = new List<Product>();
            ItemNum = 0;
        }

        private bool IsCartFull() 
        { 
            return ItemNum >= MAX_ITEMS;
        }

        public static Cart operator +(Cart cart, Product item)
        {
            cart.AddItem(item);
            return cart;
        }

        public bool AddItem(Product theProduct)
        {
            try
            {
                if (IsCartFull())
                {
                    throw new CartOverflowException($"Cart overflow: {theProduct.ProductName} Prod ID:{theProduct.ProductID}, Max items: {MAX_ITEMS}");
                }

                PurchasedItems.Add(theProduct);
                ItemNum++;
                return true;
            }
            catch (CartOverflowException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool RemoveItem(int theProductID)
        {
            try
            {
                if (ItemNum==0)
                {
                    throw new CartUnderflowException($"Cart underflow: the cart is empty");
                }

                for (int i = 0; i < PurchasedItems.Count; i++)
                {
                    if (PurchasedItems[i].ProductID == theProductID)
                    {
                        PurchasedItems.RemoveAt(i);
                        ItemNum--;
                        return true;
                    }
                }
                return false;
            }
            catch (CartUnderflowException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Product SearchProduct(string prodName)
        {
            foreach (var item in PurchasedItems)
            {
                if (item.ProductName.Equals(prodName, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
            }
            return null;
        }
        public void DisplayCart()
        {
            Console.WriteLine("\nMy Cart");
            Console.WriteLine("======");
            Console.WriteLine($"Cart Owner: {Owner}");

            double videoTotal = 0, audioTotal = 0, ebookTotal = 0, paperTotal = 0;

            Console.WriteLine("\n[Music]");
            foreach (var item in PurchasedItems)
            {
                if (item is AudioProduct)
                {
                    item.DisplayProdInfo();
                    audioTotal += item.Price;
                }
            }

            Console.WriteLine("\n[Movie]");
            foreach (var item in PurchasedItems)
            {
                if (item is VideoProduct)
                {
                    item.DisplayProdInfo();
                    videoTotal += item.Price;
                }
            }

            Console.WriteLine("\n[E book]");
            foreach (var item in PurchasedItems)
            {
                if (item is EBook)
                {
                    item.DisplayProdInfo();
                    ebookTotal += item.Price;
                }
            }

            Console.WriteLine("\n[Paper book]");
            foreach (var item in PurchasedItems)
            {
                if (item is PaperBook)
                {
                    item.DisplayProdInfo();
                    paperTotal += item.Price;
                }
            }

            double total = audioTotal + videoTotal + ebookTotal + paperTotal;

            Console.WriteLine("\n====== Summary of Purchase =======");
            Console.WriteLine($"Total number of purchases: {ItemNum}");
            Console.WriteLine($"Video product amount: ${videoTotal:0.##}");
            Console.WriteLine($"Audio product amount: ${audioTotal:0.##}");
            Console.WriteLine($"Ebook amount: ${ebookTotal:0.##}");
            Console.WriteLine($"Paper book amount: ${paperTotal:0.##}");
            Console.WriteLine($"Total purchasing amount: ${total:0.##}");

            if (ItemNum > 0)
                Console.WriteLine($"Average cost: ${(total / ItemNum):0.00}");
        }

        public bool SaveCart(string fileName)
        {
            try
            {
                using(StreamWriter sw = new StreamWriter(fileName))
                {
                    foreach (var item in PurchasedItems)
                    {
                        if (item is VideoProduct vp)
                        {
                            sw.WriteLine($"Video,{vp.ProductName},{vp.Price},{vp.Director.FirstName} {vp.Director.LastName},{vp.ReleaseYear},{vp.RunTime},{vp.FilmRate},{vp.ReviewRate}");
                        }
                        else if (item is AudioProduct ap)
                        {
                            sw.WriteLine($"Audio,{ap.ProductName},{ap.Price},{ap.Singer.FirstName} {ap.Singer.LastName},{ap.Genre},{ap.ReviewRate}");
                        }
                        else if (item is EBook eb)
                        {
                            sw.WriteLine($"Ebook,{eb.ProductName},{eb.Price},{eb.Author.FirstName} {eb.Author.LastName},{eb.Pages},{eb.ReviewRate}");
                        }
                    }
                }
                return true;
            }
            catch { return false; }
        }

        public bool ReadFromFile(string fileName)
        {
            if (!File.Exists(fileName)) return false;

            try
            {
                string[] lines = File.ReadAllLines(fileName);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 0) continue;

                    string type = parts[0].Trim();
                    string prodName = parts[1].Trim();
                    double price = double.Parse(parts[2].Trim());

                    string[] nameParts = parts[3].Trim().Split(' ');
                    string firstName = nameParts[0];
                    string lastName = nameParts.Length > 1 ? nameParts[1] : "";
                    NameType creator = new NameType(firstName, lastName);

                    Product newItem = null;

                    switch (type)
                    {
                        case "Video":
                            int releaseYear = int.Parse(parts[4].Trim());
                            int runTime = int.Parse(parts[5].Trim());
                            FilmRateType rate = (FilmRateType)Enum.Parse(typeof(FilmRateType), parts[6].Trim(), true);
                            float videoReview = float.Parse(parts[7].Trim());

                            VideoProduct vp = new VideoProduct(prodName, price, creator, releaseYear, runTime);
                            vp.FilmRate = rate;
                            vp.ReviewRate = videoReview;
                            newItem = vp;
                            break;

                        case "Audio":
                            GenreType genre = (GenreType)Enum.Parse(typeof(GenreType), parts[4].Trim(), true);
                            float audioReview = float.Parse(parts[5].Trim());

                            AudioProduct ap = new AudioProduct(prodName, price, creator);
                            ap.Genre = genre;
                            ap.ReviewRate = audioReview;
                            newItem = ap;
                            break;

                        case "Ebook":
                            int pages = int.Parse(parts[4].Trim());
                            float ebookReview = float.Parse(parts[5].Trim());

                            EBook eb = new EBook(prodName, price, creator, pages);
                            eb.ReviewRate = ebookReview;
                            newItem = eb;
                            break;
                    }

                    if (newItem != null)
                    {
                        AddItem(newItem);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading from file: {ex.Message}");
                return false;
            }
        }
        public override string ToString()
        {
            return $"Cart owned by {Owner.ToString()} containing {ItemNum} items.";
        }
    
    }
}
