using System;

namespace Sols_RNG_Copy
{
    class Program
    {
        static void Main(string[] args)
        {
            // window config
            Console.Title = "Sol's RNG Copy";
            Console.WindowHeight = 45;
            Console.WindowWidth = 35;

            // arrays

            string[] auras = {"Common", "Uncommon", "Rare", "Divinus", "Crystallized", "Magnetic", "Rage", "Aquatic", "Melodic", "Chromatic"};
            double[] chances = {5000, 2500, 1000, 150, 75, 45, 20, 10, 5, 1};
            int[] auraValue = {1, 2, 5, 10, 25, 35, 50, 75, 100, 250};
            ConsoleColor[] auraColors = {ConsoleColor.White, ConsoleColor.White, ConsoleColor.Blue, ConsoleColor.Yellow, ConsoleColor.Magenta, ConsoleColor.Red, ConsoleColor.DarkRed, ConsoleColor.Cyan, ConsoleColor.DarkYellow, ConsoleColor.DarkMagenta};
            int[] inventory = new int[auras.Length];
            double[] luckMultiplier = {0.7, 1.0, 1.5, 2.3, 3.5, 2.1, 1.8, 1.7, 1.6, 1.4};

            // variables

            Random gen = new Random();

            int attempts = 0;
            bool luckActive = false;
            int cash = 0;

            string luckPotion = "no";
            string action = "a";
            string shopLine = "a";

            // to get the auras

            void GetAura()
            {
                double[] chancesToUse;

                if (luckActive == true)
                {
                    double[] modifiedChances = new double[chances.Length];

                    for (int i = 0; i < auras.Length; i++)
                    { 
                        modifiedChances[i] = chances[i] * luckMultiplier[i];
                    }  
                    chancesToUse = modifiedChances;
                }
                else
                {
                    chancesToUse = chances;
                }

                double total = 0;

                foreach (double chance in chancesToUse)
                {
                    total += chance;
                }

                double roll = gen.NextDouble() * total;

                double cumulative = 0;

                for (int i = 0; i < chancesToUse.Length; i++)
                { 
                    cumulative += chancesToUse[i];

                    if (roll < cumulative)
                    {
                        Console.ForegroundColor = auraColors[i];
                        Console.WriteLine("You rolled.. " + auras[i]);
                        Console.ResetColor();

                        inventory[i]++;
                        break;
                    }
                }
            }

            // voids

            void Start()
            {
                attempts = 0;
                action = Console.ReadLine();

                if (action == "roll")
                {   
                    Console.WriteLine("\nPress Enter to roll!");
                    Roll();
                }  
                else if (action == "shop")
                {
                    Console.WriteLine("Available items: \nLuck Potion (luck)");
                    Shop();
                }
                else if (action == "sell")
                {
                    Sell();
                }
                else if (action == "inventory")
                {
                    ShowInventory();
                }
                else if (action == "coins")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("You have " + cash + " coins!");
                    Console.ResetColor();

                    Console.WriteLine("\nThen what would you want to do?");
                    Start();
                }
                else if (action == "luck")
                {
                    if (luckActive == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("You're lucky!");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("You were not blessed by the lady luck.");
                    }
                
                    Console.ResetColor();

                    Console.WriteLine("\nThen what would you want to do?");
                    Start();
                }
                else
                {
                    Console.WriteLine("The only commands are:\nRoll, Shop, Sell, Inventory, Coins, and Luck.\nWhat do you want to do?");
                    Start();
                }
            }

            void Shop()
            {
                shopLine = Console.ReadLine();

                if (shopLine == "luck")
                {
                    Console.WriteLine("Would you like a Luck Potion for 5 coins?");
                    luckPotion = Console.ReadLine();

                    if (luckPotion == "yes")
                    {
                        if (luckActive == true)
                        {
                            Console.WriteLine("You're already lucky. \nWhat would you like to do?");
                            Start();
                        }
                        else if (cash >= 5)
                        {
                            Console.WriteLine("\nYour luck has been increased!");
                            luckActive = true;

                            cash -= 5;

                            Console.WriteLine("Then what would you want to do?");
                            Start();
                        }
                        else
                        {
                            Console.WriteLine("You don't have enough money.");

                            Console.WriteLine("Then what would you want to do?");
                            Start();
                        }
                    }
                    else
                    {   
                        Console.WriteLine("\nAlright then!");
                        Console.WriteLine("Then what would you want to do?");
                        Start();
                    }
                }
                else
                {
                    Console.WriteLine("\nThat's not an valid argument.\nWhat would you like to do?");
                    Start();
                }
            }

            void Sell()
            {
                int totalCash = 0;

                for (int i = 0; i < auras.Length; i++)
                {
                    totalCash += inventory[i] * auraValue[i];
                    inventory[i] = 0;
                }

                cash += totalCash;

                Console.WriteLine("You sold your auras for " + totalCash + " coins!");

                Console.WriteLine("\nWhat action would you like to do?");

                Start();
            }

            void ShowInventory()
            {
                for (int i = 0; i < auras.Length; i++)
                {
                    if (inventory[i] > 0)
                    {
                        Console.WriteLine("-----------");
                        Console.ForegroundColor = auraColors[i];
                        Console.WriteLine($"{auras[i]} × {inventory[i]}");
                    }
                }

                Console.ResetColor();
                Console.WriteLine("-----------");
                Console.WriteLine("\nWhat action would you like to do?");

                Start();
            }

            void Roll()
            {
                while (attempts < 10)
                {
                    Console.ReadKey();

                    GetAura();

                    attempts++;
                }

                if (luckActive == true)
                {
                    Console.WriteLine("You've rolled 10 times and your luck has ran out.\nWhat would you like to do?");
                    luckActive = false;
                    Start();
                }
                else
                {
                    Console.WriteLine("You've rolled 10 times.\nWhat would you like to do?");
                    Start();
                }
            }

            // function
            Console.WriteLine("Welcome to Sol's RNG Copy!\nWhat action would you like to do?");
            Start();
        }
    }
}
