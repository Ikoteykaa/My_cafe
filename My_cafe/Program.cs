using System;

namespace MonksCafe
{
    class Program
    {
        static Bill bill = new();
        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                ShowMenu();

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddItem();
                        break;

                    case "2":
                        Console.WriteLine("\nFunction is not implemented yet.");
                        break;

                    case "3":
                        Console.WriteLine("\nFunction is not implemented yet.");
                        break;

                    case "4":
                        Console.WriteLine("\nFunction is not implemented yet.");
                        break;

                    case "5":
                        Console.WriteLine("\nFunction is not implemented yet.");
                        break;

                    case "6":
                        Console.WriteLine("\nFunction is not implemented yet.");
                        break;

                    case "7":
                        Console.WriteLine("\nFunction is not implemented yet.");
                        break;

                    case "0":
                        exit = true;
                        Console.WriteLine("\nGoodbye!");
                        break;

                    default:
                        Console.WriteLine("\nIncorrect choice!");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key...");
                    Console.ReadKey();
                }
            }
        }

        static void ShowMenu()
        {
            Console.Clear();

            Console.WriteLine("=================================");
            Console.WriteLine("          Monk's Cafe");
            Console.WriteLine("=================================");
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. Remove Item");
            Console.WriteLine("3. Add Tip");
            Console.WriteLine("4. Display Bill");
            Console.WriteLine("5. Clear Bill");
            Console.WriteLine("6. Save Bill");
            Console.WriteLine("7. Load Bill");
            Console.WriteLine("0. Exit");
            Console.WriteLine("=================================");
        }
        static void AddItem()
        {
            Console.Clear();

            if (bill.Count == 5)
            {
                Console.WriteLine("Bill is full.");
                return;
            }

            string description;

            while (true)
            {
                Console.Write("Description (3-20): ");
                description = Console.ReadLine();

                if (description.Length >= 3 && description.Length <= 20)
                    break;

                Console.WriteLine("Incorrect description.");
            }

            double price;

            while (true)
            {
                Console.Write("Price (>0): ");

                if (double.TryParse(Console.ReadLine(), out price))
                {
                    if (price > 0)
                        break;
                }

                Console.WriteLine("Incorrect price.");
            }

            Item item = new Item();

            item.Description = description;
            item.Price = price;

            bill.Items[bill.Count] = item;

            bill.Count++;

            Console.WriteLine();
            Console.WriteLine("Item added successfully.");
        }
    }
}

