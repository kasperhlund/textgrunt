using Caliburn.Micro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
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
        private string _filename;

        public TabViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public Sheet Sheet
        {
            get { return _sheet; }
            set
            {
                _sheet = value;
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

        public string Filename
        {
            get { return _filename; }
            set
            {
                _filename = value;
                DisplayName = _filename;
            }
        }

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

        private void RemoveWhitespaceSelected()
        {
            foreach (Row item in Selected)
            {
                item.Text = Regex.Replace(item.Text, @"\s+", "");
            }
        }

        private void CopySelected()
        {
            Clipboard.SetText(((Row)Selected[0]).Text);
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