using System;
using System.Collections.Generic;
using System.Linq;

namespace AntOptimization
{
    public class Colony
    {
        public int X { get; set; }

        public int Y { get; set; }

        public List<Trail> Trails { get; set; } = new List<Trail>();

        public Trail FindTrailToColony(Colony colony)
        {
            return Trails.Single(x => x.A == colony || x.B == colony);
        }


        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }

    }
}
