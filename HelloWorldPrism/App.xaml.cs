using HelloWorldPrism.Services;
using HelloWorldPrism.Views;
using Microsoft.Practices.ServiceLocation;
using Prism.Logging;
using Prism.Mvvm;
using Prism.SimpleInjector.Windows;
using SimpleInjector;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HelloWorldPrism
{
    sealed partial class App 
    {
        public new static PrismSimpleInjectorApplication Current => (PrismSimpleInjectorApplication)Application.Current;
        public new Container Container { get; private set; }

        public App()
        {
            InitializeComponent();
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            Window.Current.Activate();
            return Task.FromResult(true);
        }

        protected override void CreateAndConfigureContainer()
        {
            Logger.Log("Creating and Configuring Container", Category.Debug, Priority.Low);
            Container = CreateContainer();
            if (Container == null)
            {
                throw new InvalidOperationException("Simple Injector container is null");
            }

            Logger.Log("Configuring Container", Category.Debug, Priority.Low);
            ConfigureContainer();
            Logger.Log("Configuring ServiceLocator", Category.Debug, Priority.Low);
            ConfigureServiceLocator();
        }

        protected override Container CreateContainer()
        {
            return new Container();
        }

        protected override void ConfigureContainer()
        {            
            Container.Register<IProductRepository, ProductRepository>(Lifestyle.Transient);
        }


        protected override UIElement CreateShell(Frame rootFrame)
        {
            var shell = Container.GetInstance<MainView>();
            shell.SetFrame(rootFrame);
            return shell;
        }

        protected override Type GetPageType(string pageToken)
        {
            var type = Type.GetType(string.Format(CultureInfo.InvariantCulture, GetType().AssemblyQualifiedName.Replace(GetType().FullName, GetType().Namespace + ".Views.{0}View"), pageToken));
            if (type != null)
                return type;
            throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, ResourceLoader.GetForCurrentView("/Prism.Windows/Resources/").GetString("DefaultPageTypeLookupErrorMessage"), pageToken, GetType().Namespace + ".Views"), nameof(pageToken));
        }

        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            Container.RegisterSingleton(SessionStateService);
            Container.RegisterSingleton(DeviceGestureService);
            Container.RegisterSingleton(NavigationService);
            Container.RegisterSingleton(EventAggregator);

            ViewModelLocationProvider.SetDefaultViewModelFactory(viewMo‌​delType => Container.GetInstance(viewMo‌​delType));
            return Task.CompletedTask;
        }

        protected override void ConfigureViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => new SimpleInjectorServiceLocatorAdapter(Container));
        }

    }
}
