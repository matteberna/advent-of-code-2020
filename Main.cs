using System;
using System.Diagnostics;
using System.IO;
using Whiskee.AdventOfCode2020.Solutions;

namespace Whiskee.AdventOfCode2020
{
    internal class Launcher
    {
        private static readonly Stopwatch Stopwatch = new();
        public static Day[] Days;
        
        private static void Main()
        {
            AllocateDays();
            
            // Immediately print the answers for the current day
            for (int i = 25; i >= 1; i--)
            {
                if (Days[i] != null)
                {
                    Console.WriteLine(Environment.NewLine + $"AdventOfCode2020> solve {i}");
                    WriteSolutions(i);
                    break;
                }
            }

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
                            WriteSolutions(number);
                        }

                        break;
                }
            }
        }

        private static void WriteSolutions(int number)
        {
            Console.WriteLine($"https://adventofcode.com/2020/day/{number}");
            Stopwatch.Restart();
            
            string filename = $"data/day{number:D2}.txt";
            Days[number].ReadInput(File.ReadAllText(filename));
            Console.WriteLine($"{GetTimestamp()} | File /{filename} parsed ({new FileInfo(filename).Length} bytes)");
            
            var solution = Days[number].SolveFirst();
            Console.WriteLine($"{GetTimestamp()} | First solution computed: {solution}");
            
            solution = Days[number].SolveSecond();
            Console.WriteLine($"{GetTimestamp()} | Second solution computed: {solution}");
        }

        private static string GetTimestamp()
        {
            return TimeSpan.FromTicks(Stopwatch.ElapsedTicks).ToString().Substring(3);
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
            Days[8] = new Day8();
            Days[9] = new Day9();
            Days[10] = new Day10();
            Days[11] = new Day11();
            Days[12] = new Day12();
            Days[13] = new Day13();
            Days[14] = new Day14();
            Days[15] = new Day15();
            Days[16] = new Day16();
            Days[17] = new Day17();
            Days[18] = new Day18();
        }
    }

    public abstract class Day
    {
        public abstract void ReadInput(string content);
        public abstract object SolveFirst();
        public abstract object SolveSecond();
    }
}