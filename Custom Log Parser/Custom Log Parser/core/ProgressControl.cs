using System.Windows.Controls;

namespace Custom_Log_Parser.core
{
    public class ProgressControl
    {
        private readonly ProgressBar progressBar;
        private readonly TextBlock textBlock;
        public ProgressControl (ProgressBar pb, TextBlock tb)
        {
            progressBar = pb;
            textBlock = tb;
        }
        public void Init(double max)
        {
            progressBar.Minimum = 0;
            progressBar.Maximum = max;
            progressBar.Value = 0;
            SetProgressState(0);
        }
        public void SetProgressState(double value)
        {
            progressBar.Value += value;
            textBlock.Text = ProgressTextBlockFormater.Format(progressBar.Value, progressBar.Maximum);
        }
    }
}
