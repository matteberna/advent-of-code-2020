﻿using System;
using Whiskee.AdventOfCode2020.CSharp;

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
                try
                {
                    Console.Write(Environment.NewLine + "AdventOfCode2020> ");
                    string[] input = Console.ReadLine()?.Split(" ");
                    
                    switch (input?[0])
                    {
                        case "exit":
                            return;
                        case "solve":
                            if (int.TryParse(input[1], out int number) && number is >= 1 and <= 25)
                            {
                                Days[number].Run();
                            }
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.Write(e.GetBaseException().Message);
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
        }
    }

    public abstract class Day
    {
        public abstract void Run();
    }
}