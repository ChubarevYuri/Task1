using System.Windows;
using System.Windows.Controls;

namespace Task1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        readonly MainWindowVM VM = new MainWindowVM();

        public MainWindow()
        {
            DataContext = VM;
            InitializeComponent();
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {
                CounterRow selectedRow = (CounterRow)dataGrid.SelectedItem;
                if (selectedRow != null)
                {
                    VM.SelectCountry = selectedRow.Object;
                }
            }
        }
    }
}
