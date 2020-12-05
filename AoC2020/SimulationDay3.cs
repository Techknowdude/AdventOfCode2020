/*
 * --- Day 3: Toboggan Trajectory ---
With the toboggan login problems resolved, you set off toward the airport. While travel by toboggan might be easy, it's certainly not safe: there's very minimal steering and the area is covered in trees. You'll need to see which angles will take you near the fewest trees.

Due to the local geology, trees in this area only grow on exact integer coordinates in a grid. You make a map (your puzzle input) of the open squares (.) and trees (#) you can see. For example:

..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#
These aren't the only trees, though; due to something you read about once involving arboreal genetics and biome stability, the same pattern repeats to the right many times:

..##.........##.........##.........##.........##.........##.......  --->
#...#...#..#...#...#..#...#...#..#...#...#..#...#...#..#...#...#..
.#....#..#..#....#..#..#....#..#..#....#..#..#....#..#..#....#..#.
..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#
.#...##..#..#...##..#..#...##..#..#...##..#..#...##..#..#...##..#.
..#.##.......#.##.......#.##.......#.##.......#.##.......#.##.....  --->
.#.#.#....#.#.#.#....#.#.#.#....#.#.#.#....#.#.#.#....#.#.#.#....#
.#........#.#........#.#........#.#........#.#........#.#........#
#.##...#...#.##...#...#.##...#...#.##...#...#.##...#...#.##...#...
#...##....##...##....##...##....##...##....##...##....##...##....#
.#..#...#.#.#..#...#.#.#..#...#.#.#..#...#.#.#..#...#.#.#..#...#.#  --->
You start on the open square (.) in the top-left corner and need to reach the bottom (below the bottom-most row on your map).

The toboggan can only follow a few specific slopes (you opted for a cheaper model that prefers rational numbers); start by counting all the trees you would encounter for the slope right 3, down 1:

From your starting position at the top-left, check the position that is right 3 and down 1. Then, check the position that is right 3 and down 1 from there, and so on until you go past the bottom of the map.

The locations you'd check in the above example are marked here with O where there was an open square and X where there was a tree:

..##.........##.........##.........##.........##.........##.......  --->
#..O#...#..#...#...#..#...#...#..#...#...#..#...#...#..#...#...#..
.#....X..#..#....#..#..#....#..#..#....#..#..#....#..#..#....#..#.
..#.#...#O#..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#
.#...##..#..X...##..#..#...##..#..#...##..#..#...##..#..#...##..#.
..#.##.......#.X#.......#.##.......#.##.......#.##.......#.##.....  --->
.#.#.#....#.#.#.#.O..#.#.#.#....#.#.#.#....#.#.#.#....#.#.#.#....#
.#........#.#........X.#........#.#........#.#........#.#........#
#.##...#...#.##...#...#.X#...#...#.##...#...#.##...#...#.##...#...
#...##....##...##....##...#X....##...##....##...##....##...##....#
.#..#...#.#.#..#...#.#.#..#...X.#.#..#...#.#.#..#...#.#.#..#...#.#  --->
In this example, traversing the map using this slope would cause you to encounter 7 trees.

Starting at the top-left corner of your map and following a slope of right 3 and down 1, how many trees would you encounter?
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AoC2020
{
    class SimulationDay3 : SimulationDay
    {
        string[] sample = new[]{
"..##.........##.........##.........##.........##.........##.......",
"#...#...#..#...#...#..#...#...#..#...#...#..#...#...#..#...#...#..",
".#....#..#..#....#..#..#....#..#..#....#..#..#....#..#..#....#..#.",
"..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#",
".#...##..#..#...##..#..#...##..#..#...##..#..#...##..#..#...##..#.",
"..#.##.......#.##.......#.##.......#.##.......#.##.......#.##.....",
".#.#.#....#.#.#.#....#.#.#.#....#.#.#.#....#.#.#.#....#.#.#.#....#",
".#........#.#........#.#........#.#........#.#........#.#........#",
"#.##...#...#.##...#...#.##...#...#.##...#...#.##...#...#.##...#...",
"#...##....##...##....##...##....##...##....##...##....##...##....#",
".#..#...#.#.#..#...#.#.#..#...#.#.#..#...#.#.#..#...#.#.#..#...#.#"};
        private int mapWidth = 0;
        private int mapHeight = 0;

        protected override void LoadInput()
        {
            base.LoadInput();
            //Input = sample;
            mapWidth = Input[0].Length;
            mapHeight = Input.Length;

        }

        #region Part1

        public override void Part1()
        {
            int numTrees = 0;
            int moveRight = 3;
            int moveDown = 1;
            int xLocation = 0;

            for (int row = 0; row < mapHeight; row+=moveDown, xLocation+=moveRight)
            {
                numTrees += LocationTreeCount(xLocation, row);
            }

            Console.WriteLine($"Tree count: {numTrees}");
        }

        int LocationTreeCount(int x, int y)
        {
            int xLoc = GetXLocWrapped(x);

            return Input[y][xLoc] == '#' ? 1 : 0;
        }

        private int GetXLocWrapped(int x)
        {
            // wrap the input back on itself so the x location must stay within 0 and mapWidth
            return x % mapWidth;
        }

        #endregion

        #region Part2

        public override void Part2()
        {
            var slopes = new List<KeyValuePair<int,int>>() {
                new KeyValuePair<int, int>(1, 1),
                new KeyValuePair<int, int>(3, 1),
                new KeyValuePair<int, int>(5, 1),
                new KeyValuePair<int, int>(7, 1),
                new KeyValuePair<int, int>(1, 2), };

            int answer = 1;


            foreach (var slopePair in slopes)
            {
                int numTrees = 0;
                int moveRight = slopePair.Key;
                int moveDown = slopePair.Value;
                int xLocation = 0;

                for (int row = 0; row < mapHeight; row += moveDown, xLocation += moveRight)
                {
                    numTrees += LocationTreeCount(xLocation, row);
                }
                Console.WriteLine($"Tree count for slope ({moveRight},{moveDown}): {numTrees}");

                answer *= numTrees;
            }
            
            Console.WriteLine($"Total tree count: {answer}");
        }

        #endregion


    }
}