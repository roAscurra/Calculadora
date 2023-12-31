namespace Calculadora
{

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
        private void agregarNumero(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            currentExpression += button.Text;
            if(currentExpression.Contains("0-")){
                txtResultado.Text = currentExpression.Replace("0-", "-");
            }else{
                txtResultado.Text = currentExpression;
            }
        }

        private void clickOperador(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (button.Text == "-" && (string.IsNullOrEmpty(currentExpression) || currentExpression.EndsWith("(") || currentExpression.EndsWith("(-")))
            {
                currentExpression += "0" + button.Text; // Agrega un 0 antes del '-' para manejar números negativos.
            }
            else if (IsOperator(button.Text[0]) && !string.IsNullOrEmpty(currentExpression) && !IsOperator(currentExpression[currentExpression.Length - 1]) && currentExpression[currentExpression.Length - 1] != '(')
            {
                currentExpression += button.Text;
            }
            else if (button.Text == "(")
            {
                if (!string.IsNullOrEmpty(currentExpression) && (char.IsDigit(currentExpression[currentExpression.Length - 1]) || currentExpression[currentExpression.Length - 1] == ')'))
                {
                    MessageBox.Show("Formato inválido: Debe colocar un operador antes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Muestra un mensaje de error si se intenta agregar un paréntesis de apertura después de un número o un paréntesis de cierre.
                }
                currentExpression += button.Text;
            }
            else if (button.Text != "-" && !string.IsNullOrEmpty(currentExpression) && !IsOperator(currentExpression[currentExpression.Length - 1]))
            {
                currentExpression += button.Text;
            }
            if(currentExpression.Contains("0-")){
            txtResultado.Text = currentExpression.Replace("0-", "-");
            }else{
                txtResultado.Text = currentExpression;
            }
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
                        MessageBox.Show("No es posible dividir entre cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if(currentExpression.Contains("0-")){
                    txtResultado.Text = currentExpression.Replace("0-", "-");
                }else{
                    txtResultado.Text = currentExpression;
                }
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
                MessageBox.Show("El formato usado no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private bool IsDigit(char c)
        {
            return (c >= '0' && c <= '9') || c == '.';
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
        private void btnPunto_Click(object sender, EventArgs e)
        {
            if (currentExpression.Length == 0 || (char.IsDigit(currentExpression[currentExpression.Length - 1]) && !currentExpression.EndsWith(".")))
            {
                currentExpression += ".";
                txtResultado.Text = currentExpression.Replace("0-","-");
            }
        }
        private void btnSigno_Click(object sender, EventArgs e)
        {
       if (currentExpression.Length > 0)
        {
            if (currentExpression.StartsWith("0-"))
            {
                currentExpression = currentExpression.Substring(2);
            }
            else
            {
                currentExpression = "0-" + currentExpression;
            }

            txtResultado.Text = currentExpression;
        }
    }

    }
}