namespace InfluenceBot.GUI.Model
{
    public class ReinforceState
    {
        //5x5 grid rundt tile, kanskje bare 3x3
        public double[] state = new double[4 + 5 * 5 * 5];
    }
}
