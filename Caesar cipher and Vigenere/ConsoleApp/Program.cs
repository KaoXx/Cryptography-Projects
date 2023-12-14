// See https://aka.ms/new-console-template for more information

using System.Text;
using ConsoleApp;

class Program
{
    private static bool isRunning = true; // Flag to control program execution.
    
    private static void CancelHandler(object sender, ConsoleCancelEventArgs e)
    {
        // Handle Ctrl+C interruption.
        Console.WriteLine("Ctrl+C pressed. Exiting gracefully...");
        isRunning = false; // Set the flag to stop the program.
    }
    
    static void Main(string[] args)
    {
        // Subscribe to the Ctrl+C event.
        Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelHandler);
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("[!] Choose an option:");
            Console.WriteLine("C - Cesar Cypher");
            Console.WriteLine("V - Vigenère Cypher");
            Console.WriteLine("X - Exit");

            string choice = Console.ReadLine().ToUpper();

            switch (choice)
            {
                case "C":
                    Console.WriteLine("Select an option:");
                    Console.WriteLine("1 - Encrypt a Message");
                    Console.WriteLine("2 - Decrypt a Message");
                    string cipherChoice = Console.ReadLine();
                    if (cipherChoice == "1")
                    {
                        // Cesar Cypher logic
                        Console.WriteLine("Text to encrypt:");
                        var messageToEncrypt = Console.ReadLine();
                        Cesar.DoCesar(messageToEncrypt);
                    }
                    else if (cipherChoice == "2")
                    {
                        // Cesar Cypher logic
                        Console.WriteLine("Text to decrypt (Base64):");
                        var messageToDecrypt = Console.ReadLine();
                        Console.WriteLine("Plaintext message:");
                        Cesar.ReverseCesar(messageToDecrypt);
                        
                    }
                    break;
                case "V":
                    Console.WriteLine("Select an option:");
                    Console.WriteLine("1 - Encrypt a message");
                    Console.WriteLine("2 - Decrypt a message");
                    string vigenereChoice = Console.ReadLine();
                    if (vigenereChoice == "1")
                    {
                        // Vigenere Cypher logic
                        Console.WriteLine("Text to encrypt:");
                        string messageToEncrypt = Console.ReadLine().ToUpper();
                        Console.WriteLine("Enter the secret Key:");
                        string secretKey = Console.ReadLine().ToUpper();
                        Console.WriteLine(Vigenere.EncryptVigenere(messageToEncrypt,secretKey));
                    }
                    else if (vigenereChoice == "2")
                    {
                        // Vigenere Cypher logic
                        Console.WriteLine("Text to decrypt (base64): ");
                        string messageToDecrypt = Console.ReadLine();
                        Console.WriteLine("Enter the secret Key:");
                        string secretKey = Console.ReadLine().ToUpper();
                        byte[] decryptedBytes = Vigenere.DecryptVigenere(messageToDecrypt, secretKey);
                        string decryptedMessage = Encoding.UTF8.GetString(decryptedBytes);
                        Console.WriteLine("Decrypted message:");
                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine(decryptedMessage);
                        Console.WriteLine("--------------------------------------------------");
                    }
                    break;
                case "X":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("[!] Not a valid option. Please, select a valid one.");
                    break;
            }
        }
    }
}


















