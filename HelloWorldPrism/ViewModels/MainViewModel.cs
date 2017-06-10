using HelloWorldPrism.Repository;
using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace HelloWorldPrism.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IProductRepository _productRepository;

        public DelegateCommand NavegarProximaCommand { get; }
        public DelegateCommand NavegarVoltaCommand { get; }

        private bool _isNextEnable = true;
        public bool IsNextEnable
        {
            get { return _isNextEnable; }
            set { SetProperty(ref _isNextEnable, value); }
        }
        private bool _canGoBack;
        public bool CanGoBack
        {
            get { return _canGoBack; }
            set { SetProperty(ref _canGoBack, value); }
        }


        public MainViewModel(INavigationService navigationService, IProductRepository productRepository)
        {
            _navigationService = navigationService;
            _productRepository = productRepository;

            NavegarProximaCommand = new DelegateCommand(NavegarProxima).ObservesCanExecute(() => IsNextEnable);
            NavegarVoltaCommand =   new DelegateCommand(NavegarVolta).ObservesCanExecute(() => CanGoBack);

            //TestIoC();
        }

        private void TestIoC()
        {
            _productRepository.Add("New produtct");
            System.Diagnostics.Debug.Write(_productRepository.Get());
        }


        private void NavegarVolta()
        {
            //_navigationService.GoBack();
        }

        private void NavegarProxima()
        {
            IsNextEnable = false;
            CanGoBack = true;
            _navigationService.Navigate("First", null);
        }
    }
}
