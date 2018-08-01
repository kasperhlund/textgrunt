using Caliburn.Micro;
using Newtonsoft.Json;
using System;

namespace TextGrunt.Models
{
    public class Row : PropertyChangedBase
    {
        private string _text = string.Empty;
        private string _comment = string.Empty;

        [JsonProperty]
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(MoreInfo));
            }
        }

        [JsonProperty]
        public string Comment
        {
            get
            { return _comment; }
            set
            {
                _comment = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(MoreInfo));
            }
        }

        public string MoreInfo
        {
            get
            {
                return $"Length: {_text.Length}{Environment.NewLine}Comment:{Comment}";
            }
        }
    }
}