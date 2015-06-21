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

namespace DrawGame.view
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Box box = new Box(50, 0);
        double gravity = 0.1;
        double friction = 1;
        bool msg = false;
        
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        System.Drawing.Graphics graphics;

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
                    if (checkBox1.Checked)
                    {
                        box.Fuel -= 1;
                    }
                    box.Jumping = true;
                    if (box.Bottom.Y > 179 && box.Bottom.Y < 210 && (box.Bottom.X > 80 && box.Bottom.X < 164))
                    {
                        box.Bottom.Y = box.Bottom.Y - 1;
                    }
                    box.MoveMe(new Vector2(0, -2));
                    return true; //for the active control to see the keypress, return false
                }
                if (keyData == Keys.Down || keyData == Keys.S)
                {
                    if (checkBox1.Checked)
                    {
                        box.Fuel -= 1;
                    }
                    //box.Bottom.Y = box.Bottom.Y - 1;
                    box.MoveMe(new Vector2(0, 2));
                    return true; //for the active control to see the keypress, return false
                }
                if (keyData == Keys.Space)
                {
                    if (box.Jumping != true)
                    {
                        box.Bottom.Y = box.Bottom.Y - 1;
                        box.MoveMe(new Vector2(0, -3));
                        box.Jumping = true;
                    }
                    return true; //for the active control to see the keypress, return false
                }
                return true;
            }
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer.Tick += new System.EventHandler(OnTimerEvent);
            timer.Interval = 10;
            timer.Enabled = true;

            graphics = this.CreateGraphics();
        }

        private void OnTimerEvent(object sender, EventArgs e)
        {
            // Set box.Fuel level
            try
            {
                label1.Text = "Fuel: " + (box.Fuel);
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
                
                box.Velocity.Y = CollisionCheck(box.Velocity.Y);
                // Falling check
                if (box.Velocity.Y < 0.15 && box.Velocity.Y > -0.15 && box.Falling == false)
                {
                    box.Velocity.Y = 0;
                    box.Falling = true;
                }

                box.MaxVelocityCheck();


                // Check if we are moving downwards
                if (box.Velocity.Y > 0)
                {
                    // Add veloicty to position
                    box.MovePosition(new Vector2(0, speed));

                    box.Velocity.Y += gravity - (gravity / 2);
                }
                // Check if we are moving upwards
                else if (box.Velocity.Y <= 0)
                {
                    // Add veloicty to position
                    box.MovePosition(new Vector2(0, speed));

                    box.Velocity.Y += gravity * 2 - (gravity / 2);
                }
                // Check if we are moving right
                if (box.Velocity.X > 0)
                {
                    // Add veloicty to position
                    box.MovePosition(new Vector2(sideways, 0));

                    if (box.Velocity.X > 0.1 && box.Falling == false)
                    {
                        box.Velocity.X -= friction / 20;
                    }
                    else if (box.Velocity.Y > 0.01)
                    {
                        box.Velocity.X -= friction / 200;
                    }
                    else
                    {
                        if (box.Velocity.X < 0.015 && box.Velocity.X > -0.015)
                        {
                            box.Velocity.X = 0;
                        }
                    }
                }
                // Check if we are moving left
                else if (box.Velocity.X < 0)
                {
                    // Add veloicty to position
                    box.MovePosition(new Vector2(sideways, 0));

                    // Friction
                    if (box.Velocity.X < -0.1 && box.Falling == false)
                    {
                        box.Velocity.X += friction / 20;
                    }
                    else if (box.Velocity.Y > 0.01)
                    {
                        box.Velocity.X += friction / 200;
                    }
                    else
                    {
                        if (box.Velocity.X < 0.015 && box.Velocity.X > -0.015)
                        {
                            box.Velocity.X = 0;
                        }
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error", "Error");
            }
        }

        private double CollisionCheck(double velo)
        {
            if (box.Bottom.Y > 280)
            {
                if (msg != true && checkBox1.Checked)
                {
                    msg = true;
                    timer.Enabled = false;
                    MessageBox.Show("Crashed. Too far down!", "Error");
                }
            }
            
            if (box.Bottom.Y > 180 && box.Bottom.Y < 210 && (box.Bottom.X > 80 && box.Bottom.X < 164) && box.Falling == true)
            {
                if (velo > 1 && msg != true && checkBox1.Checked)
                {
                    msg = true;
                    timer.Enabled = false;
                    MessageBox.Show("Fail. Too fast!", "Error");
                }
                
                box.Jumping = false;
                int offset = (int)(box.Bottom.Y - 181);
                // Draw slightly above "flat" if we actually go through the ground
                graphics.Clear(Control.DefaultBackColor);
                graphics.DrawEllipse(System.Drawing.Pens.Black, (int)box.X, (int)box.Y - offset, box.Width, box.Height);
                graphics.DrawRectangle(System.Drawing.Pens.Red, (int)box.X, (int)box.Y - offset, box.Width, box.Height);
                box.Falling = false;

                // Move or set box Y position
                box.Y = 170;
                box.Left.Y = 175;
                box.Right.Y = 175;
                box.Top.Y = 170;
                box.Bottom.Y = 180;
                if (velo > 0)
                {
                    velo = velo - (velo * 2);
                }
                else if (velo < 0)
                {
                    velo = (velo + (velo * -2) * 0.75);
                }
                if (box.Bottom.Y >= 180)
                {
                    box.Falling = false;
                }
            }

            if (box.Top.Y > 180 && box.Top.Y < 210 && (box.Top.X > 80 && box.Top.X < 164))
            {
                // Move or set box Y position
                box.Y = 210;
                box.Left.Y = 215;
                box.Right.Y = 215;
                box.Top.Y = 210;
                box.Bottom.Y = 220;
            }
            return velo;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Restart();
        }

        private void Restart()
        {
            timer.Enabled = true;
            box.X = 100;
            box.Y = 0;
            box.Velocity.Y = 0;
            box.Velocity.X = 0;
            box.Left = new Vector2(0, 104);
            box.Right = new Vector2(10, 105);
            box.Top = new Vector2(104, 0);
            box.Bottom = new Vector2(105, 10);
            msg = false;
            box.Jumping = true;
            box.Fuel = 50;
        }
    }
}
