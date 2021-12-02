using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPC
{
    public partial class Form1 : Form
    {
        string plr, plr2;
        Label lbl, final;
        Random rnd;
        RadioButton rad, rad2, rad3;
        Button btn;
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
            //Radio buttons
            rad = new RadioButton
            {
                Location = new Point(50, 100),
                Text = "Камень",
                Name = "rock"
            };
            rad2 = new RadioButton
            {
                Location = new Point(200, 100),
                Text = "Ножницы",
                Name = "scissors"
            };
            rad3 = new RadioButton
            {
                Location = new Point(350, 100),
                Text = "Бумага",
                Name = "paper"
            };
            this.Controls.Add(rad);
            this.Controls.Add(rad2);
            this.Controls.Add(rad3);
            //play button
            btn = new Button
            {
                Text = "Играть",
                Location = new Point(250, 200),
                Height = 30,
                Width = 100
            };
            btn.Click += cycle;
            this.Controls.Add(btn);
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
                    break;
                case 2:
                    final.Text = "Вы проиграли!";
                    this.BackColor = Color.Red;
                    break;
                default:
                    final.Text = "error";
                    break;
            }
            final.Show();
        }
        //0 = draw, 1 = win, 2 = lose
        private int Play()
        {
            var checkedButton = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            plr = checkedButton.Name.ToString();
            plr2 = randomPiece();
            Console.WriteLine(plr);
            Console.WriteLine(plr2);
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
