using InfluenceBot.GUI.Model;
using System.Linq;
using System.Text;

namespace InfluenceBot.GUI.BusinessLogic.Statistics
{
    public static class PlayerStatistics
    {
        public static string GetStatistics(GameManager board)
        {
            var sb = new StringBuilder();
            int ranking = 1;
            foreach (var player in board.Players)
            {
                sb.AppendLine($"Player {ranking++} {player.Color.Name} with army strength {player.TotalArmyStrength} {player.Tiles.Sum(x=>x.ArmyCount)}");
            }
            sb.AppendLine("-------------------------------------------");
            return sb.ToString();
        }
    }
}
