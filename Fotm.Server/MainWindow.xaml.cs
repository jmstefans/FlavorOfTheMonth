
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Fotm.Server.UI.Dialogs;
using Fotm.Server.UI.Views;
using Fotm.Server.Util;
using MahApps.Metro.Controls;
using Xceed.Wpf.AvalonDock.Layout;

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

            Closing += (o, s) =>
            {
                _mainVm.CleanUp();
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
            //OpenDebugView();
            OpenDebug();
        }

        //private void OpenDebugView()
        //{
        //    if (RightPaneGroup.Children.Contains(_consoleLayout))
        //    {
        //        if (!_consoleLayout.IsActive)
        //            _consoleLayout.IsActive = true;
        //        return;
        //    }

        //    _consoleRedirectWriter.OnWrite += OnWrite;

        //    _consoleLayout.Closed += (o, s) => _consoleRedirectWriter.Release();
        //    _consoleLayout.Content = _consoleView;

        //    RightPaneGroup.InsertChildAt(0, _consoleLayout);
        //    _consoleLayout.IsActive = true;
        //}

        private void OpenDebug()
        {
            var tab = TabzMain.FindChild<TabItem>(nameof(_consoleView));
            if (tab != null)
            {
                tab.IsSelected = true;
                return;
            }

            _consoleRedirectWriter.OnWrite += OnWrite;

            tab = new TabItem
            {
                Content = _consoleView,
                Name = nameof(_consoleView),
                Header = "Debug Output",
                IsSelected = true
            };
            TabzMain.AddToSource(tab);
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
