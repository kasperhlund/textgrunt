using Caliburn.Micro;
using System.Linq;
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
        public string ShortText
        {
            get
            {
                string shortText = Text;
                if(Text.Length >= 50)
                {
                    shortText = Text.Substring(0, 47) + "...";
                }
                return shortText;
            }
        }
        public string ToolTip { get; set; }
        public ICommand ToClipBoardCommand { get; set; }
    }
}