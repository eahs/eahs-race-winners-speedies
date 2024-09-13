using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;

namespace RaceWinners;

public class Program
{
    static async Task Main(string[] args)
    {
        DataService ds = new DataService();

        // Asynchronously retrieve the group (class) data
        var data = await ds.GetGroupRanksAsync();

        for (int i = 0; i < data.Count; i++)// Combine the ranks to print as a list
        {
            var ranks = String.Join(", ", data[i].Ranks);
            
            Console.WriteLine($"{data[i].Name} - [{ranks}]");
        }

        Console.WriteLine("\n Averages of First 7 ranks for each Class:\n");
        var averages = new double[data.Count]; //double array to store averages of each class
        
        List<string> names = new List<string>();  // Declare the list outside the loop
        for (int i = 0; i < data.Count; i++)// Adds up, divides, and displays the first 7(or less) indexes in each class 
        {
            names.Add(data[i].Name);  // Add the team name to the list inside the loop
            double average = 0;
            int rankscount = data[i].Ranks.Count;
            if (rankscount > 7)
            {
            for (int j = 0; j < 7; j++)
                average += data[i].Ranks[j];
            average = average / 7;
            }
            else
            {
                for (int j = 0; j < rankscount; j++)
                    average += data[i].Ranks[j];
                average = average / rankscount;
            }
            Console.WriteLine($"{data[i].Name} - {average}");
            averages[i] = average; //stores average in double array
        }

        for (int i = 0; i < averages.Length; i++)// bubble sorts both arrays, names being attached to averages, which are sorted low to high
        {
            for (int j = 0; j < averages.Length - i - 1; j++)
            {
                if (averages[j] > averages[j + 1])
                {
                    // Swap averages
                    (averages[j + 1], averages[j]) = (averages[j], averages[j + 1]);
                    // Swap corresponding names
                    (names[j + 1], names[j]) = (names[j], names[j + 1]);
                }
            }
        }

        Console.WriteLine("\n Winners and Averages:\n");

        for (int i = 0; i < 4; i++)
        {
            Console.WriteLine($"{i + 1}. {names[i]} - {averages[i]}");
        }
        //All done :D
    }
}