using System;
using System.Collections.Generic;
using System.Linq;


namespace AntOptimization
{
    public static class ListExtension
    {

        // this is simply because I need to set up random starting city, and I don't want that to be in Program.cs
        public static T GetRandomElement<T>(this List<T> list)
        {
            Random random = new();

            for (int i = 0; i < 1000; i++)
            {
                random = new Random(random.Next());
            }
            var index = random.Next(list.Count);
            return list.ElementAt(index);
        }

        public static T GetRandomElement<T>(this List<T> list, List<T> exceptFor)
        {

            var notTaken = list.Except(exceptFor);

            Random random = new();

            for (int i = 0; i < 1000; i++)
            {
                random = new Random(random.Next());
            }
            var index = random.Next(notTaken.Count());
            return notTaken.ElementAt(index);
        }


        // setuping trails between colonies
        public static List<Trail> SetupTrails(this List<Colony> colonies)
        {
            List<Trail> trails = new List<Trail>();
            for (int i = 0; i < colonies.Count; i++)
            {
                var colony = colonies[i];

                foreach (var col in colonies)
                {

                    if (colony == col)
                    {
                        continue;
                    }

                    if (col.Trails.Any(x => x.A == colony || x.B == colony))
                    {
                        colony.Trails.Add(col.Trails.Single(x => x.A == colony || x.B == colony));
                    }
                    else
                    {
                        var trail = new Trail(colony, col);
                        colony.Trails.Add(trail);
                        trails.Add(trail);
                    }
                }

            }
            return trails;
        }
    }
}
