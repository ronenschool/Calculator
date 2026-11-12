
namespace Calculator;

public partial class Calculator : ContentPage
{
    char[] mainarr = new char[256];
    int index = 0;

    public Calculator()
    {
        InitializeComponent();
    }

    private int EvaluateArr(bool RespectOrderOfOperations)
    {
        if (RespectOrderOfOperations == true)
        {
            return 0;
        }

        while (true)
        {
            int firstOperator = 0;
            int secondOperator = 0;
            int operatorsFound = 0;

            for (int i = 2; i <= index; i++)
            {
                if (mainarr[i] == '+' || mainarr[i] == '-' || mainarr[i] == '*' || mainarr[i] == '/')
                {
                    operatorsFound++;

                    if (operatorsFound == 2)
                    {
                        secondOperator = i;
                        break;
                    }
                }
            }

            int end = 0;

            if (secondOperator == 0)
            {
                end = index + 1;
            }
            else
            {
                end = secondOperator;
            }

            for (int i = 2; i < end; i++)
            {
                if (mainarr[i] == '+' || mainarr[i] == '-' || mainarr[i] == '*' || mainarr[i] == '/')
                {
                    firstOperator = i;
                    break;
                }
            }

            if (firstOperator == 0)
            {
                break;
            }

            int assert = Replace(end);
            if (assert == 0)
            {
               return 0;
            }
        }
        return 1;
    }

    private int Replace(int count)
    {
        int operatorPos = 0;

        for (int i = 2; i < count; i++)
        {
            if (mainarr[i] == '+' || mainarr[i] == '-' || mainarr[i] == '*' || mainarr[i] == '/')
            {
                operatorPos = i;
                break;
            }
        }

        if (operatorPos == 0)
        {
            return 1;
        }

        long first = long.Parse(new string(mainarr, 1, operatorPos - 1));

        long second = long.Parse(new string(mainarr, operatorPos + 1, count - operatorPos - 1));

        long result;

        switch (mainarr[operatorPos])
        {
            case '+':
                result = first + second;
                break;

            case '-':
                result = first - second;
                break;

            case '*':
                result = first * second;
                break;

            case '/':
                if (second == 0)
                {
                    return 0;
                }

                result = first / second;
                break;

            default:
                return 1;
        }

        string remaining = "";

        if (count <= index)
        {
            remaining = new string( mainarr, count, index - count + 1);
        }

        string expression = result.ToString() + remaining;

        Array.Clear(mainarr, 0, mainarr.Length);

        for (int i = 0; i < expression.Length; i++)
        {
            mainarr[i + 1] = expression[i];
        }

        index = expression.Length;

        return 1;
    }

    private void DrawResult()
    {
        lblResult.Text = new string(mainarr, 1, index);
        index = lblResult.Text.Length;
    }

    private void OnButtonClicked(object sender, EventArgs e)
    {
        if (sender is not Button button)
            return;

        string value = button.Text;

        if (lblResult.Text == "ERORO cant divide by zero")
        {
            index = 0;
            Array.Clear(mainarr, 0, mainarr.Length);
            lblResult.Text = "";
        }

        if (value == "ac")
        {
            index = 0;
            Array.Clear(mainarr, 0, mainarr.Length);
            lblResult.Text = "";
            return;
        }

        if (value == "=")
        {
            if (mainarr[index] == '+' || mainarr[index] == '-' || mainarr[index] == '*' || mainarr[index] == '/')
            {
                return;
            }
            bool RespectOrderOfOperations = false;
            int assert = EvaluateArr(RespectOrderOfOperations);
            if (assert == 0)
            {
                lblResult.Text = "ERORO cant divide by zero";
                return;
            }
            DrawResult();
            return;
        }

        if (value == "+" || value == "-" || value == "*" || value == "/")
        {
            if (mainarr[index] == '+' || mainarr[index] == '-' || mainarr[index] == '*' || mainarr[index] == '/')
            {
                return;
            }
        }

        index += 1;

        char c = value[0];
        mainarr[index] = c;
        lblResult.Text += c;
    }
}