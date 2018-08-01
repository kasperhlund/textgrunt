using Caliburn.Micro;
using System.Windows.Input;

namespace TextGrunt.ViewModels
{
    public class InputTextViewModel : Screen
    {
        private string _challenge;
        private string _response = string.Empty;
        private bool _isMultiline;

        public InputTextViewModel(string header, string challenge, string initialResponse, bool isMultiLine = true)
        {
            DisplayName = header;
            _challenge = challenge;
            Response = initialResponse;
            _isMultiline = isMultiLine;

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

        public bool ShouldAcceptReturn => _isMultiline;

        public int InitialHeight => _isMultiline ? 200 : 20;
    }
}