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

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            documents = new ObservableCollection<document>(DocumentsData.GetDocuments());
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

                    }
                }
            }
        }
    }
}
