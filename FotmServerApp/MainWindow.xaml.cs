using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FotmServerApp.Database;
using FotmServerApp.Database.DataProvider;
using FotmServerApp.WowAPI;

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
            //var test = @"C:\Users\andre\FotmTest.db";
            //DbManager.Instance.SetDataProvider(DataProviderFactory.DataProviderType.Sqlite, test);
            //var stuff = WowAPIManager.GetPvpStats();
            //DbManager.Instance.InsertObjects(stuff);

            Closing += (o, s) => DbManager.Instance.Dispose();
        }
    }
}
