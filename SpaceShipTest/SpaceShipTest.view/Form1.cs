using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//..
using SpaceShipTest.logic;

namespace SpaceShipTest.view
{
    public partial class Form1 : Form
    {
        GameService game;
        System.Windows.Forms.Timer timer;
        System.Drawing.Graphics graphics;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (timer == null)
            {
                timer = new System.Windows.Forms.Timer();
                timer.Tick += new System.EventHandler(OnTimerEvent);
                timer.Interval = 10;
                timer.Enabled = true;
                graphics = this.CreateGraphics();
                game = new GameService();
            }
        }

        private void OnTimerEvent(object sender, EventArgs e)
        {
            // Draw ship
            graphics.Clear(Control.DefaultBackColor);
            graphics.DrawEllipse(System.Drawing.Pens.Black, (int)game.ship.Position.X, (int)game.ship.Position.Y, game.ship.Width, game.ship.Height);
            graphics.DrawRectangle(System.Drawing.Pens.Red, (int)game.ship.Position.X, (int)game.ship.Position.Y, game.ship.Width, game.ship.Height);

            // Loop through list and draw Platforms
            foreach (Platform platform in game.list.ObjectList)
            {
                graphics.DrawRectangle(System.Drawing.Pens.Blue, (int)platform.TopLeft.X, (int)platform.TopLeft.Y, platform.Width, platform.Height);
            }
            game.Start();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left || keyData == Keys.A)
            {
                game.ship.AddVelocity(new Vector2(-1,0));
                return true; //for the active control to see the keypress, return false
            }
            if (keyData == Keys.Right || keyData == Keys.D)
            {
                game.ship.AddVelocity(new Vector2(1, 0));
                return true; //for the active control to see the keypress, return false
            }
            if (keyData == Keys.Up || keyData == Keys.W)
            {
                game.ship.MovePosition(new Vector2(0, -2));
                game.ship.AddVelocity(new Vector2(0, -2));
                return true; //for the active control to see the keypress, return false
            }
            if (keyData == Keys.Down || keyData == Keys.S)
            {
                game.ship.AddVelocity(new Vector2(0, 1));
                return true; //for the active control to see the keypress, return false
            }
            if (keyData == Keys.Space)
            {
                game.ship.MovePosition(new Vector2(0, -2));
                game.ship.AddVelocity(new Vector2(0, -3));
                return true; //for the active control to see the keypress, return false
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
    }
}
