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
using System.Windows.Shapes;

namespace SqlToMongo
{
    /// <summary>
    /// Interaction logic for SqlInspector.xaml
    /// </summary>
    public partial class SqlInspector : Window
    {
        PostgreSqlDatabase db;

        public SqlInspector(PostgreSqlDatabase db)
        {
            InitializeComponent();
            this.db = db;
        }

        private void buttonLoadDbs_Click(object sender, RoutedEventArgs e)
        {
            listBoxDatabases.ItemsSource = db.ListDatabases();
        }

        private void buttonLoadTables_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxDatabases.SelectedItem == null)
                return;

            string database = listBoxDatabases.SelectedItem.ToString();
            db.ConnectToDatabase(database);
            listBoxTables.ItemsSource = db.ListTables();
        }

        private void buttonShowAll_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxTables.SelectedItem == null)
                return;

            string table = listBoxTables.SelectedItem.ToString();
            dataGridResult.ItemsSource = db.LoadTable(table).DefaultView;
        }
    }
}
