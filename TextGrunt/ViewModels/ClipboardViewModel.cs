using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using TextGrunt.Services;

namespace TextGrunt.ViewModels
{
    public class ClipboardViewModel : Screen, IShellTabItem, IHaveIcon
    {
        const int _maxSize = 100;
        IClipboardService _clipboardService;

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
