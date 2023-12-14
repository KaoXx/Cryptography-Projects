namespace HW02;

public class PrimeFactorization
{
    public static bool isPrime(long num)
    {
        if (num <= 1) return false;
        if (num <= 3) return true;
        if (num % 2 == 0 || num % 3 == 0) return false;

        for (long i = 5; i * i <= num; i += 6)
        {
            if (num % i == 0 || num % (i + 2) == 0)
                return false;
        }

        return true;
    }
    
    // Simple prime factorization
    public static long FindPrimeFactor(long num)
    {
        for (long i = 2; i <= num; i++)
        {
            if (num % i == 0 && isPrime(i))
            {
                return i;
            }
        }
        return 1; // If no prime factors found, return 1 (not recommended)
    }
    
    /*
     * PollardsRho Implemetation
     */
    static long PollardsRho(long n)
    {
        Random rand = new Random();
        int intN = (int)n; // Convert n to int

        long x = rand.Next(1, intN);
        long y = x;
        long c = rand.Next(1, intN);
        long d = 1;

        while (d == 1)
        {
            x = (x * x + c) % n;
            y = (y * y + c) % n;
            y = (y * y + c) % n;
            d = GCD(Math.Abs(x - y), n);
        }
        return d;
    }

    static long GCD(long a, long b) //Greatest common divisor
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
    
    public static void FindPrimeFactorsv2(long n)
    {
        if (n <= 1)
        {
            Console.WriteLine("No prime factors.");
            return;
        }

        if (isPrime(n))
        {
            Console.WriteLine(n);
            return;
        }

        long factor = PollardsRho(n);
        if (factor == n)
        {
            Console.WriteLine("[!] No prime factors found. Was not the product of two primes");
            return;
        }

        FindPrimeFactorsv2(factor);
        FindPrimeFactorsv2(n / factor);
    }

    public static void SimpleFactorization()
    {
        Console.WriteLine("Enter the product of two prime numbers: ");
        var productInput = Console.ReadLine();
        //Product of two prime numbers
        long productchecked;
        if (long.TryParse(productInput, out productchecked))
        {
            // Find the prime factors
            long factor1 = FindPrimeFactor(productchecked);
            long factor2 = productchecked / factor1;
            Console.WriteLine("Factor 1: " + factor1);
            Console.WriteLine("Factor 2: " + factor2);
            Thread.Sleep(5000);
            
        }
        else
        {
            Console.WriteLine("[!] The input MUST be a number...");
            Thread.Sleep(4000);
        }

    }

    public static void PollardsRhoFactorization()
    {
        Console.Write("Enter a positive integer to factorize: ");
        var productInput = Console.ReadLine();
        //Product of two prime numbers
        long productchecked;
        if (long.TryParse(productInput, out productchecked))
        {
            // Find the prime factors
            Console.WriteLine("Prime factors of " + productchecked + ":");
            FindPrimeFactorsv2(productchecked);
            Thread.Sleep(5000);
            
        }


    }
    
    
}