using Caliburn.Micro;
using System.Windows.Input;

namespace TextGrunt.ViewModels
{
    public class SystemTrayItemsViewModel : IHaveIcon
    {
        private IconType _iconType = IconType.None;

        public string Name { get; set; }
        public BindableCollection<SystemTrayItemViewModel> Items { get; set; }

        public IconType IconType
        {
            get => _iconType;
            set { _iconType = value; }
        }
    }

    public class SystemTrayItemViewModel
    {
        public string Text { get; set; }
        public string ToolTip { get; set; }
        public ICommand ToClipBoardCommand { get; set; }
    }
}