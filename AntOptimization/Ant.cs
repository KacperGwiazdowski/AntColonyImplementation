using System;
using System.Collections.Generic;
using System.Linq;

namespace AntOptimization
{
    public class Ant
    {
        const double DistanceSelectionFactor = 2.0;

        const double PheromoneSelectionFactor = 1.0;

        public string Name { get; private set; }

        public List<Colony> VisitedColonies { get; private set; }

        public Colony CurrentColony { get; private set; }

        public Ant(string name)
        {
            Name = name;
            VisitedColonies = new List<Colony>();
        }

        public Colony SelectNextColony(List<Colony> colonies, bool includePheromones = true)
        {
            var availableColonies = colonies.Except(VisitedColonies);

            Colony mostDesiredColony = VisitedColonies.First();

            double desiredColonyFactor = 0;

            foreach (var colony in availableColonies)
            {
                var trail = CurrentColony.FindTrailToColony(colony);

                var tempFactor = Math.Pow((1.0 / trail.Distance), DistanceSelectionFactor);

                if (includePheromones)
                {
                    tempFactor = tempFactor * Math.Pow(trail.PheromoneValue, PheromoneSelectionFactor);
                }

                if (tempFactor > desiredColonyFactor)
                {
                    mostDesiredColony = colony;
                    desiredColonyFactor = tempFactor;
                }
            }

            return mostDesiredColony;
        }

        public void MoveToNextColony(Colony colony)
        {
            VisitedColonies.Add(colony);
            CurrentColony.FindTrailToColony(colony).AddPheromoneValue();
            CurrentColony = colony;
        }

        public void MoveToStartColony(Colony colony)
        {
            VisitedColonies.Add(colony);
            CurrentColony = colony;
        }
    }
}
