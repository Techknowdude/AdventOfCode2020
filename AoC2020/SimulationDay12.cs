using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020
{
    class SimulationDay12 : SimulationDay
    {
        

        #region Part1

        public enum Direction
        {
            East,
            South,
            West,
            North,
        }

        public override void Part1()
        {
            var startingPosition = new Tuple<int, int>(0, 0);
            var instructionList =
                Input.Select(
                    instruction =>
                        new KeyValuePair<char, int>(instruction[0],
                            Int32.Parse(instruction.Substring(1, instruction.Length - 1)))).ToList();

            var finalPosition = PerformInstructions(instructionList, startingPosition);
            var distance = Math.Abs(startingPosition.Item1 - finalPosition.Item1) +
                           Math.Abs(startingPosition.Item2 - finalPosition.Item2);
            Console.WriteLine($"Starting position: ({startingPosition.Item1},{startingPosition.Item2})");
            Console.WriteLine($"Final position: ({finalPosition.Item1},{finalPosition.Item2})");
            Console.WriteLine($"Manhattan distance: {distance}");
        }

        private Tuple<int, int> PerformInstructions(List<KeyValuePair<char, int>> instructionList,
            Tuple<int, int> startingPosition)
        {
            var currentDir = Direction.East;
            var position = startingPosition;

            foreach (var instructionPair in instructionList)
            {
                switch (instructionPair.Key)
                {
                    case 'F':
                        position = MoveInDir(currentDir, instructionPair.Value, position);
                        break;
                    case 'L':
                        var newDir = ((int) currentDir - (instructionPair.Value/90)%4);
                        if (newDir < 0) // going backwards should result in 0 to 4 
                            newDir = Enum.GetNames(typeof(Direction)).Length + newDir;
                        currentDir = (Direction) (newDir);
                        break;
                    case 'R':
                        currentDir = (Direction) (((int) currentDir + (instructionPair.Value/90))%4);
                        break;
                    case 'N':
                        position = MoveInDir(Direction.North, instructionPair.Value, position);
                        break;
                    case 'E':
                        position = MoveInDir(Direction.East, instructionPair.Value, position);
                        break;
                    case 'S':
                        position = MoveInDir(Direction.South, instructionPair.Value, position);
                        break;
                    case 'W':
                        position = MoveInDir(Direction.West, instructionPair.Value, position);
                        break;
                }

            }


            return position;
        }

        private Tuple<int, int> MoveInDir(Direction currentDir, int length, Tuple<int, int> startingPos)
        {
            Console.WriteLine($"Moving {currentDir} by {length}");
            switch (currentDir)
            {
                case Direction.East:
                    return new Tuple<int, int>(startingPos.Item1 - length, startingPos.Item2);
                case Direction.North:
                    return new Tuple<int, int>(startingPos.Item1, startingPos.Item2 + length);
                case Direction.West:
                    return new Tuple<int, int>(startingPos.Item1 + length, startingPos.Item2);
                case Direction.South:
                    return new Tuple<int, int>(startingPos.Item1, startingPos.Item2 - length);
                default:
                    throw new ArgumentOutOfRangeException(nameof(currentDir), currentDir, null);
            }
        }

        #endregion


        #region Part2

        public override void Part2()
        {
            var startingPosition = new Tuple<int, int>(0, 0);
            var instructionList =
                Input.Select(
                    instruction =>
                        new KeyValuePair<char, int>(instruction[0],
                            Int32.Parse(instruction.Substring(1, instruction.Length - 1)))).ToList();

            var finalPosition = PerformVectorInstructions(instructionList, startingPosition);
            var distance = Math.Abs(startingPosition.Item1 - finalPosition.Item1) +
                           Math.Abs(startingPosition.Item2 - finalPosition.Item2);
            Console.WriteLine($"Starting position: ({startingPosition.Item1},{startingPosition.Item2})");
            Console.WriteLine($"Final position: ({finalPosition.Item1},{finalPosition.Item2})");
            Console.WriteLine($"Manhattan distance: {distance}");
        }

        private Tuple<int, int> PerformVectorInstructions(List<KeyValuePair<char, int>> instructionList,
            Tuple<int, int> startingPosition)
        {
            var position = startingPosition;
            var vector = new Tuple<int,int>(10,1);

            foreach (var instructionPair in instructionList)
            {
                switch (instructionPair.Key)
                {
                    case 'F':
                        position = MoveInVector(vector, instructionPair.Value, position);
                        break;
                    case 'L':
                        var leftRotations = instructionPair.Value/90;
                        for (int r = 0; r < leftRotations; ++r)
                        {
                            vector = new Tuple<int, int>(-vector.Item2,vector.Item1);
                        }
                        Console.WriteLine($"Turning left {leftRotations} times. New vector: ({vector.Item1},{vector.Item2})");
                        break;
                    case 'R':
                        var rightRotations = instructionPair.Value / 90;
                        for (int r = 0; r < rightRotations; ++r)
                        {
                            vector = new Tuple<int, int>(vector.Item2, -vector.Item1);
                        }
                        Console.WriteLine($"Turning right {rightRotations} times. New vector: ({vector.Item1},{vector.Item2})");
                        break;
                    case 'N':
                        vector = new Tuple<int, int>(vector.Item1, vector.Item2 + instructionPair.Value);
                        Console.WriteLine($"Vector to North {instructionPair.Value}. New vector: ({vector.Item1},{vector.Item2})");
                        break;
                    case 'E':
                        vector = new Tuple<int, int>(vector.Item1 + instructionPair.Value, vector.Item2);
                        Console.WriteLine($"Vector to East {instructionPair.Value}. New vector: ({vector.Item1},{vector.Item2})");
                        break;
                    case 'S':
                        vector = new Tuple<int, int>(vector.Item1, vector.Item2 - instructionPair.Value);
                        Console.WriteLine($"Vector to South {instructionPair.Value}. New vector: ({vector.Item1},{vector.Item2})");
                        break;
                    case 'W':
                        vector = new Tuple<int, int>(vector.Item1 - instructionPair.Value, vector.Item2);
                        Console.WriteLine($"Vector to West {instructionPair.Value}. New vector: ({vector.Item1},{vector.Item2})");
                        break;
                }

            }


            return position;
        }

        private Tuple<int, int> MoveInVector(Tuple<int, int> vector, int vectorMultiplier, Tuple<int, int> startingPos)
        {
            Console.WriteLine($"Moving {vector} by {vectorMultiplier}");
            
            return new Tuple<int, int>(startingPos.Item1 + (vectorMultiplier * vector.Item1), startingPos.Item2 + (vectorMultiplier * vector.Item2));
        }

        #endregion


        protected override void LoadInput()
        {
            base.LoadInput();
            //Input = new[]
            //{
            //    "F10",
            //    "N3",
            //    "F7",
            //    "R90",
            //    "F11",
            //};
        }
    }
}