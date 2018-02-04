﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
