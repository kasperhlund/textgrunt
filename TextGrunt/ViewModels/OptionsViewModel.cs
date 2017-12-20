using Caliburn.Micro;
using System.Windows.Input;
using TextGrunt.Models;
using TextGrunt.Services;

namespace TextGrunt.ViewModels
{
    public class OptionsViewModel : Screen
    {
        private IOptionsService _optionsService;

        public OptionsViewModel(IOptionsService optionsService)
        {
            DisplayName = "Options";
            _optionsService = optionsService;
            Options = (Options)optionsService.Current;
        }

        public ICommand CloseCommand { get => new RelayCommand(o => true, o => TryClose()); }

        public Options Options { get; private set; }
    }
}