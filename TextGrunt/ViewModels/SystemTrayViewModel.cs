using Caliburn.Micro;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using TextGrunt.Messages;
using TextGrunt.Models;
using TextGrunt.Services;

namespace TextGrunt.ViewModels
{
    public class SystemTrayViewModel : Screen
    {
        private IEventAggregator _eventAggregator;
        private IBookService _bookService;

        public SystemTrayViewModel(IEventAggregator eventAggregator, IBookService bookService)
        {
            _eventAggregator = eventAggregator;
            _bookService = bookService;
        }

        public BindableCollection<Sheet> Sheets => _bookService.Book.Sheets;

        public ICommand CopyCommand => new RelayCommand(o => true, DoStuff);

        public void DoStuff(object o)
        {
            Debug.WriteLine("Copy");
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            //ShowWindow();
        }

        public void ShowWindow()
        {
            _eventAggregator.PublishOnUIThread(new ToggleMainViewVisibleMessage() { Visible = true });
        }

        public bool CanShowWindow
        {
            get
            {
                return (true);
            }
        }

        public void HideWindow()
        {
            _eventAggregator.PublishOnUIThread(new ToggleMainViewVisibleMessage() { Visible = false });

            NotifyOfPropertyChange(() => CanShowWindow);
            NotifyOfPropertyChange(() => CanHideWindow);
        }

        public bool CanHideWindow
        {
            get
            {
                return true;
            }
        }

        public void ExitApplication()
        {
            /*
            if (!_dialogService.ShowOkCancel($"Close? Any unsaved changes will be lost."))
            {
                callback(false);
                return;
            }
            */

            Application.Current.Shutdown();
        }
    }
}