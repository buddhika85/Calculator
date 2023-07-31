using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SelectedOperator? _selectedOperator = null;
        private double calculatedResult = 0.0;
        public MainWindow()
        {
            InitializeComponent();
            SetDefaults();
        }

        private void SetDefaults()
        {
            ResultLbl.Content = string.Empty;
            ExpressionLbl.Content = string.Empty;
        }


        private void AcBtn_OnClick(object sender, RoutedEventArgs e)
        {
            SetDefaults();
        }

        private void NegativeBtn_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void PercentageBtn_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void EqualBtn_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void DotBtn_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void OnOperationBtnClick(object sender, RoutedEventArgs e)
        {


        }

        private void OnNumberBtnClick(object sender, RoutedEventArgs e)
        {
            var btnContent = ((Button)sender).Content.ToString();
            if (int.TryParse(btnContent, out var selectedNumber))
            {

                ResultLbl.Content = $"{ResultLbl.Content}{selectedNumber}";
            }

        }
    }

    public enum SelectedOperator
    {
        Addition,
        Subtract,
        Multiply,
        Divide
    }
}
