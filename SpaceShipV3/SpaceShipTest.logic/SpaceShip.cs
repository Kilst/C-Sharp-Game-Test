using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShipTest.logic
{
    public class SpaceShip : GameObject, iPlayerPhysics
    {
        public bool IsGrounded { get; set; }
        

        public SpaceShip()
        {
            this.Position = new Vector2(100,20);
            this.Width = 10;
            this.Height = 10;
            this.TopLeft = new Vector2(0, 0);
            this.TopRight = new Vector2(Width, 0);
            this.BottomLeft = new Vector2(0, Height);
            this.BottomRight = new Vector2(Width, Height);
            this.IsGrounded = false;
            this.Coords = new List<Vector2>();
            this.GetCoords();
            this.Velocity = new Vector2(0, 1);
        }

        //public void CollisionCheckY(GameObjectList list)
        //{
        //    // Loop through the list of GameObjects,
        //    // and compare each one's co-ords to our ships co-ords
        //    foreach (GameObject obj in list.ObjectList)
        //    {
        //        // Make sure we don't compare our own co-ords to ourself
        //        if (obj != this)
        //        {
        //            foreach (Vector2 objCoord in obj.Coords)
        //            {
        //                foreach (Vector2 shipCoord in this.Coords)
        //                {
        //                    // Do Collision Check
        //                    // If any of our ships co-ords == any collision objects co-ords
        //                    if ((int)shipCoord.X == (int)objCoord.X-1 && (int)shipCoord.Y == (int)objCoord.Y)
        //                    {
        //                        this.IsGrounded = true;
        //                        //this.Velocity.X = this.Velocity.X * -1;
        //                        // Invert Y velocity (so we "bounce" back)
        //                        this.Velocity.Y = this.Velocity.Y * -1;
        //                        return;
        //                    }
        //                    else
        //                    {
        //                        this.IsGrounded = false;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}


        public void CollisionCheck(GameObjectList list)
        {
            // Loop through the list of GameObjects,
            // and compare each one's co-ords to our ships co-ords
            foreach (GameObject obj in list.ObjectList)
            {
                // Make sure we don't compare our own co-ords to ourself
                if (obj != this)
                {
                    foreach (Vector2 objCoord in obj.Left)
                    {
                        foreach (Vector2 shipCoord in this.Coords)
                        {
                            // Do Collision Check
                            // If any of our ships co-ords == any collision objects co-ords
                            if ((int)shipCoord.X == (int)objCoord.X && (int)shipCoord.Y == (int)objCoord.Y)
                            {
                                //this.Velocity.X = this.Velocity.X * -1;
                                // Invert Y velocity (so we "bounce" back)
                                this.Velocity.X = this.Velocity.X * -1;
                                return;
                            }
                        }
                    }

                    foreach (Vector2 objCoord in obj.Right)
                    {
                        foreach (Vector2 shipCoord in this.Coords)
                        {
                            // Do Collision Check
                            // If any of our ships co-ords == any collision objects co-ords
                            if ((int)shipCoord.X == (int)objCoord.X && (int)shipCoord.Y == (int)objCoord.Y)
                            {
                                //this.Velocity.X = this.Velocity.X * -1;
                                // Invert Y velocity (so we "bounce" back)
                                this.Velocity.X = this.Velocity.X * -1;
                                return;
                            }
                        }
                    }

                    foreach (Vector2 objCoord in obj.Top)
                    {
                        foreach (Vector2 shipCoord in this.Coords)
                        {
                            // Do Collision Check
                            // If any of our ships co-ords == any collision objects co-ords
                            if ((int)shipCoord.X == (int)objCoord.X && (int)shipCoord.Y == (int)objCoord.Y)
                            {
                                this.IsGrounded = true;
                                //this.Velocity.X = this.Velocity.X * -1;
                                // Invert Y velocity (so we "bounce" back)
                                this.Velocity.Y = this.Velocity.Y * -1;
                                return;
                            }
                            else
                            {
                                this.IsGrounded = false;
                            }
                        }
                    }

                    foreach (Vector2 objCoord in obj.Bottom)
                    {
                        foreach (Vector2 shipCoord in this.Coords)
                        {
                            // Do Collision Check
                            // If any of our ships co-ords == any collision objects co-ords
                            if ((int)shipCoord.X == (int)objCoord.X && (int)shipCoord.Y == (int)objCoord.Y)
                            {
                                //this.Velocity.X = this.Velocity.X * -1;
                                // Invert Y velocity (so we "bounce" back)
                                this.Velocity.Y = this.Velocity.Y * -1;
                                return;
                            }
                            else
                            {
                                this.IsGrounded = false;
                            }
                        }
                    }

                }
            }
        }


        //public void CollisionCheckX(GameObjectList list)
        //{
        //    // Loop through the list of GameObjects,
        //    // and compare each one's co-ords to our ships co-ords
        //    foreach (GameObject obj in list.ObjectList)
        //    {
        //        // Make sure we don't compare our own co-ords to ourself
        //        if (obj != this)
        //        {
        //            foreach (Vector2 objCoord in obj.Coords)
        //            {
        //                foreach (Vector2 shipCoord in this.Coords)
        //                {
        //                    // Do Collision Check
        //                    // If any of our ships co-ords == any collision objects co-ords
        //                    if ((int)shipCoord.X == (int)objCoord.X && (int)shipCoord.Y == (int)objCoord.Y)
        //                    {
        //                        this.IsGrounded = true;
        //                        //this.Velocity.X = this.Velocity.X * -1;
        //                        // Invert Y velocity (so we "bounce" back)
        //                        this.Velocity.X = this.Velocity.X * -1;
        //                        return;
        //                    }
        //                    else
        //                    {
        //                        this.IsGrounded = false;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        public void GravityCheck()
        {
            if (this.IsGrounded == false)
            {
                // Add Gravity
                this.Velocity.Y += this.Gravity;
            }
        }
        public void FrictionCheck()
        {
            if (this.IsGrounded == false)
            {
                // Add low friction

                // If our X velocity is > 0.01
                if (this.Velocity.X > 0.01)
                {
                    // Add low friction
                    this.Velocity.X -= this.Friction / 400;
                }
                // If our X velocity is < -0.01
                else if (this.Velocity.X < -0.01)
                {
                    // Add low friction
                    this.Velocity.X += this.Friction / 400;
                }
                else
                {
                    // Stop our X velocity
                    this.Velocity.X = 0;
                }

                // Y velocity
                if (this.Velocity.Y > 0)
                {
                    this.Velocity.Y -= this.Friction / 40;
                }
                else if (this.Velocity.Y < 0)
                {
                    this.Velocity.Y += this.Friction / 40;
                }
            }
            // If we are on the ground, Y friction doesn't matter
            else
            {
                if (this.Velocity.Y > -0.2 && this.Velocity.Y < 0.2)
                {
                    this.Velocity.Y = 0;
                }

                // Add high friction
                if (this.Velocity.X > 0.01)
                {
                    this.Velocity.X -= this.Friction / 40;
                }
                else if (this.Velocity.X < -0.01)
                {
                    this.Velocity.X += this.Friction / 40;
                }
                else
                {
                    this.Velocity.X = 0;
                }
            }
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
    }
}
