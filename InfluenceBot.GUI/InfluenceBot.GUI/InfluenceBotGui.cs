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
        private Board board;
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
            board = new Board();
            board.Initialize(4);
            maxX = board.Tiles.GetLength(0);
            maxY = board.Tiles.GetLength(1);
            tileWidth = picBoard.Width / maxX;
            tileHeight = picBoard.Height / maxY;
            DrawBoard(board);
        }

        private void DrawBoard(Board board)
        {
            for (int x = 0; x < maxX; ++x)
                for (int y = 0; y < maxY; ++y)
                    DrawTile(board.Tiles[x, y]);
        }

        private void DrawTile(Tile tile)
        {
            Color color = tile.Player?.Color ?? Color.LightGray;
            int armyCount = board.GetArmyCount(tile.X, tile.Y);
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
            var states = AttackStateExtractor.ExtractAttackStates(board, board.CurrentPlayer);
            txtStatistics.Text= AttackStateStatistics.GetStatistics(states, attackStateNN);
        }

        private void btnCurrentPlayerAttack_Click(object sender, EventArgs e)
            => CurrentPlayerAttack();

        private bool CurrentPlayerAttack()
        {
            var states = AttackStateExtractor.ExtractAttackStates(board, board.CurrentPlayer).ToList();
            states.ForEach(x => x.Score = attackStateNN.Evaluate(x));
            var chosenState = states.Where(x => x.Score > 0.5).OrderByDescending(x => x.Score).FirstOrDefault();
            if (chosenState == null)
            {
                txtStatistics.Text = "Could not attack, no attack states are good enough";
                return false; ;
            }
            board.Attack(chosenState.From, chosenState.To);
            board.CurrentPlayer.AttackStates.Add(chosenState);
            DrawTile(chosenState.From);
            DrawTile(chosenState.To);
            return true;
        }

        private void btnEndTurn_Click(object sender, EventArgs e)
            => EndTurn();

        private void EndTurn()
        {
            board.CurrentPlayerIndex = (board.CurrentPlayerIndex + 1) % board.Players.Length;
            txtStatistics.Text = $"Current player is {board.CurrentPlayerIndex}{Environment.NewLine}";
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
            if (board.Finished)
            {
                ToggleTimer();
            }
            else if (board.AttachPhase)
            {
                if (!CurrentPlayerAttack())
                {
                    board.AttachPhase = false;
                    board.CurrentPlayer.Reinforcements = board.CurrentPlayer.OwnedTiles;
                }
            }
            else
            {
                if (board.CurrentPlayer.Reinforcements > 0)
                    CurrentPlayerReinforce();
                else
                {
                    board.AttachPhase = true;
                    EndTurn();
                }
            }
            txtStatistics.Text = PlayerStatistics.GetStatistics(board);
        }

        private void tmrGame_Tick(object sender, EventArgs e)
            => GameTick();

        private void CurrentPlayerReinforce()
        {
            var reinforceStates = ReinforceStateExtractor.ExtractReinforceStates(board, board.CurrentPlayer).ToList();
            reinforceStates.ForEach(x => x.Score = reinforceStateNN.Evaluate(x));
            var reinforceState = reinforceStates.OrderByDescending(x => x.Score).FirstOrDefault();
            if (reinforceState == null)
            {
                board.CurrentPlayer.Reinforcements = 0;
                return;
            }
            reinforceState.Tile.ArmyCount++;
            DrawTile(reinforceState.Tile);
            board.CurrentPlayer.TotalArmyStrength++;
            board.CurrentPlayer.Reinforcements--;
            board.CurrentPlayer.ReinforceStates.Add(reinforceState);
        }

        private void numTimerInterval_ValueChanged(object sender, EventArgs e)
            => tmrGame.Interval = (int)numTimerInterval.Value;

        private void btnAdvanceOneTick_Click(object sender, EventArgs e)
            => GameTick();
    }
}
