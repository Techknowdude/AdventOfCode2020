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
            RunSimulation(1);
        }

        private static void RunSimulation(int day)
        {
            ISimulation sim = CreateInstance("SimulationDay" + day) as ISimulation;

            if(sim == null)
                throw new InstanceNotFoundException("Could not find an instance of the class SimulationDay" + day + " in the current assembly with a default constructor.");

            sim.Run();
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
