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
using DrawGame.logic;
using System.Threading;

namespace DrawGame.view
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Thread thread = new Thread(new ThreadStart(Thread));
            box.Velocity.Y = 1;
        }
        Box box = new Box(100, 0);
        Platform platform = new Platform();
        double gravity = 0.1;
        double friction = 1;

        System.Windows.Forms.Timer timer;
        System.Drawing.Graphics graphics;

        public void Thread()
        {
            
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
                box.MoveMe(new Vector2(-1, 0));
                return true; //for the active control to see the keypress, return false
            }
            if (keyData == Keys.Right || keyData == Keys.D)
            {
                box.MoveMe(new Vector2(1, 0));
                return true; //for the active control to see the keypress, return false
            }
            if (box.Fuel > 0)
            {
                if (keyData == Keys.Up || keyData == Keys.W)
                {
                    box.Jumping = true;
                    if (box.Bottom.Y > 179 && box.Bottom.Y < 210 && (box.Bottom.X > 80 && box.Bottom.X < 164))
                    {
                        // Move up a bit so we aren't stuck on the platform
                        box.MovePosition(new Vector2(0, -1));
                    }
                    box.Falling = false;
                    box.MoveMe(new Vector2(0, -2));
                    return true; //for the active control to see the keypress, return false
                }
                if (keyData == Keys.Down || keyData == Keys.S)
                {
                    box.MoveMe(new Vector2(0, 2));
                    box.Jumping = true;
                    return true; //for the active control to see the keypress, return false
                }
                if (keyData == Keys.Space)
                {
                    if (box.Jumping != true)
                    {
                        box.MovePosition(new Vector2(0, -1));

                        box.MoveMe(new Vector2(0, -3));
                        box.Jumping = true;
                        box.Falling = false;
                    }
                    return true; //for the active control to see the keypress, return false
                }
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (timer == null)
            {
                timer = new System.Windows.Forms.Timer();
                timer.Tick += new System.EventHandler(OnTimerEvent);
                timer.Interval = 10;
                timer.Enabled = true;

                graphics = this.CreateGraphics();
            }
        }

        private void OnTimerEvent(object sender, EventArgs e)
        {
            // Are we testing or playing?
            if (checkBox1.Checked)
            {
                // Playing
                box.GameOn = true;
            }
            else
            {
                // Testing
                box.GameOn = false;
            }

            try
            {
                label1.Text = "Fuel: " + (box.Fuel);
                // Set box.Fuel level
                lblX.Text = "X Velocity:" + box.Velocity.X;
                lblY.Text = "Y Velocity:" + box.Velocity.Y;
            }
            catch (Exception)
            {
                label1.Text = "Fuel: 0";
            }

            // Main Logic
            try
            {
                double speed = (box.Velocity.Y);
                double sideways = (box.Velocity.X);

                // Redraw Box
                graphics.Clear(Control.DefaultBackColor);
                graphics.DrawEllipse(System.Drawing.Pens.Black, (int)box.X, (int)box.Y, box.Width, box.Height);
                graphics.DrawRectangle(System.Drawing.Pens.Red, (int)box.X, (int)box.Y, box.Width, box.Height);
                
                box.Velocity.Y = platform.CollisionCheck(box, box.Velocity.Y);
                box.Velocity.X = platform.CollisionCheckX(box, box.Velocity.X);
                //Collision();


                box.TerminalVelocityCheck();

                box.Movement(platform, gravity, box, friction);

                #region Unused Player Movement
                //// Check if we are moving downwards
                //if (box.Velocity.Y >= 0 && platform.CollisionTest(box) == false)
                //{
                //    box.Falling = true;
                //    // Add veloicty to position
                //    box.MovePosition(new Vector2(0, speed));

                //    box.Velocity.Y += gravity - (gravity / 2);
                //}
                //// Check if we are moving upwards
                //else if (box.Velocity.Y <= 0 && platform.CollisionTest(box) == false)
                //{
                //    // Add veloicty to position
                //    box.MovePosition(new Vector2(0, speed));

                //    box.Velocity.Y += gravity * 2 - (gravity / 2);
                //}
                //// Check if we are moving right
                //if (box.Velocity.X > 0.01)
                //{
                //    // Add veloicty to position
                //    box.MovePosition(new Vector2(sideways, 0));

                //    if (box.Velocity.X > 0.01 && box.Falling == true)
                //    {
                //        box.Velocity.X -= friction / 800;
                //    }
                //    else if (box.Falling == false)
                //    {
                //        box.Velocity.X -= friction / 40;
                //    }

                //}
                //// Check if we are moving left
                //else if (box.Velocity.X < -0.01)
                //{
                //    // Add veloicty to position
                //    box.MovePosition(new Vector2(sideways, 0));

                //    // Friction
                //    if (box.Velocity.X < -0.01 && box.Falling == true)
                //    {
                //        box.Velocity.X += friction / 800;
                //    }
                //    else if (box.Falling == false)
                //    {
                //        box.Velocity.X += friction / 40;
                //    }
                //}
                //else
                //{
                //    //box.Velocity.X = 0;
                //}
                #endregion

                lblTop.Text = "Top Position:    " + (int)box.Top.X + ", " + (int)box.Top.Y;
                lblBottom.Text = "Bottom Position: " + (int)box.Bottom.X + ", " + (int)box.Bottom.Y;
                lblLeft.Text = "Left Position:   " + (int)box.Left.X + ", " + (int)box.Left.Y;
                lblRight.Text = "Right Position:  " + (int)box.Right.X + ", " + (int)box.Right.Y;
            }
            catch (Exception)
            {

                MessageBox.Show("Error", "Error");
            }
        }


        #region Unused Collision Ceck
        //private double CollisionCheck(double velo)
        //{
        //    if (box.Bottom.Y > 280)
        //    {
        //        if (msg != true && checkBox1.Checked)
        //        {
        //            msg = true;
        //            timer.Enabled = false;
        //            MessageBox.Show("Crashed. Too far down!", "Error");
        //        }
        //    }

        //    if (box.Bottom.Y >= 180 && box.Bottom.Y < 210 && (box.Bottom.X > 80 && box.Bottom.X < 164) && (velo > -0.1 && box.Velocity.Y < 0.1) && box.Falling == false)
        //    {
        //        velo = 0;
        //    }

        //    if (box.Bottom.Y >= 180 && box.Bottom.Y < 210 && (box.Bottom.X > 80 && box.Bottom.X < 164) && box.Falling == true)
        //    {
        //        if (velo > 1 && msg != true && checkBox1.Checked)
        //        {
        //            msg = true;
        //            timer.Enabled = false;
        //            MessageBox.Show("Fail. Too fast!", "Error");
        //        }

        //        box.Jumping = false;
        //        //int offset = (int)(box.Bottom.Y - 180);
        //        // Draw slightly above "flat" if we actually go through the ground
        //        //graphics.Clear(Control.DefaultBackColor);
        //        //graphics.DrawEllipse(System.Drawing.Pens.Black, (int)box.X, (int)box.Y - offset, box.Width, box.Height);
        //        //graphics.DrawRectangle(System.Drawing.Pens.Red, (int)box.X, (int)box.Y - offset, box.Width, box.Height);

        //        // Move or set box Y position
        //        box.Y = 170;
        //        box.Left.Y = 175;
        //        box.Right.Y = 175;
        //        box.Top.Y = 170;
        //        box.Bottom.Y = 180;



                
        //        if (box.Velocity.Y >= 0)
        //        {
        //            velo = velo - (velo * 2) * box.Bounce;
        //            box.Falling = true;
        //        }
        //        else if (box.Velocity.Y < 0)
        //        {
        //            velo = velo - (velo * 2) * box.Bounce;
        //            box.Falling = true;
        //        }
        //        if (box.Velocity.Y > -0.01)
        //        {
        //            box.Falling = false;
        //        }
                
        //    }

        //    //if (box.Top.Y > 180 && box.Top.Y < 210 && (box.Top.X > 80 && box.Top.X < 164))
        //    //{
        //    //    if (velo < 0.1)
        //    //    {
        //    //        velo = ( (velo * -1) * box.Bounce);
        //    //    }
        //    //}

        //    //if (box.Left.Y > 179 && box.Left.Y < 211 && (box.Left.X > 80 && box.Left.X < 164))
        //    //{
        //    //    if (box.Velocity.X > 0)
        //    //    {
        //    //        box.Velocity.X = box.Velocity.X - (box.Velocity.X * 2) * box.Bounce;
        //    //    }
        //    //}
        //    //else if (box.Right.Y > 169 && box.Right.Y < 201 && (box.Right.X > 80 && box.Right.X < 164))
        //    //{
        //    //    if (box.Velocity.X < 0)
        //    //    {
        //    //        box.Velocity.X = ((box.Velocity.X * -1) * box.Bounce);
        //    //    }
        //    //}
        //    return velo;
        //}
        #endregion


        private void button2_Click(object sender, EventArgs e)
        {
            Restart();
        }

        private void Restart()
        {
            timer.Enabled = true;
            box.X = 100;
            box.Y = 0;
            box.Velocity.Y = 1;
            box.Velocity.X = 0;
            box.Left = new Vector2(0, 104);
            box.Right = new Vector2(10, 105);
            box.Top = new Vector2(104, 0);
            box.Bottom = new Vector2(105, 10);
            box.Jumping = true;
            box.Fuel = 50;
        }

        private void Collision()
        {
            if (box.Bottom.Y >= 270 && (box.Bottom.X > 0 || box.Bottom.X < 400) && box.Falling == true)
            {
                box.Jumping = false;

                if (box.Velocity.Y >= 0)
                {
                    box.Velocity.Y = box.Velocity.Y - (box.Velocity.Y * 2) * box.Bounce;
                    box.Falling = true;
                }
                else if (box.Velocity.Y < 0)
                {
                    box.Velocity.Y = box.Velocity.Y - (box.Velocity.Y * 2) * box.Bounce;
                    box.Falling = true;
                }
                if (box.Velocity.Y > -0.2)
                {
                    box.Falling = false;
                }
                if (box.Velocity.Y > -0.2)
                {
                    box.Falling = false;
                }
            }
            if (box.Bottom.Y >= 270 && (box.Bottom.X > 0 || box.Bottom.X < 400) && box.Falling == false)
            {
                box.Velocity.Y = 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (gravity == 0.01)
                {
                    gravity = 0.1;
                    friction = 1;
                }
                else
                {
                    gravity = 0.01;
                    friction = 0.5;
                }
                textBox1.Text = "" + gravity;
            }
            catch (Exception)
            {
                MessageBox.Show("Not a number!", "Error");
            }
        }
    }
}
