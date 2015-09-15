using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShip.logic
{
    public class Ship
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
        public double Friction { get; set; }
        public bool SideHit { get; set; }
        public bool Grounded { get; set; }
        public double Bounce { get; set; }
        public bool GameOn { get; set; }

        public Ship()
        {
            Left = new Vector2(99, 4);
            Right = new Vector2(109, 4);
            Top = new Vector2(104, 0);
            Bottom = new Vector2(105, 10);
            X = 100;
            Y = 0;
            Velocity = new Vector2(0, 0);
            Width = 10;
            Height = 10;
            Fuel = 50;
            Bounce = 1;
            Friction = 1;
            SideHit = false;
            Grounded = false;
            GameOn = false;
        }

        public Ship(double x, double y)
        {
            Left = new Vector2(x, y + 4);
            Right = new Vector2(x + 9, y + 4);
            Top = new Vector2(x + 4, y);
            Bottom = new Vector2(x + 5, y + 10);
            X = x;
            Y = y;
            Velocity = new Vector2(0, 0);
            Width = 10;
            Height = 10;
            Fuel = 50;
            Bounce = 1;
            Friction = 1;
            SideHit = false;
            Grounded = false;
            GameOn = false;
        }

        public void CollisionCheck(PlatformsList platformList)
        {
            foreach (Platform platform in platformList.Platforms)
            {
                this.Grounded = platform.CollisionTest(this);
                if (this.Grounded == true)
                {
                    break;
                }
            }
        }

        public void CollisionCheckSides(PlatformsList platformList)
        {
            foreach (Platform platform in platformList.Platforms)
            {
                this.SideHit = platform.CollisionTestSides(this);
                if (this.SideHit == true)
                {
                    break;
                }
            }
        }

        public void GravityCheck(double gravity)
        {
            if (this.SideHit == true)
            {
                this.Velocity.X = this.Velocity.X * -1;
            }
            // If we are moving, and grounded, invert our velocity
            if (this.Grounded == true && (this.Velocity.Y > 0.2 || this.Velocity.Y < -0.2))
            {
                this.Velocity.Y = this.Velocity.Y * -1;
                return;
            }
                // Check if we are moving upwards and not grounded
                // also starts the intial downward Y velocity with <= 0
            else if (this.Velocity.Y <= 0 && this.Grounded == false)
            {
                this.Velocity.Y += gravity * 2 - (gravity / 2);

            }
                // Check if we are moving downwards
            else
            {
                // If we are moving downwards and not grounded
                if (this.Velocity.Y > 0 && this.Grounded == false)
                {
                    this.Velocity.Y += gravity - (gravity / 2);
                }
                // If our Y velocity is < 0.2 or > -0.2, and we are grounded, stop our Y movement
                if ((this.Velocity.Y < 0.2 || this.Velocity.Y > -0.2) && this.Grounded == true)
                {
                    this.Velocity.Y = 0;
                }
            }
        }

        public void MoveMe(Vector2 velocity)
        {
            this.Velocity.X += velocity.X;
            this.Velocity.Y += velocity.Y;
            if (this.GameOn == true)
            {
                this.Fuel -= 1;
                if (Fuel < 0)
                {
                    Fuel = 0;
                }
            }
        }

        public void MovePosition(Vector2 velocity)
        {
            // Move X using Velocity
            this.X += velocity.X;
            this.Left.X += velocity.X;
            this.Right.X += velocity.X;
            this.Top.X += velocity.X;
            this.Bottom.X += velocity.X;
            // Move Y using Velocity
            this.Y += velocity.Y;
            this.Left.Y += velocity.Y;
            this.Right.Y += velocity.Y;
            this.Top.Y += velocity.Y;
            this.Bottom.Y += velocity.Y;
        }

        public void TerminalVelocityCheck()
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

        public void Move()
        {
            // Add veloicty to position
            this.MovePosition(new Vector2(0, this.Velocity.Y));
            // Add veloicty to position
            this.MovePosition(new Vector2(this.Velocity.X, 0));
        }

        public void FrictionCheck()
        {
            if (this.Velocity.X < 0.01 && this.Velocity.X > -0.01)
            {
                this.Velocity.X = 0;
                return;
            }
            // Check if we are moving right
            if (this.Velocity.X > 0)
            {

                if ((this.Velocity.X > 0.01 || this.Velocity.X < -0.01) && this.Grounded == false)
                {
                    if (Fuel < 1)
                    {
                        this.Velocity.X -= this.Friction / 400;
                    }
                    else
                    {
                        this.Velocity.X -= this.Friction / 800;
                    }
                }
                else
                {
                    this.Velocity.X -= this.Friction / 40;
                }
            }
            // Check if we are moving left
            else if (this.Velocity.X < 0)
            {

                // Friction
                if (this.Velocity.X < -0.01 && this.Grounded == false)
                {
                    if (Fuel < 1)
                    {
                        this.Velocity.X += this.Friction / 400;
                    }
                    else
                    {
                        this.Velocity.X += this.Friction / 800;
                    }
                }
                else
                {
                    this.Velocity.X += this.Friction / 40;
                }
            }
        }
    }
}
