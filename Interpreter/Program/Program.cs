using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Interpreter
{
    public class ExpressionProcessor
    {
        public Dictionary<char, int> Variables = new Dictionary<char, int>();

        private enum Operation { Add, Substract };

        private List<TextElement> Lex(string expression)
        {
            expression = expression.Replace(" ", "");
            var elements = new List<TextElement>();
            for (int i = 0; i < expression.Length; i++)
            {
                switch (expression[i])
                {
                    case '+':
                        elements.Add(new TextElement(TextElement.Type.Plus));
                        break;
                    case '-':
                        elements.Add(new TextElement(TextElement.Type.Minus));
                        break;
                    case '(':
                        elements.Add(new TextElement(TextElement.Type.LParen));
                        break;
                    case ')':
                        elements.Add(new TextElement(TextElement.Type.RParen));
                        break;
                    default:
                        var sb = new StringBuilder();
                        for (int j = i; j < expression.Length; j++)
                        {
                            if (char.IsLetterOrDigit(expression[j]))
                            {
                                sb.Append(expression[j]);
                            }
                            else
                            {
                                i = j - 1;
                                break;
                            }
                        }
                        var value = sb.ToString();
                        if (value.Any(c => char.IsLetter(c)) && !value.Any(c => char.IsDigit(c)))
                        {
                            elements.Add(new TextElement(TextElement.Type.Letter, value));
                        }
                        else if (value.Any(c => char.IsDigit(c)) && !value.Any(c => char.IsLetter(c)))
                        {
                            elements.Add(new TextElement(TextElement.Type.Number, value));
                        }
                        else
                        {
                            throw new Exception($"Text element of unknown type was detected at pos {elements.Count}: {value}");
                        }
                        break;
                }
            }
            return elements;
        }

        private List<ExpressionElement> Parse(List<TextElement> elements)
        {
            var expressionElements = new List<ExpressionElement>();
            for (int i = 0; i < elements.Count; i++)
            {
                switch (elements[i].ElementType)
                {
                    case TextElement.Type.Letter:
                        if (elements[i].Value.Length > 1)
                        {
                            throw new Exception($"Multi-letter variable was detected: {elements[i].Value}");
                        }
                        else if (!Variables.ContainsKey(elements[i].Value[0]))
                        {
                            throw new Exception($"Variable {elements[i].Value} has no value");
                        }
                        else
                        {
                            expressionElements.Add(new ExpressionElement(ExpressionElement.Type.Number, Variables[elements[i].Value[0]]));
                        }
                        break;
                    case TextElement.Type.LParen:
                        int j = i;
                        int parenCount = 0;
                        for (; j < elements.Count; j++)
                        {
                            if (elements[j].ElementType == TextElement.Type.LParen) parenCount++;
                            if (elements[j].ElementType == TextElement.Type.RParen) parenCount--;
                            if (parenCount == 0) break;
                        }
                        if (j == elements.Count)
                        {
                            throw new Exception("Unclosed parenthesis detected");
                        }
                        var subExpression = elements.Skip(i + 1).Take(j - i - 1).ToList();
                        expressionElements.Add(new ExpressionElement(Parse(subExpression)));
                        i = j;
                        break;
                    case TextElement.Type.Plus:
                        expressionElements.Add(new ExpressionElement(ExpressionElement.Type.Plus, 0));
                        break;
                    case TextElement.Type.Minus:
                        expressionElements.Add(new ExpressionElement(ExpressionElement.Type.Minus, 0));
                        break;
                    case TextElement.Type.Number:
                        expressionElements.Add(new ExpressionElement(ExpressionElement.Type.Number, int.Parse(elements[i].Value)));
                        break;
                }
            }
            return expressionElements;
        }

        private int Calc(List<ExpressionElement> elements)
        {
            int? leftPart = null;
            Operation? operation = null;
            for (int i = 0; i < elements.Count; i++)
            {
                switch (elements[i].ElementType)
                {
                    case ExpressionElement.Type.Number:
                        if (leftPart == null)
                        {
                            leftPart = elements[i].Value;
                        }
                        else
                        {
                            switch (operation)
                            {
                                case Operation.Add:
                                    leftPart = (int)leftPart + elements[i].Value;
                                    break;
                                case Operation.Substract:
                                    leftPart = (int)leftPart - elements[i].Value;
                                    break;
                            }
                            operation = null;
                        }
                        break;
                    case ExpressionElement.Type.Plus:
                        operation = Operation.Add;
                        break;
                    case ExpressionElement.Type.Minus:
                        operation = Operation.Substract;
                        break;
                    case ExpressionElement.Type.Sub:
                        elements[i].Value = Calc(elements[i].SubElements);
                        elements[i].ElementType = ExpressionElement.Type.Number;
                        i--;
                        break;
                }
            }
            return (int)leftPart;
        }

        public int Calculate(string expression)
        {
            try
            {
                var textElements = Lex(expression);
                var expElements = Parse(textElements);
                return Calc(expElements);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }

    public class TextElement
    {
        public enum Type
        {
            Plus, Minus, LParen, RParen, Letter, Number
        }
        public Type ElementType;
        public string Value;

        public TextElement(Type type, string value = "")
        {
            ElementType = type;
            Value = value;
        }
    }

    public class ExpressionElement
    {
        public enum Type
        {
            Plus, Minus, Sub, Number
        }
        public Type ElementType;
        public int Value;
        public bool IsComposite
        {
            get => SubElements?.Count > 0;
        }
        public List<ExpressionElement> SubElements;
        public ExpressionElement(Type type, int value = 0)
        {
            ElementType = type;
            Value = value;
        }
        public ExpressionElement(List<ExpressionElement> subElements)
        {
            ElementType = Type.Sub;
            SubElements = subElements;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var expressionProcessor = new ExpressionProcessor();
            expressionProcessor.Variables.Add('x', 3);
            var exp = "10-2-x";
            Console.WriteLine($"{exp} = {expressionProcessor.Calculate(exp)}");
        }
    }
}
