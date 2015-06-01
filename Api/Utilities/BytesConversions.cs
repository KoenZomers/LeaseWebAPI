using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace KoenZomers.LeaseWebApi.Utilities
{
    /// <summary>
    /// Utilities to help with byte conversions
    /// </summary>
    public static class BytesConversions
    {
        /// <summary>
        /// Converts bytes to a friendly readable text
        /// </summary>
        /// <param name="byteCount">Amount of bytes</param>
        /// <param name="decimalCount">Amount of decimals to use in the friendly notation</param>
        /// <returns>String containing the amount of bytes in a friendly readable text</returns>
        /// <remarks>Code sample from http://stackoverflow.com/questions/281640/how-do-i-get-a-human-readable-file-size-in-bytes-abbreviation-using-net </remarks>
        public static string BytesToFriendlyText(long byteCount, short decimalCount = 1)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            if (byteCount == 0) return "0" + suf[0];
            var bytes = Math.Abs(byteCount);
            var place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1000)));
            var num = Math.Round(bytes / Math.Pow(1000, place), decimalCount);
            return (Math.Sign(byteCount) * num).ToString(CultureInfo.InvariantCulture) + suf[place];
        }

        /// <summary>
        /// Converts a friendly readable text with a data amount in it to its equivallent in bytes
        /// </summary>
        /// <param name="friendlyText">Friendly readable data amount, i.e. 100GB, 12.5 MB, 284.123 TB</param>
        /// <returns>Equivallent in bytes as a long</returns>
        public static long FriendlyTextToBytes(string friendlyText)
        {
            // Use a regular expression to parse the text
            var dataRegEx = Regex.Match(friendlyText, @"(?<amount>[\d\.,]+)\s*(?<quantifier>(?:B|KB|MB|GB|TB|PT|EB))", RegexOptions.IgnoreCase);
            
            // Validate if the components were found in the text
            if (!dataRegEx.Groups["amount"].Success || !dataRegEx.Groups["quantifier"].Success)
            {
                throw new ArgumentException("The provided text could not be parsed", "friendlyText");
            }

            // Verify if we can parse the amoun of data
            decimal dataAmount;
            if (!decimal.TryParse(dataRegEx.Groups["amount"].Value, out dataAmount))
            {
                throw new ArgumentException("The provided text contains an invalid data amount", "friendlyText");
            }

            switch (dataRegEx.Groups["quantifier"].Value.ToUpper())
            {
                case "KB": dataAmount = decimal.Multiply(dataAmount, 1000); break;
                case "MB": dataAmount = decimal.Multiply(dataAmount, 1000000); break;
                case "GB": dataAmount = decimal.Multiply(dataAmount, 1000000000); break;
                case "TB": dataAmount = decimal.Multiply(dataAmount, 1000000000000); break;
                case "PB": dataAmount = decimal.Multiply(dataAmount, 1000000000000000); break;
                case "EB": dataAmount = decimal.Multiply(dataAmount, 1000000000000000000); break;
            }
            var bytes = (long) decimal.Round(dataAmount, 0);
            return bytes;
        }
    }
}
