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
using SpaceShipTest.service;
using System.Threading;

namespace SpaceShipTest.view
{
    public partial class GameForm : Form
    {
        GameService game;
        //System.Windows.Forms.Timer timer;
        Thread thread;
        System.Drawing.Graphics graphics;
        private bool running = false;

        public GameForm()
        {
            InitializeComponent();
        }

        //private void btnStart_Click(object sender, EventArgs e)
        //{
        //    if (timer == null)
        //    {
        //        timer = new System.Windows.Forms.Timer();
        //        timer.Tick += new System.EventHandler(OnTimerEvent);
        //        timer.Interval = 10;
        //        timer.Enabled = true;
        //        graphics = this.CreateGraphics();
        //        game = new GameService();
        //    }
        //}

        //private void OnTimerEvent(object sender, EventArgs e)
        //{
        //    // Draw ship
        //    graphics.Clear(Control.DefaultBackColor);
        //    graphics.DrawEllipse(System.Drawing.Pens.Black, (int)game.ship.Position.X, (int)game.ship.Position.Y, game.ship.Width, game.ship.Height);
        //    graphics.DrawRectangle(System.Drawing.Pens.Red, (int)game.ship.Position.X, (int)game.ship.Position.Y, game.ship.Width, game.ship.Height);

        //    // Loop through list and draw Platforms
        //    foreach (Platform platform in game.list.ObjectList)
        //    {
        //        graphics.DrawRectangle(System.Drawing.Pens.Blue, (int)platform.TopLeft.X, (int)platform.TopLeft.Y, platform.Width, platform.Height);
        //    }
        //    game.Start();
        //}

        private void btnStart_Click(object sender, EventArgs e)
        {
            graphics = this.CreateGraphics();
            game = new GameService();
            thread = new Thread(new ThreadStart(GameLoop));
            thread.Start();
            running = true;
        }

        // Main loop
        private void GameLoop()
        {
            try
            {
                while (running)
                {
                    Thread.Sleep(13);
                    // Clear Form
                    graphics.Clear(Control.DefaultBackColor);

                    // Loop through list and draw Platforms
                    foreach (Platform platform in game.List)
                    {
                        graphics.DrawRectangle(System.Drawing.Pens.Blue, (int)platform.Left[0].X, (int)platform.Left[0].Y, platform.Width, platform.Height);
                    }
                    // Draw ship
                    graphics.DrawEllipse(System.Drawing.Pens.Black, (int)game.Ship.Position.X, (int)game.Ship.Position.Y, game.Ship.Width, game.Ship.Height);
                    graphics.DrawRectangle(System.Drawing.Pens.Red, (int)game.Ship.Position.X, (int)game.Ship.Position.Y, game.Ship.Width, game.Ship.Height);

                    game.Start();
                }
            }
            catch (Exception)
            {

            }
        }

        // Keypresses for moving
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (game != null)
            {
                if (keyData == Keys.Left || keyData == Keys.A)
                {
                    game.Ship.AddVelocity(new Vector2(-1, 0));
                    return true; //for the active control to see the keypress, return false
                }
                if (keyData == Keys.Right || keyData == Keys.D)
                {
                    game.Ship.AddVelocity(new Vector2(1, 0));
                    return true; //for the active control to see the keypress, return false
                }
                if (keyData == Keys.Up || keyData == Keys.W)
                {
                    if (game.Ship.IsGrounded == true)
                    {
                        game.Ship.MovePosition(new Vector2(0, -2));
                    }
                    game.Ship.AddVelocity(new Vector2(0, -2));
                    return true; //for the active control to see the keypress, return false
                }
                if (keyData == Keys.Down || keyData == Keys.S)
                {
                    game.Ship.AddVelocity(new Vector2(0, 1));
                    return true; //for the active control to see the keypress, return false
                }
                if (keyData == Keys.Space)
                {
                    if (game.Ship.IsGrounded == true)
                    {
                        game.Ship.MovePosition(new Vector2(0, -2));
                    }
                    game.Ship.AddVelocity(new Vector2(0, -3));
                    return true; //for the active control to see the keypress, return false
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        // On Form Close
        private void GameForm_FormClosing(Object sender, FormClosingEventArgs e)
        {
            running = false;
            if (thread != null)
                thread.Abort();
            Application.Exit();
        }
    }
}
