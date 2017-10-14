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
                    Tiles[x, y] = new Tile { X = x, Y = y };
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

        public void Attack(Tile from, Tile to)
        {
            int fromTmp = from.ArmyCount - 1;
            int toTmp = to.ArmyCount;
            while (toTmp > 0 && fromTmp > 0)
            {
                if (r.NextDouble() > 0.5)
                    toTmp--;
                else
                    fromTmp--;
            }
            if (to.Player != null)
                to.Player.TotalArmyStrength -= to.ArmyCount - toTmp;
            from.Player.TotalArmyStrength -= from.ArmyCount - fromTmp - 1;
            from.ArmyCount = 1;
            if (fromTmp > 0)
            {
                if (to.Player != null)
                    to.Player.Tiles.Remove(to);
                to.Player = from.Player;
                to.ArmyCount = fromTmp;
                from.Player.Tiles.Add(to);
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
