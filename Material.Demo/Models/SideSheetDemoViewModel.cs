namespace Material.Demo.Models
{
    // Data context for sidesheet
    public class SideSheetData : ViewModelBase
    {
        private string _header = "SideSheet";
        public string Header => _header;
    }
    
    // Data context for SideSheet demo page
    public class SideSheetDemoViewModel : ViewModelBase
    {
        private SideSheetData _information = new SideSheetData();
        public SideSheetData Information => _information;
        
        private bool _sideInfoOpened = false;

        public bool SideInfoOpened
        {
            get => _sideInfoOpened;
            set
            {
                _sideInfoOpened = value;
                OnPropertyChanged();
            }
        }
    }
}