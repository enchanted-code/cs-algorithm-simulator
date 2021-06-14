using System;

namespace AlgorithmSimulator.Algorithms
{
    /**
     * <summary>
     * contains values to pass when
     * triggering a algorithm update
     * </summary>
     */
    public readonly struct UpdateArgs
    {
        /**
         * <param name="finished">whether the algorithm has finished</param>
         * <param name="highlight">highlight one index</param>
         */
        public UpdateArgs(bool finished, int highlight)
        {
            Finished = finished;
            Highlights = new int[] { highlight };
        }
        /**
         * <param name="finished">whether the algorithm has finished</param>
         * <param name="highlights">highlight multiple indexes</param>
         */
        public UpdateArgs(bool finished, int[] highlights)
        {
            Finished = finished;
            Highlights = highlights;
        }
        public readonly bool Finished;
        public readonly int[] Highlights;
    }
    /**
     * <summary>
     * contains the action to allow for
     * calling the update method and a sleep delay
     * </summary>
     */
    public readonly struct UpdateCallback
    {
        /**
         * <param name="callback">callback to run on each algorithm step</param>
         * <param name="sleepMs">wait time before going to next step</param>
         */
        public UpdateCallback(Action<UpdateArgs> callback = null, int sleepMs = 0)
        {
            Callback = callback;
            SleepMs = sleepMs;
        }
        public readonly Action<UpdateArgs> Callback;
        public readonly int SleepMs;
    }
}
