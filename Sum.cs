namespace ConsoleApp1;

public static class Sum
{
    public static void GetSum()
    {
        // Exception handling for extra ordinary scenarios
        try
        {
            // Limiting to only integers
            Console.WriteLine("Enter first non zero integer n:");
            var n = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter second non zero integer q -> (q<=n):");
            var q = Convert.ToInt32(Console.ReadLine());

            // Invalid scenarios. You could separate out all of them. Depends on the requirement.
            if (n > 0 && q > 0 && q <= n)
            {
                decimal sum = 0;
                for (var i = q; i <= n; i += q)
                {
                    sum += i;
                }

                Console.WriteLine("Sum: {0}", sum);
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