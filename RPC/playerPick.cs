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
    public partial class playerPick : Form
    {
        Form1 _parent;
        private bool _plr;
        private GroupBox plrP;
        private RadioButton rad4, rad5, rad6;
        private Button btn;
        public playerPick(Form1 parent, string _plrName, bool plr)
        {
            //InitializeComponent();
            this.Width = 500;
            this.Height = 200;
            this.Text = _plrName;
            _plr = plr;
            _parent = parent;
            plrP = new GroupBox();
            plrP.Location = new Point(0, 0);
            plrP.Width = 500;
            plrP.Height = 50;
            rad4 = new RadioButton
            {
                Location = new Point(50, 15),
                Text = "Kivi",
                Name = "rock"
            };
            rad5 = new RadioButton
            {
                Location = new Point(200, 15),
                Text = "Käärid",
                Name = "scissors"
            };
            rad6 = new RadioButton
            {
                Location = new Point(350, 15),
                Text = "Paber",
                Name = "paper"
            };
            plrP.Controls.Add(rad4);
            plrP.Controls.Add(rad5);
            plrP.Controls.Add(rad6);
            btn = new Button();
            btn.Location = new Point(200, 100);
            btn.Text = "Vali";
            btn.Click += WriteChoice;
            this.Controls.Add(btn);
            this.Controls.Add(plrP);
        }

        void WriteChoice(object sender, EventArgs e)
        {
            var rad = plrP.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            if (_plr)
            {
                _parent.plr1Hand = rad.Name;
                _parent.SetHiddenLabel(true);
            }
            else 
            {
                _parent.plr2Hand = rad.Name;
                _parent.SetHiddenLabel(false);
            }
            
            Console.WriteLine(rad.Name);
            
            this.Close();
        }
    }
}
