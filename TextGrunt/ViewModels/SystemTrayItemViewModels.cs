using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TextGrunt.ViewModels
{
    public class SystemTrayItemsViewModel : IHaveIcon
    {
        IconType _iconType = IconType.None;

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
