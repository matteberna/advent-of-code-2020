using System;
using System.IO;

namespace Whiskee.AdventOfCode2020
{
    internal class Launcher
    {
        public static Day[] Days;
        
        private static void Main()
        {
            AllocateDays();

            while (true)
            {
                Console.Write(Environment.NewLine + "AdventOfCode2020> ");
                string[] command = Console.ReadLine()?.Split(" ");

                switch (command?[0])
                {
                    case "exit":
                        return;
                    case "solve":
                        if (int.TryParse(command[1], out int number) && number is >= 1 and <= 25)
                        {
                            string input = File.ReadAllText($"data/day{number}.txt");
                            Days[number].ReadInput(input);
                            Console.WriteLine($"First part: {Days[number].SolveFirst()}");
                            Console.WriteLine($"Second part: {Days[number].SolveSecond()}");
                        }

                        break;
                }
            }
        }

        private static void AllocateDays()
        {
            Days = new Day[25 + 1];
            Days[1] = new Day1();
            Days[2] = new Day2();
            Days[3] = new Day3();
            Days[4] = new Day4();
            Days[5] = new Day5();
            Days[6] = new Day6();
            Days[7] = new Day7();
        }
    }

    public abstract class Day
    {
        public abstract void ReadInput(string content);
        public abstract object SolveFirst();
        public abstract object SolveSecond();
    }
}