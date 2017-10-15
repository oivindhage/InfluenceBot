using System.Collections.Generic;
using System.Drawing;

namespace InfluenceBot.GUI.Model
{
    public class Player
    {
        public string Name;
        public Color Color;
        public int TotalArmyStrength;
        public List<Tile> Tiles;
        public int Reinforcements;
        public int Ranking;
        public List<ReinforceState> ReinforceStates;
        public List<AttackState> AttackStates;
        public bool Active;

        public int OwnedTiles
            => Tiles.Count;

        public Player()
        {
            Tiles = new List<Tile>();
            ReinforceStates = new List<ReinforceState>();
            AttackStates = new List<AttackState>();
        }
    }
}
