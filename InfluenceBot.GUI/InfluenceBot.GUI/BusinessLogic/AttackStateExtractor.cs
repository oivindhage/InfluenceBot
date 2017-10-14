using InfluenceBot.GUI.Model;
using System.Collections.Generic;
using System;
using System.Linq;

namespace InfluenceBot.GUI.BusinessLogic
{
    public static class AttackStateExtractor
    {
        public static IEnumerable<AttackState> ExtractAttackStates(Board board, Player player)
        {
            int xMax = board.Tiles.GetLength(0);
            int yMax = board.Tiles.GetLength(1);
            Tuple<Player, int>[] armyStrengths = GetArmyStrengths(board, player);
            Tuple<Player, int>[] ownedTiles = GetOwnedTiles(board, player);
            double[] armyStrengthsAndOwnedTiles = GetArmyStrengthsAndOwnedTiles(armyStrengths, ownedTiles);
            for (int x = 0; x < xMax; ++x)
                for (int y = 0; y < yMax; ++y)
                    if (board.Tiles[x, y].Player == player && board.Tiles[x, y].ArmyCount > 1)
                        foreach (var attackState in ExtractAttackStates(board, player, x, y, xMax, yMax, armyStrengths, ownedTiles, armyStrengthsAndOwnedTiles))
                            yield return attackState;
        }

        private static IEnumerable<AttackState> ExtractAttackStates(Board board, Player player, int x, int y, int xMax, int yMax, Tuple<Player, int>[] armyStrengths, Tuple<Player, int>[] ownedTiles, double[] armyStrengthsAndOwnedTiles)
        {
            if (x > 0 && board.Tiles[x - 1, y].Player != player)//can attack left
            {
                AttackState attackState = BuildAttackState(armyStrengthsAndOwnedTiles, x, y, x - 1, y, board.Tiles);
                UpdateState(board, ownedTiles, armyStrengthsAndOwnedTiles, x - 2, x + 3, 1, y - 2, y + 3, 1, attackState);
                yield return attackState; 
            }
            if (x < (xMax - 1) && board.Tiles[x + 1, y].Player != player)//can attack right
            {
                AttackState attackState = BuildAttackState(armyStrengthsAndOwnedTiles, x, y, x + 1, y, board.Tiles);
                UpdateState(board, ownedTiles, armyStrengthsAndOwnedTiles, x + 2, x - 3, -1, y + 2, y - 3, -1, attackState);
                yield return attackState; 
            }
            if (y > 0 && board.Tiles[x, y - 1].Player != player)//can attack up
            {
                AttackState attackState = BuildAttackState(armyStrengthsAndOwnedTiles, x, y, x, y - 1, board.Tiles);
                UpdateState(board, ownedTiles, armyStrengthsAndOwnedTiles, x + 2, x - 3, -1, y - 2, y + 3, 1,attackState);
                yield return attackState; 
            }
            if (y < (yMax - 1) && board.Tiles[x, y + 1].Player != player)//can attack down
            {
                AttackState attackState = BuildAttackState(armyStrengthsAndOwnedTiles, x, y, x, y + 1, board.Tiles);
                UpdateState(board, ownedTiles, armyStrengthsAndOwnedTiles, x - 2, x + 3, 1, y + 2, y - 3, -1, attackState);
                yield return attackState; 
            }
        }

        private static void UpdateState(Board board, Tuple<Player, int>[] ownedTiles, double[] armyStrengthsAndOwnedTiles, int startX, int endX, int stepX, int startY, int endY, int stepY,AttackState attackState)
        {
            int counter = 0;
            for (int tmpX = startX; tmpX != endX; tmpX += stepX)
            {
                if (tmpX < 0 || tmpX >= 6)
                    continue;
                for (int tmpY = startY; tmpY != endY; tmpY += stepY)
                {
                    if (tmpY < 0 || tmpY >= 6)
                        continue;
                    var currentTile = board.Tiles[tmpX, tmpY];
                    SetInput(ownedTiles, attackState, counter, currentTile);
                    counter++;
                }
            }
        }

        private static void SetInput(Tuple<Player, int>[] ownedTiles, AttackState result, int counter, Tile currentTile)
        {
            if (currentTile.Player == null)
                return;
            int tileOwner = 0;
            for (int i = 0; i < ownedTiles.Length; ++i)
                if (currentTile.Player == ownedTiles[i].Item1)
                {
                    tileOwner = i;
                    break;
                }
            result.State[8 + counter + (25 * tileOwner)] = currentTile.ArmyCount / 5.0;
        }

        private static AttackState BuildAttackState(double[] armyStrengthsAndOwnedTiles, int fromX, int fromY, int toX, int toY, Tile[,] tiles)
        {
            AttackState result = new AttackState();
            result.FromX = fromX;
            result.ToX = toX;
            result.FromY = fromY;
            result.ToY = toY;
            result.From = tiles[fromX, fromY];
            result.To = tiles[toX, toY];
            for (int i = 0; i < 8; ++i)
                result.State[i] = armyStrengthsAndOwnedTiles[i];
            return result;
        }

        private static double[] GetArmyStrengthsAndOwnedTiles(Tuple<Player, int>[] armyStrengths, Tuple<Player, int>[] ownedTiles)
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

        private static Tuple<Player, int>[] GetArmyStrengths(Board board, Player player)
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

        private static Tuple<Player, int>[] GetOwnedTiles(Board board, Player player)
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
