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
    public partial class Form3 : Form
    {
        Form1 _parent;
        public Form3(Form1 parent, ListView leaderboard)
        {
            _parent = parent;
            this.Width = 200;
            this.Height = 300;
            this.Controls.Add(leaderboard);
            this.FormClosing += Form3_FormClosing;
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            _parent.clearForm3(this);
        }

        public Form3(Form1 parent, TextBox feed)
        {
            _parent = parent;
            this.Width = 200;
            this.Height = 300;
            this.Controls.Add(feed);
            this.FormClosing += Form3_FormClosing;
        }
    }
}
