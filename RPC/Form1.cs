using System;
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
        string plr, plr2;
        Label lbl, final, scores, picSlave;
        Random rnd;
        RadioButton rad, rad2, rad3, rad4, rad5, rad6;
        Button btn, scoreB;
        GroupBox plrP, plr2P;
        CheckBox pvppvpe;
        TextBox plrT, plr2T, feed;
        PictureBox pic, pic2;
        public Form1()
        {
            this.Height = 600;
            this.Width = 800;
            this.Text = "Камень, Ножницы, Бумага";
            this.BackColor = Color.White;
            //Greeting label
            lbl = new Label();
            lbl.Width = 300;
            lbl.Height = 50;
            lbl.Text = "Добро пожаловать в Камень, Ножницы, Бумага!\nВыберите руку и нажмите кнопку 'Играть'!";
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
            plrT.Text = "Игрок 1";
            plrT.Location = new Point(10, 85);
            this.Controls.Add(plrT);
            //Radio buttons
            plrP = new GroupBox();
            plrP.Location = new Point(0, 100);
            plrP.Width = 500;
            plrP.Height = 50;
            rad = new RadioButton
            {
                Location = new Point(50, 15),
                Text = "Камень",
                Name = "rock"
            };
            rad2 = new RadioButton
            {
                Location = new Point(200, 15),
                Text = "Ножницы",
                Name = "scissors"
            };
            rad3 = new RadioButton
            {
                Location = new Point(350, 15),
                Text = "Бумага",
                Name = "paper"
            };
            plrP.Controls.Add(rad);
            plrP.Controls.Add(rad2);
            plrP.Controls.Add(rad3);
            this.Controls.Add(plrP);
            //plrP label
            plr2T = new TextBox();
            plr2T.Width = 100;
            plr2T.Height = 20;
            plr2T.Text = "Игрок 2";
            plr2T.Location = new Point(10, 185);
            this.Controls.Add(plr2T);
            //Radio buttons
            plr2P = new GroupBox();
            plr2P.Location = new Point(0, 200);
            plr2P.Width = 500;
            plr2P.Height = 50;
            rad4 = new RadioButton
            {
                Location = new Point(50, 15),
                Text = "Камень",
                Name = "rock"
            };
            rad5 = new RadioButton
            {
                Location = new Point(200, 15),
                Text = "Ножницы",
                Name = "scissors"
            };
            rad6 = new RadioButton
            {
                Location = new Point(350, 15),
                Text = "Бумага",
                Name = "paper"
            };
            plr2P.Controls.Add(rad4);
            plr2P.Controls.Add(rad5);
            plr2P.Controls.Add(rad6);
            this.Controls.Add(plr2P);
            //play button
            btn = new Button
            {
                Text = "Играть",
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
            scores.Text = "Показать последние 10 результатов";
            scores.Width = 200;
            this.Controls.Add(scores);
            //last 10 scores
            scoreB = new Button();
            scoreB.Location = new Point(600, 355);
            scoreB.Text = "Показать";
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
                plr2P.Show();
                plr2T.Show();
            }
            else
            {
                plr2T.Hide();
                plr2P.Hide();
            }
        }

        private void cycle(object sender, EventArgs e)
        {
            string p2 = plr2T.Text;
            string p = plrT.Text;
            switch (Play())
            {
                case 0:
                    final.Text = "Ничья!";
                    this.BackColor = Color.White;
                    picSlave.Text = "=";
                    break;
                case 1:
                    final.Text = $"{p} победил!";
                    this.BackColor = Color.Green;
                    picSlave.Text = ">";
                    break;
                case 2:
                    final.Text = $"{p2} победил!";
                    this.BackColor = Color.Red;
                    picSlave.Text = "<";
                    break;
                default:
                    final.Text = "Выберите чем будете играть.";
                    break;
            }
            File.AppendAllText(@"../../score.txt", final.Text + Environment.NewLine);
            final.Show();
        }
        //0 = draw, 1 = win, 2 = lose
        private int Play()
        {
            var plrRad = plrP.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            if (plrRad == null) return 5;
            plr = plrRad.Name.ToString();
            if (pvppvpe.Checked)
            {
                var plr2Rad = plr2P.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
                if (plr2Rad == null) return 5;
                plr2 = plr2Rad.Name.ToString();
            }
            else
            {
                plr2 = randomPiece();
                plr2T.Text = "Игрок 2";
            }
            setImg(plr, plr2);
            return winCheck(plr, plr2);
        }
        private void setImg(string plr, string plr2)
        {
            Random rnd = new Random();
            switch (plr)
            {
                case "scissors":
                    pic2.Image = Image.FromFile($"../../pics/scissors{rnd.Next(1, 3)}.jpg");
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
