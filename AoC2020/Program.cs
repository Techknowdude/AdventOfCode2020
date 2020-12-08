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
            // get latest day and run it.
            // get all classes that are simulation days
            var assembly = Assembly.GetExecutingAssembly();
            var simulations = assembly.GetTypes().Where(t => typeof(SimulationDay).IsAssignableFrom(t)).ToList();
            simulations.Remove(typeof(SimulationDay));

            // find the one with the latest day number
            int lastDay = 0;
            foreach (var simulation in simulations)
            {
                //get name
                var name = simulation.Name.Substring(13);
                int number = Int32.Parse(name);
                if (number > lastDay)
                    lastDay = number;
            }


            RunSimulation(lastDay);
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
