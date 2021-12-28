using Avalonia.Controls;
using Material.Dialog.Icons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Avalonia.Layout;

namespace Material.Dialog.ViewModels
{
    public abstract class DialogWindowViewModel : DialogViewModelBase
    {
        #region Base Properties
        private string m_WindowTitle;
        public string WindowTitle 
            { get => m_WindowTitle; set { m_WindowTitle = value; OnPropertyChanged(); } }

        private string m_ContentHeader;
        public string ContentHeader 
            { get => m_ContentHeader; set { m_ContentHeader = value; OnPropertyChanged(); } }

        private string m_ContentMessage;
        public string ContentMessage 
            { get => m_ContentMessage; set { m_ContentMessage = value; OnPropertyChanged(); } }

        private bool m_Borderless;
        public bool Borderless { get => m_Borderless; set { m_Borderless = value; OnPropertyChanged(); } }

        private double? m_MaxWidth;
        public double? MaxWidth { get => m_MaxWidth; set { m_MaxWidth = value; OnPropertyChanged(); } }

        private double? m_Width;
        public double? Width { get => m_Width; set { m_Width = value; OnPropertyChanged(); } }

        private WindowStartupLocation m_WindowStartupLocation;
        public WindowStartupLocation WindowStartupLocation 
            { get => m_WindowStartupLocation; set { m_WindowStartupLocation = value; OnPropertyChanged(); } }

        private object? _dialogIcon;
        public object? DialogIcon { get => _dialogIcon; set { _dialogIcon = value; OnPropertyChanged(); } }
        #endregion
        
        
        private DialogResultButton[] m_DialogButtons;
        public DialogResultButton[] DialogButtons { get => m_DialogButtons; internal set => m_DialogButtons = value; }
        
        private Orientation m_ButtonsStackOrientation;
        public Orientation ButtonsStackOrientation { get => m_ButtonsStackOrientation; internal set => m_ButtonsStackOrientation = value; }
    }
}
