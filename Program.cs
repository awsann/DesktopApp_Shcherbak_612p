using System;
using System.IO;

class Program
{
    //Масиви для збереження даних про елементи
    static string[] descriptions = new string[5];
    static double[] prices = new double[5];
    static int itemCount = 0;

    //Змінні для чайових
    static double tipAmount = 0.0;
    static int tipMethod = 3; //1=Percentage, 2=Amount, 3=No Tip

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
                    RemoveItem();
                    break;
                case 3:
                    AddTip();
                    break;
                case 4:
                    DisplayBill();
                    break;
                case 5:
                    ClearAll();
                    break;
                case 6:
                    SaveToFile();
                    break;
                case 7:
                    LoadFromFile();
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

    static void RemoveItem()
    {
        if (itemCount == 0)
        {
            Console.WriteLine("There are no items in the bill to remove.");
            return;
        }
        DisplayItemList();
        int itemNumber = GetItemNumber();
        if (itemNumber == 0)
        {
            return; //Скасування операції
        }
        //Видалення елемента шляхом зсуву елементів масиву
        for (int i = itemNumber - 1; i < itemCount - 1; i++)
        {
            descriptions[i] = descriptions[i + 1];
            prices[i] = prices[i + 1];
        }
        itemCount--;
        Console.WriteLine("Remove item was successful.");
    }

    static void DisplayItemList()
    {
        Console.WriteLine("ItemNo Description              Price");
        Console.WriteLine("------ ------------------- ----------");
        for (int i = 0; i < itemCount; i++)
        {
            Console.WriteLine($"{i + 1,6} {descriptions[i],-19} ${prices[i]:F2}");
        }
    }

    static int GetItemNumber()
    {
        int itemNumber;
        do
        {
            Console.Write($"Enter the item number to remove or 0 to cancel: ");
            while (!int.TryParse(Console.ReadLine(), out itemNumber))
            {
                Console.Write("Please enter a valid number: ");
            }
            if (itemNumber == 0)
            {
                break; //Скасування операції
            }
            if (itemNumber < 1 || itemNumber > itemCount)
            {
                Console.WriteLine($"Please enter a number between 1 and {itemCount}, or 0 to cancel.");
            }
        } while (itemNumber < 0 || itemNumber > itemCount);
        return itemNumber;
    }

    static void AddTip()
    {
        if (itemCount == 0)
        {
            Console.WriteLine("There are no items in the bill to add tip for.");
            return;
        }
        double netTotal = CalculateNetTotal();
        Console.WriteLine($"Net Total: ${netTotal:F2}");
        DisplayTipOptions();
        int choice = GetTipMethod();
        switch (choice)
        {
            case 1:
                tipMethod = 1;
                tipAmount = GetTipPercentage(netTotal);
                break;
            case 2:
                tipMethod = 2;
                tipAmount = GetTipAmount();
                break;
            case 3:
                tipMethod = 3;
                tipAmount = 0.0;
                break;
        }
    }

    static double CalculateNetTotal()
    {
        double total = 0.0;
        for (int i = 0; i < itemCount; i++)
        {
            total += prices[i];
        }
        return total;
    }

    static void DisplayTipOptions()
    {
        Console.WriteLine("1 - Tip Percentage");
        Console.WriteLine("2 - Tip Amount");
        Console.WriteLine("3 - No Tip");
    }

    static int GetTipMethod()
    {
        int choice;
        do
        {
            Console.Write("Enter Tip Method: ");
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.Write("Please enter a valid number: ");
            }
            if (choice < 1 || choice > 3)
            {
                Console.WriteLine("Please enter 1, 2, or 3.");
            }
        } while (choice < 1 || choice > 3);
        return choice;
    }

    static double GetTipPercentage(double netTotal)
    {
        double percentage;
        do
        {
            Console.Write("Enter tip percentage: ");
            while (!double.TryParse(Console.ReadLine(), out percentage))
            {
                Console.Write("Please enter a valid percentage: ");
            }
            if (percentage < 0)
            {
                Console.WriteLine("Percentage cannot be negative.");
            }
        } while (percentage < 0);
        return (netTotal * percentage) / 100.0;
    }

    static double GetTipAmount()
    {
        double amount;
        do
        {
            Console.Write("Enter tip amount: ");
            while (!double.TryParse(Console.ReadLine(), out amount))
            {
                Console.Write("Please enter a valid amount: ");
            }
            if (amount < 0)
            {
                Console.WriteLine("Tip amount cannot be negative.");
            }
        } while (amount < 0);
        return amount;
    }

    static void DisplayBill()
    {
        if (itemCount == 0)
        {
            Console.WriteLine("There are no items in the bill to display.");
            return;
        }
        double netTotal = CalculateNetTotal();
        double gstAmount = netTotal * 0.05; // GST = 5%
        double totalAmount = netTotal + tipAmount + gstAmount;
        Console.WriteLine("Description              Price");
        Console.WriteLine("------------------- ----------");
        for (int i = 0; i < itemCount; i++)
        {
            Console.WriteLine($"{descriptions[i],-19} ${prices[i]:F2}");
        }
        Console.WriteLine("------------------- ----------");
        Console.WriteLine($"{"Net Total",19} ${netTotal:F2}");
        Console.WriteLine($"{"Tip Amount",19} ${tipAmount:F2}");
        Console.WriteLine($"{"GST Amount",19} ${gstAmount:F2}");
        Console.WriteLine($"{"Total Amount",19} ${totalAmount:F2}");
    }

    static void ClearAll()
    {
        for (int i = 0; i < itemCount; i++)
        {
            descriptions[i] = null;
            prices[i] = 0.0;
        }
        itemCount = 0;
        tipAmount = 0.0;
        tipMethod = 3; 
        Console.WriteLine("All items have been cleared.");
    }

    static void SaveToFile()
    {
        if (itemCount == 0)
        {
            Console.WriteLine("There are no items in the bill to save.");
            return;
        }

        string filename = GetFileName();

        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                double netTotal = CalculateNetTotal();
                double gstAmount = netTotal * 0.05; // GST = 5%
                double totalAmount = netTotal + tipAmount + gstAmount;
                writer.WriteLine("Description              Price");
                writer.WriteLine("------------------- ----------");
                for (int i = 0; i < itemCount; i++)
                {
                    writer.WriteLine($"{descriptions[i],-19} ${prices[i]:F2}");
                }
                writer.WriteLine("------------------- ----------");
                writer.WriteLine($"{"Net Total",19} ${netTotal:F2}");
                writer.WriteLine($"{"Tip Amount",19} ${tipAmount:F2}");
                writer.WriteLine($"{"GST Amount",19} ${gstAmount:F2}");
                writer.WriteLine($"{"Total Amount",19} ${totalAmount:F2}");
            }
            Console.WriteLine($"Write to file {filename} was successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to file: {ex.Message}");
        }
    }

    static string GetFileName()
    {
        string filename;
        do
        {
            Console.Write("Enter the file path to save items to: ");
            filename = Console.ReadLine();
            if (string.IsNullOrEmpty(filename))
            {
                Console.WriteLine("Filename cannot be empty.");
                continue;
            }
            // Проверяем расширение файла
            if (!filename.EndsWith(".txt"))
            {
                Console.WriteLine("File must have .txt extension.");
                continue;
            }
            //Отримуємо ім’я файлу без шляху та розширення для перевірки довжини
            string filenameOnly = Path.GetFileNameWithoutExtension(filename);
            if (filenameOnly.Length < 1 || filenameOnly.Length > 10)
            {
                Console.WriteLine("Filename must be between 1 and 10 characters (without extension).");
                continue;
            }
            break;
        } while (true);
        return filename;
    }
    static void LoadFromFile()
    {
        string filename = GetLoadFileName();
        try
        {
            //Скидаємо наявні дані перед завантаженням
            ClearAll();
            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null && itemCount < 5)
                {
                    // Пропускаем строки заголовка и разделители
                    if (line.Contains("Description") || line.Contains("---") ||
                        line.Contains("Net Total") || line.Contains("Tip Amount") ||
                        line.Contains("GST Amount") || line.Contains("Total Amount") ||
                        string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    //Парсим рядок з товаром
                    int dollarIndex = line.LastIndexOf('$');
                    if (dollarIndex > 0)
                    {
                        string description = line.Substring(0, dollarIndex).Trim();
                        string priceStr = line.Substring(dollarIndex + 1).Trim();
                        if (double.TryParse(priceStr, out double price))
                        {
                            descriptions[itemCount] = description;
                            prices[itemCount] = price;
                            itemCount++;
                        }
                    }
                }
            }
            Console.WriteLine($"Read from file {filename} was successful.");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading from file: {ex.Message}");
        }
    }

    static string GetLoadFileName()
    {
        string filename;
        do
        {
            Console.Write("Enter the file path to load items from: ");
            filename = Console.ReadLine();
            if (string.IsNullOrEmpty(filename))
            {
                Console.WriteLine("Filename cannot be empty.");
                continue;
            }
            //Перевіряємо розширення файлу
            if (!filename.EndsWith(".txt"))
            {
                Console.WriteLine("File must have .txt extension.");
                continue;
            }
            break;
        } while (true);
        return filename;
    }
}