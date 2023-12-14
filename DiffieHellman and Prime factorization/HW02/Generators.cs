namespace HW02;

public class Generators {
    public static void RandomPrimeAndGenerator(out long prime, out long generator) {
        Random rand = new Random();
        while (true) {
            prime = GenerateRandomPrime(1, 100);
            generator = rand.Next(2, (int) prime - 1);

            if (IsGenerator(generator, prime))
                break;
        }
    }
    public static bool IsGenerator(long generator, long prime) {
        if (generator <= 1 || generator >= prime)
            return false;
        // Verify if generator is mod prime 
        for (long i = 2; i <= Math.Sqrt(prime - 1); i++) {
            if ((prime - 1) % i == 0) {
                if (ModPow(generator, i, prime) == 1)
                    return false;
            }
        }

        return true;
    }
    public static long GenerateRandomPrime(int min, int max) {
        Random rand = new Random();
        long candidate;

        while (true) {
            candidate = rand.Next(min, max);
            if (PrimeFactorization.isPrime(Convert.ToInt64(candidate)))
                return candidate;
        }
    }

    public static long ModPow(long baseNum, long expo, long module) {
        long res = 1;
        // Start a loop that continues until the exponent expo becomes zero
        while (expo > 0) {
            // If the current exponent is odd, multiply the result by the current base number
            if (expo % 2 == 1) {
                res = (res * baseNum) % module;
            }
            // Square the base number and take the modulo with respect to the module
            baseNum = (baseNum * baseNum) % module;
            expo /= 2;
        }

        return res;
    }
    
    public static long GenerateRandom64BitNumber(Random rand)
    {
        byte[] buffer = new byte[8];
        rand.NextBytes(buffer);
        long random64BitNumber = BitConverter.ToInt64(buffer, 0);
        random64BitNumber = random64BitNumber & 0x7FFFFFFFFFFFFFFF;

        return random64BitNumber;
    }
}