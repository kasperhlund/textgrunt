using Caliburn.Micro;
using Newtonsoft.Json;
using System;

namespace TextGrunt.Models
{
    public class Book : PropertyChangedBase
    {
        private BindableCollection<Sheet> _sheets;

        public event EventHandler BookModifiedEvent;

        public Book()
        {
            PropertyChanged += OnPropertyChanged;
        }

        [JsonProperty]
        public BindableCollection<Sheet> Sheets
        {
            get => _sheets;
            set
            {
                _sheets = value;
                NotifyOfPropertyChange();
            }
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _sheets.CollectionChanged += OnCollectionChanged;

            foreach (var sheet in _sheets)
            {
                sheet.SheetModifiedEvent += OnBookModified;
            }
        }

        private void OnCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (Sheet sheet in e.NewItems)
                    sheet.SheetModifiedEvent += OnBookModified;

            if (e.OldItems != null)
                foreach (Sheet sheet in e.OldItems)
                    sheet.SheetModifiedEvent -= OnBookModified;

            OnBookModified(this, null);
        }

        private void OnBookModified(object sender, EventArgs e)
        {
            BookModifiedEvent?.Invoke(this, e);
        }
    }
}