using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetToolRAR.Models
{
    public class Utilities
    {
        /// <summary>
        /// Generates the random alpha numeric code (Default 6 chracters)
        /// </summary>
        /// <returns>The Alphanumeric code</returns>
        public static string GenerateSecretCode()
        {
            return GenerateSecretCode(6);
        }

        /// <summary>
        /// Generates the random alpha numeric code.
        /// </summary>
        /// <param name="length">The length of the code needed.</param>
        /// <returns>The Alphanumeric code</returns>
        public static string GenerateSecretCode(int length)
        {
            string characterSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random random = new Random();

            //The below code will select the random characters from the set
            //and then the array of these characters are passed to string 
            //constructor to make an alphanumeric string
            string randomCode = new string(
                Enumerable.Repeat(characterSet, length)
                    .Select(set => set[random.Next(set.Length)])
                    .ToArray());
            return randomCode;
        }
    }
}