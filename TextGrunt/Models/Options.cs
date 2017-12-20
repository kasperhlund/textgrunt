using Caliburn.Micro;
using Newtonsoft.Json;

namespace TextGrunt.Models
{
    public class Options : PropertyChangedBase
    {
        private bool _hasAutoStart;

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
    }
}