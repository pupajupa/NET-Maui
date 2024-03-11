namespace MauiAppAntikhovitch.View;

public partial class CalculatorPage : ContentPage
{
    int currentState = 1;
    string? operation;
    decimal firstNumber, secondNumber;
    public CalculatorPage()
    {
        InitializeComponent();
        OnClearClicked(this, null);
    }
    void OnClearClicked(object sender, EventArgs? e)
    {
        firstNumber = secondNumber = 0;
        currentState = 1;
        LResult.Text = "0";
    }
        
    void NumberClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string btnPressed = button.Text;
        if (LResult.Text == "0" || currentState < 0)
        {
            LResult.Text = string.Empty;
            if (currentState < 0)
            {
                currentState *= -1;
            }
        }
        LResult.Text += btnPressed;
        decimal number = 0;
        if (decimal.TryParse(this.LResult.Text, out number))
        {
            if (currentState == 1)
            {
                firstNumber = number;
            }
            else
            {
                secondNumber = number;
            }
            LResult.Text = number.ToString();
        }
    }
    void RootClicked(object sender, EventArgs e)
    {
        if (firstNumber >= 0)
        {
            firstNumber = (decimal)Math.Sqrt((double)firstNumber);
            LResult.Text = firstNumber.ToString();
        }
        else
        {
            OnClearClicked(this, null);
            LResult.Text = "Error";
            currentState = -1;
        }
    }
    void SquareClicked(object sender, EventArgs e)
    {
        if (firstNumber == 0)
        {
            return;
        }
        try
        {
            firstNumber = firstNumber * firstNumber;
            LResult.Text = firstNumber.ToString();
        }
        catch (OverflowException)
        {
            OnClearClicked(this, null);
            LResult.Text = "Error";
            currentState = -1;
        }
    }
    void OperatorSelectedClicked(object sender, EventArgs e)
    {
        currentState = -2;
        Button button = (Button)sender;
        string btnPressed = button.Text;
        operation = btnPressed;
    }
    void CommaClicked(object sender, EventArgs e)
    {
        if (!LResult.Text.Contains(","))
        {
            LResult.Text += ",";
        }
    }
    void ExponentiationClicked(object sender, EventArgs e)
    {
        currentState = -2;
        Button button = (Button)sender;
        string btnPressed = button.Text;
        operation = btnPressed;
    }

    void PlusOrMinusClicked(object sender, EventArgs e)
    {
        if (firstNumber != 0)
        {
            firstNumber = -firstNumber;
        }
        LResult.Text = firstNumber.ToString();
    }
    void EqualsClicked(object sender, EventArgs e)
    {
        if (currentState == 2)
        {
            decimal result = 0;
            try
            {
                switch (operation)
                {
                    case "÷":
                        result = firstNumber / secondNumber;
                        break;
                    case "×":
                        result = firstNumber * secondNumber;
                        break;
                    case "−":
                        result = firstNumber - secondNumber;
                        break;
                    case "+":
                        result = firstNumber + secondNumber;
                        break;
                    case "^":
                        result = (decimal)Math.Pow((double)firstNumber, (double)secondNumber);
                        break;
                    default:
                        break;
                }
                LResult.Text = result.ToString();
                firstNumber = result;
            }
            catch (OverflowException)
            {
                OnClearClicked(this, null);
                LResult.Text = "Error";
            }
            currentState = -1;
        }
    }
}