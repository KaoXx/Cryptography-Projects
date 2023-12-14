using System.Text;

namespace ConsoleApp;

public class Vigenere
{
    // Encrypts a message using the Vigenere cipher and returns the result encoded in Base64.
    public static string EncryptVigenere(string message, string key)
    {
        // Convert the message and the key to bytes.
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);

        // Encrypt the message using the Vigenere cipher.
        byte[] encryptedBytes = EncryptVigenereBytes(messageBytes, keyBytes);
        foreach (var textByte in encryptedBytes)
        {
            Console.Write(textByte + " ");
        }
        Console.WriteLine("Encoded base64:");
        // Encode the result in Base64.
        return Convert.ToBase64String(encryptedBytes);
    }

    // Decrypts a Base64-encoded string using the Vigenere cipher and returns the decrypted byte array.
    public static byte[] DecryptVigenere(string encryptedBase64, string key)
    {
        // Decode the Base64-encoded string to bytes.
        byte[] encryptedBytes = Convert.FromBase64String(encryptedBase64);
        
        // Convert the key to bytes.
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);

        // Decrypt the bytes using the Vigenere cipher.
        return DecryptVigenereBytes(encryptedBytes, keyBytes);
    }

    // Encrypts the bytes using the Vigenere cipher with the provided keyBytes.
    private static byte[] EncryptVigenereBytes(byte[] data, byte[] keyBytes)
    {
        byte[] encryptedBytes = new byte[data.Length];
        for (int i = 0, j = 0; i < data.Length; i++)
        {
            byte b = data[i];
            if (char.IsLetter((char)b))
            {
                // Calculate the shift amount based on the key.
                int shift = keyBytes[j % keyBytes.Length] - 'A';
                byte encryptedByte = (byte)(((b - 'A' + shift) % 26) + 'A');
                encryptedBytes[i] = encryptedByte;
                j++;
            }
            else
            {
                // Keep non-alphabet characters as they are.
                encryptedBytes[i] = b;
            }
        }
        return encryptedBytes;
    }

    // Decrypts the bytes using the Vigenere cipher with the provided keyBytes.
    private static byte[] DecryptVigenereBytes(byte[] encryptedBytes, byte[] keyBytes)
    {
        byte[] decryptedBytes = new byte[encryptedBytes.Length];
        for (int i = 0, j = 0; i < encryptedBytes.Length; i++)
        {
            byte b = encryptedBytes[i];
            if (char.IsLetter((char)b))
            {
                // Calculate the shift amount based on the key.
                int shift = keyBytes[j % keyBytes.Length] - 'A';
                byte decryptedByte = (byte)(((b - 'A' - shift + 26) % 26) + 'A');
                decryptedBytes[i] = decryptedByte;
                j++;
            }
            else
            {
                // Keep non-alphabet characters as they are.
                decryptedBytes[i] = b;
            }
        }
        return decryptedBytes;
    }
    
    
}