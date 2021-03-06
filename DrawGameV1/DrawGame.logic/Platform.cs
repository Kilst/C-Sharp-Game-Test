﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawGame.logic
{
    public class Platform
    {
        public Vector2 TopLeft { get; set; }
        public Vector2 TopRight { get; set; }
        public Vector2 BottomLeft { get; set; }
        public Vector2 BottomRight { get; set; }

        public Platform()
        {
            TopLeft = new Vector2(80, 180);
            TopRight = new Vector2(164, 180);
            BottomLeft = new Vector2(80, 208);
            BottomRight = new Vector2(164, 208);
        }

        public double CollisionCheck(Box box, double velo)
        {
            if (box.Bottom.Y >= 179 && box.Bottom.Y < 210 && (box.Bottom.X > 80 && box.Bottom.X < 164) && (velo > -0.2 && box.Velocity.Y < 0.2) && box.Falling == false)
            {
                velo = 0;

                box.Y = 170;
                box.Left.Y = 175;
                box.Right.Y = 175;
                box.Top.Y = 170;
                box.Bottom.Y = 180;
            }


            if ((box.Bottom.Y >= TopLeft.Y && box.Bottom.Y < BottomLeft.Y && (box.Bottom.X > TopLeft.X && box.Bottom.X < TopRight.X) && box.Falling == true))
            {
                box.Jumping = false;

                if (box.Velocity.Y >= 0)
                {
                    velo = velo - (velo * 2) * box.Bounce;
                    box.Falling = true;
                }
                else if (box.Velocity.Y < 0)
                {
                    velo = velo - (velo * 2) * box.Bounce;
                    box.Falling = true;
                }
                if (box.Velocity.Y > -0.2)
                {
                    box.Falling = false;
                }
            }

            else if (box.Top.Y >= TopLeft.Y && box.Top.Y < BottomLeft.Y && (box.Top.X > TopLeft.X && box.Top.X < TopRight.X))
            {
                box.Jumping = false;



                if (box.Velocity.Y >= 0)
                {
                    velo = velo - (velo * 2) * box.Bounce;
                    box.Falling = true;
                }
                else if (box.Velocity.Y < 0)
                {
                    velo = velo - (velo * 2) * box.Bounce;
                    box.Falling = true;
                }
                if (box.Velocity.Y > -0.2)
                {
                    box.Falling = false;
                }
            }
            return velo;
        }

        public double CollisionCheckX(Box box, double velo)
        {
            //if (box.Left.Y >= TopLeft.Y && box.Left.Y < BottomLeft.Y && (box.Left.X > TopLeft.X && box.Left.X < TopRight.X))
            //{
            //    box.Jumping = false;

            //    velo = velo - (velo * 2) * box.Bounce;
            //    box.Falling = true;
            //}
            //else if (box.Right.Y >= TopLeft.Y && box.Right.Y < BottomLeft.Y && (box.Right.X > TopLeft.X && box.Right.X < TopRight.X))
            //{
            //    box.Jumping = false;

            //    velo = velo - (velo * 2) * box.Bounce;
            //    box.Falling = true;
            //}

            return velo;
        }

        public bool CollisionTest(Box box)
        {
            if (box.Bottom.Y >= TopLeft.Y && box.Bottom.Y <= BottomLeft.Y && (box.Bottom.X >= TopLeft.X && box.Bottom.X <= TopRight.X) && box.Falling == true)
            {
                return true;
            }
            else if (box.Bottom.Y >= TopLeft.Y && box.Bottom.Y <= BottomLeft.Y && (box.Bottom.X >= TopLeft.X && box.Bottom.X <= TopRight.X) && box.Velocity == new Vector2(box.Velocity.X, 0))
            {
                box.Falling = true;
                return false;
            }
            else
            {
                return false;
            }
        }

        public bool CrashCheck(Box box)
        {
            if (box.Bottom.Y > 280)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //box.Bottom.Y >= 180 && box.Bottom.Y < 210 && (box.Bottom.X > 80 && box.Bottom.X < 164)
    }
}
