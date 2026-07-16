using System;
using System.IO;

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
                        RemoveItem();
                        break;

                    case "3":
                        AddTip();
                        break;

                    case "4":
                        DisplayBill();
                        break;

                    case "5":
                        ClearBill();
                        break;

                    case "6":
                        SaveBill();
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
        static void RemoveItem()
        {
            Console.Clear();

            if (bill.Count == 0)
            {
                Console.WriteLine("Bill is empty.");
                return;
            }

            Console.WriteLine("Items:");

            for (int i = 0; i < bill.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " +
                                  bill.Items[i].Description +
                                  " - " +
                                  bill.Items[i].Price);
            }

            int number;

            while (true)
            {
                Console.Write("\nEnter item number: ");

                if (int.TryParse(Console.ReadLine(), out number))
                {
                    if (number >= 1 && number <= bill.Count)
                        break;
                }

                Console.WriteLine("Incorrect number.");
            }

            number--;

            for (int i = number; i < bill.Count - 1; i++)
            {
                bill.Items[i] = bill.Items[i + 1];
            }

            bill.Items[bill.Count - 1] = null;

            bill.Count--;

            Console.WriteLine("\nItem removed successfully.");
        }
        static void AddTip()
        {
            Console.Clear();

            if (bill.Count == 0)
            {
                Console.WriteLine("Bill is empty.");
                return;
            }

            double tip;

            while (true)
            {
                Console.Write("Enter tip: ");

                if (double.TryParse(Console.ReadLine(), out tip))
                {
                    if (tip >= 0)
                        break;
                }

                Console.WriteLine("Incorrect tip.");
            }

            bill.Tip = tip;

            Console.WriteLine();
            Console.WriteLine("Tip added successfully.");
        }
        static void DisplayBill()
        {
            Console.Clear();

            if (bill.Count == 0)
            {
                Console.WriteLine("Bill is empty.");
                return;
            }

            double sum = 0;

            Console.WriteLine("=========== BILL ===========");
            Console.WriteLine();

            Console.WriteLine("{0,-20}{1,10}", "Description", "Price");
            Console.WriteLine("-------------------------------");

            for (int i = 0; i < bill.Count; i++)
            {
                Console.WriteLine("{0,-20}{1,10:F2}",
                    bill.Items[i].Description,
                    bill.Items[i].Price);

                sum += bill.Items[i].Price;
            }

            Console.WriteLine("-------------------------------");

            Console.WriteLine("{0,-20}{1,10:F2}", "Subtotal:", sum);
            Console.WriteLine("{0,-20}{1,10:F2}", "Tip:", bill.Tip);
            Console.WriteLine("-------------------------------");
            Console.WriteLine("{0,-20}{1,10:F2}", "TOTAL:", sum + bill.Tip);

            Console.WriteLine();
        }
        static void ClearBill()
        {
            Console.Clear();

            if (bill.Count == 0)
            {
                Console.WriteLine("Bill is already empty.");
                return;
            }

            Console.Write("Are you sure? (Y/N): ");

            string answer = Console.ReadLine();

            if (answer.ToUpper() == "Y")
            {
                for (int i = 0; i < bill.Count; i++)
                {
                    bill.Items[i] = null;
                }

                bill.Count = 0;
                bill.Tip = 0;

                Console.WriteLine();
                Console.WriteLine("Bill cleared successfully.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Operation cancelled.");
            }
        }
        static void SaveBill()
        {
            Console.Clear();

            if (bill.Count == 0)
            {
                Console.WriteLine("Bill is empty.");
                return;
            }

            string fileName;

            while (true)
            {
                Console.Write("Enter filename (1-10 symbols): ");
                fileName = Console.ReadLine();

                if (fileName.Length >= 1 && fileName.Length <= 10)
                    break;

                Console.WriteLine("Incorrect filename.");
            }

            fileName += ".txt";

            try
            {
                StreamWriter writer = new StreamWriter(fileName);

                double sum = 0;

                writer.WriteLine("===== BILL =====");
                writer.WriteLine();

                for (int i = 0; i < bill.Count; i++)
                {
                    writer.WriteLine(
                        bill.Items[i].Description +
                        " - " +
                        bill.Items[i].Price.ToString("F2"));

                    sum += bill.Items[i].Price;
                }

                writer.WriteLine();
                writer.WriteLine("Subtotal: " + sum.ToString("F2"));
                writer.WriteLine("Tip: " + bill.Tip.ToString("F2"));
                writer.WriteLine("Total: " + (sum + bill.Tip).ToString("F2"));

                writer.Close();

                Console.WriteLine();
                Console.WriteLine("Bill saved successfully.");
            }
            catch
            {
                Console.WriteLine("Error while saving file.");
            }
        }
    }
}

