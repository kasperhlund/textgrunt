using Caliburn.Micro;
using Microsoft.Win32;
using System.Dynamic;
using System.Windows;
using TextGrunt.ViewModels;

namespace TextGrunt.Services
{
    public class DialogService : IDialogService
    {
        private IWindowManager _windowManager;
        private OptionsViewModel _optionsViewModel;

        private const string GruntTabFiles = "TextGrunt tab files (*.tgtab)|*.tgtab|All files (*.*)|*.*";

        public DialogService(IWindowManager windowManager, OptionsViewModel optionsViewModel)
        {
            _windowManager = windowManager;
            _optionsViewModel = optionsViewModel;
        }

        public string GetUserTextInput(string header, string question, string initialResponse = "", bool isMultiline = true)
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStyle = WindowStyle.ToolWindow;
            settings.ShowInTaskbar = false;
            settings.Title = header;
            settings.ResizeMode = ResizeMode.NoResize;

            var vm = new InputTextViewModel(header, question, initialResponse, isMultiline);
            if (_windowManager.ShowDialog(vm, null, settings))
            {
                return vm.Response;
            }
            return null;
        }

        public string GetUserFileOpenInput()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Multiselect = false,
                Filter = GruntTabFiles,
            };

            if (dialog.ShowDialog() == true)
                return dialog.FileName;

            return null;
        }

        public string GetUserFileSaveInput()
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = GruntTabFiles
            };

            if (dialog.ShowDialog() == true)
                return dialog.FileName;

            return null;
        }

        public void ShowAbout()
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStyle = WindowStyle.ToolWindow;
            settings.ShowInTaskbar = false;
            settings.Title = "About";
            settings.ResizeMode = ResizeMode.NoResize;

            var vm = new AboutViewModel();
            _windowManager.ShowDialog(vm, null, settings);
        }

        public void ShowOptions()
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStyle = WindowStyle.ToolWindow;
            settings.ShowInTaskbar = false;
            settings.Title = "Options";
            settings.ResizeMode = ResizeMode.NoResize;

            _windowManager.ShowDialog(_optionsViewModel, null, settings);
        }

        public void ShowError(string text)
        {
            MessageBox.Show(text, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public bool ShowOkCancel(string text)
        {
            return MessageBox.Show(text, "Question", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel) == MessageBoxResult.OK;
        }
    }
}