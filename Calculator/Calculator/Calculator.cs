using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    

    public class Calculator
    {
        public enum Operator { ADD, SUBTRACT, MULTIPLY, DIVIDE, NONE }

        public decimal CurrentValue { get; private set; } = 0;
        public Operator CurrentOperator { get; set; } = Operator.NONE;

        private decimal firstOperand = 0;
        



        

        public Calculator() {}

        public void Add(decimal number)
        {
            if(CurrentOperator == Operator.NONE)
            {
                firstOperand = number;
                CurrentOperator = Operator.ADD;
            }
            else
            {
                Equals(number);
                CurrentOperator = Operator.ADD;
            }
        }
        public void Subtract(decimal number)
        {
            if (CurrentOperator == Operator.NONE)
            {
                firstOperand = number;
                CurrentOperator = Operator.SUBTRACT;
            }
            else
            {
                Equals(number);
                CurrentOperator = Operator.SUBTRACT;
            }
        }
        public void Multiply(decimal number)
        {
            if (CurrentOperator == Operator.NONE)
            {
                firstOperand = number;
                CurrentOperator = Operator.MULTIPLY;
            }
            else
            {
                Equals(number);
                CurrentOperator = Operator.MULTIPLY;
            }
        }
        public void Divide(decimal number)
        {
            if (CurrentOperator == Operator.NONE)
            {
                firstOperand = number;
                CurrentOperator = Operator.DIVIDE;
            }
            else
            {
                Equals(number);
                CurrentOperator = Operator.DIVIDE;
            }
        }


        public void Equals(decimal number)
        {
            switch(CurrentOperator)
            {
                case Operator.ADD:
                    try { firstOperand += number; }
                    catch (OverflowException) { throw; }

                    CurrentValue = firstOperand;
                    CurrentOperator = Operator.NONE;
                    break;
                case Operator.SUBTRACT:
                    try { firstOperand -= number; }
                    catch (OverflowException) { throw; }

                    CurrentValue = firstOperand;
                    CurrentOperator = Operator.NONE;
                    break;
                case Operator.MULTIPLY:
                    try { firstOperand *= number; }
                    catch (OverflowException) { throw; }

                    CurrentValue = firstOperand;
                    CurrentOperator = Operator.NONE;
                    break;
                case Operator.DIVIDE:
                    if(number == 0)
                    {
                        throw new DivideByZeroException();
                    }
                    try { firstOperand /= number; }
                    catch (OverflowException) { throw; }

                    CurrentValue = firstOperand;
                    CurrentOperator = Operator.NONE;
                    break;
                default:
                    CurrentValue = firstOperand;
                    CurrentOperator = Operator.NONE;
                    break;
            }
        }
        public void Clear()
        {
            CurrentOperator = Operator.NONE;
            CurrentValue = 0;
            firstOperand = 0;
        }
    }
}
