namespace InfluenceBot.GUI.Model
{
    public class ReinforceState
    {
        //5x5 grid rundt tile, kanskje bare 3x3
        public double[] State = new double[4 + 4 + 4 * 5 * 5];
        public Tile Tile;
        public double Score;
        private double weight = double.MinValue;
        public double GetWeight()
        {
            if (weight != double.MinValue)
                return weight;
             weight = 0;
            for (int i = 8; i < 8 + 10; ++i)
                weight += State[i];
            for (int i = 8 + 25; i < 8 + 25 + 10; ++i)
                weight -= State[i];
            for (int i = 8 + 50; i < 8 + 50 + 10; ++i)
                weight -= State[i] / 2;
            return weight;
        }
    }
}
