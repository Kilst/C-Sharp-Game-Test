using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShipTest.logic
{
    public class Platform : GameObject
    {
        public Platform(Vector2 position)
        {
            this.Position = position;
            this.Width = 100;
            this.Height = 30;
            this.TopLeft = new Vector2(0 + position.X, 0 + position.Y);
            this.TopRight = new Vector2(Width + position.X, 0 + position.Y);
            this.BottomLeft = new Vector2(0 + position.X, Height + position.Y);
            this.BottomRight = new Vector2(Width + position.X, Height + position.Y);
            this.Coords = new List<Vector2>();
            this.GetCoords();
            this.Top = new List<Vector2>();
            this.Bottom = new List<Vector2>();
            this.Left = new List<Vector2>();
            this.Right = new List<Vector2>();
            this.GetTopCoords();
            this.GetBottomCoords();
            this.GetLeftCoords();
            this.GetRightCoords();
        }
    }
}
