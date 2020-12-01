namespace AoC2020
{
    interface ISimulationDay
    {
        string[] Input { get; set; }

        void Run();

        void Part1();
        void Part2();
    }
}