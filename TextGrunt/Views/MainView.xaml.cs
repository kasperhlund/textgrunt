using System.Windows;
using System.Windows.Controls;

namespace TextGrunt.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = Window.GetWindow(this);
        }
    }
}