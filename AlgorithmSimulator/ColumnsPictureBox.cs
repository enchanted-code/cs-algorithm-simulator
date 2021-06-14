using System;
using System.Drawing;
using System.Windows.Forms;

namespace AlgorithmSimulator
{
    /**
     * <summary>
     * A winforms PictureBox widget that allows
     * for generating height columns.</summary>
     */
    public class ColumnsPictureBox : PictureBox
    {
        /**
         * <summary>
         * Draws columns using the heights given,
         * will automatically calculate the
         * appropriate width of columns to fit on widget
         * </summary>
         * <param name="g">the graphics obj</param>
         * <param name="heights">array containing the heights to draw.</param>
         * <param name="updateArgs">values to use with a algorithm update</param>
         */
        public void DrawColumns(Graphics g, ref int[] heights, Algorithms.UpdateArgs updateArgs)
        {
            var penMain = new Pen(Color.Black, 2);
            var penIndex = new Pen(Color.Blue, 4);

            int colWidth = Width / heights.Length;
            int currCol = 0;
            foreach (var height in heights)
            {
                int x = colWidth * currCol;
                
                var r = new Rectangle(x, 0, colWidth, height);
                if (updateArgs.Highlights != null)
                {
                    if (Array.IndexOf(updateArgs.Highlights, currCol) != -1)
                    {
                        g.DrawRectangle(penIndex, r);
                    }
                    else
                    {
                        g.DrawRectangle(penMain, r);
                    }
                }
                currCol++;
            }
            penMain.Dispose();
            penIndex.Dispose();
        }
    }
}
