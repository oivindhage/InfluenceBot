using System;
using System.Drawing;

namespace InfluenceBot.GUI.Model
{
    public class Board
    {
        public Tile[,] Tiles;
        public Player[] Players;
        public int CurrentPlayer;
        Random r = new Random((int)DateTime.Now.Ticks);

        internal void Initialize(int numberOfPlayers)
        {
            if (numberOfPlayers > 4)
                throw new InvalidOperationException("Max 4 players allowed.");
            Tiles = new Tile[6, 6];
            for (int x = 0; x < 6; ++x)
                for (int y = 0; y < 6; ++y)
                    Tiles[x, y] = new Tile();
            Color[] Colors = new Color[] { Color.Aqua, Color.Red, Color.Lime, Color.Yellow };
            Players = new Player[numberOfPlayers];
            for (int i = 0; i < numberOfPlayers; ++i)
            {
                Players[i] = new Player { Name = $"Player {1}", Color = Colors[i] };
                var tile = GetRandomUnoccupiedTile();
                tile.Player = Players[i];
                Players[i].Tiles.Add(tile);
                tile.ArmyCount = 2;
                Players[i].TotalArmyStrength = 2;
            }
        }

        internal int GetArmyCount(int x, int y)
            => Tiles[x, y].ArmyCount;

        internal Color GetColor(int x, int y)
            => Tiles[x, y].Player?.Color ?? Color.LightGray;

        private Tile GetRandomUnoccupiedTile()
        {
            for (int i = 0; i < 100; ++i)
            {
                int x = r.Next(0, 6);
                int y = r.Next(0, 6);
                if (Tiles[x, y].Player == null)
                    return Tiles[x, y];
            }
            throw new Exception("Could not find a random unoccupied tile in 100 tries.");
        }
    }
}
