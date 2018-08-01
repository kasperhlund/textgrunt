using Caliburn.Micro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using TextGrunt.Models;
using TextGrunt.Services;

namespace TextGrunt.ViewModels
{
    public class TabViewModel : Screen, IShellTabItem
    {
        private IDialogService _dialogService;
        private Sheet _sheet;
        private IList _selected;
        private IClipboardService _clipboardService;
        private IBookService _bookService;

        public TabViewModel(IDialogService dialogService, IClipboardService clipboardService, IBookService bookService)
        {
            _dialogService = dialogService;
            _clipboardService = clipboardService;
            _bookService = bookService;
        }

        public Sheet Sheet
        {
            get { return _sheet; }
            set
            {
                _sheet = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(DisplayName));
            }
        }

        public override string DisplayName
        {
            get => _sheet.Name;
            set
            {
                _sheet.Name = value;
                NotifyOfPropertyChange();
            }
        }

        public IList Selected
        {
            get { return _selected; }
            set
            {
                // dont wanna select the newitemplaceholder
                _selected = value.OfType<Row>().ToList();
                NotifyOfPropertyChange();
            }
        }

        private int _selectedIndex;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; NotifyOfPropertyChange(); }
        }

        public IEnumerable<MoveRowViewModel> MoveToTargets => _bookService.Book.Sheets.Where(sheet => sheet != Sheet)
                    .Select(sheet => new MoveRowViewModel { Title = sheet.Name, MoveCommand = new RelayCommand((o) => CountSelected() > 0, (o) => MoveSelectedRowsToOtherSheet(sheet)) });

        public ICommand MoveUpSelectedCommand => new RelayCommand(e => CountSelected() == 1, e => MoveUpSelected());
        public ICommand MoveDownSelectedCommand => new RelayCommand(e => CountSelected() == 1, e => MoveDownSelected());
        public ICommand RemoveSelectedCommand => new RelayCommand(e => CountSelected() > 0, e => RemoveSelected());
        public ICommand ToCapsCommand => new RelayCommand(e => CountSelected() > 0, e => ToCapsSelected());
        public ICommand ToLowerCommand => new RelayCommand(e => CountSelected() > 0, e => ToLowerSelected());
        public ICommand CopySelectedCommand => new RelayCommand(e => CountSelected() == 1, e => CopySelected());
        public ICommand RemoveWhitespaceSelectedCommand => new RelayCommand(e => CountSelected() > 0, e => RemoveWhitespaceSelected());
        public ICommand CommentSelectedCommand => new RelayCommand(e => CountSelected() > 0, e => GetAndSetCommentInput());
        public ICommand InternetSearchSelectedCommand => new RelayCommand(e => CountSelected() == 1, e => InternetSearchSelected());

        private void MoveUpSelected()
        {
            foreach (Row item in (new List<Row>(Selected.Cast<Row>())))
            {
                int index = Sheet.Rows.IndexOf(item);
                if (index <= 0)
                    continue;

                Sheet.Rows.RemoveAt(index);
                Sheet.Rows.Insert(index - 1, item);
            }
        }

        private void MoveDownSelected()
        {
            foreach (Row item in (new List<Row>(Selected.Cast<Row>())))
            {
                int index = Sheet.Rows.IndexOf(item);
                if ((index + 1) == Sheet.Rows.Count)
                    continue;

                Sheet.Rows.RemoveAt(index);
                Sheet.Rows.Insert(index + 1, item);
            }
        }

        private void RemoveSelected()
        {
            Sheet.Rows.RemoveRange(Selected.Cast<Row>());
        }

        private void ToCapsSelected()
        {
            foreach (Row item in Selected)
            {
                item.Text = item.Text.ToUpper();
            }
        }

        private void InternetSearchSelected()
        {
            System.Diagnostics.Process.Start($"http://www.google.com/search?q={((Row)Selected[0]).Text}");
        }

        private void ToLowerSelected()
        {
            foreach (Row item in Selected)
            {
                item.Text = item.Text.ToLower();
            }
        }

        private void MoveSelectedRowsToOtherSheet(Sheet target)
        {
            foreach (Row item in Selected)
            {
                Sheet.Rows.Remove(item);
                target.Rows.Add(item);
            }
        }

        private void RemoveWhitespaceSelected()
        {
            foreach (Row item in Selected)
            {
                item.Text = Regex.Replace(item.Text, @"\s+", "");
            }
        }

        private void CopySelected()
        {
            _clipboardService.ToClipBoard(((Row)Selected[0]).Text);
        }

        private int CountSelected()
        {
            return Selected?.Count ?? 0;
        }

        private void GetAndSetCommentInput()
        {
            string initialResponse = Selected.Count == 1 ? ((Row)Selected[0]).Comment : string.Empty;

            string response = _dialogService.GetUserTextInput("Comment", "Comment text", initialResponse);
            if (response == null)
                return;

            foreach (Row item in Selected)
            {
                item.Comment = response;
            }
        }
    }
}