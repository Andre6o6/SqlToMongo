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
    public partial class MongoInspector : Window
    {
        MongoDatabase db;

        public MongoInspector(MongoDatabase db)
        {
            InitializeComponent();
            this.db = db;
        }

        private void buttonLoadDbs_Click(object sender, RoutedEventArgs e)
        {
            listBoxDatabases.ItemsSource = db.ListDatabases();
        }

        private void buttonLoadCollections_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxDatabases.SelectedItem == null)
                return;

            string database = listBoxDatabases.SelectedItem.ToString();
            db.ConnectToDatabase(database);
            listBoxCollections.ItemsSource = db.ListTables();
        }

        private void buttonShowAll_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxCollections.SelectedItem == null)
                return;

            string collection = listBoxCollections.SelectedItem.ToString();
            listBoxResult.ItemsSource = db.ListDocumentsAsString(collection);
        }
    }
}
