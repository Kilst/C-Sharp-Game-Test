using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShip.logic
{
    public class Platform
    {
        public Vector2 TopLeft { get; set; }
        public Vector2 TopRight { get; set; }
        public Vector2 BottomLeft { get; set; }
        public Vector2 BottomRight { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Platform()
        {
            Width = 84;
            Height = 28;

            TopLeft = new Vector2(80, 180);
            TopRight = new Vector2(164, 180);
            BottomLeft = new Vector2(80, 208);
            BottomRight = new Vector2(164, 208);
        }

        public Platform(Vector2 topLeft)
        {
            Width = 84;
            Height = 28;

            TopLeft = topLeft;
            // Just use width and height to create new platform vectors from topLeft
            TopRight = new Vector2(this.Width + topLeft.X, topLeft.Y);
            BottomLeft = new Vector2(topLeft.X, this.Height + topLeft.Y);
            BottomRight = new Vector2(this.Width + topLeft.X, this.Height + topLeft.Y);

        }

        public double CollisionCheck(Ship box, double velo)
        {
            // Stop Y movement id we are on the platform and not moving fast at all
            if (box.Bottom.Y >= this.TopLeft.Y - 1 && box.Bottom.Y < this.BottomLeft.Y && (box.Bottom.X > this.TopLeft.X && box.Bottom.X < this.TopRight.X) && (velo > -0.2 && box.Velocity.Y < 0.2) && box.Grounded == false)
            {
                velo = 0;

                box.Y = this.TopLeft.Y - 10;
                box.Left.Y = this.TopLeft.Y - 5;
                box.Right.Y = this.TopLeft.Y - 5;
                box.Top.Y = this.TopLeft.Y - 10;
                box.Bottom.Y = this.TopLeft.Y;
            }

            // Returns our rebound velocity (just inverts velocity)
            if ((box.Bottom.Y >= this.TopLeft.Y && box.Bottom.Y < this.BottomLeft.Y && (box.Bottom.X > this.TopLeft.X && box.Bottom.X < this.TopRight.X) && box.Grounded == false))
            {

                if (box.Velocity.Y != 0)
                {
                    velo = velo - (velo * 2) * box.Bounce;
                    box.Grounded = true;
                }

                // Lets us bounce off the ground
                if (box.Velocity.Y > -0.2)
                {
                    box.Grounded = false;
                }
            }
            return velo;
        }

        public bool CollisionTest(Ship box)
        {

            // Check if we are colliding with the platform
            if (box.Bottom.Y >= TopLeft.Y && box.Bottom.Y <= BottomLeft.Y && (box.Bottom.X >= TopLeft.X && box.Bottom.X <= TopRight.X))
            {
                //if (box.Bottom.Y > TopLeft.Y)
                //{
                //    double offset = box.Bottom.Y - TopLeft.Y;
                //    box.Left.Y -= offset;
                //    box.Right.Y -= offset;
                //    box.Top.Y -= offset;
                //    box.Bottom.Y -= offset;
                //}
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CollisionTestSides(Ship box)
        {
            if (box.Left.Y >= TopLeft.Y && box.Left.Y <= BottomLeft.Y && (box.Left.X >= TopLeft.X && box.Left.X <= TopRight.X))
            {
                return true;
            }
            else if (box.Right.Y >= TopLeft.Y && box.Right.Y <= BottomLeft.Y && (box.Right.X >= TopLeft.X && box.Right.X <= TopRight.X))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
