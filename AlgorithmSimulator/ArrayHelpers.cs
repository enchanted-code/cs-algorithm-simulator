using System;

namespace AlgorithmSimulator
{
    public static class ArrayHelpers
    {
        /**
         * <summary>
         * Creates a 20 integer random array
         * </summary>
         * <returns>
         * Returns a 20 integer random array
         * </returns>
         */
        public static int[] GetRandomIntArray()
        {
            int[] randomarray = new int[20];
            Random _random = new Random();

            for (int i = 0; i < randomarray.Length; i++)
            {
                randomarray[i] = _random.Next(0, 500);
            }
            return randomarray;
        }
        /**
         * <summary>
         * Creates a 20 integer random array that is sorted
         * </summary>
         * <returns>
         * Returns a 20 integer random array
         * </returns>
         */
        public static int[] GetSortedIntArray()
        {
            int[] randomarray = GetRandomIntArray();
            Array.Sort(randomarray);
            return randomarray;
        }
        /**
         * <summary>
         * pick a random element from a given array
         * </summary>
         * <param name="elements">
         * an array of elements to pick from,
         * must not be empty
         * </param>
         * <returns>the random element</returns>
         */
        public static int PickRandomElement(ref int[] elements)
        {
            if (elements.Length == 0)
            {
                throw new Exception("array cannot be empty");
            }
            Random random = new Random();
            int index = random.Next(0, elements.Length - 1);
            return elements[index];
        }
    }
}
