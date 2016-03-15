using System;
using System.Windows;
using System.Windows.Threading;
using Fotm.Server.UI.Dialogs;
using Fotm.Server.UI.Views;
using Fotm.Server.Util;

namespace Fotm.Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private MainViewModel _mainVm;

        public MainWindow()
        {
            InitializeComponent();

            // Quartz.net logging
            Common.Logging.LogManager.Adapter =
                new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter { Level = Common.Logging.LogLevel.Info };

            _mainVm = new MainViewModel();
            DataContext = _mainVm;

            OpenDebug();

            Closing += (o, s) =>
            {
                _mainVm.CleanUp();
                _consoleRedirectWriter?.Release();
            };
        }

        #region Console Debug View

        ConsoleRedirectWriter _consoleRedirectWriter = new ConsoleRedirectWriter();
        private ConsoleOutputView _consoleView = new ConsoleOutputView();

        /// <summary>
        /// On click, opens the view and redirects console.
        /// </summary>
        private void DebugOpen_OnClick(object sender, RoutedEventArgs e)
        {
            OpenDebug();
        }

        private void OpenDebug()
        {
            _consoleRedirectWriter.OnWrite += OnWrite;
            DebugTab.Content = _consoleView;
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
            var dlg = new SetDatasourceDialog { DataContext = _mainVm };
            dlg.ShowDialog();
        }

        #endregion
    }
}
