using System.Threading;

namespace AlgorithmSimulator.Algorithms
{
    public class Sort
    {
        /**
         * <summary>
         * Runs bubble sort algorithm (in ascending order) on given int array,
         * calls callback on each iteration and on completion
         * </summary>
         * <param name="unsorted">The unsorted array</param>
         * <param name="updateCallback">used for triggering update callbacks</param>
         */
        static public void Bubble(ref int[] unsorted, UpdateCallback updateCallback)
        {
            bool sorted = false;
            while (!sorted)
            {
                sorted = true;
                for (int i = 1; i < unsorted.Length; i++)
                {
                    if (unsorted[i - 1] > unsorted[i])
                    {
                        // swap the current index and last index
                        (unsorted[i], unsorted[i - 1]) = (unsorted[i - 1], unsorted[i]);
                        sorted = false;

                        updateCallback.Callback?.Invoke(new UpdateArgs(false, i));
                    }
                    Thread.Sleep(updateCallback.SleepMs);
                }
            }

            updateCallback.Callback?.Invoke(new UpdateArgs(true, -1));
        }
        /**
         * <summary>
         * Runs selection sort algorithm (in ascending order) on given int array,
         * calls callback on each iteration and on completion
         * </summary>
         * <param name="unsorted">The unsorted array</param>
         * <param name="updateCallback">used for triggering update callbacks</param>
         */
        static public void Selection(ref int[] unsorted, UpdateCallback updateCallback)
        {
            // iterate over whole array
            for (int i = 0; i < unsorted.Length - 1; i++)
            {
                int minValueI = i;

                // compare leftmost value to values on the right-hand side
                for (int j = i + 1; j < unsorted.Length; j++)
                {
                    // check if new element that is lower than already found
                    if (unsorted[j] < unsorted[minValueI])
                    {
                        minValueI = j;
                    }
                }
                if (minValueI != i)
                {
                    // swap i with min i
                    (unsorted[i], unsorted[minValueI]) = (unsorted[minValueI], unsorted[i]);
                }
                updateCallback.Callback?.Invoke(new UpdateArgs(false, i));
                Thread.Sleep(updateCallback.SleepMs);
            }
            updateCallback.Callback?.Invoke(new UpdateArgs(true, -1));
        }
        /**
         * <summary>
         * Create partition and return the pivot to use
         * </summary>
         * <param name="array">the array to use</param>
         * <param name="lowIndex">the low index</param>
         * <param name="highIndex">the high index</param>
         * <returns>the pivot to use</returns>
         */
        static private int QuickPartition(ref int[] array, int lowIndex, int highIndex)
        {
            int i = lowIndex - 1;
            int x = array[highIndex];

            for (int j = lowIndex; j < highIndex; j++)
            {
                if (array[j] <= x)
                {
                    // increment smaller element
                    i++;
                    (array[i], array[j]) = (array[j], array[i]);
                }
            }
            (array[i+1], array[highIndex]) = (array[highIndex], array[i + 1]);
            return i + 1;
        }
        /**
         * <summary>
         * Runs quick sort algorithm (in ascending order) on given int array,
         * calls callback on each iteration and on completion
         * </summary>
         * <param name="unsorted">The unsorted array</param>
         * <param name="updateCallback">used for triggering update callbacks</param>
         */
        static public void Quick(ref int[] unsorted, UpdateCallback updateCallback)
        {
            int lowIndex = 0;
            int highIndex = unsorted.Length - 1;

            int size = highIndex - lowIndex + 1;
            int[] stack = new int[size];

            // init top of stack
            int top = 0;

            // set inital values in 'stack'
            stack[top] = lowIndex;
            top++;
            stack[top] = highIndex;

            while (top >= 0)
            {
                // get high and low values
                highIndex = stack[top];
                top--;
                lowIndex = stack[top];
                top--;

                // set pivot element
                int pivot = QuickPartition(ref unsorted, lowIndex, highIndex);

                // if elements are at left-side of pivot, push to left of stack
                if (pivot - 1 > lowIndex)
                {
                    top++;
                    stack[top] = lowIndex;
                    top++;
                    stack[top] = pivot - 1;
                }

                // if elements are at right-side of pivot, push to right of stack
                if (pivot + 1 < highIndex)
                {
                    top++;
                    stack[top] = pivot + 1;
                    top++;
                    stack[top] = highIndex;
                }
                updateCallback.Callback?.Invoke(new UpdateArgs(false, new int[] { highIndex, lowIndex }));
                Thread.Sleep(updateCallback.SleepMs);
            }

            updateCallback.Callback?.Invoke(new UpdateArgs(true, -1));
        }
    }
}
