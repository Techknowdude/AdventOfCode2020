using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020
{
    class Program
    {
        static void Main(string[] args)
        {
            RunSimulation(7);
        }

        private static void RunSimulation(params int[] days)
        {
            foreach (var day in days)
            {
                var handlerName = typeof(SimulationDay).Name + day;
                SimulationDay sim = CreateInstance(handlerName) as SimulationDay;

                if (sim == null)
                    throw new InstanceNotFoundException("Could not find an instance of the class SimulationDay" + day +
                                                        " in the current assembly with a default constructor.");

                sim.Run();
            }
        }

        private static object CreateInstance(string className)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var type = assembly.GetTypes()
                .FirstOrDefault(t => t.Name == className);

            return type == null ? null : Activator.CreateInstance(type);
        }
    }
}
