using System;

namespace BeverageTemplateMethod
{
    public abstract class Beverage
    {
        public void PrepareRecipe()
        {
            BoilWater();
            Brew();
            PourInCup();
            if (CustomerWantsCondiments())
            {
                AddCondiments();
            }
        }

        protected abstract void Brew();
        protected abstract void AddCondiments();

        private void BoilWater() => Console.WriteLine("Boiling water");

        private void PourInCup() => Console.WriteLine("Pouring into cup");

        protected virtual bool CustomerWantsCondiments() => true;
    }

    public class Tea : Beverage
    {
        protected override void Brew() => Console.WriteLine("Steeping the tea");

        protected override void AddCondiments() => Console.WriteLine("Adding lemon");

        protected override bool CustomerWantsCondiments()

        {
            Console.WriteLine("Would you like lemon with your tea (y/n)?");
            var answer = Console.ReadLine();
            return answer?.ToLower() == "y";
        }
    }

    public class Coffee : Beverage
    {
        protected override void Brew() => Console.WriteLine("Dripping coffee through filter");

        protected override void AddCondiments() => Console.WriteLine("Adding sugar and milk");

        protected override bool CustomerWantsCondiments()
        {
            Console.WriteLine("Would you like sugar and milk with your coffee (y/n)?");
            var answer = Console.ReadLine();
            return answer?.ToLower() == "y";
        }
    }

    public class Program
    {
        public static void Main()
        {
            var tea = new Tea();
            var coffee = new Coffee();

            Console.WriteLine("Preparing tea:");
            tea.PrepareRecipe();

            Console.WriteLine("\nPreparing coffee:");
            coffee.PrepareRecipe();
        }
    }
}
