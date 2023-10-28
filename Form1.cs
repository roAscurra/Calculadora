namespace Calculadora;

public partial class Form1 : Form
{

    private string currentExpression = "";

    public Form1()
    {
        // Configurar la configuración regional de Windows para usar el punto (.) como separador decimal
        var cultureInfo = new System.Globalization.CultureInfo("en-US");
        cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
        Thread.CurrentThread.CurrentCulture = cultureInfo;
        Thread.CurrentThread.CurrentUICulture = cultureInfo;
        InitializeComponent();
    }


    private void clickOperador(object sender, EventArgs e)
    {
        Button button = (Button)sender;

        if (button.Text == "-" && string.IsNullOrEmpty(currentExpression))
        {
            currentExpression += "0" + button.Text; // Agrega un 0 antes del '-' para manejar números negativos.
        }
        else if (IsOperator(button.Text[0]) && !string.IsNullOrEmpty(currentExpression) && !IsOperator(currentExpression[currentExpression.Length - 1]) && currentExpression[currentExpression.Length - 1] != '(')
        {
            currentExpression += button.Text;
        }
        else if (button.Text == "(" || button.Text == ")" || button.Text != "-" && !string.IsNullOrEmpty(currentExpression) && !IsOperator(currentExpression[currentExpression.Length - 1]))
        {
            currentExpression += button.Text;
        }

        txtResultado.Text = currentExpression.Replace("0-", "-");
    }

    private void agregarNumero(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        currentExpression += button.Text;
        txtResultado.Text = currentExpression.Replace("0-", "-");
    }

    private bool IsOperator(char c)
    {
        return (c == '+' || c == '-' || c == 'x' || c == '/');
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
    private void btnBorrar_Click(object sender, EventArgs e)
    {
        if (currentExpression.Length > 0)
        {
            currentExpression = currentExpression.Substring(0, currentExpression.Length - 1);
            txtResultado.Text = currentExpression.Replace("0-", "-");
        }
    }
    private void btnBorrarTodo_Click(object sender, EventArgs e)
    {
        currentExpression = "";
        txtResultado.Text = "0";
    }
    private void btnResultado_Click(object sender, EventArgs e)
    {
        try
        {
            double result = EvaluateExpression(currentExpression);
            txtResultado.Text = result.ToString();
            currentExpression = result.ToString();
        }
        catch (Exception)
        {
            // Si la expresión no es válida, no hagas nada y simplemente regresa sin mostrar un mensaje de error.
            return;
        }
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
                if (isFraction)
                {
                    result += digit * fraction;
                    fraction *= 0.1;
                }
                else
                {
                    result = result * 10 + digit;
                }
            }
        }

        if (isNegative)
        {
            result = -result;
        }

        return result;
    }
    private double EvaluateExpression(string expression)
    { return 0;}
    private void btnPunto_Click(object sender, EventArgs e)
    {
        if (currentExpression.Length == 0 || !currentExpression.EndsWith(".") && !currentExpression.Contains("."))
        {
            //Comprueba si la expresión actual está vacía o no termina con un punto decimal
            currentExpression += ".";
            txtResultado.Text = currentExpression;
        }
    }

}
