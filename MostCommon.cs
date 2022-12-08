using System.Text.RegularExpressions;

namespace ConsoleApp1;

public static class MostCommon
{
    private static Dictionary<string, int> GetInput()
    {
        Console.WriteLine("Enter words and hit enter (spaces will be trimmed. Hit enter twice to end):");

        // create dictionary 
        var dictWords = new Dictionary<string, int>();
        
        string? input;
        while (!string.IsNullOrEmpty(input = Console.ReadLine()))
        {
            if (string.IsNullOrWhiteSpace(input)) continue;
            input = Regex.Replace(input, @"\s+", "").ToLower();

            // add word to dictionary if not exists
            if (!dictWords.ContainsKey(input))
            {
                dictWords.Add(input, 1);
            }
            else // increment count of word if exists
            {
                dictWords[input] = ++dictWords[input];
            }
        }

        return dictWords;
    }
    
    public static void GetMostCommon()
    {
        // Exception handling for extra ordinary scenarios
        try
        {
            // Get input
            var dictWords = GetInput();
            
            if (dictWords.Count > 0)
            {
                var maxCount = dictWords.Select(x => x.Value).ToList().Max();
                if (maxCount > 1)
                {
                    var kvps = dictWords.Where(x => x.Value == maxCount);
                    var mostCommonWords = kvps.Select(x => x.Key);
                    
                    //Most common words that have come
                    Console.WriteLine("Most common words: ");
                    foreach (var mostCommonWord in mostCommonWords)
                    {
                        Console.WriteLine("{0}", mostCommonWord);
                    }
                }
                else
                {
                    Console.WriteLine("None of the words came more than once.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}