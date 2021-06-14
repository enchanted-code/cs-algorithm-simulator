using System.Threading;

namespace AlgorithmSimulator.Algorithms
{
    class Search
    {
        /**
         * <summary>
         * Runs a linear search algorithm,
         * runs the callback on each iteration
         * </summary>
         * <param name="elements">elements to search from</param>
         * <param name="toFind">element to find</param>
         * <param name="updateCallback">used for triggering update callbacks</param>
         * <returns>
         * -1 if element not found and a index
         * when element has been found
         * </returns>
         */
        public static int Linear(ref int[] elements, int toFind, UpdateCallback updateCallback)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] == toFind)
                {
                    // element found in array
                    updateCallback.Callback?.Invoke(new UpdateArgs(true, i));
                    return i;
                }
                updateCallback.Callback?.Invoke(new UpdateArgs(false, i));
                Thread.Sleep(updateCallback.SleepMs);
            }
            // element not found in array
            updateCallback.Callback?.Invoke(new UpdateArgs(true, -1));
            return -1;
        }
        /**
         * <summary>
         * Runs a binary search algorithm,
         * runs the callback on each iteration
         * </summary>
         * <param name="elements">
         * sorted elements to search from
         * (ascending order)</param>
         * <param name="toFind">element to find</param>
         * <param name="updateCallback">used for triggering update callbacks</param>
         * <returns>
         * -1 if element not found and a index
         * when element has been found
         * </returns>
         */
        public static int Binary(ref int[] elements, int toFind, UpdateCallback updateCallback)
        {
            int lower = 0;
            int upper = elements.Length - 1;

            while (lower <= upper)
            {
                int mid = (lower + upper) / 2;
                if (elements[mid] == toFind)
                {
                    updateCallback.Callback?.Invoke(new UpdateArgs(true, mid));
                    return mid;
                }
                else if (elements[mid] < toFind)
                {
                    lower = mid + 1;
                }
                else
                {
                    upper = mid - 1;
                }
                updateCallback.Callback?.Invoke(new UpdateArgs(false, mid));
                Thread.Sleep(updateCallback.SleepMs);
            }

            // element not found in array
            updateCallback.Callback?.Invoke(new UpdateArgs(true, -1));
            return -1;
        }
    }
}
