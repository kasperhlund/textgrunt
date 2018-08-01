using Caliburn.Micro;
using System.Linq;
using System.Windows.Input;
using TextGrunt.Messages;
using TextGrunt.Models;
using TextGrunt.Services;

namespace TextGrunt.ViewModels
{
    public class SearchViewModel : Screen
    {
        private IBookService _bookService;
        private IEventAggregator _eventAggregator;
        private string _searchString;
        private bool _shouldSearchComments;
        private bool _shouldSearchText;
        private bool _shouldIgnoreCase;

        public SearchViewModel(IBookService bookService, IEventAggregator eventAggregator)
        {
            _bookService = bookService;
            _eventAggregator = eventAggregator;
            ShouldSearchComments = true;
            ShouldSearchText = true;
            ShouldIgnoreCase = true;
            Result = new BindableCollection<SearchResultViewModel>();
        }

        public string SearchString
        {
            get => _searchString;

            set
            {
                _searchString = value;
                NotifyOfPropertyChange();
            }
        }

        public bool ShouldSearchComments
        {
            get => _shouldSearchComments;

            set
            {
                _shouldSearchComments = value;
                NotifyOfPropertyChange();
            }
        }

        public bool ShouldSearchText
        {
            get => _shouldSearchText;

            set
            {
                _shouldSearchText = value;
                NotifyOfPropertyChange();
            }
        }

        public bool ShouldIgnoreCase
        {
            get
            {
                return _shouldIgnoreCase;
            }

            set
            {
                _shouldIgnoreCase = value;
                NotifyOfPropertyChange();
            }
        }

        public BindableCollection<SearchResultViewModel> Result { get; set; }

        public ICommand SearchCommand => new RelayCommand((o) => SearchString?.Length > 0 && (ShouldSearchComments || ShouldSearchText), (o) => DoSearch());

        private void DoSearch()
        {
            Result.Clear();
            foreach (var sheet in _bookService.Book.Sheets)
            {
                var sheetResult = sheet.Rows.Where(row => IsMatch(row))
                    .Select(row => new SearchResultRowViewModel { Row = row, GoHereCommand = new RelayCommand(o => true, o => GoHere(sheet, row)) });
                if (sheetResult.Any())
                    Result.Add(new SearchResultViewModel { Sheet = sheet, Matches = sheetResult, GoHereCommand = new RelayCommand(o => true, o => GoHere(sheet)) });
            }
        }

        private bool IsMatch(Row row)
        {
            return (ShouldSearchText && !ShouldIgnoreCase && row.Text.Contains(SearchString) || ShouldSearchComments && !ShouldIgnoreCase && row.Comment.Contains(SearchString))
                || (ShouldSearchText && ShouldIgnoreCase && row.Text.ToLower().Contains(SearchString.ToLower()) || ShouldSearchComments && ShouldIgnoreCase && row.Comment.ToLower().Contains(SearchString.ToLower()));
        }

        private void GoHere(Sheet targetSheet, Row targetRow = null)
        {
            _eventAggregator.PublishOnUIThread(new GotoMessage { TargetSheet = targetSheet, TargetRow = targetRow });
        }
    }
}