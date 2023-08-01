using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SelectedOperator _selectedOperator = SelectedOperator.Nothing;
        private double _calculatedValue = 0.0;

        public MainWindow()
        {
            InitializeComponent();
            SetDefaults();
        }

        private void SetDefaults()
        {
            ResultLbl.Content = string.Empty;
            ExpressionLbl.Content = string.Empty;
            _calculatedValue = 0.0;
        }


        private void AcBtn_OnClick(object sender, RoutedEventArgs e)
        {
            SetDefaults();
        }

        private void NegativeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultLbl.Content.ToString(), out var result))
            {
                ResultLbl.Content = result * -1;
            }

        }

        private void PercentageBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultLbl.Content.ToString(), out var result))
            {

                ResultLbl.Content = result / 100;
            }
        }

        private void EqualBtn_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void DotBtn_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void OnOperationBtnClick(object sender, RoutedEventArgs e)
        {
            var operation = GetBtnContent(sender);
            if (_selectedOperator != SelectedOperator.Nothing)
            {
                ExpressionLbl.Content = $"{ExpressionLbl.Content}{ResultLbl.Content}{operation}";
                PerformOperation();     // previous calculation
            }
            else
            {
                ExpressionLbl.Content = $"{ResultLbl.Content}{operation}";
            }
            SetSelectedOperator(operation);
        }

        private void OnNumberBtnClick(object sender, RoutedEventArgs e)
        {
            var numberStr = GetBtnContent(sender);
            if (int.TryParse(numberStr, out var selectedNumber))
            {
                if (_selectedOperator != SelectedOperator.Nothing)
                {
                    if (_calculatedValue == 0.0)
                    {
                        double.TryParse(ResultLbl.Content.ToString(), out _calculatedValue);
                        ResultLbl.Content = $"{selectedNumber}";
                    }
                    else
                    {
                        if (_selectedOperator == SelectedOperator.Nothing)
                            ResultLbl.Content = $"{ResultLbl.Content}{selectedNumber}";
                        else
                            ResultLbl.Content = $"{selectedNumber}";
                    }
                }
                else
                {
                    ResultLbl.Content = $"{ResultLbl.Content}{selectedNumber}";
                }

            }

        }

        private void PerformOperation()
        {
            if (double.TryParse(ResultLbl.Content.ToString(), out var lastNumber))
            {
                switch (_selectedOperator)
                {
                    case SelectedOperator.Addition:
                        _calculatedValue += lastNumber;
                        break;
                    case SelectedOperator.Subtract:
                        _calculatedValue -= lastNumber;
                        break;
                    case SelectedOperator.Multiply:
                        _calculatedValue *= lastNumber;
                        break;
                    case SelectedOperator.Divide:
                        _calculatedValue /= lastNumber;
                        break;
                    case SelectedOperator.Nothing:
                        break;
                }
            }
            _selectedOperator = SelectedOperator.Nothing;
            //ExpressionLbl.Content = lastNumber;
            ResultLbl.Content = _calculatedValue;
        }

        private string? GetBtnContent(object sender)
        {
            return ((Button)sender).Content?.ToString();
        }

        private void SetSelectedOperator(string? operation)
        {
            switch (operation)
            {
                case "+":
                    _selectedOperator = SelectedOperator.Addition;
                    break;
                case "-":
                    _selectedOperator = SelectedOperator.Subtract;
                    break;
                case "*":
                    _selectedOperator = SelectedOperator.Multiply;
                    break;
                case "/":
                    _selectedOperator = SelectedOperator.Divide;
                    break;
                default:
                    _selectedOperator = SelectedOperator.Nothing;
                    break;
            }
        }

        private string GetSelectedOperator()
        {
            switch (_selectedOperator)
            {
                case SelectedOperator.Addition:
                    return "+";
                case SelectedOperator.Subtract:
                    return "-";
                case SelectedOperator.Multiply:
                    return "*";
                case SelectedOperator.Divide:
                    return "/";
                case SelectedOperator.Nothing:
                    return "";
                default:
                    return "";
            }
        }
    }

    public enum SelectedOperator
    {
        Addition,
        Subtract,
        Multiply,
        Divide,
        Nothing
    }
}
