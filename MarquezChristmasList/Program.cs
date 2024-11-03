using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using MarquezChristmasList.Models;

namespace MarquezChristmasList
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            using (var reader = new StreamReader("ChristmasList.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var people = csv.GetRecords<ChristmasListEntry>().ToList();
                List<Tuple<string, string>> giftTo;
                do
                {
                    giftTo = new List<Tuple<string, string>>();
                    var rand = new Random();
                    foreach (var person in people)
                    {
                        var givenTo = giftTo.FirstOrDefault(t => t.Item2 == person.Name);
                        var givenToHistory = person.GiftTo.Where(g => !string.IsNullOrEmpty(g)).ToHashSet();
                        var possibleEntries = people
                            .Where(r => r != person && r.Family != person.Family) //cannot give to family
                            .Where(r => givenTo == null || givenTo.Item1 != r.Name) //cannot give to each other
                            .Where(r => !givenToHistory.Contains(r.Name)) //cannot give to anyone who they've given to
                            .ToList();
                        //weight entries based on if you have given to that person
                        var weightedEntries = new List<string>();
                        foreach (var e in possibleEntries)
                        {
                            int weight = 0;
                            int idx = person.GiftTo.IndexOf(e.Name);

                            if (idx < 0) weight = 15;
                            else weight = Math.Min(idx * 5, 10);
                            weightedEntries.AddRange((new string[weight]).Select((_x) => e.Name));
                        }

                        var giftToIdx = rand.Next(weightedEntries.Count);
                        giftTo.Add(Tuple.Create(person.Name, weightedEntries[giftToIdx]));
                    }
                }
                while (giftTo.Select(g => g.Item2).Distinct().Count() != people.Count);

                foreach (var g in giftTo)
                {
                    Console.WriteLine($"{g.Item1} -> {g.Item2}");
                }
            }
        }
    }
}
