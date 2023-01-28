using System;

namespace Queo.Commons.Builders.Model.Examples.Person.Mocks
{
    public class MockDataGenerator : IDataGenerator
    {
        private int _seed;
        private Random _rndInt;
        private Random _rndString;

        public MockDataGenerator(int seed = 1413821239)
        {
            _seed = seed;
            _rndInt = new Random(seed);
            _rndString = new Random(seed);
        }

        public int GetInt(int min = int.MinValue, int max = int.MaxValue)
        {
            return _rndInt.Next(min, max);
        }


        private static readonly string[] CONSONANTS = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
        private static readonly string[] VOWELS = { "a", "e", "i", "o", "u" };

        /// <summary>
        ///		Primitive name generator
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public string GetString(int min = 4, int max = 10)
        {
            int length = _rndString.Next(min, max);
            string name = "";

            name += CONSONANTS[_rndString.Next(CONSONANTS.Length)].ToUpper();
            name += VOWELS[_rndString.Next(VOWELS.Length)];
            int b = 2;
            while (b < length)
            {
                name += CONSONANTS[_rndString.Next(CONSONANTS.Length)];
                name += VOWELS[_rndString.Next(VOWELS.Length)];
                b += 2;
            }

            return name;
        }
    }
}
