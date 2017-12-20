using Caliburn.Micro;
using System;
using System.Dynamic;
using System.Windows;
using System.Windows.Media.Imaging;
using TextGrunt.Messages;
using TextGrunt.ViewModels;

namespace TextGrunt.Services
{
    public class ToggleMainViewVisibleMessageHandler : IHandle<ToggleMainViewVisibleMessage>
    {
        private IWindowManager _windowManager;
        private MainViewModel _mainViewModel;

        public ToggleMainViewVisibleMessageHandler(IWindowManager windowManager, MainViewModel mainViewModel, IEventAggregator eventAggregator)
        {
            _windowManager = windowManager;
            _mainViewModel = mainViewModel;
            eventAggregator.Subscribe(this);
        }

        public void Handle(ToggleMainViewVisibleMessage message)
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStyle = WindowStyle.ToolWindow;
            settings.ShowInTaskbar = true;
            settings.ResizeMode = ResizeMode.NoResize;
            settings.Icon = new BitmapImage(new Uri("pack://application:,,,/../Assets/Textgrunt.ico"));

            if (message.Visible && _mainViewModel.IsClosed)
            {
                _windowManager.ShowWindow(_mainViewModel, null, settings);
            }
            else if (!message.Visible && !_mainViewModel.IsClosed)
            {
                _mainViewModel.TryClose();
            }
        }
    }
}