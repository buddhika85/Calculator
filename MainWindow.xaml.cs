using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SelectedOperator _selectedOperator = SelectedOperator.Nothing;
        private double _calculatedValue;
        private bool _lastInputOperator;

        public MainWindow()
        {
            InitializeComponent();
            SetDefaults();
            KeyDown += HandleKeyPress;
        }

        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            int? numberPressed = null;
            string? operationPressed = null;
            switch (e.Key)
            {
                case Key.NumPad0:
                case Key.D0:
                    numberPressed = 0;
                    break;
                case Key.NumPad1:
                case Key.D1:
                    numberPressed = 1;
                    break;
                case Key.NumPad2:
                case Key.D2:
                    numberPressed = 2;
                    break;
                case Key.NumPad3:
                case Key.D3:
                    numberPressed = 3;
                    break;
                case Key.NumPad4:
                case Key.D4:
                    numberPressed = 4;
                    break;
                case Key.NumPad5:
                case Key.D5:
                    numberPressed = 5;
                    break;
                case Key.NumPad6:
                case Key.D6:
                    numberPressed = 6;
                    break;
                case Key.NumPad7:
                case Key.D7:
                    numberPressed = 7;
                    break;
                case Key.NumPad8:
                case Key.D8:
                    numberPressed = 8;
                    break;
                case Key.NumPad9:
                case Key.D9:
                    numberPressed = 9;
                    break;
                case Key.Add:
                    operationPressed = "+";
                    break;
                case Key.Subtract:
                    operationPressed = "-";
                    break;
                case Key.Multiply:
                    operationPressed = "*";
                    break;
                case Key.Divide:
                    operationPressed = "/";
                    break;
                case Key.Enter:
                    operationPressed = "=";
                    break;
                    //case Key.Decimal:
                    //    operationPressed = ".";
                    //    break;
            }

            if (numberPressed != null)
                OnNumberInput(numberPressed.Value);
            else if (operationPressed != null)
                OnOperationInput(operationPressed);
        }

        private void SetDefaults()
        {
            ResultLbl.Content = string.Empty;
            ExpressionLbl.Content = string.Empty;
            _calculatedValue = 0.0;
            _lastInputOperator = false;
            _selectedOperator = SelectedOperator.Nothing;
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
            if (_lastInputOperator)
                return;     // you cannot press two operator buttons together
            ExpressionLbl.Content = $"{ExpressionLbl.Content}{ResultLbl.Content} = ";
            PerformOperation();
            _selectedOperator = SelectedOperator.Equal;
        }


        private void OnOperationBtnClick(object sender, RoutedEventArgs e)
        {
            if (_lastInputOperator)
                return;     // you cannot press two operator buttons together
            _lastInputOperator = true;
            var operation = GetBtnContent(sender);
            OnOperationInput(operation);
        }

        private void OnNumberBtnClick(object sender, RoutedEventArgs e)
        {
            var numberStr = GetBtnContent(sender);
            if (!int.TryParse(numberStr, out var selectedNumber))
                return;
            _lastInputOperator = false;
            OnNumberInput(selectedNumber);
        }

        /// <summary>
        /// used to manage operation key presses from GUI and keyboard
        /// </summary>
        /// <param name="operation"></param>
        private void OnOperationInput(string? operation)
        {
            if (_selectedOperator != SelectedOperator.Nothing)
            {
                ExpressionLbl.Content = $"{ExpressionLbl.Content}{ResultLbl.Content}{operation}";
                PerformOperation(); // previous calculation
            }
            else
            {
                ExpressionLbl.Content = $"{ResultLbl.Content}{operation}";
            }

            SetSelectedOperator(operation);
        }

        /// <summary>
        /// used to manage number key presses from GUI and keyboard
        /// </summary>
        /// <param name="selectedNumber"></param>
        private void OnNumberInput(int selectedNumber)
        {
            if (_selectedOperator == SelectedOperator.Equal)
            {
                // previous calculation is complete, starting a new
                SetDefaults();
                ResultLbl.Content = $"{selectedNumber}";
            }
            else if (_selectedOperator != SelectedOperator.Nothing)
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
    }

    public enum SelectedOperator
    {
        Equal,

        Addition,
        Subtract,
        Multiply,
        Divide,
        Nothing
    }
}
