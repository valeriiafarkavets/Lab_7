using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Lab_7.Models;
namespace Lab_7.Services{
    public class DateParserService {
        private string[] months = {
            "January", "February", "March", "April",
            "May", "June", "July", "August",
            "September", "October", "November", "December"
        };
        private string monthPattern =
            @"(?:January|February|March|April|May|June|July|August|September|October|November|December|Jan|Feb|Mar|Apr|Jun|Jul|Aug|Sep|Sept|Oct|Nov|Dec)";
        public List<DateResult> ParseText(string text){
            List<DateResult> results = new List<DateResult>();
            string[] lines = text.Split('\n');
            for (int i = 0; i < lines.Length; i++){
                string line = lines[i].Trim();
                if (line == "")
                    continue;
                results.Add(ParseLine(line, results.Count + 1));
            }
            return results;
        }
        public DateResult ParseLine(string text, int number){
            DateResult result = new DateResult();
            result.Number = number;
            result.OriginalText = text;
            string separator = @"[\s.,_-]+";
            string pattern1 =
                @"(?<![A-Za-z0-9])(?<day>\d{1,2})" +
                separator + @"(?<month>" + monthPattern + @")(?![A-Za-z])";
            string pattern2 =
                @"(?<![A-Za-z])(?<month>" + monthPattern + @")" +
                separator + @"(?<day>\d{1,2})(?!\d)";
            RegexOptions options =
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant;
            Match match = Regex.Match(text, pattern1, options);
            if (!match.Success)
                match = Regex.Match(text, pattern2, options);
            if (!match.Success)
                return result;
            string dayText = match.Groups["day"].Value;
            string monthText = match.Groups["month"].Value;
            if (!int.TryParse(dayText, out int day))
                return result;
            int month = GetMonthNumber(monthText);
            if (month == 0)
                return result;
            string dateText = day.ToString("D2") + "-" +
                              month.ToString("D2") + "-2025";
            bool valid = DateOnly.TryParseExact( dateText, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly date);
            if (!valid)
                return result;
            string normalized = Regex.Replace( match.Value, @"^.*$", m => date.ToString("dd-MMM", CultureInfo.InvariantCulture));
            result.Day = day.ToString();
            result.Month = months[month - 1];
            result.NormalizedDate = normalized;
            result.Status = "Розпізнано";
            result.IsRecognized = true;
            return result;
        }
        private int GetMonthNumber(string name){
            for (int i = 0; i < months.Length; i++) {
                if (name.Equals(months[i], StringComparison.OrdinalIgnoreCase))
                    return i + 1;
                string shortName = months[i].Substring(0, 3);
                if (name.Equals(shortName, StringComparison.OrdinalIgnoreCase))
                    return i + 1;
                if (i == 8 && name.Equals("Sept", StringComparison.OrdinalIgnoreCase))
                    return 9;
            }
            return 0;
        }
    }
}
