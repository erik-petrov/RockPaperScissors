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
        int plrW, plr2W;
        Label lbl, final, plrL, plr2L, score;
        Random rnd;
        RadioButton rad, rad2, rad3, rad4, rad5, rad6;
        Button btn;
        GroupBox plrP, plr2P;
        public Form1()
        {
            this.Height = 500;
            this.Width = 500;
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
            //scoreboard
            score = new Label();
            score.Width = 100;
            score.Location = new Point(225, 75);
            using (FileStream scoreboard = File.OpenRead(@"../../score.txt"))
            {
                byte[] b = new byte[256];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (scoreboard.Read(b, 0, b.Length) > 0)
                {
                    score.Text += temp.GetString(b);
                }
            }
            string[] scores = score.Text.Split(':');
            plrW = Convert.ToInt32(Convert.ToString(scores[0]));
            plr2W = Convert.ToInt32(scores[1]);
            this.Controls.Add(score);
            //plrP label
            plrL = new Label();
            plrL.Width = 100;
            plrL.Height = 20;
            plrL.Text = "Игрок 1";
            plrL.Location = new Point(10, 85);
            this.Controls.Add(plrL);
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
            plr2L = new Label();
            plr2L.Width = 100;
            plr2L.Height = 20;
            plr2L.Text = "Игрок 2";
            plr2L.Location = new Point(10, 185);
            this.Controls.Add(plr2L);
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
            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.IO.File.WriteAllText(@"../../score.txt", score.Text);
        }

        private void cycle(object sender, EventArgs e)
        {
            switch (Play())
            {
                case 0:
                    final.Text = "Ничья!";
                    this.BackColor = Color.White;
                    break;
                case 1:
                    final.Text = "Вы победили!";
                    this.BackColor = Color.Green;
                    plrW++;
                    break;
                case 2:
                    final.Text = "Вы проиграли!";
                    this.BackColor = Color.Red;
                    plr2W++;
                    break;
                default:
                    final.Text = "error";
                    break;
            }
            score.Text = $"{plrW}:{plr2W}";
            final.Show();
        }
        //0 = draw, 1 = win, 2 = lose
        private int Play()
        {
            var plrRad = plrP.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            plr = plrRad.Name.ToString();
            var plr2Rad = plr2P.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            plr2 = plr2Rad.Name.ToString();
            return winCheck(plr, plr2);
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
