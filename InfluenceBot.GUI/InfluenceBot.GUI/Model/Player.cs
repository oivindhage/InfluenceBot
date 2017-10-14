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

        public int OwnedTiles
            => Tiles.Count;

        public Player()
        {
            Tiles = new List<Tile>();
        }
    }
}
