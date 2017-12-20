using Caliburn.Micro;
using Newtonsoft.Json;
using System;

namespace TextGrunt.Models
{
    public class Sheet : PropertyChangedBase
    {
        private string _name = string.Empty;
        private BindableCollection<Row> _rows;

        public event EventHandler SheetModifiedEvent;

        public Sheet()
        {
            PropertyChanged += (s, args) => OnPropertyChanged(args.PropertyName);
        }

        [JsonProperty]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyOfPropertyChange();
            }
        }

        [JsonProperty]
        public BindableCollection<Row> Rows
        {
            get => _rows;
            set
            {
                _rows = value;
                NotifyOfPropertyChange();
            }
        }

        private void OnPropertyChanged(string propName)
        {
            if (propName.Equals(nameof(Rows)))
            {
                foreach (var row in Rows)
                {
                    row.PropertyChanged += OnSheetModified;
                }

                Rows.CollectionChanged += OnCollectionChanged;
            }
            else
            {
                OnSheetModified(this, null);
            }
        }

        private void OnCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (Row row in e.NewItems)
                    row.PropertyChanged += OnSheetModified;

            if (e.OldItems != null)
                foreach (Row row in e.OldItems)
                    row.PropertyChanged -= OnSheetModified;

            OnSheetModified(this, null);
        }

        private void OnSheetModified(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SheetModifiedEvent?.Invoke(this, new EventArgs());
        }
    }
}