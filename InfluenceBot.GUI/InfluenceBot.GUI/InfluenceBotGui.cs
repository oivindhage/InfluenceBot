using InfluenceBot.GUI.BusinessLogic;
using InfluenceBot.GUI.BusinessLogic.Statistics;
using InfluenceBot.GUI.Model;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Threading.Tasks;
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
        private bool RunTask;
        private int episodeCounter;
        private int epsilon;
        private Random r;

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
            episodeCounter = 0;
            r = new Random();
            InitializeManager();
        }

        private void InfluenceBotGui_Validated(object sender, EventArgs e)
            => InitializeManager();

        private void btnInitializeBoard_Click(object sender, EventArgs e)
            => InitializeManager();

        private void InitializeManager()
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

        private Tile[] CurrentPlayerAttack()
        {
            var states = AttackStateExtractor.ExtractAttackStates(manager, manager.CurrentPlayer).ToList();
            states.ForEach(x => x.Score = attackStateNN.Evaluate(x));
            var chosenState = epsilon > r.Next(1, 101)
                ? states.OrderBy(x => r.NextDouble()).FirstOrDefault()
                : states.Where(x => x.Score > 0.5).OrderByDescending(x => x.Score).FirstOrDefault();
            if (chosenState == null)
                return null;
            manager.Attack(chosenState.From, chosenState.To);
            manager.CurrentPlayer.AttackStates.Add(chosenState);
            return new Tile[] { chosenState.From, chosenState.To };
        }

        private void btnEndTurn_Click(object sender, EventArgs e)
            => EndTurn();

        private void EndTurn()
        {
            manager.CurrentPlayerIndex = (manager.CurrentPlayerIndex + 1) % manager.Players.Length;
        }

        private void btnStartStopGame_Click(object sender, EventArgs e)
            => ToggleTimer();

        private void ToggleTimer()
        {
            tmrGame.Enabled = !tmrGame.Enabled;
            btnExtractAndPrint.Enabled = !tmrGame.Enabled;
            btnEndTurn.Enabled = !tmrGame.Enabled;
            btnInitializeBoard.Enabled = !tmrGame.Enabled;
        }

        private Tile[] GameTick()
        {
            Tile[] tilesToBeRedrawn = null;
            if (manager.Finished)
            {
                var losingPlayer = manager.Players.OrderByDescending(x => x.Ranking).First();
                var winningPlayer = manager.Players.OrderBy(x => x.Ranking).First();
                var losingReinforceStates = losingPlayer.ReinforceStates.Select(x => x.State).ToArray();
                var losingAttackStates = losingPlayer.AttackStates.Select(x => x.State).ToArray();
                var winningReinforceStates = winningPlayer.ReinforceStates.Select(x => x.State).ToArray();
                var winningAttackStates = winningPlayer.AttackStates.Select(x => x.State).ToArray();
                for (int i = 0; i < 10; ++i)
                {
                    reinforceStateNN.teacher.RunEpoch(losingReinforceStates, CreateDoubles(losingReinforceStates.Length, 0));
                    attackStateNN.teacher.RunEpoch(losingAttackStates, CreateDoubles(losingAttackStates.Length, 0));
                    reinforceStateNN.teacher.RunEpoch(winningReinforceStates, CreateDoubles(winningReinforceStates.Length, 1));
                    attackStateNN.teacher.RunEpoch(winningAttackStates, CreateDoubles(winningAttackStates.Length, 1));
                }
            }
            else if (manager.AttachPhase)
            {
                tilesToBeRedrawn = CurrentPlayerAttack();
                if (tilesToBeRedrawn == null)
                {
                    manager.AttachPhase = false;
                    manager.CurrentPlayer.Reinforcements = manager.CurrentPlayer.OwnedTiles;
                }
            }
            else
            {
                if (manager.CurrentPlayer.Reinforcements > 0)
                {
                    var tile = CurrentPlayerReinforce();
                    if (tile != null)
                        tilesToBeRedrawn = new Tile[] { tile };
                }
                else
                {
                    manager.AttachPhase = true;
                    EndTurn();
                }
            }
            return tilesToBeRedrawn;
        }

        private double[][] CreateDoubles(int length, int v)
        {
            double[][] result = new double[length][];
            for (int i = 0; i < length; ++i)
                result[i] = new double[] { v };
            return result;
        }

        private void tmrGame_Tick(object sender, EventArgs e)
        {
            ProceedAndUpdateUI();
            if (manager.Finished)
                ToggleTimer();
        }

        private void ProceedAndUpdateUI()
        {
            var tilesToBeRedrawn = GameTick();
            if (tilesToBeRedrawn != null)
                foreach (var tile in tilesToBeRedrawn)
                    DrawTile(tile);
        }

        private Tile CurrentPlayerReinforce()
        {
            var reinforceStates = ReinforceStateExtractor.ExtractReinforceStates(manager, manager.CurrentPlayer).ToList();
            reinforceStates.ForEach(x => x.Score = reinforceStateNN.Evaluate(x));
            var chosenState = epsilon > r.Next(1, 101)
                ? reinforceStates.OrderBy(x => r.NextDouble()).FirstOrDefault()
                : reinforceStates.OrderByDescending(x => x.Score).FirstOrDefault();
            if (chosenState == null)
            {
                manager.CurrentPlayer.Reinforcements = 0;
                return null;
            }
            manager.ReinforceTile(chosenState.Tile);
            manager.CurrentPlayer.ReinforceStates.Add(chosenState);
            return chosenState.Tile;
        }

        private void numTimerInterval_ValueChanged(object sender, EventArgs e)
            => tmrGame.Interval = (int)numTimerInterval.Value;

        private void btnAdvanceOneTick_Click(object sender, EventArgs e)
            => ProceedAndUpdateUI();

        private void btnFullSpeedLearning_Click(object sender, EventArgs e)
        {
            if (RunTask)
            {
                RunTask = false;
                return;
            }
            RunTask = true;
            Task t = new Task(() =>
            {
                while (RunTask)
                {
                    GameTick();
                    if (manager.Finished)
                    {
                        GameTick();
                        episodeCounter++;
                        manager.Initialize(4);
                    }
                }
            });
            t.Start();
        }

        private void btnSaveNetworks_Click(object sender, EventArgs e)
        {
            attackStateNN.network.Save(txtNetworkPath.Text + "\\AttackNetwork.nn");
            reinforceStateNN.network.Save(txtNetworkPath.Text + "\\ReinforceNetwork.nn");
        }

        private void btnLoadNetworks_Click(object sender, EventArgs e)
        {
            attackStateNN.Load(txtNetworkPath.Text + "\\AttackNetwork.nn");
            reinforceStateNN.Load(txtNetworkPath.Text + "\\ReinforceNetwork.nn");
        }

        private void btnRedrawEverything_Click(object sender, EventArgs e)
        {
            DrawBoard(manager);
            lblEpisode.Text = $"{episodeCounter}";
        }

        private void tbrEpsilon_ValueChanged(object sender, EventArgs e)
            => epsilon = tbrEpsilon.Value;
    }
}
