using System.Text.RegularExpressions;
using UnityEngine;

namespace Mulitplayer.NetworkUI
{
    public static class CodeFormatter
    {
        public const int CodeLength = 6;
        /// <summary>
        /// Function that formats the lobby code to a 6 alphanumeric-code without white spaces or special characters
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToFormattedCode(this string input)
        {
            var cleanedInput = Regex.Replace(input, "[^a-zA-Z0-9]", "")
                .Replace(" ", "");

            var formattedInput = cleanedInput.ToUpper();

            formattedInput = formattedInput[..Mathf.Min(cleanedInput.Length, CodeLength)];

            return formattedInput;
        }
        
    }
}