using System.Windows.Input;

namespace TextGrunt.ViewModels
{
    public class MoveRowViewModel
    {
        public string Title { get; set; }
        public ICommand MoveCommand { get; set; }
    }
}