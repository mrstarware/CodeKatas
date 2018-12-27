using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeKatas {
    public class Runes {
        const int noSolutions = -1;

        public static int PerformOperation (char operation, int one, int two) {
            switch (operation) {
                case '*':
                    return one - two;
                case '+':
                    return one + two;
                case '-':
                    return one - two;
            }
            throw new ArgumentOutOfRangeException (nameof (operation));
        }

        public static int Calculate (List<char> operations, List<int> numbers) {

            // Not an equation
            if (operations.Count == 0) {

                if (numbers.Count == 0) {
                    return noSolutions; // There is no calculation to be done and also no value.
                }
                return numbers.First ();
            }

            var result = numbers[0];
            for (var index = 1; index < numbers.Count; index++) {
                var operation = operations[index - 1];
                var num2 = numbers[index];
                result = PerformOperation (operation, result, num2);
            }

            return result;

        }

        public static int ComputeAnswer (string expression) {
            
            var operations = new Stack<char> ();
            var numbers = new Stack<int> ();

            var numberString = "";
            var parsedNumber = 0;
            var isValidLocationForNegative = true;

            for (var characterIndex = 0; characterIndex < expression.Length; characterIndex++) {

                var currentCharacter = expression[characterIndex];
                var isLeadingNegative = (isValidLocationForNegative && currentCharacter == '-');
                if (isLeadingNegative || char.IsDigit (currentCharacter)) {
                    numberString += currentCharacter;
                    isValidLocationForNegative = false;
                } else {
                    parsedNumber = int.Parse (numberString);
                    numberString = "";
                    numbers.Push (parsedNumber);
                    operations.Push (currentCharacter);
                    isValidLocationForNegative = true;
                }
            }
            if (numberString.Length > 0) {
                parsedNumber = int.Parse (numberString);
                numbers.Push (parsedNumber);
            }

            var resultSideOne = Calculate (operations.Reverse ().ToList (), numbers.Reverse ().ToList ());
            return resultSideOne;
        }

        public static bool IsEquationBalanced (string equation) {
            var sidesOfEquation = equation.Split ('=');
            var leftHandSide = ComputeAnswer(sidesOfEquation[0]);
            var rightHandSide = ComputeAnswer(sidesOfEquation[1]);
            return leftHandSide == rightHandSide;
        }

        public static bool IsInvalidStartValue (string expression) => expression.StartsWith ("?0") || expression.StartsWith ("??");

        public static bool IsZeroInvalid (string expression) {
            var sides = expression.Split ('=');
            return (IsInvalidStartValue (sides[0]) || IsInvalidStartValue (sides[1]));

        }

        public static bool IsIncorrectFormat (string expression) {
            var equalSignCount = expression.Count (x => x.Equals ('='));
            return (equalSignCount != 1);
        }

        public static List<char> GetAlreadyKnownDigits (string expression) {
            return expression.Where (x => char.IsDigit (x)).Distinct ().ToList ();
        }

        public static List<int> GetPossibleAnswers (string expression) {
            var possibleDigits = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var isZeroInvalid = IsZeroInvalid (expression);
            if (isZeroInvalid) {
                possibleDigits.Remove (0);
            }

            var alreadyKnownDigits = GetAlreadyKnownDigits (expression);
            possibleDigits.RemoveAll (x => alreadyKnownDigits.Contains ($"{x}" [0]));

            return possibleDigits;
        }

        public static bool IsSolutionToEquation (string equation, int possibleAnswer) {
            var equationWithReplacedQuestionMarks = equation.Replace ('?', possibleAnswer.ToString () [0]);
            return IsEquationBalanced (equationWithReplacedQuestionMarks);

        }

        public static int solveExpression (string equation) {

            if (IsIncorrectFormat (equation)) {
                return noSolutions;
            }

            var possibleAnswers = GetPossibleAnswers (equation);

            var answers = possibleAnswers.Where (possibility => IsSolutionToEquation (equation, possibility)).ToList ();
            if (answers.Count () > 0) {
                return answers.First ();
            }

            return noSolutions;
        }
    }
}