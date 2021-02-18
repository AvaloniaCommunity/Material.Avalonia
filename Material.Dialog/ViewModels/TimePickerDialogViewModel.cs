using Avalonia.Layout;
using Avalonia.Threading;
using Material.Dialog.Commands;
using Material.Dialog.ViewModels.TextField;
using Material.Dialog.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Media;

namespace Material.Dialog.ViewModels
{
    public class TimePickerDialogViewModel : DialogWindowViewModel
    {
        private TimePickerDialog _window;

        private DialogResultButton[] m_DialogButtons;
        public DialogResultButton[] DialogButtons { get => m_DialogButtons; internal set => m_DialogButtons = value; }

        private DialogResultButton m_PositiveButton;
        public DialogResultButton PositiveButton { get => m_PositiveButton; internal set => m_PositiveButton = value; }

        private DialogResultButton m_NegativeButton;
        public DialogResultButton NegativeButton { get => m_NegativeButton; internal set => m_NegativeButton = value; }

        private ushort _firstField = 0;
        public ushort FirstField
        {
            get => _firstField;
            set
            {
                _firstField = value;
                OnPropertyChanged();
            }
        }
        
        private ushort _secondField = 0;
        public ushort SecondField
        {
            get => _secondField;
            set
            {
                _secondField = value;
                OnPropertyChanged();
            }
        }

        private double _firstPanelPointerAngle;
        public double FirstPanelPointerAngle
        {
            get => _firstPanelPointerAngle;
            set
            {
                _firstPanelPointerAngle = value;
                OnPropertyChanged();
            }
        }
        
        private double _secondPanelPointerAngle;
        public double SecondPanelPointerAngle
        {
            get => _secondPanelPointerAngle;
            set
            {
                _secondPanelPointerAngle = value;
                OnPropertyChanged();
            }
        }

        private bool _isAm = true;
        public bool IsAm
        {
            get => _isAm;
            set
            {
                _isAm = value;
                OnPropertyChanged();
            }
        }

        private int _carouselIndex = 0;
        public int CarouselIndex
        {
            get => _carouselIndex;
            set
            {
                _carouselIndex = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FirstFieldSelected));
                OnPropertyChanged(nameof(SecondFieldSelected));
            }
        }

        public bool FirstFieldSelected
        {
            get => _carouselIndex == 0;
            set
            { 
                CarouselIndex = value ? 0 : 1; 
                OnPropertyChanged();
            }
        }
        
        public bool SecondFieldSelected
        {
            get => _carouselIndex == 1;
            set
            {
                CarouselIndex = value ? 1 : 0;
                OnPropertyChanged();
            }
        }

        public void SetFirstField(int v)
        {
            FirstField = (ushort)v;
            FirstPanelPointerAngle = (v / (double)12) * 360;
        }
        
        public void SetSecondField(int v)
        {
            SecondField = (ushort)v;
            SecondPanelPointerAngle = (v / (double)60) * 360;
        }

        public TimePickerDialogViewModel(TimePickerDialog dialog)
        {
            _window = dialog;
            ButtonClick = new RelayCommand(OnPressButton, CanPressButton);
        }

        public bool ValidateFields()
        {
            return true;
        }

        public bool CanPressButton(object args) => true;
        public async void OnPressButton(object args)
        {
            var button = args as DialogResultButton;
            if (button is null)
                return; 

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                var result = new DateTimePickerDialogResult() { _result = button.Result };
                var fields = new List<TextFieldResult>();

                result._dateTime = new DateTime();
                _window.Result = result;
                _window.Close();
            });
        }

        public RelayCommand ButtonClick { get; private set; }
    }
}
