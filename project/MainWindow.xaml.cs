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

namespace SqlToMongo
{
    public partial class MainWindow : Window
    {
        Connector connector;
        //remove
        MongoDatabase mongo;
        PostgreSqlDatabase sql;

        public MainWindow()
        {
            InitializeComponent();

            //todo remove
            mongo = new MongoDatabase();
            sql = new PostgreSqlDatabase();
        }

        private void buttonInspectMongo_Click(object sender, RoutedEventArgs e)
        {
            var w = new MongoInspector(mongo);
            w.ShowDialog();
        }

        private void buttonInspectPostgres_Click(object sender, RoutedEventArgs e)
        {
            var w = new SqlInspector(sql);
            w.ShowDialog();
        }
    }
}
