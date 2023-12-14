// See https://aka.ms/new-console-template for more information

using System.Reflection;

namespace HW02
{
    public class DeffieHellman
    {
        private static bool exitRequested = false;

        static void Main() {
            
            
            
            // Register the Ctrl+C handler
            Console.CancelKeyPress += new ConsoleCancelEventHandler(CtrlCHandler);    
         while (!exitRequested)
         {
             Console.Clear();
             Console.WriteLine("================= Deffie Hellman menu =================");
             Console.WriteLine("1. Calculate shared secret");
             Console.WriteLine("2. Prime factorization v1 (Simple prime factorization)");
             Console.WriteLine("3. Prime factorization v2 (Pollard's Rho method)");
             Console.WriteLine("4. Exit");
             Console.Write("Select an option: ");

             string userInput = Console.ReadLine();

             switch (userInput)
             {
                 case "1":
                     Console.WriteLine("--------------\t[!] Key exchange...  --------------");
                     DeffieExchange();
                     break;
                 case "2":
                     Console.WriteLine("--------------\t[!] Prime factorization V.1  --------------");
                     PrimeFactorization.SimpleFactorization();
                     break;
                 case "3":
                     Console.WriteLine("--------------\t[!] Prime factorization V.2  --------------"); 
                     PrimeFactorization.PollardsRhoFactorization();
                     break;
                 case "4":
                     Console.WriteLine("\tExiting the program.");
                     exitRequested = true;
                     break;
                 default:
                     Console.WriteLine("\tOption not valid. Press ENTER to continue.");
                     Console.ReadLine();
                     break;
             }
         }
    
        }

        static void DeffieExchange()
        {
            long p, g;
            //Choose prime numbers p and g (Public keys) prime and generator
            Generators.RandomPrimeAndGenerator(out p, out g);
            Console.WriteLine("Chosen p: " + p);
            Console.WriteLine("Chosen g: " + g);
            //long p = 23;
            //long g = 5; //Primitive root modulo p
            //Alice's private key (randomly)
            Random rand = new Random();
            var a = Generators.GenerateRandom64BitNumber(rand);
            Console.WriteLine("Alice's private key: " + a );
            //long a = 6;
            //Bob's private key (randomly)
            var b = Generators.GenerateRandom64BitNumber(rand);
            Console.WriteLine("Bob's private key : " + b);
            //long b = 15;
            //Calculate public keys
            long A = Generators.ModPow(g, a, p); //Alice
            long B = Generators.ModPow(g, b, p); //Bob
         
            //Shared secret (Keys Exchange)

            long sharedA = Generators.ModPow(B, a, p);
            long sharedB = Generators.ModPow(A, b, p);
         
            Console.WriteLine("Alice's shared secret: " + sharedA);
            Console.WriteLine("Bob's shared secret: " + sharedB);
            Thread.Sleep(7000);
        }
        // Ctrl+C handler
        private static void CtrlCHandler(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true; // Prevent the default Ctrl+C behavior (program termination)
            Console.WriteLine("Ctrl+C pressed. Exiting gracefully...");
            exitRequested = true;
        }
    }
    
}
