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
        IEventAggregator _eventAggregator;
        IBookService _bookService;
        IClipboardService _clipboardService;
        BindableCollection<SystemTrayItemsViewModel> _items;

        public SystemTrayViewModel(IEventAggregator eventAggregator, IBookService bookService, IClipboardService clipboardService)
        {
            _eventAggregator = eventAggregator;
            _bookService = bookService;
            _clipboardService = clipboardService;
            _items = new BindableCollection<SystemTrayItemsViewModel>();
            PopulateItems();

            _bookService.Book.BookModifiedEvent += (s, e) => PopulateItems();
            _clipboardService.Clips.CollectionChanged += (s, e) => PopulateItems();
        }

        protected override void OnViewLoaded(object view)
        {
            _clipboardService.Initiate(view as Window);
        }

        public BindableCollection<SystemTrayItemsViewModel> Items => _items;

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

        void PopulateItems()
        {
            _items.Clear();
            _items.AddRange(_bookService.Book.Sheets.Select(sheet => new SystemTrayItemsViewModel { Name = sheet.Name, Items = new BindableCollection<SystemTrayItemViewModel>(sheet.Rows.Where(row => row.Text.Count() > 0).Select(row => new SystemTrayItemViewModel { Text = row.Text, ToolTip = row.MoreInfo, ToClipBoardCommand = new RelayCommand(o => true, o => _clipboardService.ToClipBoard(row.Text)) })) }));
            _items.Add(new SystemTrayItemsViewModel { Name = "Clipboard history", IconType = IconType.ClipBoard,Items = new BindableCollection<SystemTrayItemViewModel>(_clipboardService.Clips.Select(clip => new SystemTrayItemViewModel{ Text = clip, ToolTip = clip, ToClipBoardCommand = new RelayCommand(o => true, o => _clipboardService.ToClipBoard(clip)) })) });
        }

    }
}