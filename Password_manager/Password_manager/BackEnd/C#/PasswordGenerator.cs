using System;
using System.Collections.Generic;
using System.Linq;

public static class PasswordGenerator
{
    public static string GeneratePassword(int length, int numUpperCase, int numLowerCase, int numDigits, int numSpecialChars)
    {
        const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
        const string digitChars = "0123456789";
        const string specialChars = "!@#$%^&*";

        Random random = new Random();
        List<char> password = new List<char>();

        for (int i = 0; i < numUpperCase; i++)
            password.Add(upperChars[random.Next(upperChars.Length)]);

        for (int i = 0; i < numLowerCase; i++)
            password.Add(lowerChars[random.Next(lowerChars.Length)]);

        for (int i = 0; i < numDigits; i++)
            password.Add(digitChars[random.Next(digitChars.Length)]);

        for (int i = 0; i < numSpecialChars; i++)
            password.Add(specialChars[random.Next(specialChars.Length)]);

        while (password.Count < length)
        {
            string allChars = upperChars + lowerChars + digitChars + specialChars;
            password.Add(allChars[random.Next(allChars.Length)]);
        }

        return new string(password.OrderBy(x => random.Next()).ToArray());
    }
}
