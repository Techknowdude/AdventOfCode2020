namespace AoC2020
{
    interface ISimulation
    {
        string[] Input { get; set; }

        void Run();

        void Part1();
        void Part2();

        void LoadInput();
    }
}