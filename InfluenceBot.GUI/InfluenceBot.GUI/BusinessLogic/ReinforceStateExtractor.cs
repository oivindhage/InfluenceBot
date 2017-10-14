using InfluenceBot.GUI.Model;
using System.Collections.Generic;
using System;

namespace InfluenceBot.GUI.BusinessLogic
{
    public static class ReinforceStateExtractor
    {
        public static IEnumerable<ReinforceState> ExtractReinforceStates(Board board, Player player)
        {
            int xMax = board.Tiles.GetLength(0);
            int yMax = board.Tiles.GetLength(1);
            Tuple<Player, int>[] armyStrengths = StateExtractor.GetArmyStrengths(board, player);
            Tuple<Player, int>[] ownedTiles = StateExtractor.GetOwnedTiles(board, player);
            double[] armyStrengthsAndOwnedTiles = StateExtractor.GetArmyStrengthsAndOwnedTiles(armyStrengths, ownedTiles);
            for (int x = 0; x < xMax; ++x)
                for (int y = 0; y < yMax; ++y)
                    if (board.Tiles[x, y].Player == player && board.Tiles[x, y].ArmyCount < 5)
                        foreach (var reinforceState in ExtractReinforcementStates(board, player, x, y, xMax, yMax, armyStrengths, ownedTiles, armyStrengthsAndOwnedTiles))
                            yield return reinforceState;
        }

        private static IEnumerable<ReinforceState> ExtractReinforcementStates(Board board, Player player, int x, int y, int xMax, int yMax, Tuple<Player, int>[] armyStrengths, Tuple<Player, int>[] ownedTiles, double[] armyStrengthsAndOwnedTiles)
        {
            var reinforceState = BuildReinforceState(armyStrengthsAndOwnedTiles, board.Tiles[x, y]);
            //left heavy board
            UpdateState(board, ownedTiles, x - 2, x + 3, 1, y - 2, y + 3, 1, reinforceState);
            double maxValue = reinforceState.GetWeight();
            //right heavy board
            var reinforceStateTmp = BuildReinforceState(armyStrengthsAndOwnedTiles, board.Tiles[x, y]);
            UpdateState(board, ownedTiles, x + 2, x - 3, -1, y + 2, y - 3, -1, reinforceStateTmp);
            double valueTmp = reinforceStateTmp.GetWeight();
            if (valueTmp > maxValue)
            {
                maxValue = valueTmp;
                reinforceState = reinforceStateTmp;
                reinforceStateTmp = BuildReinforceState(armyStrengthsAndOwnedTiles, board.Tiles[x, y]);
            }
            //top heavy board
            UpdateState(board, ownedTiles, x + 2, x - 3, -1, y - 2, y + 3, 1, reinforceStateTmp);
            valueTmp = reinforceStateTmp.GetWeight();
            if (valueTmp > maxValue)
            {
                maxValue = valueTmp;
                reinforceState = reinforceStateTmp;
                reinforceStateTmp = BuildReinforceState(armyStrengthsAndOwnedTiles, board.Tiles[x, y]);
            }
            //bottom heavy board
            UpdateState(board, ownedTiles, x - 2, x + 3, 1, y + 2, y - 3, -1, reinforceStateTmp);
            valueTmp = reinforceStateTmp.GetWeight();
            if (valueTmp > maxValue)
                reinforceState = reinforceStateTmp;
            yield return reinforceState;
        }

        private static void UpdateState(Board board, Tuple<Player, int>[] ownedTiles, int startX, int endX, int stepX, int startY, int endY, int stepY, ReinforceState reinforceState)
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
                    SetInput(ownedTiles, reinforceState, counter, currentTile);
                    counter++;
                }
            }
        }

        private static void SetInput(Tuple<Player, int>[] ownedTiles, ReinforceState reinforceState, int counter, Tile currentTile)
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
            reinforceState.State[8 + counter + (25 * tileOwner)] = currentTile.ArmyCount / 5.0;
        }

        private static ReinforceState BuildReinforceState(double[] armyStrengthsAndOwnedTiles, Tile tile)
        {
            var result = new ReinforceState();
            result.Tile = tile;
            for (int i = 0; i < 8; ++i)
                result.State[i] = armyStrengthsAndOwnedTiles[i];
            return result;
        }
    }
}
