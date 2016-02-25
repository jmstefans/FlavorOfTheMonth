
using System;
using System.Windows;
using System.Windows.Threading;
using Fotm.Server.UI.Views;
using Fotm.Server.Util;
using Xceed.Wpf.AvalonDock.Layout;

namespace Fotm.Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            // Quartz.net logging
            Common.Logging.LogManager.Adapter =
                new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter { Level = Common.Logging.LogLevel.Info };

            var vm = new MainViewModel();
            DataContext = vm;

            OpenDebugView();

            Closing += (o, s) =>
            {
                vm.CleanUp();
                _consoleRedirectWriter?.Release();
            };
        }

        #region Console Debug View

        ConsoleRedirectWriter _consoleRedirectWriter = new ConsoleRedirectWriter();
        private ConsoleOutputView _consoleView = new ConsoleOutputView();

        private LayoutAnchorable _consoleLayout = new LayoutAnchorable
        {
            ContentId = "DebugOutput",
            Title = "Debug Output",
        };

        /// <summary>
        /// On click, opens the view and redirects console.
        /// </summary>
        private void DebugOpen_OnClick(object sender, RoutedEventArgs e)
        {
            if (RightPaneGroup.Children.Contains(_consoleLayout))
            {
                if (!_consoleLayout.IsActive)
                    _consoleLayout.IsActive = true;
                return;
            }

            OpenDebugView();
        }

        private void OpenDebugView()
        {
            _consoleRedirectWriter.OnWrite += OnWrite;

            _consoleLayout.Closed += (o, s) => _consoleRedirectWriter.Release();
            _consoleLayout.Content = _consoleView;

            RightPaneGroup.InsertChildAt(0, _consoleLayout);
            _consoleLayout.IsActive = true;
        }

        private void OnWrite(string s)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (Action<string>)delegate (string value)
               {
                   _consoleView.ConsoleTextBox.AppendText(value);
                   _consoleView.ConsoleTextBox.ScrollToEnd();
               }, s);
        }

        #endregion

        #region Set Data Source

        private void SetDataSource_OnClick(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }
}
