using System.Text;

namespace ConsoleApp;

public static class Cesar
{
    public static byte GetCesarShiftAmount()
    {
        int shiftAmount = 0;
        do
        {
            Console.WriteLine("Shift amount:");
            var shiftStr = Console.ReadLine();
            if (!int.TryParse(shiftStr,out shiftAmount))
            {
                Console.WriteLine("Cannot parse this into INTEGER!");
                //handle fail?
            }
            shiftAmount = int.Parse(shiftStr);
            shiftAmount %= 256;
            if (shiftAmount < 0)
            {
                shiftAmount += 256;
            }
        
        } while (shiftAmount == 0);
        return (byte)shiftAmount;
    }

    public static void DoCesar(String message)
    {
        var shiftAmount = GetCesarShiftAmount();
        var textBytes = Encoding.
            UTF8.
            GetPreamble().
            Concat(Encoding.UTF8.GetBytes(message)).ToArray();

        foreach (var textByte in textBytes)
        {
            Console.Write(textByte + " ");
        }

        for (var i = 0; i < textBytes.Count() ; i++)
        {
            textBytes[i] = (byte) ((textBytes[i] + shiftAmount) % 256);
        }
        Console.WriteLine($"Encrypted text with shift {shiftAmount}:");
        Console.WriteLine("-------------------------");
        Console.WriteLine(System.Convert.ToBase64String(textBytes));
        Console.WriteLine("-------------------------");

    }

    public static void ReverseCesar(String message) //From base64
    {
        var encryptedBytes = System.Convert.FromBase64String(message);
        var shiftAmount = GetCesarShiftAmount();
        for (var i = 0; i < encryptedBytes.Length; i++)
        {
            encryptedBytes[i] = (byte) (((encryptedBytes[i] - shiftAmount) + 256) % 256);
        }

        Console.WriteLine(Encoding.UTF8.GetString(encryptedBytes));
    }
}