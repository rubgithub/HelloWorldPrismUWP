using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace HelloWorldPrism.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public DelegateCommand NavegarPriximaCommand { get; private set; }

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavegarPriximaCommand = new DelegateCommand(NavegarPrixima);
        }

        private void NavegarPrixima()
        {
            _navigationService.Navigate("First", null);
        }
    }
}
