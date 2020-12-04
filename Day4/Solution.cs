using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Whiskee.AdventOfCode2020.Day4
{
    public class Solution
    {
        private static readonly string[] RequiredFragments = {"byr:", "iyr:", "eyr:", "hgt:", "hcl:", "ecl:", "pid:"}; 
        
        public static void Run()
        {
            string input = File.ReadAllText(@"Day4/input.txt");

            // Passports are separated by empty lines
            string[] passports = input.Split(Environment.NewLine + Environment.NewLine);

            int validPassports = passports.Count(passport => RequiredFragments.All(passport.Contains));

            Console.WriteLine($"First solution: {validPassports}");
        }

    }
}