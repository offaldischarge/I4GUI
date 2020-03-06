using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfCalculator
{
    /// <summary>
    /// Interaction logic for Calculator.xaml
    /// </summary>
    public partial class Calculator : Window
    {
        private Operator lastOperator = Operator.None;
        private decimal valueSoFar = 0;
        private bool numberHitSinceLastOperator = false;

        public Calculator()
        {
            InitializeComponent();
        }

        static Calculator()
        {
            EventManager.RegisterClassHandler(typeof(Button), Button.ClickEvent,
            new RoutedEventHandler(ClassButtonHandler));
        }

        private static void ClassButtonHandler(object sender, RoutedEventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();
        }

        private void OnAnyClickOnForm(object sender, RoutedEventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();
        }

        private void ExecuteLastOperator(Operator newOperator)
        {
            decimal currentValue = Convert.ToDecimal(textBoxDisplay.Text);
            decimal newValue = currentValue;
            if (numberHitSinceLastOperator)
            {
                switch (lastOperator)
                {
                    case Operator.Plus:
                        newValue = valueSoFar + currentValue;
                        break;
                    case Operator.Minus:
                        newValue = valueSoFar - currentValue;
                        break;
                    case Operator.Times:
                        newValue = valueSoFar * currentValue;
                        break;
                    case Operator.Divide:
                        if (currentValue == 0)
                            newValue = 0;
                        else
                            newValue = valueSoFar / currentValue;
                        break;
                    case Operator.Equals:
                        newValue = currentValue;
                        break;
                }
            }
            valueSoFar = newValue;
            lastOperator = newOperator;
            numberHitSinceLastOperator = false;
            textBoxDisplay.Text = valueSoFar.ToString();
        }

        private void HandleDigit(int digit)
        {
            string valueSoFar = numberHitSinceLastOperator ?
            textBoxDisplay.Text : "";
            string newValue = valueSoFar + digit.ToString();
            textBoxDisplay.Text = newValue;
            numberHitSinceLastOperator = true;
        }

        private void HandleDecimal()
        {
            string valueSoFar = numberHitSinceLastOperator ? textBoxDisplay.Text : "";
            string newValue = "";
            if (valueSoFar.IndexOf(".") < 0)
            {
                if (valueSoFar.Length == 0)
                    newValue = "0.";
                else
                    newValue = valueSoFar + ".";
            }
            else
                newValue = valueSoFar;
            textBoxDisplay.Text = newValue;
            numberHitSinceLastOperator = true;
        }

        private void OnAnyButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = e.OriginalSource as Button;
            if (btn.Tag is Operator)
                OnClickOperator(e.OriginalSource, e);
            else
                OnClickDigit(e.OriginalSource, e);
        }

        private void OnClickDigit(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int digit = Convert.ToInt32(btn.Content.ToString());
            HandleDigit(digit);
        }

        private void OnClickOperator(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Tag != null)
            {
                Operator op = (Operator)btn.Tag;
                ExecuteLastOperator(op);
                
            }
        }

        private void OnClickDecimal(object sender, RoutedEventArgs e)
        {
            HandleDecimal();
            e.Handled = true;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.D0) && (e.Key <= Key.D9))
            {
                int digit = (int)(e.Key - Key.D0);
                HandleDigit(digit);
            }
            else if ((e.Key >= Key.NumPad0) && (e.Key <= Key.NumPad9))
            {
                int digit = (int)(e.Key - Key.NumPad0);
                HandleDigit(digit);
            }
            else
            {
                switch (e.Key)
                {
                    case Key.Add:
                        ExecuteLastOperator(Operator.Plus);
                        break;
                    case Key.Subtract:
                        ExecuteLastOperator(Operator.Minus);
                        break;
                    case Key.Divide:
                        ExecuteLastOperator(Operator.Divide);
                        break;
                    case Key.Multiply:
                        ExecuteLastOperator(Operator.Times);
                        break;
                    case Key.OemPlus:
                    case Key.Enter:
                        ExecuteLastOperator(Operator.Equals);
                        break;
                    case Key.Decimal:
                        HandleDecimal();
                        break;
                }
            }
            e.Handled = true;
        }
    }
}
