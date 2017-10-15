using InfluenceBot.GUI.BusinessLogic;
using InfluenceBot.GUI.BusinessLogic.Statistics;
using InfluenceBot.GUI.Model;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace InfluenceBot.GUI
{
    public partial class InfluenceBotGui : Form
    {
        private GameManager manager;
        private Graphics g;
        private Pen pen;
        private Font font;
        private Brush brush;
        private StringFormat stringFormat;
        private AttackStateNN attackStateNN;
        private ReinforceStateNN reinforceStateNN;
        private int maxX;
        private int maxY;
        private int tileWidth;
        private int tileHeight;

        public InfluenceBotGui()
        {
            InitializeComponent();
            g = picBoard.CreateGraphics();
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            var fontFamily = new FontFamily("Times New Roman");
            font = new Font(fontFamily, 16, FontStyle.Regular, GraphicsUnit.Pixel);
            brush = new SolidBrush(Color.FromArgb(255, 0, 0, 255));
            pen = new Pen(Color.Black);
            stringFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            attackStateNN = new AttackStateNN();
            reinforceStateNN = new ReinforceStateNN();
        }

        private void btnInitializeBoard_Click(object sender, EventArgs e)
        {
            manager = new GameManager();
            manager.Initialize(4);
            maxX = manager.Tiles.GetLength(0);
            maxY = manager.Tiles.GetLength(1);
            tileWidth = picBoard.Width / maxX;
            tileHeight = picBoard.Height / maxY;
            DrawBoard(manager);
        }

        private void DrawBoard(GameManager board)
        {
            for (int x = 0; x < maxX; ++x)
                for (int y = 0; y < maxY; ++y)
                    DrawTile(board.Tiles[x, y]);
        }

        private void DrawTile(Tile tile)
        {
            Color color = tile.Player?.Color ?? Color.LightGray;
            int armyCount = manager.GetArmyCount(tile.X, tile.Y);
            string armyCountText = armyCount == 0
                ? string.Empty
                : $"{armyCount}";
            var rectangleF = new RectangleF(tile.X * tileWidth, tile.Y * tileHeight, tileWidth, tileHeight);
            g.FillRectangle(new SolidBrush(color), rectangleF);
            g.DrawRectangle(pen, rectangleF.X, rectangleF.Y, rectangleF.X + rectangleF.Width, rectangleF.Y + rectangleF.Height);
            g.DrawString(armyCountText, font, brush, rectangleF, stringFormat);
        }

        private void btnExtractAndPrint_Click(object sender, EventArgs e)
        {
            var states = AttackStateExtractor.ExtractAttackStates(manager, manager.CurrentPlayer);
            txtStatistics.Text = AttackStateStatistics.GetStatistics(states, attackStateNN);
        }

        private void btnCurrentPlayerAttack_Click(object sender, EventArgs e)
            => CurrentPlayerAttack();

        private bool CurrentPlayerAttack()
        {
            var states = AttackStateExtractor.ExtractAttackStates(manager, manager.CurrentPlayer).ToList();
            states.ForEach(x => x.Score = attackStateNN.Evaluate(x));
            var chosenState = states.Where(x => x.Score > 0.5).OrderByDescending(x => x.Score).FirstOrDefault();
            if (chosenState == null)
            {
                txtStatistics.Text = "Could not attack, no attack states are good enough";
                return false; ;
            }
            manager.Attack(chosenState.From, chosenState.To);
            manager.CurrentPlayer.AttackStates.Add(chosenState);
            DrawTile(chosenState.From);
            DrawTile(chosenState.To);
            return true;
        }

        private void btnEndTurn_Click(object sender, EventArgs e)
            => EndTurn();

        private void EndTurn()
        {
            manager.CurrentPlayerIndex = (manager.CurrentPlayerIndex + 1) % manager.Players.Length;
            txtStatistics.Text = $"Current player is {manager.CurrentPlayerIndex}{Environment.NewLine}";
        }

        private void btnStartStopGame_Click(object sender, EventArgs e)
            => ToggleTimer();

        private void ToggleTimer()
        {
            tmrGame.Enabled = !tmrGame.Enabled;
            btnCurrentPlayerAttack.Enabled = !tmrGame.Enabled;
            btnExtractAndPrint.Enabled = !tmrGame.Enabled;
            btnEndTurn.Enabled = !tmrGame.Enabled;
            btnInitializeBoard.Enabled = !tmrGame.Enabled;
        }

        private void GameTick()
        {
            if (manager.Finished)
            {
                var loosingPlayer = manager.Players.OrderByDescending(x => x.Ranking).First();
                var winningPlayer = manager.Players.OrderBy(x => x.Ranking).First();
                var loosingReinforceStates = loosingPlayer.ReinforceStates.Select(x => x.State).ToArray();
                var loosingAttackStates = loosingPlayer.AttackStates.Select(x => x.State).ToArray();
                var winningReinforceStates = winningPlayer.ReinforceStates.Select(x => x.State).ToArray();
                var winningAttackStates = winningPlayer.AttackStates.Select(x => x.State).ToArray();
                for (int i = 0; i < 10; ++i)
                {
                    reinforceStateNN.teacher.RunEpoch(loosingReinforceStates, CreateDoubles(loosingReinforceStates.Length, 0));
                    attackStateNN.teacher.RunEpoch(loosingAttackStates, CreateDoubles(loosingAttackStates.Length, 0));
                    reinforceStateNN.teacher.RunEpoch(winningReinforceStates, CreateDoubles(winningReinforceStates.Length, 1));
                    attackStateNN.teacher.RunEpoch(winningAttackStates, CreateDoubles(winningAttackStates.Length, 1));
                }
                ToggleTimer();
            }
            else if (manager.AttachPhase)
            {
                if (!CurrentPlayerAttack())
                {
                    manager.AttachPhase = false;
                    manager.CurrentPlayer.Reinforcements = manager.CurrentPlayer.OwnedTiles;
                }
            }
            else
            {
                if (manager.CurrentPlayer.Reinforcements > 0)
                    CurrentPlayerReinforce();
                else
                {
                    manager.AttachPhase = true;
                    EndTurn();
                }
            }
            txtStatistics.Text = PlayerStatistics.GetStatistics(manager);
        }

        private double[][] CreateDoubles(int length, int v)
        {
            double[][] result = new double[length][];
            for (int i = 0; i < length; ++i)
                result[i] = new double []{ v };
            return result;
        }

        private void tmrGame_Tick(object sender, EventArgs e)
            => GameTick();

        private void CurrentPlayerReinforce()
        {
            var reinforceStates = ReinforceStateExtractor.ExtractReinforceStates(manager, manager.CurrentPlayer).ToList();
            reinforceStates.ForEach(x => x.Score = reinforceStateNN.Evaluate(x));
            var reinforceState = reinforceStates.OrderByDescending(x => x.Score).FirstOrDefault();
            if (reinforceState == null)
            {
                manager.CurrentPlayer.Reinforcements = 0;
                return;
            }
            DrawTile(reinforceState.Tile);
            manager.ReinforceTile(reinforceState.Tile);
            manager.CurrentPlayer.ReinforceStates.Add(reinforceState);
        }

        private void numTimerInterval_ValueChanged(object sender, EventArgs e)
            => tmrGame.Interval = (int)numTimerInterval.Value;

        private void btnAdvanceOneTick_Click(object sender, EventArgs e)
            => GameTick();
    }
}
