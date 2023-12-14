using System.Text;

namespace WebApp.Utils;

public class EncryptionService
{
    /*
     * Caesar encryption
     */

    public static string EncryptCaesar(string text, int shift)
    {
        StringBuilder result = new StringBuilder();
        foreach (char ch in text)
        {
            if (char.IsLetter(ch))
            {
                char encryptedChar = (char)(ch + shift);

                if ((char.IsLower(ch) && encryptedChar > 'z') || (char.IsUpper(ch) && encryptedChar > 'Z'))
                {
                    encryptedChar = (char)(ch - (26 - shift));
                }

                result.Append(encryptedChar);
            }
            else
            {
                result.Append(ch);
            }
        }
        return result.ToString();
    }
    
    /*
     * Vigenere encryption
     */
    public static string EncryptVigenere(string text, string key)
    {
        StringBuilder result = new StringBuilder();
        int keyIndex = 0;

        foreach (char ch in text)
        {
            if (char.IsLetter(ch))
            {
                char encryptedChar = (char)(ch + key[keyIndex] - 'A');

                if ((char.IsLower(ch) && encryptedChar > 'z') || (char.IsUpper(ch) && encryptedChar > 'Z'))
                {
                    encryptedChar = (char)(ch - (26 - (key[keyIndex] - 'A')));
                }

                result.Append(encryptedChar);

                keyIndex = (keyIndex + 1) % key.Length;
            }
            else
            {
                result.Append(ch);
            }
        }

        return result.ToString();
    }
    
    
}