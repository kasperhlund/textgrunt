using Caliburn.Micro;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using TextGrunt.Messages;
using TextGrunt.Models;
using TextGrunt.Services;
using System.Linq;

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
            Application.Current.Shutdown();
        }
    }
}