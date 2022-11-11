using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using ZadanieCSV.Data;
using ZadanieCSV.Models;

namespace ZadanieCSV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<document> documents { get; set; }
        public ObservableCollection<item> items { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            documents = new ObservableCollection<document>(DocumentsData.GetDocuments());
            items = new ObservableCollection<item>(DocumentsData.GetItems(0));
        }

        private void ReadDocuments_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            dialog.FileName = "Documents";
            dialog.DefaultExt = ".csv";
            dialog.Filter = "Comma-separated values|*.csv";
            dialog.Multiselect = false;

            if (dialog.ShowDialog() != true)
            {
                return;
            }

            using (StreamReader reader = new StreamReader(dialog.FileName))
            {
                reader.ReadLine();
                while(!reader.EndOfStream)
                {
                    string[] line = reader.ReadLine().Split(',', ';');
                    if(line.Length == 6)
                    {
                        document doc = new document()
                        {
                            Id = int.Parse(line[0]),
                            Type = line[1],
                            Date = DateTime.Parse(line[2]),
                            FirstName = line[3],
                            LastName = line[4],
                            City = line[5]
                        };
                        DocumentsData.AddDocumentToDb(doc);
                        documents.Add(doc);
                    }
                }
            }
        }

        private void ReadDocumentItems_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            dialog.FileName = "DocumentItems";
            dialog.DefaultExt = ".csv";
            dialog.Filter = "Comma-separated values|*.csv";
            dialog.Multiselect = false;

            if (dialog.ShowDialog() != true)
            {
                return;
            }

            using (StreamReader reader = new StreamReader(dialog.FileName))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string[] line = reader.ReadLine().Split(';');
                    if (line.Length == 6)
                    {
                        item it = new item()
                        {
                            DocumentId = int.Parse(line[0]),
                            Ordinal = int.Parse(line[1]),
                            Product = line[2],
                            Quantity = int.Parse(line[3]),
                            Price = decimal.Parse(line[4]),
                            TaxRate = int.Parse(line[5])
                        };
                        DocumentsData.AddItemToDb(it);
                    }
                }
            }
        }

        private void Documents_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            items.Clear();
            foreach (var Data in DocumentsData.GetItems(Documents.SelectedIndex + 1))
            {
                items.Add(Data);
            }

            ItemsLabel.Visibility = Visibility.Visible;
            Items.Visibility = Visibility.Visible;
        }
    }
}
