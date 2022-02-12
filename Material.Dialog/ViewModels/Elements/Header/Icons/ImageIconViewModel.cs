using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace Material.Dialog.ViewModels.Elements.Header.Icons
{
    public class ImageIconViewModel : IconViewModelBase
    {
        private Bitmap _bitmap;

        public Bitmap Bitmap
        {
            get => _bitmap;
            set
            {
                _bitmap = value;
                OnPropertyChanged();
            }
        }

        private Stretch _stretch = Stretch.Uniform;

        public Stretch Stretch
        {
            get => _stretch;
            set
            {
                _stretch = value;
                OnPropertyChanged();
            }
        }
    }
}