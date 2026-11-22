using System.Windows;
using Lab_7.ViewModels;
namespace Lab_7{
    public partial class MainWindow : Window{
        private MainViewModel viewModel = new MainViewModel();
        public MainWindow(){
            InitializeComponent();
        }
        private void AnalyzeButton_Click(object sender, RoutedEventArgs e){
            viewModel.InputText = InputTextBox.Text;
            viewModel.Analyze();
            ResultsGrid.ItemsSource = viewModel.Results;
            TotalText.Text = viewModel.TotalCount.ToString();
            RecognizedText.Text = viewModel.RecognizedCount.ToString();
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e){
            viewModel.Clear();
            InputTextBox.Clear();
            ResultsGrid.ItemsSource = null;
            TotalText.Text = "0";
            RecognizedText.Text = "0";
        }
    }
}
