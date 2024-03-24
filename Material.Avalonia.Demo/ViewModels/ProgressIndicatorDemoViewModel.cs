using System.Timers;

namespace Material.Avalonia.Demo.ViewModels;

public class ProgressIndicatorDemoViewModel : ViewModelBase {
    private readonly Timer _timer;
    private double _progress;
    private int _progressSate;

    public ProgressIndicatorDemoViewModel() {
        _timer = new Timer(1000);
        _timer.Elapsed += Timer_Elapsed;
        _timer.Start();
    }

    public double Progress {
        get => _progress;
        set {
            _progress = value;
            OnPropertyChanged();
        }
    }

    private void Timer_Elapsed(object? sender, ElapsedEventArgs e) {
        Progress = SwitchProgress();
    }

    private double SwitchProgress() {
        switch (_progressSate) {
            case 0:
                _progressSate++;
                return 30;
            case 1:
                _progressSate++;
                return 45;
            case 2:
                _progressSate++;
                return 50;
            case 3:
                _progressSate++;
                return 80;
            case 4:
                _progressSate++;
                return 100;
            case 5:
                _progressSate = 0;
                return 0;
            default:
                _progressSate = 0;
                return 0;
        }
    }
}