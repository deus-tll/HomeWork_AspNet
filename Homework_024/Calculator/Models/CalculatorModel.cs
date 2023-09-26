using System.Data;

namespace Calculator.Models
{
    public class CalculatorModel
    {
        public string DisplayText { get; set; } = "0";
        public string FirstNumber { get; set; } = "0";
        public string Action { get; set; } = "";
        public string SecondNumber { get; set; } = "0";
        public bool ClickedEqual { get; set; } = false;
        public string Memory { get; set; } = "0";

        public void UpdateNumber(string input)
        {
            if (Action == "")
            {
                if (FirstNumber == "0" || ClickedEqual)
                {
                    FirstNumber = input;
                    ClickedEqual = false;
                }
                else if (input == "." && !FirstNumber.Contains('.'))
                    FirstNumber += input;

                else if (input != ".")
                    FirstNumber += input;
            }
            else
            {
                if (SecondNumber == "0")
                    SecondNumber = input;

                else if (input == "." && !SecondNumber.Contains('.'))
                    SecondNumber += input;

                else if (input != ".")
                    SecondNumber += input;
            }
        }

        public bool TryCalculateResult()
        {
            if (!string.IsNullOrEmpty(FirstNumber) && !string.IsNullOrEmpty(SecondNumber) && !string.IsNullOrEmpty(Action))
            {
                string expression = $"{FirstNumber} {Action} {SecondNumber}";
                DisplayText = CalculateResult(expression);
                FirstNumber = DisplayText;
                SecondNumber = "";
                Action = "";
                return true;
            }
            return false;
        }

        public void ResetCalculator()
        {
            FirstNumber = "0";
            Action = "";
            SecondNumber = "0";
            DisplayText = "0";
        }

        public void ClearEntry()
        {
            if (Action == "")
                FirstNumber = "";
            else
                SecondNumber = "";
        }

        public void DeleteDigit()
        {
            if (Action == "")
            {
                if (FirstNumber.Length > 0)
                    FirstNumber = FirstNumber[..^1];
            }
            else
            {
                if (SecondNumber.Length > 0)
                    SecondNumber = SecondNumber[..^1];
            }
        }

        public void CalculateSquareRootAction()
        {
            if (Action == "")
            {
                if (FirstNumber != "0")
                    FirstNumber = CalculateSquareRoot(FirstNumber);
            }
            else
            {
                if (SecondNumber != "0")
                    SecondNumber = CalculateSquareRoot(SecondNumber);
            }
        }

        public void CalculateSquareAction()
        {
            if (Action == "")
            {
                if (FirstNumber != "0")
                    FirstNumber = CalculateSquare(FirstNumber);
            }
            else
            {
                if (SecondNumber != "0")
                    SecondNumber = CalculateSquare(SecondNumber);
            }
        }

        public void CalculateInverseAction()
        {
            if (Action == "")
            {
                if (FirstNumber != "0")
                    FirstNumber = CalculateInverse(FirstNumber);
            }
            else
            {
                if (SecondNumber != "0")
                    SecondNumber = CalculateInverse(SecondNumber);
            }
        }

        public void ToggleSignAction()
        {
            if (Action == "")
            {
                if (FirstNumber != "0")
                    FirstNumber = ToggleSign(FirstNumber);
            }
            else
            {
                if (SecondNumber != "0")
                    SecondNumber = ToggleSign(SecondNumber);
            }
        }

        private static string CalculateResult(string expression)
        {
            try
            {
                DataTable dataTable = new();
                var result = dataTable.Compute(expression, "");

                return result.ToString() ?? "";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public static bool IsNumeric(string value)
        {
            return decimal.TryParse(value, out _);
        }

        private static string ToggleSign(string number)
        {
            if (decimal.TryParse(number, out decimal numericValue))
            {
                numericValue = -numericValue;
                return numericValue.ToString();
            }
            return number;
        }

        private static string CalculateInverse(string number)
        {
            if (decimal.TryParse(number, out decimal numericValue) && numericValue != 0)
            {
                numericValue = 1 / numericValue;
                return numericValue.ToString();
            }
            return number;
        }

        private static string CalculateSquare(string number)
        {
            if (decimal.TryParse(number, out decimal numericValue))
            {
                numericValue *= numericValue;
                return numericValue.ToString();
            }
            return number;
        }

        private static string CalculateSquareRoot(string number)
        {
            if (decimal.TryParse(number, out decimal numericValue) && numericValue >= 0)
            {
                numericValue = (decimal)Math.Sqrt((double)numericValue);
                return numericValue.ToString();
            }
            return number;
        }

        public void ClearMemory()
        {
            Memory = "0";
        }

        public void RecallMemory()
        {
            FirstNumber = Memory;
            if (Action == "")
            {
                DisplayText = FirstNumber;
            }
            else
            {
                DisplayText = SecondNumber;
            }
        }

        public void AddToMemory()
        {
            if (Action == "")
            {
                if (decimal.TryParse(FirstNumber, out decimal firstNumericValue))
                {
                    if (decimal.TryParse(Memory, out decimal memoryValue))
                    {
                        Memory = (memoryValue + firstNumericValue).ToString();
                    }
                }
            }
            else
            {
                if (decimal.TryParse(SecondNumber, out decimal secondNumericValue))
                {
                    if (decimal.TryParse(Memory, out decimal memoryValue))
                    {
                        Memory = (memoryValue + secondNumericValue).ToString();
                    }
                }
            }
        }

        public void SubtractFromMemory()
        {
            if (Action == "")
            {
                if (decimal.TryParse(FirstNumber, out decimal firstNumericValue))
                {
                    if (decimal.TryParse(Memory, out decimal memoryValue))
                    {
                        Memory = (memoryValue - firstNumericValue).ToString();
                    }
                }
            }
            else
            {
                if (decimal.TryParse(SecondNumber, out decimal secondNumericValue))
                {
                    if (decimal.TryParse(Memory, out decimal memoryValue))
                    {
                        Memory = (memoryValue - secondNumericValue).ToString();
                    }
                }
            }
        }

        public void StoreInMemory()
        {
            if (Action == "")
            {
                if (decimal.TryParse(FirstNumber, out decimal firstNumericValue))
                {
                    Memory = firstNumericValue.ToString();
                }
            }
            else
            {
                if (decimal.TryParse(SecondNumber, out decimal secondNumericValue))
                {
                    Memory = secondNumericValue.ToString();
                }
            }
        }
    }
}