using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Whiskee.AdventOfCode2020.Solutions
{
    public class Day4 : Day
    {
        private string[] _passports;
        
        private readonly Regex _hexRegex = new(@"[0-9a-f]{6}");
        private readonly string[] _eyeColors = {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
        
        public override void ReadInput(string content)
        {
            // Passports are separated by empty lines
            _passports = content.SplitParagraphs();
            
            // Discard passports with missing fields, we don't need them for the second part
            string[] requiredFields = {"byr:", "iyr:", "eyr:", "hgt:", "hcl:", "ecl:", "pid:"}; 
            _passports = _passports.Where(passport => requiredFields.All(passport.Contains)).ToArray();
        }

        public override object SolveFirst()
        {
            return _passports.Length;
        }

        public override object SolveSecond()
        {
            int validPassports = 0;
            foreach (string passport in _passports)
            {
                // Fragments (key:value) are separated by a single whitespace or newline character
                string[] fragments = passport.Replace(Environment.NewLine, " ").Split(" ");
                
                bool isValid = true;
                foreach (string fragment in fragments)
                {
                    string key = fragment.Substring(0, 3);
                    string value = fragment.Substring(fragment.IndexOf(":", StringComparison.Ordinal) + 1);
                    if (!ValidatePair(key, value))
                    {
                        isValid = false;
                    }
                }
                if (isValid)
                {
                    validPassports++;
                }
            }

            return validPassports;
        }

        private bool ValidatePair(string key, string value)
        {
            switch (key)
            {
                // Birth year: 1920-2002
                case "byr":
                    return int.Parse(value) is >= 1920 and <= 2002;
                // Issue year: 2010-2020
                case "iyr":
                    return int.Parse(value) is >= 2010 and <= 2020;
                // Expiration year: 2020-2030
                case "eyr":
                    return int.Parse(value) is >= 2020 and <= 2030;
                // Height
                case "hgt":
                    // 150-193 (cm)
                    if (value.EndsWith("cm"))
                    {
                        int? height = value.FindFirstInteger();
                        return height >= 150 && height <= 193;
                    }
                    // 59-76 (in)
                    else if (value.EndsWith("in"))
                    {
                        int? height = value.FindFirstInteger();
                        return height >= 59 && height <= 76;
                    }
                    return false;
                // Hair color: 6-char hexadecimal
                case "hcl":
                    return value[0] == '#' && _hexRegex.Matches(value.Substring(1)).Count == 1;
                // Eye color: allowed 3-char values only
                case "ecl":
                    return value.Length == 3 && _eyeColors.Contains(value);
                // Passport ID: exactly 9 digits
                case "pid":
                    return value.Length == 9 && int.TryParse(value, out _);
                // Country ID: optional
                case "cid":
                    return true;
                // Unrecognized, but we shouldn't invalidate the passport because of this
                default:
                    return true;
            }
        }
    }
}