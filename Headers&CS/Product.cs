using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace TinyMart
{
    public abstract class Product
    {
        private static int nextID = 1;

        public int ProductID { get; private set; }

        private string pName;
        public string ProductName
        {
            get { return pName; }
            set { pName = string.IsNullOrWhiteSpace(value) ? "!No Name Product! " : value; }
        }

        private double price;
        public double Price
        {
            get { return price; }
            set
            {
                if (value > 0.0 && value < 1000.00)
                    price = value;
                else
                    price = 0.01; //defult
            }
        }

        public float ReviewRate { get; set; }

        public Product()
        {
            ProductID = CreateNewID();
            ProductName = "!No Name Product!";
            Price = 0.01;
        }

        public Product(string eProdName, double ePrice)
        {
            ProductID = CreateNewID();
            ProductName = eProdName;
            Price = ePrice;
        }

        private static int CreateNewID()
        {
            return nextID++;
        }

        public abstract string GetProdTypeStr();
        public abstract void DisplayContentsInfo();

        public virtual void DisplayProdInfo()
        {
            Console.WriteLine($"Product ID: {ProductID} Product Name: {ProductName}");
            Console.WriteLine($"Price: ${Price} Product Review Rate: {ReviewRate}");

            DisplayContentsInfo();

        }

    }

}