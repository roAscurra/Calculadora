namespace Calculadora;

public partial class Form1 : Form
{

    private string currentExpression = "";

    public Form1()
    {
        InitializeComponent();  
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
