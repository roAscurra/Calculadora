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
