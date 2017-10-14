namespace InfluenceBot.GUI.Model
{
    public class AttackState
    {
        //5x5 rundt tile? Kanskje bare 3x3?
        //5 grids, egne styrker, sterkeste fiende, nest sterkeste, svakeste, off-grid
        //Egen totalArmyStrengthe, relativ til de tre andre
        //Egen totalTileCount, relativ til de tre andre
        public double[] State = new double[4 + 4 + 5 * 5 * 5];
        public int FromX;
        public int FromY;
        public int ToX;
        public int ToY;
        public Tile From;
        public Tile To;
    }
}
