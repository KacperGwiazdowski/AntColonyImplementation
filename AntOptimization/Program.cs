using System;
using System.Collections.Generic;
using System.Linq;

namespace AntOptimization
{
    class Program
    {
        static void Main()
        {
            // list of colonies that out ants have to visit
            List<Colony> colonyList = new()
            {
                new Colony { X = 0, Y = 0 },
                new Colony { X = 4, Y = 4 },
                new Colony { X = 0, Y = 4 },
                new Colony { X = 4, Y = 0 },
                new Colony { X = 2, Y = 3 },
                new Colony { X = 10, Y = 10 },
                //new Colony { X = 4, Y = 10 },
            };

            // setuping trails between cities
            var trails = colonyList.SetupTrails();

            List<Ant> ants = new List<Ant>
            {
                new Ant("Steve"),
                new Ant("Bill"),
                new Ant("Jeff"),
            };


            SetRandomStartingColony(ants, colonyList);

            DisplayCurrentAntPositions(ants);

            // first run does not include pheromones
            // minus one cause we set up starting city earlier
            for (int i = 0; i < colonyList.Count - 1; i++)
            {
                foreach (var ant in ants)
                {
                    var nextColony = ant.SelectNextColony(colonyList, false);
                    ant.MoveToNextColony(nextColony);
                }

                DisplayCurrentAntPositions(ants);
            }

            DisplayTraveledDistances(ants);

            for (int j = 0; j < 100; j++)
            {
                // we need new ants or we could have cleaned visited colonies for those we initiated earlier
                 ants = new List<Ant>
                {
                    new Ant("Steve"),
                    new Ant("Bill"),
                    new Ant("Jeff"),
                };

                SetRandomStartingColony(ants, colonyList);

                for (int i = 0; i < colonyList.Count - 1; i++)
                {
                    foreach (var ant in ants)
                    {
                        var nextColony = ant.SelectNextColony(colonyList);
                        ant.MoveToNextColony(nextColony);
                    }

                }

                DisplayTraveledDistances(ants);


                // every go we the pheromones evaporate
                trails.ForEach(x => x.SubtractPheromoneValue());
            }

        }

        private static void SetRandomStartingColony(List<Ant> ants, List<Colony> colonies)
        {
            List<Colony> taken = new();

            foreach (var ant in ants)
            {
                var randomStartingColony = colonies.GetRandomElement(taken);
                taken.Add(randomStartingColony);
                ant.MoveToStartColony(randomStartingColony);
            }
        }

        private static void DisplayCurrentAntPositions(List<Ant> ants)
        {
            foreach (var ant in ants)
            {
                Console.WriteLine($"Ant {ant.Name} is at {ant.CurrentColony}");
            }
        }

        private static void DisplayTraveledDistances(List<Ant> ants)
        {
            foreach (var ant in ants)
            {
                var distance = 0.0;
                for (int i = 0; i < ant.VisitedColonies.Count(); i++)
                {
                    var colony = ant.VisitedColonies[i];
                    Colony nextColony;
                    if (i == ant.VisitedColonies.Count() - 1)
                    {
                        nextColony = ant.VisitedColonies[0];
                    }
                    else
                    {
                        nextColony = ant.VisitedColonies[i + 1];
                    }
                    distance += colony.FindTrailToColony(nextColony).Distance;

                }
                Console.WriteLine($"Ant {ant.Name} traveled: {distance}");
            }
        }

    }
}


