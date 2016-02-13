using System.Windows.Controls;

namespace FotmServerApp.UI.Views
{
    /// <summary>
    /// Interaction logic for ConsoleOutputView.xaml
    /// </summary>
    public partial class ConsoleOutputView : UserControl
    {

        public ConsoleOutputView()
        {
            InitializeComponent();
        }

        private bool _autoScroll = true;
        private void ScrollViewer_OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange == 0)
            {
                _autoScroll = ScrollViewer.VerticalOffset == ScrollViewer.ScrollableHeight;
            }

            if (_autoScroll && e.ExtentHeightChange != 0)
            {
                ScrollViewer.ScrollToVerticalOffset(ScrollViewer.ExtentHeight);
            }
        }
    }
}
