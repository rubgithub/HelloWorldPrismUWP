using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace HelloWorldPrism.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
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


        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavegarProximaCommand = new DelegateCommand(NavegarProxima).ObservesCanExecute(() => IsNextEnable);
            NavegarVoltaCommand =   new DelegateCommand(NavegarVolta).ObservesCanExecute(() => CanGoBack);
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
