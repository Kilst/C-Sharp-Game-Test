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
using SpaceShip.logic;

namespace SpaceShip.view
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Ship ship = new Ship(100, 0);
        Platform platform1 = new Platform();
        Platform platform2 = new Platform(new Vector2(180, 210));
        double gravity = 0.1;
        double friction = 1;

        PlatformsList platformList = new PlatformsList();

        System.Windows.Forms.Timer timer;
        System.Drawing.Graphics graphics;

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (timer == null)
            {
                timer = new System.Windows.Forms.Timer();
                timer.Tick += new System.EventHandler(OnTimerEvent);
                timer.Interval = 10;
                timer.Enabled = true;

                graphics = this.CreateGraphics();
                platformList.Platforms.Add(platform1);
                platformList.Platforms.Add(platform2);
            }
        }

        private void OnTimerEvent(object sender, EventArgs e)
        {
            // Redraw ship
            graphics.Clear(Control.DefaultBackColor);
            graphics.DrawEllipse(System.Drawing.Pens.Black, (int)ship.X, (int)ship.Y, ship.Width, ship.Height);
            graphics.DrawRectangle(System.Drawing.Pens.Red, (int)ship.X, (int)ship.Y, ship.Width, ship.Height);

            foreach (Platform platform in platformList.Platforms)
            {
                graphics.DrawRectangle(System.Drawing.Pens.Blue, (int)platform.TopLeft.X, (int)platform.TopLeft.Y, platform.Width, platform.Height);
            }
            ship.CollisionCheck(platformList);
            ship.CollisionCheckSides(platformList);
            ship.GravityCheck(gravity);
            ship.TerminalVelocityCheck();
            ship.MovePosition(ship.Velocity);
            ship.FrictionCheck();

            lblTop.Text = "Top Position:    " + (int)ship.Top.X + ", " + (int)ship.Top.Y;
            lblBottom.Text = "Bottom Position: " + (int)ship.Bottom.X + ", " + (int)ship.Bottom.Y;
            lblLeft.Text = "Left Position:   " + (int)ship.Left.X + ", " + (int)ship.Left.Y;
            lblRight.Text = "Right Position:  " + (int)ship.Right.X + ", " + (int)ship.Right.Y;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Key down event
            //e.KeyCode
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left || keyData == Keys.A)
            {
                ship.MoveMe(new Vector2(-1, 0));
                return true; //for the active control to see the keypress, return false
            }
            if (keyData == Keys.Right || keyData == Keys.D)
            {
                ship.MoveMe(new Vector2(1, 0));
                return true; //for the active control to see the keypress, return false
            }
            if (ship.Fuel > 0)
            {
                if (keyData == Keys.Up || keyData == Keys.W)
                {
                    if (ship.Grounded == true)
                    {
                        // Move up a bit so we aren't stuck on the platform
                        ship.MovePosition(new Vector2(0, -1));
                    }
                    ship.MoveMe(new Vector2(0, -2));
                    return true; //for the active control to see the keypress, return false
                }
                if (keyData == Keys.Down || keyData == Keys.S)
                {
                    ship.MoveMe(new Vector2(0, 2));
                    return true; //for the active control to see the keypress, return false
                }
                if (keyData == Keys.Space)
                {
                    //if (ship.Jumping != true)
                    //{
                        ship.MovePosition(new Vector2(0, -1));

                        ship.MoveMe(new Vector2(0, -3));
                        //ship.Jumping = true;
                    //}
                    return true; //for the active control to see the keypress, return false
                }
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
    }
}
