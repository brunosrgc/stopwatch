using System;
using System.Threading;

namespace Stopwatch
{
    class Program
    {
        const int PRE_START_WAIT = 1000;
        const int START_DELAY = 2500;
        const int MENU_DELAY = 2000;

        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("<------------------------------->");
            Console.WriteLine("S = Seconds => 10s");
            Console.WriteLine("M = Minutes => 1m");
            Console.WriteLine("E = exit");
            Console.WriteLine("<------------------------------->");

            Console.WriteLine("");
            Console.Write("How long do you want to count: ");
            string input = Console.ReadLine().ToLower();
            char type;
            int time;
            bool validInput = ParseInput(input, out type, out time);

            if (!validInput)
            {
                Console.WriteLine("Invalid input. Please try again.");
                Thread.Sleep(MENU_DELAY);
                Menu();
                return;
            }

            if (time == 0)
            {
                Console.Clear();
                Console.WriteLine("Exiting program...");
                Thread.Sleep(MENU_DELAY);
                Environment.Exit(0);
            }

            PreStart(time, type);
        }

        static bool ParseInput(string input, out char type, out int time)
        {
            type = ' ';
            time = 0;

            if (string.IsNullOrEmpty(input) || !char.TryParse(input.Substring(input.Length - 1, 1), out type))
                return false;

            if (type == 'e')
                return true;

            if (!int.TryParse(input.Substring(0, input.Length - 1), out time))
                return false;

            if (type == 'm') // Convertendo minutos para segundos
                time *= 60;

            return true;
        }

        static void PreStart(int time, char type)
        {
            Console.Clear();
            Console.WriteLine("Ready...");
            Thread.Sleep(PRE_START_WAIT);
            Console.WriteLine("Set...");
            Thread.Sleep(PRE_START_WAIT);
            Console.WriteLine("Go...");
            Thread.Sleep(START_DELAY);
            Start(time, type);
        }

        static void Start(int time, char type)
        {
            int currentTime = 0;
            while (currentTime < time)
            {
                Console.Clear();
                Console.WriteLine("Current Time: " + currentTime);
                Console.WriteLine("Press any key to stop.");
                if (Console.KeyAvailable)
                    break;

                currentTime++;
                Thread.Sleep(1000);
            }

            Console.Clear();
            Console.WriteLine("Finished stopwatch! =D");
            Thread.Sleep(START_DELAY);

            Console.WriteLine("Press 'R' to restart or any other key to return to the menu.");
            char keyPressed = Console.ReadKey().KeyChar;
            if (char.ToLower(keyPressed) == 'r')
                Menu();
            else
            {
                Console.Clear();
                Console.WriteLine("Returning to menu...");
                Thread.Sleep(2000);
                Menu();
            }
        }
    }
}