using System.Text.RegularExpressions;

namespace ConsoleApp1;

public static class FromTo
{
    public static void RemoveTags()
    {
        // The regex is copied as is from the web. Didn't get time to test for all scenarios. Only the one mentioned in doc.
        // Could be handled on case by case basis
        const string reggie = "<.*?>"; // Look for < till >, '.' for all text, '*' multiple instances, '?' for non-greedy 
        
        var strInput = GetInput("Enter the string: ");
        if (string.IsNullOrWhiteSpace(strInput)) return;
        
        var strStart = GetInput("Enter starting tag: ");
        if (string.IsNullOrWhiteSpace(strStart)) return;
        
        var strEnd = GetInput("Enter ending tag: ");
        if (string.IsNullOrWhiteSpace(strEnd)) return;

        Console.WriteLine("Result: {0}", Regex.Replace 
            (strInput, reggie, string.Empty));
    }

    private static string GetInput(string msg)
    {
        Console.WriteLine(msg);
        var strInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(strInput))
        {
            Console.WriteLine("Invalid input");
        }
        return strInput;
    }
}