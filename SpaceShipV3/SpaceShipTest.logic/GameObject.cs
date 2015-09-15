using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShipTest.logic
{
    public abstract class GameObject
    {
        public Vector2 TopLeft { get; set; }
        public Vector2 TopRight { get; set; }
        public Vector2 BottomLeft { get; set; }
        public Vector2 BottomRight { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public List<Vector2> Coords { get; set; }
        public readonly double Gravity = 0.1;
        public readonly double Friction = 1;
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
            // Add veloicty to position
            this.MovePosition(new Vector2(0, this.Velocity.Y));
            // Add veloicty to position
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
            this.TopLeft.X += velocity.X;
            this.TopRight.X += velocity.X;
            this.BottomLeft.X += velocity.X;
            this.BottomRight.X += velocity.X;
            // Move Y using Velocity
            this.Position.Y += velocity.Y;
            this.TopLeft.Y += velocity.Y;
            this.TopRight.Y += velocity.Y;
            this.BottomLeft.Y += velocity.Y;
            this.BottomRight.Y += velocity.Y;
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

        public void GetCoords()
        {
            // 2 for loops to get co-ords
            for (int y = 0; y <= Height; y++)
            {
                for (int x = 0; x <= Width; x++)
                {
                    if (y == 0 || y == Height)
                    {
                        this.Coords.Add(new Vector2(x+Position.X, y+Position.Y));
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
