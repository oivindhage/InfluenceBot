using InfluenceBot.GUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InfluenceBot.GUI.BusinessLogic
{
    public static class StateExtractor
    {
        public static double[] GetArmyStrengthsAndOwnedTiles(Tuple<Player, int>[] armyStrengths, Tuple<Player, int>[] ownedTiles)
        {
            var result = new double[8];
            double maxArmies = armyStrengths.Max(x => x.Item2);
            for (int i = 0; i < armyStrengths.Length; ++i)
            {
                if (armyStrengths[i] == null)
                    result[i] = 0;
                else
                    result[i] = armyStrengths[i].Item2 / maxArmies;
            }
            double maxOwnedTiles = ownedTiles.Max(x => x.Item2);
            for (int i = 0; i < ownedTiles.Length; ++i)
            {
                if (ownedTiles[i] == null)
                    result[4 + i] = 0;
                else
                    result[4 + i] = ownedTiles[i].Item2 / maxOwnedTiles;
            }
            return result;
        }

        public static Tuple<Player, int>[] GetArmyStrengths(Board board, Player player)
        {
            List<Tuple<Player, int>> strengths = new List<Tuple<Player, int>>();
            strengths.Add(new Tuple<Player, int>(player, player.TotalArmyStrength));
            foreach (Player p in board.Players.OrderByDescending(x => x.TotalArmyStrength))
            {
                if (p == player)
                    continue;
                strengths.Add(new Tuple<Player, int>(p, p.TotalArmyStrength));
            }
            return strengths.ToArray();
        }

        public static Tuple<Player, int>[] GetOwnedTiles(Board board, Player player)
        {
            List<Tuple<Player, int>> ownedtiles = new List<Tuple<Player, int>>();
            ownedtiles.Add(new Tuple<Player, int>(player, player.OwnedTiles));
            foreach (Player p in board.Players.OrderByDescending(x => x.OwnedTiles))
            {
                if (p == player)
                    continue;
                ownedtiles.Add(new Tuple<Player, int>(p, p.OwnedTiles));
            }
            return ownedtiles.ToArray();
        }
    }
}
