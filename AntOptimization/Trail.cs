using System;

namespace AntOptimization
{
    public class Trail
    {
        public Trail(Colony a, Colony b)
        {
            A = a;
            B = b;
        }

        public double Distance => Math.Sqrt(Math.Pow(A.X - B.X, 2) + Math.Pow(A.Y - B.Y, 2));

        public Colony A { get; private set; }
        public Colony B { get; private set; }

        public double PheromoneValue { get; private set; } = 0.0001;

        public void AddPheromoneValue()
        {
            PheromoneValue += Pheromone.PheromoneIncrease / Distance;
        }

        public void SubtractPheromoneValue()
        {
            PheromoneValue = PheromoneValue * (1 - Pheromone.PheromoneDecrease);
        }

        public override string ToString()
        {
            return $"A X: {A.X} Y:{A.Y} -> B {B.X} Y:{B.Y}; Pheromone value: {PheromoneValue}";
        }

    }
}
