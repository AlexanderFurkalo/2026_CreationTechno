using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7_8_ForDecorator
{
    abstract class IceCream
    {
        public abstract string GetDescription();
        public abstract double GetCost();
    }

    class SimpleIceCream : IceCream
    {
        public override string GetDescription()
        {
            return "Vanilla Ice Cream";
        }

        public override double GetCost()
        {
            return 2.0;
        }
    }

    abstract class IceCreamDecorator : IceCream
    {
        protected IceCream iceCream;

        public IceCreamDecorator(IceCream iceCream)
        {
            this.iceCream = iceCream;
        }

        public override string GetDescription()
        {
            return iceCream.GetDescription();
        }

        public override double GetCost()
        {
            return iceCream.GetCost();
        }
    }

    class ChocolateDecorator : IceCreamDecorator
    {
        public ChocolateDecorator(IceCream iceCream) : base(iceCream)
        {
        }

        public override string GetDescription()
        {
            return iceCream.GetDescription() + ", Chocolate";
        }

        public override double GetCost()
        {
            return iceCream.GetCost() + 0.7;
        }
    }

    class NutsDecorator : IceCreamDecorator
    {
        public NutsDecorator(IceCream iceCream) : base(iceCream)
        {
        }

        public override string GetDescription()
        {
            return iceCream.GetDescription() + ", Nuts";
        }

        public override double GetCost()
        {
            return iceCream.GetCost() + 0.8;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IceCream iceCream = new SimpleIceCream();

            Console.WriteLine(iceCream.GetDescription() + " | Cost: " + iceCream.GetCost());

            iceCream = new ChocolateDecorator(iceCream);

            Console.WriteLine(iceCream.GetDescription() + " | Cost: " + iceCream.GetCost());

            iceCream = new NutsDecorator(iceCream);

            Console.WriteLine(iceCream.GetDescription() + " | Cost: " + iceCream.GetCost());

            Console.ReadLine();
        }
    }
}
