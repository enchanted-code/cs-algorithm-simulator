using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System;

namespace AlgorithmSimulator
{
    public partial class FormMain : Form
    {
        private readonly string[] algorithmNames = {
            "Bubble Sort",
            "Selection Sort",
            "Quick Sort",
            "Linear Search",
            "Binary Search",
        };
        private int[] arrayItems = new int[20];
        private Algorithms.UpdateArgs CurrentUpdateArgs;
        private delegate void UpdateAlgorithmDelegate(Algorithms.UpdateArgs updateArgs);
        private const int stepWaitMs = 250;
        private Stopwatch stopwatch;
        public FormMain()
        {
            InitializeComponent();
            comboAlgorithm.DataSource = algorithmNames;
        }
        /**
         * <summary>Toggles the UI lock.</summary>
         */
        private void ToggleUiLock()
        {
            comboAlgorithm.Enabled = !comboAlgorithm.Enabled;
            buttonStart.Enabled = !buttonStart.Enabled;
            buttonStep.Enabled = !buttonStep.Enabled;
        }
        /**
         * <summary>Triggered when the picture box needs re-drawing</summary>
         */
        private void Algorithm_Paint(object sender, PaintEventArgs e)
        {
            pictureBoxOutput.Image = null;
            Graphics g = e.Graphics;
            pictureBoxOutput.DrawColumns(g, ref arrayItems, CurrentUpdateArgs);
        }
        private void UpdateTimerLabel(TimeSpan timeSpan)
        {
            double seconds = timeSpan.TotalSeconds;
            labelRunTime.Text = "Last Run Time: " + seconds.ToString() + " seconds";
        }
        /**
         * <summary>Causes the PhotoBox to update, is thread safe.</summary>
         */
        public void DoAlgorithmUpdateDisplay(Algorithms.UpdateArgs updateArgs)
        {
            if (pictureBoxOutput.InvokeRequired)
            {
                // invoke on UI thread as method was called from another
                pictureBoxOutput.Invoke(
                    new UpdateAlgorithmDelegate(DoAlgorithmUpdateDisplay),
                    updateArgs
                );
            }
            else
            {
                CurrentUpdateArgs = updateArgs;
                if (updateArgs.Finished)
                {
                    stopwatch.Stop();
                    var timeSpan = TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds);
                    UpdateTimerLabel(timeSpan);
                    ToggleUiLock();
                }
            }
        }
        /**
         * <summary>Randomise the array and update the display.</summary>
         */
        private void RandomiseArray()
        {
            arrayItems = ArrayHelpers.GetRandomIntArray();
            DoAlgorithmUpdateDisplay(new Algorithms.UpdateArgs(false, 0));
        }
        /**
         * <summary>Start a new timer and lock the UI</summary>
         */
        private void MarkAlgorithmStarted()
        {
            ToggleUiLock();
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }
        /**
         * <summary>
         * Start the chosen algorithm that is
         * currently selected in combobox.
         * </summary>
         * <param name="useStepped">Whether to use the stepWaitMs</param>
         */
        private void StartChosenAlgorithm(bool useStepped)
        {
            int waitMs = 0;
            if (useStepped) { waitMs = stepWaitMs; }

            var updateCallback = new Algorithms.UpdateCallback(DoAlgorithmUpdateDisplay, waitMs);

            switch (comboAlgorithm.SelectedValue)
            {
                case "Bubble Sort":
                    RandomiseArray();
                    MarkAlgorithmStarted();
                    Task.Run(() => Algorithms.Sort.Bubble(
                        ref arrayItems, updateCallback
                    ));
                    break;
                case "Selection Sort":
                    RandomiseArray();
                    MarkAlgorithmStarted();
                    Task.Run(() => Algorithms.Sort.Selection(
                        ref arrayItems, updateCallback
                    ));
                    break;
                case "Quick Sort":
                    RandomiseArray();
                    MarkAlgorithmStarted();
                    Task.Run(() => Algorithms.Sort.Quick(
                        ref arrayItems, updateCallback
                    ));
                    break;
                case "Linear Search":
                    RandomiseArray();
                    MarkAlgorithmStarted();
                    int toFind = ArrayHelpers.PickRandomElement(ref arrayItems);
                    Task.Run(() => Algorithms.Search.Linear(
                        ref arrayItems, toFind, updateCallback
                    ));
                    break;
                case "Binary Search":
                    arrayItems = ArrayHelpers.GetSortedIntArray();
                    MarkAlgorithmStarted();
                    int toFind2 = ArrayHelpers.PickRandomElement(ref arrayItems);
                    Task.Run(() => Algorithms.Search.Binary(
                        ref arrayItems, toFind2, updateCallback
                    ));
                    break;
            }
        }
        /**
         * <summary>Triggered when the start realtime button is clicked.</summary>
         */
        private void ButtonStartRealTime_Click(object sender, System.EventArgs e)
        {
            StartChosenAlgorithm(false);
        }
        /**
         * <summary>Triggered when the start step button is clicked.</summary>
         */
        private void ButtonStartStep_Click(object sender, System.EventArgs e)
        {
            StartChosenAlgorithm(true);
        }
    }
}
