/*
 * --- Day 11: Seating System ---
Your plane lands with plenty of time to spare. The final leg of your journey is a ferry that goes directly to the tropical island where you can finally start your vacation. As you reach the waiting area to board the ferry, you realize you're so early, nobody else has even arrived yet!

By modeling the process people use to choose (or abandon) their seat in the waiting area, you're pretty sure you can predict the best place to sit. You make a quick map of the seat layout (your puzzle input).

The seat layout fits neatly on a grid. Each position is either floor (.), an empty seat (L), or an occupied seat (#). For example, the initial seat layout might look like this:

L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL
Now, you just need to model the people who will be arriving shortly. Fortunately, people are entirely predictable and always follow a simple set of rules. All decisions are based on the number of occupied seats adjacent to a given seat (one of the eight positions immediately up, down, left, right, or diagonal from the seat). The following rules are applied to every seat simultaneously:

If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
If a seat is occupied (#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
Otherwise, the seat's state does not change.
Floor (.) never changes; seats don't move, and nobody sits on the floor.

After one round of these rules, every seat in the example layout becomes occupied:

#.##.##.##
#######.##
#.#.#..#..
####.##.##
#.##.##.##
#.#####.##
..#.#.....
##########
#.######.#
#.#####.##
After a second round, the seats with four or more occupied adjacent seats become empty again:

#.LL.L#.##
#LLLLLL.L#
L.L.L..L..
#LLL.LL.L#
#.LL.LL.LL
#.LLLL#.##
..L.L.....
#LLLLLLLL#
#.LLLLLL.L
#.#LLLL.##
This process continues for three more rounds:

#.##.L#.##
#L###LL.L#
L.#.#..#..
#L##.##.L#
#.##.LL.LL
#.###L#.##
..#.#.....
#L######L#
#.LL###L.L
#.#L###.##
#.#L.L#.##
#LLL#LL.L#
L.L.L..#..
#LLL.##.L#
#.LL.LL.LL
#.LL#L#.##
..L.L.....
#L#LLLL#L#
#.LLLLLL.L
#.#L#L#.##
#.#L.L#.##
#LLL#LL.L#
L.#.L..#..
#L##.##.L#
#.#L.LL.LL
#.#L#L#.##
..L.L.....
#L#L##L#L#
#.LLLLLL.L
#.#L#L#.##
At this point, something interesting happens: the chaos stabilizes and further applications of these rules cause no seats to change state! Once people stop moving around, you count 37 occupied seats.

Simulate your seating area by applying the seating rules repeatedly until no seats change state. How many seats end up occupied?

--- Part Two ---
As soon as people start to arrive, you realize your mistake. People don't just care about adjacent seats - they care about the first seat they can see in each of those eight directions!

Now, instead of considering just the eight immediately adjacent seats, consider the first seat in each of those eight directions. For example, the empty seat below would see eight occupied seats:

.......#.
...#.....
.#.......
.........
..#L....#
....#....
.........
#........
...#.....
The leftmost empty seat below would only see one empty seat, but cannot see any of the occupied ones:

.............
.L.L.#.#.#.#.
.............
The empty seat below would see no occupied seats:

.##.##.
#.#.#.#
##...##
...L...
##...##
#.#.#.#
.##.##.
Also, people seem to be more tolerant than you expected: it now takes five or more visible occupied seats for an occupied seat to become empty (rather than four or more from the previous rules). The other rules still apply: empty seats that see no occupied seats become occupied, seats matching no rule don't change, and floor never changes.

Given the same starting layout as above, these new rules cause the seating area to shift around as follows:

L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL
#.##.##.##
#######.##
#.#.#..#..
####.##.##
#.##.##.##
#.#####.##
..#.#.....
##########
#.######.#
#.#####.##
#.LL.LL.L#
#LLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLL#
#.LLLLLL.L
#.LLLLL.L#
#.L#.##.L#
#L#####.LL
L.#.#..#..
##L#.##.##
#.##.#L.##
#.#####.#L
..#.#.....
LLL####LL#
#.L#####.L
#.L####.L#
#.L#.L#.L#
#LLLLLL.LL
L.L.L..#..
##LL.LL.L#
L.LL.LL.L#
#.LLLLL.LL
..L.L.....
LLLLLLLLL#
#.LLLLL#.L
#.L#LL#.L#
#.L#.L#.L#
#LLLLLL.LL
L.L.L..#..
##L#.#L.L#
L.L#.#L.L#
#.L####.LL
..#.#.....
LLL###LLL#
#.LLLLL#.L
#.L#LL#.L#
#.L#.L#.L#
#LLLLLL.LL
L.L.L..#..
##L#.#L.L#
L.L#.LL.L#
#.LLLL#.LL
..#.L.....
LLL###LLL#
#.LLLLL#.L
#.L#LL#.L#
Again, at this point, people stop shifting around and the seating area reaches equilibrium. Once this occurs, you count 26 occupied seats.

Given the new visibility method and the rule change for occupied seats becoming empty, once equilibrium is reached, how many seats end up occupied?
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2020
{
    class SimulationDay11 : SimulationDay
    {
        public delegate char PositionUpdatingFunction(int row, int col, List<string> positionMap);

        List<string> seatingMap = new List<string>();

        #region Part1

        public override void Part1()
        {
            //create map.
            foreach (var line in Input)
            {
                seatingMap.Add(line);
            }

            //create copy
            List<string> oldMap;
            var newMap = seatingMap.ToList();

            do
            {
                oldMap = newMap;
                newMap = UpdateMap(oldMap, GetNewPositionValue);

            } while (MapsDiffer(newMap, oldMap));

            var occupiedSeats = GetOccupiedSeats(newMap);
            Console.WriteLine($"Occupied Seats: {occupiedSeats}");
        }

        private int GetOccupiedSeats(List<string> currentMap)
        {
            int seatCount = 0;

            foreach (var row in currentMap)
            {
                seatCount += row.Count(i => i == '#');
            }

            return seatCount;
        }

        private List<string> UpdateMap(List<string> currentMap, PositionUpdatingFunction updatingFunction)
        {
            var updatedMap = new List<string>();

            for (int row = 0; row < currentMap.Count; row++)
            {
                var updatedRow = new StringBuilder();
                for (int column = 0; column < currentMap[row].Length; column++)
                {
                    var newValue = updatingFunction(row, column, currentMap);

                    updatedRow.Append(newValue);
                }
                updatedMap.Add(updatedRow.ToString());
            }

            return updatedMap;
        }

        private char GetNewPositionValue(int row, int column, List<string> currentMap)
        {
            if (currentMap[row][column] == '.') return '.'; // floor does not change.

            /*
             * If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
             * If a seat is occupied (#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
             * Otherwise, the seat's state does not change.
             */

            int adjacentSeatsOccupied = 0;

            // check rows
            for (var r = Math.Max(row - 1, 0); r < Math.Min(currentMap.Count, row + 2); r++)
            {
                for (var c = Math.Max(0, column - 1); c < Math.Min(column + 2, currentMap[row].Length); c++)
                {
                    if (r == row && c == column) continue; // Don't include the current seat.

                    if (currentMap[r][c] == '#')
                        adjacentSeatsOccupied++;
                }
            }

            if (currentMap[row][column] == 'L' && adjacentSeatsOccupied == 0)
                return '#';

            if (currentMap[row][column] == '#' && adjacentSeatsOccupied >= 4)
                return 'L';

            return currentMap[row][column];
        }

        private bool MapsDiffer(List<string> newMap, List<string> oldMap)
        {
            for (var row = 0; row < newMap.Count; row++)
            {
                if (newMap[row] != oldMap[row])
                    return true;
            }

            return false;
        }

        #endregion


        public override void Part2()
        {
            //create copy
            List<string> oldMap;
            var newMap = seatingMap.ToList();

            do
            {
                oldMap = newMap;
                newMap = UpdateMap(oldMap, UpdatingFunctionPart2);

            } while (MapsDiffer(newMap, oldMap));

            var occupiedSeats = GetOccupiedSeats(newMap);
            Console.WriteLine($"Occupied Seats: {occupiedSeats}");
        }

        private char UpdatingFunctionPart2(int row, int col, List<string> positionMap)
        {
            if (positionMap[row][col] == '.') return '.'; // floor does not change.

            /*
             * If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
             * If a seat is occupied (#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
             * Otherwise, the seat's state does not change.
             */

            int adjacentSeatsOccupied = 0;

            for (var r = -1; r <= 1; r++)
            {
                for (var c = -1; c <= 1; c++)
                {
                    if (r == 0 && c == 0) continue; // Don't include the current seat.

                    var nextSeatObserved = GetNextObservedSeat(r, c, positionMap, row+r, col+c);
                    if (nextSeatObserved == '#')
                        adjacentSeatsOccupied++;
                }
            }

            if (positionMap[row][col] == 'L' && adjacentSeatsOccupied == 0)
                return '#';

            if (positionMap[row][col] == '#' && adjacentSeatsOccupied >= 5)
                return 'L';

            return positionMap[row][col];
        }

        private char GetNextObservedSeat(int rowDelta, int colDelta, List<string> positionMap, int row, int col)
        {
            // check bounds
            if (row < 0 || row >= positionMap.Count || col < 0 || col >= positionMap[row].Length) return 'W'; // we see the wall.

            // check for seat taken or seat empty.
            if (positionMap[row][col] == '#' || positionMap[row][col] == 'L')
                return positionMap[row][col];

            return GetNextObservedSeat(rowDelta, colDelta, positionMap, row + rowDelta, col + colDelta);
        }

        protected override void LoadInput()
        {
            base.LoadInput();
            //Input = new[]
            //{
            //    "L.LL.LL.LL",
            //    "LLLLLLL.LL",
            //    "L.L.L..L..",
            //    "LLLL.LL.LL",
            //    "L.LL.LL.LL",
            //    "L.LLLLL.LL",
            //    "..L.L.....",
            //    "LLLLLLLLLL",
            //    "L.LLLLLL.L",
            //    "L.LLLLL.LL",
            //};
        }
    }
}