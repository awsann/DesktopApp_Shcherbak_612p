using System;
using System.IO;

class Program
{
    //Масиви для збереження даних про елементи
    static string[] descriptions = new string[5];
    static double[] prices = new double[5];
    static int itemCount = 0;

    static void Main(string[] args)
    {
        int choice;
        do
        {
            DisplayMenu();
            choice = GetUserChoice();
            switch (choice)
            {
                case 1:
                    AddItem();
                    break;
                case 2:
                    //RemoveItem();
                    Console.WriteLine(" ");
                    break;
                case 3:
                    //AddTip();
                    Console.WriteLine(" ");
                    break;
                case 4:
                    //DisplayBill();
                    Console.WriteLine(" ");
                    break;
                case 5:
                    //ClearAll();
                    Console.WriteLine(" ");
                    break;
                case 6:
                    //SaveToFile();
                    Console.WriteLine(" ");
                    break;
                case 7:
                    //LoadFromFile();
                    Console.WriteLine(" ");
                    break;
                case 0:
                    Console.WriteLine("Good-bye and thanks for using this program.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        } while (choice != 0);
    }

    static void DisplayMenu()
    {
        Console.WriteLine("┌─────────────────────────┐");
        Console.WriteLine("│ Cozy Corner             │");
        Console.WriteLine("│ ----------------------- │");
        Console.WriteLine("│ 1. Add Item             │");
        Console.WriteLine("│ 2. Remove Item          │");
        Console.WriteLine("│ 3. Add Tip              │");
        Console.WriteLine("│ 4. Display Bill         │");
        Console.WriteLine("│ 5. Clear All            │");
        Console.WriteLine("│ 6. Save to file         │");
        Console.WriteLine("│ 7. Load from file       │");
        Console.WriteLine("│ 0. Exit                 │");
        Console.WriteLine("└─────────────────────────┘");
    }

    static int GetUserChoice()
    {
        Console.Write("Enter your choice: ");
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice))
        {
            Console.Write("Please enter a valid number: ");
        }
        return choice;
    }

    static void AddItem()
    {
        if (itemCount >= 5)
        {
            Console.WriteLine("Cannot add more than 5 items.");
            return;
        }
        string description = GetDescription();
        double price = GetPrice();
        descriptions[itemCount] = description;
        prices[itemCount] = price;
        itemCount++;
        Console.WriteLine("Add item was successful.");
    }

    static string GetDescription()
    {
        string description;
        do
        {
            Console.Write("Enter description: ");
            description = Console.ReadLine();
            if (string.IsNullOrEmpty(description) || description.Length < 3 || description.Length > 20)
            {
                Console.WriteLine("Description must be between 3 and 20 characters.");
            }
        } while (string.IsNullOrEmpty(description) || description.Length < 3 || description.Length > 20);
        return description;
    }

    static double GetPrice()
    {
        double price;
        do
        {
            Console.Write("Enter price: ");
            while (!double.TryParse(Console.ReadLine(), out price))
            {
                Console.Write("Please enter a valid price: ");
            }
            if (price <= 0)
            {
                Console.WriteLine("Price must be greater than 0.");
            }
        } while (price <= 0);
        return price;
    }
}