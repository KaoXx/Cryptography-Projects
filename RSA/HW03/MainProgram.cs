namespace HW03;

public class MainProgram
{
    private static bool exitRequested = false;
    static void Main(string[] args)
    {
        // Register the Ctrl+C handler
        Console.CancelKeyPress += new ConsoleCancelEventHandler(CtrlCHandler);    
        while (!exitRequested)
        {
            Console.Clear();
            Console.WriteLine("================= RSA Menu =================");
            Console.WriteLine("1. RSA (User input)");
            Console.WriteLine("2. RSA (Long 64 bits)");
            Console.WriteLine("3. Brute force");
            Console.WriteLine("4. Exit");
            Console.Write("Select an option: ");

            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Console.WriteLine("--------------\t[!] Key exchange...  --------------");
                    RSA.initRSA();
                    break;
                case "2":
                    Console.WriteLine("--------------\t[!] Key exchange 64 bits  --------------");
                    RSA.initRSA64bits();
                    break;
                case "3":
                    Console.WriteLine("--------------\t[!] Brute Force  --------------");
                    RSA.BruteForceDecrypt();
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
        
        // Ctrl+C handler
        void CtrlCHandler(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true; // Prevent the default Ctrl+C behavior (program termination)
            Console.WriteLine("Ctrl+C pressed. Exiting gracefully...");
            exitRequested = true;
        }
        
        
    }
}