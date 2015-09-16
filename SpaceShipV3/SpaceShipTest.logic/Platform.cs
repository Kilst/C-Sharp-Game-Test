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

        public Platform(Vector2 position, int width, int height)
        {
            this.Position = position;
            this.Width = width;
            this.Height = height;
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
