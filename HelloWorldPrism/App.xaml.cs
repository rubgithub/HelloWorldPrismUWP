using Autofac;
using HelloWorldPrism.Repository;
using HelloWorldPrism.Views;
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
        public App()
        {
            InitializeComponent();
        }
        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            Window.Current.Activate();
            return Task.FromResult(true);
        }

        protected override UIElement CreateShell(Frame rootFrame)
        {
            var shell = Container.Resolve<MainView>();
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

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);

            builder.RegisterType<ProductRepository>().As<IProductRepository>();
        }
    }
}
