using Caliburn.Micro;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TextGrunt.Messages;
using TextGrunt.ViewModels;

namespace TextGrunt.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        IEventAggregator _eventAggregator;

        public MainView(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            InitializeComponent();

        }

        void TabItem_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var tabItem = e.Source as TabItem;

            if (tabItem == null)
                return;

            if (Mouse.PrimaryDevice.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(tabItem, tabItem, DragDropEffects.All);
            }
        }


        void TabItem_Drop(object sender, DragEventArgs e)
        {
            var target = (e.Source as TabItem)?.Content as IShellTabItem;
            var source = (e.Data.GetData(typeof(TabItem)) as TabItem)?.Content as IShellTabItem;

            if (target.Equals(source) || target == null || source == null)
                return;

            _eventAggregator.PublishOnUIThread(new MoveTabMessage { Source = source, Target = target });
        }

    }
}