using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace TextGrunt
{
    public class CustomDataGrid : DataGrid
    {
        public CustomDataGrid()
        {
            SelectionChanged += (sender, eventArgs) => SelectedItemsList = SelectedItems;
        }

        public IList SelectedItemsList
        {
            get { return (IList)GetValue(SelectedItemsListProperty); }
            set { SetValue(SelectedItemsListProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemsListProperty =
        DependencyProperty.Register("SelectedItemsList", typeof(IList), typeof(CustomDataGrid), new PropertyMetadata(null));
    }
}