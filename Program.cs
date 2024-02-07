using static System.Console;
using System.IO;
using System.Collections.Generic;
using System.Threading.Channels;

namespace LeonardStorefront
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * Name: Jaiden Leonard
             * Class: CPSC-23000-001
             * Assignment: Storefront
             * Date: 2/6/2024
            */

            /*
             *  Struggles:
             *  I had a difficult time trying to get the program to terminate when the user entered
             *  "quit" (it still asked how many before terminating) so I had to add an if statement
             *  inside to break from the loop properly.
             *  
             *  I also had a difficult time getting the program to terminate properly if the user entered
             *  an input that was incorrect because again, it asked how many before closing so I had to
             *  do the same thing as before and add an if statement to tell it to close.
             *  
             *  Conclusion:
             *  I may have made this super complicated on myself which I honestly think I did but I tried
             *  my hardest and this was the best that I could come up with that functioned properly. If you
             *  notice anything that could be changed or I messed up on, please let me know as I want a
             *  career in software development/engineering and all feedback (positive or negative) is 
             *  good for me to learn and get better.
            */

            //print header
            WriteLine("**************************************");
            WriteLine("          STOREFRONT V1.0");
            WriteLine("**************************************");

            //declare variables
            string userEntry;
            int quantity = 0;
            double price = 0;
            double cost = 0;

            //declare dictionaries for items and prices
            Dictionary<string, double> items = new Dictionary<string, double>();
            Dictionary<string, int> purchases = new Dictionary<string, int>();

            //ask user for file path
            Write("\nEnter name of file: ");
            String path = ReadLine();

            //try catch block for reading the file
            try
            {
                if (File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        while (!sr.EndOfStream)
                        {
                            String line = sr.ReadLine();
                            string[] parts = line.Split('$');
                            string itemName = parts[0].Trim();
                            price = double.Parse(parts[1].Trim());
                            items.Add(itemName, price);
                        }
                    }
                }
                else
                {
                    WriteLine("\nThis file does not exist");
                    return;
                }
            }
            catch (Exception ex)
            {
                WriteLine("The file could not be read");
                return;
            }

            //do while loop for the shopping list
            do
            {
                //output list to the console
                WriteLine("\nWhat would you like to buy?");
                foreach (var item in items)
                {
                    WriteLine($"{item.Key,-20} ${item.Value}");
                }

                //ask user what they want
                Write("Enter your choice: ");
                userEntry = ReadLine();

                //if statement to leave the loop if "quit" is entered
                if (userEntry == "quit")
                {
                    break;
                }

                //if statement to leave the loop if a wrong option is entered
                if (!items.ContainsKey(userEntry))
                {
                    WriteLine("\nThat is not an option on the list.");
                    break;
                }

                //ask user how many they want
                Write("How many do you want? ");
                quantity = int.Parse(ReadLine());
                if (items.ContainsKey(userEntry))
                {
                    if (purchases.ContainsKey(userEntry))
                    {
                        purchases[userEntry] += quantity;
                    }
                    else
                    {
                        purchases.Add(userEntry, quantity);
                    }
                }
            } while (userEntry != "quit");

            //tally up all of the items and output to user
            double totalCost = 0;
            WriteLine("\nThank you for shopping. Here is what you bought:");
            foreach (var pair in purchases)
            {
                string item = pair.Key;
                quantity = pair.Value;
                price = items[item];
                cost = price * quantity;
                WriteLine($"{item,-15} {quantity}");
                totalCost += cost;
            }

            //output total bill to the user
            WriteLine($"Your total bill is ${totalCost:F2}");
        }
    }
}
