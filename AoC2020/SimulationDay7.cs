/*
 * --- Day 7: Handy Haversacks ---
You land at the regional airport in time for your next flight. In fact, it looks like you'll even have time to grab some food: all flights are currently delayed due to issues in luggage processing.

Due to recent aviation regulations, many rules (your puzzle input) are being enforced about bags and their contents; bags must be color-coded and must contain specific quantities of other color-coded bags. Apparently, nobody responsible for these regulations considered how long they would take to enforce!

For example, consider the following rules:

light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.
These rules specify the required contents for 9 bag types. In this example, every faded blue bag is empty, every vibrant plum bag contains 11 bags (5 faded blue and 6 dotted black), and so on.

You have a shiny gold bag. If you wanted to carry it in at least one other bag, how many different bag colors would be valid for the outermost bag? (In other words: how many colors can, eventually, contain at least one shiny gold bag?)

In the above rules, the following options would be available to you:

A bright white bag, which can hold your shiny gold bag directly.
A muted yellow bag, which can hold your shiny gold bag directly, plus some other bags.
A dark orange bag, which can hold bright white and muted yellow bags, either of which could then hold your shiny gold bag.
A light red bag, which can hold bright white and muted yellow bags, either of which could then hold your shiny gold bag.
So, in this example, the number of bag colors that can eventually contain at least one shiny gold bag is 4.

How many bag colors can eventually contain at least one shiny gold bag? (The list of rules is quite long; make sure you get all of it.)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2020
{
    class SimulationDay7 : SimulationDay
    {
        #region Part1

        protected override void LoadInput()
        {
            base.LoadInput();

            //Input = new[] {
                //"light red bags contain 1 bright white bag, 2 muted yellow bags.",
                //"dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
                //"bright white bags contain 1 shiny gold bag.",
                //"muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
                //"shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
                //"dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
                //"vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
                //"faded blue bags contain no other bags.",
                //"dotted black bags contain no other bags."};

            //Input = new[] {
            //    "shiny gold bags contain 2 dark red bags.",
            //    "dark red bags contain 2 dark orange bags.",
            //    "dark orange bags contain 2 dark yellow bags.",
            //    "dark yellow bags contain 2 dark green bags.",
            //    "dark green bags contain 2 dark blue bags.",
            //    "dark blue bags contain 2 dark violet bags.",
            //    "dark violet bags contain no other bags."};
        }

        public override void Part1()
        {
            foreach (var inputLine in Input)
            {
                //shiny purple bags contain 2 pale blue bags, 1 wavy fuchsia bag, 5 pale salmon bags.
                var split = inputLine.Split(new[] {' ', ',','.'}, StringSplitOptions.RemoveEmptyEntries);
                int lineIndex = 0;

                StringBuilder bagName = new StringBuilder(split[lineIndex++]);

                while (split[lineIndex] != "bags")
                {
                    bagName.Append($" {split[lineIndex++]}");
                }
                //skip "bags contain"
                lineIndex+=2;

                var newBag = Bag.GetOrCreateBag(bagName.ToString());

                while (lineIndex < split.Length && split[lineIndex] != "no")
                {
                    // get count
                    var bagCount = Int32.Parse(split[lineIndex++]);

                    bagName.Clear();
                    bagName.Append(split[lineIndex++]);

                    while (split[lineIndex] != "bags" && split[lineIndex] != "bag")
                    {
                        bagName.Append($" {split[lineIndex++]}");
                    }
                    //skip "bags"
                    ++lineIndex;

                    var childBag = Bag.GetOrCreateBag(bagName.ToString());
                    childBag.ParentBags[newBag] = bagCount;
                    newBag.ChildBags[childBag] = bagCount;
                }
            }
            var bag = "shiny gold";
            var allBagsContainingBag = Bag.BagsContainingBag(bag);

            Console.WriteLine($"There are {allBagsContainingBag.Count} bags that can contain a {bag}");
        }

        partial class Bag
        {
            public static Bag GetOrCreateBag(string name)
            {
                if (AllBags.ContainsKey(name))
                    return AllBags[name];

                return new Bag(name);
            }

            protected Bag(string name)
            {
                Name = name;
                AllBags[name] = this;
            }

            public string Name { get; set; }

            public Dictionary<Bag,int> ParentBags = new Dictionary<Bag, int>();
            public Dictionary<Bag,int> ChildBags = new Dictionary<Bag, int>();

            public static Dictionary<string,Bag> AllBags = new Dictionary<string, Bag>();

            public static List<Bag> BagsContainingBag(string bagName)
            {
                List<Bag> bagsThatCanContainBag = new List<Bag>();
                var foundBag = AllBags.ContainsKey(bagName);

                if(!foundBag) return bagsThatCanContainBag;

                AllBags[bagName].GetAllParents(bagsThatCanContainBag);

                bagsThatCanContainBag.Remove(AllBags[bagName]);

                return bagsThatCanContainBag;
            }

            private void GetAllParents(List<Bag> bagsThatCanContainBag)
            {
                //ignore bags being added twice to improve performance and prevent infinite recursion.
                if (bagsThatCanContainBag.Contains(this)) return;

                bagsThatCanContainBag.Add(this);

                foreach (var parentBag in ParentBags)
                {
                    parentBag.Key.GetAllParents(bagsThatCanContainBag);
                }
            }
        }

        #endregion


        #region Part2

        public override void Part2()
        {
            // part1 will load all bags already, so don't worry about that.

            Bag shinyGoldBag = Bag.GetOrCreateBag("shiny gold");

            var bagsInsidebag = shinyGoldBag.GetChildCount();

            Console.WriteLine($"There are {bagsInsidebag} bags inside the shiny gold bag.");
        }

        partial class Bag
        {
            public int GetChildCount()
            {
                int thisTotal = 0; //never count the bag we start at.

                foreach (var bagPair in ChildBags)
                {
                    thisTotal += (bagPair.Key.GetChildCountRecursive() * bagPair.Value);
                }

                return thisTotal;
            }

            protected int GetChildCountRecursive()
            {
                if (ChildBags.Count == 0)
                    return 1;

                int thisTotal = 1;

                foreach (var bagPair in ChildBags)
                {
                    thisTotal += (bagPair.Key.GetChildCountRecursive() * bagPair.Value);
                }

                return thisTotal;
            }
        }
        #endregion

    }
}