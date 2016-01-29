
namespace FotmServerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
            
            var vm = new MainViewModel();
            DataContext = vm;
            Closing += (o, s) => vm.CleanUp();
        }
    }
}
