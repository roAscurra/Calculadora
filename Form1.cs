namespace Calculadora;

public partial class Form1 : Form
{

    private string currentExpression = "";

    public Form1()
    {
        InitializeComponent();  
    }
    private void clickOperador(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        currentExpression += button.Text; // Agregar el texto del botón, no solo la etiqueta (Tag).
        txtResultado.Text = currentExpression;
    }
    private void btnResultado_Click(object sender, EventArgs e)
    {
        try
        {
            double result = EvaluateExpression(currentExpression);
            txtResultado.Text = result.ToString();
            currentExpression = result.ToString();
            }
        catch (Exception ex)
        {
            txtResultado.Text = "Error: " + ex.Message;
        }
    }
     private double EvaluateExpression(string expression)
    {
        var numberStack = new Stack<double>();
        var operatorStack = new Stack<char>();

        int i = 0;
        while (i < expression.Length)
        {
            char c = expression[i];

            if (c == ' ')
            {
                i++;
                continue;
            }

            if (IsDigit(c) || (c == '-' && (i == 0 || IsOperator(expression[i - 1]))))
            {
                string numStr = "";
                while (i < expression.Length && (IsDigit(expression[i]) || expression[i] == '.'))
                {
                    numStr += expression[i];
                    i++;
                }
                i--;

                double num = ParseDouble(numStr);
                numberStack.Push(num);
            }
            else if (c == '(')
            {
                operatorStack.Push(c);
            }
            else if (c == ')')
            {
                while (operatorStack.Count > 0 && operatorStack.Peek() != '(')
                {
                    ComputeTopOperation(numberStack, operatorStack);
                }
                operatorStack.Pop(); // Pop '('
            }
            else if (IsOperator(c))
            {
                while (operatorStack.Count > 0 && Precedence(operatorStack.Peek()) >= Precedence(c))
                {
                    ComputeTopOperation(numberStack, operatorStack);
                }
                operatorStack.Push(c);
            }
            else
            {
                throw new Exception("Carácter no válido: " + c);
            }

            i++;
        }

        while (operatorStack.Count > 0)
        {
            ComputeTopOperation(numberStack, operatorStack);
        }

        if (numberStack.Count == 1 && operatorStack.Count == 0)
        {
            return numberStack.Pop();
            }
        else
        {
            throw new Exception("Expresión inválida");
        }
    }
    private bool IsOperator(char c)
    {
        return (c == '+' || c == '-' || c == 'x' || c == '/');
    }

    private bool IsDigit(char c)
    {
        return (c >= '0' && c <= '9') || c == '.';
    }
    private double ParseDouble(string s)
    {
        double result = 0.0;
        double fraction = 0.1;
        bool isFraction = false;
        bool isNegative = false;

        foreach (char c in s)
        {
            if (c == '-')
            {
                isNegative = true;
            }
            else if (c == '.')
            {
                isFraction = true;
            }
            else
            {
                int digit = c - '0';
                result = result * 10 + digit;
                if (isFraction)
                {
                    result += digit * fraction;
                    fraction *= 0.1;
                }
            }
        }

        if (isNegative)
        {
            result = -result;
        }

        return result;
    }

    private int Precedence(char op)
    {
        if (op == '+' || op == '-')
            return 1;
        if (op == 'x' || op == '/')
            return 2;
        return 0;
    }

    private void ComputeTopOperation(Stack<double> numberStack, Stack<char> operatorStack)
    {
        char op = operatorStack.Pop();
        double b = numberStack.Pop();
        double a = numberStack.Pop();

        double result = 0;
        switch (op)
        {
            case '+':
                result = a + b;
                break;
            case '-':
                result = a - b;
                break;
            case 'x':
                result = a * b;
                break;
               case '/':
                if (b == 0)
                {
                    throw new DivideByZeroException();
                }
                result = a / b;
                break;
        }

        numberStack.Push(result);
    }
    private void btnPunto_Click(object sender, EventArgs e)
    {
        if (currentExpression.Length == 0 || !currentExpression.EndsWith(".") && !currentExpression.Contains("."))
        {
            currentExpression += ".";
            txtResultado.Text = currentExpression;
        }
    }

}
