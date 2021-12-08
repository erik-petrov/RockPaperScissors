﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPC
{
    public partial class Form1 : Form
    {
        Label lbl, final, scores, picSlave, choicePlr1, choicePlr2;
        Random rnd;
        Button btn, scoreB, about, lockpl1, lockpl2, showpl1, showpl2;
        CheckBox pvppvpe;
        TextBox plrT, plr2T, feed;
        PictureBox pic, pic2;
        private string _plr1Hand = string.Empty;
        private string _plr2Hand = string.Empty;

        public string plr1Hand
        {
            get {return _plr1Hand; }
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
            this.Height = 600;
            this.Width = 800;
            this.Text = "Kivi, Käärid, Paber";
            this.BackColor = Color.White;
            //Greeting label
            lbl = new Label();
            lbl.Width = 300;
            lbl.Height = 50;
            lbl.Text = "Tere tulemast rakendusse Kivi, Käärid, Paber!\nValige oma käsi ja vajutage nuppu Esita!";
            lbl.Location = new Point(110, 20);
            this.Controls.Add(lbl);
            //Final label
            final = new Label();
            final.Width = 300;
            final.Location = new Point(250, 400);
            final.Hide();
            this.Controls.Add(final);
            //plrP label
            plrT = new TextBox();
            plrT.Width = 100;
            plrT.Height = 20;
            plrT.Text = "Mängija 1";
            plrT.Location = new Point(10, 85);
            this.Controls.Add(plrT);
            //lock pl2
            lockpl1 = new Button();
            lockpl1.Location = new Point(50, 115);
            lockpl1.Width = 40;
            lockpl1.Text = "Vali";
			lockpl1.Click += Lockpl1_Click;
            this.Controls.Add(lockpl1);
            //plrP label
            plr2T = new TextBox();
            plr2T.Width = 100;
            plr2T.Height = 20;
            plr2T.Text = "Mängija 2";
            plr2T.Location = new Point(10, 185);
            this.Controls.Add(plr2T);
            //lock pl2
            lockpl2 = new Button();
            lockpl2.Location = new Point(50, 215);
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
            pvppvpe.Location = new Point(350, 300);
            pvppvpe.Text = "PVP?";
            pvppvpe.Checked = true;
            pvppvpe.CheckedChanged += Pvppvpe_CheckedChanged;
            this.Controls.Add(pvppvpe);
            //score feed
            feed = new TextBox();
            feed.ReadOnly = true;
            feed.Location = new Point(550, 25);
            feed.Width = 200;
            feed.Height = 300;
            feed.Multiline = true;
            this.Controls.Add(feed);
            //last10 label
            scores = new Label();
            scores.Location = new Point(550, 330);
            scores.Text = "Kuva viimased 10 tulemust";
            scores.Width = 200;
            this.Controls.Add(scores);
            //last 10 scores
            scoreB = new Button();
            scoreB.Location = new Point(600, 355);
            scoreB.Text = "Kuva";
            this.Controls.Add(scoreB);
            scoreB.Click += ScoreB_Click;
            //pic pl1
            pic = new PictureBox();
            pic.Location = new Point(150, 450);
            pic.Width = 100;
            pic.Height = 100;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(pic);
            //pic pl2
            pic2 = new PictureBox();
            pic2.Location = new Point(350, 450);
            pic2.Width = 100;
            pic2.Height = 100;
            pic2.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(pic2);
            //pic slave
            picSlave = new Label();
            picSlave.Location = new Point(250, 450);
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
            choicePlr1.Location = new Point(150, 115);
            choicePlr1.Width = 150;
            choicePlr1.TextAlign = ContentAlignment.MiddleCenter;
            choicePlr1.BackColor = Color.Black;
            choicePlr1.Font = new Font("Arial", 16);
            choicePlr1.Hide();
            this.Controls.Add(choicePlr1);
            //lbl1 button to show
            showpl1 = new Button();
            showpl1.Location = new Point(330, 115);
            showpl1.Text = "Näidata";
            showpl1.Hide();
            showpl1.Click += delegate { choicePlr1.BackColor = Color.White; };
            this.Controls.Add(showpl1);
            //label plr2
            choicePlr2 = new Label();
            choicePlr2.Location = new Point(150, 215);
            choicePlr2.Width = 150;
            choicePlr2.TextAlign = ContentAlignment.MiddleCenter;
            choicePlr2.BackColor = Color.Black;
            choicePlr2.Font = new Font("Arial", 16);
            choicePlr2.Hide();
            this.Controls.Add(choicePlr2);
            //lbl2 button to show
            showpl2 = new Button();
            showpl2.Location = new Point(330, 215);
            showpl2.Text = "Näidata";
            showpl2.Hide();
            showpl2.Click += delegate { choicePlr2.BackColor = Color.White; };
            this.Controls.Add(showpl2);
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
                feed.Text = "";
                string[] lines = File.ReadAllLines(@"../../score.txt");
                for (int i = lines.Length - 11; i < lines.Length; i++)
                {
                    feed.Text += lines[i] + Environment.NewLine;
                }
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
            string p2 = plr2T.Text;
            string p = plrT.Text;
            switch (Play())
            {
                case 0:
                    final.Text = "Viik!";
                    this.BackColor = Color.White;
                    picSlave.Text = "=";
                    break;
                case 1:
                    final.Text = $"{p} võitis!";
                    this.BackColor = Color.Green;
                    choicePlr1.BackColor = Color.Green;
                    choicePlr2.BackColor = Color.Green;
                    picSlave.Text = ">";
                    break;
                case 2:
                    final.Text = $"{p2} võitis!";
                    this.BackColor = Color.Red;
                    choicePlr1.BackColor = Color.Red;
                    choicePlr2.BackColor = Color.Red;
                    picSlave.Text = "<";
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
                plr2T.Text = "Mängija 2";
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
    }
}
