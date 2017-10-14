using InfluenceBot.GUI.BusinessLogic;
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
        private Pen pen = new Pen(Color.Black);
        private Font font;
        private Brush brush;
        private StringFormat stringFormat;
        private AttackStateNN attackStateNN;
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
            stringFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            attackStateNN = new AttackStateNN();
        }

        private void btnInitializeBoard_Click(object sender, EventArgs e)
        {
            board = new Board();
            board.Initialize(3);
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
                    DrawTile(board, tileWidth, tileHeight, x, y);
        }

        private void DrawTile(Board board, int tileWidth, int tileHeight, int x, int y)
        {
            Color color = board.GetColor(x, y);
            int armyCount = board.GetArmyCount(x, y);
            string armyCountText = armyCount == 0
                ? string.Empty
                : $"{armyCount}";
            var rectangleF = new RectangleF(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
            g.FillRectangle(new SolidBrush(color), rectangleF);
            g.DrawRectangle(pen, rectangleF.X, rectangleF.Y, rectangleF.X + rectangleF.Width, rectangleF.Y + rectangleF.Height);
            g.DrawString(armyCountText, font, brush, rectangleF, stringFormat);
        }

        private void btnExtractAndPrint_Click(object sender, EventArgs e)
        {
            var states = AttackStateExtractor.ExtractAttackStates(board, board.Players[board.CurrentPlayer]);
            foreach (var state in states)
            {
                txtStatistics.AppendText($"From {state.FromX},{state.FromY} to {state.ToX},{state.ToY}, score {attackStateNN.Evaluate(state)}{Environment.NewLine}");
                txtStatistics.AppendText(string.Join("\t", state.State.Skip(0).Take(4).Select(x => $"{x}").ToArray()) + Environment.NewLine);
                txtStatistics.AppendText(string.Join("\t", state.State.Skip(4).Take(4).Select(x => $"{x}").ToArray()) + Environment.NewLine);
                txtStatistics.AppendText(Environment.NewLine);
                for (int i = 0; i < 4; ++i)
                {
                    for (int row = 0; row < 5; ++row)
                        txtStatistics.AppendText(string.Join("\t", state.State.Skip(8 + i * 25 + row * 5).Take(5).Select(x => $"{x}").ToArray()) + Environment.NewLine);
                    txtStatistics.AppendText(Environment.NewLine);
                }
                txtStatistics.AppendText(Environment.NewLine + "---------------------------------------------" + Environment.NewLine);
            }
        }

        private void btnCurrentPlayerAttack_Click(object sender, EventArgs e)
        {
            var states = AttackStateExtractor.ExtractAttackStates(board, board.Players[board.CurrentPlayer]).ToList();
            states.ForEach(x => x.Score = attackStateNN.Evaluate(x));
            var chosenState = states.Where(x => x.Score > 0.5).OrderByDescending(x => x.Score).FirstOrDefault();
            if (chosenState == null)
            {
                txtStatistics.Text = "Could not attack, no attack states are good enough";
                return;
            }
            board.Attack(chosenState.From, chosenState.To);
            DrawTile(board, tileWidth, tileHeight, chosenState.FromX, chosenState.FromY);
            DrawTile(board, tileWidth, tileHeight, chosenState.ToX, chosenState.ToY);
        }

        private void btnEndTurn_Click(object sender, EventArgs e)
        {
            var currentPlayer = board.Players[board.CurrentPlayer];
            foreach (var tile in currentPlayer.Tiles.Where(x => x.ArmyCount < 5))
            {
                tile.ArmyCount++;
                currentPlayer.TotalArmyStrength++;
                DrawTile(board, tileWidth, tileHeight, tile.X, tile.Y);
            }
            board.CurrentPlayer = (board.CurrentPlayer + 1) % board.Players.Length;
            txtStatistics.Text = $"Current player is {board.CurrentPlayer}{Environment.NewLine}";
        }
    }
}
