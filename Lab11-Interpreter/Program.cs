using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab11_Interpreter
{
    class Context
    {
        Dictionary<string, int> variables;
        public Context()
        {
            variables = new Dictionary<string, int>();
        }
        // отримуємо значення змінної за її ім'ям
        public int GetVariable(string name)
        {
            return variables[name];
        }
        public void SetVariable(string name, int value)
        {
            if (variables.ContainsKey(name))
                variables[name] = value;
            else
                variables.Add(name, value);
        }
    }
    // інтерфейс інтерпретатора
    interface IExpression
    {
        int Interpret(Context context);
    }
    // термінальний вираз
    class NumberExpression : IExpression
    {
        string name; // ім'я змінної
        public NumberExpression(string variableName)
        {
            name = variableName;
        }
        public int Interpret(Context context)
        {
            return context.GetVariable(name);
        }
    }
    // нетермінальний вираз для додавання
    class AddExpression : IExpression
    {
        IExpression leftExpression;
        IExpression rightExpression;
        public AddExpression(IExpression left, IExpression right)
        {
            leftExpression = left;
            rightExpression = right;
        }
        public int Interpret(Context context)
        {
            return leftExpression.Interpret(context) + rightExpression.Interpret(context);
        }
    }
    // нетермінальний вираз для віднімання
    class SubtractExpression : IExpression
    {
        IExpression leftExpression;
        IExpression rightExpression;
        public SubtractExpression(IExpression left, IExpression right)
        {
            leftExpression = left;
            rightExpression = right;
        }
        public int Interpret(Context context)
        {
            return leftExpression.Interpret(context) - rightExpression.Interpret(context);
        }
    }

    class MultiplyExpression : IExpression
    {
        IExpression leftExpression;
        IExpression rightExpression;

        public MultiplyExpression(IExpression left, IExpression right)
        {
            leftExpression = left;
            rightExpression = right;
        }

        public int Interpret(Context context)
        {
            return leftExpression.Interpret(context) * rightExpression.Interpret(context);
        }
    }

    class DivideExpression : IExpression
    {
        IExpression leftExpression;
        IExpression rightExpression;

        public DivideExpression(IExpression left, IExpression right)
        {
            leftExpression = left;
            rightExpression = right;
        }

        public int Interpret(Context context)
        {
            int right = rightExpression.Interpret(context);

            if (right == 0)
                throw new DivideByZeroException();

            return leftExpression.Interpret(context) / right;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Context context = new Context();
            // визначаємо набір змінних
            int x = 5;
            int y = 8;
            int z = 2;
            int v = 4;
            // додаємо змінні в контекст
            context.SetVariable("x", x);
            context.SetVariable("y", y);
            context.SetVariable("z", z);
            context.SetVariable("v", v);
            // створюємо об'єкт для обчислення виразу x + y
            var addExpr = new AddExpression(new NumberExpression("x"), new NumberExpression("y"));
            int results1 = addExpr.Interpret(context);
            Console.WriteLine($"Результат: {results1}");
            // створюємо об'єкт для обчислення виразу (x + y) - z
            var subExpr = new SubtractExpression(addExpr, new NumberExpression("z"));
            Console.WriteLine(subExpr);
            int results2 = subExpr.Interpret(context);
            Console.WriteLine($"Результат: {results2}");
            // ((x + y) - z) * x
            var multExpr = new MultiplyExpression(subExpr, new NumberExpression("x"));
            int results3 = multExpr.Interpret(context);
            Console.WriteLine($"Результат: {results3}");
            // (((x + y) - z) * x) / v
            IExpression expression = new DivideExpression(multExpr, new NumberExpression("v"));
            int result4 = expression.Interpret(context); 
            Console.WriteLine($"Результат: {result4}");

            IExpression expression2 = new AddExpression(new AddExpression(new NumberExpression("x"),new NumberExpression("y")),
                new AddExpression(new NumberExpression("z"),new NumberExpression("v")));
            int result5 = expression2.Interpret(context);
            Console.WriteLine(result5);
            Console.Read();
        }
    }
}
