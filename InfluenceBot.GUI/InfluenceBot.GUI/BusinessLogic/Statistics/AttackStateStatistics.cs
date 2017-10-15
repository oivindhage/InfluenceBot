using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InfluenceBot.GUI.Model;

namespace InfluenceBot.GUI.BusinessLogic.Statistics
{
    public static class AttackStateStatistics
    {
        internal static string GetStatistics(IEnumerable<AttackState> states, AttackStateNN attackStateNN)
        {
            var sb = new StringBuilder();
            foreach (var state in states)
            {
                sb.Append($"From {state.From.X},{state.From.Y} to {state.To.X},{state.To.Y}, score {attackStateNN.Evaluate(state)}{Environment.NewLine}");
                sb.Append(string.Join("\t", state.State.Skip(0).Take(4).Select(x => $"{x}").ToArray()) + Environment.NewLine);
                sb.Append(string.Join("\t", state.State.Skip(4).Take(4).Select(x => $"{x}").ToArray()) + Environment.NewLine);
                sb.Append(Environment.NewLine);
                for (int i = 0; i < 4; ++i)
                {
                    for (int row = 0; row < 5; ++row)
                        sb.Append(string.Join("\t", state.State.Skip(8 + i * 25 + row * 5).Take(5).Select(x => $"{x}").ToArray()) + Environment.NewLine);
                    sb.Append(Environment.NewLine);
                }
                sb.Append(Environment.NewLine + "---------------------------------------------" + Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}
