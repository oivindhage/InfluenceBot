using InfluenceBot.GUI.Model;
using System.Collections.Generic;
using System;

namespace InfluenceBot.GUI.BusinessLogic
{
    public static class AttackStateExtractor
    {
        public static IEnumerable<AttackState> ExtractAttackStates(GameManager board, Player player)
        {
            int xMax = board.Tiles.GetLength(0);
            int yMax = board.Tiles.GetLength(1);
            Tuple<Player, int>[] armyStrengths = StateExtractor.GetArmyStrengths(board, player);
            Tuple<Player, int>[] ownedTiles = StateExtractor.GetOwnedTiles(board, player);
            double[] armyStrengthsAndOwnedTiles = StateExtractor.GetArmyStrengthsAndOwnedTiles(armyStrengths, ownedTiles);
            for (int x = 0; x < xMax; ++x)
                for (int y = 0; y < yMax; ++y)
                    if (board.Tiles[x, y].Player == player && board.Tiles[x, y].ArmyCount > 1)
                        foreach (var attackState in ExtractAttackStates(board, player, x, y, xMax, yMax, armyStrengths, ownedTiles, armyStrengthsAndOwnedTiles))
                            yield return attackState;
        }

        private static IEnumerable<AttackState> ExtractAttackStates(GameManager board, Player player, int x, int y, int xMax, int yMax, Tuple<Player, int>[] armyStrengths, Tuple<Player, int>[] ownedTiles, double[] armyStrengthsAndOwnedTiles)
        {
            if (x > 0 && board.Tiles[x - 1, y].Player != player)//can attack left
            {
                AttackState attackState = BuildAttackState(armyStrengthsAndOwnedTiles,board.Tiles[x,y], board.Tiles[x-1, y]);
                UpdateState(board, ownedTiles, armyStrengthsAndOwnedTiles, x - 2, x + 3, 1, y - 2, y + 3, 1, attackState);
                yield return attackState; 
            }
            if (x < (xMax - 1) && board.Tiles[x + 1, y].Player != player)//can attack right
            {
                AttackState attackState = BuildAttackState(armyStrengthsAndOwnedTiles,board.Tiles[x,y], board.Tiles[x+1, y]);
                UpdateState(board, ownedTiles, armyStrengthsAndOwnedTiles, x + 2, x - 3, -1, y + 2, y - 3, -1, attackState);
                yield return attackState; 
            }
            if (y > 0 && board.Tiles[x, y - 1].Player != player)//can attack up
            {
                AttackState attackState = BuildAttackState(armyStrengthsAndOwnedTiles,board.Tiles[x,y], board.Tiles[x, y-1]);
                UpdateState(board, ownedTiles, armyStrengthsAndOwnedTiles, x + 2, x - 3, -1, y - 2, y + 3, 1,attackState);
                yield return attackState; 
            }
            if (y < (yMax - 1) && board.Tiles[x, y + 1].Player != player)//can attack down
            {
                AttackState attackState = BuildAttackState(armyStrengthsAndOwnedTiles,board.Tiles[x,y], board.Tiles[x, y+1]);
                UpdateState(board, ownedTiles, armyStrengthsAndOwnedTiles, x - 2, x + 3, 1, y + 2, y - 3, -1, attackState);
                yield return attackState; 
            }
        }

        private static void UpdateState(GameManager board, Tuple<Player, int>[] ownedTiles, double[] armyStrengthsAndOwnedTiles, int startX, int endX, int stepX, int startY, int endY, int stepY,AttackState attackState)
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

        private static AttackState BuildAttackState(double[] armyStrengthsAndOwnedTiles, Tile from, Tile to)
        {
            AttackState result = new AttackState();
            result.From = from;
            result.To = to;
            for (int i = 0; i < 8; ++i)
                result.State[i] = armyStrengthsAndOwnedTiles[i];
            return result;
        }
    }
}
