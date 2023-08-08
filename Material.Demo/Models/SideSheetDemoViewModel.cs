using Material.Demo.ViewModels;

namespace Material.Demo.Models {
    // Data context for sidesheet
    public class SideSheetData : ViewModelBase {
        private readonly string _header = "SideSheet";
        public string Header => _header;

        public string ContentHeader { get; set; } = "What is Lorem Ipsum?";

        public string ImportantInfos { get; set; } =
            "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
    }

    // Data context for SideSheet demo page
    public class SideSheetDemoViewModel : ViewModelBase {
        private readonly SideSheetData _information = new SideSheetData();

        private bool _sideInfoOpened = false;
        public SideSheetData Information => _information;

        public bool SideInfoOpened {
            get => _sideInfoOpened;
            set {
                _sideInfoOpened = value;
                OnPropertyChanged();
            }
        }
    }
}
