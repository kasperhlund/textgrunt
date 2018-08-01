using Caliburn.Micro;
using System.Windows.Input;
using TextGrunt.Services;

namespace TextGrunt.ViewModels
{
    public class ClipboardViewModel : Screen, IShellTabItem, IHaveIcon
    {
        private const int _maxSize = 100;
        private IClipboardService _clipboardService;

        public ClipboardViewModel(IClipboardService clipboardService)
        {
            DisplayName = "Clipboard history";
            _clipboardService = clipboardService;
        }

        public BindableCollection<string> Clips => _clipboardService.Clips;

        public string SelectedItem { get; set; }
        public ICommand CopyCommand => new RelayCommand(o => SelectedItem != null, o => _clipboardService.ToClipBoard(SelectedItem));

        public ICommand RemoveCommand => new RelayCommand(o => SelectedItem != null, o => Clips.Remove(SelectedItem));

        public IconType IconType => IconType.ClipBoard;
    }
}