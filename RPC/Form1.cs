using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace RPC
{
    public class PlayersList
	{
        public List<Player> Players{ get; set; }
	}
    public class Player
    {
        public string Name { get; set; }
        public int Victories { get; set; }
        public int Losses { get; set; }
    }

    public partial class Form1 : Form
    {
        Label lbl, final, scores, picSlave, choicePlr1, choicePlr2, leaderboardL;
        Random rnd;
        Button btn, scoreB, about, lockpl1, lockpl2, showpl1, showpl2, leaderboardToggle;
        CheckBox pvppvpe;
        TextBox plrT, plr2T, feed;
        PictureBox pic, pic2;
        PlayersList players;
        ListView leaderboard;
        private List<string> playerNames;
        private string _plr1Hand = string.Empty;
        private string _plr2Hand = string.Empty;
        private Color draw;

        public string plr1Hand
        {
            get { return _plr1Hand; }
            set
            {
                if (_plr1Hand != value)
                    _plr1Hand = value;
            }
        }
        public string plr2Hand
        {
            get { return _plr2Hand; }
            set
            {
                if (_plr2Hand != value)
                    _plr2Hand = value;
            }
        }
        public Form1()
        {
            draw = Color.FromArgb(92, 93, 99);
            this.Height = 700;
            this.Width = 525;
            this.Text = "Kivi, Käärid, Paber";
            this.BackColor = draw;
            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;
            //Greeting label
            lbl = new Label();
            lbl.Width = 300;
            lbl.Height = 50;
            lbl.Text = "Tere tulemast rakendusse Kivi, Käärid, Paber!\nValige oma käsi ja vajutage nuppu Esita!";
            lbl.Location = new Point(140, 20);
            this.Controls.Add(lbl);
            //Final label
            final = new Label();
            final.Width = 300;
            final.Location = new Point(210, 400);
            final.Hide();
            this.Controls.Add(final);
            //plrP label
            plrT = new TextBox();
            plrT.Width = 100;
            plrT.Height = 20;
            plrT.Text = "Mangija 1";
            plrT.Location = new Point(200, 75);
            this.Controls.Add(plrT);
            //lock pl2
            lockpl1 = new Button();
            lockpl1.Location = new Point(232, 105);
            lockpl1.Width = 40;
            lockpl1.Text = "Vali";
            lockpl1.Click += Lockpl1_Click;
            this.Controls.Add(lockpl1);
            //plrP2 label
            plr2T = new TextBox();
            plr2T.Width = 100;
            plr2T.Height = 20;
            plr2T.Text = "Mangija 2";
            plr2T.Location = new Point(200, 175);
            this.Controls.Add(plr2T);
            //lock pl2
            lockpl2 = new Button();
            lockpl2.Location = new Point(232, 205);
            lockpl2.Width = 40;
            lockpl2.Text = "Vali";
            lockpl2.Click += Lockpl2_Click;
            this.Controls.Add(lockpl2);
            //play button
            btn = new Button
            {
                Text = "Mängi",
                Location = new Point(200, 300),
                Height = 30,
                Width = 100
            };
            btn.Click += cycle;
            this.Controls.Add(btn);
            //pvepvp check
            pvppvpe = new CheckBox();
            pvppvpe.Location = new Point(225, 280);
            pvppvpe.Text = "PVP?";
            pvppvpe.Checked = true;
            pvppvpe.CheckedChanged += Pvppvpe_CheckedChanged;
            this.Controls.Add(pvppvpe);
            //score feed
            feed = new TextBox();
            feed.ReadOnly = true;
            feed.Location = new Point(0, 0);
            feed.Width = 200;
            feed.Height = 300;
            feed.Multiline = true;
            //this.Controls.Add(feed);
            //last10 label
            scores = new Label();
            scores.Location = new Point(25, 120);
            scores.Text = "Kuva viimased 10 tulemust";
            scores.Height = 15;
            scores.Width = 200;
            this.Controls.Add(scores);
            //last 10 scores
            scoreB = new Button();
            scoreB.Location = new Point(25, 145);
            scoreB.Text = "Kuva";
            this.Controls.Add(scoreB);
            scoreB.Click += ScoreB_Click;
            //pic pl1
            pic = new PictureBox();
            pic.Location = new Point(100, 450);
            pic.Width = 100;
            pic.Height = 100;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(pic);
            //pic pl2
            pic2 = new PictureBox();
            pic2.Location = new Point(300, 450);
            pic2.Width = 100;
            pic2.Height = 100;
            pic2.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(pic2);
            //pic slave
            picSlave = new Label();
            picSlave.Location = new Point(200, 450);
            picSlave.Font = new Font("Arial", 76);
            picSlave.Width = 100;
            picSlave.Height = 100;
            this.Controls.Add(picSlave);
            //about button
            about = new Button();
            about.Location = new Point(700, 530);
            about.Text = "About";
            about.Click += About_Click;
            this.Controls.Add(about);
            //label plr1
            choicePlr1 = new Label();
            choicePlr1.Location = new Point(170, 140);
            choicePlr1.Width = 150;
            choicePlr1.TextAlign = ContentAlignment.MiddleCenter;
            choicePlr1.BackColor = Color.Black;
            choicePlr1.Font = new Font("Arial", 16);
            choicePlr1.Hide();
            this.Controls.Add(choicePlr1);
            //lbl1 button to show
            showpl1 = new Button();
            showpl1.Location = new Point(330, 140);
            showpl1.Text = "Näidata";
            showpl1.Hide();
            showpl1.Click += delegate { choicePlr1.BackColor = draw; };
            this.Controls.Add(showpl1);
            //label plr2
            choicePlr2 = new Label();
            choicePlr2.Location = new Point(170, 240);
            choicePlr2.Width = 150;
            choicePlr2.TextAlign = ContentAlignment.MiddleCenter;
            choicePlr2.BackColor = Color.Black;
            choicePlr2.Font = new Font("Arial", 16);
            choicePlr2.Hide();
            this.Controls.Add(choicePlr2);
            //lbl2 button to show
            showpl2 = new Button();
            showpl2.Location = new Point(330, 240);
            showpl2.Text = "Näidata";
            showpl2.Hide();
            showpl2.Click += delegate { choicePlr2.BackColor = draw; };
            this.Controls.Add(showpl2);
            //leaderboard
            leaderboard = new ListView();
            leaderboard.Location = new Point(0, 0);
            leaderboard.GridLines = true;
            leaderboard.View = View.Details;
            leaderboard.Width = 245;
            leaderboard.Height = 300;
            leaderboard.Sorting = SortOrder.Descending;
            leaderboard.Columns.Add("Name");
            leaderboard.Columns.Add("Victories");
            leaderboard.Columns.Add("Losses");
            leaderboard.Columns.Add("Win%");
            //this.Controls.Add(leaderboard);
            //showleader
            leaderboardToggle = new Button();
            leaderboardToggle.Location = new Point(25, 220);
            leaderboardToggle.Text = "Kuva";
            this.Controls.Add(leaderboardToggle);
            leaderboardToggle.Click += LeaderboardToggle_Click;
            //label leaderboard
            leaderboardL = new Label();
            leaderboardL.Location = new Point(25, 190);
            leaderboardL.Text = "Kuva 10 parimad mängijad";
            leaderboardL.Width = 200;
            this.Controls.Add(leaderboardL);
        }

        private void LeaderboardToggle_Click(object sender, EventArgs e)
        {
            //if (leaderboard.Visible) { leaderboard.Hide(); return; }
            //else leaderboard.Show();
            Form3 fr = new Form3(this, leaderboard);
            fr.Show();
        }

        private void PopulateLeaderboard(Player plr)
        {
            ListViewItem itm = new ListViewItem(plr.Name);
            itm.SubItems.Add(plr.Victories.ToString());
            itm.SubItems.Add(plr.Losses.ToString());
            decimal winrate;
            try
            {
                winrate = Math.Round((decimal)plr.Victories / (plr.Losses + plr.Victories) * 100, 1);
            }
            catch(DivideByZeroException)
            {
                 winrate = 0.0M;
            }
            itm.SubItems.Add(winrate.ToString() + "%");
            leaderboard.Items.Add(itm);
            ListViewItemStringComparer sorter = new ListViewItemStringComparer(3,SortOrder.Descending);
            leaderboard.ListViewItemSorter = sorter;
            leaderboard.Sort();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string json = JsonConvert.SerializeObject(players);
            File.WriteAllText(@"../../players.json", json);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string file = Regex.Replace(File.ReadAllText(@"../../players.json"), @"[\r\n\t ]+", " ").ToString();
            players = JsonConvert.DeserializeObject<PlayersList>(file);
            playerNames = new List<string>();
            foreach (Player player in players.Players)
            {
                playerNames.Add(player.Name);
                PopulateLeaderboard(player);
            }

        }

        private void Lockpl1_Click(object sender, EventArgs e)
        {
            playerPick pick = new playerPick(this, plr2T.Text, true);
            pick.Show();
        }

        private void Lockpl2_Click(object sender, EventArgs e)
        {
            playerPick pick = new playerPick(this, plr2T.Text, false);
            pick.Show();
        }
        public void SetHiddenLabel(bool plr1)
        {
            if (plr1)
            {
                choicePlr1.Show();
                showpl1.Show();
                choicePlr1.BackColor = Color.Black;
                choicePlr1.Text = _plr1Hand;
            }
            else
            {
                choicePlr2.Show();
                showpl2.Show();
                choicePlr2.BackColor = Color.Black;
                choicePlr2.Text = _plr2Hand;
            }
        }
        private void About_Click(object sender, EventArgs e)
        {
            Form2 aboutF = new Form2();
            aboutF.Show();
        }

        private void ScoreB_Click(object sender, EventArgs e)
        {
            try
            {
                //if (feed.Visible) { feed.Hide(); return; }
                //else feed.Show();
                feed.Text = "";
                string[] lines = File.ReadAllLines(@"../../score.txt");
                for (int i = lines.Length - 11; i < lines.Length; i++)
                {
                    feed.Text += lines[i] + Environment.NewLine;
                }
                Form3 fr = new Form3(this, feed);
                fr.Show();
            }
            catch
            {
                Console.WriteLine("error");
            }
        }

        private void Pvppvpe_CheckedChanged(object sender, EventArgs e)
        {
            if (pvppvpe.Checked)
            {
                lockpl2.Show();
                plr2T.Show();
            }
            else
            {
                plr2T.Hide();
                lockpl2.Hide();
            }
        }

        private void cycle(object sender, EventArgs e)
        {
            Player p, p2;
            if (!playerNames.Contains(plrT.Text)) { p = CreatePlayer(plrT.Text, 0, 0); }
            else 
            if (!playerNames.Contains(plr2T.Text)) { p2 = CreatePlayer(plr2T.Text, 0, 0); }
            switch (Play())
            {
                case 0:
                    final.Text = "Viik!";
                    this.BackColor = draw;
                    choicePlr1.BackColor = draw;
                    choicePlr2.BackColor = draw;
                    picSlave.Text = "=";
                    break;
                case 1:
                    final.Text = $"{plrT.Text} võitis!";
                    this.BackColor = Color.Green;
                    choicePlr1.BackColor = Color.Green;
                    choicePlr2.BackColor = Color.Green;
                    picSlave.Text = ">";
                    foreach (Player player in players.Players)
                    {
                        if (player.Name == plrT.Text) { player.Victories += 1; p = player; }
                        if (player.Name == plr2T.Text) player.Losses += 1;
                    }
                    break;
                case 2:
                    final.Text = $"{plr2T.Text} võitis!";
                    this.BackColor = Color.Red;
                    choicePlr1.BackColor = Color.Red;
                    choicePlr2.BackColor = Color.Red;
                    picSlave.Text = "<";
                    foreach (Player player in players.Players)
                    {
                        if (player.Name == plr2T.Text) { player.Victories += 1; p2 = player; }
                        if (player.Name == plrT.Text) player.Losses += 1;
                    }
                    break;
                default:
                    final.Text = "Valige, mida mängite.";
                    break;
            }

            lockpl1.Show();
            if (pvppvpe.Checked) lockpl2.Show();
            _plr1Hand = String.Empty;
            _plr2Hand = String.Empty;
            File.AppendAllText(@"../../score.txt", final.Text + Environment.NewLine);
            UpdateLeaderboard();
            final.Show();
        }
        //0 = draw, 1 = win, 2 = lose
        private int Play()
        {
            if (_plr1Hand == string.Empty) return 5;
            if (pvppvpe.Checked)
            {
                if (_plr2Hand == string.Empty) return 5;
            }
            else
            {
                _plr2Hand = randomPiece();
                plr2T.Text = "Mangija 2";
            }
            setImg(_plr1Hand, _plr2Hand);
            return winCheck(_plr1Hand, _plr2Hand);
        }
        private void setImg(string plr, string plr2)
        {
            Random rnd = new Random();
            switch (plr)
            {
                case "scissors":
                    pic.Image = Image.FromFile($"../../pics/scissors{rnd.Next(1, 3)}.jpg");
                    break;
                case "paper":
                    pic.Image = Image.FromFile("../../pics/paper.jpg");
                    break;
                case "rock":
                    pic.Image = Image.FromFile($"../../pics/rock{rnd.Next(1, 4)}.jpg");
                    break;
            }
            switch (plr2)
            {
                case "scissors":
                    pic2.Image = Image.FromFile($"../../pics/scissors{rnd.Next(1, 3)}.jpg");
                    break;
                case "paper":
                    pic2.Image = Image.FromFile("../../pics/paper.jpg");
                    break;
                case "rock":
                    pic2.Image = Image.FromFile($"../../pics/rock{rnd.Next(1, 4)}.jpg");
                    break;
            }
        }
        private string randomPiece()
        {
            rnd = new Random();
            int num = rnd.Next(0, 3);
            switch (num)
            {
                case 0:
                    return "scissors";
                case 1:
                    return "paper";
                case 2:
                    return "rock";
                default:
                    return "error";
            }
        }

        //0 = draw, 1 = win, 2 = lose
        private int winCheck(string player, string enemy)
        {
            if (player == enemy)
            {
                return 0;
            }
            switch (player)
            {
                case "scissors":
                    switch (enemy)
                    {
                        case "paper":
                            return 1;
                        case "rock":
                            return 2;
                    }
                    break;
                case "rock":
                    switch (enemy)
                    {
                        case "scissors":
                            return 1;
                        case "paper":
                            return 2;
                    }
                    break;
                case "paper":
                    switch (enemy)
                    {
                        case "rock":
                            return 1;
                        case "scissors":
                            return 2;
                    }
                    break;
            }
            return 0;
        }
        public Player CreatePlayer(string _name, int _victories, int _losses)
        {
            Player plr = new Player { Name = _name, Victories = _victories, Losses = _losses };
            players.Players.Add(plr);
            playerNames.Add(_name);
            return plr;
        }
        public void UpdateLeaderboard()
        {
            leaderboard.Items.Clear();
            foreach (Player plr in players.Players)
            {
                ListViewItem itm = new ListViewItem(plr.Name);
                itm.SubItems.Add(plr.Victories.ToString());
                itm.SubItems.Add(plr.Losses.ToString());
                decimal winrate;
                try
                {
                    winrate = Math.Round((decimal)plr.Victories / (plr.Losses + plr.Victories) * 100, 1);
                }
                catch (DivideByZeroException)
                {
                    winrate = 0.0M;
                }
                itm.SubItems.Add(winrate.ToString() + "%");
                leaderboard.Items.Add(itm);
                ListViewItemStringComparer sorter = new ListViewItemStringComparer(3, SortOrder.Descending);
                leaderboard.ListViewItemSorter = sorter;
                leaderboard.Sort();
            }
        }
        public void clearForm3(Form3 fr)
        {
            fr.BackColor = Color.Green;
            fr.Hide();
        }
    }
}
