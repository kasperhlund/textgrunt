using Caliburn.Micro;
using System;
using System.Timers;
using TextGrunt.Models;

namespace TextGrunt.Services
{
    public class BookService : IBookService, IDisposable
    {
        private IStorageService _storageService;
        private IOptionsService _optionsService;
        private Timer _saveTimer;
        private Book _book;
        private bool _isDirty;

        public BookService(IStorageService storageService, IOptionsService optionsService)
        {
            _storageService = storageService;
            _optionsService = optionsService;
            InitBook();
            _saveTimer = new Timer(TimeSpan.FromSeconds(5).TotalMilliseconds);
            _saveTimer.Elapsed += (s, e) => OnTimerElapsed();
            _saveTimer.Enabled = true;
        }

        private void InitBook()
        {
            Book = _storageService.Read<Book>(_optionsService.Current.DataFilePath);
            if (Book == null)
            {
                Book = BuildNewBook();
                AddTutorial();
            }
        }

        public Book Book
        {
            get => _book;
            set
            {
                if (_book != null)
                    _book.BookModifiedEvent -= OnBookModified;
                _book = value;
                if (_book != null)
                    _book.BookModifiedEvent += OnBookModified;
            }
        }

        private void OnBookModified(object sender, System.EventArgs e)
        {
            _isDirty = true;
        }

        private void OnTimerElapsed()
        {
            if (!_isDirty)
                return;

            _storageService.Write(Book, _optionsService.Current.DataFilePath);
            _isDirty = false;
        }

        private Book BuildNewBook()
        {
            return new Book()
            {
                Sheets = new BindableCollection<Sheet>()
                {
                    BuildNewSheet(),
                }
            };
        }

        public Sheet BuildNewSheet()
        {
            return new Sheet()
            {
                Rows = new BindableCollection<Row>(),
                Name = "Untitled"
            };
        }

        public void Dispose()
        {
            _saveTimer.Dispose();
            _storageService.Write(Book, _optionsService.Current.DataFilePath);
        }

        private void AddTutorial()
        {
            var rows = Book.Sheets[0].Rows;
            rows.Add(new Row()
            {
                Text =
                    $"This is an example row of text.{Environment.NewLine}" +
                    $"To edit, click this row.{Environment.NewLine}" +
                    $"To finish editing a row, press <Ctrl>+<Enter> or select a different cell.",
                Comment = "Add comment about row here..."
            });
        }
    }
}