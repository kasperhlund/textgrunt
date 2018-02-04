using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TextGrunt.ViewModels
{
    public class MoveRowViewModel
    {
        public string Title { get; set; }
        public ICommand MoveCommand { get; set; }


    }
}
