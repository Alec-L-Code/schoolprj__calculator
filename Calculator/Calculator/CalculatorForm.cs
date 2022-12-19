using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
/*
 * This is a really messy implementation of a calculator. I wanted to add some standard functionality that most
 * calculators have and I probably ended up putting a bunch of unnecessary repetitive code through out here because of my
 * brain farts. If you see something that's repetitive and completely unneeded I'm sorry.
 */
namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        private Calculator calculator;

        public CalculatorForm()
        {
            InitializeComponent();
            calculator = new Calculator();
            IO_TextBox.Text = "0";
        }

        // A method to change the text of the IO textbox. Handles zeros and any letters.
        private void ChangeIOText(string text, bool ignoreParse = false)
        {
            if(ignoreParse)
            {
                IO_TextBox.Text = text;
                return;
            }
            if (text == string.Empty) { IO_TextBox.Text = string.Empty; return; }
            if (IO_TextBox.Text == "0") { IO_TextBox.Text = text; }
            else if (IO_TextBox.Text == "-0") { IO_TextBox.Text = text; }
            else if (IO_TextBox.Text.Contains("*")) { IO_TextBox.Text = text; }
            else if (Regex.IsMatch(IO_TextBox.Text, @"[a-zA-Z]")) { IO_TextBox.Text = text; }
            else { IO_TextBox.Text += text; }
        }
        // An event handler for every number click
        private void Calculator_Number_Click(object sender, EventArgs e)
        {
            Button parsedSender = (Button)sender;
            string numberClickedStr = (string)(parsedSender.Tag); // Gets the string object from the tag associated with the button clicked
            parsedSender.Focus();
            ChangeIOText(numberClickedStr);
        }


        private void Calculator_Addition_Click(object sender, EventArgs e)
        {
            if(decimal.TryParse(IO_TextBox.Text, out decimal number))
            {
                calculator.Add(number);
                ChangeIOText(string.Empty);
                ChangeIOText("0");
                if(calculator.CurrentValue != 0m)
                {
                    ChangeIOText("*" + calculator.CurrentValue, true);
                }
            }
            else
            {
                calculator.CurrentOperator = Calculator.Operator.ADD;
            }
        }
        private void Calculator_Subtraction_Click(object sender, EventArgs e)
        {
            if(decimal.TryParse(IO_TextBox.Text, out decimal number))
            {
                calculator.Subtract(number);
                ChangeIOText(string.Empty);
                ChangeIOText("0");
                if (calculator.CurrentValue != 0m)
                {
                    ChangeIOText("*" + calculator.CurrentValue, true);
                }
            }
            else
            {
                calculator.CurrentOperator = Calculator.Operator.SUBTRACT;
            }
        }
        private void Calculator_Multiply_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(IO_TextBox.Text, out decimal number))
            {
                calculator.Multiply(number);
                ChangeIOText(string.Empty);
                ChangeIOText("0");
                if (calculator.CurrentValue != 0m)
                {
                    ChangeIOText("*" + calculator.CurrentValue, true);
                }
            }
            else
            {
                calculator.CurrentOperator = Calculator.Operator.MULTIPLY;
            }
        }
        private void Calculator_Divide_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(IO_TextBox.Text, out decimal number))
            {
                try
                {
                    calculator.Divide(number);
                } catch(DivideByZeroException) { return; }
                ChangeIOText(string.Empty);
                ChangeIOText("0");
                if (calculator.CurrentValue != 0m)
                {
                    ChangeIOText("*" + calculator.CurrentValue, true);
                }
            }
            else
            {
                calculator.CurrentOperator = Calculator.Operator.DIVIDE;
            }
        }
        private void Calculator_Clear_Click(object sender, EventArgs e)
        {
            calculator.Clear();
            ChangeIOText(string.Empty);
            ChangeIOText("0");
        }
        private void Calculator_Back_Click(object sender, EventArgs e)
        {
            try 
            {
                if (IO_TextBox.Text == "0")
                {
                    return;
                }
                string parsedBackString = IO_TextBox.Text.Remove(IO_TextBox.Text.Length - 1, 1);
                if(parsedBackString == string.Empty)
                {
                    ChangeIOText("0", true);
                    return;
                }
                ChangeIOText(parsedBackString, true);
            }
            catch(ArgumentOutOfRangeException) { ChangeIOText("0", true); }
        }
        private void Calculator_Invert_Click(object sender, EventArgs e)
        {
            if(IO_TextBox.Text == string.Empty) { return; }
            if(IO_TextBox.Text.Contains("-"))
            {
                ChangeIOText(IO_TextBox.Text.Replace("-", string.Empty), true);
            }
            else
            {
                ChangeIOText(IO_TextBox.Text.Insert(0, "-"), true);
            }
        }
        private void Calculator_Decimal_Click(object sender, EventArgs e)
        {
            if (IO_TextBox.Text == string.Empty) { return; }
            if (IO_TextBox.Text.Contains("."))
            {
                return;
            }
            else
            {
                ChangeIOText(IO_TextBox.Text += ".", true);
            }
        }
        private void Calculator_Reciprocal_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(IO_TextBox.Text, out decimal number))
            {
                try
                {
                    decimal recip = 1m / number;
                    ChangeIOText(recip.ToString(), true);
                }
                catch (OverflowException) { }
                catch (DivideByZeroException) { ChangeIOText("ERROR: DIV BY ZERO", true); calculator.Clear(); return; }
            }
        }
        private void Calculator_Sqrt_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(IO_TextBox.Text, out decimal number))
            {
                try
                {
                    decimal sqrt = (decimal)Math.Sqrt((double)number);
                    ChangeIOText(sqrt.ToString(), true);
                } catch(OverflowException) { ChangeIOText("ERROR: IMAGINARY NUM", true); calculator.Clear(); return; }
            }
        }
        private void Calculator_Equals_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(IO_TextBox.Text, out decimal number))
            {
                try
                {
                    calculator.Equals(number);
                } catch(DivideByZeroException) { ChangeIOText("ERROR: DIV BY ZERO", true); calculator.Clear(); return; }
                ChangeIOText(calculator.CurrentValue.ToString(), true);
            }
            else
            {
                ChangeIOText(calculator.CurrentValue.ToString(), true);
            }
        }
    }
}
