using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShipTest.logic
{
    public abstract class GameObject
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Vector2 Position { get; set; } /* Not used in calcs from what I remember */
        public Vector2 Velocity { get; set; }
        public List<Vector2> Coords { get; set; }
        public readonly double Gravity = 0.1; /* Should really set gravity and friction elsewhere */
        public readonly double Friction = 1; /* Should really set gravity and friction elsewhere */
        public List<Vector2> Top { get; set; }
        public List<Vector2> Bottom { get; set; }
        public List<Vector2> Left { get; set; }
        public List<Vector2> Right { get; set; }


        public void AddVelocity(Vector2 velocity)
        {
            this.Velocity.X += velocity.X;
            this.Velocity.Y += velocity.Y;
        }

        public virtual void Move()
        {
            // Add veloicty to X position
            this.MovePosition(new Vector2(0, this.Velocity.Y));
            // Add veloicty to Y position
            this.MovePosition(new Vector2(this.Velocity.X, 0));
        }

        public virtual void MovePosition(Vector2 velocity)
        {
            foreach (Vector2 item in this.Coords)
            {
                item.AddVector2(velocity);
            }
            // Move X using Velocity
            this.Position.X += velocity.X;
            // Move Y using Velocity
            this.Position.Y += velocity.Y;
        }

        public void GetTopCoords()
        {
            for (int i = 0; i <= Width; i++)
            {
                this.Top.Add(new Vector2(Position.X + i, Position.Y));
            }
        }

        public void GetBottomCoords()
        {
            for (int i = 0; i <= Width; i++)
            {
                this.Bottom.Add(new Vector2(Position.X + i, Position.Y + Height));
            }
        }

        public void GetLeftCoords()
        {
            for (int i = 2; i < Height-2; i++)
            {
                this.Left.Add(new Vector2(Position.X, Position.Y + i));
            }
        }

        public void GetRightCoords()
        {
            for (int i = 2; i < Height-2; i++)
            {
                this.Right.Add(new Vector2(Position.X + Width, Position.Y + i));
            }
        }

        public void GetCoords() /* Can't differentiate boundaries */
        {
            // 2 for loops to get co-ords
            for (int y = 0; y <= Height; y++)
            {
                for (int x = 0; x <= Width; x++)
                {
                    if (y == 0 || y == Height)
                    {
                        this.Coords.Add(new Vector2(x + Position.X, y + Position.Y));
                    }
                    else
                    {
                        if (x == 0 || x == Width)
                        {
                            this.Coords.Add(new Vector2(x + Position.X, y + Position.Y));
                        }
                    }
                }
            }
        }
    }
}
