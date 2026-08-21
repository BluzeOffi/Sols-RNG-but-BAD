using System;
using System.Collections;

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

            // variables

            string[] auras = {"Common", "Uncommon", "Rare", "Divinus", "Crystallized", "Magnetic", "Rage", "Aquatic"};

            Random gen = new Random();

            int sols = 0;
            int attempts = 0;
            int luck = 0;
            int rolled = 0;
            int cash = 0;
            int rolledExpensive = 0;

            string end = "no";
            string luckPotion = "no";
            string action = "a";
            string shopLine = "a";

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
                    Console.WriteLine("What do you want to do?");
                    Shop();
                }
                else
                {
                    Console.WriteLine("That's not a valid arguement.\nWhat do you want to do?");
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
                        if (cash >= 5)
                        {
                            Console.WriteLine("\nYour luck has been increased!");
                            luck = 5;

                            for (int i = 0; i < 5; i++)
                            {
                                cash--;
                            }

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
                else if (shopLine == "sell")
                {
                    if (rolled >= 1)
                    {
                        if (rolledExpensive >= 1 && rolled >= 1)
                        {
                            for (int i = 0; i < 10; i++)
                        {
                            cash++;
                        }
                        Console.WriteLine("Here is your 10 coins!\nThen what would you like to do?");
                        rolled = 0;
                        rolledExpensive = 0;
                        Start();
                        }
                        else if(rolled >= 1 && rolledExpensive <= 0)
                        {
                            for (int i = 0; i < 5; i++)
                        {
                            cash++;
                        }
                        Console.WriteLine("Here is your 5 coins!\nThen what would you like to do?");
                        rolled = 0;
                        Start();
                        }

                    }
                    else
                    {
                        Console.WriteLine("You haven't even gotten any auras.\nWhat would you like to do?");
                        Start();
                    }
                }
            }

            void Roll()
            {
                while (attempts < 11)
                {
                Console.ReadKey();

                if (luck <= 0)
                {
                    sols = gen.Next(0, 3);
                    switch (sols)
                    {
                        case 0:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("You rolled.. " + auras[sols]);
                            attempts++;
                            break;
                        case 1:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("You rolled.. " + auras[sols]);
                            attempts++;
                            break;
                        case 2:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("You rolled.. " + auras[sols]);
                            attempts++;
                            break;
                    }   
                }
                else if (luck == 5)
                {
                    sols = gen.Next(0, 6);
                    switch (sols)
                    {
                        case 0:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("You rolled.. Common!");
                            attempts++;
                            break;
                        case 1:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("You rolled.. " + auras[sols]);
                            attempts++;
                            break;
                        case 2:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("You rolled.. " + auras[sols]);
                            attempts++;
                            break;
                        case 3:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("You rolled.. " + auras[sols]);
                            attempts++;
                            break;
                        case 4:
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("You rolled.. " + auras[sols]);
                            attempts++;
                            rolledExpensive = 1;
                            break;
                        case 5:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("You rolled.. " + auras[sols]);
                            attempts++;
                            rolledExpensive = 1;
                            break;
                    } 
                }    
                }

                rolled++;
                luck = 0;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\nYou've rolled 10 times. Now what would you like to do?");
                Start();
            }


            // function
            Console.WriteLine("Welcome to Sol's RNG Copy!\nWhat action would you like to do?");
            Start();
        }
    }
}
