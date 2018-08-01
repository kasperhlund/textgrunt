using System.Collections.Generic;
using System.Windows.Input;
using TextGrunt.Models;

namespace TextGrunt.ViewModels
{
    public class SearchResultViewModel
    {
        public Sheet Sheet { get; set; }
        public IEnumerable<SearchResultRowViewModel> Matches { get; set; }
        public ICommand GoHereCommand { get; set; }
    }

    public class SearchResultRowViewModel
    {
        public Row Row { get; set; }
        public ICommand GoHereCommand { get; set; }
    }
}