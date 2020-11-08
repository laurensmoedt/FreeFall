using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class SecureHelper
{
    public static string EncryptDecrypt(string data, int key)
    {
        StringBuilder input = new StringBuilder(data);
        StringBuilder output = new StringBuilder(data.Length);

        char character;

        for(int i = 0; i < data.Length; i++)
        {
            character = input[i];
            character = (char)(character ^ key);
            output.Append(character);
        }

        return output.ToString();
    }
}
