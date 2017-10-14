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
        private Font font;
        private Brush brush;
        private StringFormat stringFormat;

        public InfluenceBotGui()
        {
            InitializeComponent();
            g = picBoard.CreateGraphics();
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            var fontFamily = new FontFamily("Times New Roman");
            font = new Font(fontFamily, 16, FontStyle.Regular, GraphicsUnit.Pixel);
            brush = new SolidBrush(Color.FromArgb(255, 0, 0, 255));
            stringFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        }

        private void btnInitializeBoard_Click(object sender, EventArgs e)
        {
            board = new Board();
            board.Initialize(3);
            DrawBoard(board);
        }

        private void DrawBoard(Board board)
        {
            int maxX = board.Tiles.GetLength(0);
            int maxY = board.Tiles.GetLength(1);
            int tileWidth = picBoard.Width / maxX;
            int tileHeight = picBoard.Height / maxY;
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
            g.DrawString(armyCountText, font, brush, rectangleF, stringFormat);
        }

        private void btnExtractAndPrint_Click(object sender, EventArgs e)
        {
            var states = AttackStateExtractor.ExtractAttackStates(board, board.Players[board.CurrentPlayer]);
            foreach (var state in states)
            {
                txtStatistics.AppendText($"From {state.FromX},{state.FromY} to {state.ToX},{state.ToY}{Environment.NewLine}");
                txtStatistics.AppendText(string.Join(Environment.NewLine, state.State.Select(x => x.ToString())));
                txtStatistics.AppendText(Environment.NewLine + "---------------------------------------------" + Environment.NewLine);
            }
        }
    }
}
