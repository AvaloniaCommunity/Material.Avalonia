using System.ComponentModel;
using System.Timers;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Material.Demo.Pages
{
    public class ProgressIndicatorDemo : UserControl
    {
        private Timer timer;
        private int caseProgress;

        public class Context : INotifyPropertyChanged
        {
            private double m_Progress = 0;

            public double Progress
            {
                get => m_Progress;
                set
                {
                    m_Progress = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Progress)));
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        private Context context;

        public ProgressIndicatorDemo()
        {
            this.InitializeComponent();

            timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;

            this.DataContext = context = new Context();

            this.AttachedToVisualTree += ProgressIndicatorDemo_AttachedToVisualTree;
        }

        private void ProgressIndicatorDemo_AttachedToVisualTree(object sender, VisualTreeAttachmentEventArgs e)
        {
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            context.Progress = SwitchProgress();
        }

        private double SwitchProgress()
        {
            switch (caseProgress)
            {
                case 0:
                    caseProgress++;
                    return 30;
                case 1:
                    caseProgress++;
                    return 45;
                case 2:
                    caseProgress++;
                    return 50;
                case 3:
                    caseProgress++;
                    return 80;
                case 4:
                    caseProgress++;
                    return 100;
                case 5:
                    caseProgress = 0;
                    return 0;
                default:
                    caseProgress = 0;
                    return 0;
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}