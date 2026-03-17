using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7_8_ForComposite
{
    abstract class Shape
    {
        public abstract void Draw();

        public abstract void Add(Shape shape);

        public abstract void Remove(Shape shape);
    }

    class CompositeShape : Shape
    {
        private List<Shape> children = new List<Shape>();

        public override void Add(Shape shape)
        {
            children.Add(shape);
        }

        public override void Remove(Shape shape)
        {
            children.Remove(shape);
        }

        public override void Draw()
        {
            Console.WriteLine("Drawing Composite Shape:");

            foreach (Shape shape in children)
            {
                shape.Draw();
            }
        }
    }

    class Circle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Drawing Circle");
        }

        public override void Add(Shape shape)
        {
            Console.WriteLine("Cannot add.");
        }

        public override void Remove(Shape shape)
        {
            Console.WriteLine("Cannot remove.");
        }
    }

    class Rectangle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Drawing Rectangle");
        }

        public override void Add(Shape shape)
        {
            Console.WriteLine("Cannot add.");
        }

        public override void Remove(Shape shape)
        {
            Console.WriteLine("Cannot remove.");
        }
    }

    class Triangle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Drawing Triangle");
        }

        public override void Add(Shape shape)
        {
            Console.WriteLine("Cannot add.");
        }

        public override void Remove(Shape shape)
        {
            Console.WriteLine("Cannot remove.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Створення фігур
            Shape circle = new Circle();
            Shape rectangle = new Rectangle();
            Shape triangle = new Triangle();

            // Група фігур 1
            CompositeShape group1 = new CompositeShape();
            group1.Add(circle);
            group1.Add(rectangle);

            // Група фігур 2, об'єднання
            CompositeShape group2 = new CompositeShape();
            group2.Add(triangle);
            group2.Add(group1);

            group2.Draw();

            Console.ReadLine();
        }
    }
}
