using Caliburn.Micro;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using TextGrunt.Messages;
using TextGrunt.Services;
using TextGrunt.ViewModels;

namespace TextGrunt
{
    public class Bootstrapper : BootstrapperBase
    {
        private StandardKernel _kernel = new StandardKernel();
        private IHandle<ToggleMainViewVisibleMessage> _mainViewDisplayMessageHandler;
        private IBookService _bookService;
        private Mutex _singleAppMutex;

        public Bootstrapper()
        {
            Initialize();
        }

        public static string CommandlineOnlyTrayBar = "onlytraybar";

        protected override void Configure()
        {
            _kernel.Bind<StandardKernel>().ToConstant(_kernel);
            _kernel.Bind<IStorageService>().To<JsonStorageService>();
            _kernel.Bind<IOptionsService>().To<OptionsService>().InSingletonScope();
            _kernel.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            _kernel.Bind<IDialogService>().To<DialogService>().InSingletonScope();
            _kernel.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();
            _kernel.Bind<IBookService>().To<BookService>().InSingletonScope();
            _kernel.Bind<IHandle<ToggleMainViewVisibleMessage>>().To<ToggleMainViewVisibleMessageHandler>().InSingletonScope();
            _kernel.Bind<IClipboardService>().To<ClipboardService>().InSingletonScope();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            bool isNewInstance = false;
            _singleAppMutex = new Mutex(true, "TextGruntSingleInstanceLock", out isNewInstance);
            if (!isNewInstance)
            {
                //Application is already running elsewhere, shut this down.
                Application.Current.Shutdown();
            }

            base.OnStartup(sender, e);
            _mainViewDisplayMessageHandler = _kernel.Get<IHandle<ToggleMainViewVisibleMessage>>();
            _bookService = _kernel.Get<IBookService>();
            DisplayRootViewFor<SystemTrayViewModel>();

            if (!Environment.GetCommandLineArgs().Contains(CommandlineOnlyTrayBar))
                _mainViewDisplayMessageHandler.Handle(new ToggleMainViewVisibleMessage() { Visible = true });
        }

        protected override object GetInstance(Type service, string key)
        {
            return _kernel.Get(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _kernel.GetAll(service);
        }

        protected override void BuildUp(object instance)
        {
            _kernel.Inject(instance);
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            _kernel?.Dispose();
            _singleAppMutex?.Dispose();
            base.OnExit(sender, e);
        }
    }
}