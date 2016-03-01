using System.Windows;

namespace Fotm.Server.UI.Dialogs
{
    /// <summary>
    /// Interaction logic for SetDatasourceDialog.xaml
    /// </summary>
    public partial class SetDatasourceDialog 
    {
        public SetDatasourceDialog()
        {
            InitializeComponent();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
