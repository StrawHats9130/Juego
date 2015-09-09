using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
 

namespace Engine
{
    
    public static class NumberGeneratorSimple
    {
        private static readonly Random _generator = new Random();

        public static int NumberBetween(int minimumValue, int maximumValue)
        {
            return _generator.Next(minimumValue, maximumValue);
        }
    
    }

    public static class RandomNumberGeneratorComplex
    {
        private static readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();

        public static int NumberBetween(int minimumValue, int maximumValue)
        {
            byte[] randomNumber = new byte[1];
            _generator.GetBytes(randomNumber);

            double asciiValueRandomCharacter = Convert.ToDouble(randomNumber[0]);

            //Using Math.Max & supbtracting 0.00000000001,
            //to ensure "multiplier" will always be between 0.0 and 99999999999
            //Otherwise, it's possible for it to be "1", which causes rounding issues
            double multiplier = Math.Max(0, (asciiValueRandomCharacter/255d) - 0.00000000001d);

            //We need to add one to the range, to allow for the rounding done with Math.Floor
            int range = maximumValue - minimumValue + 1;

            double randomValueInRange = Math.Floor(multiplier*range);

            return (int) (minimumValue + randomValueInRange);

        }

    }


}
