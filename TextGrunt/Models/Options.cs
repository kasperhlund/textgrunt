using Caliburn.Micro;
using Newtonsoft.Json;

namespace TextGrunt.Models
{
    public class Options : PropertyChangedBase
    {
        private bool _hasAutoStart;
        private string _dataFilePath;

        [JsonProperty]
        public bool HasAutoStart
        {
            get
            {
                return _hasAutoStart;
            }

            set
            {
                _hasAutoStart = value;
                NotifyOfPropertyChange();
            }
        }

        [JsonProperty]
        public string DataFilePath
        {
            get
            {
                return _dataFilePath;
            }
            set
            {
                _dataFilePath = value;
                NotifyOfPropertyChange();
            }
        }
    }
}