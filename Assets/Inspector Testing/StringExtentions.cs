using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringExtentions
{
    public static string Test(this string str)
    {
        string res = String.Empty;
        for (int i = 0; i < str.Length; i++)
        {
            string chr = str[i].ToString();
            res += i % 2 == 1 ? chr.ToUpper() : chr.ToLower();
        }

        return res;
    }
    
    
    public static string ToReadable(this string str)
    {
        string res = String.Empty;
        for (int i = 0; i < str.Length; i++)
        {
            char chr = str[i];
            if (i == 0)
                chr = char.ToUpper(chr);
            else if (char.IsUpper(chr))
                res += " ";
            res += chr;
        }

        return res;
    }
}
