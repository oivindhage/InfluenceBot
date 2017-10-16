using System;
using System.Drawing;
using System.Linq;

namespace InfluenceBot.GUI.Model
{
    public class GameManager
    {
        public Tile[,] Tiles;
        public Player[] Players;
        public int CurrentPlayerIndex;
        public bool AttachPhase;
        Random r = new Random((int)DateTime.Now.Ticks);
        public int NextRanking;
        public bool Finished;

        public void Initialize(int numberOfPlayers)
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
                Players[i] = new Player
                {
                    Name = $"Player {1}",
                    Color = Colors[i],
                    Active = true,
                    TotalArmyStrength = 2
                };
                var tile = GetRandomUnoccupiedTile();
                tile.Player = Players[i];
                Players[i].Tiles.Add(tile);
                tile.ArmyCount = 2;
                Finished = false;
            }
            AttachPhase = true;
            NextRanking = numberOfPlayers;
        }

        public Player CurrentPlayer
            => Players[CurrentPlayerIndex];

        public void Attack(Tile from, Tile to)
        {
            while (to.ArmyCount > 0 && from.ArmyCount > 1)
                DoSingleAttackRound(from, to);
            if (to.ArmyCount == 0 && to.Player != null)
            {
                DeactivatePlayerIfDead(to.Player);
                to.Player.Tiles.Remove(to);
                to.Player = null;
            }
            if (from.ArmyCount > 1)
            {
                to.ArmyCount = from.ArmyCount - 1;
                from.ArmyCount = 1;
                to.Player = from.Player;
                to.Player.Tiles.Add(to);
            }
            if (Players.Count(x => x.Active) == 1)
                Finished = true;
        }

        private void DoSingleAttackRound(Tile from, Tile to)
        {
            if (r.NextDouble() > 0.5)
            {
                to.ArmyCount--;
                to.Player.TotalArmyStrength--;
            }
            else
            {
                from.ArmyCount--;
                from.Player.TotalArmyStrength--;
            }
        }

        private void DeactivatePlayerIfDead(Player player)
        {
            if (player.TotalArmyStrength > 0)
                return;
            player.Ranking = NextRanking--;
            player.Active = false;
        }

        internal int GetArmyCount(int x, int y)
            => Tiles[x, y].ArmyCount;

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

        internal void ReinforceTile(Tile tile)
        {
            tile.ArmyCount++;
            tile.Player.TotalArmyStrength++;
            tile.Player.Reinforcements--;
        }
    }
}
