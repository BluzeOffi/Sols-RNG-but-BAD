using System;
using System.Formats.Asn1;
using System.IO;

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
            double[] superLuckMultiplier = {0.2, 0.7, 0.9, 1.6, 2.3, 2.7, 2.0, 1.9, 1.8, 1.7};

            // variables

            Random gen = new Random();

            int attempts = 0;
            bool luckActive = false;
            bool cursedLuckActive = false;
            bool rageActive = false;
            int cash = 0;

            string luckPotion = "no";
            string cursedPotion = "no";
            string ragePotion = "no";
            string action = "a";
            string shopLine = "a";

            // to get the auras

            void GetAura()
            {
                double[] chancesToUse;

                if (cursedLuckActive == true && luckActive == true)
                {
                    double[] superModifiedChances = new double[chances.Length];

                    for (int i = 0; i < auras.Length; i++)
                    { 
                        superModifiedChances[i] = chances[i] * superLuckMultiplier[i];
                    }  
                    chancesToUse = superModifiedChances;
                }
                else if (cursedLuckActive == true)
                {
                    double[] superModifiedChances = new double[chances.Length];

                    for (int i = 0; i < auras.Length; i++)
                    { 
                        superModifiedChances[i] = chances[i] * superLuckMultiplier[i];
                    }  
                    chancesToUse = superModifiedChances;
                }
                else if (luckActive == true)
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
                    Console.WriteLine("Available items: \nLuck Potion (luck)\nRage Potion (rage)\nCursed Potion (cursed)");
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
                else if (action == "rage")
                {
                    if (rageActive == true)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Your rage burns with fire.");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You're not enraged right now.");
                    }
                
                    Console.ResetColor();

                    Console.WriteLine("\nThen what would you want to do?");
                    Start();
                }
                else if (action == "curse")
                {
                    if (cursedLuckActive == true)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("You're cursed beyond saving..");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("You're still safe from the curse.");
                    }
                
                    Console.ResetColor();

                    Console.WriteLine("\nThen what would you want to do?");
                    Start();
                }
                else if (action == "save")
                {
                    SaveGame();
                    Start();
                    
                }
                else if (action == "load")
                {
                    LoadGame();
                    Start();
                }
                else
                {
                    Console.WriteLine("The only commands are:\nRoll, Shop, Sell, Inventory, Coins, Luck, Rage, Curse, Save, and Load.\nWhat do you want to do?");
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
                else if (shopLine == "rage")
                {
                    Console.WriteLine("Would you like a Rage Potion for 10 coins?");
                    ragePotion = Console.ReadLine();

                    if (ragePotion == "yes")
                    {
                        if (rageActive == true)
                        {
                            Console.WriteLine("You're already angry. \nWhat would you like to do?");
                            Start();
                        }
                        else if (cash >= 5)
                        {
                            Console.WriteLine("\nYou have been enraged!");
                            rageActive = true;

                            cash -= 10;

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
                else if (shopLine == "cursed")
                {
                    Console.WriteLine("Would you like a cursed Potion for 350 coins?");
                    cursedPotion = Console.ReadLine();

                    if (cursedPotion == "yes")
                    {
                        if (cursedLuckActive == true)
                        {
                            Console.WriteLine("You're already cursed. \nWhat would you like to do?");
                            Start();
                        }
                        else if (cash >= 5)
                        {
                            Console.WriteLine("\nYou have been cursed by the ominous potion.");
                            cursedLuckActive = true;

                            cash -= 350;

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
                if (rageActive == true)
                {
                    while (attempts < 15)
                    {
                        Console.ReadKey();

                        GetAura();

                        attempts++;
                    }
                }
                else
                {
                    while (attempts < 10)
                    {
                        Console.ReadKey();

                        GetAura();

                        attempts++;
                    }
                }
                
                if (rageActive == true && luckActive == true && cursedLuckActive == true)
                {
                    Console.WriteLine("You've rolled 15 times, your cursed luck and rage has dissipated.\nWhat would you like to do?");
                    luckActive = false;
                    rageActive = false;
                    cursedLuckActive = false;
                    Start();
                }
                else if (rageActive == true && cursedLuckActive == true)
                {
                    Console.WriteLine("You've rolled 15 times, your cursed luck and rage has dissipated.\nWhat would you like to do?");
                    luckActive = false;
                    rageActive = false;
                    cursedLuckActive = false;
                    Start();
                }
                else if (luckActive == true && cursedLuckActive == true)
                {
                    Console.WriteLine("You've rolled 15 times, your cursed luck and rage has dissipated.\nWhat would you like to do?");
                    luckActive = false;
                    rageActive = false;
                    cursedLuckActive = false;
                    Start();
                }
                else if (rageActive == true && luckActive == true)
                {
                    Console.WriteLine("You've rolled 15 times, your luck and rage has dissipated.\nWhat would you like to do?");
                    luckActive = false;
                    rageActive = false;
                    Start();
                }
                else if (rageActive == true)
                {
                    Console.WriteLine("You've rolled 15 times and you've calmed down.\nWhat would you like to do?");
                    rageActive = false;
                    Start();
                }
                else if (luckActive == true)
                {
                    Console.WriteLine("You've rolled 10 times and your luck has ran out.\nWhat would you like to do?");
                    luckActive = false;
                    Start();
                }
                else if (cursedLuckActive == true)
                {
                    Console.WriteLine("You've rolled 10 times and your cursed luck has ran out.\nWhat would you like to do?");
                    cursedLuckActive = false;
                    Start();
                }
                else
                {
                    Console.WriteLine("You've rolled 10 times.\nWhat would you like to do?");
                    Start();
                }
            }

            void SaveGame()
            {
                using (StreamWriter writer = new StreamWriter("savefile.txt"))
                {
                    writer.WriteLine(cash);
                    writer.WriteLine(luckActive);

                    for (int i = 0; i < inventory.Length; i++)
                    {
                        writer.WriteLine(inventory[i]);
                    }
                }

                Console.WriteLine("Game saved!");

                Console.WriteLine("\nWhat action would you like to do?");
            }

            void LoadGame()
            {
                if (!File.Exists("savefile.txt"))
                {
                    Console.WriteLine("No save file found.");
                    return;
                }

                using (StreamReader reader = new StreamReader("savefile.txt"))
                {
                    cash = int.Parse(reader.ReadLine());
                    luckActive = bool.Parse(reader.ReadLine());

                    for (int i = 0; i < inventory.Length; i++)
                    {
                        inventory[i] = int.Parse(reader.ReadLine());
                    }
                }

                Console.WriteLine("The save file has been loaded!");

                Console.WriteLine("\nWhat action would you like to do?");
            }

            // function
            Console.WriteLine("Welcome to Sol's RNG Copy!\nWhat action would you like to do?");
            Start();
        }
    }
}
