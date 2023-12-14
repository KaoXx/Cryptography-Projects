namespace HW03;

public class Generators
{
    /**
     * Check if a number is prime
     */
    public static bool IsPrime(long number)
    {
        if (number <= 1)
            return false;
        if (number <= 3)
            return true;

        if (number % 2 == 0 || number % 3 == 0)
            return false;

        for (long i = 5; i * i <= number; i += 6)
        {
            if (number % i == 0 || number % (i + 2) == 0)
                return false;
        }

        return true;
    }
    /**
     * Generates Random prime
     */
    public long GenerateRandomPrime(Random rand, long minValue, long maxValue)
    {
        long candidate;
        do
        {
            candidate = rand.Next((int)minValue, (int)maxValue);
        } while (!Generators.IsPrime(candidate));
        return candidate;
    }

    /**
     * Grant common divisor
     */
    public static long GCD(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
    /**
     * Generates random public key using gdc
     */
    public static long GenerateRandomPublicKey(long phi, Random random)
    {
        long publicKey;
        do
        {
            publicKey = random.Next(3, (int)phi - 1);

        } while (GCD(publicKey,phi) != 1);

        return publicKey;
    }
    /**
     * Generates random 64 bits long 
     */
    public static long GenerateRandom64BitNumber(Random rand)
    {
        byte[] buffer = new byte[8];
        rand.NextBytes(buffer);
        long random64BitNumber = BitConverter.ToInt64(buffer, 0);
        random64BitNumber = random64BitNumber & 0x7FFFFFFFFFFFFFFF;

        return random64BitNumber;
    }
    
}