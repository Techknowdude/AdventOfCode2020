using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020
{
    class SimulationDay13 : SimulationDay
    {
        #region Part1

        public override void Part1()
        {
            int timeReady = Int32.Parse(Input[0]);
            var split = Input[1].Split(new [] {',','x'}, StringSplitOptions.RemoveEmptyEntries);

            List<int> buses = split.Select(s => Int32.Parse(s)).ToList();

            int currentTime = timeReady;
            int busToTake = -1;
            do
            {
                foreach (var bus in buses)
                {
                    if (currentTime%bus == 0)
                    {
                        busToTake = bus;
                        break;
                    }
                }
                currentTime++;
            } while (busToTake == -1);
            --currentTime;

            Console.WriteLine($"Can take bus {busToTake} at {currentTime} after a {currentTime-timeReady} wait.");
        }

        #endregion


        #region Part2

        public override void Part2()
        {

        }

        #endregion

        protected override void LoadInput()
        {
            base.LoadInput();
            Input = new[]
            {
                "939",
                "7,13,x,x,59,x,31,19",
            };
        }
    }
}