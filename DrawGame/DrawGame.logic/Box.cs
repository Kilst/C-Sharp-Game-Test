﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawGame.logic
{
    public class Box
    {
        public Vector2 Left { get; set; }
        public Vector2 Right { get; set; }
        public Vector2 Top { get; set; }
        public Vector2 Bottom { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public Vector2 Velocity { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Fuel { get; set; }
        public bool Falling { get; set; }
        public bool Jumping { get; set; }

        public Box()
        {
            Left = new Vector2(0, 104);
            Right = new Vector2(10, 105);
            Top = new Vector2(104, 0);
            Bottom = new Vector2(105, 10);
            X = 100;
            Y = 0;
            Velocity = new Vector2(0, 0);
            Width = 10;
            Height = 10;
            Fuel = 50;
            Falling = false;
            Jumping = true;
        }

        public Box(double x, double y)
        {
            Left = new Vector2(y, x+4);
            Right = new Vector2(y+10, x+5);
            Top = new Vector2(x+4, y);
            Bottom = new Vector2(x+5, y+10);
            X = x;
            Y = y;
            Velocity = new Vector2(0, 0);
            Width = 10;
            Height = 10;
            Fuel = 50;
            Falling = false;
            Jumping = true;
        }

        public void MoveMe(Vector2 velocity)
        {
            this.Velocity.X += velocity.X;
            this.Velocity.Y += velocity.Y;
            //MoveEntire(velocity);
        }

        public void MovePosition(Vector2 velocity)
        {
            // Move X
            this.X += velocity.X;
            this.Left.X += velocity.X;
            this.Right.X += velocity.X;
            this.Top.X += velocity.X;
            this.Bottom.X += velocity.X;
            // Move Y
            this.Y += velocity.Y;
            this.Left.Y += velocity.Y;
            this.Right.Y += velocity.Y;
            this.Top.Y += velocity.Y;
            this.Bottom.Y += velocity.Y;
        }

        public void MaxVelocityCheck()
        {
            // Limit X velocity
            if (this.Velocity.X < -1)
            {
                this.Velocity.X = -1;
            }
            else if (this.Velocity.X > 1)
            {
                this.Velocity.X = 1;
            }
            // Limit Y velocity
            if (this.Velocity.Y > 3)
            {
                this.Velocity.Y = 3;
            }
            else if (this.Velocity.Y < -3)
            {
                this.Velocity.Y = -3;
            }
        }
    }
}
