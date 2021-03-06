using System;
using System.IO;
using System.Reflection;

namespace AoC2020
{
    abstract class SimulationDay : ISimulationDay
    {
        public string[] Input { get; set; }

        protected string Name { get { return GetType().Name; } }

        public virtual void Run()
        {
            Console.WriteLine($"---{Name} Loading Input---");
            LoadInput();
            Console.WriteLine($"---{Name} Part 1---");
            Part1();
            Console.WriteLine($"---{Name} Part 2---");
            Part2();
            Console.WriteLine($"---{Name} Complete---");
            Console.ReadKey();
        }

        public abstract void Part1();

        public abstract void Part2();

        protected virtual void LoadInput()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{GetType().Name}.txt");
            Input = File.ReadAllLines(path);
        }
    }
}