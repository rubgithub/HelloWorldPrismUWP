using Windows.UI.Xaml.Controls;

namespace HelloWorldPrism.Views
{
    public sealed partial class MainView
    {
        //public MainViewModel ViewModel => DataContext as MainViewModel;

        public MainView()
        {
            InitializeComponent();
        }

        public void SetFrame(Frame frame)
        {
            FrameHost.Content = frame;
        }
    }
}
