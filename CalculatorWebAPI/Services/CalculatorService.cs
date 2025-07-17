using CalculatorWebAPI.Models;

namespace CalculatorWebAPI.Services
{
    public class CalculatorService
    {
        public Calculator Add(int op1, int op2)
        {
            return new Calculator
            {
                operand1 = op1,
                operand2 = op2,
                operation = "+",
                result = op1 + op2
            };
        }

        public Calculator Subtract(int op1, int op2)
        {
            return new Calculator
            {
                operand1 = op1,
                operand2 = op2,
                operation = "-",
                result = op1 - op2
            };
        }

        public Calculator Multiply(int op1, int op2)
        {
            return new Calculator
            {
                operand1 = op1,
                operand2 = op2,
                operation = "*",
                result = op1 * op2
            };
        }

        public Calculator Divide(int op1, int op2)
        {
            if (op2 == 0)
                throw new DivideByZeroException("Division by zero is not allowed.");

            return new Calculator
            {
                operand1 = op1,
                operand2 = op2,
                operation = "/",
                result = op1 / op2
            };
        }

        public Calculator Calculate(Calculator calculator)
        {
            switch (calculator.operation)
            {
                case "+":
                    calculator.result = calculator.operand1 + calculator.operand2;
                    break;
                case "-":
                    calculator.result = calculator.operand1 - calculator.operand2;
                    break;
                case "*":
                    calculator.result = calculator.operand1 * calculator.operand2;
                    break;
                case "/":
                    if (calculator.operand2 == 0)
                        throw new DivideByZeroException("Division by zero is not allowed.");
                    calculator.result = calculator.operand1 / calculator.operand2;
                    break;
                default:
                    throw new ArgumentException("Invalid operation");
            }

            return calculator;
        }
    }
}
