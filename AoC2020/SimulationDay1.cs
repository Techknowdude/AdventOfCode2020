using System;
using System.IO;
using System.Reflection;

namespace AoC2020
{
    class SimulationDay1 : ISimulation
    {
        public string[] Input { get; set; }

        public void Run()
        {
            LoadInput();
            Part1();
            Part2();
        }

        public void Part1()
        {
            int number1 = 0;
            int number2 = 0;
            int result = 0;

            for (int f = 0; f < Input.Length && result == 0; f++)
            {
                number1 = Int32.Parse(Input[f]);

                for (int s = 1; s < Input.Length && result == 0; s++)
                {
                    number2 = Int32.Parse(Input[s]);
                    if (number1 + number2 == 2020)
                        result = number1 * number2;
                }
            }

            Console.WriteLine($"Day1 Part1 results:\nNumber1: {number1}\nNumber2: {number2}\nResult: {result}\n\n");
        }

        public void Part2()
        {
            int number1 = 0;
            int number2 = 0;
            int number3 = 0;
            int result = 0;

            for (int f = 0; f < Input.Length && result == 0; f++)
            {
                number1 = Int32.Parse(Input[f]);

                for (int s = f+1; s < Input.Length && result == 0; s++)
                {
                    number2 = Int32.Parse(Input[s]);
                    for (int t = s+1; t < Input.Length && result == 0; t++)
                    {
                        number3 = Int32.Parse(Input[t]);
                        if (number1 + number2 + number3 == 2020)
                            result = number1*number2*number3;
                    }
                }
            }

            Console.WriteLine($"Day1 Part2 results:\nNumber1: {number1}\nNumber2: {number2}\nNumber3: {number3}\nResult: {result}");
            Console.ReadKey();
        }

        public void LoadInput()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Day1.txt");
            Input = File.ReadAllLines(path);
        }
    }
}