using Caliburn.Micro;
using System.Windows.Input;

namespace TextGrunt.ViewModels
{
    public class InputTextViewModel : Screen
    {
        private string _challenge;
        private string _response = string.Empty;

        public InputTextViewModel(string header, string challenge, string initialResponse)
        {
            DisplayName = header;
            _challenge = challenge;
            Response = initialResponse;

            OkCommand = new RelayCommand(o => true, o => TryClose(true));
            CancelCommand = new RelayCommand(o => true, o => TryClose(false));
        }

        public string Challenge { get { return _challenge; } }

        public string Response
        {
            get
            {
                return _response;
            }
            set
            {
                _response = value;
                NotifyOfPropertyChange();
            }
        }

        public ICommand OkCommand
        {
            get; private set;
        }

        public ICommand CancelCommand
        {
            get; private set;
        }
    }
}