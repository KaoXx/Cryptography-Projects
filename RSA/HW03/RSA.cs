using System;
using System.Diagnostics;
using System.Numerics;

namespace HW03
{
    public class RSA
    {
        /*
         * RSA user input
         */
        public static void initRSA()
        {
            Random rand = new Random();
            long p, q, n, phi, publicKey, privateKey;
            var userelection = false;
            p = GenerateRandomPrime(rand);
            q = GenerateRandomPrime(rand);

            n = p * q;
            phi = (p - 1) * (q - 1);

            publicKey = GenerateRandomPublicKey(rand, phi);
            privateKey = ModInverse(publicKey, phi);
            
            Console.Write("Enter the message to encrypt: ");
            string message = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(message))
            {
                long[] encryptedMessage = Encrypt(message, publicKey, n);
                Console.WriteLine("Encrypted Message: " + string.Join(" ", encryptedMessage));

                string decryptedMessage = Decrypt(encryptedMessage, privateKey, n);
                Console.WriteLine("Decrypted Message: " + decryptedMessage);
                Thread.Sleep(7000);  
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a non-empty message.");

            }
        }
        /*
         * RSA Longint 64 bits
         * Generates random 64 bits long
         */
        public static void initRSA64bits()
        {
            Random rand = new Random();
            long p, q, n, phi, publicKey, privateKey;
            var userelection = false;
            p = GenerateRandomPrime(rand);
            q = GenerateRandomPrime(rand);

            n = p * q;
            phi = (p - 1) * (q - 1);

            publicKey = GenerateRandomPublicKey(rand, phi);
            privateKey = ModInverse(publicKey, phi);
            var message = Generators.GenerateRandom64BitNumber(rand);
            Console.WriteLine("64 bits long: " + message);

            long[] encryptedMessage = Encrypt(message.ToString(), publicKey, n);
            Console.WriteLine("Encrypted Message: " + string.Join(" ", encryptedMessage));

            string decryptedMessage = Decrypt(encryptedMessage, privateKey, n);
            Console.WriteLine("Decrypted Message: " + decryptedMessage);
            Thread.Sleep(7000);

        }
        /*
         * Bruteforce algorithm with hardcoded prime numbers
         * Time of bruteforce depends of the modulus(n)
         * Checks if takes more than 5 minutes
         */
        public static void BruteForceDecrypt()
        {
            long p = 40277       ;  // Prime number p
            long q = 40283       ;  // Prime number q
            long n = p * q;  // Modulus (n)
            long publicKey = 65537;  // 64 bits public key

            // Small cipher
            long encryptedNumber = 9223372036854775;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            long decryptedNumber = BruteForceDecryptSingleNumber(encryptedNumber, publicKey, n);
            stopwatch.Stop();
            long elapsedTime = stopwatch.ElapsedMilliseconds;
            Console.WriteLine("Decryption Time: " + elapsedTime + " ms");
            if (elapsedTime < 5 * 60 * 1000) // 5 minutes in milliseconds
            {
                Console.WriteLine("Decryption completed in less than 5 minutes.");
            }
            else
            {
                Console.WriteLine("Decryption took longer than 5 minutes.");
            }

            Console.WriteLine("Original message: " + decryptedNumber);
            Thread.Sleep(7000);
            
        }

        /*
         * Bruteforce function to decrypt a single number
         */

        public static long BruteForceDecryptSingleNumber(long encryptedNumber, long publicKey, long n)
        {
            long decryptedNumber = -1;
            long minDifference = long.MaxValue;

            for (long i = 0; i < n; i++)
            {
                long candidate = ModPow(i, publicKey, n);
                long difference = Math.Abs(encryptedNumber - candidate);

                if (difference < minDifference)
                {
                    minDifference = difference;
                    decryptedNumber = i;
                }
            }

            return decryptedNumber;
        }

        /*
         * Random prime generator 
         */
        
        static long GenerateRandomPrime(Random rand)
        {
            long candidate;
            do
            {
                candidate = rand.Next(100, 1000); // You can adjust the range for larger primes
            } while (!Generators.IsPrime(candidate));
            return candidate;
        }
        /*
         * Random public key generator
         */
        static long GenerateRandomPublicKey(Random rand, long phi)
        {
            long candidate;
            do
            {
                candidate = rand.Next(2, (int)phi);
            } while (Generators.GCD(candidate, phi) != 1);
            return candidate;
        }
        
        /*
         * For testing (debug)
         */
        static void GenerateKeys(out long p, out long q, out long n, out long phi, out long publicKey, out long privateKey)
        {
            p = 61;
            q = 53;
            n = p * q;
            phi = (p - 1) * (q - 1);
            publicKey = 17; // A common choice for the public exponent.
            privateKey = ModInverse(publicKey, phi);
        }


        /*
         * Encryption function (Modpow)
         */
        static long[] Encrypt(string plaintext, long e, long n)
        {
            long[] encrypted = new long[plaintext.Length];
            for (int i = 0; i < plaintext.Length; i++)
            {
                encrypted[i] = ModPow(plaintext[i], e, n);
            }
            return encrypted;
        }
        /*
         * Modpow inverse
         */
        static long ModInverse(long a, long m)
        {
            long m0 = m;
            long x0 = 0;
            long x1 = 1;

            while (a > 1)
            {
                long q = a / m;
                long t = m;
                m = a % m;
                a = t;
                t = x0;
                x0 = x1 - q * x0;
                x1 = t;
            }

            if (x1 < 0)
                x1 += m0;

            return x1;
        }
        /*
         * Decrypt algorithm (Modpow)
         */
        static string Decrypt(long[] encrypted, long d, long n)
        {
            char[] decrypted = new char[encrypted.Length];
            for (int i = 0; i < encrypted.Length; i++)
            {
                decrypted[i] = (char)ModPow(encrypted[i], d, n);
            }
            return new string(decrypted);
        }

        /*
         * Modpow implementation
         */
        static long ModPow(long baseValue, long exponent, long modulus)
        {
            if (modulus <= 0)
                throw new ArgumentException("Modulus must be a positive number.");
    
            long result = 1;
            baseValue = baseValue % modulus; // Asegurarse de que la base esté dentro del módulo

            while (exponent > 0)
            {
                if (exponent % 2 == 1)
                    result = (result * baseValue) % modulus;

                exponent >>= 1;
                baseValue = (baseValue * baseValue) % modulus;
            }

            return result;
        }
    }
}
